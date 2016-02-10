using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Diagnostics;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
using System.Xml.Linq;
using System.Xml;
using System.Data;

namespace IntegrationTests.RTUReadWriteWF.test
{
    [TestClass]
    public class RTUReadWriteWF : TestDriverBase
    {
        static string host, mode, rtutyp, bufferDataType, wellnum;

        [ClassInitialize()]
        public static void ClassInit(TestContext context)
        {
            host = ConfigurationManager.AppSettings.Get("Host");
            mode = ConfigurationManager.AppSettings.Get("Mode");
            rtutyp = ConfigurationManager.AppSettings.Get("RTUTYP");
            bufferDataType = ConfigurationManager.AppSettings.Get("BufferDataType");
            wellnum = ConfigurationManager.AppSettings.Get("Wellnum");           
        }
        [ClassCleanup()]
        public static void ClassCleanup()
        {
            //TODO
        }


        [TestInitialize()]
        public void Initialize()
        {
            //TODO
        }


        [TestCategory("Read"),TestMethod]
        [Priority(0)]
        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
        //    "..\\AssetsDev\\TestConfigurations.xml",
        //    "Test", DataAccessMethod.Sequential)]
        public void RTUReadTestforBeam()
        {
           
            try
            {
                #region Well creation
                CheckIfLowisExist();

                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("EPICRP");
                }
                StartRTUEmu();
                CreateBeamWells(rtutyp);
                #endregion

                //Run the all read/write tests once for a given RTUTYP
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load("..\\AssetsDev\\"+ rtutyp + "TestConfigurations.xml");
                XmlNode xmlRoot = xmlDoc.GetElementsByTagName("Tests")[0];
                XmlNodeList listofTests = xmlRoot.ChildNodes;
                int numberofTests = 0;
				bool isReadSucessful = true;
                bool isReadOverAllSucessful = true;
                string str = "";
                foreach (XmlNode testNode in listofTests)
                {
                    string reqdef = testNode.ChildNodes[0].InnerText; //<ReqDef> Node
                    string outputXml = testNode.ChildNodes[1].InnerText;

                    if (!File.Exists(reqdef) || !File.Exists(reqdef))
                    {
                        isReadSucessful = false;
                        isReadOverAllSucessful = false;
                    }

                    #region Perform RTU read
                    string liftrun = GetLiftRunFolder();
                    string RWConsole = Path.Combine(liftrun, "RTUReadWriteConsole.exe");

                    if (!File.Exists(RWConsole))
                    {
                        Assert.Fail("Error: " + RWConsole + "Does not exist");
                    }

                    Process testRW = new Process();
                    testRW.StartInfo.UseShellExecute = false;
                    testRW.StartInfo.RedirectStandardOutput = true;
                   // testRW.StartInfo.CreateNoWindow = false;
                    testRW.StartInfo.FileName = RWConsole;
                    testRW.StartInfo.Arguments = String.Format("-host {0} -reqdef {1} -output {2} -mode {3} -bufferDataType {4} -wellnum {5}",
                                                                host, reqdef, outputXml, mode, bufferDataType, wellnum);

                    TestContext.WriteLine(RWConsole + " " + testRW.StartInfo.Arguments);
                    Assert.IsTrue(testRW.Start());

                    TestContext.WriteLine("Process {0} is running at process id {1}.", testRW.ProcessName, testRW.Id);

                    string strError = "";

                    while (!testRW.StandardOutput.EndOfStream)
                    {
                        string line = testRW.StandardOutput.ReadLine();
                        TestContext.WriteLine(line);
                        if (line.Length > 30 && mode.Equals("read"))
                        {
                            if (line.Substring(15, 10).Equals("Status: -1"))
                            {
                                isReadSucessful = false;
                                isReadOverAllSucessful = false;
								strError = strError + String.Format("{0} read failed", reqdef);
                               
                            }
                        }
                        else if (line.Length > 30 && mode.Equals("write"))
                        {
                            TestContext.WriteLine(line);
                            if (line.Substring(17, 10).Equals("Status: -1"))
                            {
                                strError = strError + String.Format("{0} write failed\n", reqdef);
                               
                            }
                        }
                    }
                    while (!testRW.StandardOutput.EndOfStream)
                    {
                        string line = testRW.StandardOutput.ReadLine();
                        TestContext.WriteLine(line);
                    }

                    testRW.WaitForExit();
                    if (!isReadSucessful)
                    {
                        str += "[ERROR] Test" + numberofTests++ + ": Read Failed " + reqdef + "\n";
                        isReadSucessful = true;
                    }
                    else
                    {
                        str += "Test" + numberofTests++ + ": Read successfully " + reqdef + "\n";
                    }
                }
                TestContext.WriteLine(str);
                if (!isReadOverAllSucessful)
                {
                    TestContext.WriteLine("[ERROR] Some of files are failed to read, check the log"); 
                }
                else
                {
                    TestContext.WriteLine("All files read successfully!!!! ");
                }
                #endregion
            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
            finally
            {
                StopRTUEmu();
            }
        }

