using System;
using System.Collections.Generic;

namespace pizza_api.Models
{
    public class Pizza : IEquatable<Pizza>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; }
        public string Size { get; set; }
        public int Slices { get; set; }
        public bool HasExtraCheese { get; set; }

        public bool Equals(Pizza other)
        {
            return Name.Equals(other.Name);
        }
    }
}
