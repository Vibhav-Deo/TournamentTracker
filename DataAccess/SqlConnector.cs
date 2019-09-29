using System; 
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using Dapper;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        //TODO: Make sure that the prize is actually saved to database.
        /// <summary>
        /// Saves a prize to database
        /// </summary>
        /// <param name="prize">Prize to be save to database</param>
        /// <returns>The prize that was save to database</returns>
        public PrizeModel CreatePrize(PrizeModel prize)
        {
            using(IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionStr("TournamentsDB")))
            {
                var prizes = new DynamicParameters();
                prizes.Add("@PlaceName", prize.PlaceName);
                prizes.Add("@PlaceNUmber", prize.PlaceNumber);
                prizes.Add("@PrizeAmount", prize.PrizeAmount);
                prizes.Add("@PrizePercentage", prize.PrizePercentage);
                prizes.Add("@id", dbType: DbType.Int32, direction:ParameterDirection.Output);

                connection.Execute("SP_PrizesTable_Insert", prizes, commandType: CommandType.StoredProcedure);

                prize.Id = prizes.Get<int>("@id");
            }
            return prize;
        }
    }
}
