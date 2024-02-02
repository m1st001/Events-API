using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventsApi.Data.Models
{
    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public List<int>? RegisteredUsersIds { get; set; }

        public Event(int authorId, string? name, string? description, DateTime startDate)
        {
            AuthorId = authorId;
            Name = name;
            Description = description;
            StartDate = startDate;
        }
    }
}
