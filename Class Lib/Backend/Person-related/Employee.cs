﻿using Class_Lib.Backend.Person_related;
using Class_Lib.Backend.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Class_Lib
{
    public class Employee : Person
    {
        public uint? WorkplaceID { get; internal set; } // workplace ID (for db purposes)
        public BaseLocation? Workplace { get; set; } // current workplace of the employee
        public User? User { get; set; } // user account of the employee (if any)
        public List<BaseLocation>? ManagedLocations { get; set; } = new();

        internal Employee() : base() { }

        public Employee(string name, string surname, string phoneNumber, string email, BaseLocation? workplace, List<BaseLocation>? managedLocations = null)
            : base(name, surname, phoneNumber, email)
        {
            Workplace = workplace;

            if (workplace != null)
                WorkplaceID = workplace.ID; // set the workplace ID for db purposes
            else
                WorkplaceID = null; // set to null if no workplace is assigned

            if(managedLocations != null)
                ManagedLocations = managedLocations;
        }

        public Employee(string name, string surname, string phoneNumber, string email, string position, BaseLocation? workplace, List<BaseLocation>? managedLocations = null)
            : base(name, surname, phoneNumber, email)
        {
            Workplace = workplace;
            //Position = position;
            if (workplace != null)
                WorkplaceID = workplace.ID; // set the workplace ID for db purposes
            else
                WorkplaceID = null; // set to null if no workplace is assigned

            if (managedLocations != null)
                ManagedLocations = managedLocations;
        }
    }
}
