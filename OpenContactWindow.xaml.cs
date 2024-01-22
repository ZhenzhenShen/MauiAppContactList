using System;
using Microsoft.Maui.Controls;

namespace MauiAppContactList
{
    public partial class OpenContactWindow : ContentPage
    {
        private Contact _currentContact;
        private bool _isEditMode;

        // Constructor for creating a new contact
        public OpenContactWindow() : this(null) { }

        // Constructor for editing an existing contact
        public OpenContactWindow(Contact contactToEdit)
        {
            InitializeComponent();

            _currentContact = contactToEdit;
            _isEditMode = contactToEdit != null;

            // If editing a contact, fill in the fields with the current contact's information
            if (_isEditMode)
            {
                nameEntry.Text = _currentContact.Name;
                companyEntry.Text = _currentContact.Company;
                phoneEntry.Text = _currentContact.PhoneNumber;
                emailEntry.Text = _currentContact.EmailAddress;
            }
        }

        private void SaveContactClicked(object sender, EventArgs e)
        {
            var name = nameEntry.Text;
            var company = companyEntry.Text;
            var phone = phoneEntry.Text;
            var email = emailEntry.Text;

            if (_isEditMode)
            {
                _currentContact.Name = name;
                _currentContact.Company = company;
                _currentContact.PhoneNumber = phone;
                _currentContact.EmailAddress = email;

                MessagingCenter.Send(this, "UpdateContact", _currentContact);
            }
            else
            {
                var newContact = new Contact(name, company, phone, email);
                MessagingCenter.Send(this, "AddContact", newContact);
            }

            Navigation.PopAsync();
        }

    }
}
