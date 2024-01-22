using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.Maui.Storage;

namespace MauiAppContactList
{
    public static class FileManager
    {
        private static string FilePath = Path.Combine(FileSystem.AppDataDirectory, "contacts.json");

        public static List<Contact> LoadContacts()
        {
            if (!File.Exists(FilePath))
            {
                SaveContacts(new List<Contact>());
            }

            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Contact>>(json) ?? new List<Contact>();
        }


        public static void SaveContacts(List<Contact> contacts)
        {
            string json = JsonSerializer.Serialize(contacts);
            File.WriteAllText(FilePath, json);
        }
    }
}
