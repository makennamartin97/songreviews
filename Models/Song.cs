
using System;
using System.ComponentModel.DataAnnotations;


namespace songreviews.Models
{
    public class Song
    {
        [Key]
        public int songID {get;set;}

        [Required(ErrorMessage="Title is required")]
        public string Title {get; set;}

        [Required(ErrorMessage="Artist is required")]
        public string Artist {get;set;}

        [Required(ErrorMessage="Genre is required")]
        public string Genre {get;set;}

        [Required(ErrorMessage="Stars are required")]
        [Range(1,5)]
        public int Stars {get;set;}

        public string Review {get;set;}

        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        //public Song() {}



        

        
    }
}