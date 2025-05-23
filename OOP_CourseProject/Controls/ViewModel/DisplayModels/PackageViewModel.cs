﻿using Class_Lib.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Class_Lib;
using Class_Lib.Backend.Package_related;
using Class_Lib.Backend.Services;

namespace OOP_CourseProject.Controls.ViewModel
{
    public class PackageViewModel : IInfoProviderViewModel
    {
        public ObservableCollection<InfoSection> InfoSections { get; } = [];

        public PackageViewModel(Package package)
        {
            //InfoSections.Add(new InfoSection
            //{
            //    SectionTitle = "Інформація відправника",
            //    InfoItems = new List<InfoItem>
            //    {
            //        new() { Label = "Ім'я", Value = $"{package.Sender.FirstName} {package.Sender.Surname}" },
            //        new() { Label = "Телефон", Value = package.Sender.PhoneNumber },
            //        new() { Label = "Email", Value = package.Sender.Email }
            //    }
            //});

            //InfoSections.Add(new InfoSection
            //{
            //    SectionTitle = "Інформація отримувача",
            //    InfoItems = new List<InfoItem>
            //    {
            //        new() { Label = "Ім'я", Value = $"{package.Receiver.FirstName} {package.Receiver.Surname}" },
            //        new() { Label = "Телефон", Value = package.Receiver.PhoneNumber },
            //        new() { Label = "Email", Value = package.Receiver.Email }
            //    }
            //});

            InfoSections.Add(new InfoSection
            {
                SectionTitle = $"Інформація посилки {package.ID}",
                InfoItems = new List<InfoItem>
                {
                    new() { Label = "Дата оформлення", Value = package.CreatedAt.ToString("HH:mm, dd-MM-yyyy") },
                    new() { Label = "Розмір", Value = $"{package.Length} x {package.Width} x {package.Height} см" },
                    new() { Label = "Вага", Value = $"{package.Weight:C2} кг" },
                    new() { Label = "Тип", Value = $"{package.Type.GetDescription()}" }
                }
            });

            //InfoSections.Add(new InfoSection
            //{
            //    SectionTitle = "Інформація доставки",
            //    InfoItems = new List<InfoItem>
            //    {
            //        new() { Label = "Пункт відправлення", Value = $"{package.SentFromID}" },
            //        new() { Label = "Пункт отримання", Value = $"{package.SentToID}" },
            //        //new() { Label = "Час доставки", Value = $"{}" } // need to figure out how to best include it first
            //    }
            //});
        }
    }

}
