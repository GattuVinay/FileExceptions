using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExceptions
{
    public class WorkThread
    {
       public  void WorkerThread()
        {
            try
            {
                // Perform background thread operations
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("Worker thread: Performing operation " + i);
                    Thread.Sleep(1000); // Simulating some work
                }

                // Simulating an exception in the background thread
                throw new InvalidOperationException("Simulated exception in worker thread");
            }
            catch (System.Exception ex)
            {
                // Log the exception
                Console.WriteLine("Exception occurred in the worker thread: " + ex.Message);

                // Communicate the exception to the main thread or user interface for appropriate action
                // You can use synchronization mechanisms like events or message passing to accomplish this
                // For simplicity, we are just printing the exception in the main thread
                Console.WriteLine("Main thread: Exception occurred in the worker thread!");
            }
        }
    }
}
