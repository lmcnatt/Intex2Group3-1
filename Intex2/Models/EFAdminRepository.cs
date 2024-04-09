using Microsoft.EntityFrameworkCore;

namespace Intex2.Models
{
    public class EFAdminRepository : IAdminRepository
    {
        private IntexDbContext _context;

        public EFAdminRepository(IntexDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products.Include(p => p.ProductCategories).ThenInclude(pc => pc.Category)
                                                                .Select(p => new Product
                                                                {
                                                                    ProductId = p.ProductId,
                                                                    Name = p.Name,
                                                                    Year = p.Year,
                                                                    NumParts = p.NumParts,
                                                                    Price = p.Price,
                                                                    ImgLink = p.ImgLink,
                                                                    PrimaryColor = p.PrimaryColor,
                                                                    SecondaryColor = p.SecondaryColor,
                                                                    Description = p.Description,
                                                                    ProductCategories = (ICollection<ProductCategory>)p.ProductCategories.Select(pc => new ProductCategory
                                                                    {
                                                                        Category = pc.Category,
                                                                        CategoryId = pc.CategoryId
                                                                    })
                                                                });
    }
}