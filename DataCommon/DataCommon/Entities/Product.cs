using DataCommon.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCommon.Entities
{
    public class Product : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? ImageURL { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public decimal? RatingPoint { get; set; }
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        public Product()
        {

        }

        public Product(ProductRequestModel product, Category category)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            ImageURL = product.ImageURL;
            CreatedDate = product.CreatedDate;
            UpdatedDate = product.UpdatedDate;
            CategoryId = product.CategoryId;
            Category = category;
        }
    }
}
