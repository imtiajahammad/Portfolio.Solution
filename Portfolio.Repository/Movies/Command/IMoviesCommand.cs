using System;
using System.Collections.Generic;
using System.Text;
using Portfolio.Entities;

namespace Portfolio.Repository.Movies.Command
{
    public interface IMoviesCommand
    {
        void Add(Entities.Movies movies);
        void Update(Entities.Movies movies);
        void Delete(Entities.Movies movies);
    }
}
