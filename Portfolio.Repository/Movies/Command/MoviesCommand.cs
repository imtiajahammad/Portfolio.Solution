using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Repository.Movies.Command
{
    public class MoviesCommand:IMoviesCommand
    {
        private readonly PortfolioDbContext _portfolioDbContext;
        public MoviesCommand(PortfolioDbContext frapperDbContext)
        {
            _portfolioDbContext = frapperDbContext;
        }
        public void Add(Entities.Movies movies)
        {
            _portfolioDbContext.Movies.Add(movies);
        }

        public void Update(Entities.Movies movies)
        {
            _portfolioDbContext.Entry(movies).State = EntityState.Modified;
        }

        public void Delete(Entities.Movies movies)
        {
            _portfolioDbContext.Entry(movies).State = EntityState.Deleted;
        }
    }
}
