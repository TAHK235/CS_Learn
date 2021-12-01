﻿using System;

namespace SerializePeopleXML
{
    [Serializable]
    class Person
    {
        public string Name;
        public DateTime DateOfBirth;
        [NonSerialized] public int Age;

        public Person(string name, DateTime dateOfBirth)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            CalculateAge();
        }

        public Person()
        {
        }

        public override string ToString()
        {
            return Name + " was born on " + DateOfBirth.ToShortDateString() + " and is " + Age.ToString() +
                   " years old.";
        }

        private void CalculateAge()
        {
            Age = DateTime.Now.Year - DateOfBirth.Year;
            
            if (DateOfBirth.AddYears(DateTime.Now.Year - DateOfBirth.Year) > DateTime.Now)
            {
                Age--;
            }
        }
    }
}