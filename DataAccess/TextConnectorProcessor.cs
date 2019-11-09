using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess.TextHelper
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string filename)
        {
            //
            return $"{ConfigurationManager.AppSettings["filePath"]}\\{filename}";
        }

        public static List<string> LoadFile(this string file)
        {
            if(!File.Exists(file))
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }

        public static List<PrizeModel> ConvertToPrizeModel(this List<string> lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();

            foreach(var line in lines)
            {
                string[] cols = line.Split(',');

                PrizeModel prize = new PrizeModel();

                prize.Id = int.Parse(cols[0]);
                prize.PlaceNumber = int.Parse(cols[1]);
                prize.PlaceName = cols[2];
                prize.PrizeAmount = decimal.Parse(cols[3]);
                prize.PrizePercentage = float.Parse(cols[4]);
                output.Add(prize);
            }

            return output;
        }

        public static List<PersonModel> ConvertToPersonModel(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();

            foreach (var line in lines)
            {
                string[] cols = line.Split(',');

                PersonModel person = new PersonModel();

                person.Id = int.Parse(cols[0]);
                person.FirstName = cols[1];
                person.LastName = cols[2];
                person.Email = cols[3];
                person.PhoneNumber = cols[4];
                output.Add(person);
            }

            return output;
        }

        public static List<TeamModel> ConvertToTeamModel(this List<string> lines, string PersonFile)
        {
            List<PersonModel> people = PersonFile.FullFilePath().LoadFile().ConvertToPersonModel();
            List<TeamModel> output = new List<TeamModel>();

            foreach (var line in lines)
            {
                string[] cols = line.Split(',');

                TeamModel team = new TeamModel();

                team.Id = int.Parse(cols[0]);
                team.TeamName = cols[1];
                string[] personIds = cols[2].Split('|');

                foreach(string id in personIds)
                {
                    team.TeamMembers.Add(people.Where(x => x.Id == int.Parse(id)).First());
                }

                output.Add(team);
            }

            return output;
        }

        public static void SaveToFile(this List<PrizeModel> prizes, string fileName)
        {
            List<string> lines = new List<string>();

            foreach(PrizeModel model in prizes)
            {
                lines.Add($"{model.Id},{model.PlaceName},{model.PlaceNumber},{model.PrizeAmount},{model.PrizePercentage}");

            }
            File.WriteAllLines(fileName.FullFilePath(),lines);
        }

        public static void SaveToFile(this List<PersonModel> people, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (PersonModel model in people)
            {
                lines.Add($"{model.Id},{model.FirstName},{model.LastName},{model.Email},{model.PhoneNumber}");

            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToFile(this List<TeamModel> team, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (TeamModel model in team)
            {
                lines.Add($"{model.Id},{model.TeamName},{ConvertToPersonList(model.TeamMembers)}");

            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        private static string ConvertToPersonList(List<PersonModel> people)
        {
            string output = "";

            if (people.Count == 0)
                return "";

            foreach(var person in people)
            {
                output += $"{person.Id}|";
            }
            output = output.TrimEnd('|');

            return output;
        }
    }
}
