using Dapper;
using Microsoft.Data.Sqlite;
namespace Canducci.QueryFilters.Dapper.Tests.Database
{
    public class SqliteDatabaseFixture : IDisposable
    {
        public SqliteConnection Connection { get; }

        public SqliteDatabaseFixture()
        {
            Connection = new SqliteConnection("Data Source=:memory:");
            Connection.Open();

            CreateDatabase();
        }

        private void CreateDatabase()
        {
            Connection.Execute(@"
                CREATE TABLE Users
                (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Age INTEGER NOT NULL,
                    Active INTEGER NOT NULL
                );
            ");

            Connection.Execute(@"
                INSERT INTO Users (Name, Age, Active) VALUES
                ('Fulvio', 35, 1),
                ('Maria', 28, 1),
                ('João', 42, 0),
                ('Ana', 22, 1),
                ('Pedro', 17, 0),
                ('Ronaldo', 30, 1),
                ('Sergio', 20, 1);
            ");
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
