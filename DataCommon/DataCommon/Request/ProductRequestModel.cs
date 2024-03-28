using DataCommon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCommon.Request
{
    public class ProductRequestModel : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? ImageURL { get; set; }
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public Guid CategoryId { get; set; }
    }
}
