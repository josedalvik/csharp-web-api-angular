using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Model
{
    [Table("Inbox")]
    public class Inbox
    {
        [Key]
        [Required]
        public string Id { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string Subject { get; set; }
    }
}
