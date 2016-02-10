using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using System.Configuration;
using System.Linq;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32;


namespace MOPTests
{
    /// <summary>
    /// Summary description for MOPTests
    /// </summary>
    [CodedUITest]
    public class MOPTests : Base
    {
        [TestCategory("MOPBeamToESPTest"), TestMethod]
        public void MOPBeamToESPTest()
        {

            try
            {
                DeleteStore();
                GetTheRightDirectory();
                string[] WellInfo = CreateBeamWells("AEPOC2");
                WellLName = WellInfo[0];
                WellFacName = WellInfo[1];

                ImportSomeWellInfo(WellLName);
                ImportWellTests(WellLName);
                ImportGoTrend(WellLName);

                StartLOWISGetToWell(WellLName);
                ExportBeamConfigGrid(WellLName, "");

                WellTypeIconFullFile = ExportWellTypeNIcon(WellLName);
                TestWellTypeNIcon("Before", WellTypeIconFullFile, WellLName);

                MOPBeamToESP(WellLName);
                ExportBeamConfigGrid(WellLName, "After");
                ExportESPGroupConfigGrid(WellLName, "After");
                CloseLOWISClient();

                welltestsFullFileAfter = ExportWelltests(WellLName);
                goTrendFullFileAfter = ExportGOTrend(WellLName);

                WellTypeIconFullFile = ExportWellTypeNIcon(WellLName);
                TestWellTypeNIcon("After", WellTypeIconFullFile, WellLName);


                TestConfigOutputFiles(BeamGridFullFileBefore, WellLName);
                TestConfigOutputFiles(BeamGridFullFileAfter, WellLName);
                TestMOPOutputFiles(MOPGridFullFileBefore, WellLName);
                TestMOPOutputFiles(MOPGridFullFileAfter, WellLName);
                //Assert.IsTrue(times[1] > times[0], "MOP Time Test Failed");
                TestConfigOutputFiles(ESPGridFullFileAfter, WellLName);
                TestCompareWellTestTrendFiles(ImportWellTestsF, welltestsFullFileAfter);
                TestCompareWellTestTrendFiles(ImportGoTrendF, goTrendFullFileAfter);
            }
            finally
            {
                UnLinkWellToFacility(WellFacName, WellLName);
                DeleteFacility(WellFacName);
                if (DeleteOutputfiles == "true") { Cleanup(true); }
                else { Cleanup(false); }

            }
        }

        [TestCategory("MOPESPToBeamTest"), TestMethod]
        public void MOPESPToBeamTest()
        {
            try
            {
                DeleteStore();
                GetTheRightDirectory();
                string[] WellInfo = CreateESPWells("GCSVFD");
                WellLName = WellInfo[0];
                WellFacName = WellInfo[1];

                ImportSomeWellInfo(WellLName);
                ImportWellTests(WellLName);
                ImportGoTrend(WellLName);

                StartLOWISGetToWell(WellLName);
                ExportESPGroupConfigGrid(WellLName, "");

                WellTypeIconFullFile = ExportWellTypeNIcon(WellLName);
                TestWellTypeNIcon("Before", WellTypeIconFullFile, WellLName);

                MOPESPToBeam(WellLName);
                ExportESPGroupConfigGrid(WellLName, "After");
                ExportBeamConfigGrid(WellLName, "After");
                CloseLOWISClient();


                welltestsFullFileAfter = ExportWelltests(WellLName);
                goTrendFullFileAfter = ExportGOTrend(WellLName);

                WellTypeIconFullFile = ExportWellTypeNIcon(WellLName);
                TestWellTypeNIcon("After", WellTypeIconFullFile, WellLName);

                TestConfigOutputFiles(ESPGridFullFileBefore, WellLName);
                TestConfigOutputFiles(ESPGridFullFileAfter, WellLName);
                TestMOPOutputFiles(MOPGridFullFileBefore, WellLName);
                TestMOPOutputFiles(MOPGridFullFileAfter, WellLName);
                //Assert.IsTrue(times[1] > times[0], "MOP Time Test Failed");
                TestConfigOutputFiles(BeamGridFullFileAfter, WellLName);
                TestCompareWellTestTrendFiles(ImportWellTestsF, welltestsFullFileAfter);
                // TestCompareWellTestTrendFiles(@"C:\Users\Shahzad.Ali\Desktop\gotrend.csv", @"C:\Users\Shahzad.Ali\Downloads\gotrendAfter.csv");
                TestCompareWellTestTrendFiles(ImportGoTrendF, goTrendFullFileAfter);
            }
            finally
            {
                UnLinkWellToFacility(WellFacName, WellLName);
                DeleteFacility(WellFacName);
                if (DeleteOutputfiles == "true") { Cleanup(true); }
                else { Cleanup(false); }
            }
        }

        [TestCategory("MOPBeamToPCPTest"), TestMethod]
        public void MOPBeamToPCPTest()
        {

            try
            {
                DeleteStore();
                GetTheRightDirectory();
                string[] WellInfo = CreateBeamWells("AEPOC2");
                WellLName = WellInfo[0];
                WellFacName = WellInfo[1];

                ImportSomeWellInfo(WellLName);
                ImportWellTests(WellLName);
                ImportGoTrend(WellLName);

                StartLOWISGetToWell(WellLName);
                ExportBeamConfigGrid(WellLName, "");

                WellTypeIconFullFile = ExportWellTypeNIcon(WellLName);
                TestWellTypeNIcon("Before", WellTypeIconFullFile, WellLName);

                MOPBeamToPCP(WellLName);
                ExportBeamConfigGrid(WellLName, "After");
                ExportPCPGroupConfigGrid(WellLName, "After");
                CloseLOWISClient();

                welltestsFullFileAfter = ExportWelltests(WellLName);
                goTrendFullFileAfter = ExportGOTrend(WellLName);

                WellTypeIconFullFile = ExportWellTypeNIcon(WellLName);
                TestWellTypeNIcon("After", WellTypeIconFullFile, WellLName);


                TestConfigOutputFiles(BeamGridFullFileBefore, WellLName);
                TestConfigOutputFiles(BeamGridFullFileAfter, WellLName);
                TestMOPOutputFiles(MOPGridFullFileBefore, WellLName);
                TestMOPOutputFiles(MOPGridFullFileAfter, WellLName);
                //Assert.IsTrue(times[1] > times[0], "MOP Time Test Failed");
                TestConfigOutputFiles(PCPGridFullFileAfter, WellLName);
                TestCompareWellTestTrendFiles(ImportWellTestsF, welltestsFullFileAfter);
                TestCompareWellTestTrendFiles(ImportGoTrendF, goTrendFullFileAfter);
            }
            finally
            {
                UnLinkWellToFacility(WellFacName, WellLName);
                DeleteFacility(WellFacName);
                if (DeleteOutputfiles == "true") { Cleanup(true); }
                else { Cleanup(false); }
            }
        }
    }





    public class Base
    {
        #region variables
        protected string BeamGridFullFileBefore = String.Empty;
        protected string BeamGridFullFileAfter = String.Empty;
        protected string ESPGridFullFileBefore = String.Empty;
        protected string ESPGridFullFileAfter = String.Empty;
        protected string PCPGridFullFileBefore = String.Empty;
        protected string PCPGridFullFileAfter = String.Empty;
        protected string MOPGridFullFileBefore = String.Empty;
        protected string MOPGridFullFileAfter = String.Empty;
        protected string goTrendFullFileAfter = String.Empty;
        protected string welltestsFullFileAfter = String.Empty;
        protected string WellTypeIconFullFile = String.Empty;

        protected string WellLName = String.Empty;
        protected string WellFacName = String.Empty;
        public List<int> times = new List<int>();

        protected string m_strLiftDir = GetLiftRunFolder();
        string LowisClientPath = ConfigurationManager.AppSettings["LowisClientPath"];
        string ConnectClientTo = ConfigurationManager.AppSettings["ConnectClientTo"];
        public string ExportLocation = ConfigurationManager.AppSettings.Get("ExportLocation");
        public string DeleteOutputfiles = ConfigurationManager.AppSettings.Get("DeleteOutputfiles");
        public string RunningInATS = ConfigurationManager.AppSettings.Get("IsRunningInATS");


        public string ImportWellTestsF = String.Empty;
        public string ImportGoTrendF = String.Empty;
        public string ImportWellInfo = String.Empty;
        string strMakeBulkWellScript = String.Empty;
        string strDeleteBulkWellScript = String.Empty;
        #endregion

        public void GetTheRightDirectory()
        {
            if (RunningInATS == "true")
            {
                string CWDPath = ConfigurationManager.AppSettings.Get("ATSCWD");
                Trace.WriteLine(CWDPath);

                string WantedCWDPath = Directory.GetParent(CWDPath).FullName;
                Trace.WriteLine(WantedCWDPath);

                ImportWellTestsF = WantedCWDPath + ConfigurationManager.AppSettings.Get("ImportWellTests");
                ImportGoTrendF = WantedCWDPath + ConfigurationManager.AppSettings.Get("ImportGOTrend");
                ImportWellInfo = WantedCWDPath + ConfigurationManager.AppSettings.Get("ImportWellInfo");
                strMakeBulkWellScript = WantedCWDPath + ConfigurationManager.AppSettings.Get("CreateWellScript");
                strDeleteBulkWellScript = WantedCWDPath + ConfigurationManager.AppSettings.Get("DeleteWellScript");
            }
            else //running local
            {
                ImportWellTestsF = ConfigurationManager.AppSettings.Get("ImportWellTests");
                ImportGoTrendF = ConfigurationManager.AppSettings.Get("ImportGOTrend");
                ImportWellInfo = ConfigurationManager.AppSettings.Get("ImportWellInfo");
                strMakeBulkWellScript = ConfigurationManager.AppSettings.Get("CreateWellScript");
                strDeleteBulkWellScript = ConfigurationManager.AppSettings.Get("DeleteWellScript");
            }

        }

        public void DeleteStore()
        {
            RegistryKey reg = Registry.CurrentUser;
            reg = reg.OpenSubKey("Software");
            reg = reg.OpenSubKey("Weatherford");
            reg = reg.OpenSubKey("LowisDesktop");
            string CStore = reg.GetValue("StoreLocation").ToString();

            DirectoryInfo di = new DirectoryInfo(CStore);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

            Directory.Delete(CStore, true);
        }

        public void ImportSomeWellInfo(string WellName)
        {
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");




            Process UpdateWell = new Process();

            try
            {
                UpdateWell.StartInfo.UseShellExecute = true;
                UpdateWell.StartInfo.FileName = strMcsscrip;


                UpdateWell.StartInfo.Arguments = String.Format("{0} UpdateWellFields \"\" \"{1}\" \"{2}\" -db prodrtdb", strMakeBulkWellScript, WellName, ImportWellInfo);

                Trace.WriteLine(strMcsscrip + " " + UpdateWell.StartInfo.Arguments);
                Assert.IsTrue(UpdateWell.Start());
                UpdateWell.WaitForExit();



            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }

            //UpdateWellFields
        }

        public void ExportPCPGroupConfigGrid(string WellName, string WhenExported)
        {
            Mouse.Click(GetAnalysisWorkflowButton());
            Trace.WriteLine("Clicked Analysis Workflow Button");
            Mouse.Click(GetStartButton());
            Trace.WriteLine("Clicked Start Button");
            Mouse.Click(GetConfigurationItem("PCPGroupConfigGrid"));
            Trace.WriteLine("Clicked PCPGroupConfigGrid Button");
            System.Threading.Thread.Sleep(2000);
            if (WhenExported == "After")
            {
                PCPGridFullFileAfter = GridExport("PCPGroupConfigGrid", WhenExported);
            }
            else
            {
                PCPGridFullFileBefore = GridExport("PCPGroupConfigGrid", WhenExported);
            }
            CloseOpenGrid("PCPGroupConfigGrid");
        }

        public void ExportESPGroupConfigGrid(string WellName, string WhenExported)
        {
            Mouse.Click(GetAnalysisWorkflowButton());
            Trace.WriteLine("Clicked Analysis Workflow Button");
            Mouse.Click(GetStartButton());
            Trace.WriteLine("Clicked Start Button");
            Mouse.Click(GetConfigurationItem("ESPGroupConfigGrid"));
            Trace.WriteLine("Clicked ESPGroupConfigGrid Button");
            System.Threading.Thread.Sleep(2000);
            if (WhenExported == "After")
            {
                ESPGridFullFileAfter = GridExport("ESPGroupConfigGrid", WhenExported);
            }
            else
            {
                ESPGridFullFileBefore = GridExport("ESPGroupConfigGrid", WhenExported);
            }
            CloseOpenGrid("ESPGroupConfigGrid");
        }

        public void StartLOWISGetToWell(string WellName)
        {

            ApplicationUnderTest LowisClient = ApplicationUnderTest.Launch(LowisClientPath);

            ConnectToLowis(false);

            System.Threading.Thread.Sleep(20000);

            WinWindow MainWindow = GetMainWindow(); MainWindow.Maximized = true; Trace.WriteLine("Maximized Main Window");
            System.Threading.Thread.Sleep(2000);
            Rectangle MyRect = GetMainRefreshButton().BoundingRectangle;
            Point RefreshClick = new Point(MyRect.X + 5, MyRect.Y + 5);
            Mouse.Click(RefreshClick);
            Trace.WriteLine("Clicked Main Refresh Button");
            System.Threading.Thread.Sleep(2000);
            Mouse.Click(RefreshClick);
            Trace.WriteLine("Clicked Main Refresh Button");
            WinCheckBoxTreeItem allckhbox = GetAllCheckbox("WellGrouping");
            Mouse.Click(allckhbox);
            Trace.WriteLine("Clicked All Wells checkbox for WellGrouping Window");
            //allckhbox.Checked = true;
            WinCheckBoxTreeItem allcheckbox = GetAllCheckbox("WellConditions");
            Mouse.Click(allcheckbox);
            Trace.WriteLine("Clicked All Wells checkbox for WellConditions Window");
            //allcheckbox.Checked = true;
            WinButton AllWellsRefreshButton = GetAllWellNavigatorButton("Refresh") as WinButton;
            Rectangle AllWellsRefreshButtonRect = AllWellsRefreshButton.BoundingRectangle;
            Point AllWellsRefreshClick = new Point(AllWellsRefreshButtonRect.X, AllWellsRefreshButtonRect.Y);
            Mouse.Click(AllWellsRefreshClick);
            Trace.WriteLine("Clicked All Wells Refresh Button");
            Mouse.Click(AllWellsRefreshButton);
            Trace.WriteLine("Clicked All Wells Refresh Button");
            System.Threading.Thread.Sleep(2000);
            Mouse.Click(GetAllWellNavigatorButton("SearchWell"));
            Trace.WriteLine("Clicked Search wells Button");
            WinEdit SearchBox = SearchWellWinControls("SearchTxtBox") as WinEdit;
            SearchBox.Text = WellName;
            Trace.WriteLine("Set Search Well Text box to " + WellName);
            Mouse.Click(SearchWellWinControls("OKButton") as WinButton);
            Trace.WriteLine("Clicked OK Button in search well window");
            Mouse.Click(GetAnalysisWorkflowButton());
            Trace.WriteLine("Clicked Analysis Workflow Button");

        }

