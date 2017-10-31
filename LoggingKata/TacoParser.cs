using System.Collections;
using System.Collections.Generic;
using log4net;
using System;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the TacoBells
    /// </summary>
    public class TacoParser
    {
        public TacoParser()
        {

        }

        private static readonly ILog Logger =
            LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ITrackable Parse(string line)
        {
            var cells = line.Split(','); 
            if (cells.Length < 3)
            {
                Logger.Error("Much have at least 3 elements.");
                    return null;
            }
            
                double lon = 0;
                double lat = 0;
            try
            {
                Logger.Debug("Attempt Parsing Longitude");
                lon = double.Parse(cells[0]);

                Logger.Debug("Attempt Parsing Latitude");
                lon = double.Parse(cells[1]);
            }
            catch (Exception e)
            {
                Logger.Error("Fail to parse the location", e);
                Console.WriteLine(e);
                    return null;
            }
            var tacoBell = new TacoBell
            {
                Name = cells[2],
                Location = new Point
                {
                    Longitude = lon,
                    Latitude = lat
                };
                Logger.Info();
            }



            //DO not fail if one record parsing fails, return null
            return null; //TODO Implement
        }
    }
}