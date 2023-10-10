using System;
using System.Collections.Generic;
using System.Linq;
using DoyleyRSVP2.Models;
using SQLite;

namespace DoyleyRSVP2.Repositories
{
    public class EventRepository
    {
        private SQLiteConnection conn;

        public EventRepository(string dbPath)
        {
            conn = new SQLiteConnection(dbPath);
            conn.CreateTable<EventModel>();
            conn.CreateTable<UserEventModel>();
        }

        // Method to add an event
        public void AddEvent(EventModel newEvent)
        {
            conn.Insert(newEvent);
        }

        // Method to get all the events
        public List<EventModel> GetAllEvents()
        {
            return conn.Table<EventModel>().ToList();
        }

        // Method to get event by user
        public List<EventModel> GetEventsByUser(UserModel user)
        {
            return conn.Table<EventModel>().Where(e => e.HostID == user.ID).ToList();
        }

        // Method to get event by event id
        public EventModel GetEventById(int id)
        {
            return conn.Table<EventModel>().FirstOrDefault(e => e.ID == id);
        }

        // Method to update an event
        public void UpdateEvent(EventModel updatedEvent)
        {
            conn.Update(updatedEvent);
        }

        // Method to delete and event using the event id
        public void DeleteEvent(int id)
        {
            var existingEvent = GetEventById(id);
            if (existingEvent != null)
            {
                conn.Delete(existingEvent);
            }
        }

        // Method to RSVP to an event
        public void RsvpEvent(int userId, int eventId)
        {
            var existingUserEvent = conn.Table<UserEventModel>().FirstOrDefault(ue => ue.UserId == userId && ue.EventId == eventId);
            if (existingUserEvent == null)
            {
                var userEvent = new UserEventModel
                {
                    UserId = userId,
                    EventId = eventId,
                    IsReserved = true,
                    IsHost = false
                };
                conn.Insert(userEvent);
            }
            else if (!existingUserEvent.IsReserved)
            {
                existingUserEvent.IsReserved = true;
                conn.Update(existingUserEvent);
            }
        }

        // Method to Decline an event after RSVP'ing
        public void DeclineRsvp(int userId, int eventId)
        {
            var existingUserEvent = conn.Table<UserEventModel>().FirstOrDefault(ue => ue.UserId == userId && ue.EventId == eventId);
            if (existingUserEvent != null && existingUserEvent.IsReserved)
            {
                existingUserEvent.IsReserved = false;
                conn.Update(existingUserEvent);
            }
        }

        // Method to get events that a user is attending
        public List<EventModel> GetAttendingEvents(int userId)
        {
            var attendingIds = conn.Table<UserEventModel>().Where(ue => ue.UserId == userId && ue.IsReserved).Select(ue => ue.EventId).ToList();
            return conn.Table<EventModel>().Where(e => attendingIds.Contains(e.ID)).ToList();
        }

        // Method to get events that a user is hosting
        public List<EventModel> GetHostedEvents(int userId)
        {
            var hostedIds = conn.Table<UserEventModel>().Where(ue => ue.UserId == userId && ue.IsHost).Select(ue => ue.EventId).ToList();
            return conn.Table<EventModel>().Where(e => hostedIds.Contains(e.ID)).ToList();
        }
    }
}
