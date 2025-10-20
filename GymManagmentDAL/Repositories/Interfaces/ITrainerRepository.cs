using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    public interface ITrainerRepository
    {
        Trainer? GetById(int Id);

        IEnumerable<Trainer> GetAll();

        // return int in Add ==> SaveChane return Numbers of Effect Row
        int Add(Trainer member);
        int Update(Trainer member);
        int Delete(int Id);
    }
}
