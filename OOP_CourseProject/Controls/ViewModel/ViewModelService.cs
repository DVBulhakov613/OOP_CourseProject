﻿using Class_Lib;
using Class_Lib.Backend.Package_related;
using Class_Lib.Backend.Person_related;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_CourseProject.Controls.ViewModel
{
    public static class ViewModelService
    {
        public static GenericInfoDisplayViewModel CreateViewModel(IEnumerable<object> models)
        {
            return new GenericInfoDisplayViewModel(models, ChooseViewModel);
        }

        private static IInfoProviderViewModel ChooseViewModel(object model)
        {
            return model switch
            {
                Package p => new PackageViewModel(p),
                Employee e => new EmployeeViewModel(e),
                PostalOffice po => new PostalOfficeViewModel(po),
                Warehouse w => new WarehouseViewModel(w),
                Delivery d => new DeliveryViewModel(d),
                Content c => new ContentViewModel(c),
                Client c => new ClientViewModel(c),
                _ => throw new NotSupportedException($"Не підтримуємий тип: {model.GetType().Name}")
            };
        }
    }
}
