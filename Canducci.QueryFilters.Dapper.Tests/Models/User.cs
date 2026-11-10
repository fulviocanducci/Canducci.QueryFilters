namespace Canducci.QueryFilters.Dapper.Tests.Models
{
    public class User
    {
        public User()
        {

        }
        public User(int id, string name, int age, bool active)
        {
            Id = id;
            Name = name;
            Age = age;
            Active = active;
        }

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public bool Active { get; set; }
    }
}
