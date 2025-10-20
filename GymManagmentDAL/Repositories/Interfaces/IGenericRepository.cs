using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEnitity , new()
    {
        TEntity? GetById(int Id);

        IEnumerable<TEntity> GetAll(Func<TEntity , bool>? condition = null);

        void Add(TEntity member);

        void Update(TEntity member);    
        void Delete(TEntity member);
    }
}
