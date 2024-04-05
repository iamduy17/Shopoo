using DataCommon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCommon.Response.ProductModel
{
    public class GetProductListModel
    {
        public List<Product> Products { get; set; } = null!;

        public GetProductListModel()
        {

        }

        public GetProductListModel(List<Product> reponse) 
        {
            Products = reponse.OrderByDescending(x => x.CreatedDate).ToList();
        }
    }
}
