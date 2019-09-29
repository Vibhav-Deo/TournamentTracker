using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using TrackerLibrary.DataAccess;
using TrackerLibrary.Enums;
namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public  static IDataConnection Connection { get; private set; }
        //TODO In future this can be modified to read YAML/Config file of some sort 
        public static void InitializeConnection(DatabaseType connectionType)
        {
            switch (connectionType)
            {
                case DatabaseType.SQL:
                    //TODO: Setup sql connection correctly
                    SqlConnector sqlConnector = new SqlConnector();
                    Connection = sqlConnector;
                    break;
                case DatabaseType.TextFile:
                    TextFileConnector textFileConnector = new TextFileConnector();
                    Connection = textFileConnector;
                    break;
                default:
                    break;
            }
        }

        public static string ConnectionStr(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
