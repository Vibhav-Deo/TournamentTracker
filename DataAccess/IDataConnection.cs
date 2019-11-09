using System;
using System.Collections.Generic;
using System.Text;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public interface IDataConnection
    {
        PrizeModel CreatePrize(PrizeModel prize);
        PersonModel CreatePerson(PersonModel person);
        List<PersonModel> GetAllPersons();
        TeamModel CreateTeam(TeamModel team);
    }
}
