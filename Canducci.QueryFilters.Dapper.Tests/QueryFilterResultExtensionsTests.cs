using Canducci.QueryFilters.Dapper.Tests.Database;
using Canducci.QueryFilters.Dapper.Tests.Models;
using Dapper;

namespace Canducci.QueryFilters.Dapper.Tests
{
    public class QueryFilterResultExtensionsTests
    {
        [Fact]
        public void ToDynamicParameters_ShouldExecuteQueryWithFilter()
        {
            // Arrange
            using (var database = new SqliteDatabaseFixture())
            {
                var filter = QueryFilter.Create().WhereEquals("Name", "Fulvio");

                var result = new QueryFilterCompiler().Compile(filter);
                var parameters = result.ToDynamicParameters();

                // Act
                var sql = $"SELECT * FROM Users WHERE {result.Sql}";
                var users = database.Connection.Query<User>(sql, parameters).AsList();

                // Assert
                Assert.Single(users);
                Assert.Equal("Fulvio", users[0].Name);
                Assert.Equal(35, users[0].Age);
            }
        }

        [Fact]
        public void ToDynamicParameters_ShouldExecuteQueryWithFilterConstructor()
        {
            // Arrange
            using (var database = new SqliteDatabaseFixture())
            {
                var filter = QueryFilter.Create(c => c.WhereEquals("Name", "Fulvio"));

                var result = new QueryFilterCompiler().Compile(filter);
                var parameters = result.ToDynamicParameters();

                // Act
                var sql = $"SELECT * FROM Users WHERE {result.Sql}";
                var users = database.Connection.Query<User>(sql, parameters).AsList();

                // Assert
                Assert.Single(users);
                Assert.Equal("Fulvio", users[0].Name);
                Assert.Equal(35, users[0].Age);
            }
        }

        [Fact]
        public void ToDynamicParameters_ShouldCompileFilterAndExecuteQuery()
        {
            // Arrange
            using (var database = new SqliteDatabaseFixture())
            {
                var filter = QueryFilter.Create()
                    .WhereEquals("Name", "Fulvio")
                    .AndGreaterThan("Age", 18);

                var parameters = filter.ToDynamicParameters();
                var result = filter.Compile();

                // Act
                var sql = $"SELECT * FROM Users WHERE {result.Sql}";
                var users = database.Connection.Query<User>(sql, parameters).AsList();

                // Assert
                Assert.Single(users);
                Assert.Equal("Fulvio", users[0].Name);
                Assert.Equal(35, users[0].Age);
            }
        }

        [Fact]
        public void ToDynamicParameters_ShouldExecuteQueryWithGroup()
        {
            // Arrange
            using (var database = new SqliteDatabaseFixture())
            {
                var filter = QueryFilter
                    .Create(group =>
                    {
                        group.WhereGreaterThan("Age", 30);
                        group.Group(group =>
                        {
                            group.AndEquals("Active", true);
                            group.OrEquals("Active", false);
                        });
                    });

                var result = new QueryFilterCompiler().Compile(filter);
                var parameters = result.ToDynamicParameters();

                // Act
                var sql = $"SELECT * FROM Users WHERE {result.Sql}";
                var users = database.Connection.Query<User>(sql, parameters).AsList();

                // Assert
                Assert.Equal(2, users.Count);
                Assert.Contains(users, user => user.Name == "Fulvio");
            }
        }
    }
}
