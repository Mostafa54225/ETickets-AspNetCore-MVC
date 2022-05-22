using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ETickets.Data;

namespace ETickets.Models
{
    public class MovieVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Movie Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Movie Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required")]
        [Display(Name = "Price in $")]
        public double Price { get; set; }

        
        [Display(Name = "Movie image")]
        public byte[] Image { get; set; }
        [Required(ErrorMessage = "Movie start date is required")]
        [Display(Name = "Stard Date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Movie end ate is required")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }



        //Relationships
        [Required(ErrorMessage = "Movie actors is required")]
        [Display(Name = "Select Actors")]
        public List<int> ActorsIds { get; set; }
        [Required(ErrorMessage = "Movie cinema is required")]
        [Display(Name = "Select Cinema")]
        public int CinemaId { get; set; }
        [Required(ErrorMessage = "Movie producers is required")]
        [Display(Name = "Select Producers")]
        public int ProducerId { get; set; }
        
    }
}
