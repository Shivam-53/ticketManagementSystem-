using System;
using System.ComponentModel.DataAnnotations;

namespace ticketManagementSystem_2.Models{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = "Open";
        public DateTime CreatedAt { get; set; }
    }
}
