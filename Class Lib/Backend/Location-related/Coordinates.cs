﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Lib
{
    public class Coordinates
    {
        private double? _latitude;
        private double? _longitude;
        public double? Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                if(value < -90 || value > 90)
                    throw new ArgumentOutOfRangeException("Latitude must be between -90 and 90");
                _latitude = value;
            }
        }
        public double? Longitude
        {
            get
            {
                return _longitude;
            }
            set
            {
                if(value < -180 || value > 180)
                    throw new ArgumentOutOfRangeException("Longitude must be between -180 and 180");
                _longitude = value;
            }
        }
        public string? Address
        { get; set; }
        public string Region
        { get; set; }
        protected Coordinates()
        {
            RowVersion = Array.Empty<byte>();
        }
        public Coordinates(double? latitude, double? longitude, string? address, string region)
        {
            Latitude = latitude;
            Longitude = longitude;
            Address = address;
            Region = region;
            RowVersion = Array.Empty<byte>();
        }

        [Timestamp] // concurrency token property
        public byte[] RowVersion { get; set; }
    }
}
