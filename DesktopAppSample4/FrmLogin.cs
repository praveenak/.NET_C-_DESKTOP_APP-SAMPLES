using DesktopAppSample3.Database;
using DesktopAppSample4.Classes;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

/**
 * Form Login
 */ 
namespace DesktopAppSample4
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            //Default method called
            InitializeComponent();

            //Custom Initialization
            txtPassword.UseSystemPasswordChar = true; //Password to be hidden.
            txtPassword.MaxLength = 25; //Max limit of length of password field
            txtUsername.MaxLength = 25; //Max limit of length of username field
        }

        //Tab or Enter key to lead to Password field
        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        //Tab or enter key calls Login method
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login();
            }
        }

        //Highlight the Border of Button Login on Mouse Move
        private void btnLogin_MouseMove(object sender, MouseEventArgs e)
        {
            btnLogin.FlatStyle = FlatStyle.Flat;
        }

        //Highlight the Border of Button Login on Mouse Leave
        private void btnLogin_MouseLeave(object sender, EventArgs e)
        {
            btnLogin.FlatStyle = FlatStyle.Standard;
        }

        //Highlight the Border of Button Exit on Mouse Leave
        private void btnExit_MouseLeave(object sender, EventArgs e)
        {
            btnExit.FlatStyle = FlatStyle.Standard;
        }

        //Highlight the Border of Button Login on Mouse Move
        private void btnExit_MouseMove(object sender, MouseEventArgs e)
        {
            btnExit.FlatStyle = FlatStyle.Flat;
        }

        //Exit the Application on Exit button click
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Invoke Login on Login button click
        private void btnLogin_Click(object sender, EventArgs e)
        {
            login(); //Button click event calls Login method
        }

        /**
         * Validate Login fields for valid entries and against database entries
         * and call the MDI MAIN Screen.
         */
        private void login()
        {
            //Create an instance of application validator
            AppValidator appValidatorObj = AppValidator.getValidatorInstance();

            //Check if Username field is empty
            if (txtUsername.Text == string.Empty)
            {
                txtFieldFocus(txtUsername);
                return;
            }

            //Get value of username and password
            String username = txtUsername.Text.Trim();

            //Validate Username for alphanumeric characters
            /*if (appValidatorObj.isValidField(username, 7))
            {
                txtFieldFocus(txtUsername);
            }
            else
            {
                lblStatus.Text = "1. Can contain digit or alphanumeric characters" + "\n" +
                                 "2. Length at least 3 characters and maximum of 15";
                return;
            }*/

            //Check if password field is empty
            if (txtPassword.Text == string.Empty)
            {
                txtFieldFocus(txtPassword);
                return;
            }

            String password = txtPassword.Text.Trim();
            //Validate Password for alphanumeric and special characters
            /*if (appValidatorObj.isValidField(password, 8))
            {
                txtFieldFocus(txtPassword);
            }
            else
            {
                lblStatus.Text = "Password :" +
                                             "1. Must contain at least one digit" + "\n" +
                                             "2. Must contain at least one uppercase character." + "\n" +
                                             "3. Must contain at least one special symbol" + "\n" +
                                             "4. Length at least 8 characters and maximum of 15";
                return;
            }*/

            /**
             * Establish Database Connectivity Based on type of Database Server(MS SQL SERVER if default type)
             * If MY SQL, use IConnector dbConnector = connectionObj.connectToServer("MY SQL");
             * SQL specific methods are stored in interface ISQLConnector which is child of IConnector interface.
             * Similiarly for other types of Database Servers
             * DB Connection takes the database connection string identifier from DbConnection.config file.
             * When "default" is passed, default connection identifier from config file is choosen.
             * Another instance of DBConnection is to be created to connect to different database for
             * file handling.
             */
            DBConnection connectionObj = new DBConnection(AppConstants.DEFAULT_DBCONN_IDENTIFIER);
            DesktopAppSample3.ISQLConnector dbConnector = (DesktopAppSample3.ISQLConnector)connectionObj.connectToServer(AppConstants.MS_SQL_SERVER);

            /**
             * Initialization fields of the application
             * is stored in AppData object. Validate input Username and Password 
             * with the values in Database.
             */ 
            AppData dataObj = getLoginData(username, password, dbConnector);

            if (dataObj != null)
            {
                dataObj.setDBConnector(dbConnector);

                FrmMDIMain mainForm = new FrmMDIMain(); //Pass Data Object to Main Form for using the stored username and other fields.
                this.Hide();
                mainForm.Show();
                txtPassword.Text.Trim();
            }
            else //Invalid entries provided
            {
                lblStatus.Text = "Invalid Username/Password.";
            }
        }

        //Set the Focus for textField and update the status field for relavant errors
        private void txtFieldFocus(TextBox txtField)
        {
            txtField.Focus();
            lblStatus.Text = txtField.Tag.ToString();
        }

        /**
         * Establish Database Connectivity and check for validity of login fields
         * and return the initial parameters from Login table for initializing the application
         */
        public AppData getLoginData(String username, String password, DesktopAppSample3.ISQLConnector dbConnector)
        {
            String sqlQuery = "SELECT * FROM userlogin WHERE username='" + username + "'";//and userpassword=password and
                                                                                          //active="Y"
            AppData dataObj = null;

            try
            {
                dbConnector.openConnection();
                object[] loginDataArr = dbConnector.executeQuery(sqlQuery);
                if (loginDataArr != null && loginDataArr.Length > 0)
                {
                    dataObj = new AppData();                                        //DB Mapping field names                            
                    dataObj.setUsername(loginDataArr[4].ToString());                //username
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, AppConstants.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);

                //dbConnector.closeConnection();

                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                dbConnector.closeConnection();
            }
            return dataObj;
        }

        private void pnlLogin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void picAudeasy_Click(object sender, EventArgs e)
        {

        }
    }
}
