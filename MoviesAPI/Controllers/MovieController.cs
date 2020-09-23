using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MoviesAPI.Controllers
{
    [Route("Movie")] //Changed this
    [ApiController]
    public class MovieController : ControllerBase
    {

        [HttpGet] //Ideally, displays all movies.
        public List<Movie> Movie()
        {
            List<Movie> movieLib = DataAccessor.GetAllMovies();
            return movieLib;
        }

        [HttpGet("Genre")] //Type this as an extension to see a list of available genres (categories).
        public List<string> Genre()
        {
            List<string> movieLib = DataAccessor.GetAllCategories();
            return movieLib;
        }

        [HttpGet("{genre}")] //Type any genre listed in the DB to show all movies of that genre. Ex: Movie/Horror or Movie/Drama.
        public List<Movie> Movie(string genre)
        {
            List<Movie> movieLib = DataAccessor.GetAllFromGenre(genre);
            return movieLib;
        }

        [HttpGet("Search/{search}")] //Type Search/{MovieTitle} to see if we have what you're looking for.
        public List<Movie> Search(string search)
        {
            List<Movie> movieLib = DataAccessor.GetTitlesContaining(search);
            return movieLib;
        }

        [HttpGet("Keyword/{search}")]
        public List<Movie> Keyword(string search)
        {
            List<Movie> movieLib = DataAccessor.GetAllMovies();
            List<Movie> keyLib = new List<Movie>();

                foreach(Movie m in movieLib)
                {
                    if (m.Title.Contains(search))
                    {
                        keyLib.Add(m);
                    }
                }

        return keyLib;
        }

        [HttpGet("Random")] //Returns a random movie.
        public Movie RandomMovie()
        {
            Random rand = new Random();
            List<Movie> movieLib = DataAccessor.GetAllMovies();
            int count = movieLib.Count();
            Movie movie = movieLib[rand.Next(0, count + 1)];
            return movie;
        }

        [HttpGet("Random/{quantity}")] //Returns a list of the chosen quantity.
        public List<Movie> RandomMovie(int quantity)
        {
            Random rand = new Random();
            List<Movie> movieLib = DataAccessor.GetAllMovies();
            List<Movie> randomLib = new List<Movie>();
            int count = movieLib.Count();

            for (int i = 0; i<quantity; i++) {
                int shuffle = rand.Next(0, count);
                Movie movie = movieLib[shuffle];
                movieLib.Remove(movie);
                randomLib.Add(movie);
                count--;
            }
            return randomLib;
        }
    }
}
