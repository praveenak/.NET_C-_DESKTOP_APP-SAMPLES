using DesktopAppSample3.Database;
using System.Data;
using System.Data.SqlClient;

namespace DesktopAppSample3
{
    public interface ISQLConnector : IConnector
    {
        /**
         * Multiple query SELECT with tables to add New
         * records to DB
         */
        DataSet fillDataSet(string query, string[] tableArr);

        /**
        * Get DataReader and Execute Query
        */
        object[] executeQuery(string query);

        /**
         * Reurn Connection Object
         */
        SqlConnection getConnectionObj();

        /**
         * Reurn transaction Object
         */
        SqlTransaction getTransactionObj();
    }
}
