namespace MonikasPieShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MonikasPieShopDbContext _monikasPieShopDbContext;

        public CategoryRepository(MonikasPieShopDbContext monikasPieShopDbContext)
        {
            _monikasPieShopDbContext = monikasPieShopDbContext;
        }

        //public IEnumerable<Category> AllCategories {
        //    get { return _monikasPieShopDbContext.Categories.OrderBy(o => o.CategoryName); }
        //     }

        public IEnumerable<Category> AllCategories => _monikasPieShopDbContext.Categories.OrderBy(o => o.CategoryName); 

    }
}
