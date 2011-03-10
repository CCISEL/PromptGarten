namespace PromptGarten.Domain.Model
{
    public struct LectiveYear
    {
        public LectiveYear(int startYear)
            : this()
        {
            StartYear = startYear;
        }

        public int StartYear { get; private set; }
        public int EndYear
        {
            get { return StartYear + 1; }
        }

        public bool Equals(LectiveYear other)
        {
            return other.StartYear == StartYear;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != typeof (LectiveYear)) return false;
            return Equals((LectiveYear) obj);
        }

        public override int GetHashCode()
        {
            return StartYear;
        }
    }
}