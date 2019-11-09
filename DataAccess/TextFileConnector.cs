using System;
using System.Collections.Generic;
using System.Text;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelper;
using System.Linq;

namespace TrackerLibrary.DataAccess
{
    public class TextFileConnector : IDataConnection
    {
        private const string PrizesFile = "PrizeModel.csv";
        private const string PersonFile = "PersonModel.csv";
        private const string TeamFile = "TeamModel.csv";
        public PersonModel CreatePerson(PersonModel person)
        {
            List<PersonModel> people = PersonFile.FullFilePath().LoadFile().ConvertToPersonModel();

            int currentId = 1;

            if (people.Count > 0)
            {
                currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }

            person.Id = currentId;

            people.Add(person);

            people.SaveToFile(PersonFile);

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
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModel();

            int currentId = 1;

            if(prizes.Count > 0)
            {
                currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }

            prize.Id = currentId;

            prizes.Add(prize);

            prizes.SaveToFile(PrizesFile);

            return prize;
        }

        public TeamModel CreateTeam(TeamModel team)
        {
            List<TeamModel> teams = TeamFile.FullFilePath().LoadFile().ConvertToTeamModel(PersonFile);

            int currentId = 1;

            if (teams.Count > 0)
            {
                currentId = teams.OrderByDescending(x => x.Id).First().Id + 1;
            }

            team.Id = currentId;

            teams.Add(team);

            teams.SaveToFile(TeamFile);

            return team;
        }

        public List<PersonModel> GetAllPersons()
        {
            List<PersonModel> output = PersonFile.FullFilePath().LoadFile().ConvertToPersonModel();
            return output;
        }
    }
}
