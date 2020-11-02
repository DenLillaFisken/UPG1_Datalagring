using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UPG1_Datalagring.Models
{
    public class Person
    {
        public Person()
        {

        }
        public Person(string name, int age, string city)
        {
            Name = name;
            Age = age;
            City = city;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }
}
