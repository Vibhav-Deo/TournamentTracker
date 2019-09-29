using System;
using System.Collections.Generic;
using System.Text;

namespace TrackerLibrary.Models
{
    public class MatchUpEntryModel
    {
        /// <summary>
        /// This class will hold all the match up entries
        /// it will contain score, competing team and the 
        /// previous round from where the team won and proceeded
        /// </summary>
        public TeamModel TeamCompeting { get; set; }
        public double Score { get; set; }
        public MatchUpModel ParentMatchUp { get; set; }
    }
}
