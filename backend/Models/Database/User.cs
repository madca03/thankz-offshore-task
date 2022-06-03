using System;
using System.Collections.Generic;

#nullable disable

namespace backend.Models.Database
{
    public partial class User
    {
        public string Username { get; set; }
        public Guid UserId { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Contact { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
