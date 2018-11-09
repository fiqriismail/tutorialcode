using System;
using System.Collections.Generic;
using Google.Cloud.Datastore.V1;

namespace dotnetstoreconsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Lets creat some movies!");
            // CreateMovies();
            // Console.WriteLine("We are done :)");

            Console.WriteLine("Listing all movies...");
            GetMovies();
        }

        static void CreateMovies()
        {
            //Project Id from the Project at GCP 
            string projectId = "<your-project-id>";
            //We are storing movies. So this is a Movie kind. 
            string kind = "Movie"; 

            //Create the datastore db
            var db = DatastoreDb.Create(projectId); 

            //List of movies
            List<Entity> movieEntities = new List<Entity>();

            //I need 10 movies 
            for (int i=1; i<=10; i++)
            {
                movieEntities.Add(
                        new Entity 
                        {
                            Key = db.CreateKeyFactory(kind).CreateKey($"key{i}"),
                            ["title"] = $"Movie Title {i}",
                            ["releasedYear"] = 2018,
                            ["director"] = $"Director {i}"
                        }
                    );
            }

            //Lets send the movies to the datastore
            using (var transction = db.BeginTransaction())
            {
                transction.Upsert(movieEntities);
                transction.Commit();
            }

        }

        static void GetMovies()
        {
            //Project Id from the Project at GCP 
            string projectId = "<your-project-id>";

            //We are storing movies. So this is a Movie kind. 
            string kind = "Movie"; 

            //Create the datastore db
            var db = DatastoreDb.Create(projectId); 

            Query query = new Query(kind);

            foreach(var movie in db.RunQueryLazily(query))
            {
                string line = $"The movie {movie["title"].StringValue} is directed by {movie["director"].StringValue} and released in year {movie["releasedYear"].IntegerValue}";
                
                Console.WriteLine(line);
            }
        }


    }
}
