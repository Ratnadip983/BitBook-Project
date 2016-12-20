using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitBookProj.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name Required")]
        [DisplayName("First Name")]
        [StringLength(20, MinimumLength = 3,ErrorMessage = "First Name Must be 3-20 chracter")]
        [Column(TypeName = "varchar")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name Required")]
        [DisplayName("Last Name")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Last Name Must be 3-20 chracter")]
        [Column(TypeName = "varchar")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Please provide valid email")]
        [Remote("IsEmailExist","Users",ErrorMessage = "Email already used")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Select gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Date of Birth Required")]
        [DataType(DataType.Date)]
        [DisplayName("Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Password must required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please provide your confirm password")]
        [DisplayName("Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password",ErrorMessage = "Must be same with password")]
        [NotMapped]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public byte[] ProfilePic { get; set; }
        public byte[] CoverPic { get; set; }
        public string City { get; set; }

        public string Country { get; set; }
        public virtual List<Friend> Friends { get; set; }
        public virtual List<Status> Statuses { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Like> Liks { get; set; }


    }
}