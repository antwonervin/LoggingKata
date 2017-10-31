using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.IO;
using Geolocation;

namespace LoggingKata
{
    class Program
    {
        //Why do you think we use ILog?
        private static readonly ILog Logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            var path = Environment.CurrentDirectory + "\\Taco_Bell-US-AL-Alabama.csv";

            if (path.Length == 0)
            {
                Console.WriteLine("You must provide a filename as an argument");
                Logger.Fatal("Cannot import without filename specified as an argument");
                Console.ReadLine();
                return;
            }

            Logger.Info("Log initialized");
            Logger.Info("Grabbing from path:" + path);

            var lines = File.ReadAllLines(path);
            if   (lines.Length == 0)
            {
                Logger.Error("No Locations to check. Must have at least one location.");
            }
            else if (lines.Length == 1)
            {
                Logger.Warn("Only one location provided. Must have two to perform a check.");   
            }
            var parser = new TacoParser();
            Logger.Debug("Initialized our Parser");

            var locations = lines.Select(line => parser.Parse(line))
                .OrderBy(loc => loc.Location.Longitude)
                .ThenBy(loc => loc.Location.Latitude)
                .ToArray();
                                                                        
            ITrackable a = null;
            ITrackable b = null;
            double distance = 0;

            //TODO:  Find the two TacoBells in Alabama that are the furthurest from one another.

            foreach (var locA in locations)
            {
                var origin = new Coordinate {Latitude = locA.Location.Latitude,Longitude = locA.Location.Longitude};

                foreach(var locB in locations)
                {
                    var destination = new Coordinate
                    {
                        Latitude = locB.Location.Latitude,
                        Longitude = locB.Location.Latitude
                    };

                    var nDestination = GeoCalculator.GetDistance(origin, destination);
                    
                    if (nDestination > distance)
                    {
                        a = locA;
                        b = locB;
                        distance = nDestination;
                    }
                }
            }
            Console.WriteLine($"The two TacoBells that are furthest aprt are:{a.Name} and:{b.Name}.");
            Console.WriteLine($"These two locations are:{distance} miles apart.");

        }
    }
}