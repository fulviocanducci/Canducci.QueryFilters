using Canducci.QueryFilters.ADO;
using Canducci.QueryFilters.Dapper.Tests.Database;
using Canducci.QueryFilters.Dapper.Tests.Models;
using Dapper;

namespace Canducci.QueryFilters.Dapper.Tests
{
    public class QueryFilterResultExtensionsTests
    {

        #region ADONET
        [Fact]
        public void ToDbParameters_ShouldExecuteQueryWithFilter()
        {
            // Arrange
            using (var database = new SqliteDatabaseFixture())
            {
                var filter = QueryFilter.Create()
                    .WhereNotNull("Name")
                    .WhereEquals("Name", "Fulvio")
                    .AndGreaterThan("Age", 18);

                using (var command = database.Connection.CreateCommand())
                {
                    var result = filter.Compile(command);
                    var sql = $"SELECT * FROM Users WHERE {result.Sql}";
                    foreach (var parameter in result.Parameters)
                    {
                        command.Parameters.Add(parameter);
                    }
                    command.CommandText = sql;
                    using (var reader = command.ExecuteReader())
                    {
                        Assert.True(reader.Read());
                        Assert.Equal("Fulvio", reader["Name"]);
                        Assert.Equal(35, (long)reader["Age"]);
                        Assert.False(reader.Read());
                    }
                }
            }
        }

        #endregion

        [Fact]
        public void ToDynamicParameters_ShouldExecuteQueryWithIn()
        {
            // Arrange
            using (var database = new SqliteDatabaseFixture())
            {
                var filter = QueryFilter.Create()
                    .WhereIn("Age", 28, 35, 42);

                var result = filter.Compile(CompileType.Dapper);

                // Act
                var sql = $"SELECT * FROM Users WHERE {result.Sql}";
                var users = database.Connection.Query<User>(sql, result.Parameters).AsList();

                // Assert
                Assert.Equal(3, users.Count);
                Assert.Contains(users, user => user.Name == "Fulvio");
                Assert.Contains(users, user => user.Name == "Maria");
                Assert.Contains(users, user => user.Name == "João");
            }
        }


        [Fact]
        public void ToDynamicParameters_ShouldExecuteQueryWithContains()
        {
            // Arrange
            using (var database = new SqliteDatabaseFixture())
            {
                var filter = QueryFilter.Create()
                    .WhereContains("Name", "a");

                var result = filter.Compile(CompileType.Dapper);

                // Act
                var sql = $"SELECT * FROM Users WHERE {result.Sql}";
                var users = database.Connection.Query<User>(sql, result.Parameters).AsList();

                // Assert
                Assert.Equal(3, users.Count);
            }
        }


        [Fact]
        public void ToDynamicParameters_ShouldExecuteQueryWithBetween()
        {
            // Arrange
            using (var database = new SqliteDatabaseFixture())
            {
                var filter = QueryFilter.Create()
                    .WhereBetween("Age", 20, 35);

                var result = filter.Compile(CompileType.Dapper);
                // Act
                var sql = $"SELECT * FROM Users WHERE {result.Sql}";
                var users = database.Connection.Query<User>(sql, result.Parameters).AsList();

                // Assert
                Assert.Equal(5, users.Count);
                Assert.Contains(users, user => user.Name == "Fulvio");
                Assert.Contains(users, user => user.Name == "Maria");
                Assert.Contains(users, user => user.Name == "Ana");
            }
        }


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
                var filter = QueryFilter.Create().WhereEquals("Name", "Fulvio").AndGreaterThan("Age", 18);

                var result = filter.Compile();
                var sql = $"SELECT * FROM Users WHERE {result.Sql}";
                var users = database.Connection.Query<User>(sql, result.Parameters).AsList();

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
