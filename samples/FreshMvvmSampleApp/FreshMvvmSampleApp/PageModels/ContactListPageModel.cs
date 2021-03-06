﻿using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using PropertyChanged;
using System.Collections.ObjectModel;
using FreshMvvm;

namespace FreshMvvmSampleApp
{
    [ImplementPropertyChanged]
    public class ContactListPageModel : FreshBasePageModel
    {
        IDatabaseService _databaseService;

        public ContactListPageModel (IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public ObservableCollection<Contact> Contacts { get; set; }

        public override async Task Init (object initData)
        {
            Contacts = new ObservableCollection<Contact> (_databaseService.GetContacts ());
        }

        protected override void ViewIsAppearing (object sender, EventArgs e)
        {
            //You can do stuff here
        }

        public override void ReverseInit (object value)
        {
            var newContact = value as Contact;
            if (!Contacts.Contains (newContact)) {
                Contacts.Add (newContact);
            }
        }

        Contact _selectedContact;

        public Contact SelectedContact {
            get {
                return _selectedContact;
            }
            set {
                _selectedContact = value;
                if (value != null)
                    ContactSelected.Execute (value);
            }
        }

        public Command AddContact {
            get {
                return new Command (async () => {
                    await CoreMethods.PushPageModel<ContactPageModel> ();
                });
            }
        }

        public Command<Contact> ContactSelected {
            get {
                return new Command<Contact> (async (contact) => {
                    await CoreMethods.PushPageModel<ContactPageModel> (contact);
                });
            }
        }
    }
}

