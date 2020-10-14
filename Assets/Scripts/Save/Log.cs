using System.Collections;
using System.Collections.Generic;
using System.IO;

public static class Log
{
    static string path = @"C:\Users\user";

    public static void WriteLog<T>(T whatToWrite)
    {
        try
        {
            using (StreamWriter streamWriter = File.AppendText(path.ToString()))
            {
                streamWriter.WriteLine(whatToWrite);
            }
        }
        catch (IOException e)
        {
            throw;
        }
    }

    public static void WriteLog<T>(T[] whatToWrite)
    {
        try
        {
            using (StreamWriter streamWriter = File.AppendText(path.ToString()))
            {
                foreach (T item in whatToWrite)
                {
                    streamWriter.WriteLine(item);
                }                
            }
        }
        catch (IOException e)
        {
            throw;
        }
    }
}
