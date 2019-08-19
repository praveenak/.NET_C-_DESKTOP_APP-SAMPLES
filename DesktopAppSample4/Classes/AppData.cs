using System;

namespace DesktopAppSample4.Classes
{
    public class AppData
    {
        //Login Table entries
        String username;                            //Username field stored for display in MDI Form

        DesktopAppSample3.ISQLConnector dbConnector;                  //DbConnetor Instance to be used across the application

        /**
         * Return DB Connector Object
         */
        public DesktopAppSample3.ISQLConnector getDBConnector()
        {
            return dbConnector;
        }

        /**
         * Set DB Connector Object
         */
        public void setDBConnector(DesktopAppSample3.ISQLConnector connObj)
        {
            dbConnector = connObj;
        }

        /**
         * Return Logged In Username
         */
        public string getUsername()
        {
            return username;
        }

        /**
         * Set Username
         */
        public void setUsername(string name)
        {
            username = name;
        }
    }
}
