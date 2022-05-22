using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETickets.Models
{
    public class Producer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Profile Pictures")]
        public byte[] ProfilePicture { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 charcters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Bio is required")]
        [Display(Name = "Bio")]
        public string Bio { get; set; }
        // Relationships
        public List<Movie> Movies { get; set; }


    }
}
