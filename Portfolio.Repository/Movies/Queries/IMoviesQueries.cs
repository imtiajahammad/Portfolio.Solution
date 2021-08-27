using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portfolio.Repository.Movies.Queries
{
    public interface IMoviesQueries
    {
        Entities.Movies GetMoviesbyId(int? moviesId);
        IQueryable<Entities.Movies> GetMovies();
    }
}
