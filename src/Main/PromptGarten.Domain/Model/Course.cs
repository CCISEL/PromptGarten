namespace PromptGarten.Domain.Model
{
    public class Course
    {
        public Course(string tag, string name, int responsibleTeacher)
        {
            Tag = tag;
            Name = name;
            ResponsibleTeacher = responsibleTeacher;
        }

        public string Tag { get; private set; }
        public string Name { get; private set; }
        public int ResponsibleTeacher { get; private set; }
    }
}