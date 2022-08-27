using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ReadAndWriteToAFileExperiment
{

    // I'm aware that breaking a 10 entries in the for loop defeates the purpose and I should have used an array
    // I want to learn how to break using a CTRL + Shift + another key or something if it's possible
    class Program
    {
        static void Main(string[] args)
        {

            string text;
            var testPathway = @"C:\users\admin\desktop\Test";
            var testTextFile = @"C:\users\admin\desktop\Test\Text.txt";
            List<string> customerInfo = new List<string>();

            //---------------------------------------------

            if (!Directory.Exists(testPathway))
            {
                Directory.CreateDirectory(testPathway);
            }            

            File.AppendText(testTextFile).Close();

            //----------------------------------------------

            Console.WriteLine("Type anything and press Enter.");

            try
            {
                for (int i = -1; i < customerInfo.Count; i++)
                {
                    customerInfo.Add(Console.ReadLine());

                    if (customerInfo.Count >= 10)
                    {
                        break;
                    }
                }

                // This foreach loop prints each entry in the console and puts it in the text file
                foreach (string customer in customerInfo)
                {
                    // Write each customer in the console
                    //Console.WriteLine(customer);

                    // Assign "file" to open the text file and tell the computer to append it
                    using var file = File.Open(testTextFile, FileMode.Append);

                    // Assign "writer" to the new StreamWriter to write to "file" from before
                    using var writer = new StreamWriter(file);

                    // Write each customer in the "file"
                    writer.WriteLine(customer);

                    writer.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

            Console.WriteLine("Now let's read your list from the file. Please press Enter");
            Console.ReadKey();

            // This is how you read the text in a text file and print it in the console

            // This part is confusing
            // Store File.ReadAllText(textTextFile); in the variable "text"
            // Then Console.WriteLine(text); not the pathway
            // "text" includes the File.ReadAllText() method
            // If you don't do it this way, it will print the pathway "C:\\"

            // File.ReadAllText(); does not allow you to reference each line/entry like an array[0]
            text = File.ReadAllText(testTextFile);
            Console.WriteLine(text);
        }
    }
}