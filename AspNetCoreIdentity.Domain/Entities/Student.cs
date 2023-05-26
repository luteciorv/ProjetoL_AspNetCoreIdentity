namespace AspNetCoreIdentity.Domain.Entities
{
    public class Student
    {
        public Student(string name, string email, int age, string course)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Age = age;
            Course = course;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public int Age { get; private set; }

        public string Course { get; private set; }
    }
}
