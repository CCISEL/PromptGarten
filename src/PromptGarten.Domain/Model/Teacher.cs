namespace PromptGarten.Domain.Model
{
    public class Teacher
    {
        public Teacher(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; set; }
    }
}