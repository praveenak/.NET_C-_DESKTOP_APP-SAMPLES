using System.Data;

namespace DesktopAppSample3.Database
{
    public interface IConnector
    {
        /**
         * Open DB connection and call begin Transaction
         */
        void openConnection();

        /**
         * Close Connect and Commit the Transaction. If
         * and errors, Rollback the transaction
         */
        void closeConnection();

        /**
         * Check Connection status
         */
        bool isConnectionOpen();
    }
}
