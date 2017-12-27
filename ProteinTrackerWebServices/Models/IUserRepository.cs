using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteinTrackerWebServices.Models
{
    public interface IUserRepository
    {
        void Add(User user);
        ReadOnlyCollection<User> GetAll();
        User GetById(int Id);
        void Save(User updateUser);
    }
}