        public void CloseLOWISClient()
        {
            WinWindow MainWindow = GetMainWindow(); MainWindow.Maximized = true; Trace.WriteLine("Maximized Main Window");
            MainWindow.SetFocus();
            Keyboard.SendKeys("{F4}", ModifierKeys.Alt);
            Trace.WriteLine("Closing Client");
            System.Threading.Thread.Sleep(3000);

        }

        public void ExportBeamConfigGrid(string WellName, string WhenExported)
        {

            Mouse.Click(GetAnalysisWorkflowButton());
            Trace.WriteLine("Clicked Analysis Workflow Button");
            Mouse.Click(GetStartButton());
            Trace.WriteLine("Clicked Start Button");
            Mouse.Click(GetConfigurationItem("BeamWellGroupConfig"));
            Trace.WriteLine("Clicked BeamWellGroupConfig Button");
            System.Threading.Thread.Sleep(2000);
            if (WhenExported == "After")
            {
                BeamGridFullFileAfter = GridExport("BeamWellGroupConfig", WhenExported);
            }
            else
            {
                BeamGridFullFileBefore = GridExport("BeamWellGroupConfig", WhenExported);
            }

            CloseOpenGrid("BeamWellGroupConfig");
        }

        public void MOPBeamToESP(string WellName)
        {
            Mouse.Click(GetAnalysisWorkflowButton());
            Trace.WriteLine("Clicked Analysis Workflow Button");
            Mouse.Click(GetStartButton());
            Trace.WriteLine("Clicked Start Button");
            Mouse.Click(GetStatusItem("MOP"));
            Trace.WriteLine("Clicked MOP Grid");
            Thread.Sleep(3000);
            MOPGridFullFileBefore = GridExport("MOP", "");
            Mouse.Click(MOPAddButton());
            Trace.WriteLine("Clicked MOP Grid add button");
            System.Threading.Thread.Sleep(4000);
            MOPAddWellStatusWin("NewStatus", "ACTIVE","");
            System.Threading.Thread.Sleep(2000);
            MOPAddWellStatusWin("MOP", "Electrical Submersible Pump","");
            System.Threading.Thread.Sleep(3000);
            MOPAddWellStatusWin("WellType", "OIL","");
            System.Threading.Thread.Sleep(3000);
            MOPAddWellStatusWin("StatusComment", "Changing MOP from BEAM PUMPING to ESP","");
            System.Threading.Thread.Sleep(3000);
            MOPAddWellStatusWin("OKGreenCheck", "",WellName);
            System.Threading.Thread.Sleep(4000);
            MOPGridFullFileAfter = GridExport("MOP", "After");
            CloseOpenGrid("MOP");

            //// MOPAddWellStatusWin("RedXButton", "");

        }

        public void MOPBeamToPCP(string WellName)
        {
            Mouse.Click(GetAnalysisWorkflowButton());
            Trace.WriteLine("Clicked Analysis Workflow Button");
            Mouse.Click(GetStartButton());
            Trace.WriteLine("Clicked Start Button");
            Mouse.Click(GetStatusItem("MOP"));
            Trace.WriteLine("Clicked MOP Grid");
            Thread.Sleep(3000);
            MOPGridFullFileBefore = GridExport("MOP", "");
            Mouse.Click(MOPAddButton());
            Trace.WriteLine("Clicked MOP Grid add button");
            System.Threading.Thread.Sleep(4000);
            MOPAddWellStatusWin("NewStatus", "ACTIVE","");
            System.Threading.Thread.Sleep(2000);
            MOPAddWellStatusWin("MOP", "Progressive Cavity Pump","");
            System.Threading.Thread.Sleep(3000);
            MOPAddWellStatusWin("WellType", "OIL","");
            System.Threading.Thread.Sleep(3000);
            MOPAddWellStatusWin("StatusComment", "Changing MOP from BEAM PUMPING to PCP","");
            System.Threading.Thread.Sleep(3000);
            MOPAddWellStatusWin("OKGreenCheck", "",WellName);
            System.Threading.Thread.Sleep(4000);
            MOPGridFullFileAfter = GridExport("MOP", "After");
            CloseOpenGrid("MOP");

            //// MOPAddWellStatusWin("RedXButton", "");

        }

        public void MOPESPToBeam(string WellName)
        {
            Mouse.Click(GetAnalysisWorkflowButton());
            Trace.WriteLine("Clicked Analysis Workflow Button");
            Mouse.Click(GetStartButton());
            Trace.WriteLine("Clicked Start Button");
            Mouse.Click(GetStatusItem("MOP"));
            Trace.WriteLine("Clicked MOP Grid");
            Thread.Sleep(3000);
            MOPGridFullFileBefore = GridExport("MOP", "");
            Mouse.Click(MOPAddButton());
            Trace.WriteLine("Clicked MOP Grid add button");
            System.Threading.Thread.Sleep(4000);
            MOPAddWellStatusWin("NewStatus", "ACTIVE","");
            System.Threading.Thread.Sleep(2000);
            MOPAddWellStatusWin("MOP", "Beam Pumping","");
            System.Threading.Thread.Sleep(3000);
            MOPAddWellStatusWin("WellType", "OIL","");
            System.Threading.Thread.Sleep(3000);
            MOPAddWellStatusWin("StatusComment", "Changing MOP from ESP To Beam Pumping","");
            System.Threading.Thread.Sleep(3000);
            MOPAddWellStatusWin("OKGreenCheck", "",WellName);
            System.Threading.Thread.Sleep(4000);
            MOPGridFullFileAfter = GridExport("MOP", "After");
            CloseOpenGrid("MOP");

            //// MOPAddWellStatusWin("RedXButton", "");

        }

        protected static string GetLiftRunFolder()
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

        protected string[] CreateBeamWells(string rtuType)
        {
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");


            string parmWellNamePrefix = String.Empty;
            string parmAltAddress = String.Empty;
            string parmRtuType = String.Empty;
            string parmRtuSubType = String.Empty;
            string FacName = String.Empty;
            string Key = String.Empty;
            string WellName = String.Empty;
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


                    case "AEPOC2":
                        parmWellNamePrefix = "AE2_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 41;
                        parmLastAddress = 41;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "AEPOC2";
                        parmRtuSubType = "AEPOC2";
                        FacName = "BeamFac";
                        Key = "BeamKey";
                        break;


                }

                mbw.StartInfo.Arguments = String.Format("{0} makeberange \"\" \"{1}\" {2} {3} {4} {5} \"{6}\" \"{7}\" \"{8}\" -db beamdb", strMakeBulkWellScript, parmWellNamePrefix, parmFirstWellNumber, parmFirstAddress, parmLastAddress, parmChannel, parmAltAddress, parmRtuType, parmRtuSubType);

                Trace.WriteLine(strMcsscrip + " " + mbw.StartInfo.Arguments);
                Assert.IsTrue(mbw.Start());
                mbw.WaitForExit();

                WellName = parmWellNamePrefix + "000" + parmFirstWellNumber;

