﻿using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using FreshMvvm;

namespace FreshMvvmSampleApp
{
    public class App : Application
    {
        public App ()
        {
            FreshIOC.Container.Register<IDatabaseService, DatabaseService> ();

            MainPage = new NavigationPage (new LaunchPage (this));
        }

        public async Task LoadBasicNav ()
        {
            var page = await FreshPageModelResolver.ResolvePageModel<MainMenuPageModel> ();
            var basicNavContainer = new FreshNavigationContainer (page);
            MainPage = basicNavContainer;
        }

        public async Task LoadMasterDetail ()
        {
            var masterDetailNav = new FreshMasterDetailNavigationContainer ();
            masterDetailNav.Init ("Menu", "Menu.png");
            await masterDetailNav.AddPage<ContactListPageModel> ("Contacts", null);
            await masterDetailNav.AddPage<QuoteListPageModel> ("Quotes", null);
            MainPage = masterDetailNav;
        }

        public async Task LoadTabbedNav ()
        {
            var tabbedNavigation = new FreshTabbedNavigationContainer ();
            await tabbedNavigation.AddTab<ContactListPageModel> ("Contacts", "contacts.png", null);
            await tabbedNavigation.AddTab<QuoteListPageModel> ("Quotes", "document.png", null);
            MainPage = tabbedNavigation;
        }

        public void LoadCustomNav ()
        {
            MainPage = new CustomImplementedNav ();
        }

        protected override void OnStart ()
        {
            // Handle when your app starts
        }

        protected override void OnSleep ()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume ()
        {
            // Handle when your app resumes
        }
    }
}

