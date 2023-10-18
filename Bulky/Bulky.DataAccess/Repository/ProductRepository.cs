using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var productObj = _db.Products.FirstOrDefault(p => p.Id == product.Id);
            if (productObj != null)
            {
                productObj.Title = product.Title;
                productObj.Description = product.Description;
                productObj.Category = product.Category;
                productObj.ISBN = product.ISBN;
                productObj.Price = product.Price;
                productObj.Price50 = product.Price50;
                productObj.Price100 = product.Price100;
                productObj.CategoryId = product.CategoryId;
                productObj.Author = product.Author;
                if (!string.IsNullOrEmpty(product.ImageURL))
                {
                    productObj.ImageURL = product.ImageURL;
                }
            }
            _db.Products.Update(productObj);
        }
    }
}