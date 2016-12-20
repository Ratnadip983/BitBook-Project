using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BitBookProj.Models
{
    public class Friend
    {

        [Key]
        [Column(Order = 1)]
        public int User1Id { get; set; }
        [ForeignKey("User1Id")]
        public virtual User User1 { get; set; }
        [Key]
        [Column(Order = 2)]
        public int User2Id { get; set; }
        [ForeignKey("User2Id")]
        public virtual User User2 { get; set; }
        [Required]
        public bool IsAccepted { get; set; }
    }
}