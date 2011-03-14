using System;
using PromptGarten.Common;

namespace PromptGarten.Domain.Model
{
    public class Teacher
    {
        private string _name;

        public Teacher(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value.IsNullOrEmpty())
                {
                    throw new ArgumentException("Name");
                }

                _name = value;
            }
        }
    }
}