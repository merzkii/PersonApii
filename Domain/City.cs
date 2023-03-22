using PersonApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class City
    {
        public string Name { get; set; }
        public ICollection<Person> persons { get; set; }
    }
}
