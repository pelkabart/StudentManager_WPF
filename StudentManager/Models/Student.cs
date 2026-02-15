using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManager.Models
{
    public class Student
    {



        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public float AvgGrade { get; set; }
        public int Year {  get; set; }

        public override string ToString()
        {
            return $"{Name} {Surname} {Id} | Rok: {Year}";
        }

    }
}
