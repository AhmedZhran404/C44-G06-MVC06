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
    internal class TrainerRepository : ITrainerRepository
    {
        private readonly GymDbContext _context;

        //GymDbContext _context = new GymDbContext();

        public TrainerRepository(GymDbContext context)
        {
            this._context = context;
        }

        public int Add(Trainer trainer)
        {
            _context.Trainers.Add(trainer);

            return _context.SaveChanges();
        }

        public int Delete(int Id)
        {
            var trainer = GetById(Id);

            if (trainer is not null)
            {
                _context.Trainers.Remove(trainer);
                return _context.SaveChanges();
            }
            return 0;
        }

        public IEnumerable<Trainer> GetAll() => _context.Trainers.ToList();

        public Trainer? GetById(int Id) => _context.Trainers.Find(Id);

        public int Update(Trainer trainer)
        {
            _context.Trainers.Update(trainer);

            return _context.SaveChanges();
        }


    }
}
