using System;
using System.Collections.Generic;
using SQLite;

namespace DoyleyRSVP2.Models
{
	public class UserModel
	{
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string Email { get; set; }

        [NotNull]
        public string Password { get; set; }

        [NotNull]
        public string MobileNumber { get; set; }
    }
}

