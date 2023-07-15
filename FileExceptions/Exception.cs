using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FileExceptions;

namespace FileExceptions
{
    public class Exception
    {
         public static string connectionString = "data source=COGNINE-L172;database=nrc;Integrated Security=SSPI;persist security info=True;";
        public string userinput;

        #region File not found
        public void Filenotfound()
        {
            try
            {
                string filePath = @"Documents";
                string fileContents = File.ReadAllText(filePath);

                //file not found

                if (File.Exists(fileContents))
                {
                    throw new InvalidDataException("file not found!");
                }
                //Invalid file Format
                if (File.Exists(fileContents + ".doc"))
                {
                    throw new InvalidDataException("Invalid file Format!");
                }
            }

            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found! Please check the file path and try again.");
            }
            catch (InvalidDataException ex)
            {
                Console.WriteLine($"Invalid file format: {ex.Message}");
            }

        }
        #endregion

        #region Database Exceptions 
        public void DatabaseExceptions()
        {
            try
            {
                //string connectionString = "Data Source = 192.168.0.30;Initial Catalog =AnilApplicationDb;User Id = User5;Password = CDev005#8";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Perform database operations
                    string sqlQuery = "SELECT * FROM MSreplication_options";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        // Process the query result
                        while (reader.Read())
                        {
                            // Process each row
                            // ...
                        }

                        reader.Close();
                    }

                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL-specific exceptions
                Console.WriteLine("A SQL exception occurred: " + ex.Message);
                FallBackException();
            }
            catch (System.Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine("An exception occurred: " + ex);
            }
        }

        public static void FallBackException()
        {
            Console.WriteLine("Check the Connection string! " + connectionString);
        }

        #endregion
        #region Network Exception
        public void NetworkExceptions()
        {
            try
            {
                // Perform network-related operations
                WebClient client = new WebClient();
                string response = client.DownloadString("https://api.example.com/data");

            }
            catch (WebException ex)
            {
                // Handle web-related exceptions
                Console.WriteLine("A web exception occurred: " + ex.Message);

                // Check for specific network-related errors
                if (ex.Status == WebExceptionStatus.Timeout)
                {
                    // Retry the operation or perform fallback logic for timeouts
                    RetryOrPerformFallback();
                }
              
            }
            catch (System.Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine("An exception occurred: " + ex);

            }

            // Retry operation or perform fallback logic for network timeouts
            void RetryOrPerformFallback()
            {
                const int MaxRetries = 3;
                const int InitialDelayMs = 1000; // 1 second

                bool success = false;
                int retries = 0;
                int delayMs = InitialDelayMs;

                while (!success && retries < MaxRetries)
                {
                    try
                    {
                        WebClient client = new WebClient();
                        string response = client.DownloadString("https://api.example.com/data");

                        // Process the response
                        // ...

                        success = true; // Operation succeeded
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine("A web exception occurred: " + ex.Message);

                        // Check if it's worth retrying the operation
                        if (IsRetryableException(ex))
                        {
                            retries++;
                            Console.WriteLine($"Retry attempt: {retries}");

                            // Implement exponential backoff to increase delay between retries
                            delayMs *= 2;

                            // Wait before retrying
                            System.Threading.Thread.Sleep(delayMs);
                        }
                       
                    }
                    catch (System.Exception ex)
                    {
                        Console.WriteLine("An exception occurred: " + ex.Message);
                    }
                }

                if (!success)
                {
                    Console.WriteLine("Operation failed after maximum retries.");
                }

                // Check if the exception is worth retrying
                bool IsRetryableException(WebException ex)
                {
                    // Check for specific exceptions that can be retried
                    return ex.Status == WebExceptionStatus.Timeout || ex.Status == WebExceptionStatus.ConnectionClosed ||
                        ex.Status == WebExceptionStatus.NameResolutionFailure || ex.Status == WebExceptionStatus.ProtocolError;
                }

            }

            
        }
        #endregion
        #region Inputvalidation Exceptions
        public void InputvalidationExceptions()
        {
            try
            {
                Console.WriteLine("Enter a numeric value:");
                userinput = Console.ReadLine();

                // Attempt to parse the input as a numeric value
                double numericValue = double.Parse(userinput);

            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input! Please enter a valid numeric value. "+ userinput);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("An exception occurred: " + ex.Message);
            }
        }
        #endregion

        #region Multithreading Exceptions
        public void MultithreadingExceptions()
        {
            //Object for WorkerThread class 
            WorkThread workThread = new WorkThread();
            // Create a worker thread
            Thread WorkerThread = new Thread(workThread.WorkerThread);

            // Start the worker thread
            WorkerThread.Start();

            // Do other tasks in the main thread
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Main thread: Performing operation " + i);
                Thread.Sleep(1500); // Simulating some work
            }

            // Wait for the worker thread to complete
            WorkerThread.Join();

            // Continue with other operations or terminate the application
            Console.WriteLine("Main thread: Worker thread completed. Exiting the application.");

        }
        #endregion

    }
}
