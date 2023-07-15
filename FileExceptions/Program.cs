using System;
using FileExceptions;

public class Program
{


    public static void Main(string[] args)
    {
        FileExceptions.Exception Exception = new FileExceptions.Exception();
        {
            ////Exception.Filenotfound();
            ////Exception.DatabaseExceptions();
            ////Exception.NetworkExceptions();
            //  Exception.InputvalidationExceptions();
            Exception.MultithreadingExceptions();



        }
    }
}