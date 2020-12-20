using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Uplift.Models;

namespace Uplift.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetCategoryDropdown()
        {
            return _db.Categories.Select(c => new SelectListItem{
                Text = c.Name,
                Value = c.Id.ToString()
            });
        }


        public async Task Update(Category category)
        {
            var targetObj = await _db.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
            targetObj.Name = category.Name;
            targetObj.DisplayOrder = category.DisplayOrder;
            _db.SaveChanges();
        }
    }
}