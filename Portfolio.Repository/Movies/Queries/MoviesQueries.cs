using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portfolio.Repository.Movies.Queries
{
    public class MoviesQueries : IMoviesQueries
    {
        private readonly PortfolioDbContext _portfolioDbContext;
        public MoviesQueries(PortfolioDbContext portfolioDbContext)
        {
            _portfolioDbContext = portfolioDbContext;
        }

        public Entities.Movies GetMoviesbyId(int? moviesId)
        {
            var movie = _portfolioDbContext.Movies
                .FirstOrDefault(m => m.MoviesID == moviesId);

            return movie;
        }


        public IQueryable<Entities.Movies> GetMovies()
        {
            var source = (from movies in _portfolioDbContext.Movies.
                    OrderBy(a => a.MoviesID)
                          select movies).AsQueryable();

            return source;
        }
    }
}
