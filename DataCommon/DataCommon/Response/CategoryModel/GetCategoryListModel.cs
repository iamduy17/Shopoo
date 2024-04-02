using DataCommon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCommon.Response.CategoryModel
{
    public class GetCategoryListModel
    {
        public List<Category> Categories { get; set; } = null!;

        public GetCategoryListModel()
        {

        }

        public GetCategoryListModel(List<Category> response)
        {
            Categories = response;
        }
    }
}
