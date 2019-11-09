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
        private readonly string dbName = "TournamentsDB";
        public PersonModel CreatePerson(PersonModel person)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionStr(dbName)))
            {
                var personList = new DynamicParameters();
                personList.Add("@FirstName", person.FirstName);
                personList.Add("@LastName", person.LastName);
                personList.Add("@EmailAddress", person.Email);
                personList.Add("@PhoneNumber", person.PhoneNumber);
                personList.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("SP_PeopleTable_Insert", personList, commandType: CommandType.StoredProcedure);

                person.Id = personList.Get<int>("@id");
            }
            return person;
        }

        //TODO: Make sure that the prize is actually saved to database.
        /// <summary>
        /// Saves a prize to database
        /// </summary>
        /// <param name="prize">Prize to be save to database</param>
        /// <returns>The prize that was save to database</returns>
        public PrizeModel CreatePrize(PrizeModel prize)
        {
            using(IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionStr(dbName)))
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

        public TeamModel CreateTeam(TeamModel team)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionStr(dbName)))
            {
                var teams = new DynamicParameters();

                teams.Add("@TeamName", team.TeamName);
                teams.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("SP_TeamTable_Insert", teams, commandType: CommandType.StoredProcedure);

                team.Id = teams.Get<int>("@id");

                foreach(var person in team.TeamMembers)
                {
                    teams = new DynamicParameters();
                    teams.Add("@TeamId", team.Id);
                    teams.Add("@PersonId", person.Id);

                    connection.Execute("SP_TeamMembers_Insert", teams, commandType: CommandType.StoredProcedure);
                }
            }
            return team;
        }

        public List<PersonModel> GetAllPersons()
        {
            List<PersonModel> output;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.ConnectionStr(dbName)))
            {
                output = connection.Query<PersonModel>("dbo.SP_PeopleTable_GetAll").AsList();
            }

            return output;
        }
    }
}