                AddFacility(FacName, Key);
                LinkWellToFacility(FacName, WellName);


            }

            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }


            return new string[] { WellName, FacName };

        }

        protected string[] CreateESPWells(string rtuType)
        {


            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");



            string parmWellNamePrefix = String.Empty;
            string parmAltAddress = String.Empty;
            string parmRtuType = String.Empty;
            string parmRtuSubType = String.Empty;
            string FacName = String.Empty;
            string Key = String.Empty;
            string WellName = String.Empty;
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




                    case "GCSVFD":
                        parmWellNamePrefix = "GVFD_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 123;
                        parmLastAddress = 123;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "GCSVFD";
                        parmRtuSubType = "GCSVFD";
                        FacName = "ESPFac";
                        Key = "ESPKey";
                        break;


                }

                mbw.StartInfo.Arguments = String.Format("{0} makesurange \"\" \"{1}\" {2} {3} {4} {5} \"{6}\" \"{7}\" \"{8}\" -db subsdb", strMakeBulkWellScript, parmWellNamePrefix, parmFirstWellNumber, parmFirstAddress, parmLastAddress, parmChannel, parmAltAddress, parmRtuType, parmRtuSubType);

                Trace.WriteLine(strMcsscrip + " " + mbw.StartInfo.Arguments);
                Assert.IsTrue(mbw.Start());
                mbw.WaitForExit();

                WellName = parmWellNamePrefix + "000" + parmFirstWellNumber;

                AddFacility(FacName, Key);
                LinkWellToFacility(FacName, WellName);



            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
            return new string[] { WellName, FacName };
        }

        protected void Cleanup(bool DeleteExportedFiles)
        {

            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");

            string[] welltypesToDelete = { "deletebeamwells", "deletesubswells", "deletepcpwells" };

            foreach (string WellTypeToDelete in welltypesToDelete)
            {
                Process dbw = new Process();
                switch (WellTypeToDelete)
                {
                    case "deletebeamwells":
                        try
                        {
                            dbw.StartInfo.UseShellExecute = true;
                            dbw.StartInfo.FileName = strMcsscrip;
                            dbw.StartInfo.Arguments = String.Format(strDeleteBulkWellScript + " deletebeamwells \"\" \"\" 1 -db beamdb");

                            Trace.WriteLine(strMcsscrip + " " + dbw.StartInfo.Arguments);
                            Assert.IsTrue(dbw.Start());
                            dbw.WaitForExit();
                        }
                        catch (Exception e)
                        {
                            Assert.Fail("ERROR: {0}", e.ToString());
                        }
                        break;

                    case "deleteinjectionwells":
                        try
                        {
                            dbw.StartInfo.UseShellExecute = true;
                            dbw.StartInfo.FileName = strMcsscrip;
                            dbw.StartInfo.Arguments = String.Format(strDeleteBulkWellScript + " deleteinjectionwells \"\" \"\" 1 -db injectdb");

                            Trace.WriteLine(strMcsscrip + " " + dbw.StartInfo.Arguments);
                            Assert.IsTrue(dbw.Start());
                            dbw.WaitForExit();
                        }
                        catch (Exception e)
                        {
                            Assert.Fail("ERROR: {0}", e.ToString());
                        }
                        break;

                    case "deletesubswells":
                        try
                        {
                            dbw.StartInfo.UseShellExecute = true;
                            dbw.StartInfo.FileName = strMcsscrip;
                            dbw.StartInfo.Arguments = String.Format(strDeleteBulkWellScript + " deletesubswells \"\" \"\" 1 -db subsdb");

                            Trace.WriteLine(strMcsscrip + " " + dbw.StartInfo.Arguments);
                            Assert.IsTrue(dbw.Start());
                            dbw.WaitForExit();
                        }
                        catch (Exception e)
                        {
                            Assert.Fail("ERROR: {0}", e.ToString());
                        }
                        break;
                    case "deletepcpwells":
                        try
                        {
                            dbw.StartInfo.UseShellExecute = true;
                            dbw.StartInfo.FileName = strMcsscrip;
                            dbw.StartInfo.Arguments = String.Format(strDeleteBulkWellScript + " deletepcpwells \"\" \"\" 1 -db pcpdb");

                            Trace.WriteLine(strMcsscrip + " " + dbw.StartInfo.Arguments);
                            Assert.IsTrue(dbw.Start());
                            dbw.WaitForExit();
                        }
                        catch (Exception e)
                        {
                            Assert.Fail("ERROR: {0}", e.ToString());
                        }
                        break;
                }
            }
            // let's also delete the output files
            if (DeleteExportedFiles == true)
            {
                try
                {
                    string[] csvList = Directory.GetFiles(ExportLocation, "*.csv");
                    string[] txtList = Directory.GetFiles(ExportLocation, "*.txt");

                    foreach (string c in csvList)
                    {
                        if ((c.Contains("SomeWell") && c.Contains(".csv")) || (c.Contains("gotrend") && c.Contains(".csv")) || (c.Contains("welltests") && c.Contains(".csv")))
                        {
                            Trace.WriteLine("Deleting File " + c);
                            File.Delete(c);
                        }
                    }
                    foreach (string t in txtList)
                    {
                        if ((t.Contains("Grid") && t.Contains(".txt")))
                        {
                            Trace.WriteLine("Deleting File " + t);
                            File.Delete(t);
                        }
                    }
                }
                catch (DirectoryNotFoundException e)
                {
                    Assert.Fail("ERROR: {0}", e.ToString());
                }
            }
        }

        protected void AddFacility(string FacName, string key)
        {
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");

            Process AddFac = new Process();

            try
            {
                AddFac.StartInfo.UseShellExecute = true;
                AddFac.StartInfo.FileName = strMcsscrip;


                AddFac.StartInfo.Arguments = String.Format("{0} AddFacility \"\" \"{1}\" \"{2}\" -db beamdb", strMakeBulkWellScript, FacName, key);

                Trace.WriteLine(strMcsscrip + " " + AddFac.StartInfo.Arguments);
                Assert.IsTrue(AddFac.Start());
                AddFac.WaitForExit();



            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
        }

        protected void DeleteFacility(string FacName)
        {
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");




            Process DelFac = new Process();

            try
            {
                DelFac.StartInfo.UseShellExecute = true;
                DelFac.StartInfo.FileName = strMcsscrip;


                DelFac.StartInfo.Arguments = String.Format("{0} DeleteFacility \"\" \"{1}\" -db beamdb", strMakeBulkWellScript, FacName);

                Trace.WriteLine(strMcsscrip + " " + DelFac.StartInfo.Arguments);
                Assert.IsTrue(DelFac.Start());
                DelFac.WaitForExit();



            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
        }

        protected void LinkWellToFacility(string FacName, string WellName)
        {
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");



            Process LinkFac = new Process();

            try
            {
                LinkFac.StartInfo.UseShellExecute = true;
                LinkFac.StartInfo.FileName = strMcsscrip;


                LinkFac.StartInfo.Arguments = String.Format("{0} LinkWellToFacility \"\" \"{1}\" \"{2}\" -db beamdb", strMakeBulkWellScript, FacName, WellName);

                Trace.WriteLine(strMcsscrip + " " + LinkFac.StartInfo.Arguments);
                Assert.IsTrue(LinkFac.Start());
                LinkFac.WaitForExit();



            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
        }

        protected void UnLinkWellToFacility(string FacName, string WellName)
        {
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");




            Process UnLinkFac = new Process();

            try
            {
                UnLinkFac.StartInfo.UseShellExecute = true;
                UnLinkFac.StartInfo.FileName = strMcsscrip;


                UnLinkFac.StartInfo.Arguments = String.Format("{0} UnlinkWellFromFacility \"\" \"{1}\" \"{2}\" -db beamdb", strMakeBulkWellScript, FacName, WellName);

                Trace.WriteLine(strMcsscrip + " " + UnLinkFac.StartInfo.Arguments);
                Assert.IsTrue(UnLinkFac.Start());
                UnLinkFac.WaitForExit();



            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
        }

        protected void ImportWellTests(string WellName)
        {
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");




            Process ImportWellTests = new Process();

            try
            {
                ImportWellTests.StartInfo.UseShellExecute = true;
                ImportWellTests.StartInfo.FileName = strMcsscrip;


                ImportWellTests.StartInfo.Arguments = String.Format("{0} ImportWelltests \"\" \"{1}\" \"{2}\" -db beamdb", strMakeBulkWellScript, WellName, ImportWellTestsF);

                Trace.WriteLine(strMcsscrip + " " + ImportWellTests.StartInfo.Arguments);
                Assert.IsTrue(ImportWellTests.Start());
                ImportWellTests.WaitForExit();



            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
        }

        protected void ImportGoTrend(string WellName)
        {
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");




            Process ImportGOTrend = new Process();

            try
            {
                ImportGOTrend.StartInfo.UseShellExecute = true;
                ImportGOTrend.StartInfo.FileName = strMcsscrip;


                ImportGOTrend.StartInfo.Arguments = String.Format("{0} ImportGOTrend \"\" \"{1}\" \"{2}\" -db beamdb", strMakeBulkWellScript, WellName, ImportGoTrendF);

                Trace.WriteLine(strMcsscrip + " " + ImportGOTrend.StartInfo.Arguments);
                Assert.IsTrue(ImportGOTrend.Start());
                ImportGOTrend.WaitForExit();



            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
        }

        protected string ExportWelltests(string WellName)
        {
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");


            string FileNameToExport = ExportLocation + "welltestsAfter.csv";


            Process ExportWelltests = new Process();

            try
            {
                ExportWelltests.StartInfo.UseShellExecute = true;
                ExportWelltests.StartInfo.FileName = strMcsscrip;


                ExportWelltests.StartInfo.Arguments = String.Format("{0} ExportWelltests \"\" \"{1}\" \"{2}\" -db beamdb", strMakeBulkWellScript, WellName, FileNameToExport);

                Trace.WriteLine(strMcsscrip + " " + ExportWelltests.StartInfo.Arguments);
                Assert.IsTrue(ExportWelltests.Start());
                ExportWelltests.WaitForExit();



            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
            return FileNameToExport;
        }

        protected string ExportGOTrend(string WellName)
        {
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");


            string FileNameToExport = ExportLocation + "gotrendAfter.csv";


            Process ExportGOTrend = new Process();

            try
            {
                ExportGOTrend.StartInfo.UseShellExecute = true;
                ExportGOTrend.StartInfo.FileName = strMcsscrip;


                ExportGOTrend.StartInfo.Arguments = String.Format("{0} ExportGOTrend \"\" \"{1}\" \"{2}\" -db beamdb", strMakeBulkWellScript, WellName, FileNameToExport);

                Trace.WriteLine(strMcsscrip + " " + ExportGOTrend.StartInfo.Arguments);
                Assert.IsTrue(ExportGOTrend.Start());
                ExportGOTrend.WaitForExit();



            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
            return FileNameToExport;
        }

        protected string ExportWellTypeNIcon(string WellName)
        {
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");

            string FileNameToExport = ExportLocation + "SomeWellInfo.csv";


            Process ExportSomeWellInfo = new Process();

            try
            {
                ExportSomeWellInfo.StartInfo.UseShellExecute = true;
                ExportSomeWellInfo.StartInfo.FileName = strMcsscrip;


                ExportSomeWellInfo.StartInfo.Arguments = String.Format("{0} DumpSomeWellInfo \"\" \"{1}\" \"{2}\" -db prodrtdb", strMakeBulkWellScript, WellName, FileNameToExport);


                Trace.WriteLine(strMcsscrip + " " + ExportSomeWellInfo.StartInfo.Arguments);
                Assert.IsTrue(ExportSomeWellInfo.Start());
                ExportSomeWellInfo.WaitForExit();



            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
            return FileNameToExport;
        }

        protected void TestWellTypeNIcon(string WhenExported, string fileName1, string WellName)
        {

            if (!File.Exists(fileName1))
            {
                Trace.WriteLine("File not found: " + (fileName1));
                Assert.Fail("Test OutputFiles Failure");
            }

            DirectoryInfo TestFile = new DirectoryInfo(fileName1);
            string TestFileName = TestFile.Name;
            string[] scanA = File.ReadAllLines((fileName1));



            if (scanA.Length < 1)
            {
                Trace.WriteLine("ERROR: Output File #1 is empty.");
                Assert.Fail("Test OutputFiles Failure");
            }

            Assert.IsTrue(scanA.Length == 3, "File Length not an expected one for WellType file");
            string InfoLine = scanA[1];
            char[] delimiter1 = new char[] { ',' };
            string[] things = InfoLine.Split(delimiter1); // 7 items - Group1,Group2,Group3,Group4,WellType,IconIden,comport,Nothing) //dont use remove white space incase we want to update a config field later
            if (WhenExported == "Before" && WellName.Contains("AE"))
            {
                Trace.WriteLine("Well Type = " + things[4].ToString());
                Trace.WriteLine("IconNumber = " + things[5].ToString());

                Assert.IsTrue(things[4].ToString() == "BE", "Well Type is not BEAM , fail test " + things[4].ToString());
                Assert.IsTrue(things[5].ToString() == "1001", "Well ICON is not BEAM , fail test  " + things[5].ToString());
                if (things[5].ToString() == "1001") { Trace.Write("Icon is of BEAM Well Type\n"); }
                Assert.IsTrue(things[6].ToString() == "COM2", "COMPort is not COM2 , fail test  " + things[6].ToString());
                if (things[6].ToString() == "COM2") { Trace.Write("COMPort is COM2 \n"); }
            }
            if (WhenExported == "After" && WellName.Contains("AE"))
            {
                Trace.WriteLine("Well Type = " + things[4].ToString());
                Trace.WriteLine("IconNumber = " + things[5].ToString());
                Assert.IsTrue((things[4].ToString() == "SU") || (things[4].ToString() == "PC"), "Well Type is not SUBS OR PCP, fail test " + things[4].ToString());
                Assert.IsTrue((things[5].ToString() == "1011") || (things[5].ToString() == "1007"), "Well ICON is not SUBS or PCP , fail test  " + things[5].ToString());
                if (things[5].ToString() == "1011") { Trace.Write("Icon is of SUBS Well Type\n"); }
                if (things[5].ToString() == "1007") { Trace.Write("Icon is of PCP Well Type\n"); }
                Assert.IsTrue(things[6].ToString() == "COM2", "COMPort is not COM2 , fail test  " + things[6].ToString());
                if (things[6].ToString() == "COM2") { Trace.Write("COMPort is COM2 \n"); }
            }
            if (WhenExported == "Before" && WellName.Contains("GVFD"))
            {
                Trace.WriteLine("Well Type = " + things[4].ToString());
                Trace.WriteLine("IconNumber = " + things[5].ToString());
                Assert.IsTrue(things[4].ToString() == "SU", "Well Type is not SUBS , fail test " + things[4].ToString());
                Assert.IsTrue(things[5].ToString() == "1011", "Well ICON is not SUBS , fail test  " + things[5].ToString());
                if (things[5].ToString() == "1011") { Trace.Write("Icon is of SUBS Well Type\n"); }
                Assert.IsTrue(things[6].ToString() == "COM2", "COMPort is not COM2 , fail test  " + things[6].ToString());
                if (things[6].ToString() == "COM2") { Trace.Write("COMPort is COM2 \n"); }
            }
            if (WhenExported == "After" && WellName.Contains("GVFD"))
            {
                Trace.WriteLine("Well Type = " + things[4].ToString());
                Trace.WriteLine("IconNumber = " + things[5].ToString());
                Assert.IsTrue(things[4].ToString() == "BE", "Well Type is not BEAM , fail test  " + things[4].ToString());
                Assert.IsTrue(things[5].ToString() == "1001", "Well ICON is not BEAM , fail test  " + things[5].ToString());
                if (things[5].ToString() == "1001") { Trace.Write("Icon is of BEAM Well Type\n"); }
                Assert.IsTrue(things[6].ToString() == "COM2", "COMPort is not COM2 , fail test  " + things[6].ToString());
                if (things[6].ToString() == "COM2") { Trace.Write("COMPort is COM2 \n"); }
            }
        }

        public void CheckIfExportExists(string File)
        {
            System.Threading.Thread.Sleep(3000);
            Assert.IsTrue(System.IO.File.Exists(File), "Exported File not found in location!");
            Trace.WriteLine("File found in location");

        }

        public void ConnectToLowis(bool CreateNewStore)
        {
            WpfWindow LOWISConnect = new WpfWindow();
            LOWISConnect.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            LOWISConnect.SearchProperties.Add(WpfWindow.PropertyNames.Name, "LOWIS: Connect");
            LOWISConnect.WindowTitles.Add("LOWIS: Connect");

            WpfComboBox HostCB = new WpfComboBox(LOWISConnect);
            HostCB.WindowTitles.Add("LOWIS: Connect");

            WpfEdit HostTB = new WpfEdit(HostCB);
            HostTB.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            HostTB.SearchProperties.Add(WpfEdit.PropertyNames.AutomationId, "PART_EditableTextBox");


            HostTB.Text = ConnectClientTo;
            Trace.WriteLine("Set ServerName to " + ConnectClientTo);
            WpfRadioButton Credentials = new WpfRadioButton(LOWISConnect);
            Credentials.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            Credentials.SearchProperties.Add(WpfRadioButton.PropertyNames.Name, "Use my windows credentials.");
            Credentials.WindowTitles.Add("LOWIS: Connect");

            Credentials.Selected = true;
            Trace.WriteLine("Use Windows Credentials checkbox is set to true");

            WpfButton ConnectButton = new WpfButton(LOWISConnect);
            ConnectButton.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            ConnectButton.SearchProperties[WpfButton.PropertyNames.AccessKey] = "\r";
            ConnectButton.WindowTitles.Add("LOWIS: Connect");


            WpfButton SettingsButton = new WpfButton(LOWISConnect);
            SettingsButton.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            SettingsButton.WindowTitles.Add("LOWIS: Connect");

            WpfWindow SettingsWin = new WpfWindow();
            SettingsWin.SearchProperties[WpfWindow.PropertyNames.Name] = "LOWIS: Settings";
            SettingsWin.SearchProperties.Add(new PropertyExpression(WpfWindow.PropertyNames.ClassName, "HwndWrapper", PropertyExpressionOperator.Contains));
            SettingsWin.WindowTitles.Add("LOWIS: Settings");

            WpfEdit CsStore = new WpfEdit(SettingsWin);
            CsStore.WindowTitles.Add("LOWIS: Settings");

            WpfButton SaveButton = new WpfButton(SettingsWin);
            SaveButton.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            SaveButton.SearchProperties[WpfButton.PropertyNames.AccessKey] = "\r";
            SaveButton.WindowTitles.Add("LOWIS: Settings");

            if (CreateNewStore == true)
            {
                Mouse.Click(SettingsButton);
                Trace.WriteLine("Clicked Settings button on client Logon");
                CsStore.Text = "C:\\csstore18";
                Trace.WriteLine("Set CsStore path");
                Mouse.Click(SaveButton);
                Trace.WriteLine("Clicked Save Button on settings window");
            }
            Mouse.Click(ConnectButton);
            Trace.WriteLine("Clicked LOWIS connect button");

        }

        public void CloseOpenGrid(string GridName)
        {
            switch (GridName)
            {
                case "BeamWellGroupConfig":
                    {
                        WinWindow ConfigWindow = new WinWindow(GetMainWindow());
                        ConfigWindow.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        //ConfigWindow.SearchProperties[WinWindow.PropertyNames.ControlId] = "203";
                        ConfigWindow.WindowTitles.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString());

                        WinControl ButtonImage = new WinControl(ConfigWindow);
                        ButtonImage.SearchProperties[UITestControl.PropertyNames.Name] = "Beam Well Group Configuration";
                        ButtonImage.SearchProperties[UITestControl.PropertyNames.ControlType] = "Image";
                        ButtonImage.WindowTitles.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString());
                        Mouse.Click(ButtonImage);
                        Trace.WriteLine("Clicked Grid " + GridName);
                        System.Threading.Thread.Sleep(2000);
                        Mouse.Click(ButtonImage);
                        Trace.WriteLine("Clicked Grid " + GridName);

                        break;
                    }
                case "MOP":
                    {
                        WinWindow ConfigWindow = new WinWindow(GetMainWindow());
                        ConfigWindow.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        //ConfigWindow.SearchProperties[WinWindow.PropertyNames.ControlId] = "202";
                        ConfigWindow.WindowTitles.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString());

                        WinControl ButtonImage = new WinControl(ConfigWindow);
                        ButtonImage.SearchProperties[UITestControl.PropertyNames.Name] = "Well Status/MOP/Type History";
                        ButtonImage.SearchProperties[UITestControl.PropertyNames.ControlType] = "Image";
                        ButtonImage.WindowTitles.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString());
                        Mouse.Click(ButtonImage);
                        Trace.WriteLine("Clicked Grid " + GridName);
                        System.Threading.Thread.Sleep(2000);
                        Mouse.Click(ButtonImage);
                        Trace.WriteLine("Clicked Grid " + GridName);

                        break;
                    }
                case "ESPGroupConfigGrid":
                    {
                        WinWindow ConfigWindow = new WinWindow(GetMainWindow());
                        ConfigWindow.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        //ConfigWindow.SearchProperties[WinWindow.PropertyNames.ControlId] = "203";
                        ConfigWindow.WindowTitles.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString());

                        WinControl ButtonImage = new WinControl(ConfigWindow);
                        ButtonImage.SearchProperties[UITestControl.PropertyNames.Name] = "ESP Well Group Configuration";
                        ButtonImage.SearchProperties[UITestControl.PropertyNames.ControlType] = "Image";
                        ButtonImage.WindowTitles.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString());
                        Mouse.Click(ButtonImage);
                        Trace.WriteLine("Clicked Grid " + GridName);
                        System.Threading.Thread.Sleep(2000);
                        Mouse.Click(ButtonImage);
                        Trace.WriteLine("Clicked Grid " + GridName);

                        break;
                    }
                case "PCPGroupConfigGrid":
                    {
                        WinWindow ConfigWindow = new WinWindow(GetMainWindow());
                        ConfigWindow.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        //ConfigWindow.SearchProperties[WinWindow.PropertyNames.ControlId] = "203";
                        ConfigWindow.WindowTitles.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString());

                        WinControl ButtonImage = new WinControl(ConfigWindow);
                        ButtonImage.SearchProperties[UITestControl.PropertyNames.Name] = "PCP Well Group Configuration";
                        ButtonImage.SearchProperties[UITestControl.PropertyNames.ControlType] = "Image";
                        ButtonImage.WindowTitles.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString());
                        Mouse.Click(ButtonImage);
                        Trace.WriteLine("Clicked Grid " + GridName);
                        System.Threading.Thread.Sleep(2000);
                        Mouse.Click(ButtonImage);
                        Trace.WriteLine("Clicked Grid " + GridName);

                        break;
                    }
                //PCPGroupConfigGrid

            }
            WinWindow ContxtMenu = new WinWindow();
            ContxtMenu.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            ContxtMenu.SearchProperties[WinWindow.PropertyNames.AccessibleName] = "Context";
            ContxtMenu.SearchProperties[WinWindow.PropertyNames.ClassName] = "#32768";

            WinMenu UiContextMenu = new WinMenu(ContxtMenu);
            UiContextMenu.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            UiContextMenu.SearchProperties[WinMenu.PropertyNames.Name] = "Context";

            WinMenuItem UiContextMenuItem = new WinMenuItem(UiContextMenu);
            UiContextMenuItem.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            UiContextMenuItem.SearchProperties[WinMenuItem.PropertyNames.Name] = "Close";

            Mouse.Click(UiContextMenuItem);
            Trace.WriteLine("Clicked Grid Close");





        }

        public string GridExport(string GridName, string WhenExported)
        {
            string Fileoutput = String.Empty;
			Trace.WriteLine("Inside GridExport method , waiting");
			System.Threading.Thread.Sleep(5000); 
            WinWindow ItemWindow = new WinWindow(GetMainWindow());
            ItemWindow.SearchProperties[WinWindow.PropertyNames.ClassName] = "#32770";
            ItemWindow.SearchProperties[WinWindow.PropertyNames.Instance] = "45";
            ItemWindow.WindowTitles.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString());

            WinControl ItemDialog = new WinControl(ItemWindow);
            ItemDialog.SearchProperties[UITestControl.PropertyNames.ControlType] = "Dialog";
            ItemDialog.WindowTitles.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString());

            WinClient Grid = new WinClient(ItemDialog);
            Grid.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            Grid.SearchProperties[WinControl.PropertyNames.Name] = "Spread Control (spr32d70)";
            Grid.SearchProperties[WinControl.PropertyNames.ClassName] = "fpSpread70";
            Grid.WindowTitles.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString());

            Grid.DrawHighlight();
            var GridPostion = Grid.BoundingRectangle;
            Point GridClick = new Point(GridPostion.X + 75, GridPostion.Y + 75);
            Mouse.Move(GridClick);
			Trace.WriteLine("Moved mouse to gridclick position");
            Mouse.Click(MouseButtons.Right);
            Trace.WriteLine("Right Clicked on Grid " + GridName);
            System.Threading.Thread.Sleep(1800);

            WinWindow ItemWin2 = new WinWindow();
            ItemWin2.SearchProperties[WinWindow.PropertyNames.AccessibleName] = "Context";
            ItemWin2.SearchProperties[WinWindow.PropertyNames.ClassName] = "#32768";

            WinMenu ContextMenu = new WinMenu(ItemWin2);
            ContextMenu.SearchProperties[WinMenu.PropertyNames.Name] = "Context";

            WinMenuItem ExportItem = new WinMenuItem(ContextMenu);
            ExportItem.SearchProperties[WinMenuItem.PropertyNames.Name] = "Export...";
            Mouse.Hover(ExportItem);
            Mouse.Click(ExportItem);
            Trace.WriteLine("Clicked Export Context Menu Button on grid " + GridName);

            switch (GridName)
            {
                case "BeamWellGroupConfig":
                    {
                        string BeamConfigExportLocation = ExportLocation;
                        string BeamConfigExportFileName = ConfigurationManager.AppSettings["BeamConfigExportFileName"];

                        WinWindow SaveBeamWellConfig = new WinWindow();
                        SaveBeamWellConfig.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "Save Beam Well Group Configuration", PropertyExpressionOperator.Contains));
                        SaveBeamWellConfig.SearchProperties[WinWindow.PropertyNames.ClassName] = "#32770";
                        SaveBeamWellConfig.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save Beam Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));

                        WinWindow ItemWindowDialog = new WinWindow(SaveBeamWellConfig);
                        ItemWindowDialog.SearchProperties[WinWindow.PropertyNames.ClassName] = "ToolbarWindow32";
                        //ItemWindowDialog.SearchProperties[WinWindow.PropertyNames.Instance] = "4";
                        ItemWindowDialog.WindowTitles.Add((new PropertyExpression(WinToolBar.PropertyNames.Name, "Save Beam Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));

                        WinToolBar LocationBar = new WinToolBar(ItemWindowDialog);
                        LocationBar.WindowTitles.Add((new PropertyExpression(WinToolBar.PropertyNames.Name, "Save Beam Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));



                        WinButton PreviousLocation = new WinButton(LocationBar);
                        PreviousLocation.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        PreviousLocation.SearchProperties[WinButton.PropertyNames.Name] = "Previous Locations";
                        PreviousLocation.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save Beam Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));


                        Mouse.Click(PreviousLocation);
                        Trace.WriteLine("Clicked Previous Location on Save Dialog ");

                        WinWindow ItemWin1 = new WinWindow(SaveBeamWellConfig);
                        ItemWin1.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        ItemWin1.SearchProperties.Add(WinWindow.PropertyNames.ControlId, "41477");
                        ItemWin1.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save Beam Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));

                        WinEdit AddressEdit = new WinEdit(ItemWin1);
                        AddressEdit.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        AddressEdit.SearchProperties.Add(WinEdit.PropertyNames.Name, "Address");
                        AddressEdit.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save Beam Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));

                        AddressEdit.Text = BeamConfigExportLocation;
                        Trace.WriteLine("Set export ouput path to " + BeamConfigExportLocation);

                        //Keyboard.SendKeys(BeamConfigExportLocation);
                        Keyboard.SendKeys("{ENTER}");
                        Trace.WriteLine("Set ENTER key");

                        WinPane DPane = new WinPane(SaveBeamWellConfig);
                        DPane.SearchProperties[WinControl.PropertyNames.Name] = "Details Pane";
                        DPane.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save Beam Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));

                        WinComboBox FileName = new WinComboBox(DPane);
                        FileName.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        FileName.SearchProperties.Add(WinComboBox.PropertyNames.Name, "File name:");
                        FileName.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save Beam Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));

                        FileName.EditableItem = BeamConfigExportFileName + WhenExported;
                        Trace.WriteLine("Set FileName of export file to " + BeamConfigExportFileName + WhenExported);
                        WinButton SaveExport = new WinButton(SaveBeamWellConfig);
                        SaveExport.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        SaveExport.SearchProperties.Add(WinButton.PropertyNames.Name, "Save");
                        SaveExport.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save Beam Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));

                        Mouse.Click(SaveExport);
                        Trace.WriteLine("Save Export button clicked");
                        Fileoutput = (BeamConfigExportLocation + BeamConfigExportFileName + WhenExported + ".txt");
                        CheckIfExportExists(Fileoutput);

                        break;
                    }
                case "MOP":
                    {
                        string MOPExportLocation = ExportLocation;
                        string MOPExportFileName = ConfigurationManager.AppSettings["MOPExportFileName"];

                        WinWindow SaveMOP = new WinWindow();
                        SaveMOP.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "Save Well Status_MOP_Type History", PropertyExpressionOperator.Contains));
                        SaveMOP.SearchProperties[WinWindow.PropertyNames.ClassName] = "#32770";
                        SaveMOP.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save Well Status_MOP_Type History", PropertyExpressionOperator.Contains).ToString()));

                        WinWindow ItemWindowDialog = new WinWindow(SaveMOP);
                        ItemWindowDialog.SearchProperties[WinWindow.PropertyNames.ClassName] = "ToolbarWindow32";
                        //ItemWindowDialog.SearchProperties[WinWindow.PropertyNames.Instance] = "4";
                        ItemWindowDialog.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save Well Status_MOP_Type History", PropertyExpressionOperator.Contains).ToString()));

                        WinToolBar LocationBar = new WinToolBar(ItemWindowDialog);
                        LocationBar.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save Well Status_MOP_Type History", PropertyExpressionOperator.Contains).ToString()));



                        WinButton PreviousLocation = new WinButton(LocationBar);
                        PreviousLocation.SearchProperties[WinButton.PropertyNames.Name] = "Previous Locations";
                        PreviousLocation.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save Well Status_MOP_Type History", PropertyExpressionOperator.Contains).ToString()));


                        Mouse.Click(PreviousLocation);
                        Trace.WriteLine("Clicked Previous Location on Save Dialog ");

                        WinWindow ItemWin1 = new WinWindow(SaveMOP);
                        ItemWin1.SearchProperties.Add(WinWindow.PropertyNames.ControlId, "41477");
                        ItemWin1.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save Well Status_MOP_Type History", PropertyExpressionOperator.Contains).ToString()));

                        WinEdit AddressEdit = new WinEdit(ItemWin1);
                        AddressEdit.SearchProperties.Add(WinEdit.PropertyNames.Name, "Address");
                        AddressEdit.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save Well Status_MOP_Type History", PropertyExpressionOperator.Contains).ToString()));

                        AddressEdit.Text = MOPExportLocation;
                        Trace.WriteLine("Set export ouput path to " + MOPExportLocation);

                        //Keyboard.SendKeys(BeamConfigExportLocation);
                        Keyboard.SendKeys("{ENTER}");
                        Trace.WriteLine("Set ENTER key");

                        WinPane DPane = new WinPane(SaveMOP);
                        DPane.SearchProperties[WinControl.PropertyNames.Name] = "Details Pane";
                        DPane.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save Well Status_MOP_Type History", PropertyExpressionOperator.Contains).ToString()));

                        WinComboBox FileName = new WinComboBox(DPane);
                        FileName.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        FileName.SearchProperties.Add(WinComboBox.PropertyNames.Name, "File name:");
                        FileName.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save Well Status_MOP_Type History", PropertyExpressionOperator.Contains).ToString()));

                        FileName.EditableItem = MOPExportFileName + WhenExported;
                        Trace.WriteLine("Set FileName of export file to " + MOPExportFileName + WhenExported);
                        WinButton SaveExport = new WinButton(SaveMOP);
                        SaveExport.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        SaveExport.SearchProperties.Add(WinButton.PropertyNames.Name, "Save");
                        SaveExport.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save Well Status_MOP_Type History", PropertyExpressionOperator.Contains).ToString()));

                        Mouse.Click(SaveExport);
                        Trace.WriteLine("Save Export button clicked");
                        Fileoutput = (MOPExportLocation + MOPExportFileName + WhenExported + ".txt");
                        CheckIfExportExists(Fileoutput);
                        break;
                    }
                case "ESPGroupConfigGrid":
                    {
                        string ESPGroupConfigGridExportLocation = ExportLocation;
                        string ESPGroupConfigGridExportFileName = ConfigurationManager.AppSettings["ESPConfigExportFileName"];

                        WinWindow SaveESPGroupConfigGrid = new WinWindow();
                        SaveESPGroupConfigGrid.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "Save ESP Well Group Configuration", PropertyExpressionOperator.Contains));
                        SaveESPGroupConfigGrid.SearchProperties[WinWindow.PropertyNames.ClassName] = "#32770";
                        SaveESPGroupConfigGrid.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save ESP Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));

                        WinWindow ItemWindowDialog = new WinWindow(SaveESPGroupConfigGrid);
                        ItemWindowDialog.SearchProperties[WinWindow.PropertyNames.ClassName] = "ToolbarWindow32";
                        //ItemWindowDialog.SearchProperties[WinWindow.PropertyNames.Instance] = "4";
                        ItemWindowDialog.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save ESP Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));

                        WinToolBar LocationBar = new WinToolBar(ItemWindowDialog);
                        LocationBar.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save ESP Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));



                        WinButton PreviousLocation = new WinButton(LocationBar);
                        PreviousLocation.SearchProperties[WinButton.PropertyNames.Name] = "Previous Locations";
                        PreviousLocation.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save ESP Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));


                        Mouse.Click(PreviousLocation);
                        Trace.WriteLine("Clicked Previous Location on Save Dialog ");

                        WinWindow ItemWin1 = new WinWindow(SaveESPGroupConfigGrid);
                        ItemWin1.SearchProperties.Add(WinWindow.PropertyNames.ControlId, "41477");
                        ItemWin1.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save ESP Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));

                        WinEdit AddressEdit = new WinEdit(ItemWin1);
                        AddressEdit.SearchProperties.Add(WinEdit.PropertyNames.Name, "Address");
                        AddressEdit.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save ESP Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));

                        AddressEdit.Text = ESPGroupConfigGridExportLocation;
                        Trace.WriteLine("Set export ouput path to " + ESPGroupConfigGridExportLocation);

                        //Keyboard.SendKeys(BeamConfigExportLocation);
                        Keyboard.SendKeys("{ENTER}");
                        Trace.WriteLine("Set ENTER key");

                        WinPane DPane = new WinPane(SaveESPGroupConfigGrid);
                        DPane.SearchProperties[WinControl.PropertyNames.Name] = "Details Pane";
                        DPane.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save ESP Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));

                        WinComboBox FileName = new WinComboBox(DPane);
                        FileName.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        FileName.SearchProperties.Add(WinComboBox.PropertyNames.Name, "File name:");
                        FileName.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save ESP Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));

                        FileName.EditableItem = ESPGroupConfigGridExportFileName + WhenExported;
                        Trace.WriteLine("Set FileName of export file to " + ESPGroupConfigGridExportFileName + WhenExported);
                        WinButton SaveExport = new WinButton(SaveESPGroupConfigGrid);
                        SaveExport.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        SaveExport.SearchProperties.Add(WinButton.PropertyNames.Name, "Save");
                        SaveExport.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save ESP Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));

                        Mouse.Click(SaveExport);
                        Trace.WriteLine("Save Export button clicked");
                        Fileoutput = (ESPGroupConfigGridExportLocation + ESPGroupConfigGridExportFileName + WhenExported + ".txt");
                        CheckIfExportExists(Fileoutput);
                        break;
                    }
                case "PCPGroupConfigGrid":
                    {
                        string PCPGroupConfigGridExportLocation = ExportLocation;
                        string PCPGroupConfigGridExportFileName = ConfigurationManager.AppSettings["PCPConfigExportFileName"];

                        WinWindow SavePCPGroupConfigGrid = new WinWindow();
                        SavePCPGroupConfigGrid.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "Save PCP Well Group Configuration", PropertyExpressionOperator.Contains));
                        SavePCPGroupConfigGrid.SearchProperties[WinWindow.PropertyNames.ClassName] = "#32770";
                        SavePCPGroupConfigGrid.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save PCP Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));

                        WinWindow ItemWindowDialog = new WinWindow(SavePCPGroupConfigGrid);
                        ItemWindowDialog.SearchProperties[WinWindow.PropertyNames.ClassName] = "ToolbarWindow32";
                        //ItemWindowDialog.SearchProperties[WinWindow.PropertyNames.Instance] = "4";
                        ItemWindowDialog.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save PCP Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));

                        WinToolBar LocationBar = new WinToolBar(ItemWindowDialog);
                        LocationBar.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save PCP Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));



                        WinButton PreviousLocation = new WinButton(LocationBar);
                        PreviousLocation.SearchProperties[WinButton.PropertyNames.Name] = "Previous Locations";
                        PreviousLocation.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save PCP Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));


                        Mouse.Click(PreviousLocation);
                        Trace.WriteLine("Clicked Previous Location on Save Dialog ");

                        WinWindow ItemWin1 = new WinWindow(SavePCPGroupConfigGrid);
                        ItemWin1.SearchProperties.Add(WinWindow.PropertyNames.ControlId, "41477");
                        ItemWin1.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save PCP Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));

                        WinEdit AddressEdit = new WinEdit(ItemWin1);
                        AddressEdit.SearchProperties.Add(WinEdit.PropertyNames.Name, "Address");
                        AddressEdit.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save PCP Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));

                        AddressEdit.Text = PCPGroupConfigGridExportLocation;
                        Trace.WriteLine("Set export ouput path to " + PCPGroupConfigGridExportLocation);

                        //Keyboard.SendKeys(BeamConfigExportLocation);
                        Keyboard.SendKeys("{ENTER}");
                        Trace.WriteLine("Set ENTER key");

                        WinPane DPane = new WinPane(SavePCPGroupConfigGrid);
                        DPane.SearchProperties[WinControl.PropertyNames.Name] = "Details Pane";
                        DPane.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save PCP Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));

                        WinComboBox FileName = new WinComboBox(DPane);
                        FileName.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        FileName.SearchProperties.Add(WinComboBox.PropertyNames.Name, "File name:");
                        FileName.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save PCP Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));

                        FileName.EditableItem = PCPGroupConfigGridExportFileName + WhenExported;
                        Trace.WriteLine("Set FileName of export file to " + PCPGroupConfigGridExportFileName + WhenExported);
                        WinButton SaveExport = new WinButton(SavePCPGroupConfigGrid);
                        SaveExport.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        SaveExport.SearchProperties.Add(WinButton.PropertyNames.Name, "Save");
                        SaveExport.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Save PCP Well Group Configuration", PropertyExpressionOperator.Contains).ToString()));

                        Mouse.Click(SaveExport);
                        Trace.WriteLine("Save Export button clicked");
                        Fileoutput = (PCPGroupConfigGridExportLocation + PCPGroupConfigGridExportFileName + WhenExported + ".txt");
                        CheckIfExportExists(Fileoutput);
                        break;
                    }
                //PCPGroupConfigGrid
            }

            return Fileoutput;

        }

        protected void TestConfigOutputFiles(String fileName1, string WellName)
        {

            string BeamConfigExportFileName = ConfigurationManager.AppSettings.Get("BeamConfigExportFileName");
            string MOPExportFileName = ConfigurationManager.AppSettings.Get("MOPExportFileName");
            string ESPConfigExportFileName = ConfigurationManager.AppSettings.Get("ESPConfigExportFileName");
            string PCPConfigExportFileName = ConfigurationManager.AppSettings.Get("PCPConfigExportFileName");


            if (!File.Exists(fileName1))
            {
                Trace.WriteLine("File not found: " + (fileName1));
                Assert.Fail("Test OutputFiles Failure");
            }

            DirectoryInfo TestFile = new DirectoryInfo(fileName1);
            string TestFileName = TestFile.Name;
            string[] scanA = File.ReadAllLines((fileName1));



            if (scanA.Length < 1)
            {
                Trace.WriteLine("ERROR: Output File #1 is empty.");
                Assert.Fail("Test OutputFiles Failure");
            }

            foreach (string line in scanA)
            {
                if ((TestFileName == BeamConfigExportFileName + ".txt"))
                {
                    if ((WellName.StartsWith("AE2")) && line.Contains(WellName))
                    {
                        Trace.WriteLine("\nLine found in " + BeamConfigExportFileName + ".txt" + "    ---     " + line);
                        Trace.WriteLine("\nStarting Test validation of file");

                        char[] delimiter1 = new char[] { '\t' };
                        string[] things = line.Split(delimiter1, StringSplitOptions.RemoveEmptyEntries);
                        Assert.IsTrue(things[0] == WellName, "Element 0 is not WellName and it should be"); //LWName
                        Assert.IsTrue(things[1] == WellName, "Element 1 is not WellName and it should be"); //NavName
                        Assert.IsTrue(things[2] == "API10Name", "Element 2 is not API10Name and it should be"); //API10
                        Assert.IsTrue(things[3] == "API12Name", "Element 3 is not API12Name and it should be"); //API12
                        Assert.IsTrue(things[4] == "API14Name", "Element 4 is not API14Name and it should be"); //API14
                        Assert.IsTrue(things[5] == WellName, "Element 5 is not WellName and it should be"); //CAOID
                        Assert.IsTrue(things[6] == WellName, "Element 6 is not WellName and it should be"); //Long Name
                        Assert.IsTrue(things[7] == "Group1", "Element 7 is not Group1 and it should be"); //Group1
                        Assert.IsTrue(things[8] == "Group2", "Element 8 is not Group2 and it should be"); //Group2
                        Assert.IsTrue(things[9] == "Group3", "Element 9 is not Group3 and it should be"); //Group3
                        Assert.IsTrue(things[10] == "Group4", "Element 10 is not Group4 and it should be"); //Group4
                        Assert.IsTrue(things[11] == "BeamFac", "Element 11 is not BeamFac and it should be"); //FacDescription
                        Assert.IsTrue((things[12] == "1") || (things[12] == "1.00"), "Element 12 is not 1 or 1.00 and it should be"); //Xcoord
                        Assert.IsTrue((things[13] == "2") || (things[13] == "2.00"), "Element 13 is not 2 or 2.00 and it should be"); //Ycoord
                        Assert.IsTrue(things[14] == "AE RPC Ver 22+", "Element 14 is not AE RPC Ver 22+ and it should be"); //Controller
                        Assert.IsTrue(things[15] == "COM2", "Element 15 is not COM2 and it should be"); //COM  Port
                        Assert.IsTrue(things[16] == "AUTOCOMM", "Element 16 is not AUTOCOMM and it should be"); //Protocol
                        Assert.IsTrue(things[17] == "41", "Element 17 is not 41 and it should be"); //Rtu Address
                        Assert.IsTrue(things[18] == "127.0.0.1/10000", "Element 18 is not 127.0.0.1/10000 and it should be"); //Alt Address
                        Assert.IsTrue(things[19] == "2", "Element 19 is not 2 and it should be"); //COM Retries
                        Assert.IsTrue(things[20] == "99", "Element 20 is not 99 and it should be"); //KEYON Delay
                        Assert.IsTrue(things[21] == "98", "Element 21 is not 98 and it should be"); //KEYOF Delay
                        Assert.IsTrue(things[22] == "31", "Element 22 is not 31 and it should be"); //ALARM Delay 
                        Assert.IsTrue(things[23] == "30", "Element 23 is not 30 and it should be"); //CLR Dealay
                        Trace.WriteLine("\nTest validation of file COMPLETE! Information is correct");

                    }


                }

                if ((TestFileName == ESPConfigExportFileName + ".txt"))
                {
                    if ((WellName.StartsWith("GVFD")) && line.Contains(WellName))
                    {
                        Trace.WriteLine("\nLine found in " + ESPConfigExportFileName + ".txt" + "    ---     " + line);
                        Trace.WriteLine("\nStarting Test validation of file");
                        char[] delimiter1 = new char[] { '\t' };
                        string[] things = line.Split(delimiter1, StringSplitOptions.RemoveEmptyEntries);

                        Assert.IsTrue(things[0] == WellName, "Element 0 is not WellName and it should be"); //LWName
                        Assert.IsTrue(things[1] == WellName, "Element 1 is not WellName and it should be"); //NavName
                        Assert.IsTrue(things[2] == "API10Name", "Element 2 is not API10Name and it should be"); //API10
                        Assert.IsTrue(things[3] == "API12Name", "Element 3 is not API12Name and it should be"); //API12
                        Assert.IsTrue(things[4] == "API14Name", "Element 4 is not API14Name and it should be"); //API14

                        Assert.IsTrue(things[5] == "Group1", "Element 5 is not Group1 and it should be"); //Group1
                        Assert.IsTrue(things[6] == "Group2", "Element 6 is not Group2 and it should be"); //Group2
                        Assert.IsTrue(things[7] == "Group3", "Element 7 is not Group3 and it should be"); //Group3
                        Assert.IsTrue(things[8] == "Group4", "Element 8 is not Group4 and it should be"); //Group4

                        Assert.IsTrue((things[9] == "1") || (things[9] == "1.00"), "Element 9 is not 1 or 1.00 and it should be"); //Xcoord
                        Assert.IsTrue((things[10] == "2") || (things[10] == "2.00"), "Element 10 is not 2 or 2.00 and it should be"); //Ycoord
                        Assert.IsTrue(things[11] == "GCS Electrospd VFD", "Element 11 is not GCS Electrospd VFD and it should be"); //Controller
                        Assert.IsTrue(things[12] == "MODBUS", "Element 12 is not MODBUS and it should be"); //Protocol
                        Assert.IsTrue(things[13] == "COM2", "Element 13 is not COM2 and it should be"); //COM Port
                        Assert.IsTrue(things[14] == "123", "Element 14 is not 123 and it should be"); //Rtu Address
                        Assert.IsTrue(things[15] == "127.0.0.1/10000", "Element 15 is not 127.0.0.1/10000 and it should be"); //Alt Address
                        Assert.IsTrue(things[16] == "2", "Element 16 is not 2 and it should be"); //COM Retries
                        Assert.IsTrue(things[17] == "99", "Element 17 is not 99 and it should be"); //KEYON Delay
                        Assert.IsTrue(things[18] == "98", "Element 18 is not 98 and it should be"); //KEYOF Delay
                        Assert.IsTrue(things[19] == "31", "Element 19 is not 31 and it should be"); //ALARM Delay 
                        Assert.IsTrue(things[20] == "30", "Element 20 is not 30 and it should be"); //CLR Dealay
                        Trace.WriteLine("\nTest validation of file COMPLETE! Information is correct");


                    }


                }


                if ((TestFileName == BeamConfigExportFileName + "After" + ".txt"))
                {
                    if ((WellName.StartsWith("AE2")) && line.Contains(WellName))
                    {
                        Assert.Fail(line);
                    }
                    else if ((WellName.StartsWith("GVFD")) && line.Contains(WellName))
                    {
                        Trace.WriteLine("\nLine found in " + BeamConfigExportFileName + "After" + ".txt" + "    ---     " + line);
                        Trace.WriteLine("\nStarting Test validation of file");
                        char[] delimiter1 = new char[] { '\t' };
                        string[] things = line.Split(delimiter1, StringSplitOptions.RemoveEmptyEntries);

                        Assert.IsTrue(things[0] == WellName, "Element 0 is not WellName and it should be"); //LWName
                        Assert.IsTrue(things[1] == WellName, "Element 1 is not WellName and it should be"); //NavName
                        Assert.IsTrue(things[2] == "API10Name", "Element 2 is not API10Name and it should be"); //API10
                        Assert.IsTrue(things[3] == "API12Name", "Element 3 is not API12Name and it should be"); //API12
                        Assert.IsTrue(things[4] == "API14Name", "Element 4 is not API14Name and it should be"); //API14
                        Assert.IsTrue(things[5] == WellName, "Element 5 is not WellName and it should be"); //CAOID
                        Assert.IsTrue(things[6] == WellName, "Element 6 is not WellName and it should be"); //Long Name
                        Assert.IsTrue(things[7] == "Group1", "Element 7 is not Group1 and it should be"); //Group1
                        Assert.IsTrue(things[8] == "Group2", "Element 8 is not Group2 and it should be"); //Group2
                        Assert.IsTrue(things[9] == "Group3", "Element 9 is not Group3 and it should be"); //Group3
                        Assert.IsTrue(things[10] == "Group4", "Element 10 is not Group4 and it should be"); //Group4
                        Assert.IsTrue(things[11] == "ESPFac", "Element 11 is not ESPFac and it should be"); //FacDescription
                        Assert.IsTrue((things[12] == "1") || (things[12] == "1.00"), "Element 12 is not 1 or 1.00 and it should be"); //Xcoord
                        Assert.IsTrue((things[13] == "2") || (things[13] == "2.00"), "Element 13 is not 2 or 2.00 and it should be"); //Ycoord
                        Assert.IsTrue(things[14] == "None", "Element 14 is not None and it should be"); //Controller
                        Assert.IsTrue(things[15] == "COM2", "Element 15 is not COM2 and it should be"); //COM  Port
                        Assert.IsTrue(things[16] == "NONE", "Element 16 is not NONE and it should be"); //Protocol
                        Assert.IsTrue(things[17] == "123", "Element 17 is not 123 and it should be"); //Rtu Address
                        Assert.IsTrue(things[18] == "127.0.0.1/10000", "Element 18 is not 127.0.0.1/10000 and it should be"); //Alt Address
                        Assert.IsTrue(things[19] == "2", "Element 19 is not 2 and it should be"); //COM Retries

                        //Assert.IsTrue(things[20] == "99", "Element 20 is not 99 and it should be"); //KEYON Delay
                        //Assert.IsTrue(things[21] == "98", "Element 21 is not 98 and it should be"); //KEYOF Delay
                        //Assert.IsTrue(things[22] == "31", "Element 22 is not 31 and it should be"); //ALARM Delay 
                        //Assert.IsTrue(things[23] == "30", "Element 23 is not 30 and it should be"); //CLR Dealay 
                        Trace.WriteLine("\nTest validation of file COMPLETE! Information is correct");

                    }

                }

                if ((TestFileName == ESPConfigExportFileName + "After" + ".txt"))
                {
                    if ((WellName.StartsWith("GVFD")) && line.Contains(WellName))
                    {
                        Assert.Fail(line);
                    }
                    else if ((WellName.StartsWith("AE2")) && line.Contains(WellName))
                    {
                        Trace.WriteLine("\nLine found in " + ESPConfigExportFileName + "After" + ".txt" + "    ---     " + line);
                        Trace.WriteLine("\nStarting Test validation of file");
                        char[] delimiter1 = new char[] { '\t' };
                        string[] things = line.Split(delimiter1, StringSplitOptions.RemoveEmptyEntries);

                        Assert.IsTrue(things[0] == WellName, "Element 0 is not WellName and it should be"); //LWName
                        Assert.IsTrue(things[1] == WellName, "Element 1 is not WellName and it should be"); //NavName
                        Assert.IsTrue(things[2] == "API10Name", "Element 2 is not API10Name and it should be"); //API10
                        Assert.IsTrue(things[3] == "API12Name", "Element 3 is not API12Name and it should be"); //API12
                        Assert.IsTrue(things[4] == "API14Name", "Element 4 is not API14Name and it should be"); //API14

                        Assert.IsTrue(things[5] == "Group1", "Element 5 is not Group1 and it should be"); //Group1
                        Assert.IsTrue(things[6] == "Group2", "Element 6 is not Group2 and it should be"); //Group2
                        Assert.IsTrue(things[7] == "Group3", "Element 7 is not Group3 and it should be"); //Group3
                        Assert.IsTrue(things[8] == "Group4", "Element 8 is not Group4 and it should be"); //Group4

                        Assert.IsTrue((things[9] == "1") || (things[9] == "1.00"), "Element 9 is not 1 or 1.00 and it should be"); //Xcoord
                        Assert.IsTrue((things[10] == "2") || (things[10] == "2.00"), "Element 10 is not 2 or 2.00 and it should be"); //Ycoord
                        Assert.IsTrue(things[11] == "None", "Element 11 is not None and it should be"); //Controller
                        Assert.IsTrue(things[12] == "NONE", "Element 12 is not NONE and it should be"); //Protocol
                        Assert.IsTrue(things[13] == "COM2", "Element 13 is not COM2 and it should be"); //COM Port
                        Assert.IsTrue(things[14] == "41", "Element 14 is not 41 and it should be"); //Rtu Address
                        Assert.IsTrue(things[15] == "127.0.0.1/10000", "Element 15 is not 127.0.0.1/10000 and it should be"); //Alt Address
                        Assert.IsTrue(things[16] == "2", "Element 16 is not 2 and it should be"); //COM Retries

                        //Dont Persist on MOP change
                        //Assert.IsTrue(things[17] == "99", "Element 20 is not 99 and it should be"); //KEYON Delay
                        //Assert.IsTrue(things[18] == "98", "Element 21 is not 98 and it should be"); //KEYOF Delay
                        //Assert.IsTrue(things[19] == "31", "Element 22 is not 31 and it should be"); //ALARM Delay 
                        //Assert.IsTrue(things[20] == "30", "Element 23 is not 30 and it should be"); //CLR Dealay
                        Trace.WriteLine("\nTest validation of file COMPLETE! Information is correct");


                    }

                }

                if ((TestFileName == PCPConfigExportFileName + "After" + ".txt"))
                {
                    if ((WellName.StartsWith("YSK")) && line.Contains(WellName))
                    {
                        Assert.Fail(line);
                    }
                    else if ((WellName.StartsWith("AE2")) && line.Contains(WellName))
                    {
                        Trace.WriteLine("\nLine found in " + PCPConfigExportFileName + "After" + ".txt" + "    ---     " + line);
                        Trace.WriteLine("\nStarting Test validation of file");
                        char[] delimiter1 = new char[] { '\t' };
                        string[] things = line.Split(delimiter1, StringSplitOptions.RemoveEmptyEntries);

                        Assert.IsTrue(things[0] == WellName, "Element 0 is not WellName and it should be"); //LWName
                        Assert.IsTrue(things[1] == WellName, "Element 1 is not WellName and it should be"); //NavName
                        Assert.IsTrue(things[2] == "API10Name", "Element 2 is not API10Name and it should be"); //API10
                        Assert.IsTrue(things[3] == "API12Name", "Element 3 is not API12Name and it should be"); //API12
                        Assert.IsTrue(things[4] == "API14Name", "Element 4 is not API14Name and it should be"); //API14
                        Assert.IsTrue(things[5] == WellName, "Element 5 is not WellName and it should be"); //LWName
                        Assert.IsTrue(things[6] == WellName, "Element 6 is not WellName and it should be"); //NavName

                        Assert.IsTrue(things[7] == "Group1", "Element 7 is not Group1 and it should be"); //Group1
                        Assert.IsTrue(things[8] == "Group2", "Element 8 is not Group2 and it should be"); //Group2
                        Assert.IsTrue(things[9] == "Group3", "Element 9 is not Group3 and it should be"); //Group3
                        Assert.IsTrue(things[10] == "Group4", "Element 10 is not Group4 and it should be"); //Group4

                        //Assert.IsTrue((things[9] == "1") || (things[12] == "1.00"), "Element 12 is not 1 or 1.00 and it should be"); // PCP not Coord on grid //Xcoord
                        //Assert.IsTrue((things[10] == "2") || (things[13] == "2.00"), "Element 13 is not 2 or 2.00 and it should be");   // PCP not Coord on grid //Ycoord
                        Assert.IsTrue(things[11] == "None", "Element 11 is not None and it should be"); //Controller
                        Assert.IsTrue(things[12] == "NONE", "Element 12 is not NONE and it should be"); //Protocol
                        //Assert.IsTrue(things[13] == "COM2", "Element 16 is not COM2 and it should be"); // No COM Port on Grid, check BFile //COM Port 
                        Assert.IsTrue(things[13] == "41", "Element 13 is not 41 and it should be"); //Rtu Address
                        Assert.IsTrue(things[14] == "127.0.0.1/10000", "Element 14 is not 127.0.0.1/10000 and it should be"); //Alt Address
                        Assert.IsTrue(things[15] == "2", "Element 15 is not 2 and it should be"); //COM Retries

                        //Dont Persist after MOP changes
                        //Assert.IsTrue(things[17] == "99", "Element 20 is not 99 and it should be"); //KEYON Delay
                        //Assert.IsTrue(things[18] == "98", "Element 21 is not 98 and it should be"); //KEYOF Delay
                        //Assert.IsTrue(things[19] == "31", "Element 22 is not 31 and it should be"); //ALARM Delay 
                        //Assert.IsTrue(things[20] == "30", "Element 23 is not 30 and it should be"); //CLR Dealay
                        Trace.WriteLine("\nTest validation of file COMPLETE! Information is correct");

                    }

                }





            }





        }

        protected void TestMOPOutputFiles(String fileName1, string WellName)
        {
            string BeamConfigExportFileName = ConfigurationManager.AppSettings.Get("BeamConfigExportFileName");
            string MOPExportFileName = ConfigurationManager.AppSettings.Get("MOPExportFileName");
            string ESPConfigExportFileName = ConfigurationManager.AppSettings.Get("ESPConfigExportFileName");



            if (!File.Exists(fileName1))
            {
                Trace.WriteLine("File not found: " + (fileName1));
                Assert.Fail("Test OutputFiles Failure");
            }

            DirectoryInfo TestFile = new DirectoryInfo(fileName1);
            string TestFileName = TestFile.Name;
            string[] scanA = File.ReadAllLines((fileName1));

            Trace.WriteLine("\n\n");


            if (scanA.Length < 1)
            {
                Trace.WriteLine("ERROR: Output File #1 is empty.");
                Assert.Fail("Test OutputFiles Failure");
            }

            if ((TestFileName == MOPExportFileName + ".txt"))
            {
                if ((WellName.StartsWith("GVFD")))
                {
                    Assert.IsTrue(scanA.Length == 2);
                    string thing = scanA.Last();
                    Trace.WriteLine("Line found " + "   ----  " + thing);
                    Trace.WriteLine("Checking found line for correct information...");
                    char[] delimiter1 = new char[] { '\t' };
                    string[] things = thing.Split(delimiter1, StringSplitOptions.RemoveEmptyEntries);
                    //Electrical Submersible Pump
                    times.Add(Convert.ToInt32(things[1].Split(':')[1]));
                    Assert.IsTrue(things[2] == "ACTIVE", "Status is not ACTIVE");
                    Assert.IsTrue(things[3] == "Electrical Submersible Pump", "MOP is not Electrical Submersible Pump");
                    Assert.IsTrue(things[4] == "OIL", "Type is not OIL");
                    Assert.IsTrue(things[5].Contains("Initial Record"));
                    Trace.WriteLine("Information is Correct...");

                }
                else if ((WellName.StartsWith("AE2")))
                {
                    Assert.IsTrue(scanA.Length == 2);
                    string thing = scanA.Last();
                    Trace.WriteLine("Line found " + "   ----  " + thing);
                    Trace.WriteLine("Checking found line for correct information...");
                    char[] delimiter1 = new char[] { '\t' };
                    string[] things = thing.Split(delimiter1, StringSplitOptions.RemoveEmptyEntries);
                    times.Add(Convert.ToInt32(things[1].Split(':')[1]));
                    Assert.IsTrue(things[2] == "ACTIVE", "Status is not ACTIVE");
                    Assert.IsTrue(things[3] == "Beam Pumping", "MOP is not BeamPumping");
                    Assert.IsTrue(things[4] == "OIL", "Type is not OIL");
                    Assert.IsTrue(things[5].Contains("Initial Record"));
                    Trace.WriteLine("Information is Correct...");

                    //do work
                }


            }

            if ((TestFileName == MOPExportFileName + "After" + ".txt"))
            {
                if ((WellName.StartsWith("GVFD")))
                {
                    Assert.IsTrue(scanA.Length == 3);
                    string thing = scanA[1];
                    Trace.WriteLine("Line found " + "   ----  " + thing);
                    Trace.WriteLine("Checking found line for correct information...");
                    char[] delimiter1 = new char[] { '\t' };
                    string[] things = thing.Split(delimiter1, StringSplitOptions.RemoveEmptyEntries);
                    //Electrical Submersible Pump
                    times.Add(Convert.ToInt32(things[1].Split(':')[1]));
                    Assert.IsTrue(things[2] == "ACTIVE", "Status is not ACTIVE");
                    Assert.IsTrue(things[3] == "Beam Pumping", "MOP is not changed to Beam Pumping");
                    Assert.IsTrue(things[4] == "OIL", "Type is not OIL");
                    Assert.IsTrue(things[5].Contains("Changing MOP"));
                    Trace.WriteLine("Information is Correct...");

                }
                else if ((WellName.StartsWith("AE2")))
                {
                    Assert.IsTrue(scanA.Length == 3);
                    string thing = scanA[1];
                    Trace.WriteLine("Line found " + "   ----  " + thing);
                    Trace.WriteLine("Checking found line for correct information...");
                    char[] delimiter1 = new char[] { '\t' };
                    string[] things = thing.Split(delimiter1, StringSplitOptions.RemoveEmptyEntries);
                    times.Add(Convert.ToInt32(things[1].Split(':')[1]));
                    Assert.IsTrue((things[2] == "ACTIVE"), "Status is not ACTIVE");
                    Assert.IsTrue((things[3] == "Electrical Submersible Pump") || (things[3] == "Progressive Cavity Pump"), "MOP is not changed to Progressive Cavity Pump OR Electrical Submersible Pump Check output file");
                    Assert.IsTrue((things[4] == "OIL"), "Type is not OIL");
                    Assert.IsTrue((things[5].Contains("Changing MOP")), "Comment is not correct");
                    Trace.WriteLine("Information is Correct...");

                    //do work
                }

            }



        }

        protected bool TestCompareWellTestTrendFiles(String fileName1, String fileName2)
        {
            bool bEqual = true;


            if (!File.Exists(fileName1))
            {
                Trace.WriteLine("File not found: " + (fileName1));
                return false;
            }

            if (!File.Exists(fileName2))
            {
                Trace.WriteLine("File not found: " + (fileName2));
                return false;
            }

            string[] scanA = File.ReadAllLines(fileName1);
            string[] scanB = File.ReadAllLines(fileName2);

            Trace.WriteLine("\n\n");
            Trace.WriteLine("##### START OF FILE COMPARE #####");
            Trace.WriteLine("Comparing " + fileName1 + " (length " + scanA.Length + ")" + " to " + fileName2 + " (length " + scanB.Length + ")");

            if (scanA.Length < 1)
            {
                Assert.Fail("ERROR: Output File #1 is empty.");
                return false;
            }

            if (scanB.Length < 1)
            {
                Assert.Fail("ERROR: Output File #2 is empty.");
                return false;
            }

            if (scanA.Length != scanB.Length)
            {
                Assert.Fail("Error: Length of both scan files do not match!");
                return false;
            }

            for (int i = 0; i < scanA.Length - 1; i++)
            {
                if (!scanA[i].Equals(scanB[i]))
                {
                    Assert.Fail("MISMATCH FOUND! " + "\"" + scanA[i] + "\"" + " does not match " + "\"" + scanB[i] + "\"");
                    bEqual = false;
                }
                else
                {
                    Trace.WriteLine(scanA[i] + " = " + scanB[i]);
                }
            }

            Trace.WriteLine("##### END OF FILE COMPARE #####\n\n");



            return bEqual;

        }



        //Controls
        public WinWindow GetMainWindow()
        {
            WinWindow MainWindow = new WinWindow();
            MainWindow.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            MainWindow.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.ClassName, "WindowsForms10.Window", PropertyExpressionOperator.Contains));
            MainWindow.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains));
            return MainWindow;
        }

        public WinWindow GetMainRefreshButton()
        {
            WinWindow RefWin = new WinWindow(GetMainWindow());
            RefWin.SearchProperties[WinWindow.PropertyNames.ControlId] = "201";
            RefWin.SearchProperties[WinWindow.PropertyNames.Instance] = "3";
            RefWin.WindowTitles.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString());


            WinWindow RefWin2 = new WinWindow(RefWin);
            RefWin2.SearchProperties[WinWindow.PropertyNames.ClassName] = "#32770";
            RefWin2.WindowTitles.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString());

            return RefWin2;

        }

        public WinWindow GetItemWellGrouping()
        {
            WinWindow ItemWindowGrouping = new WinWindow(GetMainWindow());
            ItemWindowGrouping.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            ItemWindowGrouping.SearchProperties.Add(WinWindow.PropertyNames.ClassName, "SysTreeView32");
            return ItemWindowGrouping;
        }

        public WinWindow GetItemWindowWellConditions()
        {
            WinWindow ItemWindowWellCond = new WinWindow(GetMainWindow());
            ItemWindowWellCond.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            ItemWindowWellCond.SearchProperties.Add(WinWindow.PropertyNames.Instance, "2");
            ItemWindowWellCond.SearchProperties.Add(WinWindow.PropertyNames.ClassName, "SysTreeView32");

            return ItemWindowWellCond;
        }

        public WinTree GetTreeView(string List)
        {
            WinTree TreeViewToUse = new WinTree();
            if (List == "WellGrouping")
            {
                WinTree LTreeView = new WinTree(GetItemWellGrouping());
                LTreeView.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                LTreeView.SearchProperties.Add(WinTree.PropertyNames.ClassName, "SysTreeView32");
                TreeViewToUse = LTreeView;
            }
            else if (List == "WellConditions")
            {
                WinTree LTreeView = new WinTree(GetItemWindowWellConditions());
                LTreeView.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                LTreeView.SearchProperties.Add(WinTree.PropertyNames.ClassName, "SysTreeView32");
                TreeViewToUse = LTreeView;

            }
            return TreeViewToUse;

        }

        public WinCheckBoxTreeItem GetAllCheckbox(string List)
        {
            WinCheckBoxTreeItem AllCheckBox = new WinCheckBoxTreeItem(GetTreeView(List));
            AllCheckBox.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            AllCheckBox.SearchProperties.Add(WinCheckBoxTreeItem.PropertyNames.Name, "All");
            AllCheckBox.SearchProperties["Value"] = "0";
            AllCheckBox.Expanded = true;
            Trace.WriteLine("Expanded All checkbox for " + List);
            System.Threading.Thread.Sleep(1000);
            //Mouse.DoubleClick(AllCheckBox);


            WinCheckBoxTreeItem AllCheckBox2 = new WinCheckBoxTreeItem(AllCheckBox);
            AllCheckBox2.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            AllCheckBox2.SearchProperties[WinCheckBoxTreeItem.PropertyNames.Name] = "All Wells";
            AllCheckBox2.SearchProperties["Value"] = "1";
            AllCheckBox2.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
            AllCheckBox2.SearchConfigurations.Add(SearchConfiguration.NextSibling);
            AllCheckBox2.WindowTitles.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString());


            return AllCheckBox2;
        }

        public UITestControl GetAllWellNavigatorButton(string ControlName)
        {
            WinWindow AllWellNavigator = new WinWindow(GetMainWindow());
            AllWellNavigator.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            AllWellNavigator.SearchProperties.Add(WinWindow.PropertyNames.ClassName, "ToolbarWindow32");
            AllWellNavigator.SearchProperties[WinWindow.PropertyNames.Instance] = "5";
            AllWellNavigator.WindowTitles.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString());

            WinToolBar AllWellNavToolBar = new WinToolBar(AllWellNavigator);
            AllWellNavToolBar.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            AllWellNavToolBar.SearchProperties.Add(WinToolBar.PropertyNames.ClassName, "ToolbarWindow32");
            AllWellNavToolBar.WindowTitles.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString());
            AllWellNavToolBar.SearchProperties.Add(WinToolBar.PropertyNames.ControlId, "0");

            UITestControlCollection ToolBarRefreshButton = AllWellNavToolBar.GetChildren();
            UITestControl mybar = ToolBarRefreshButton[3];
            UITestControlCollection mycol = mybar.GetChildren();   //6 is Favorites 7 Is Search Well
            UITestControl ReturnThis = new UITestControl();
            switch (ControlName)
            {
                case "Refresh":
                    {
                        ReturnThis = mycol[2];
                        break;
                    }
                case "Favorites":
                    {
                        ReturnThis = mycol[6];
                        break;
                    }
                case "SearchWell":
                    {
                        ReturnThis = mycol[7];
                        break;
                    }
            }

            return ReturnThis;
        }

        public UITestControl SearchWellWinControls(string ControlName)
        {
            WinWindow SearchWellWin = new WinWindow();
            SearchWellWin.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            SearchWellWin.SearchProperties.Add(WinWindow.PropertyNames.ClassName, "#32770");
            SearchWellWin.SearchProperties.Add(WinWindow.PropertyNames.Name, "Search Well");

            WinWindow SearchWellWinEdit = new WinWindow(SearchWellWin);
            SearchWellWinEdit.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            SearchWellWinEdit.SearchProperties.Add(WinWindow.PropertyNames.ControlId, "1007");

            WinWindow OKButtonWindow = new WinWindow(SearchWellWin);
            OKButtonWindow.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            OKButtonWindow.SearchProperties.Add(WinWindow.PropertyNames.ControlId, "1");

            //IEnumerable<WinButton> Test = OKButtonWindow.GetChildren().OfType<WinButton>();


            UITestControl ReturnThis = new UITestControl();

            switch (ControlName)
            {
                case "SearchTxtBox":
                    {
                        ReturnThis = SearchWellWinEdit.GetChildren().OfType<WinEdit>().First();
                        break;
                    }
                case "OKButton":
                    {
                        WinButton OKButton = new WinButton(OKButtonWindow);
                        OKButton.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        OKButton.SearchProperties.Add(WinWindow.PropertyNames.Name, "OK");
                        ReturnThis = OKButton;
                        break;
                    }

            }

            return ReturnThis;



        }

        public WinButton GetAnalysisWorkflowButton()
        {
            WinWindow AnalysisWorkflowWin = new WinWindow(GetMainWindow());
            AnalysisWorkflowWin.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            AnalysisWorkflowWin.SearchProperties.Add(WinWindow.PropertyNames.ControlName, "Analysis");
            AnalysisWorkflowWin.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));

            WinButton AnalysisWorkflowButton = new WinButton(AnalysisWorkflowWin);
            AnalysisWorkflowButton.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            AnalysisWorkflowButton.SearchProperties.Add(WinButton.PropertyNames.Name, "Analysis");
            AnalysisWorkflowButton.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));

            return AnalysisWorkflowButton;
        }

        public WinControl GetStartButton()
        {
            WinWindow StartWindow = new WinWindow(GetMainWindow());
            StartWindow.SearchProperties[WinWindow.PropertyNames.ControlId] = "201";
            StartWindow.SearchProperties[WinWindow.PropertyNames.Instance] = "2";
            StartWindow.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));

            WinControl StartButton = new WinControl(StartWindow);
            StartButton.SearchProperties[UITestControl.PropertyNames.Name] = "Start";
            StartButton.SearchProperties[UITestControl.PropertyNames.ControlType] = "Image";
            StartButton.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));
            return StartButton;
        }

        public WinMenuItem GetConfigurationItem(string GridName)
        {
            WinWindow ItemWin = new WinWindow();
            ItemWin.SearchProperties[WinWindow.PropertyNames.AccessibleName] = "Context";
            ItemWin.SearchProperties[WinWindow.PropertyNames.ClassName] = "#32768";

            WinMenuItem Configuration = new WinMenuItem(ItemWin);
            Configuration.SearchProperties[WinMenuItem.PropertyNames.Name] = ".Configuration";
            WinMenuItem ReturnThis = new WinMenuItem();

            switch (GridName)
            {
                case "BeamWellGroupConfig":
                    {
                        WinMenuItem BeamWellGroupConfig = new WinMenuItem(Configuration);
                        BeamWellGroupConfig.SearchProperties[WinMenuItem.PropertyNames.Name] = "Beam Well Group Configuration";
                        BeamWellGroupConfig.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
                        ReturnThis = BeamWellGroupConfig;
                        break;
                    }
                case "AnalogConfiguration":
                    {
                        WinMenuItem AnalogConfiguration = new WinMenuItem(Configuration);
                        AnalogConfiguration.SearchProperties[WinMenuItem.PropertyNames.Name] = "Analog Configuration";
                        AnalogConfiguration.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
                        ReturnThis = AnalogConfiguration;
                        break;
                    }
                case "ESPGroupConfigGrid":
                    {
                        WinMenuItem ESPConfiguration = new WinMenuItem(Configuration);
                        ESPConfiguration.SearchProperties[WinMenuItem.PropertyNames.Name] = "ESP Well Group Configuration";
                        ESPConfiguration.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
                        ReturnThis = ESPConfiguration;
                        break;

                    }
                case "PCPGroupConfigGrid":
                    {
                        WinMenuItem PCPConfiguration = new WinMenuItem(Configuration);
                        PCPConfiguration.SearchProperties[WinMenuItem.PropertyNames.Name] = "PCP Well Group Configuration";
                        PCPConfiguration.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
                        ReturnThis = PCPConfiguration;
                        break;
                    }
            }
            return ReturnThis;
        }

        public WinMenuItem GetStatusItem(string GridName)
        {
            WinWindow ItemWin = new WinWindow();
            ItemWin.SearchProperties[WinWindow.PropertyNames.AccessibleName] = "Context";
            ItemWin.SearchProperties[WinWindow.PropertyNames.ClassName] = "#32768";

            WinMenuItem Status = new WinMenuItem(ItemWin);
            Status.SearchProperties[WinMenuItem.PropertyNames.Name] = ".Status";
            WinMenuItem ReturnThis = new WinMenuItem();

            switch (GridName)
            {
                case "MOP":
                    {
                        WinMenuItem MOPGridItem = new WinMenuItem(Status);
                        MOPGridItem.SearchProperties[WinMenuItem.PropertyNames.Name] = "Well Status/MOP/Type History";
                        MOPGridItem.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
                        ReturnThis = MOPGridItem;
                        break;
                    }
                case "MeterStatus":
                    {
                        WinMenuItem MeterStatus = new WinMenuItem(Status);
                        MeterStatus.SearchProperties[WinMenuItem.PropertyNames.Name] = "Meter Status";
                        MeterStatus.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
                        ReturnThis = MeterStatus;
                        break;
                    }
            }
            return ReturnThis;
        }

        public WinButton MOPAddButton()
        {
            WinWindow ItemWin4 = new WinWindow(GetMainWindow());
            ItemWin4.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            ItemWin4.SearchProperties.Add(WinWindow.PropertyNames.ControlId, "59392");


            WinButton AddMOP = new WinButton(ItemWin4);
            AddMOP.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            AddMOP.SearchProperties.Add(WinButton.PropertyNames.Name, "Add Well Status/MOP/Type History to Selected Well");

            return AddMOP;
        }

        public void MOPAddWellStatusWin(string ComboBox, string CBValue, string WellName)
        {
            WinWindow MOPAddWellStatWin = new WinWindow();
            MOPAddWellStatWin.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            MOPAddWellStatWin.SearchProperties.Add(WinWindow.PropertyNames.ClassName, "#32770");
            MOPAddWellStatWin.SearchProperties.Add(WinWindow.PropertyNames.Name, "Add Well Status/MOP/Type History to Selected Well");

            #region CB
            switch (ComboBox)
            {
                case "NewStatus":
                    {
                        WinWindow NewStatusComboWin = new WinWindow(MOPAddWellStatWin);
                        NewStatusComboWin.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        NewStatusComboWin.SearchProperties.Add(WinWindow.PropertyNames.ControlName, "combo");
                        NewStatusComboWin.SearchProperties.Add(WinWindow.PropertyNames.Instance, "3"); // New Status CB
                        NewStatusComboWin.WindowTitles.Add("Add Well Status/MOP/Type History to Selected Well");
                        WinComboBox NewStatusComboBox = NewStatusComboWin.GetChildren().OfType<WinComboBox>().First();


                        WinWindow ComboBoxWindow = new WinWindow();
                        ComboBoxWindow.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        ComboBoxWindow.SearchProperties[WinWindow.PropertyNames.Name] = "ComboBox";
                        ComboBoxWindow.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.ClassName, "WindowsForms10.Window", PropertyExpressionOperator.Contains));
                        ComboBoxWindow.WindowTitles.Add("ComboBox");


                        WinWindow UIListWin = new WinWindow(ComboBoxWindow);
                        UIListWin.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        UIListWin.SearchProperties[WinWindow.PropertyNames.ControlName] = "list";
                        UIListWin.WindowTitles.Add("ComboBox");
                        if (NewStatusComboBox.SelectedItem != CBValue)
                        {

                            //Mouse.Click(NewStatusComboBox);
                            // NewStatusComboBox.Expanded = true;


                            //WinList List = new WinList(UIListWin);
                            //List.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                            //List.WindowTitles.Add("ComboBox");
                            //string[] Items = List.SelectedItems;
                            //foreach (string item in Items)
                            //{
                            //    if (item == CBValue)
                            //    {
                            //        WinListItem ItemToClick = new WinListItem(UIListWin);
                            //        ItemToClick.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                            //        ItemToClick.SearchProperties.Add(WinListItem.PropertyNames.Name, item);
                            //        Mouse.Click(ItemToClick);
                            //    }
                            //}
                            while (NewStatusComboBox.SelectedItem != CBValue)
                            {

                                Mouse.Click(NewStatusComboBox);
                                Trace.WriteLine("Clicked Status Combo Box");
                                //NewStatusComboBox.Expanded = true;
                                //Trace.WriteLine("Expanded Status Combo Box");
                                System.Threading.Thread.Sleep(5000);
                                WinList List = new WinList(UIListWin);
                                List.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                                List.WindowTitles.Add("ComboBox");

                                WinListItem Item = new WinListItem(UIListWin);
                                Item.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                                Item.SearchProperties.Add(WinListItem.PropertyNames.Name, CBValue);

                                if (UIListWin.Exists && Item.Exists)
                                {
                                    // List.SelectedItemsAsString = CBValue;
                                    Mouse.Hover(Item);
                                    Trace.WriteLine("Mouse Hover on item " + Item);
                                    //System.Threading.Thread.Sleep(3000);
                                    Mouse.DoubleClick(Item);
                                    Trace.WriteLine("Double Click " + Item);
                                }

                            }
                        }
                        Assert.IsTrue(NewStatusComboBox.SelectedItem == CBValue, "Combox value : " + NewStatusComboBox.SelectedItem.ToString() + " Does NOT EQUAL  " + CBValue + " the correct item was not set");
                        break;
                    }

                case "MOP":
                    {
                        WinWindow NewStatusComboWin = new WinWindow(MOPAddWellStatWin);
                        NewStatusComboWin.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        NewStatusComboWin.SearchProperties.Add(WinWindow.PropertyNames.ControlName, "combo");
                        NewStatusComboWin.SearchProperties.Add(WinWindow.PropertyNames.Instance, "2"); // MOP CB
                        NewStatusComboWin.WindowTitles.Add("Add Well Status/MOP/Type History to Selected Well");

                        WinComboBox NewStatusComboBox = NewStatusComboWin.GetChildren().OfType<WinComboBox>().First();


                        WinWindow ComboBoxWindow = new WinWindow();
                        ComboBoxWindow.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        ComboBoxWindow.SearchProperties[WinWindow.PropertyNames.Name] = "ComboBox";
                        ComboBoxWindow.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.ClassName, "WindowsForms10.Window", PropertyExpressionOperator.Contains));
                        ComboBoxWindow.WindowTitles.Add("ComboBox");


                        WinWindow UIListWin = new WinWindow(ComboBoxWindow);
                        UIListWin.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        UIListWin.SearchProperties[WinWindow.PropertyNames.ControlName] = "list";
                        UIListWin.WindowTitles.Add("ComboBox");
                        if (NewStatusComboBox.SelectedItem != CBValue)
                        {
                            while (NewStatusComboBox.SelectedItem != CBValue)
                            {

                                Mouse.Click(NewStatusComboBox);
                                Trace.WriteLine("Clicked MOP combobox");
                                //NewStatusComboBox.Expanded = true;
                                //Trace.WriteLine("Expanded MOP combobox");
                                System.Threading.Thread.Sleep(5000);
                                WinList List = new WinList(UIListWin);
                                List.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                                List.WindowTitles.Add("ComboBox");

                                WinListItem Item = new WinListItem(UIListWin);
                                Item.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                                Item.SearchProperties.Add(WinListItem.PropertyNames.Name, CBValue);

                                if (UIListWin.Exists && Item.Exists)
                                {
                                    // List.SelectedItemsAsString = CBValue;
                                    Mouse.Hover(Item);
                                    Trace.WriteLine("Mouse Hover on item " + Item);
                                    //System.Threading.Thread.Sleep(3000);
                                    Mouse.DoubleClick(Item);
                                    Trace.WriteLine("Double Click " + Item);
                                }

                            }
                        }
                        Assert.IsTrue(NewStatusComboBox.SelectedItem == CBValue, "Combox value : " + NewStatusComboBox.SelectedItem.ToString() + " Does NOT EQUAL  " + CBValue + " the correct item was not set");
                        break;
                    }



                case "WellType":
                    {
                        WinWindow NewStatusComboWin = new WinWindow(MOPAddWellStatWin);
                        NewStatusComboWin.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        NewStatusComboWin.SearchProperties.Add(WinWindow.PropertyNames.ControlName, "combo");
                        //NewStatusComboWin.SearchProperties.Add(WinWindow.PropertyNames.Instance, "2"); //
                        NewStatusComboWin.WindowTitles.Add("Add Well Status/MOP/Type History to Selected Well");
                        WinComboBox NewStatusComboBox = NewStatusComboWin.GetChildren().OfType<WinComboBox>().First();


                        WinWindow ComboBoxWindow = new WinWindow();
                        ComboBoxWindow.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        ComboBoxWindow.SearchProperties[WinWindow.PropertyNames.Name] = "ComboBox";
                        ComboBoxWindow.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.ClassName, "WindowsForms10.Window", PropertyExpressionOperator.Contains));
                        ComboBoxWindow.WindowTitles.Add("ComboBox");


                        WinWindow UIListWin = new WinWindow(ComboBoxWindow);
                        UIListWin.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                        UIListWin.SearchProperties[WinWindow.PropertyNames.ControlName] = "list";
                        UIListWin.WindowTitles.Add("ComboBox");

                        if (NewStatusComboBox.SelectedItem != CBValue)
                        {
                            while (NewStatusComboBox.SelectedItem != CBValue)
                            {

                                Mouse.Click(NewStatusComboBox);
                                Trace.WriteLine("Clicked WellType combobox");
                                //NewStatusComboBox.Expanded = true;
                                //Trace.WriteLine("Expanded WellType combobox");
                                System.Threading.Thread.Sleep(5000);
                                WinList List = new WinList(UIListWin);
                                List.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                                List.WindowTitles.Add("ComboBox");

                                WinListItem Item = new WinListItem(UIListWin);
                                Item.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
                                Item.SearchProperties.Add(WinListItem.PropertyNames.Name, CBValue);

                                if (UIListWin.Exists && Item.Exists)
                                {
                                    // List.SelectedItemsAsString = CBValue;
                                    Mouse.Hover(Item);
                                    Trace.WriteLine("Mouse Hover on item " + Item);
                                    //System.Threading.Thread.Sleep(3000);
                                    Mouse.DoubleClick(Item);
                                    Trace.WriteLine("Mouse DoubleClick on item " + Item);
                                }

                            }
                        }
                        Assert.IsTrue(NewStatusComboBox.SelectedItem == CBValue, "Combox value : " + NewStatusComboBox.SelectedItem.ToString() + " Does NOT EQUAL  " + CBValue + " the correct item was not set");
                        break;
                    }
                case "StatusComment":
                    {
                        WinClient CommentClient = new WinClient(MOPAddWellStatWin);
                        CommentClient.SearchProperties[WinControl.PropertyNames.ClassName] = "Internet Explorer_Server";
                        CommentClient.WindowTitles.Add("Add Well Status/MOP/Type History to Selected Well");

                        Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument EventDocument = new Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument(CommentClient);
                        EventDocument.SearchProperties[Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.Id] = null;
                        EventDocument.SearchProperties[Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.RedirectingPage] = "False";
                        EventDocument.SearchProperties[Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.FrameDocument] = "False";
                        EventDocument.FilterProperties[Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.Title] = "Adding Well Status Event to Selected Well";
                        EventDocument.WindowTitles.Add("Add Well Status/MOP/Type History to Selected Well");

                        HtmlTextArea CommentTB = new HtmlTextArea(EventDocument);
                        CommentTB.SearchProperties[HtmlEdit.PropertyNames.Id] = "5";
                        CommentTB.SearchProperties[HtmlEdit.PropertyNames.Name] = null;
                        CommentTB.SearchProperties[HtmlEdit.PropertyNames.LabeledBy] = null;
                        CommentTB.FilterProperties[HtmlEdit.PropertyNames.Title] = null;
                        CommentTB.FilterProperties[HtmlEdit.PropertyNames.Class] = "csTextArea";
                        CommentTB.FilterProperties[HtmlEdit.PropertyNames.ControlDefinition] = "tabIndex=0 id=5 title=\"\" class=csTextAre";
                        CommentTB.FilterProperties[HtmlEdit.PropertyNames.TagInstance] = "1";
                        CommentTB.WindowTitles.Add("Add Well Status/MOP/Type History to Selected Well");


                        Mouse.Click(CommentTB);
                        Trace.WriteLine("Mouse Clicked inside comment textbox");
                        Keyboard.SendKeys(CommentTB, "A", ModifierKeys.Control);
                        Trace.WriteLine("Control + A sent to textbox");
                        Keyboard.SendKeys(CBValue);
                        Trace.WriteLine("Textbox comment set to " + CBValue);
                        break;
                    }

                case "OKGreenCheck":
                    {
                        WinClient CommentClient = new WinClient(MOPAddWellStatWin);
                        CommentClient.SearchProperties[WinControl.PropertyNames.ClassName] = "Internet Explorer_Server";
                        CommentClient.WindowTitles.Add("Add Well Status/MOP/Type History to Selected Well");

                        Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument EventDocument = new Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument(CommentClient);
                        EventDocument.SearchProperties[Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.Id] = null;
                        EventDocument.SearchProperties[Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.RedirectingPage] = "False";
                        EventDocument.SearchProperties[Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.FrameDocument] = "False";
                        EventDocument.FilterProperties[Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.Title] = "Adding Well Status Event to Selected Well";
                        EventDocument.WindowTitles.Add("Add Well Status/MOP/Type History to Selected Well");

                        HtmlButton OkGreenCheck = new HtmlButton(EventDocument);
                        OkGreenCheck.SearchProperties[HtmlButton.PropertyNames.Id] = "savebtn";
                        OkGreenCheck.SearchProperties[HtmlButton.PropertyNames.Name] = null;
                        OkGreenCheck.SearchProperties[HtmlButton.PropertyNames.DisplayText] = null;
                        OkGreenCheck.SearchProperties[HtmlButton.PropertyNames.Type] = "button";
                        OkGreenCheck.FilterProperties[HtmlButton.PropertyNames.Title] = "Add Well Status Comment";
                        OkGreenCheck.FilterProperties[HtmlButton.PropertyNames.Class] = null;
                        OkGreenCheck.FilterProperties[HtmlButton.PropertyNames.ControlDefinition] = "onclick=addwlstatevnt() id=savebtn title";
                        OkGreenCheck.FilterProperties[HtmlButton.PropertyNames.TagInstance] = "1";
                        OkGreenCheck.WindowTitles.Add("Add Well Status/MOP/Type History to Selected Well");

                        Mouse.Click(OkGreenCheck);
                        Trace.WriteLine("Clicked Green OK Check");

                        WinButton WarningOKButton = new WinButton();
                        WarningOKButton.SearchProperties[WinButton.PropertyNames.Name] = "OK";
                        WarningOKButton.WindowTitles.Add("VBScript: Warning");

                        WinButton ChangedOKButton = new WinButton();
                        ChangedOKButton.SearchProperties[WinButton.PropertyNames.Name] = "OK";
                        ChangedOKButton.WindowTitles.Add("VBScript: Message");

                        Rectangle WarningOKButtonRect = WarningOKButton.BoundingRectangle;
                        int WarningOKButtonRectx = (WarningOKButtonRect.X + (WarningOKButtonRect.Width / 2));
                        int WarningOKButtonRecty = (WarningOKButtonRect.Y + (WarningOKButtonRect.Height / 2));
                        Mouse.Click(new Point(WarningOKButtonRectx, WarningOKButtonRecty));
                        Trace.WriteLine("Clicked  OK on warning OK button");

                        if (WellName.Contains("GVFD"))
                        {
                            System.Threading.Thread.Sleep(20000); // ESP to Beam takes an extraordinary amount of time to change the MOP 
                        }
                        Rectangle ChangedOKButtonRect = ChangedOKButton.BoundingRectangle;
                        int ChangedOKButtonRectx = (ChangedOKButtonRect.X + (ChangedOKButtonRect.Width / 2));
                        int ChangedOKButtonRecty = (ChangedOKButtonRect.Y + (ChangedOKButtonRect.Height / 2));
                        Mouse.Click(new Point(ChangedOKButtonRectx, ChangedOKButtonRecty));
                        Trace.WriteLine("Clicked  OK on MOPChanged OK button");
                        //Mouse.Click(WarningOKButton);

                        break;
                    }

                case "RedXButton":
                    {
                        WinClient CommentClient = new WinClient(MOPAddWellStatWin);
                        CommentClient.SearchProperties[WinControl.PropertyNames.ClassName] = "Internet Explorer_Server";
                        CommentClient.WindowTitles.Add("Add Well Status/MOP/Type History to Selected Well");

                        Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument EventDocument = new Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument(CommentClient);
                        EventDocument.SearchProperties[Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.Id] = null;
                        EventDocument.SearchProperties[Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.RedirectingPage] = "False";
                        EventDocument.SearchProperties[Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.FrameDocument] = "False";
                        EventDocument.FilterProperties[Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.Title] = "Adding Well Status Event to Selected Well";
                        EventDocument.WindowTitles.Add("Add Well Status/MOP/Type History to Selected Well");

                        HtmlButton RedXButton = new HtmlButton(EventDocument);
                        RedXButton.SearchProperties[HtmlButton.PropertyNames.Id] = "first";
                        RedXButton.SearchProperties[HtmlButton.PropertyNames.Name] = null;
                        RedXButton.SearchProperties[HtmlButton.PropertyNames.DisplayText] = null;
                        RedXButton.SearchProperties[HtmlButton.PropertyNames.Type] = "button";
                        RedXButton.FilterProperties[HtmlButton.PropertyNames.Title] = "Cancel Add";
                        RedXButton.FilterProperties[HtmlButton.PropertyNames.Class] = null;
                        RedXButton.FilterProperties[HtmlButton.PropertyNames.ControlDefinition] = "onclick=canceladd() id=first title=\"Canc";
                        RedXButton.FilterProperties[HtmlButton.PropertyNames.TagInstance] = "2";
                        RedXButton.WindowTitles.Add("Add Well Status/MOP/Type History to Selected Well");

                        Mouse.Click(RedXButton);
                        Trace.WriteLine("Clicked Red X Check button");

                        break;
                    }

            }
            #endregion

        }

    }
}
