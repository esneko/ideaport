using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ideaport.Models
{
    public partial class Task
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<UserTask> UserTasks { get; set; }
    }
}
