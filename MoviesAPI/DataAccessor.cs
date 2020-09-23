using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;


namespace MoviesAPI
{
    public class DataAccessor
    {

        private static string GetConnectionString()
        {
            try
            {
                StreamReader reader = new StreamReader(@"C:\temp\db4server.txt");
                string server = reader.ReadLine();
                reader.Close();
                return server;
            }
            catch
            {
                return "Server=; Database = Movies; user id = user4; password=pass1;";

            }
        }

        private static IDbConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

        public static List<Movie> GetAllMovies()
        {
            IDbConnection db = GetConnection();
            return db.GetAll<Movie>().ToList();
        }

        public static List<Movie> GetAllFromGenre(string genre)
        {
            IDbConnection db = GetConnection();
            string query = $"SELECT * FROM movie WHERE genre = '{genre}'";
            return db.Query<Movie>(query).ToList();
        }

        public static List<string> GetAllCategories()
        {
            IDbConnection db = GetConnection();
            string query = $"SELECT distinct genre FROM movie";
            return db.Query<string>(query).ToList();
        }

        public static List<Movie> GetTitlesContaining(string search)
        {
            IDbConnection db = GetConnection();
            string query = $"SELECT * FROM movie WHERE title LIKE '%{search}%'";
            return db.Query<Movie>(query).ToList();
        }
    }
}
