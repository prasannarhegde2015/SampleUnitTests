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
using System.ServiceProcess;
using System.Xml.Linq;
using System.Xml;
using System.Text.RegularExpressions;

namespace IntegrationTests.CurrentStatusWF
{
    [TestClass]
    public class CurrentStatusWF : TestDriverBase
    {
        [TestCategory("SUGNMB"), TestMethod]
        public void CurrentStatusWorkFlowTestSUGNMB()
        {
            try
            {
                CheckIfLowisExist();
                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("SUGNMB");
                }
                SetupSUGNMB();
                StartRTUEmu();
                CreateESPWells("SUGNMB");
                SetCSVOutputDir("SUGNMB");
                TestCurrentStatus("Sugnmb", "SUGNMB");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("SUGNMB");
                }
            }
        }

        [TestCategory("VORTEX"), TestMethod]
        public void CurrentStatusWorkFlowTestVORTEX()
        {
            try
            {
                CheckIfLowisExist();

                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("VORTEX");
                }
                StartRTUEmu();
                CreateESPWells("VORTEX");
                SetCSVOutputDir("VORTEX");
                TestCurrentStatus("Vortex", "VORTEX");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("VORTEX");
                }
            }
        }

        [TestCategory("SPDSTR"), TestMethod]
        public void CurrentStatusWorkFlowTestSPDSTR()
        {
            try
            {
                CheckIfLowisExist();

                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("SPDSTR");
                }
                StartRTUEmu();
                CreateESPWells("SPDSTR");
                SetCSVOutputDir("SPDSTR");
                TestCurrentStatus("Spdstr", "SPDSTR");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("SPDSTR");
                }
            }
        }

        [TestCategory("ROBICN"), TestMethod]
        public void CurrentStatusWorkFlowTestROBICN()
        {
            try
            {
                CheckIfLowisExist();

                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("ROBICN");
                }
                StartRTUEmu();
                CreateESPWells("ROBICN");
                SetCSVOutputDir("ROBICN");
                TestCurrentStatus("Robicn", "ROBICN");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("ROBICN");
                }
            }
        }

        [TestCategory("KLT595"), TestMethod]
        public void CurrentStatusWorkFlowTestKLT595()
        {
            try
            {
                CheckIfLowisExist();

                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("KLT595");
                }

                StartRTUEmu();

                CreateESPWells("KLT595");
                SetCSVOutputDir("KLT595");
                TestCurrentStatus("Klt595", "KLT595");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("KLT595");
                }
            }
        }

        [TestCategory("KELTRN"), TestMethod]
        public void CurrentStatusWorkFlowTestKELTRN()
        {
            try
            {
                CheckIfLowisExist();

                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("KELTRN");
                }
                StartRTUEmu();

                CreateESPWells("KELTRN");
                SetCSVOutputDir("KELTRN");
                TestCurrentStatus("Keltrn", "KELTRN");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("KELTRN");
                }
            }
        }

        [TestCategory("GCSVFD"), TestMethod]
        public void CurrentStatusWorkFlowTestGCSVFD()
        {
            try
            {
                CheckIfLowisExist();

                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("GCSVFD");
                }
                StartRTUEmu();
                CreateESPWells("GCSVFD");
                SetCSVOutputDir("GCSVFD");
                TestCurrentStatus("Gcsvfd", "GCSVFD");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("GCSVFD");
                }
            }
        }

        [TestCategory("GCSV72"), TestMethod]
        public void CurrentStatusWorkFlowTestGCSV72()
        {
            try
            {
                CheckIfLowisExist();

                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("GCSV72");
                }
                StartRTUEmu();
                CreateESPWells("GCSV72");
                SetCSVOutputDir("GCSV72");
                TestCurrentStatus("Gcsesp", "GCSESP");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("GCSV72");
                }
            }
        }

        [TestCategory("GCSV84"), TestMethod]
        public void CurrentStatusWorkFlowTestGCSV84()
        {
            try
            {
                CheckIfLowisExist();

                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("GCSV84");
                }
                StartRTUEmu();
                CreateESPWells("GCSV84");
                SetCSVOutputDir("GCSV84");
                TestCurrentStatus("Gcsesp", "GCSESP");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("GCSV84");
                }
            }
        }

        [TestCategory("VTXICM"), TestMethod]
        public void CurrentStatusWorkFlowTestVTXICM()
        {
            try
            {
                CheckIfLowisExist();

                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("VTXICM");
                }
                StartRTUEmu();
                CreateESPWells("VTXICM");
                SetCSVOutputDir("VTXICM");
                TestCurrentStatus("Vtxicm", "VTXICM");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("VTXICM");
                }
            }
        }

        [TestCategory("YSKAWA"), TestMethod]
        public void CurrentStatusWorkFlowTestYSKAWA()
        {
            try
            {
                CheckIfLowisExist();

                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("YSKAWA");
                }
                StartRTUEmu();
                CreatePCPWells("YSKAWA");
                SetCSVOutputDir("YSKAWA");
                TestCurrentStatus("Yskawa", "YSKAWA");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("YSKAWA");
                }
            }
        }

        [TestCategory("REDUNIFSONLY"), TestMethod]
        public void CurrentStatusWorkFlowTestREDUNIFSONLY()
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
                SetCSVOutputDir("REDUNI");
                TestCurrentStatus("Reduni", "REDUNI");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("FSONLY");
                }
            }
        }

        [TestCategory("REDUNIFSWPHX"), TestMethod]
        public void CurrentStatusWorkFlowTestREDUNIFSWPHX()
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
                SetCSVOutputDir("REDUNI");
                TestCurrentStatus("Reduni", "REDUNI");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("FSWPHX");
                }
            }
        }

        [TestCategory("REDUNIVSONLY"), TestMethod]
        public void CurrentStatusWorkFlowTestREDUNIVSONLY()
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
                SetCSVOutputDir("REDUNI");
                TestCurrentStatus("Reduni", "REDUNI");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("VSONLY");
                }
            }
        }

        [TestCategory("REDUNIVSWPHX"), TestMethod]
        public void CurrentStatusWorkFlowTestREDUNIVSWPHX()
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
                SetCSVOutputDir("REDUNI");
                TestCurrentStatus("Reduni", "REDUNI");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("VSWPHX");
                }
            }
        }

        [TestCategory("UNILRP"), TestMethod]
        public void CurrentStatusWorkFlowTestUNILRP()
        {
            try
            {
                CheckIfLowisExist();

                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("UNILRP");
                }
                StartRTUEmu();
                CreateBeamWells("UNILRP");
                SetCSVOutputDir("UNIXRP");
                TestCurrentStatus("UnicoXRP", "UNIXRP");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("UNILRP");
                }
            }
        }

        [TestCategory("UNISRPV100"), TestMethod]
        public void CurrentStatusWorkFlowTestUNISRPV100()
        {
            try
            {
                CheckIfLowisExist();

                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("UNISRPV100");
                }
                StartRTUEmu();
                CreateBeamWells("UNISRPV100");
                SetCSVOutputDir("UNIXRP");
                TestCurrentStatus("UnicoXRP", "UNIXRP");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("UNISRPV100");
                }
            }
        }

        [TestCategory("UNISRPV110"), TestMethod]
        public void CurrentStatusWorkFlowTestUNISRPV110()
        {
            try
            {
                CheckIfLowisExist();

                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("UNISRPV110");
                }
                StartRTUEmu();
                CreateBeamWells("UNISRPV110");
                SetCSVOutputDir("UNIXRP");
                TestCurrentStatus("UnicoXRP", "UNIXRP");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("UNISRPV110");
                }
            }
        }

        [TestCategory("UNISRPV200"), TestMethod]
        public void CurrentStatusWorkFlowTestUNISRPV200()
        {
            try
            {
                CheckIfLowisExist();

                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("UNISRPV200");
                }
                StartRTUEmu();
                CreateBeamWells("UNISRPV200");
                SetCSVOutputDir("UNIXRP");
                TestCurrentStatus("UnicoXRP", "UNIXRP");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("UNISRPV200");
                }
            }
        }

        [TestCategory("AE6008"), TestMethod]
        public void CurrentStatusWorkFlowTestAE6008()
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
                SetCSVOutputDir("AE6008");
                TestCurrentStatus("AutoCom", "AE6008");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("AE6008");
                }
            }
        }

        [TestCategory("AEPOC2"), TestMethod]
        public void CurrentStatusWorkFlowTestAEPOC2()
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
                SetCSVOutputDir("AEPOC2");
                TestCurrentStatus("AutoCom", "AEPOC2");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("AEPOC2");
                }
            }
        }

        [TestCategory("SAMFS"), TestMethod]
        public void CurrentStatusWorkFlowTestSAMFS()
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
                SetCSVOutputDir("SAMRP2");
                TestCurrentStatus("LufkinSam", "SAMRP2");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("SAMRP2");
                }
            }
        }

        [TestCategory("SAMFW"), TestMethod]
        public void CurrentStatusWorkFlowTestSAMFW()
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
                SetCSVOutputDir("SAMRP2");
                TestCurrentStatus("LufkinSam", "SAMRP2");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("SAMRP2");
                }
            }
        }

        [TestCategory("SAMVS"), TestMethod]
        public void CurrentStatusWorkFlowTestSAMVS()
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
                SetCSVOutputDir("SAMRP2");
                TestCurrentStatus("LufkinSam", "SAMRP2");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("SAMRP2");
                }
            }
        }

        [TestCategory("SAMVW"), TestMethod]
        public void CurrentStatusWorkFlowTestSAMVW()
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
                SetCSVOutputDir("SAMRP2");
                TestCurrentStatus("LufkinSam", "SAMRP2");
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("SAMRP2");
                }
            }
        }

        [TestCategory("EPICFS"), TestMethod]
        public void CurrentStatusWorkFlowTestEPICFS()
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
                StopEmuUpdates(1);
                StopEmuUpdates(2);
                SetCSVOutputDir("EPICRP");
                TestCurrentStatus("EProdRpc", "EPICRP");
                StartEmuUpdates(1);
                StartEmuUpdates(2);
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("EPICRP");
                }
            }
        }

        [TestCategory("EPICVF"), TestMethod]
        public void CurrentStatusWorkFlowTestEPICVF()
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
                StopEmuUpdates(1);
                StopEmuUpdates(2);
                SetCSVOutputDir("EPICRP");
                TestCurrentStatus("EProdRpc", "EPICRP");
                StartEmuUpdates(1);
                StartEmuUpdates(2);
            }
            finally
            {
                StopRTUEmu();

                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("EPICRP");
                }
            }
        }

        [TestCategory("EPICLM"), TestMethod]
        public void CurrentStatusWorkFlowTestEPICLM()
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
                StopEmuUpdates(1);
                StopEmuUpdates(2);
                SetCSVOutputDir("EPICRP");
                TestCurrentStatus("EProdRpc", "EPICRP");
                StartEmuUpdates(1);
                StartEmuUpdates(2);
            }
            finally
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
        public string b_GetStats = (ConfigurationManager.AppSettings.Get("GetStats")); // True/False to get stats
        public List<string> MethodNames = new List<string>(); public string RtuType = "";
        public List<double> SampleRunTimes = new List<double>();
        public List<double> CreateWellsElapsedMilliSeconds = new List<double>();
        public Stopwatch Stopwatch = new Stopwatch();
        public void GatherStat(string MethodName, string rtuType, List<double> SampleRunTimes, List<double> CreateWellsElapsedMilliSeconds)
        {
            if (MethodName == "CheckIfLowisExist")
            {
                MethodNames.Add(MethodName);
            }
            else if (MethodName == "Cleanup")
            {
                MethodNames.Add(MethodName);
            }
            else if (MethodName == "InstallDevices")
            {
                MethodNames.Add(MethodName);
            }
            else if (MethodName == "StartRTUEmu")
            {
                MethodNames.Add(MethodName);
            }
            else if (MethodName == "CreateBeamWells")
            {
                if (!MethodNames.Contains("CreateBeamWells"))
                {
                    MethodNames.Add(MethodName);
                }
            }
            else if (MethodName == "CreateInjectionWells")
            {
                if (!MethodNames.Contains("CreateInjectionWells"))
                {
                    MethodNames.Add(MethodName);
                }
            }
            else if (MethodName == "CreatePCPWells")
            {
                if (!MethodNames.Contains("CreatePCPWells"))
                {
                    MethodNames.Add(MethodName);
                }
            }
            else if (MethodName == "SetCSVOutputDir")
            {
                MethodNames.Add(MethodName);
            }
            else if (MethodName == "TestCurrentStatus")
            {
                MethodNames.Add(MethodName);
            }
            else if (MethodName == "StopRTUEmu")
            {
                MethodNames.Add(MethodName);
            }
            if (MethodNames.Count >= 9) // all methods ran and times accumulated
            {
                GetStats(MethodNames, RtuType, SampleRunTimes, CreateWellsElapsedMilliSeconds);
            }
        }

        public void GetStats(List<string> MethodNames, string rtuType, List<double> SampleRunTimesElapsedMilliSeconds, List<double> CreateWellsElapsedMilliSeconds)
        {
            List<string> XMLString = new List<string>();
            XMLString.Add("CheckIfLowisExistTook"); XMLString.Add("CleanupTook"); XMLString.Add("Creating" + NumberofWells + "CygnetDeviceTook");
            XMLString.Add("StartingRtuEmuTook"); XMLString.Add("CreateWellNative"); XMLString.Add("SetCSVOutputTook"); XMLString.Add("TestCurrentStatusTook"); XMLString.Add("StopRtuEmuTook"); XMLString.Add("CleanupTook");
            string strMcBridges = Path.Combine(m_strLiftDir, "lowisadmincli.exe");
            List<string> ProcessesRunning = new List<string>();
            Process checkRunningProcesses = new Process();
            string Arugument0 = ("localhost" + " " + "ListRunningProcesses");
            try
            {
                checkRunningProcesses.StartInfo.UseShellExecute = false;
                checkRunningProcesses.StartInfo.RedirectStandardOutput = true;
                checkRunningProcesses.StartInfo.FileName = strMcBridges;
                checkRunningProcesses.StartInfo.Arguments = String.Format(Arugument0);
                TestContext.WriteLine(strMcBridges + " " + checkRunningProcesses.StartInfo.Arguments);

                Assert.IsTrue(checkRunningProcesses.Start());
                while (!checkRunningProcesses.StandardOutput.EndOfStream)
                {
                    string process = checkRunningProcesses.StandardOutput.ReadLine();

                    if (process.Contains("CygNet") && process.Contains("Bridge"))
                    {
                        ProcessesRunning.Add("\"" + process + "\"");
                    }
                }

                checkRunningProcesses.WaitForExit();
            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }

            Process GetStats = new Process();
            string strGetStats = Path.Combine(m_strLiftDir, "lowisadmincli.exe");
            string ProcessConcat = String.Join(",", ProcessesRunning);
            int i;
            int j;
            XmlDocument xml = new XmlDocument();
            XmlNodeList xnList;

            switch (rtuType)
            {

                case ("AEPOC2"):

                    string AEPOC2argument = ("localhost" + " " + "GetProcessStats" + " " + @"""" + "Beam Processor" + @"""" + "," + @"""" + "AEPOC2 (AUTOCOMM) RTU" + @"""" + "," + ProcessConcat + " " + @"""" + "CPU Utilization %" + @"""" + "," + "Handles" + "," + @"""" + "Private Size Bytes" + @"""" + "," + "Threads" + "," + @"""" + "Virtual Size Bytes" + @"""" + "," + @"""" + "Working Set Bytes" + @"""" + " " + "\"C:\\csLift\\liftnt\\run\\" + rtuType + ".xml\"" + " " + @"""" + "RawValues" + @"""");

                    try
                    {
                        GetStats.StartInfo.UseShellExecute = true;
                        GetStats.StartInfo.FileName = strGetStats;
                        GetStats.StartInfo.Arguments = String.Format(AEPOC2argument);
                        TestContext.WriteLine(strGetStats + " " + GetStats.StartInfo.Arguments);

                        Assert.IsTrue(GetStats.Start());
                        GetStats.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }

                    XDocument xdAEPOC2 = XDocument.Load("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");
                    i = 0;
                    foreach (string method in MethodNames)
                    {
                        xdAEPOC2.Element("Stats").Add(new XElement("Method", new XAttribute("Name", method),
                                                new XElement("Stat", new XAttribute("Name", "RunTime" + method),
                                                new XElement("Sample", new XAttribute(XMLString[i], SampleRunTimesElapsedMilliSeconds[i]) + "milliseconds"))));
                        i++;
                    }
                    foreach (XElement childElement in from x in xdAEPOC2.DescendantNodes().OfType<XElement>() where x.IsEmpty select x)
                    {
                        if (childElement.Name != "Sample")
                        {
                            childElement.Value = string.Empty;
                        }
                    }
                    xdAEPOC2.Save("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");

                    xml.Load("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");

                    xnList = xml.SelectNodes("//node()[@Name='RunTimeCreateBeamWells']");
                    foreach (XmlElement xn in xnList)
                    {
                        Console.WriteLine(xn.InnerText);
                        if (xn.InnerText.Contains("CreateWell"))
                        {
                            for (j = 0; j < CreateWellsElapsedMilliSeconds.Count; j++)
                            {
                                XmlElement newsample = xml.CreateElement("", "Sample", null);
                                XmlAttribute newAttribute = xml.CreateAttribute("", "CreateWellLLISPE", "");
                                newAttribute.Value = CreateWellsElapsedMilliSeconds[j].ToString() + "milliseconds";

                                newsample.Attributes.Append(newAttribute);

                                xn.AppendChild(newsample);
                            }
                        }
                    }
                    xml.Save("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");
                    break;

                case ("AE6008"):

                    string AE6008argument = ("localhost" + " " + "GetProcessStats" + " " + @"""" + "Injection Processor" + @"""" + "," + @"""" + "AE6008 Scan Task" + @"""" + "," + ProcessConcat + " " + @"""" + "CPU Utilization %" + @"""" + "," + "Handles" + "," + @"""" + "Private Size Bytes" + @"""" + "," + "Threads" + "," + @"""" + "Virtual Size Bytes" + @"""" + "," + @"""" + "Working Set Bytes" + @"""" + " " + "\"C:\\csLift\\liftnt\\run\\" + rtuType + ".xml\"" + " " + @"""" + "RawValues" + @"""");

                    try
                    {
                        GetStats.StartInfo.UseShellExecute = true;
                        GetStats.StartInfo.FileName = strGetStats;
                        GetStats.StartInfo.Arguments = String.Format(AE6008argument);
                        TestContext.WriteLine(strGetStats + " " + GetStats.StartInfo.Arguments);

                        Assert.IsTrue(GetStats.Start());
                        GetStats.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }

                    XDocument xdAE6008 = XDocument.Load("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");
                    i = 0;
                    foreach (string method in MethodNames)
                    {
                        xdAE6008.Element("Stats").Add(new XElement("Method", new XAttribute("Name", method),
                                                new XElement("Stat", new XAttribute("Name", "RunTime" + method),
                                                new XElement("Sample", new XAttribute(XMLString[i], SampleRunTimesElapsedMilliSeconds[i]) + "milliseconds"))));
                        i++;
                    }
                    foreach (XElement childElement in from x in xdAE6008.DescendantNodes().OfType<XElement>() where x.IsEmpty select x)
                    {
                        if (childElement.Name != "Sample")
                        {
                            childElement.Value = string.Empty;
                        }
                    }
                    xdAE6008.Save("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");

                    xml.Load("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");

                    xnList = xml.SelectNodes("//node()[@Name='RunTimeCreateInjectionWells']");
                    foreach (XmlElement xn in xnList)
                    {
                        Console.WriteLine(xn.InnerText);
                        if (xn.InnerText.Contains("CreateWell"))
                        {
                            for (j = 0; j < CreateWellsElapsedMilliSeconds.Count; j++)
                            {
                                XmlElement newsample = xml.CreateElement("", "Sample", null);
                                XmlAttribute newAttribute = xml.CreateAttribute("", "CreateWellLLISPE", "");
                                newAttribute.Value = CreateWellsElapsedMilliSeconds[j].ToString() + "milliseconds";

                                newsample.Attributes.Append(newAttribute);

                                xn.AppendChild(newsample);
                            }
                        }
                    }
                    xml.Save("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");
                    break;


                case "SAMFS":
                    string SAMFSargument = ("localhost" + " " + "GetProcessStats" + " " + @"""" + "Beam Processor" + @"""" + "," + @"""" + "Lufkin SAM RPC2" + @"""" + "," + ProcessConcat + " " + @"""" + "CPU Utilization %" + @"""" + "," + "Handles" + "," + @"""" + "Private Size Bytes" + @"""" + "," + "Threads" + "," + @"""" + "Virtual Size Bytes" + @"""" + "," + @"""" + "Working Set Bytes" + @"""" + " " + "\"C:\\csLift\\liftnt\\run\\" + rtuType + ".xml\"" + " " + @"""" + "RawValues" + @"""");

                    try
                    {
                        GetStats.StartInfo.UseShellExecute = true;
                        GetStats.StartInfo.FileName = strGetStats;
                        GetStats.StartInfo.Arguments = String.Format(SAMFSargument);
                        TestContext.WriteLine(strGetStats + " " + GetStats.StartInfo.Arguments);

                        Assert.IsTrue(GetStats.Start());
                        GetStats.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }

                    XDocument xdSAMFS = XDocument.Load("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");
                    i = 0;
                    foreach (string method in MethodNames)
                    {
                        xdSAMFS.Element("Stats").Add(new XElement("Method", new XAttribute("Name", method),
                                                new XElement("Stat", new XAttribute("Name", "RunTime" + method),
                                                new XElement("Sample", new XAttribute(XMLString[i], SampleRunTimesElapsedMilliSeconds[i]) + "milliseconds"))));
                        i++;
                    }
                    foreach (XElement childElement in from x in xdSAMFS.DescendantNodes().OfType<XElement>() where x.IsEmpty select x)
                    {
                        if (childElement.Name != "Sample")
                        {
                            childElement.Value = string.Empty;
                        }
                    }
                    xdSAMFS.Save("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");

                    xml.Load("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");

                    xnList = xml.SelectNodes("//node()[@Name='RunTimeCreateBeamWells']");
                    foreach (XmlElement xn in xnList)
                    {
                        Console.WriteLine(xn.InnerText);
                        if (xn.InnerText.Contains("CreateWell"))
                        {
                            for (j = 0; j < CreateWellsElapsedMilliSeconds.Count; j++)
                            {
                                XmlElement newsample = xml.CreateElement("", "Sample", null);
                                XmlAttribute newAttribute = xml.CreateAttribute("", "CreateWellLLISPE", "");
                                newAttribute.Value = CreateWellsElapsedMilliSeconds[j].ToString() + "milliseconds";

                                newsample.Attributes.Append(newAttribute);

                                xn.AppendChild(newsample);
                            }
                        }
                    }
                    xml.Save("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");
                    break;

                case "SAMFW":
                    string SAMFWargument = ("localhost" + " " + "GetProcessStats" + " " + @"""" + "Beam Processor" + @"""" + "," + @"""" + "Lufkin SAM RPC2" + @"""" + "," + ProcessConcat + " " + @"""" + "CPU Utilization %" + @"""" + "," + "Handles" + "," + @"""" + "Private Size Bytes" + @"""" + "," + "Threads" + "," + @"""" + "Virtual Size Bytes" + @"""" + "," + @"""" + "Working Set Bytes" + @"""" + " " + "\"C:\\csLift\\liftnt\\run\\" + rtuType + ".xml\"" + " " + @"""" + "RawValues" + @"""");

                    try
                    {
                        GetStats.StartInfo.UseShellExecute = true;
                        GetStats.StartInfo.FileName = strGetStats;
                        GetStats.StartInfo.Arguments = String.Format(SAMFWargument);
                        TestContext.WriteLine(strGetStats + " " + GetStats.StartInfo.Arguments);

                        Assert.IsTrue(GetStats.Start());
                        GetStats.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }

                    XDocument xdSAMFW = XDocument.Load("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");
                    i = 0;
                    foreach (string method in MethodNames)
                    {
                        xdSAMFW.Element("Stats").Add(new XElement("Method", new XAttribute("Name", method),
                                                new XElement("Stat", new XAttribute("Name", "RunTime" + method),
                                                new XElement("Sample", new XAttribute(XMLString[i], SampleRunTimesElapsedMilliSeconds[i]) + "milliseconds"))));
                        i++;
                    }
                    foreach (XElement childElement in from x in xdSAMFW.DescendantNodes().OfType<XElement>() where x.IsEmpty select x)
                    {
                        if (childElement.Name != "Sample")
                        {
                            childElement.Value = string.Empty;
                        }
                    }
                    xdSAMFW.Save("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");

                    xml.Load("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");

                    xnList = xml.SelectNodes("//node()[@Name='RunTimeCreateBeamWells']");
                    foreach (XmlElement xn in xnList)
                    {
                        Console.WriteLine(xn.InnerText);
                        if (xn.InnerText.Contains("CreateWell"))
                        {
                            for (j = 0; j < CreateWellsElapsedMilliSeconds.Count; j++)
                            {
                                XmlElement newsample = xml.CreateElement("", "Sample", null);
                                XmlAttribute newAttribute = xml.CreateAttribute("", "CreateWellLLISPE", "");
                                newAttribute.Value = CreateWellsElapsedMilliSeconds[j].ToString() + "milliseconds";

                                newsample.Attributes.Append(newAttribute);

                                xn.AppendChild(newsample);
                            }
                        }
                    }
                    xml.Save("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");
                    break;

                case "SAMVS":
                    string SAMVSargument = ("localhost" + " " + "GetProcessStats" + " " + @"""" + "Beam Processor" + @"""" + "," + @"""" + "Lufkin SAM RPC2" + @"""" + "," + ProcessConcat + " " + @"""" + "CPU Utilization %" + @"""" + "," + "Handles" + "," + @"""" + "Private Size Bytes" + @"""" + "," + "Threads" + "," + @"""" + "Virtual Size Bytes" + @"""" + "," + @"""" + "Working Set Bytes" + @"""" + " " + "\"C:\\csLift\\liftnt\\run\\" + rtuType + ".xml\"" + " " + @"""" + "RawValues" + @"""");

                    try
                    {
                        GetStats.StartInfo.UseShellExecute = true;
                        GetStats.StartInfo.FileName = strGetStats;
                        GetStats.StartInfo.Arguments = String.Format(SAMVSargument);
                        TestContext.WriteLine(strGetStats + " " + GetStats.StartInfo.Arguments);

                        Assert.IsTrue(GetStats.Start());
                        GetStats.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }

                    XDocument xdSAMVS = XDocument.Load("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");
                    i = 0;
                    foreach (string method in MethodNames)
                    {
                        xdSAMVS.Element("Stats").Add(new XElement("Method", new XAttribute("Name", method),
                                                new XElement("Stat", new XAttribute("Name", "RunTime" + method),
                                                new XElement("Sample", new XAttribute(XMLString[i], SampleRunTimesElapsedMilliSeconds[i]) + "milliseconds"))));
                        i++;
                    }
                    foreach (XElement childElement in from x in xdSAMVS.DescendantNodes().OfType<XElement>() where x.IsEmpty select x)
                    {
                        if (childElement.Name != "Sample")
                        {
                            childElement.Value = string.Empty;
                        }
                    }
                    xdSAMVS.Save("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");

                    xml.Load("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");

                    xnList = xml.SelectNodes("//node()[@Name='RunTimeCreateBeamWells']");
                    foreach (XmlElement xn in xnList)
                    {
                        Console.WriteLine(xn.InnerText);
                        if (xn.InnerText.Contains("CreateWell"))
                        {
                            for (j = 0; j < CreateWellsElapsedMilliSeconds.Count; j++)
                            {
                                XmlElement newsample = xml.CreateElement("", "Sample", null);
                                XmlAttribute newAttribute = xml.CreateAttribute("", "CreateWellLLISPE", "");
                                newAttribute.Value = CreateWellsElapsedMilliSeconds[j].ToString() + "milliseconds";

                                newsample.Attributes.Append(newAttribute);

                                xn.AppendChild(newsample);
                            }
                        }
                    }
                    xml.Save("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");
                    break;

                case "SAMVW":
                    string SAMVWargument = ("localhost" + " " + "GetProcessStats" + " " + @"""" + "Beam Processor" + @"""" + "," + @"""" + "Lufkin SAM RPC2" + @"""" + "," + ProcessConcat + " " + @"""" + "CPU Utilization %" + @"""" + "," + "Handles" + "," + @"""" + "Private Size Bytes" + @"""" + "," + "Threads" + "," + @"""" + "Virtual Size Bytes" + @"""" + "," + @"""" + "Working Set Bytes" + @"""" + " " + "\"C:\\csLift\\liftnt\\run\\" + rtuType + ".xml\"" + " " + @"""" + "RawValues" + @"""");

                    try
                    {
                        GetStats.StartInfo.UseShellExecute = true;
                        GetStats.StartInfo.FileName = strGetStats;
                        GetStats.StartInfo.Arguments = String.Format(SAMVWargument);
                        TestContext.WriteLine(strGetStats + " " + GetStats.StartInfo.Arguments);

                        Assert.IsTrue(GetStats.Start());
                        GetStats.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }


                    XDocument xdSAMVW = XDocument.Load("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");
                    i = 0;
                    foreach (string method in MethodNames)
                    {
                        xdSAMVW.Element("Stats").Add(new XElement("Method", new XAttribute("Name", method),
                                                new XElement("Stat", new XAttribute("Name", "RunTime" + method),
                                                new XElement("Sample", new XAttribute(XMLString[i], SampleRunTimesElapsedMilliSeconds[i]) + "milliseconds"))));
                        i++;
                    }
                    foreach (XElement childElement in from x in xdSAMVW.DescendantNodes().OfType<XElement>() where x.IsEmpty select x)
                    {
                        if (childElement.Name != "Sample")
                        {
                            childElement.Value = string.Empty;
                        }
                    }
                    xdSAMVW.Save("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");

                    xml.Load("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");

                    xnList = xml.SelectNodes("//node()[@Name='RunTimeCreateBeamWells']");
                    foreach (XmlElement xn in xnList)
                    {
                        Console.WriteLine(xn.InnerText);
                        if (xn.InnerText.Contains("CreateWell"))
                        {
                            for (j = 0; j < CreateWellsElapsedMilliSeconds.Count; j++)
                            {
                                XmlElement newsample = xml.CreateElement("", "Sample", null);
                                XmlAttribute newAttribute = xml.CreateAttribute("", "CreateWellLLISPE", "");
                                newAttribute.Value = CreateWellsElapsedMilliSeconds[j].ToString() + "milliseconds";

                                newsample.Attributes.Append(newAttribute);

                                xn.AppendChild(newsample);
                            }
                        }
                    }
                    xml.Save("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");
                    break;

                case "EPICFS":
                    string EPICFSargument = ("localhost" + " " + "GetProcessStats" + " " + @"""" + "Beam Processor" + @"""" + "," + @"""" + "EPICRP (EPROD) RTU" + @"""" + "," + ProcessConcat + " " + @"""" + "CPU Utilization %" + @"""" + "," + "Handles" + "," + @"""" + "Private Size Bytes" + @"""" + "," + "Threads" + "," + @"""" + "Virtual Size Bytes" + @"""" + "," + @"""" + "Working Set Bytes" + @"""" + " " + "\"C:\\csLift\\liftnt\\run\\" + rtuType + ".xml\"" + " " + @"""" + "RawValues" + @"""");
                    try
                    {
                        GetStats.StartInfo.UseShellExecute = true;
                        GetStats.StartInfo.FileName = strGetStats;
                        GetStats.StartInfo.Arguments = String.Format(EPICFSargument);
                        TestContext.WriteLine(strGetStats + " " + GetStats.StartInfo.Arguments);

                        Assert.IsTrue(GetStats.Start());
                        GetStats.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }

                    XDocument xdEPICFS = XDocument.Load("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");
                    i = 0;
                    foreach (string method in MethodNames)
                    {
                        xdEPICFS.Element("Stats").Add(new XElement("Method", new XAttribute("Name", method),
                                                new XElement("Stat", new XAttribute("Name", "RunTime" + method),
                                                new XElement("Sample", new XAttribute(XMLString[i], SampleRunTimesElapsedMilliSeconds[i]) + "milliseconds"))));
                        i++;
                    }
                    foreach (XElement childElement in from x in xdEPICFS.DescendantNodes().OfType<XElement>() where x.IsEmpty select x)
                    {
                        if (childElement.Name != "Sample")
                        {
                            childElement.Value = string.Empty;
                        }
                    }
                    xdEPICFS.Save("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");

                    xml.Load("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");

                    xnList = xml.SelectNodes("//node()[@Name='RunTimeCreateBeamWells']");
                    foreach (XmlElement xn in xnList)
                    {
                        Console.WriteLine(xn.InnerText);
                        if (xn.InnerText.Contains("CreateWell"))
                        {
                            for (j = 0; j < CreateWellsElapsedMilliSeconds.Count; j++)
                            {
                                XmlElement newsample = xml.CreateElement("", "Sample", null);
                                XmlAttribute newAttribute = xml.CreateAttribute("", "CreateWellLLISPE", "");
                                newAttribute.Value = CreateWellsElapsedMilliSeconds[j].ToString() + "milliseconds";

                                newsample.Attributes.Append(newAttribute);

                                xn.AppendChild(newsample);
                            }
                        }
                    }
                    xml.Save("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");
                    break;

                case "EPICVF":
                    string EPICVFargument = ("localhost" + " " + "GetProcessStats" + " " + @"""" + "Beam Processor" + @"""" + "," + @"""" + "EPICRP (EPROD) RTU" + @"""" + "," + ProcessConcat + " " + @"""" + "CPU Utilization %" + @"""" + "," + "Handles" + "," + @"""" + "Private Size Bytes" + @"""" + "," + "Threads" + "," + @"""" + "Virtual Size Bytes" + @"""" + "," + @"""" + "Working Set Bytes" + @"""" + " " + "\"C:\\csLift\\liftnt\\run\\" + rtuType + ".xml\"" + " " + @"""" + "RawValues" + @"""");

                    try
                    {
                        GetStats.StartInfo.UseShellExecute = true;
                        GetStats.StartInfo.FileName = strGetStats;
                        GetStats.StartInfo.Arguments = String.Format(EPICVFargument);
                        TestContext.WriteLine(strGetStats + " " + GetStats.StartInfo.Arguments);

                        Assert.IsTrue(GetStats.Start());
                        GetStats.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }

                    XDocument xdEPICVF = XDocument.Load("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");
                    i = 0;
                    foreach (string method in MethodNames)
                    {


                        xdEPICVF.Element("Stats").Add(new XElement("Method", new XAttribute("Name", method),
                                                new XElement("Stat", new XAttribute("Name", "RunTime" + method),
                                                new XElement("Sample", new XAttribute(XMLString[i], SampleRunTimesElapsedMilliSeconds[i]) + "milliseconds"))));
                        i++;
                    }
                    foreach (XElement childElement in from x in xdEPICVF.DescendantNodes().OfType<XElement>() where x.IsEmpty select x)
                    {
                        if (childElement.Name != "Sample")
                        {
                            childElement.Value = string.Empty;
                        }
                    }
                    xdEPICVF.Save("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");

                    xml.Load("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");

                    xnList = xml.SelectNodes("//node()[@Name='RunTimeCreateBeamWells']");
                    foreach (XmlElement xn in xnList)
                    {
                        Console.WriteLine(xn.InnerText);
                        if (xn.InnerText.Contains("CreateWell"))
                        {
                            for (j = 0; j < CreateWellsElapsedMilliSeconds.Count; j++)
                            {
                                XmlElement newsample = xml.CreateElement("", "Sample", null);
                                XmlAttribute newAttribute = xml.CreateAttribute("", "CreateWellLLISPE", "");
                                newAttribute.Value = CreateWellsElapsedMilliSeconds[j].ToString() + "milliseconds";

                                newsample.Attributes.Append(newAttribute);

                                xn.AppendChild(newsample);
                            }
                        }
                    }
                    xml.Save("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");
                    break;

                case "EPICLM":
                    string EPICLMargument = ("localhost" + " " + "GetProcessStats" + " " + @"""" + "Beam Processor" + @"""" + "," + @"""" + "EPICRP (EPROD) RTU" + @"""" + "," + ProcessConcat + " " + @"""" + "CPU Utilization %" + @"""" + "," + "Handles" + "," + @"""" + "Private Size Bytes" + @"""" + "," + "Threads" + "," + @"""" + "Virtual Size Bytes" + @"""" + "," + @"""" + "Working Set Bytes" + @"""" + " " + "\"C:\\csLift\\liftnt\\run\\" + rtuType + ".xml\"" + " " + @"""" + "RawValues" + @"""");
                    try
                    {
                        GetStats.StartInfo.UseShellExecute = true;
                        GetStats.StartInfo.FileName = strGetStats;
                        GetStats.StartInfo.Arguments = String.Format(EPICLMargument);
                        TestContext.WriteLine(strGetStats + " " + GetStats.StartInfo.Arguments);

                        Assert.IsTrue(GetStats.Start());
                        GetStats.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }

                    XDocument xdEPICLM = XDocument.Load("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");
                    i = 0;
                    foreach (string method in MethodNames)
                    {
                        xdEPICLM.Element("Stats").Add(new XElement("Method", new XAttribute("Name", method),
                                                new XElement("Stat", new XAttribute("Name", "RunTime" + method),
                                                new XElement("Sample", new XAttribute(XMLString[i], SampleRunTimesElapsedMilliSeconds[i]) + "milliseconds"))));
                        i++;
                    }
                    foreach (XElement childElement in from x in xdEPICLM.DescendantNodes().OfType<XElement>() where x.IsEmpty select x)
                    {
                        if (childElement.Name != "Sample")
                        {
                            childElement.Value = string.Empty;
                        }
                    }
                    xdEPICLM.Save("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");

                    xml.Load("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");

                    xnList = xml.SelectNodes("//node()[@Name='RunTimeCreateBeamWells']");
                    foreach (XmlElement xn in xnList)
                    {
                        Console.WriteLine(xn.InnerText);
                        if (xn.InnerText.Contains("CreateWell"))
                        {
                            for (j = 0; j < CreateWellsElapsedMilliSeconds.Count; j++)
                            {
                                XmlElement newsample = xml.CreateElement("", "Sample", null);
                                XmlAttribute newAttribute = xml.CreateAttribute("", "CreateWellLLISPE", "");
                                newAttribute.Value = CreateWellsElapsedMilliSeconds[j].ToString() + "milliseconds";

                                newsample.Attributes.Append(newAttribute);

                                xn.AppendChild(newsample);
                            }
                        }
                    }
                    xml.Save("C:\\csLift\\liftnt\\run\\" + rtuType + ".xml");
                    break;
            }
        }

        protected void CheckIfLowisExist()
        {
            Stopwatch.Start();
            string strLiftRoot = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Session Manager\\Environment", "LIFTROOT", null);

            if (String.IsNullOrEmpty(strLiftRoot))
            {
                Assert.Fail("LIFTROOT not found in Registry, is LOWIS installed on this system?");
            }
            else
            {
                TestContext.WriteLine("LOWIS root directory found at : {0}", strLiftRoot);
            }
            Stopwatch.Stop();
            SampleRunTimes.Add(Stopwatch.ElapsedMilliseconds);
            Stopwatch.Reset();
            if (b_GetStats == "true")
            {
                GatherStat("CheckIfLowisExist", "", SampleRunTimes, null);
            }
        }

        protected void TestCurrentStatus(string eieType, string rtuType)
        {
            Stopwatch.Start();
            bool testPassed = true;
            for (int i = 1; (i < NumberofWells + 1); i++)
            {
                switch (rtuType)
                {
                    case "UNIXRP":
                    case "AEPOC2":
                    case "SAMRP2":
                    case "EPICRP":
                        DeleteOutputFiles("BE1.csv", "BE" + (i + 1) + ".csv");
                        break;
                    case "AE6008":
                        DeleteOutputFiles("IJ1.csv", "IJ" + (i + 1) + ".csv");
                        break;
                    case "KELTRN":
                    case "KLT595":
                    case "GCSVFD":
                    case "GCSESP":
                    case "VTXICM":
                    case "REDUNI":
                    case "ROBICN":
                    case "SPDSTR":
                    case "VORTEX":
                    case "SUGNMB":
                        DeleteOutputFiles("SU1.csv", "SU" + (i + 1) + ".csv");
                        break;
                    case "YSKAWA":
                        DeleteOutputFiles("GL1.csv", "GL" + (i + 1) + ".csv");
                        break;
                }
                GetCurrentStatus(1, rtuType);
                TestContext.WriteLine("Sent Get Current Status Command for well 1");
                GetCurrentStatus(i + 1, rtuType);
                TestContext.WriteLine("Sent Get Current Status Command for well " + (i + 1));

                switch (rtuType)
                {
                    case "UNIXRP":
                    case "AEPOC2":
                    case "SAMRP2":
                    case "EPICRP":
                        if (!CompareOutputFiles("BE1.csv", "BE" + (i + 1) + ".csv"))
                        {
                            testPassed = false;
                            Assert.IsTrue(testPassed);
                        }
                        break;
                    case "AE6008":
                        if (!CompareOutputFiles("IJ1.csv", "IJ" + (i + 1) + ".csv"))
                        {
                            testPassed = false;
                            Assert.IsTrue(testPassed);
                        }
                        break;
                    case "KELTRN":
                    case "KLT595":
                    case "GCSVFD":
                    case "GCSESP":
                    case "VTXICM":
                    case "REDUNI":
                    case "ROBICN":
                    case "SPDSTR":
                    case "VORTEX":
                    case "SUGNMB":
                        if (!CompareOutputFiles("SU1.csv", "SU" + (i + 1) + ".csv"))
                        {
                            testPassed = false;
                            Assert.IsTrue(testPassed);
                        }
                        break;
                    case "YSKAWA":
                        if (!CompareOutputFiles("GL1.csv", "GL" + (i + 1) + ".csv"))
                        {
                            testPassed = false;
                            Assert.IsTrue(testPassed);
                        }
                        break;
                }
            }

            Stopwatch.Stop();
            SampleRunTimes.Add(Stopwatch.ElapsedMilliseconds);
            Stopwatch.Reset();
            if (b_GetStats == "true")
            {
                GatherStat("TestCurrentStatus", "", SampleRunTimes, CreateWellsElapsedMilliSeconds);
            }
        }

        protected void DeleteOutputFiles(string fileName1, string fileName2)
        {
            try
            {
                string filePath1 = Path.Combine(m_strLiftDir, fileName1);

                if (File.Exists(filePath1))
                {
                    TestContext.WriteLine("Deleting existing file: " + filePath1);

                    File.Delete(filePath1);
                }

                string filePath2 = Path.Combine(m_strLiftDir, fileName2);

                if (File.Exists(filePath2))
                {
                    TestContext.WriteLine("Deleting existing file: " + filePath2);

                    File.Delete(filePath2);
                }
            }
            catch (Exception e)
            {
                TestContext.WriteLine("Caught exception while deleting output files: " + e.ToString());
            }
        }

        protected static string GetLiftRunFolder()
        {


            // First check for the development environment variable
            string liftrun = Environment.GetEnvironmentVariable("LIFTRUN");

            if (!String.IsNullOrEmpty(liftrun))
            {
                return liftrun;
            }

            string strLiftRoot = ConfigurationManager.AppSettings.Get("LiftRoot"); //(@"C:\csLift\lift\Default\root\");
            //TestContext.WriteLine("Provided Lift Root folder is " + strLiftRoot);

            string csLift = Directory.GetParent(strLiftRoot).Parent.Parent.FullName;

            liftrun = Path.Combine(csLift, "liftnt", "run");

            // TestContext.WriteLine("Found Lift Run folder " + liftrun);

            return liftrun;
        }

        protected void StartRTUEmu()
        {
            Stopwatch.Start();
            try
            {
                string strFolder = ConfigurationManager.AppSettings.Get("RTUEmuFolder");
                string strCommand = strFolder + m_strEmulatorExe;

                Process emulator = new Process();

                emulator.StartInfo.UseShellExecute = true;
                emulator.StartInfo.FileName = strCommand;

                TestContext.WriteLine(strCommand);

                Assert.IsTrue(emulator.Start());

                System.Threading.Thread.Sleep(9000);

                if (emulator.Responding)
                {
                    TestContext.WriteLine("Emulator {0} is running at process id {1}.", emulator.ProcessName, emulator.Id);
                }

            }
            catch (Exception e)
            {
                Assert.Fail("Failed to start RTUEmu process: {0}", e.ToString());
            }
            Stopwatch.Stop();
            SampleRunTimes.Add(Stopwatch.ElapsedMilliseconds);
            Stopwatch.Reset();
            if (b_GetStats == "true")
            {
                GatherStat("StartRTUEmu", "", SampleRunTimes, null);
            }
        }

        protected void StopRTUEmu()
        {
            Stopwatch.Start();
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
            Stopwatch.Stop();
            SampleRunTimes.Add(Stopwatch.ElapsedMilliseconds);
            Stopwatch.Reset();
            if (b_GetStats == "true")
            {
                GatherStat("StopRTUEmu", "", SampleRunTimes, CreateWellsElapsedMilliSeconds);
            }
        }

        protected void Cleanup(string rtuType)
        {
            Stopwatch.Start();
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            string strDeleteBulkWellScript = ConfigurationManager.AppSettings.Get("DeleteWellScript");
            Process mbw = new Process();
            switch (rtuType)
            {
                case "UNILRP":
                case "UNISRPV100":
                case "UNISRPV110":
                case "UNISRPV200":
                case "AEPOC2":
                case "SAMRP2":
                case "EPICRP":
                    try
                    {
                        mbw.StartInfo.UseShellExecute = true;
                        mbw.StartInfo.FileName = strMcsscrip;
                        mbw.StartInfo.Arguments = String.Format(strDeleteBulkWellScript + " deletebeamwells \"\" \"\" 1 -db beamdb");

                        TestContext.WriteLine(strMcsscrip + " " + mbw.StartInfo.Arguments);
                        Assert.IsTrue(mbw.Start());
                        mbw.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;

                case "AE6008":
                    try
                    {
                        mbw.StartInfo.UseShellExecute = true;
                        mbw.StartInfo.FileName = strMcsscrip;
                        mbw.StartInfo.Arguments = String.Format(strDeleteBulkWellScript + " deleteinjectionwells \"\" \"\" 1 -db injectdb");

                        TestContext.WriteLine(strMcsscrip + " " + mbw.StartInfo.Arguments);
                        Assert.IsTrue(mbw.Start());
                        mbw.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;

                case "KLT595":
                case "KELTRN":
                case "GCSVFD":
                case "GCSV72":
                case "GCSV84":
                case "VTXICM":
                case "FSONLY":
                case "FSWPHX":
                case "VSONLY":
                case "VSWPHX":
                case "ROBICN":
                case "SPDSTR":
                case "VORTEX":
                case "SUGNMB":
                    try
                    {
                        mbw.StartInfo.UseShellExecute = true;
                        mbw.StartInfo.FileName = strMcsscrip;
                        mbw.StartInfo.Arguments = String.Format(strDeleteBulkWellScript + " deletesubswells \"\" \"\" 1 -db subsdb");

                        TestContext.WriteLine(strMcsscrip + " " + mbw.StartInfo.Arguments);
                        Assert.IsTrue(mbw.Start());
                        mbw.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;
                case "YSKAWA":
                    try
                    {
                        mbw.StartInfo.UseShellExecute = true;
                        mbw.StartInfo.FileName = strMcsscrip;
                        mbw.StartInfo.Arguments = String.Format(strDeleteBulkWellScript + " deletepcpwells \"\" \"\" 1 -db pcpdb");

                        TestContext.WriteLine(strMcsscrip + " " + mbw.StartInfo.Arguments);
                        Assert.IsTrue(mbw.Start());
                        mbw.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;
            }

            // let's also delete the scan task output files BE*.csv, IJ*.csv, and SU*.csv

            try
            {
                string[] csvList = Directory.GetFiles(m_strLiftDir, "*.csv");

                foreach (string f in csvList)
                {
                    if ((f.Contains("BE") && f.Contains(".csv")) || (f.Contains("IJ") && f.Contains(".csv")) || (f.Contains("SU") && f.Contains(".csv")) || (f.Contains("GL") && f.Contains(".csv")))
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


            Stopwatch.Stop();
            SampleRunTimes.Add(Stopwatch.ElapsedMilliseconds);
            Stopwatch.Reset();
            if (b_GetStats == "true")
            {
                GatherStat("Cleanup", "", SampleRunTimes, CreateWellsElapsedMilliSeconds);
            }
        }

        protected void CreateBeamWells(string rtuType)
        {

            Stopwatch.Start();
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
                    case "UNILRP":
                        parmWellNamePrefix = "ULRP_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 45;
                        parmLastAddress = 45 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "UNILRP";
                        parmRtuSubType = "UNILRP";
                        break;

                    case "UNISRPV100":
                        parmWellNamePrefix = "UV100_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 81;
                        parmLastAddress = 81 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "UNISRP";
                        parmRtuSubType = "V100";
                        break;

                    case "UNISRPV110":
                        parmWellNamePrefix = "UV110_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 91;
                        parmLastAddress = 91 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "UNISRP";
                        parmRtuSubType = "V110";
                        break;

                    case "UNISRPV200":
                        parmWellNamePrefix = "UV200_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 101;
                        parmLastAddress = 101 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "UNISRP";
                        parmRtuSubType = "V200";
                        break;

                    case "AEPOC2":
                        parmWellNamePrefix = "AE2_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 41;
                        parmLastAddress = 41 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "AEPOC2";
                        parmRtuSubType = "AEPOC2";
                        break;

                    case "SAMFS":
                        parmWellNamePrefix = "SAMFS_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 21;
                        parmLastAddress = 21 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "SAMRP2";
                        parmRtuSubType = "SAMFS";
                        break;

                    case "SAMFW":
                        parmWellNamePrefix = "SAMFW_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 21;
                        parmLastAddress = 21 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "SAMRP2";
                        parmRtuSubType = "SAMFW";
                        break;

                    case "SAMVS":
                        parmWellNamePrefix = "SAMVS_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 21;
                        parmLastAddress = 21 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "SAMRP2";
                        parmRtuSubType = "SAMVS";
                        break;

                    case "SAMVW":
                        parmWellNamePrefix = "SAMVW_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 21;
                        parmLastAddress = 21 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "SAMRP2";
                        parmRtuSubType = "SAMVW";
                        break;

                    case "EPICLM":
                        parmWellNamePrefix = "EPICLM_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 120;
                        parmLastAddress = 120 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "EPICRP";
                        parmRtuSubType = "EPICLM";
                        break;

                    case "EPICVF":
                        parmWellNamePrefix = "EPICVF_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 20;
                        parmLastAddress = 20 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "EPICRP";
                        parmRtuSubType = "EPICVF";
                        break;

                    case "EPICFS":
                        parmWellNamePrefix = "EPICFS_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 70;
                        parmLastAddress = 70 + NumberofWells;
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

                CreateAnalogPoints(parmWellNamePrefix, parmFirstWellNumber, parmLastAddress - parmFirstAddress + 1);
                Stopwatch.Stop();
                SampleRunTimes.Add(Stopwatch.ElapsedMilliseconds);

                Stopwatch.Reset();
                if (b_GetStats == "true")
                {
                    GatherStat("CreateBeamWells", rtuType, SampleRunTimes, null);
                }

            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
        }

        protected void CreatePCPWells(string rtuType)
        {

            Stopwatch.Start();
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
                    case "YSKAWA":
                        parmWellNamePrefix = "YSK_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 241;
                        parmLastAddress = 241 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "YSKAWA";
                        parmRtuSubType = "NONE";
                        break;

                }

                mbw.StartInfo.Arguments = String.Format("{0} makepcprange \"\" \"{1}\" {2} {3} {4} {5} \"{6}\" \"{7}\" \"{8}\" -db pcpdb", strMakeBulkWellScript, parmWellNamePrefix, parmFirstWellNumber, parmFirstAddress, parmLastAddress, parmChannel, parmAltAddress, parmRtuType, parmRtuSubType);

                TestContext.WriteLine(strMcsscrip + " " + mbw.StartInfo.Arguments);
                Assert.IsTrue(mbw.Start());
                mbw.WaitForExit();

                CreateAnalogPoints(parmWellNamePrefix, parmFirstWellNumber, parmLastAddress - parmFirstAddress + 1);


                Stopwatch.Stop();
                SampleRunTimes.Add(Stopwatch.ElapsedMilliseconds);

                Stopwatch.Reset();
                if (b_GetStats == "true")
                {
                    GatherStat("CreatePCPWells", rtuType, SampleRunTimes, null);
                }

            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
        }

        protected void CreateESPWells(string rtuType)
        {

            Stopwatch.Start();
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
                    case "SUGNMB":
                        parmWellNamePrefix = "SUG_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 149;
                        parmLastAddress = 149 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "SUGNMB";
                        parmRtuSubType = "SUGMB1";
                        break;

                    case "VORTEX":
                        parmWellNamePrefix = "VOR_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 161;
                        parmLastAddress = 161 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "VORTEX";
                        parmRtuSubType = "NONE";
                        break;
                    case "SPDSTR":
                        parmWellNamePrefix = "SPD_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 145;
                        parmLastAddress = 145 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "SPDSTR";
                        parmRtuSubType = "NONE";
                        break;
                    case "ROBICN":
                        parmWellNamePrefix = "RBN_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 141;
                        parmLastAddress = 141 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "ROBICN";
                        parmRtuSubType = "NONE";
                        break;

                    case "KLT595":
                        parmWellNamePrefix = "K595_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 131;
                        parmLastAddress = 131 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "KLT595";
                        parmRtuSubType = "KLT595";
                        break;

                    case "KELTRN":
                        parmWellNamePrefix = "KTRN_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 127;
                        parmLastAddress = 127 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "KELTRN";
                        parmRtuSubType = "KELTRN";
                        break;

                    case "GCSVFD":
                        parmWellNamePrefix = "GVFD_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 123;
                        parmLastAddress = 123 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "GCSVFD";
                        parmRtuSubType = "GCSVFD";
                        break;

                    case "GCSV84":
                        parmWellNamePrefix = "GV84_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 119;
                        parmLastAddress = 119 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "GCSESP";
                        parmRtuSubType = "8V04";
                        break;

                    case "GCSV72":
                        parmWellNamePrefix = "GV72_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 115;
                        parmLastAddress = 115 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "GCSESP";
                        parmRtuSubType = "7V20";
                        break;

                    case "VTXICM":
                        parmWellNamePrefix = "VICM_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 111;
                        parmLastAddress = 111 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "VTXICM";
                        parmRtuSubType = "VTXICM";
                        break;

                    case "FSONLY":
                        parmWellNamePrefix = "FSO_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 200;
                        parmLastAddress = 200 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "REDUNI";
                        parmRtuSubType = "FSONLY";
                        break;

                    case "FSWPHX":
                        parmWellNamePrefix = "FSW_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 211;
                        parmLastAddress = 211 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "REDUNI";
                        parmRtuSubType = "FSWPHX";
                        break;

                    case "VSONLY":
                        parmWellNamePrefix = "VSO_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 221;
                        parmLastAddress = 221 + NumberofWells;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "REDUNI";
                        parmRtuSubType = "VSONLY";
                        break;

                    case "VSWPHX":
                        parmWellNamePrefix = "VSW_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 231;
                        parmLastAddress = 231 + NumberofWells;
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

                CreateAnalogPoints(parmWellNamePrefix, parmFirstWellNumber, parmLastAddress - parmFirstAddress + 1);

                Stopwatch.Stop();
                SampleRunTimes.Add(Stopwatch.ElapsedMilliseconds);// should add at first index 0 of list

                Stopwatch.Reset();
                if (b_GetStats == "true")
                {
                    GatherStat("CreateESPionWells", rtuType, SampleRunTimes, null);
                }

            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
        }

        protected void CreateInjectionWells(string rtuType)
        {

            Stopwatch.Start();
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
                        parmLastAddress = 151 + NumberofWells;
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

                CreateAnalogPoints(parmWellNamePrefix, parmFirstWellNumber, parmLastAddress - parmFirstAddress + 1);
                if (rtuType == "AE6008")
                {
                    CreateDiscretePoints(parmWellNamePrefix, parmFirstWellNumber, parmLastAddress - parmFirstAddress + 1);
                }

                Stopwatch.Stop();
                SampleRunTimes.Add(Stopwatch.ElapsedMilliseconds); // should add at first index 0 of list

                Stopwatch.Reset();
                if (b_GetStats == "true")
                {
                    GatherStat("CreateInjectionWells", rtuType, SampleRunTimes, null);
                }

            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
        }

        protected void CreateAnalogPoints(string wellNamePrefix, int wellNameNumber, int iNumberWells)
        {
            for (int i = 0; i < iNumberWells; i++)
            {
                string wellName = String.Format("{0}{1:D4}", wellNamePrefix, wellNameNumber + i);

                string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
                string strAnalogsScript = ConfigurationManager.AppSettings.Get("CreateAnalogsScript");
                string strAnalogImportFile = ConfigurationManager.AppSettings.Get("AnalogsImportFile");

                Process importAnalogs = new Process();

                try
                {
                    importAnalogs.StartInfo.UseShellExecute = true;
                    importAnalogs.StartInfo.FileName = strMcsscrip;

                    string strArgs = String.Format("{0} importanalogs \"\" \"{1}\" \"{2}\" -db monitordb", strAnalogsScript, wellName, strAnalogImportFile);
                    importAnalogs.StartInfo.Arguments = strArgs;
                    TestContext.WriteLine(strMcsscrip + " " + importAnalogs.StartInfo.Arguments);

                    Assert.IsTrue(importAnalogs.Start());
                    importAnalogs.WaitForExit();
                }
                catch (Exception e)
                {
                    Assert.Fail("ERROR: {0}", e.ToString());
                }
            }
        }

        protected void CreateDiscretePoints(string wellNamePrefix, int wellNameNumber, int iNumberWells)
        {
            for (int i = 0; i < iNumberWells; i++)
            {
                string wellName = String.Format("{0}{1:D4}", wellNamePrefix, wellNameNumber + i);
                string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
                string strAnalogsScript = ConfigurationManager.AppSettings.Get("CreateAnalogsScript");
                string strDiscreteImportFile = ConfigurationManager.AppSettings.Get("DiscretesImportFile");

                Process importDiscretes = new Process();

                try
                {
                    importDiscretes.StartInfo.UseShellExecute = true;
                    importDiscretes.StartInfo.FileName = strMcsscrip;

                    string strArgs = String.Format("{0} importdiscretes \"\" \"{1}\" \"{2}\" -db monitordb", strAnalogsScript, wellName, strDiscreteImportFile);
                    importDiscretes.StartInfo.Arguments = strArgs;
                    TestContext.WriteLine(strMcsscrip + " " + importDiscretes.StartInfo.Arguments);

                    Assert.IsTrue(importDiscretes.Start());
                    importDiscretes.WaitForExit();
                }
                catch (Exception e)
                {
                    Assert.Fail("ERROR: {0}", e.ToString());
                }
            }
        }

        protected void SetupSUGNMB()
        {
            File.Copy(ConfigurationManager.AppSettings.Get("SetupSUGNMB"), Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SetupSUGNMB"))), true);
            File.SetAttributes(Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SetupSUGNMB"))), FileAttributes.Normal);
            File.Copy(ConfigurationManager.AppSettings.Get("SetupSUGNMBInput"), Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SetupSUGNMBInput"))), true);
            File.SetAttributes(Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SetupSUGNMBInput"))), FileAttributes.Normal);
            string SUGNMBScript = "SUGNMBSetup.css";
            string SUGNMBInputFile = "SUGMB1.tsv";
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            string SetupSUGNMB = Path.Combine(m_strLiftDir, SUGNMBScript);
            string SetupSUGNMBInput = Path.Combine(m_strLiftDir, SUGNMBInputFile);
            Process sugnmb = new Process();
            try
            {
                sugnmb.StartInfo.UseShellExecute = true;
                sugnmb.StartInfo.FileName = strMcsscrip;
                sugnmb.StartInfo.Arguments = String.Format("\"" + SetupSUGNMB + "\" SUGNMBSetup \"\" \"" + SetupSUGNMBInput + "\"" + " " + "-db subsdb");
                TestContext.WriteLine(strMcsscrip + " " + sugnmb.StartInfo.Arguments);

                Assert.IsTrue(sugnmb.Start());
                sugnmb.WaitForExit();
            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }

        }

        protected void SetCSVOutputDir(string rtuType)
        {
            Stopwatch.Start();
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            string strSetCsvOption = ConfigurationManager.AppSettings.Get("CSVOptionScript");

            Process setcsv = new Process();

            switch (rtuType)
            {
                case "UNIXRP":
                case "AEPOC2":
                case "SAMRP2":
                case "EPICRP":
                    try
                    {
                        setcsv.StartInfo.UseShellExecute = true;
                        setcsv.StartInfo.FileName = strMcsscrip;
                        setcsv.StartInfo.Arguments = String.Format(strSetCsvOption + " setBeamprocCsvFilePath \"\" \"" + m_strLiftDir + "\"");
                        TestContext.WriteLine(strMcsscrip + " " + setcsv.StartInfo.Arguments);

                        Assert.IsTrue(setcsv.Start());
                        setcsv.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;

                case "AE6008":
                    try
                    {
                        setcsv.StartInfo.UseShellExecute = true;
                        setcsv.StartInfo.FileName = strMcsscrip;
                        setcsv.StartInfo.Arguments = String.Format(strSetCsvOption + " setInjectprocCsvFilePath \"\" \"" + m_strLiftDir + "\"");
                        TestContext.WriteLine(strMcsscrip + " " + setcsv.StartInfo.Arguments);

                        Assert.IsTrue(setcsv.Start());
                        setcsv.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;

                case "KLT595":
                case "KELTRN":
                case "GCSVFD":
                case "GCSV72":
                case "GCSV84":
                case "VTXICM":
                case "REDUNI":
                case "ROBICN":
                case "SPDSTR":
                case "VORTEX":
                case "SUGNMB":
                    try
                    {
                        setcsv.StartInfo.UseShellExecute = true;
                        setcsv.StartInfo.FileName = strMcsscrip;
                        setcsv.StartInfo.Arguments = String.Format(strSetCsvOption + " setEspprocCsvFilePath \"\" \"" + m_strLiftDir + "\"");
                        TestContext.WriteLine(strMcsscrip + " " + setcsv.StartInfo.Arguments);

                        Assert.IsTrue(setcsv.Start());
                        setcsv.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;

                case "YSKAWA":
                    try
                    {
                        setcsv.StartInfo.UseShellExecute = true;
                        setcsv.StartInfo.FileName = strMcsscrip;
                        setcsv.StartInfo.Arguments = String.Format(strSetCsvOption + " setPCPprocCsvFilePath \"\" \"" + m_strLiftDir + "\"");
                        TestContext.WriteLine(strMcsscrip + " " + setcsv.StartInfo.Arguments);

                        Assert.IsTrue(setcsv.Start());
                        setcsv.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;

            }

            Stopwatch.Stop();
            SampleRunTimes.Add(Stopwatch.ElapsedMilliseconds);
            Stopwatch.Reset();
            if (b_GetStats == "true")
            {
                GatherStat("SetCSVOutputDir", "", SampleRunTimes, CreateWellsElapsedMilliSeconds);
            }
        }

        protected void GetCurrentStatus(int wellNumber, string rtuType)
        {

            string strCurrentStatus = Path.Combine(m_strLiftDir, "mtstbeam.exe");
            string strCurrentStatusSub = Path.Combine(m_strLiftDir, "mtstsub.exe");
            string strHostName = Dns.GetHostName();

            Process mtstbeam = new Process();
            Process mtstsub = new Process();

            switch (rtuType)
            {
                case "UNIXRP":
                case "AEPOC2":
                case "SAMRP2":
                case "EPICRP":
                    try
                    {
                        mtstbeam.StartInfo.UseShellExecute = true;
                        mtstbeam.StartInfo.FileName = strCurrentStatus;
                        mtstbeam.StartInfo.Arguments = String.Format(strHostName + " beamdb\\\\cs_beamp 1 " + wellNumber);

                        TestContext.WriteLine(strCurrentStatus + " " + mtstbeam.StartInfo.Arguments);
                        Assert.IsTrue(mtstbeam.Start());
                        mtstbeam.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;

                case "AE6008":
                    try
                    {
                        mtstbeam.StartInfo.UseShellExecute = true;
                        mtstbeam.StartInfo.FileName = strCurrentStatus;
                        mtstbeam.StartInfo.Arguments = String.Format(strHostName + " injectdb\\\\cs_injp 1 " + wellNumber);

                        TestContext.WriteLine(strCurrentStatus + " " + mtstbeam.StartInfo.Arguments);
                        Assert.IsTrue(mtstbeam.Start());
                        mtstbeam.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;

                case "VTXICM":
                case "GCSESP":
                case "GCSVFD":
                case "KELTRN":
                case "KLT595":
                case "REDUNI":
                case "ROBICN":
                case "SPDSTR":
                case "VORTEX":
                case "SUGNMB":
                    try
                    {
                        mtstsub.StartInfo.UseShellExecute = true;
                        mtstsub.StartInfo.FileName = strCurrentStatusSub;
                        mtstsub.StartInfo.Arguments = String.Format(strHostName + " subsdb\\\\cs_subp 1 " + wellNumber);

                        TestContext.WriteLine(strCurrentStatusSub + " " + mtstsub.StartInfo.Arguments);
                        Assert.IsTrue(mtstsub.Start());
                        mtstsub.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;

                case "YSKAWA":
                    try
                    {
                        mtstbeam.StartInfo.UseShellExecute = true;
                        mtstbeam.StartInfo.FileName = strCurrentStatus;
                        mtstbeam.StartInfo.Arguments = String.Format(strHostName + " pcpdb\\\\cs_pcpp 1 " + wellNumber);

                        TestContext.WriteLine(strCurrentStatus + " " + mtstbeam.StartInfo.Arguments);
                        Assert.IsTrue(mtstbeam.Start());
                        mtstbeam.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;
            }
        }

        // dataSource: 0 is Always Poll, 1 is Poll If No Recent Stored Data, and 2 is Never Poll 
        // maxDataAge: time in seconds
        protected void GetCurrentStatus(int wellNumber, int dataSource, int maxDataAge, string rtuType)
        {

            string strCurrentStatus = Path.Combine(m_strLiftDir, "mtstbeam.exe");
            string strCurrentStatusSub = Path.Combine(m_strLiftDir, "mtstsub.exe");
            string strHostName = Dns.GetHostName();

            Process mtstbeam = new Process();
            Process mtstsub = new Process();

            switch (rtuType)
            {
                case "UNIXRP":
                case "AEPOC2":
                case "SAMRP2":
                case "EPICRP":
                    try
                    {
                        mtstbeam.StartInfo.UseShellExecute = true;
                        mtstbeam.StartInfo.FileName = strCurrentStatus;
                        mtstbeam.StartInfo.Arguments = String.Format(strHostName + " beamdb\\\\cs_beamp 1 " + wellNumber + " " + dataSource + " " + maxDataAge);

                        TestContext.WriteLine(strCurrentStatus + " " + mtstbeam.StartInfo.Arguments);
                        Assert.IsTrue(mtstbeam.Start());
                        mtstbeam.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;

                case "AE6008":
                    try
                    {
                        mtstbeam.StartInfo.UseShellExecute = true;
                        mtstbeam.StartInfo.FileName = strCurrentStatus;
                        mtstbeam.StartInfo.Arguments = String.Format(strHostName + " injectdb\\\\cs_injp 1 " + wellNumber + " " + dataSource + " " + maxDataAge);

                        TestContext.WriteLine(strCurrentStatus + " " + mtstbeam.StartInfo.Arguments);
                        Assert.IsTrue(mtstbeam.Start());
                        mtstbeam.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;

                case "VTXICM":
                case "GCSESP":
                case "GCSVFD":
                case "KELTRN":
                case "KLT595":
                case "REDUNI":
                case "ROBICN":
                case "SPDSTR":
                case "VORTEX":
                case "SUGNMB":
                    try
                    {
                        mtstsub.StartInfo.UseShellExecute = true;
                        mtstsub.StartInfo.FileName = strCurrentStatusSub;
                        mtstsub.StartInfo.Arguments = String.Format(strHostName + " subsdb\\\\cs_subp 1 " + wellNumber + " " + dataSource + " " + maxDataAge);

                        TestContext.WriteLine(strCurrentStatusSub + " " + mtstsub.StartInfo.Arguments);
                        Assert.IsTrue(mtstsub.Start());
                        mtstsub.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;
                case "YSKAWA":
                    try
                    {
                        mtstbeam.StartInfo.UseShellExecute = true;
                        mtstbeam.StartInfo.FileName = strCurrentStatus;
                        mtstbeam.StartInfo.Arguments = String.Format(strHostName + " pcpdb\\\\cs_pcpp 1 " + wellNumber + " " + dataSource + " " + maxDataAge);

                        TestContext.WriteLine(strCurrentStatus + " " + mtstbeam.StartInfo.Arguments);
                        Assert.IsTrue(mtstbeam.Start());
                        mtstbeam.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;
            }
        }

        protected bool CompareOutputFiles(String fileName1, String fileName2)
        {
            bool bEqual = true;

            try
            {
                if (!File.Exists(Path.Combine(m_strLiftDir, fileName1)))
                {
                    TestContext.WriteLine("File not found: " + Path.Combine(m_strLiftDir, fileName1));
                    return false;
                }

                if (!File.Exists(Path.Combine(m_strLiftDir, fileName2)))
                {
                    TestContext.WriteLine("File not found: " + Path.Combine(m_strLiftDir, fileName2));
                    return false;
                }

                string[] scanA = File.ReadAllLines(Path.Combine(m_strLiftDir, fileName1));
                string[] scanB = File.ReadAllLines(Path.Combine(m_strLiftDir, fileName2));

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("##### START OF FILE COMPARE #####");
                TestContext.WriteLine("Comparing " + fileName1 + " (length " + scanA.Length + ")" + " to " + fileName2 + " (length " + scanB.Length + ")");

                if (scanA.Length < 1)
                {
                    TestContext.WriteLine("ERROR: Output File #1 is empty.");
                    return false;
                }

                if (scanB.Length < 1)
                {
                    TestContext.WriteLine("ERROR: Output File #2 is empty.");
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

        public void StartEmuUpdates(int wellNumber)
        {
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            string strSetEmuUpdatesScript = ConfigurationManager.AppSettings.Get("EmuUpdates");

            Process EmuUpdates = new Process();

            try
            {
                EmuUpdates.StartInfo.UseShellExecute = true;
                EmuUpdates.StartInfo.FileName = strMcsscrip;
                EmuUpdates.StartInfo.Arguments = String.Format(strSetEmuUpdatesScript + " " + "Enable_RTUEmu_EPICRP_Update" + " " + "\"\"" + " " + wellNumber + " " + "\"C:\\csLift\\liftnt\\run" + @"""" + " " + "-db beamdb");
                TestContext.WriteLine(strMcsscrip + " " + EmuUpdates.StartInfo.Arguments);

                Assert.IsTrue(EmuUpdates.Start());
                EmuUpdates.WaitForExit();
            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }

        }

        public void StopEmuUpdates(int wellNumber)
        {
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            string strSetEmuUpdatesScript = ConfigurationManager.AppSettings.Get("EmuUpdates");

            Process EmuUpdates = new Process();

            try
            {
                EmuUpdates.StartInfo.UseShellExecute = true;
                EmuUpdates.StartInfo.FileName = strMcsscrip;
                EmuUpdates.StartInfo.Arguments = String.Format(strSetEmuUpdatesScript + " " + "Disable_RTUEmu_EPICRP_Update" + " " + "\"\"" + " " + wellNumber + " " + "\"C:\\csLift\\liftnt\\run" + @"""" + " " + "-db beamdb");
                TestContext.WriteLine(strMcsscrip + " " + EmuUpdates.StartInfo.Arguments);

                Assert.IsTrue(EmuUpdates.Start());
                EmuUpdates.WaitForExit();
            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }

        }

        //protected void TestTimeDataFetched(string rtuType)
        //{
        //    try
        //    {
        //        GetCurrentStatus(2, rtuType);
        //        TestContext.WriteLine("Sent Get Current Status Command for well 2 with the no cached data parameter");
        //        // get scan time for current status with no cached parameter
        //        GetScanTime(2);
        //        //Rename Scan2.txt to Scan1.txt for comparison
        //        System.IO.File.Move(Path.Combine(m_strLiftDir, "Scan2.txt"), Path.Combine(m_strLiftDir, "Scan1.txt"));
        //        System.Threading.Thread.Sleep(65000); // wait over a minute to make sure scan time goes over to the next minute

        //        GetCurrentStatus(2, 1, 200, rtuType);
        //        TestContext.WriteLine("Sent Get Current Status Command for well 2 with the cached data parameter");
        //        System.Threading.Thread.Sleep(5000);
        //        // get scan time for current status with cached parameter
        //        GetScanTime(2);

        //        string[] arrStrScan1;
        //        string[] arrStrScan2;

        //        arrStrScan1 = File.ReadAllLines(Path.Combine(m_strLiftDir, "Scan1.txt"));
        //        arrStrScan2 = File.ReadAllLines(Path.Combine(m_strLiftDir, "Scan2.txt"));

        //        TestContext.WriteLine("Date and Time for first scan: {0}", arrStrScan1[1]);
        //        TestContext.WriteLine("Date and Time for second scan: {0}", arrStrScan2[1]);

        //        // the scan time for non-cached and cached parameter should be identical
        //        // Assert is true that both times are equal
        //        Assert.IsTrue(CompareOutputFiles("Scan1.txt", "Scan2.txt"), "Scan times for non cached and cached current status do not match!");
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.Fail("ERROR: {0}", e.ToString());
        //    }
        //    finally
        //    {
        //        File.Delete(Path.Combine(m_strLiftDir, "Scan1.txt"));
        //        File.Delete(Path.Combine(m_strLiftDir, "Scan2.txt"));
        //    }
        //}

        //protected void GetScanTime(int wellNumber)
        //{
        //    string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
        //    string strGetScanTime = ConfigurationManager.AppSettings.Get("ScanTimeScript");

        //    Process scantime = new Process();

        //    try
        //    {
        //        scantime.StartInfo.UseShellExecute = true;
        //        scantime.StartInfo.FileName = strMcsscrip;
        //        scantime.StartInfo.Arguments = String.Format(strGetScanTime + " getScanTime \"\" " + wellNumber + " \"BE\" \"C:\\cslift\\liftnt\\run\\\\\" -db proddb");
        //        TestContext.WriteLine(strMcsscrip + " " + scantime.StartInfo.Arguments);

        //        Assert.IsTrue(scantime.Start());
        //        scantime.WaitForExit();
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.Fail("ERROR: {0}", e.ToString());
        //    }

        //}
    }
}
