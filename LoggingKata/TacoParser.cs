using System.Collections;
using System.Collections.Generic;
using log4net;

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
            //DO not fail if one record parsing fails, return null
            return null; //TODO Implement
        }
    }
}