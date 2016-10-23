using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] drives = System.Environment.GetLogicalDrives();

            //foreach (string x in drives)
            //{

            //    System.IO.DriveInfo di = new System.IO.DriveInfo(x);

            //    // Here we skip the drive if it is not ready to be read. This
            //    // is not necessarily the appropriate action in all scenarios.
            //    if (di.IsReady)
            //    {
            //        walkthrough(di.RootDirectory);
            //    }
            //}

            walkthrough(new System.IO.DirectoryInfo(@"C:\Users\User\Downloads"));

           // Console.WriteLine("Done");

            Console.ReadKey();

        }

        public static void walkthrough(System.IO.DirectoryInfo root)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            try
            {
                files = root.GetFiles("*.*");
            }
            // This is thrown if even one of the files requires permissions greater
            // than the application provides.
            catch (UnauthorizedAccessException e)
            {
                // This code just writes out the message and continues to recurse.
                // You may decide to do something different here. For example, you
                // can try to elevate your privileges and access the file again.

            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                //Console.WriteLine(e.Message);
            }
            catch { 
            }

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    // In this example, we only access the existing FileInfo object. If we
                    // want to open, delete or modify the file, then
                    // a try-catch block is required here to handle the case
                    // where the file has been deleted since the call to TraverseTree().
                    try
                    {
                        Console.WriteLine(fi.FullName);
                    }
                    catch { }
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    // Resursive call for each subdirectory.
                    Task t = new Task(() => walkthrough(dirInfo));
                    t.Start();
                    //walkthrough(dirInfo);
                }

                Task.WhenAll(new Task(()=> done()));
            }

            
        }

        public static void done()
        {
            Console.WriteLine("Done");
        }
    }


}
