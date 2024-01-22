using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace MauiAppContactList
{
    public partial class MainPage : ContentPage
    {
        private List<Contact> allContacts; // Keep the original list of contacts here
        //Real-time updates of contacts data when adding or deleting
        public ObservableCollection<Contact> Contacts { get; set; }

        public MainPage()
        {
            InitializeComponent();
            allContacts = FileManager.LoadContacts();
            Contacts = new ObservableCollection<Contact>(allContacts);
            BindingContext = this;

            //Communication between main page and opencontactpage
                // Subscribe to the AddContact message
                MessagingCenter.Subscribe<OpenContactWindow, Contact>(this, "AddContact", (sender, contact) =>
                {
                    Contacts.Add(contact);
                    FileManager.SaveContacts(Contacts.ToList());
                });

                // Subscribe to the UpdateContact message
                MessagingCenter.Subscribe<OpenContactWindow, Contact>(this, "UpdateContact", (sender, updatedContact) =>
                {
                    var existingContact = Contacts.FirstOrDefault(c => c.Name == updatedContact.Name);
                    if (existingContact != null)
                    {
                        int index = Contacts.IndexOf(existingContact);
                        Contacts[index] = updatedContact;
                        FileManager.SaveContacts(Contacts.ToList());
                    }
                });
        }


        private async void OnAddContactClicked(object sender, EventArgs e)
        {
            var contactWindow = new OpenContactWindow();
            await Navigation.PushAsync(contactWindow);
        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Contact contact)
            {
                Contacts.Remove(contact);
                FileManager.SaveContacts(Contacts.ToList());
            }
        }

        private void OnSearchClicked(object sender, EventArgs e)
        {
            string searchText = searchEntry.Text?.ToLower() ?? string.Empty;
            var filteredContacts = FileManager.LoadContacts()
                .Where(c => c.Name.ToLower().Contains(searchText))
                .ToList();

            Contacts.Clear();
            foreach (var contact in filteredContacts)
            {
                Contacts.Add(contact);
            }
        }

        private async void OnEditClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is Contact contact)
            {
                // Logic for editing a contact
                var editContactPage = new OpenContactWindow(contact);
                await Navigation.PushAsync(editContactPage);
            }
        }

        private void OnShowAllClicked(object sender, EventArgs e)
        {
            // Clearing the current display or view that shows contacts
            Contacts.Clear();

            // Reloading all contacts from the original list
            foreach (var contact in allContacts)
            {
                Contacts.Add(contact);
            }
        }
    }
}
