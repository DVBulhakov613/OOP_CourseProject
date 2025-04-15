﻿using Class_Lib.Backend.Person_related;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Lib.Location_related.Interfaces
{
    public interface IPackageHandler
    {
        Package CreatePackage
            (uint packageID, uint length, uint width, uint height, uint weight, Client sender, Client receiver,
            PostalOffice sentFrom, PostalOffice sentTo, Coordinates currentLocation, List<Content> declaredContent,
            PackageType type);
    }
}
