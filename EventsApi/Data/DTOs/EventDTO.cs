using EventsApi.Data.Models;

namespace EventsApi.Data.DTOs
{
    public class EventDTO
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public List<int> RegisteredUsersIds { get; set; }
        public EventDTO(Event item) =>
        (Id, AuthorId, Name, Description, CreatedDate, StartDate, RegisteredUsersIds) = 
            (item.Id, item.AuthorId, item.Name, item.Description, item.CreatedDate, item.StartDate, item.RegisteredUsersIds);
    }
}