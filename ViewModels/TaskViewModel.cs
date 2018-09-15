using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ideaport.Models
{
    public partial class TaskViewModel
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public ICollection<EmployeeViewModel> Employees { get; set; } = new List<EmployeeViewModel>();
    }
}
