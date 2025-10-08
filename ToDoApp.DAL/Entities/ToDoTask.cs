using ToDoApp.DAL.Enums;

namespace ToDoApp.DAL.Entities
{
    public class ToDoTask
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public Status Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CompletedAt { get; set; }


    }
}
