using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Models
{
    public class CategoryRepository :ICategoryRepository
    {
        private AppDbcontext _appDbcontext;

        public CategoryRepository(AppDbcontext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }

        public IEnumerable<Category> Categories => _appDbcontext.Categories;
    }
}
