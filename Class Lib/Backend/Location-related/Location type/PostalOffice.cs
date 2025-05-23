﻿using Class_Lib.Backend.Package_related;
using Class_Lib.Backend.Person_related;
using Class_Lib.Location_related.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Lib
{
    public class PostalOffice : Warehouse, IPackageHandler, ICustomerFacing
    {

        public bool HandlesPublicDropOffs { get; set; }
        public bool IsRegionalHQ { get; set; }
        internal PostalOffice() : base()
        {
        }
        public PostalOffice(Coordinates location, uint maxStorageCapacity, bool isAutomated, bool handlesPublicDropOffs, bool isRegionalHQ)
            : base(location, maxStorageCapacity, isAutomated)
        {
            HandlesPublicDropOffs = handlesPublicDropOffs;
            IsRegionalHQ = isRegionalHQ;
        }

        public Package CreatePackage(uint length, uint width, uint height, uint weight, 
            Client sender, Client receiver, PostalOffice sentFrom, PostalOffice sentTo, PackageType type)
        {
            return new Package(length, width, height, weight, sender, receiver, sentFrom, sentTo, type);
        }
    }
}
