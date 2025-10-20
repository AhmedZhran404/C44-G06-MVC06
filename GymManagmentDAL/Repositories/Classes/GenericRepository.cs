using GymManagmentDAL.Data.GymDBContext;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Classes
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEnitity , new()
    {
        private readonly GymDbContext _context;

        public GenericRepository(GymDbContext context)
        {
            this._context = context;
        }
        public void Add(TEntity member)
        {
            _context.Add(member);
            
        }

        public void Delete(TEntity member)
        {
            _context.Remove(member);
        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null)
        {
            if(condition is not null)
            {
                return _context.Set<TEntity>().AsNoTracking().Where(condition).ToList();
                
            }

            return _context.Set<TEntity>().AsNoTracking().ToList();
        }

        public TEntity? GetById(int Id)
        {
            return _context.Set<TEntity>().Find(Id);
        }

        public void Update(TEntity member)
        {
            _context.Update(member);
        }
    }
}
