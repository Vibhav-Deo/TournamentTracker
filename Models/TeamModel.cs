using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerLibrary.Models
{
    public class TeamModel
    {
        /// <summary>
        /// Model for a team in the tournament
        /// Each team would consist of team members and a team name
        /// </summary>
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();
        public string TeamName { get; set; }
        public int Id { get; set; }
    }
}
