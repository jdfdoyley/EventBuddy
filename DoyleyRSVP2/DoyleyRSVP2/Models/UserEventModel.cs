using System;
using SQLite;

namespace DoyleyRSVP2.Models
{
	public class UserEventModel
	{
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        public bool IsReserved { get; set; }

        public bool IsHost { get; set; }
    }
}

