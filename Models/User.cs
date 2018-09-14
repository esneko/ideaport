using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ideaport.Models
{
    public partial class User
    {
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; }
        public ICollection<UserTask> UserTasks { get; set; }
    }
}
