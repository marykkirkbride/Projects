using EmployeePhoneBook.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Globalization.PhoneNumberFormatting;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace EmployeePhoneBook
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DisplayPage : Page
    {

        private static ObservableCollection<Models.EmployeeContact> sixColumnLists;

        public ObservableCollection<Models.EmployeeContact> SixColumnLists
        {
            get { return sixColumnLists ?? (sixColumnLists = new ObservableCollection<Models.EmployeeContact>()); }
            set { sixColumnLists = value; }
        }
        public DisplayPage()
        {
            this.InitializeComponent();
            LoadData();

        }

        private async void LoadData()
        {
            // you can also change the private SixColumnLists to a static 
            // and do 
            SixColumnLists = await SixColumnListManager.GetEmployeContacts();
            if (SixColumnLists.Count == 0)
            {
                Display_StatusMessage.Text = "No Records to Show. Try clicking on 'Add New Employee' section to add some contacts in your phonebook.";
                Display_StatusMessage.Visibility = Visibility;
            }
        }

    }

    public class EmployeeContact : INotifyPropertyChanged
    {
        private string employeeID = string.Empty;
        public string EmployeeId { get { return employeeID; } set { employeeID = value; NotifyPropertyChanged("EmployeeId"); } }
        private string firstName = string.Empty;
        public string FirstName { get { return firstName; } set { firstName = value; NotifyPropertyChanged("FirstName"); } }

        private string lastName = string.Empty;
        public string LastName { get { return lastName; } set { lastName = value; NotifyPropertyChanged("LastName"); } }

        private string emailAddress = string.Empty;
        public string EmailAddress { get { return emailAddress; } set { emailAddress = value; NotifyPropertyChanged("EmailAddress"); } }

        private string jobTitle = string.Empty;
        public string JobTitle { get { return jobTitle; } set { jobTitle = value; NotifyPropertyChanged("JobTitle"); } }

        private string location = string.Empty;
        public string Location { get { return location; } set { location = value; NotifyPropertyChanged("Location"); } }

        private string phoneNumber = string.Empty;
        public string PhoneNumber { get { return phoneNumber; } set { phoneNumber = value; NotifyPropertyChanged("PhoneNumber"); } }

        //PropertyChanged handers
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class SixColumnListManager
    {
        public static async Task<ObservableCollection<Models.EmployeeContact>> GetEmployeContacts()
        {
            var sixColumnLists = new ObservableCollection<Models.EmployeeContact>
            {
                //Add a dummy row to present something on screen
                //new Models.EmployeeContact {  EmployeeID = "Employee Id", FirstName = "FirstName", LastName = "LastName", PhoneNumber = "PhoneNumber", JobTitle = "JobTitle", Email = "Email", Location = "Location" }
                
            };

            //First working method
            await readList(sixColumnLists);

            //Second Working Method
            //tryAgain(threecolumnlists);

            return sixColumnLists;
        }

        public static async Task readList(ObservableCollection<Models.EmployeeContact> tcl)
        {
            var employeeContactList = new List<EmployeeContact>();
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            if (!(File.Exists($"{localFolder.Path.ToString()}\\{Utility.TEXT_FILE_NAME}")))
            {
                await localFolder.CreateFileAsync(Utility.TEXT_FILE_NAME, CreationCollisionOption.OpenIfExists);
            }
            StorageFile textFile = await localFolder.GetFileAsync(Utility.TEXT_FILE_NAME);
            var file = new StreamReader(textFile.Path);
            var line = string.Empty;

            //Phone number formatter
            PhoneNumberFormatter currentFormatter;
            currentFormatter= new PhoneNumberFormatter();

            try
            {
                while ((line = file.ReadLine()) != null || string.IsNullOrWhiteSpace((line = file.ReadLine())))
                {
                    var employee = JsonConvert.DeserializeObject<EmployeeContact>(line);

                    if (!Utility.IsAnyNullOrEmpty(employee))
                    {
                        employeeContactList.Add(employee);
                        tcl.Add(new Models.EmployeeContact { FirstName = employee.FirstName, LastName = employee.LastName, EmailAddress = employee.EmailAddress, JobTitle = employee.JobTitle, Location = employee.Location, PhoneNumber = currentFormatter.FormatPartialString(employee.PhoneNumber), EmployeeID = employee.EmployeeId });
                    }
                }
            }
            catch (Exception)
            {
                file.Close();
            }

            finally
            {
                file.Close();
            }
            
        }
    }

}