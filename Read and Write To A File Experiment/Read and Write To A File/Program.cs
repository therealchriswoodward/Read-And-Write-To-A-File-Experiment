// REVIEW: I left a few comments in your other repo so I won't comment
//  on the same issues here.
//  Unless I feel like it.

using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ReadAndWriteToAFileExperiment
{
    // REVIEW: I think it's good to pick a license for all code you put
    //  on github. You never know if someone finds it useful and would
    //  like to use your code. A license controls how they can use the code
    //  Unlicensed code (like this one) I can't even read without your consent
    //  as you have full control of it as copyright holder.
    //  Unlicensed doesn't mean public domain which is a quite common
    //  misunderstanding.
    //  When creating the GIT repo pick for example the MIT license which
    //  allows others to use your code with few obligations.

    // REVIEW: You (by mistake mostly) commited output binary files to the
    //  repo. This is a quite common mistake but something one should
    //  be careful about avoid as it can make the size of the GIT repo
    //  go up as GIT don't compress binary diffs that well.
    //  Deleting the files is a good thing but they are still in the history
    //  once commited. To remove files you have to do a history rewrite
    //  which is complex and by nature destructive.
    //  Easiest just making sure you never commit the output binary files.

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

            // REVIEW: I would prefer to use using var to ensure
            //  StreamWriter is always disposed. Now given how the
            //  code looks it's hard to imagine a scenario where
            //  close isn't called but I just think it's a good
            //  pattern that should be used in general.
            File.AppendText(testTextFile).Close();

            //----------------------------------------------

            Console.WriteLine("Type anything and press Enter.");

            try
            {
                // REVIEW: This I think is essentially a while(true) looking
                //  at how the rest of the code looks. It looks odd to me
                for (int i = -1; i < customerInfo.Count; i++)
                {
                    // REVIEW: Console.ReadLine() might return
                    //  null but your List<string> says all entries
                    //  should be not null. If you like to support
                    //  null entries the signature should be List<string?>
                    //  VS complains about this since the project has
                    //  Nullable enabled
                    customerInfo.Add(Console.ReadLine());

                    // REVIEW: If you want to capture 10 customer infos I 
                    //  think this loop could be a while loop and this is the
                    //  test condition to determine when done
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

                    // REVIEW: You could open this stream once instead of opening
                    //  once per customer in your collection.
                    //  Potentially a performance boost if this was a real program
                    // Assign "file" to open the text file and tell the computer to append it
                    using var file = File.Open(testTextFile, FileMode.Append);

                    // Assign "writer" to the new StreamWriter to write to "file" from before
                    using var writer = new StreamWriter(file);

                    // Write each customer in the "file"
                    writer.WriteLine(customer);

                    // REVIEW: using var writer ensure .Dispose is closed
                    //  automatically and in all possible code paths.
                    //  .Dipose calls .Close so this should be unnecessary
                    //  If you would like explicit close I would expect
                    //  explicit closing of the file object as well after the
                    //  writer object is closed.
                    //  And handling exceptions
                    //  using var does all this automatically.
                    writer.Close();
                }
            }
            catch (Exception e)
            {
                // REVIEW: Printing out some details of the exception is great
                //  but if this was a log I would also like the call stack to
                //  be printed. I have had to handly many production issues
                //  where I only have the message and while it helps it would help
                //  lot more to know where it threw.
                // REVIEW: My pet thing to care about which no one else cares about
                //  is to make sure that string interpolation uses a known culture.
                //  Which culture you have impact on how the text is formatted and
                //  relying on the default is the source of an endless line of
                //  "it works on my machine" kind of errors.
                //  String interpolation out of the box uses the CultureInfo.CurrentCulture
                //  You have little guarantee of what that is as that can be changed
                //  by code so you have little guarantee how the text is formatted.
                //  I think it's better to just specify the Culture explicitly
                //  so I know how it comes out. Unfortunately string interpolation
                //  don't make it easy for us which is one reason I am kind of cool
                //  on string interpolation despite many benefits.
                //  If you have total control of your source code including all dependencies
                //  and transitive dependencies you can set CultureInfo.CurrentCulture
                //  and CultureInfo.DefaultThreadCurrentCulture to the culture you prefer
                //  when the program start.
                //  But most devs don't care about this (until they experience their
                //  first 24 hour debugging sessions because of unexpected culture formatting)
                //  so from most devs perspective it's fine.
                // REVIEW: Perhaps write the error to the Console.Error stream?
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

            // REVIEW: File.ReadLines will return the file content as an array

            // File.ReadAllText(); does not allow you to reference each line/entry like an array[0]
            text = File.ReadAllText(testTextFile);
            Console.WriteLine(text);
        }
    }
}