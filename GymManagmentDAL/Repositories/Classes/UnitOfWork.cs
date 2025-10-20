using GymManagmentDAL.Data.GymDBContext;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbContext _context;

        private readonly Dictionary<string, object> repositories = [];
        public ISessionRepository SessionRepository { get; set; }

        public UnitOfWork(GymDbContext context , ISessionRepository sessionRepository)
        {
            this._context = context;
            SessionRepository = sessionRepository;
        }


        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEnitity , new()
        {
            var entityName = typeof(TEntity).Name; // Member

            if(repositories.TryGetValue( entityName, out object? value))
            {
                return (IGenericRepository<TEntity>)value;
            }

            var repository = new GenericRepository<TEntity>(_context);

            repositories.Add(entityName, repository); // In Dictionary

            return repository;
        }

        public int SaveChanges()
        {
          return  _context.SaveChanges();
        }
    }
}
