using Microsoft.EntityFrameworkCore;

namespace MonikasPieShop.Models
{
    public class PieRepository : IPieRepository
    {
        private  readonly MonikasPieShopDbContext _monikasPieShopDbContext;

        //constructor injection
        public PieRepository(MonikasPieShopDbContext monikasPieShopDbContext)
        {
            _monikasPieShopDbContext = monikasPieShopDbContext;
        }

        public IEnumerable<Pie> AllPies
        {
            get { return _monikasPieShopDbContext.Pies.Include(i => i.Category); }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get { return _monikasPieShopDbContext.Pies.Include(i => i.Category).Where(w => w.IsPieOfTheWeek); }
        }

        public Pie? GetPieById(int pieId)
        {
            //FirstOrDefault only returns one pie, Where can return lots of pies. If PieId is the primary key then they would both be as quick.If PieId is just a regular field then FirstOrDefault is the quicker as it stops searching once it's found a pie that contains that PieId. 
            //return _monikasPieShopDbContext.Pies.Where(w => w.PieId == pieId).FirstOrDefault();
            return _monikasPieShopDbContext.Pies.FirstOrDefault(w => w.PieId == pieId);

        }

        public IEnumerable<Pie> SearchPies(string searchQuery)
        {
            return _monikasPieShopDbContext.Pies.Where(w=>w.Name.Contains(searchQuery));
        }
    }
}
