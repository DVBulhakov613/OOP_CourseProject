﻿using System.Windows;
using Class_Lib;
using Class_Lib.Backend.Database.Repositories;
using Class_Lib.Backend.Person_related;
using Class_Lib.Backend.Services;
using Class_Lib.Database.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace OOP_CourseProject
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginButton.IsEnabled = false; // disabling the button first and foremost to prevent multiple clicks in an async operation
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            var userRepository = App.LoginHost.Services.GetRequiredService<UserRepository>();
            var roleService = App.LoginHost.Services.GetRequiredService<RoleService>();

            var user = await userRepository.GetByUsernameAsync(username);

            if (user == null || !PasswordHelper.VerifyPassword(password, user.PasswordHash))
            {
                MessageBox.Show("Недійсне ім'я користувача або пароль.", "Помилка авторизації", MessageBoxButton.OK, MessageBoxImage.Error);
                LoginButton.IsEnabled = true; // re-enable the button if login fails
                return;
            }

            await roleService.CachePermissionsAsync(user);

            var result = await userRepository.GetByCriteriaAsync(e => e.PersonID == user.Employee.ID);

            App.CurrentEmployee = result.First();
            

            DialogResult = true; // this is how App.xaml.cs will know login was successful
            Close();
            LoginButton.IsEnabled = true; // re-enabling once closed to allow login afterwards
        }

        /// <summary>
        /// Debug button for testing purposes. It bypasses the login process and sets a default user. Will be deleted when in prod.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DebugButton_Click(object sender, RoutedEventArgs e)
        {
            var username = "d.v.bulhakov";

            var userRepository = App.LoginHost.Services.GetRequiredService<UserRepository>();
            var roleService = App.LoginHost.Services.GetRequiredService<RoleService>();

            var user = await userRepository.GetByUsernameAsync(username);

            await roleService.CachePermissionsAsync(user);

            var result = await userRepository.GetByCriteriaAsync(e => e.PersonID == user.Employee.ID);

            App.CurrentEmployee = result.First();


            DialogResult = true; // this is how App.xaml.cs will know login was successful
            Close();
        }
    }
}
