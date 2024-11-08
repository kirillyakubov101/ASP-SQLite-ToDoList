using System.ComponentModel.DataAnnotations;

namespace Todo.Web.Models.Entities
{
    public class ToDoElement
    {
        public ToDoElement() { }
        public ToDoElement(string title,bool IsCompleted)
        {
            this.Title = title;
            this.IsCompleted = IsCompleted;
        }
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public bool IsCompleted {  get; set; }
    }
}
