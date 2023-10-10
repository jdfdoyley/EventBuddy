using System;
using System.Collections.Generic;
using DoyleyRSVP2.Models;
using DoyleyRSVP2.Repositories;
using Xamarin.Forms;

namespace DoyleyRSVP2.View
{
    public partial class AllEventsView : ContentPage
    {
        public AllEventsView()
        {
            InitializeComponent();

            // Wire up the ItemSelected event
            AllEventsList.ItemSelected += AllEventsList_ItemSelected;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Fetch all the events
            var events = App.EventRepo.GetAllEvents();
            AllEventsList.ItemsSource = events;
        }

        private async void AllEventsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            var selectedEvent = e.SelectedItem as EventModel;
            await Navigation.PushAsync(new EventDetailsView(selectedEvent));

            App.EventRepo.RsvpEvent(App.CurrentUser.ID, selectedEvent.ID);

            // Deselect the item
            AllEventsList.SelectedItem = null;
        }

        private async void AddNewEvent_Clicked(object sender, EventArgs e)
        {
            // Navigate to the Add New Event page, pass the current user
            await Navigation.PushAsync(new AddEventView(App.CurrentUser));
        }
    }
}

