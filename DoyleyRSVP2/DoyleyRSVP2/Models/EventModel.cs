using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace DoyleyRSVP2.Models
{
    public class EventModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int HostID { get; set; }

        [NotNull]
        public string HostName { get; set; }

        [NotNull]
        public string EventName { get; set; }

        [NotNull]
        public string EventAddress { get; set; }

        [NotNull]
        public int MaxAttendees { get; set; }

        [NotNull]
        public DateTime EventDateTime { get; set; }

        [NotNull]
        public DateTime RsvpDeadline { get; set; }

        public string RsvpUserIdsString { get; set; }

        [Ignore]
        public UserModel Host { get; set; }

        [Ignore]
        public List<string> RsvpUsers { get; set; }

        [Ignore]
        public List<int> RsvpUserIds
        {
            get
            {
                if (string.IsNullOrEmpty(RsvpUserIdsString)) return new List<int>();
                return RsvpUserIdsString.Split(',').Select(int.Parse).ToList();
            }
            set
            {
                RsvpUserIdsString = string.Join(",", value);
            }
        }
    }
}

