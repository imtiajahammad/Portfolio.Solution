using Portfolio.Repository.Movies.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Repository
{
    public interface IUnitOfWorkEntityFramework
    {
        //IUserMasterCommand UserMasterCommand { get; }
        //IAssignedRolesCommand AssignedRolesCommand { get; }
        //IUserTokensCommand UserTokensCommand { get; }
        IMoviesCommand MoviesCommand { get; }
        bool Commit();
        void Dispose();
    }
}
