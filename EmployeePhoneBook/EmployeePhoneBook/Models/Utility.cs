using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace EmployeePhoneBook.Models
{
    public class Utility
    {
        public const string TEXT_FILE_NAME = "EmployeePhoneBook.dat";
        public static async Task<string> AddEmployeeContact(EmployeeContact employeeContact)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile textFile = await localFolder.CreateFileAsync(TEXT_FILE_NAME, CreationCollisionOption.OpenIfExists);
            var content = JsonConvert.SerializeObject(employeeContact);
            using (StreamWriter outputFile = new StreamWriter(textFile.Path, true))
            {
                await outputFile.WriteLineAsync(content);
                outputFile.Close();
            }
            return textFile.Path;
        }

        public static async Task<ICollection<EmployeeContact>> GetEmployeeContacts()
        {
            var employeeContactList = new List<EmployeeContact>();
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile textFile = await localFolder.GetFileAsync(TEXT_FILE_NAME);
            var file = new StreamReader(textFile.Path);
            var line = string.Empty;
            while ((line = file.ReadLine()) != null)
            {
                var employee = JsonConvert.DeserializeObject<EmployeeContact>(line);
                employeeContactList.Add(employee);
            }
            file.Close();
           return employeeContactList;
        }

        public static bool IsAnyNullOrEmpty(object employeeContact)
        {
            return employeeContact.GetType().GetProperties()
           .Where(pi => pi.GetValue(employeeContact) is string)
           .Select(pi => (string)pi.GetValue(employeeContact))
           .Any(value => String.IsNullOrEmpty(value));
        }
    }
}
