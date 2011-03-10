namespace PromptGarten.Domain.Model
{
    public struct LectiveSemester
    {
        public LectiveSemester(SemesterPeriod semester, LectiveYear lectiveYear)
            : this()
        {
            Semester = semester;
            LectiveYear = lectiveYear;
        }

        public SemesterPeriod Semester { get; private set; }
        public LectiveYear LectiveYear { get; private set; }

        public bool Equals(LectiveSemester other)
        {
            return Equals(other.Semester, Semester) && other.LectiveYear.Equals(LectiveYear);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != typeof (LectiveSemester)) return false;
            return Equals((LectiveSemester) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Semester.GetHashCode()*397) ^ LectiveYear.GetHashCode();
            }
        }
    }
}