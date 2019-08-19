using System;
using System.Net.NetworkInformation;

namespace DesktopAppSample1
{
    public class InternetConnectivity
    {
        /**
        * PING to check internet connectivity.
        */
        public static bool PingTest()
        {
            bool connectionExists = false;
            try
            {
                Ping ping = new Ping();
                PingReply pingStatus = ping.Send("www.google.com", 1000);

                if (pingStatus.Status == IPStatus.Success)
                {
                    connectionExists = true;
                }
                else
                {
                    connectionExists = false;
                }
            }
            catch (PingException ex)
            {
                throw new ApplicationException("Ping Failed.", ex);
            }

            return connectionExists;
        }

        /**
         * Send a web request to a website which assumed to be online always.
         */
        public static bool WebRequestTest()
        {
            string url = "http://www.google.com";
            try
            {
                System.Net.WebRequest myRequest = System.Net.WebRequest.Create(url);
                System.Net.WebResponse myResponse = myRequest.GetResponse();
            }
            catch (System.Net.WebException ex) 
            {
                return false;
            }
            return true;
        }

        /**
         * TCP Socket : check whether port 80, default port for http traffic, of an always online website.  
         */
        public static bool TcpSocketTest()
        {
            try
            {
                System.Net.Sockets.TcpClient client =
                    new System.Net.Sockets.TcpClient("www.google.com", 80);
                client.Close();
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
    }
}
