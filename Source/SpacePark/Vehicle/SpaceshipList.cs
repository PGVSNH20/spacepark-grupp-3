using System.Collections.Generic;

namespace SpacePark
{
    public partial class SpaceshipList
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public object Previous { get; set; }
        public List<Spaceship> Results { get; set; }
    }
}
