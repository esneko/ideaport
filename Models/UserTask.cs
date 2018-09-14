using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ideaport.Models
{
    public partial class UserTask
    {
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Task Task { get; set; }
    }
}
