using INNOVATEQ.DATA.Context;
using INNOVATEQ.DATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INNOVATEQ.DAL.Repository
{

    public class UserRepo : RepositoryBase<User>, IUserRepo
    {
        public UserRepo(INNOVATEQDBContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
