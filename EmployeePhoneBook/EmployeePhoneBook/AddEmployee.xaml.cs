using EmployeePhoneBook.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace EmployeePhoneBook
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddEmployee : Page
    {
        public AddEmployee()
        {
            this.InitializeComponent();
        }

        

        private async void submitButton_Click(object sender, RoutedEventArgs e)
        {
            var employeeContact = new Models.EmployeeContact()
            {
                EmailAddress = emailAddress.Text,
                FirstName = firstName.Text,
                JobTitle = jobTitle.Text,
                LastName = lastName.Text,
                Location = location.Text,
                PhoneNumber = phoneNumber.Text,
                EmployeeID = employeeID.Text,
            };

            if (Utility.IsAnyNullOrEmpty(employeeContact))
            {
                StatusMessage.Text = "Oops. All fields are mandatory. Could you please add all required details?";
                StatusMessage.Visibility = Visibility;
            }

            else
            {
                string textFilePath = await Utility.AddEmployeeContact(employeeContact);
                StatusMessage.Text = "New Employee Contact Added Successfuly. Click on 'Display Phone Book' to see the record you just added.";
                StatusMessage.Visibility = Visibility;
            }

            emailAddress.Text = string.Empty;
            firstName.Text = string.Empty;
            jobTitle.Text = string.Empty;
            lastName.Text = string.Empty;
            location.Text = string.Empty;
            phoneNumber.Text = string.Empty;
            employeeID.Text = string.Empty;
        }

    }
}
