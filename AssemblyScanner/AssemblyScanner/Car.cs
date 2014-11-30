using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyScanner
{
    public class Car
    {
        public int Speed { get; set; }
        public string Name { get; set; }

        public Car() : this("MyCar", 60) { }

        public Car(string name, int speed)
        {
            Name = name;
            Speed = speed;
        }
    }

}
