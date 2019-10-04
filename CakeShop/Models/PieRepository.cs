using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CakeShop.Models
{
    public class PieRepository : IPieRepository
    {
        private AppDbcontext _appDbcontext;

        public PieRepository(AppDbcontext appDbcontext)
        {
            _appDbcontext = appDbcontext;
        }

        public IEnumerable<Pie> Pies
        {
            get
            {
                return _appDbcontext.Pies.Include(c => c.Category);
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _appDbcontext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek == true);
            }
        }

        public Pie GetPieById(int pieId)
        {
            return _appDbcontext.Pies.Include(c => c.Category).FirstOrDefault(p => p.Id == pieId);
        }
    }
}
