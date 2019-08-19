using System;
using System.Configuration;

namespace DesktopAppSample3.Database
{
    public class DBConnection
    {
        private String type = "MS SQL SERVER";
        private String conStrIdentifier;

        /**
         * Set Connection Timeout : 0 : TBD
         * Connection Pooling
         * Command Timeout
         */
        public DBConnection(String dbConnIdentifier)
        {
            conStrIdentifier = dbConnIdentifier;
        }

        //Connect to MS SQL SERVER
        public IConnector connectToServer(String dbType)
        {
            if (dbType.Equals(type))//MS SQL SERVER
            {
                type = dbType;
                return new DBSQLServConnect(getConnStrByName(conStrIdentifier));
            }
            /*else if()//MY SQL or ORACLE or INFORMIX etc
             * {
             * Create an instance of new DBMYSQLConnect
             * }
             */
            return null;
        }

        /**
         * Retrieves a connection string by name from DBConnection.config file.
         * Returns null if the name is not found.
         */
        private string getConnStrByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
}
