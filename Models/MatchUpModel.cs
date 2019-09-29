using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerLibrary.Models
{
    public class MatchUpModel
    {
        /// <summary>
        /// This model holds on to list of all the teams entered in the tournament,
        /// winner for the round and the round number
        /// </summary>
        public List<MatchUpEntryModel> Entries { get; set; } = new List<MatchUpEntryModel>();
        public TeamModel Winner { get; set; }
        public int MatchUpRound { get; set; }

    }
}
