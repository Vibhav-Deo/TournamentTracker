using System;
using System.Collections.Generic;
using System.Text;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public class TextFileConnector : IDataConnection
    {
        //TODO: Make sure that the prize is actually saved to database.
        /// <summary>
        /// Saves a prize to database
        /// </summary>
        /// <param name="prize">Prize to be save to database</param>
        /// <returns>The prize that was save to database</returns>
        public PrizeModel CreatePrize(PrizeModel prize)
        {
            prize.Id = 1;
            return prize;
        }
    }
}
