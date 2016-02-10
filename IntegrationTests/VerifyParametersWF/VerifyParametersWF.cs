using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.IO;
using Microsoft.Win32;
using System.Configuration;
using System.Text.RegularExpressions;



namespace IntegrationTests.VerifyParameters
{
    [TestClass]
    public class VerifyParametersWF : TestDriverBase
    {

        [TestCategory("REDUNIFSONLY"), TestMethod]
        public void VerifyParametersWorkFlowTestREDUNIFSONLY()
        {
            try
            {
                CheckIfLowisExist();
                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("FSONLY");

                }


                StartRTUEmu();
                CreateESPWells("FSONLY");

                ZeroOutESPVerifyParametersFields(1);
                RunVerifyParametersOnESP(1);
                OutputESPVerifyParametersToCSV(1);
                for (int i = 2; (i < (NumberofWells + 2)); i++)
                {
                    ZeroOutESPVerifyParametersFields(i);
                    RunVerifyParametersOnESP(i);
                    OutputESPVerifyParametersToCSV(i);
                    Assert.IsTrue(CompareOutputFiles("VerifyBE1.csv", "VerifyBE" + i + ".csv"));
                }
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("FSONLY");

                }
            }
        }

        [TestCategory("REDUNIFSWPHX"), TestMethod]
        public void VerifyParametersWorkFlowTestREDUNIFSWPHX()
        {
            try
            {
                CheckIfLowisExist();
                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("FSWPHX");

                }


                StartRTUEmu();
                CreateESPWells("FSWPHX");

                ZeroOutESPVerifyParametersFields(1);
                RunVerifyParametersOnESP(1);
                OutputESPVerifyParametersToCSV(1);
                for (int i = 2; (i < (NumberofWells + 2)); i++)
                {
                    ZeroOutESPVerifyParametersFields(i);
                    RunVerifyParametersOnESP(i);
                    OutputESPVerifyParametersToCSV(i);
                    Assert.IsTrue(CompareOutputFiles("VerifyBE1.csv", "VerifyBE" + i + ".csv"));
                }
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("FSWPHX");

                }
            }
        }

        [TestCategory("REDUNIVSONLY"), TestMethod]
        public void VerifyParametersWorkFlowTestREDUNIVSONLY()
        {
            try
            {
                CheckIfLowisExist();
                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("VSONLY");

                }


                StartRTUEmu();
                CreateESPWells("VSONLY");

                ZeroOutESPVerifyParametersFields(1);
                RunVerifyParametersOnESP(1);
                OutputESPVerifyParametersToCSV(1);
                for (int i = 2; (i < (NumberofWells + 2)); i++)
                {
                    ZeroOutESPVerifyParametersFields(i);
                    RunVerifyParametersOnESP(i);
                    OutputESPVerifyParametersToCSV(i);
                    Assert.IsTrue(CompareOutputFiles("VerifyBE1.csv", "VerifyBE" + i + ".csv"));
                }
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("VSONLY");

                }
            }
        }

        [TestCategory("REDUNIVSWPHX"), TestMethod]
        public void VerifyParametersWorkFlowTestREDUNIVSWPHX()
        {
            try
            {
                CheckIfLowisExist();
                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("VSWPHX");

                }


                StartRTUEmu();
                CreateESPWells("VSWPHX");

                ZeroOutESPVerifyParametersFields(1);
                RunVerifyParametersOnESP(1);
                OutputESPVerifyParametersToCSV(1);
                for (int i = 2; (i < (NumberofWells + 2)); i++)
                {
                    ZeroOutESPVerifyParametersFields(i);
                    RunVerifyParametersOnESP(i);
                    OutputESPVerifyParametersToCSV(i);
                    Assert.IsTrue(CompareOutputFiles("VerifyBE1.csv", "VerifyBE" + i + ".csv"));
                }
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("VSWPHX");

                }
            }
        }

        [TestCategory("AEPOC2"), TestMethod]
        public void VerifyParametersWorkFlowTestAEPOC2()
        {
            try
            {
                CheckIfLowisExist();
                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("AEPOC2");

                }


                StartRTUEmu();
                CreateBeamWells("AEPOC2");

                ZeroOutBeamVerifyParametersFields(1);
                RunVerifyParametersOnBeam(1);
                OutputBeamVerifyParametersToCSV(1);
                for (int i = 2; (i < (NumberofWells + 2)); i++)
                {
                    ZeroOutBeamVerifyParametersFields(i);
                    RunVerifyParametersOnBeam(i);
                    OutputBeamVerifyParametersToCSV(i);
                    Assert.IsTrue(CompareOutputFiles("VerifyBE1.csv", "VerifyBE" + i + ".csv"));
                }
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("AEPOC2");

                }
            }
        }

        [TestCategory("AE6008"), TestMethod]
        public void VerifyParametersWorkFlowTestAE6008()
        {
            try
            {
                CheckIfLowisExist();
                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("AE6008");

                }


                StartRTUEmu();
                CreateInjectionWells("AE6008");
                ZeroOutBeamVerifyParametersFields(1);
                RunVerifyParametersOnInjection(1);
                OutputInjectionVerifyParametersToCSV(1);
                for (int i = 2; (i < (NumberofWells + 2)); i++)
                {
                    ZeroOutBeamVerifyParametersFields(i);
                    RunVerifyParametersOnInjection(i);
                    OutputInjectionVerifyParametersToCSV(i);
                    Assert.IsTrue(CompareOutputFiles("VerifyBE1.csv", "VerifyBE" + i + ".csv"));
                }
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("AE6008");

                }
            }
        }

        [TestCategory("SAMFS"), TestMethod]
        public void VerifyParametersWorkFlowTestSAMFS()
        {
            try
            {
                CheckIfLowisExist();
                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("SAMRP2");

                }


                StartRTUEmu();
                CreateBeamWells("SAMFS");

                ZeroOutBeamVerifyParametersFields(1);
                RunVerifyParametersOnBeam(1);
                OutputBeamVerifyParametersToCSV(1);
                for (int i = 2; (i < (NumberofWells + 2)); i++)
                {
                    ZeroOutBeamVerifyParametersFields(i);
                    RunVerifyParametersOnBeam(i);
                    OutputBeamVerifyParametersToCSV(i);
                    Assert.IsTrue(CompareOutputFiles("VerifyBE1.csv", "VerifyBE" + i + ".csv"));
                }
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("SAMRP2");

                }
            }
        }

        [TestCategory("SAMFW"), TestMethod]
        public void VerifyParametersWorkFlowTestSAMFW()
        {
            try
            {
                CheckIfLowisExist();
                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("SAMRP2");

                }


                StartRTUEmu();
                CreateBeamWells("SAMFW");

                ZeroOutBeamVerifyParametersFields(1);
                RunVerifyParametersOnBeam(1);
                OutputBeamVerifyParametersToCSV(1);
                for (int i = 2; (i < (NumberofWells + 2)); i++)
                {
                    ZeroOutBeamVerifyParametersFields(i);
                    RunVerifyParametersOnBeam(i);
                    OutputBeamVerifyParametersToCSV(i);
                    Assert.IsTrue(CompareOutputFiles("VerifyBE1.csv", "VerifyBE" + i + ".csv"));
                }
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("SAMRP2");

                }
            }
        }

        [TestCategory("SAMVS"), TestMethod]
        public void VerifyParametersWorkFlowTestSAMVS()
        {
            try
            {
                CheckIfLowisExist();
                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("SAMRP2");

                }


                StartRTUEmu();
                CreateBeamWells("SAMVS");

                ZeroOutBeamVerifyParametersFields(1);
                RunVerifyParametersOnBeam(1);
                OutputBeamVerifyParametersToCSV(1);
                for (int i = 2; (i < (NumberofWells + 2)); i++)
                {
                    ZeroOutBeamVerifyParametersFields(i);
                    RunVerifyParametersOnBeam(i);
                    OutputBeamVerifyParametersToCSV(i);
                    Assert.IsTrue(CompareOutputFiles("VerifyBE1.csv", "VerifyBE" + i + ".csv"));
                }

            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("SAMRP2");
                }
            }
        }

        [TestCategory("SAMVW"), TestMethod]
        public void VerifyParametersWorkFlowTestSAMVW()
        {
            try
            {
                CheckIfLowisExist();
                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("SAMRP2");

                }


                StartRTUEmu();
                CreateBeamWells("SAMVW");

                ZeroOutBeamVerifyParametersFields(1);
                RunVerifyParametersOnBeam(1);
                OutputBeamVerifyParametersToCSV(1);
                for (int i = 2; (i < (NumberofWells + 2)); i++)
                {
                    ZeroOutBeamVerifyParametersFields(i);
                    RunVerifyParametersOnBeam(i);
                    OutputBeamVerifyParametersToCSV(i);
                    Assert.IsTrue(CompareOutputFiles("VerifyBE1.csv", "VerifyBE" + i + ".csv"));
                }
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("SAMRP2");

                }
            }
        }

        [TestCategory("EPICFS"), TestMethod]
        public void VerifyParametersWorkFlowTestEPICFS()
        {
            try
            {
                CheckIfLowisExist();
                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("EPICRP");

                }


                StartRTUEmu();
                CreateBeamWells("EPICFS");

                ZeroOutBeamVerifyParametersFields(1);
                RunVerifyParametersOnBeam(1);
                OutputBeamVerifyParametersToCSV(1);
                for (int i = 2; (i < (NumberofWells + 2)); i++)
                {
                    ZeroOutBeamVerifyParametersFields(i);
                    RunVerifyParametersOnBeam(i);
                    OutputBeamVerifyParametersToCSV(i);
                    Assert.IsTrue(CompareOutputFiles("VerifyBE1.csv", "VerifyBE" + i + ".csv"));
                }

            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("EPICRP");

                }
            }
        }

        [TestCategory("EPICLM"), TestMethod]
        public void VerifyParametersWorkFlowTestEPICLM()
        {
            try
            {
                CheckIfLowisExist();
                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("EPICRP");

                }


                StartRTUEmu();
                CreateBeamWells("EPICLM");

                ZeroOutBeamVerifyParametersFields(1);
                RunVerifyParametersOnBeam(1);
                OutputBeamVerifyParametersToCSV(1);
                for (int i = 2; (i < (NumberofWells + 2)); i++)
                {
                    ZeroOutBeamVerifyParametersFields(i);
                    RunVerifyParametersOnBeam(i);
                    OutputBeamVerifyParametersToCSV(i);
                    Assert.IsTrue(CompareOutputFiles("VerifyBE1.csv", "VerifyBE" + i + ".csv"));
                }

            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("EPICRP");

                }
            }
        }

        [TestCategory("EPICVF"), TestMethod]
        public void VerifyParametersWorkFlowTestEPICVF()
        {
            try
            {
                CheckIfLowisExist();
                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("EPICRP");

                }



                StartRTUEmu();
                CreateBeamWells("EPICVF");


                ZeroOutBeamVerifyParametersFields(1);
                RunVerifyParametersOnBeam(1);
                OutputBeamVerifyParametersToCSV(1);
                for (int i = 2; (i < (NumberofWells + 2)); i++)
                {
                    ZeroOutBeamVerifyParametersFields(i);
                    RunVerifyParametersOnBeam(i);
                    OutputBeamVerifyParametersToCSV(i);
                    Assert.IsTrue(CompareOutputFiles("VerifyBE1.csv", "VerifyBE" + i + ".csv"));
                }

            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("EPICRP");

                }
            }
        }

    }

    [TestClass]
    public class TestDriverBase
    {
        public TestContext testContextInstance;
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        protected string m_strEmulatorExe = "RTUEmu.exe";

        protected string m_strLiftDir = GetLiftRunFolder();


        public int NumberofWells = Convert.ToInt32(ConfigurationManager.AppSettings.Get("NumberofWells"));
        public int comPort; // the com port number on which to start creating com ports is 10



        public void CheckIfLowisExist()
        {

            string strLiftRoot = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment", "LIFTROOT", null);

            if (String.IsNullOrEmpty(strLiftRoot))
            {
                Assert.Fail("LIFTROOT not found in Registry, is LOWIS installed on this system?");
            }
            else
            {
                TestContext.WriteLine("LOWIS root directory found at : {0}", strLiftRoot);
            }

        }

        public void StartRTUEmu()
        {

            try
            {
                string strFolder = ConfigurationManager.AppSettings.Get("RTUEmuFolder");
                string strCommand = Path.Combine(strFolder, m_strEmulatorExe);

                Process emulator = new Process();

                emulator.StartInfo.UseShellExecute = true;
                emulator.StartInfo.FileName = strCommand;

                TestContext.WriteLine(strCommand);

                Assert.IsTrue(emulator.Start());

                System.Threading.Thread.Sleep(10000);

                if (emulator.Responding)
                {
                    TestContext.WriteLine("Emulator {0} is running at process id {1}.", emulator.ProcessName, emulator.Id);
                }

            }
            catch (Exception e)
            {
                Assert.Fail("Failed to start RTUEmu process: {0}", e.ToString());
            }

        }



        public void StopRTUEmu()
        {

            Process[] arrEmulators = new Process[] { };

            try
            {
                string strName = System.IO.Path.GetFileNameWithoutExtension(m_strEmulatorExe);
                arrEmulators = Process.GetProcessesByName(strName);
            }
            catch (Exception e)
            {
                Assert.Fail("Failed to obtain emulator processes: {0}", e.ToString());
                return;
            }

            try
            {
                foreach (Process emulator in arrEmulators)
                {
                    TestContext.WriteLine("Closing emulator {0} at process id {1}...", emulator.ProcessName, emulator.Id);

                    Assert.IsTrue(emulator.CloseMainWindow());

                    if (!emulator.WaitForExit(30000))
                    {
                        Assert.Fail("   Emulator did not close window in 30 seconds.");

                        emulator.Kill();

                        if (!emulator.WaitForExit(30000))
                        {
                            Assert.Fail("   Emulator process did not kill in 30 seconds.");
                        }
                    }

                    Assert.IsTrue(emulator.HasExited);

                    if (emulator.HasExited)
                    {
                        TestContext.WriteLine("   Simulator {0} has exited.", m_strEmulatorExe);
                    }

                    emulator.Dispose();
                }
            }
            catch (Exception e)
            {
                Assert.Fail("Caught exception closing simulator: {0}", e.ToString());
            }

        }

        protected void Cleanup(string rtuType)
        {

            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            string strDeleteBulkWellScript = ConfigurationManager.AppSettings.Get("DeleteWellScript");

            Process dbw = new Process();
            switch (rtuType)
            {
                case "AEPOC2":
                case "SAMRP2":
                case "EPICRP":
                    try
                    {
                        dbw.StartInfo.UseShellExecute = true;
                        dbw.StartInfo.FileName = strMcsscrip;
                        dbw.StartInfo.Arguments = String.Format(strDeleteBulkWellScript + " deletebeamwells \"\" \"\" 1 -db beamdb");

                        TestContext.WriteLine(strMcsscrip + " " + dbw.StartInfo.Arguments);
                        Assert.IsTrue(dbw.Start());
                        dbw.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;

                case "FSONLY":
                case "FSWPHX":
                case "VSONLY":
                case "VSWPHX":
                    try
                    {
                        dbw.StartInfo.UseShellExecute = true;
                        dbw.StartInfo.FileName = strMcsscrip;
                        dbw.StartInfo.Arguments = String.Format(strDeleteBulkWellScript + " deletesubswells \"\" \"\" 1 -db subsdb");

                        TestContext.WriteLine(strMcsscrip + " " + dbw.StartInfo.Arguments);
                        Assert.IsTrue(dbw.Start());
                        dbw.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;
                case "AE6008":
                    try
                    {
                        dbw.StartInfo.UseShellExecute = true;
                        dbw.StartInfo.FileName = strMcsscrip;
                        dbw.StartInfo.Arguments = String.Format(strDeleteBulkWellScript + " deleteinjectionwells \"\" \"\" 1 -db injectdb");

                        TestContext.WriteLine(strMcsscrip + " " + dbw.StartInfo.Arguments);
                        Assert.IsTrue(dbw.Start());
                        dbw.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;
            }

            // let's also delete the scan task output files

            try
            {
                string[] csvList = Directory.GetFiles(m_strLiftDir, "*.csv");

                foreach (string f in csvList)
                {
                    if ((f.Contains("BE") && f.Contains(".csv")) || (f.Contains("SU") && f.Contains(".csv")))
                    {
                        TestContext.WriteLine("Deleting File " + f);
                        File.Delete(f);
                    }
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }

        }

        protected void CreateESPWells(string rtuType)
        {

            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            string strMakeBulkWellScript = ConfigurationManager.AppSettings.Get("CreateWellScript");

            string parmWellNamePrefix = String.Empty;
            string parmAltAddress = String.Empty;
            string parmRtuType = String.Empty;
            string parmRtuSubType = String.Empty;
            int parmFirstWellNumber = 0;
            int parmFirstAddress = 0;
            int parmLastAddress = 0;
            int parmChannel = 0;
            Process mbw = new Process();

            try
            {
                mbw.StartInfo.UseShellExecute = true;
                mbw.StartInfo.FileName = strMcsscrip;




                switch (rtuType)
                {
                    case "FSONLY":
                        parmWellNamePrefix = "FSO_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 200;
                        parmLastAddress = 201;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "REDUNI";
                        parmRtuSubType = "FSONLY";
                        break;

                    case "FSWPHX":
                        parmWellNamePrefix = "FSW_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 211;
                        parmLastAddress = 212;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "REDUNI";
                        parmRtuSubType = "FSWPHX";
                        break;

                    case "VSONLY":
                        parmWellNamePrefix = "VSO_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 221;
                        parmLastAddress = 222;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "REDUNI";
                        parmRtuSubType = "VSONLY";
                        break;

                    case "VSWPHX":
                        parmWellNamePrefix = "VSW_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 231;
                        parmLastAddress = 232;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "REDUNI";
                        parmRtuSubType = "VSWPHX";
                        break;
                }

                mbw.StartInfo.Arguments = String.Format("{0} makesurange \"\" \"{1}\" {2} {3} {4} {5} \"{6}\" \"{7}\" \"{8}\" -db subsdb", strMakeBulkWellScript, parmWellNamePrefix, parmFirstWellNumber, parmFirstAddress, parmLastAddress, parmChannel, parmAltAddress, parmRtuType, parmRtuSubType);

                TestContext.WriteLine(strMcsscrip + " " + mbw.StartInfo.Arguments);
                Assert.IsTrue(mbw.Start());
                mbw.WaitForExit();


            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
        }

        protected void CreateInjectionWells(string rtuType)
        {

            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            string strMakeBulkWellScript = ConfigurationManager.AppSettings.Get("CreateWellScript");

            string parmWellNamePrefix = String.Empty;
            string parmAltAddress = String.Empty;
            string parmRtuType = String.Empty;
            string parmRtuSubType = String.Empty;
            int parmFirstWellNumber = 0;
            int parmFirstAddress = 0;
            int parmLastAddress = 0;
            int parmChannel = 0;

            Process mbw = new Process();

            try
            {
                mbw.StartInfo.UseShellExecute = true;
                mbw.StartInfo.FileName = strMcsscrip;
                switch (rtuType)
                {
                    case "AE6008":
                        parmWellNamePrefix = "AE68_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 151;
                        parmLastAddress = 152;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "AE6008";
                        parmRtuSubType = "NONE";
                        break;

                }

                mbw.StartInfo.Arguments = String.Format("{0} makeijrange \"\" \"{1}\" {2} {3} {4} {5} \"{6}\" \"{7}\" \"{8}\" -db injectdb", strMakeBulkWellScript, parmWellNamePrefix, parmFirstWellNumber, parmFirstAddress, parmLastAddress, parmChannel, parmAltAddress, parmRtuType, parmRtuSubType);

                TestContext.WriteLine(strMcsscrip + " " + mbw.StartInfo.Arguments);
                Assert.IsTrue(mbw.Start());
                mbw.WaitForExit();

            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
        }


        protected void CreateBeamWells(string rtuType)
        {
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            string strMakeBulkWellScript = ConfigurationManager.AppSettings.Get("CreateWellScript");

            string parmWellNamePrefix = String.Empty;
            string parmAltAddress = String.Empty;
            string parmRtuType = String.Empty;
            string parmRtuSubType = String.Empty;
            int parmFirstWellNumber = 0;
            int parmFirstAddress = 0;
            int parmLastAddress = 0;
            int parmChannel = 0;
            comPort = 10;
            Process mbw = new Process();

            try
            {
                mbw.StartInfo.UseShellExecute = true;
                mbw.StartInfo.FileName = strMcsscrip;
                switch (rtuType)
                {
                    case "AEPOC2":
                        parmWellNamePrefix = "AE2_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 41;
                        parmLastAddress = 42;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "AEPOC2";
                        parmRtuSubType = "AEPOC2";
                        break;

                    case "SAMFS":
                        parmWellNamePrefix = "SAMFS_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 21;
                        parmLastAddress = 22;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "SAMRP2";
                        parmRtuSubType = "SAMFS";
                        break;

                    case "SAMFW":
                        parmWellNamePrefix = "SAMFW_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 21;
                        parmLastAddress = 22;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "SAMRP2";
                        parmRtuSubType = "SAMFW";
                        break;

                    case "SAMVS":
                        parmWellNamePrefix = "SAMVS_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 21;
                        parmLastAddress = 22;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "SAMRP2";
                        parmRtuSubType = "SAMVS";
                        break;

                    case "SAMVW":
                        parmWellNamePrefix = "SAMVW_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 21;
                        parmLastAddress = 22;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "SAMRP2";
                        parmRtuSubType = "SAMVW";
                        break;

                    case "EPICLM":
                        parmWellNamePrefix = "EPICLM_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 120;
                        parmLastAddress = 121;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "EPICRP";
                        parmRtuSubType = "EPICLM";
                        break;

                    case "EPICVF":
                        parmWellNamePrefix = "EPICVF_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 20;
                        parmLastAddress = 21;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "EPICRP";
                        parmRtuSubType = "EPICVF";
                        break;

                    case "EPICFS":
                        parmWellNamePrefix = "EPICFS_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 70;
                        parmLastAddress = 71;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "EPICRP";
                        parmRtuSubType = "EPICFS";
                        break;
                }

                mbw.StartInfo.Arguments = String.Format("{0} makeberange \"\" \"{1}\" {2} {3} {4} {5} \"{6}\" \"{7}\" \"{8}\" -db beamdb", strMakeBulkWellScript, parmWellNamePrefix, parmFirstWellNumber, parmFirstAddress, parmLastAddress, parmChannel, parmAltAddress, parmRtuType, parmRtuSubType);

                TestContext.WriteLine(strMcsscrip + " " + mbw.StartInfo.Arguments);
                Assert.IsTrue(mbw.Start());
                mbw.WaitForExit();
            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
        }

        protected void ZeroOutBeamVerifyParametersFields(int wellNumber)
        // using script to zero out all values in the host to prevent failing test in ARST due to UI automation writing values to host.
        {
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            string strZeroVerifyParametersFieldsScript = ConfigurationManager.AppSettings.Get("zeroVerifyParametersFields");

            Process ZeroVerifyParametersFields = new Process();

            try
            {
                ZeroVerifyParametersFields.StartInfo.UseShellExecute = true;
                ZeroVerifyParametersFields.StartInfo.FileName = strMcsscrip;
                ZeroVerifyParametersFields.StartInfo.Arguments = String.Format(strZeroVerifyParametersFieldsScript + " " + "zeroVerifyParametersFields" + " " + "\"\"" + " " + wellNumber + " " + "-db beamdb");
                TestContext.WriteLine(strMcsscrip + " " + ZeroVerifyParametersFields.StartInfo.Arguments);

                Assert.IsTrue(ZeroVerifyParametersFields.Start());
                ZeroVerifyParametersFields.WaitForExit();
            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
        }

        protected void ZeroOutESPVerifyParametersFields(int wellNumber)
        // using script to zero out all values in the host to prevent failing test in ARST due to UI automation writing values to host.
        {
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            string strZeroVerifyParametersFieldsScript = ConfigurationManager.AppSettings.Get("zeroVerifyParametersFields");

            Process ZeroVerifyParametersFields = new Process();

            try
            {
                ZeroVerifyParametersFields.StartInfo.UseShellExecute = true;
                ZeroVerifyParametersFields.StartInfo.FileName = strMcsscrip;
                ZeroVerifyParametersFields.StartInfo.Arguments = String.Format(strZeroVerifyParametersFieldsScript + " " + "zeroVerifyParametersFieldsESP" + " " + "\"\"" + " " + wellNumber + " " + "-db subsdb");
                TestContext.WriteLine(strMcsscrip + " " + ZeroVerifyParametersFields.StartInfo.Arguments);

                Assert.IsTrue(ZeroVerifyParametersFields.Start());
                ZeroVerifyParametersFields.WaitForExit();
            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
        }

        protected void OutputInjectionVerifyParametersToCSV(int wellNumber)
        {

            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            string strSetCsvOption = ConfigurationManager.AppSettings.Get("OutputVerifyParametersCSV");

            Process setcsv = new Process();

            try
            {
                setcsv.StartInfo.UseShellExecute = true;
                setcsv.StartInfo.FileName = strMcsscrip;
                setcsv.StartInfo.Arguments = String.Format(strSetCsvOption + " generateVerifyCSV \"\"" + " " + wellNumber + " \"IN\" " + "\"C:\\csLift\\liftnt\\run\\\\\" -db injectdb");
                TestContext.WriteLine(strMcsscrip + " " + setcsv.StartInfo.Arguments);

                Assert.IsTrue(setcsv.Start());
                setcsv.WaitForExit();
            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }

        }


        protected void OutputBeamVerifyParametersToCSV(int wellNumber)
        {

            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            string strSetCsvOption = ConfigurationManager.AppSettings.Get("OutputVerifyParametersCSV");

            Process setcsv = new Process();

            try
            {
                setcsv.StartInfo.UseShellExecute = true;
                setcsv.StartInfo.FileName = strMcsscrip;
                setcsv.StartInfo.Arguments = String.Format(strSetCsvOption + " generateVerifyCSV \"\"" + " " + wellNumber + " \"BE\" " + "\"C:\\csLift\\liftnt\\run\\\\\" -db beamdb");
                TestContext.WriteLine(strMcsscrip + " " + setcsv.StartInfo.Arguments);

                Assert.IsTrue(setcsv.Start());
                setcsv.WaitForExit();
            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }

        }

        protected void OutputESPVerifyParametersToCSV(int wellNumber)
        {

            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            string strSetCsvOption = ConfigurationManager.AppSettings.Get("OutputVerifyParametersCSV");

            Process setcsv = new Process();

            try
            {
                setcsv.StartInfo.UseShellExecute = true;
                setcsv.StartInfo.FileName = strMcsscrip;
                setcsv.StartInfo.Arguments = String.Format(strSetCsvOption + " generateVerifyCSV \"\"" + " " + wellNumber + " \"SU\" " + "\"C:\\csLift\\liftnt\\run\\\\\" -db subsdb");
                TestContext.WriteLine(strMcsscrip + " " + setcsv.StartInfo.Arguments);

                Assert.IsTrue(setcsv.Start());
                setcsv.WaitForExit();
            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }

        }

        protected void RunVerifyParametersOnInjection(int wellNumber)
        {

            string strVerifyParameters = Path.Combine(m_strLiftDir, "mtstbeam.exe");
            string strHostName = Dns.GetHostName();

            Process mtstbeam = new Process();
            try
            {
                mtstbeam.StartInfo.UseShellExecute = true;
                mtstbeam.StartInfo.FileName = strVerifyParameters;
                mtstbeam.StartInfo.Arguments = String.Format(strHostName + " injectdb\\\\cs_injp 20 " + wellNumber + " " + 1);

                TestContext.WriteLine(strVerifyParameters + " " + mtstbeam.StartInfo.Arguments);
                Assert.IsTrue(mtstbeam.Start());
                mtstbeam.WaitForExit();
            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }

        }


        protected void RunVerifyParametersOnBeam(int wellNumber)
        {

            string strVerifyParameters = Path.Combine(m_strLiftDir, "mtstbeam.exe");
            string strHostName = Dns.GetHostName();

            Process mtstbeam = new Process();
            try
            {
                mtstbeam.StartInfo.UseShellExecute = true;
                mtstbeam.StartInfo.FileName = strVerifyParameters;
                mtstbeam.StartInfo.Arguments = String.Format(strHostName + " beamdb\\\\cs_beamp 20 " + wellNumber + " " + 1);

                TestContext.WriteLine(strVerifyParameters + " " + mtstbeam.StartInfo.Arguments);
                Assert.IsTrue(mtstbeam.Start());
                mtstbeam.WaitForExit();
            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }

        }

        protected void RunVerifyParametersOnESP(int wellNumber)
        {

            string strVerifyParameters = Path.Combine(m_strLiftDir, "mtstsub.exe");
            string strHostName = Dns.GetHostName();

            Process mtstsub = new Process();
            try
            {
                mtstsub.StartInfo.UseShellExecute = true;
                mtstsub.StartInfo.FileName = strVerifyParameters;
                mtstsub.StartInfo.Arguments = String.Format(strHostName + " subsdb\\\\cs_subp 20 " + wellNumber + " " + 1);

                TestContext.WriteLine(strVerifyParameters + " " + mtstsub.StartInfo.Arguments);
                Assert.IsTrue(mtstsub.Start());
                mtstsub.WaitForExit();
            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }

        }

        protected bool CompareOutputFiles(String fileName1, String fileName2)
        {
            bool bEqual = true;

            try
            {
                if (!File.Exists(Path.Combine(m_strLiftDir, fileName1)))
                {
                    TestContext.WriteLine("Error: File not found: " + Path.Combine(m_strLiftDir, fileName1));
                    return false;
                }

                if (!File.Exists(Path.Combine(m_strLiftDir, fileName2)))
                {
                    TestContext.WriteLine("Error: File not found: " + Path.Combine(m_strLiftDir, fileName2));
                    return false;
                }

                string[] scanA = File.ReadAllLines(Path.Combine(m_strLiftDir, fileName1));
                string[] scanB = File.ReadAllLines(Path.Combine(m_strLiftDir, fileName2));

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("##### START OF FILE COMPARE #####");
                TestContext.WriteLine("Comparing " + fileName1 + " (length " + scanA.Length + ")" + " to " + fileName2 + " (length " + scanB.Length + ")");

                if (scanA.Length < 1)
                {
                    TestContext.WriteLine("Error: Output File #1 is empty.");
                    return false;
                }

                if (scanB.Length < 1)
                {
                    TestContext.WriteLine("Error: Output File #2 is empty.");
                    return false;
                }

                if (scanA.Length != scanB.Length)
                {
                    TestContext.WriteLine("Error: Length of both scan files do not match!");
                    return false;
                }

                for (int i = 0; i < scanA.Length - 1; i++)
                {
                    if (!scanA[i].Equals(scanB[i]))
                    {
                        TestContext.WriteLine("MISMATCH FOUND! " + "\"" + scanA[i] + "\"" + " does not match " + "\"" + scanB[i] + "\"");
                        bEqual = false;
                    }
                    else
                    {
                        TestContext.WriteLine(scanA[i] + " = " + scanB[i]);
                    }
                }

                TestContext.WriteLine("##### END OF FILE COMPARE #####\n\n");

            }
            catch (Exception e)
            {
                TestContext.WriteLine("ERROR: {0}", e.ToString());
                return false;
            }

            return bEqual;

        }

        protected static string GetLiftRunFolder()
        {


            // First check for the development environment variable
            string liftrun = Environment.GetEnvironmentVariable("LIFTRUN");

            if (!String.IsNullOrEmpty(liftrun))
            {
                return liftrun;
            }

            string strLiftRoot = ConfigurationManager.AppSettings.Get("LiftRoot");
            // TestContext.WriteLine("Provided Lift Root folder is " + strLiftRoot);

            string csLift = Directory.GetParent(strLiftRoot).Parent.Parent.FullName;

            liftrun = Path.Combine(csLift, "liftnt", "run");

            //  TestContext.WriteLine("Found Lift Run folder " + liftrun);

            return liftrun;
        }



    }
}

