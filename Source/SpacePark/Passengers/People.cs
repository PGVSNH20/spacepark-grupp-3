using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacePark
{
    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public List<string> Starships { get; set; }
        public string Url { get; set; }
    }
}
