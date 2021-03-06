using System;

namespace DatingApp.API.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; } // check the main photo
        public string PublicId { get; set; } // id to public to CLoudinary
        public User User { get; set; }
        public int UserId { get; set; }
    }
}