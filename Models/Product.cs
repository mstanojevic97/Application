using Application.Infrastructure.Validation;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a value")]
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter a value")]
        public double Price { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
        public string Image { get; set; } 

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
    }
}