        [TestCategory("Read"), TestMethod]
        [Priority(1)]
        //[DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
        //   "..\\AssetsDev\\TestConfigurations.xml",
        //   "Test", DataAccessMethod.Sequential)]
        public void RTUReadValidationforBeam()
        {
            //Run the all read/write tests once for a given RTUTYP
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("..\\AssetsDev\\" + rtutyp + "TestConfigurations.xml");
            XmlNode xmlRoot = xmlDoc.GetElementsByTagName("Tests")[0];
            XmlNodeList listofTests = xmlRoot.ChildNodes;
            int numberofTests = 0;
            bool isTestSucessful = true;

            foreach (XmlNode testNode in listofTests)
            {
                string expectedXml = testNode.ChildNodes[2].InnerText;
                string outputXml = testNode.ChildNodes[1].InnerText;

                if (!File.Exists(outputXml))
                {
                    isTestSucessful = false;
                    
                    TestContext.WriteLine("[ERROR] Test" + numberofTests++ + ": Read test did not produce corresponding output " + outputXml);
                }
                else
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(outputXml);

                    XmlDocument doc1 = new XmlDocument();
                    doc1.Load(expectedXml);

                    //Parsing the response xml
                    XmlNodeList nodelist1 = doc.GetElementsByTagName("Response");
                    XmlNode node1 = nodelist1.Item(0);
                    XmlNodeList childs1 = node1.ChildNodes;

                    //Parsing the expected xml
                    XmlNodeList nodelist2 = doc1.GetElementsByTagName("Response");
                    XmlNode node2 = nodelist2.Item(0);
                    XmlNodeList childs2 = node2.ChildNodes;

                    String strResponse = childs1.Item(2).InnerText;
                    String strExpeted = childs2.Item(2).InnerText;

                    if (!strResponse.Equals(strExpeted))
                    {
                        isTestSucessful = false;
                        TestContext.WriteLine("[ERROR] Test" + numberofTests++ + ": validation fails " + outputXml);
                    }
                    else
                        TestContext.WriteLine("Test" + numberofTests++ + ": validation successful " + outputXml);
                }
            }
            if (!isTestSucessful)
            {
                Assert.Fail("[ERROR] Some of validations failed, check the log");
            }
            else
            {
                TestContext.WriteLine("All Test validation successful ");
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
                string strCommand = strFolder + "RTUEmu.exe";

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
                string strName = System.IO.Path.GetFileNameWithoutExtension("RTUEmu.exe");
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
            string strMcsscrip = Path.Combine(GetLiftRunFolder(), "mcsscrip.exe");
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

            Stopwatch.Stop();
            SampleRunTimes.Add(Stopwatch.ElapsedMilliseconds);
            Stopwatch.Reset();

        }

        protected void CreateBeamWells(string rtuType)
        {

            Stopwatch.Start();
            string strMcsscrip = Path.Combine(GetLiftRunFolder(), "mcsscrip.exe");
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
                        parmLastAddress = 45;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "UNILRP";
                        parmRtuSubType = "UNILRP";
                        break;

                    case "UNISRPV100":
                        parmWellNamePrefix = "UV100_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 81;
                        parmLastAddress = 81;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "UNISRP";
                        parmRtuSubType = "V100";
                        break;

                    case "UNISRPV110":
                        parmWellNamePrefix = "UV110_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 91;
                        parmLastAddress = 91;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "UNISRP";
                        parmRtuSubType = "V110";
                        break;

                    case "UNISRPV200":
                        parmWellNamePrefix = "UV200_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 101;
                        parmLastAddress = 101;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "UNISRP";
                        parmRtuSubType = "V200";
                        break;

                    case "AEPOC2":
                        parmWellNamePrefix = "AE2_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 41;
                        parmLastAddress = 41;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "AEPOC2";
                        parmRtuSubType = "AEPOC2";
                        break;

                    case "SAMFS":
                        parmWellNamePrefix = "SAMFS_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 21;
                        parmLastAddress = 21;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "SAMRP2";
                        parmRtuSubType = "SAMFS";
                        break;

                    case "SAMFW":
                        parmWellNamePrefix = "SAMFW_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 21;
                        parmLastAddress = 21;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "SAMRP2";
                        parmRtuSubType = "SAMFW";
                        break;

                    case "SAMVS":
                        parmWellNamePrefix = "SAMVS_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 21;
                        parmLastAddress = 21;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "SAMRP2";
                        parmRtuSubType = "SAMVS";
                        break;

                    case "SAMVW":
                        parmWellNamePrefix = "SAMVW_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 21;
                        parmLastAddress = 21;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "SAMRP2";
                        parmRtuSubType = "SAMVW";
                        break;

                    case "EPICLM":
                        parmWellNamePrefix = "EPICLM_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 120;
                        parmLastAddress = 120;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "EPICRP";
                        parmRtuSubType = "EPICLM";
                        break;

                    case "EPICVF":
                        parmWellNamePrefix = "EPICVF_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 20;
                        parmLastAddress = 20;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "EPICRP";
                        parmRtuSubType = "EPICVF";
                        break;

                    case "EPICFS":
                        parmWellNamePrefix = "EPICFS_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 70;
                        parmLastAddress = 70;
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

                //CreateAnalogPoints(parmWellNamePrefix, parmFirstWellNumber, parmLastAddress - parmFirstAddress + 1);
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

                //CreateAnalogPoints(parmWellNamePrefix, parmFirstWellNumber, parmLastAddress - parmFirstAddress + 1);


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

                //CreateAnalogPoints(parmWellNamePrefix, parmFirstWellNumber, parmLastAddress - parmFirstAddress + 1);

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

                //CreateAnalogPoints(parmWellNamePrefix, parmFirstWellNumber, parmLastAddress - parmFirstAddress + 1);
                if (rtuType == "AE6008")
                {
                    //CreateDiscretePoints(parmWellNamePrefix, parmFirstWellNumber, parmLastAddress - parmFirstAddress + 1);
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

