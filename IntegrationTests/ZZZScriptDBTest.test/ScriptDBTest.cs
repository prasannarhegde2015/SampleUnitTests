using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Threading;


namespace ZZZScriptDBTest.test
{
    [TestClass]
    public class ScriptDBTest
    {
        [TestMethod]
        public void RunCheckDBScript()
        {
            string m_strLiftDir = GetLiftRunFolder();
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            string csLift = Directory.GetParent(m_strLiftDir).Parent.FullName;
            string liftsrv = Path.Combine(csLift, "liftsrv");
            string checkdbscript = Path.Combine(liftsrv, "csscript", "util","checkdb.css");
            string checkDBOutFolder = Path.Combine(csLift, "lift", "Default", "log", "checkdb"); //C:\csLift\lift\Default\log\checkdb
            if(Directory.Exists(checkDBOutFolder))
            {
                Directory.Delete(checkDBOutFolder, true);
            }


            Process checkdb = new Process();
            try
            {
                checkdb.StartInfo.UseShellExecute = true;
                checkdb.StartInfo.FileName = strMcsscrip;
                checkdb.StartInfo.Arguments = String.Format("{0} checkdb \"\" -db prodrtdb", checkdbscript);

                Trace.WriteLine(strMcsscrip + " " + checkdb.StartInfo.Arguments);
                Assert.IsTrue(checkdb.Start());
                checkdb.WaitForExit();
                CheckIfExportDirExists(checkDBOutFolder);
                TestTextFileSize(checkDBOutFolder);
            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }

        }

        [TestMethod]
        public void RunCheckRefListScript()
        {
            string m_strLiftDir = GetLiftRunFolder();
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            string csLift = Directory.GetParent(m_strLiftDir).Parent.FullName;
            string liftsrv = Path.Combine(csLift, "liftsrv");
            string checkRefListscript = Path.Combine(liftsrv, "csscript", "util", "checkdbreflists.css");
            string checkDBOutFolder = Path.Combine(csLift, "lift", "Default", "log", "checkdb"); //C:\csLift\lift\Default\log\checkdb
            
            Process CheckRefList = new Process();
            try
            {
                CheckRefList.StartInfo.UseShellExecute = true;
                CheckRefList.StartInfo.FileName = strMcsscrip;
                CheckRefList.StartInfo.Arguments = String.Format("{0} checkAllDBRefLists \"\" -db db", checkRefListscript);

                Trace.WriteLine(strMcsscrip + " " + CheckRefList.StartInfo.Arguments);
                Assert.IsTrue(CheckRefList.Start());
                CheckRefList.WaitForExit();
                CheckIfExportDirExists(checkDBOutFolder);
                TestCSVFileSize(checkDBOutFolder);
            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }

        }






        public string GetLiftRunFolder()
        {

            string liftrun = String.Empty;
            //// First check for the development environment variable
            //string liftrun = Environment.GetEnvironmentVariable("LIFTRUN");

            //if (!String.IsNullOrEmpty(liftrun))
            //{
            //    return liftrun;
            //}

            string strLiftRoot = ConfigurationManager.AppSettings.Get("LiftRoot"); //(@"C:\csLift\lift\Default\root\");
            //TestContext.WriteLine("Provided Lift Root folder is " + strLiftRoot);

            string csLift = Directory.GetParent(strLiftRoot).Parent.Parent.FullName; 
            

            liftrun = Path.Combine(csLift, "liftnt", "run");

            // TestContext.WriteLine("Found Lift Run folder " + liftrun);

            return liftrun;
        }

        public void CheckIfExportDirExists(string Dir)
        {
            System.Threading.Thread.Sleep(3000);
            Assert.IsTrue(System.IO.Directory.Exists(Dir), "checkdb directory not found in location! "+"\n");
            Trace.WriteLine("Found checkdb Directory in location\n");
            
        }

        public void TestTextFileSize(string checkDBOutFolder)
        {
            try
            {
                
                string[] txtList = Directory.GetFiles(checkDBOutFolder, "*.txt");

                foreach (string t in txtList)
                {
                    FileInfo FI = new FileInfo(t);
                    long size = FI.Length;
                   
                    if (size == 0)
                    {
                       
                        Trace.WriteLine("The following file is size 0 ------- " + t);
                    }
                    else
                    {

                        Trace.WriteLine("\nThe following file is GREATER THAN size 0 ------- " + t + " -------- File size in bytes is : " + size + "\n");
                    }
                  
                }
                Trace.WriteLine("\n"+ "Done checking files...");
                
            }
            catch (DirectoryNotFoundException e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
        }

        public void TestCSVFileSize(string checkDBOutFolder)
        {
            try
            {

                string[] csvList = Directory.GetFiles(checkDBOutFolder, "*.csv");

                foreach (string c in csvList)
                {
                    FileInfo FI = new FileInfo(c);
                    long size = FI.Length;
                    if (size == 0)
                    {

                        Trace.WriteLine("The following file is size 0 ------- " + c);
                    }
                    else
                    {

                        Trace.WriteLine("\nThe following file is GREATER THAN size 0 ------- " + c + " -------- File size in bytes is : " + size + "\n");
                    }

                }
                Trace.WriteLine("\n" + "Done checking files...");

            }
            catch (DirectoryNotFoundException e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
        }
    }
}
