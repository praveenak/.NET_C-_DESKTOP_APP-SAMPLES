using System;
using System.Data;
using System.Data.SqlClient;

namespace DesktopAppSample3.Database
{
    class DBSQLServConnect : ISQLConnector
    {
        private SqlConnection conn;
        private SqlTransaction transaction;

        public DBSQLServConnect(String connStr)
        {
            conn = new SqlConnection(connStr);
        }

        /**
         * Reurn Connection Object
         */
        public SqlConnection getConnectionObj()
        {
            return conn;
        }

        /**
         * Reurn transaction Object
         */
        public SqlTransaction getTransactionObj()
        {
            return transaction;
        }

        /**
         * Open connection to MS SQL SERVER
         */
        public void openConnection()
        {
            try
            {
                if (isConnectionOpen())
                    conn.Close();

                conn.Open();
                transaction = conn.BeginTransaction();
            }
            catch (Exception ex)
            {
               throw new ApplicationException("Database Server Error.", ex);
            }
        }

        /**
         * Close DB Connection
         */
        public void closeConnection()
        {
            try
            {
                transaction.Commit();
            }
            catch (Exception ex)
            {
                try
                {
                   transaction.Rollback();
                }
                catch (Exception ex2)
                {
                   throw new ApplicationException("Commit Transaction Failed.", ex2);
                }
            }
            conn.Close();
        }

        /**
         * Use DataReader for getting the data from SQL SERVER for the query
         */
        private SqlDataReader getDataReader(string query)
        {
            SqlCommand cmd = new SqlCommand(query, conn, transaction);
            //cmd.CommandTimeout = AppConstants.CommandTimeout;
            SqlDataReader rtnReader = cmd.ExecuteReader();
            return rtnReader;
        }

        /**
        * Get DataReader and Execute Query
        */
        public object[] executeQuery(string query)
        {
            object[] result = null;
            using (SqlCommand cmd = new SqlCommand())
            {
                SqlDataReader conReader = null;

                try
                {
                    conReader = getDataReader(query);
                    while (conReader.Read())
                    {
                        result = new Object[conReader.FieldCount];
                        conReader.GetValues(result);
                    }
                }
                catch (Exception ex)
                {
                    if (conReader != null)
                        conReader.Close();

                    throw new ApplicationException(ex.Message, ex);
                }
                finally
                {
                    if (conReader != null)
                        conReader.Close();
                }
            }

            return result;
        }

        /**
         * Check Connection status 
         */
        public bool isConnectionOpen()
        {
            if (conn.State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /**
        * Execute query using DataAdapter and return the 
        * Dataset will result data of the query provided.
        * Multiple SELECT can be done 
        */
        public DataSet fillDataSet(string query, string[] tableArr)
        {
            SqlCommand cmd = new SqlCommand(query, conn, transaction);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            if (tableArr != null)
            {
                for (int index = 0; index < tableArr.Length; index++)
                {
                    adapter.TableMappings.Add("Table" + index, tableArr[index]);
                }
            }

            DataSet dsData = new DataSet();
            adapter.Fill(dsData);

            adapter.Dispose();
            cmd.Dispose();

            return dsData;
        }
    }
}
