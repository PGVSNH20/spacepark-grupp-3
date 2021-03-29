using System.Collections.Generic;

namespace SpacePark
{
    public partial class PeopleList
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public object Previous { get; set; }
        public List<People> Results { get; set; }
    }
}


