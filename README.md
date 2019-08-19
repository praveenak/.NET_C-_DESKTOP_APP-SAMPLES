# .NET_CSharp_DESKTOP_APP_SAMPLES

DesktopAppSample 1 - Class Library for checking if internet connectivity exists or not.

DesktopAppSample 2 - Class Library for FTP Upload/Download of files to remote server.

DesktopAppSample 3 - Class Library for MS SQL SERVER DB Connection using Factory Design Pattern. After uploading the Project, Right Click on References and add "System.Configuration" and then build the Sample. Database Credentials are to be maintained in a new config file "DBConnection.config" which inturn is accessed using ConfigurationManager in code to ensure security of the credentials.

DesktopSample 4 - Windows Form Client Application for Login. This application has dependency on Database dll created above.

                  1. Add a new Reference to refer to DesktopAppSample3.dll for Database Connectivity.

                  2. Create a UserLogin table in MS SQL SERVER DB with fields 'username' and 'userpassword'.

                  3. App.config file to be edited to refer to new config file
                  
                  4. DbConnection.config to be added newly.
                  
                  5. Dbconnection.config - property "copy to outpt directory" to be set to "copy always".                   
