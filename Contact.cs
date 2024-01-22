
    public class Contact
    {
        // Properties matching JSON keys
        public string Name { get; set; }
        public string Company { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        // Parameterless constructor
        public Contact() { }

        // Parameterized constructor 
        public Contact(string name, string company, string phoneNumber, string emailAddress)
        {
            this.Name = name;
            this.Company = company;
            this.PhoneNumber = phoneNumber;
            this.EmailAddress = emailAddress;
        }

    }


