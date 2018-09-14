using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ideaport.Models
{
    public partial class Client
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
