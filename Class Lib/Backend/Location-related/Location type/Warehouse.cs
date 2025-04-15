﻿using Class_Lib.Location_related.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Lib
{
    public class Warehouse : BaseLocation, IStorage, IDeliveryHandler
    {

        public uint MaxStorageCapacity { get; set; }
        public bool IsAutomated { get; set; }
        public List<Package> StoredPackages { get; set; } = [];

        public bool IsFull => throw new NotImplementedException();

        protected internal Warehouse() : base()
        {
        }

        public Warehouse(uint maxStorageCapacity, bool isAutomated) : base()
        {
            MaxStorageCapacity = maxStorageCapacity;
            IsAutomated = isAutomated;
        }

        public Warehouse(uint maxStorageCapacity, bool isAutomated, List<Package> storedPackages) : base()
        {
            MaxStorageCapacity = maxStorageCapacity;
            StoredPackages = storedPackages;
        }

        public void StorePackage(Package package) // adds a package to a warehouse's storage
        {
            if (StoredPackages.Count == MaxStorageCapacity)
                throw new InvalidOperationException("Склад повний.");
            else
                StoredPackages.Add(package);
        }
        public void RemovePackage(Package package) // removes a package from a warehouse's storage
        {
            if (!StoredPackages.Contains(package))
                throw new InvalidOperationException("Видалення неможливо: Посилку не знайдено на складі.");
            else
                StoredPackages.Remove(package);
        }
        public void ClearStorage() // clears the storage of a warehouse
        {
            StoredPackages.Clear();
        }

        public void SendPackage(Package package, PostalOffice sentFrom, PostalOffice sentTo) // sends a package from one postal office to another
        {
            if (StoredPackages.Contains(package))
            {
                StoredPackages.Remove(package);
                sentTo.StoredPackages.Add(package); // temporary thing as i cant really simulate the entire delivery of a package
            }
            else
            {
                throw new InvalidOperationException("Відправлення неможливо: Посилку не знайдено на складі.");
            }
        }

        public void ReceivePackage(Package package, PostalOffice sentFrom, PostalOffice sentTo)
        {
            throw new NotImplementedException();
        }
    }
}
