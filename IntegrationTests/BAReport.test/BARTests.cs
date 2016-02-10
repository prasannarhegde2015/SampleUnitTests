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
using System.Data;
using System.Xml;
using Novacode;



namespace BARTests
{
    /// <summary>
    /// Summary description for MOPTests
    /// </summary>
    [CodedUITest]
    public class BARTests : Base
    {
        [TestCategory("BAReportTest"), TestMethod]
        public void BAReportTest()
        {

            try
            {

                DeleteStore();
                GetTheRightDirectory();
                StartRTUEmu();
                ImportSomeCrankInfo(WellLName);
                string[] WellInfo = CreateBeamWells("SAMVS");
                WellLName = WellInfo[0];
                WellFacName = WellInfo[1];

                ImportSomeWellInfo(WellLName);
                SetBeamSystemConfig();
                SetBeamAnalysisParams(WellLName);


                StartLOWISGetToWell(WellLName);
                Mouse.Click(GetStartButton());
                Trace.WriteLine("Clicked Start Button");

                Mouse.Hover(GetAnalysisItem("BAWB"));
                Trace.WriteLine("Hovered over Beam Analysis Workbench");
                Mouse.Click(GetAnalysisItem("BAWB"));
                Trace.WriteLine("Clicked Beam Analysis Workbench");


                Point AnaPoint = new Point();
                bool AnaPointBool = GetTab("AnalysisReport").TryGetClickablePoint(out AnaPoint);
                if (AnaPointBool == true)
                {
                    Mouse.Click(MoveButtonToClick("Less")); Mouse.Click(MoveButtonToClick("Less")); Mouse.Click(MoveButtonToClick("Less")); Mouse.Click(MoveButtonToClick("Less")); Mouse.Click(MoveButtonToClick("Less"));
                    Trace.WriteLine("Clicked Right Arrow 5 times");
                    Mouse.Click(GetTab("SurfaceParameters")); Trace.WriteLine("Clicked SurfaceParameters Tab"); System.Threading.Thread.Sleep(5000);
                    HtmlImage RecalcButton = SurfaceParameterControl("RecalculateCBT") as HtmlImage;
                    Mouse.Click(RecalcButton); Trace.WriteLine("Clicked Recalc Button");
                    HtmlImage SaveCbutton = SurfaceParameterControl("SaveChanges") as HtmlImage;
                    Mouse.Click(SaveCbutton); Trace.WriteLine("Clicked Save Button");
                    HtmlEdit CBTTBox = SurfaceParameterControl("CBTTxt") as HtmlEdit;
                    Assert.IsTrue((CBTTBox.Text == "1972.79") || (CBTTBox.Text == "1972.788"), "CBT Text box does not contain the expected value"); Trace.WriteLine("Reading CBT Textbox .....");
                    Trace.WriteLine("CBT Textbox value is " + CBTTBox.Text);
                    Mouse.Click(MoveButtonToClick("More")); Mouse.Click(MoveButtonToClick("More")); Mouse.Click(MoveButtonToClick("More")); Mouse.Click(MoveButtonToClick("More")); Mouse.Click(MoveButtonToClick("More"));
                    Trace.WriteLine("Clicked Left Arrow 5 times");
                    //Mouse.Click(GetTab("AnalysisReport"));
                    //Trace.WriteLine("Clicked AnalysisReport Button");
                }
                else
                {

                    Point AnalysisRPoint = new Point();
                    bool AnalysisR = false;
                    while (AnalysisR == false)
                    {
                        System.Threading.Thread.Sleep(2000);
                        Rectangle MySRect = SplitterButton().BoundingRectangle;
                        Point SClick = new Point(MySRect.X, MySRect.Y - 1);
                        Point SClickToStop = new Point(MySRect.X, MySRect.Y / 2);
                        Mouse.MouseDragSpeed = 50;
                        Mouse.Hover(SClick);
                        Trace.WriteLine("Hover over SClick point");
                        Mouse.StartDragging();
                        Trace.WriteLine("Start dragging at SClick point");
                        //Mouse.StartDragging(SplitterButton(), SClick, MouseButtons.Left, ModifierKeys.None);
                        Mouse.StopDragging(SClickToStop);
                        Trace.WriteLine("Stopped Dragging");
                        AnalysisR = GetTab("AnalysisReport").TryGetClickablePoint(out AnalysisRPoint);
                    }
                    System.Threading.Thread.Sleep(2000);
                    Mouse.Click(MoveButtonToClick("Less")); Mouse.Click(MoveButtonToClick("Less")); Mouse.Click(MoveButtonToClick("Less")); Mouse.Click(MoveButtonToClick("Less")); Mouse.Click(MoveButtonToClick("Less"));
                    Trace.WriteLine("Clicked Right Arrow 5 times");
                    Mouse.Click(GetTab("SurfaceParameters")); Trace.WriteLine("Clicked SurfaceParameters Tab"); System.Threading.Thread.Sleep(5000);
                    HtmlImage RecalcButton = SurfaceParameterControl("RecalculateCBT") as HtmlImage;
                    Mouse.Click(RecalcButton); Trace.WriteLine("Clicked Recalc Button");
                    HtmlImage SaveCbutton = SurfaceParameterControl("SaveChanges") as HtmlImage;
                    Mouse.Click(SaveCbutton); Trace.WriteLine("Clicked Save Button");
                    HtmlEdit CBTTBox = SurfaceParameterControl("CBTTxt") as HtmlEdit;
                    Assert.IsTrue((CBTTBox.Text == "1972.79") || (CBTTBox.Text == "1972.788"), "CBT Text box does not contain the expected value"); Trace.WriteLine("Reading CBT Textbox .....");
                    Trace.WriteLine("CBT Textbox value is " + CBTTBox.Text);
                    Mouse.Click(MoveButtonToClick("More")); Mouse.Click(MoveButtonToClick("More")); Mouse.Click(MoveButtonToClick("More")); Mouse.Click(MoveButtonToClick("More")); Mouse.Click(MoveButtonToClick("More"));
                    Trace.WriteLine("Clicked Left Arrow 5 times");




                }

                Point TryCollectionPoint = new Point();
                bool TryCollection = CardTypeCollectionButton().TryGetClickablePoint(out TryCollectionPoint);
                if (TryCollection == true)
                {
                    Mouse.Click(CardTypeCollectionButton());
                    Trace.WriteLine("Clicked Card Collection Button");
                }
                else
                {
                    Point CollectionPoint = new Point();
                    bool CollectionR = false;
                    while (CollectionR == false)
                    {
                        System.Threading.Thread.Sleep(2000);
                        Rectangle MySRect = SplitterButton().BoundingRectangle;
                        Point SClick = new Point(MySRect.X, MySRect.Y - 1);
                        Point SClickToStop = new Point(MySRect.X, MySRect.Y * 6);
                        Mouse.MouseDragSpeed = 50;
                        Mouse.Hover(SClick);
                        Trace.WriteLine("Hover over SClick point");
                        Mouse.StartDragging();
                        Trace.WriteLine("Start dragging at SClick point");
                        //Mouse.StartDragging(SplitterButton(), SClick, MouseButtons.Left, ModifierKeys.None);
                        Mouse.StopDragging(SClickToStop);
                        Trace.WriteLine("Stopped Dragging");
                        CollectionR = CardTypeCollectionButton().TryGetClickablePoint(out CollectionPoint);
                    }
                    Mouse.Click(CardTypeCollectionButton());
                    Trace.WriteLine("Clicked Card Collection Button");
                }


                Mouse.Click(GetCardItem("LastFullCard"));
                Trace.WriteLine("Clicked Last Full Card from context drop down");
                Mouse.Click(CardListItem("LastFullCard"));
                Trace.WriteLine("Clicked Full Card List Item");

                GetChartClient("ShowDownHoleCards");
                System.Threading.Thread.Sleep(3000);
                Mouse.Click(GetRunAnalysisButton());
                Trace.WriteLine("Clicked Run Analysis Button");
                System.Threading.Thread.Sleep(3000);
                Trace.WriteLine("Waiting 3 seconds");
                if (OKWarningButton().Exists)
                {
                    Mouse.Click(OKWarningButton());
                    Trace.WriteLine("Warning Dialog exists, clicked OK button");
                }



                Mouse.Click(GetTab("AnalysisReport"));
                Trace.WriteLine("Clicked AnalysisReport Button");
                Thread.Sleep(5000);
                Mouse.Click(PrintToWord());
                Trace.WriteLine("Clicked Print to Word on report");
                bool OkButOneIsthere = OKButOne().Exists;
                if (OkButOneIsthere)
                {
                    Mouse.Click(OKButOne()); //for not having word
                    Trace.WriteLine("Clicked OK button 1 for not having Word on the box");
                }
                bool OkButTwoIsthere = OKButTwo().Exists;
                if (OkButTwoIsthere)
                {
                    Mouse.Click(OKButTwo()); //for not having word
                    Trace.WriteLine("Clicked OK button 2 for not having Word on the box");
                }
                if (OkButOneIsthere && OkButTwoIsthere) // means for sure no word on machine
                {
                    PasteToText();
                    TestBARExportFile();
                }
                else   //word probably is now open since word is probably on machine
                {
                    WinWindow WordAppWindow = WordApp();
                    WordAppWindow.SetFocus();
                    System.Threading.Thread.Sleep(2000);
                    Keyboard.SendKeys("F", ModifierKeys.Alt);
                    Keyboard.SendKeys("A");
                    string WordFileLocation = SaveWordDoco();
                    Keyboard.SendKeys("{F4}", ModifierKeys.Alt);
                    CheckIfExportExists(WordFileLocation);
                    TestBARExportWordDoc(WordFileLocation);
                   // TestBARExportWordDoc(@"C:\Users\E208099\Downloads\BeamAnalysisReport.docx");
                }
                    CloseLOWISClient();
            }


            finally
            {
                UnLinkWellToFacility(WellFacName, WellLName);
                DeleteFacility(WellFacName);
                StopRTUEmu();
                if (DeleteOutputfiles == "true") { Cleanup(true); }
                else { Cleanup(false); }

            }
        }

    }

    public class Base
    {
        #region variables
        protected string BAReport = String.Empty;
        protected string m_strEmulatorExe = "RTUEmu.exe";

        protected string WellLName = String.Empty;
        protected string WellFacName = String.Empty;
        

        protected string m_strLiftDir = GetLiftRunFolder();
        string LowisClientPath = ConfigurationManager.AppSettings["LowisClientPath"];
        string ConnectClientTo = ConfigurationManager.AppSettings["ConnectClientTo"];
        public string ExportLocation = ConfigurationManager.AppSettings.Get("ExportLocation");
        public string DeleteOutputfiles = ConfigurationManager.AppSettings.Get("DeleteOutputfiles");
        public string RunningInATS = ConfigurationManager.AppSettings.Get("IsRunningInATS");
        public string BeamAnalysisText = ConfigurationManager.AppSettings.Get("BeamAnalysisText");
        public string BeamAnalysisWord = ConfigurationManager.AppSettings.Get("BeamAnalysisWord");



        public string ExpectedResult = String.Empty;
        public string ImportWellInfo = String.Empty;
        public string ImportCrankInfoFile = String.Empty;
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

                ExpectedResult = WantedCWDPath + ConfigurationManager.AppSettings.Get("BeamAnalysisReportExpected");
                ImportWellInfo = WantedCWDPath + ConfigurationManager.AppSettings.Get("ImportWellInfo");
                ImportCrankInfoFile = WantedCWDPath + ConfigurationManager.AppSettings.Get("ImportCrankInfo");
                strMakeBulkWellScript = WantedCWDPath + ConfigurationManager.AppSettings.Get("CreateWellScript");
                strDeleteBulkWellScript = WantedCWDPath + ConfigurationManager.AppSettings.Get("DeleteWellScript");
            }
            else //running local
            {
                ExpectedResult = ConfigurationManager.AppSettings.Get("BeamAnalysisReportExpected");
                ImportCrankInfoFile = ConfigurationManager.AppSettings.Get("ImportCrankInfo");
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

        public void SetBeamSystemConfig()
        {
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            Process SetBeamSystemConfigProcess = new Process();

            try
            {
                SetBeamSystemConfigProcess.StartInfo.UseShellExecute = true;
                SetBeamSystemConfigProcess.StartInfo.FileName = strMcsscrip;


                SetBeamSystemConfigProcess.StartInfo.Arguments = String.Format("{0} SetBeamSystemConfig \"\" -db beamdb", strMakeBulkWellScript);

                Trace.WriteLine(strMcsscrip + " " + SetBeamSystemConfigProcess.StartInfo.Arguments);
                Assert.IsTrue(SetBeamSystemConfigProcess.Start());
                SetBeamSystemConfigProcess.WaitForExit();



            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }


        }

        public void ImportSomeCrankInfo(string WellName)
        {
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            Process ImportCrankInfo = new Process();

            try
            {
                ImportCrankInfo.StartInfo.UseShellExecute = true;
                ImportCrankInfo.StartInfo.FileName = strMcsscrip;


                ImportCrankInfo.StartInfo.Arguments = String.Format("{0} ImportCrank \"\" \"{1}\"  -db beamdb", strMakeBulkWellScript, ImportCrankInfoFile);

                Trace.WriteLine(strMcsscrip + " " + ImportCrankInfo.StartInfo.Arguments);
                Assert.IsTrue(ImportCrankInfo.Start());
                ImportCrankInfo.WaitForExit();



            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }

            
        }

        public string SaveWordDoco()
        {

            //WinButton FileButton = new WinButton();
            //FileButton.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            //FileButton.SearchProperties[WinButton.PropertyNames.Name] = "File Tab";

            //Mouse.DoubleClick(FileButton);

            WinWindow SaveAs = new WinWindow();
            SaveAs.SearchProperties[WinWindow.PropertyNames.Name] = "Save As";
            SaveAs.SearchProperties[WinWindow.PropertyNames.ClassName] = "#32770";
            SaveAs.WindowTitles.Add("Save As");

            WinWindow ItemWin = new WinWindow(SaveAs);
            ItemWin.SearchProperties[WinWindow.PropertyNames.ClassName] = "ToolbarWindow32";
            ItemWin.SearchProperties[WinWindow.PropertyNames.Instance] = "5";
            ItemWin.WindowTitles.Add("Save As");

            WinToolBar Toolbar = new WinToolBar(ItemWin);
            Toolbar.WindowTitles.Add("Save As");

            WinButton PreviousLocations = new WinButton(Toolbar);
            PreviousLocations.SearchProperties[WinButton.PropertyNames.Name] = "Previous Locations";
            PreviousLocations.WindowTitles.Add("Save As");
            System.Threading.Thread.Sleep(2000);
            Mouse.Click(PreviousLocations);

            WinWindow ItemWin1 = new WinWindow(SaveAs);
            ItemWin1.SearchProperties[WinWindow.PropertyNames.ControlId] = "41477";
            ItemWin1.SearchProperties[WinWindow.PropertyNames.Instance] = "4";
            ItemWin1.WindowTitles.Add("Save As");

            WinEdit AddressBar = new WinEdit(ItemWin1);
            AddressBar.SearchProperties[WinEdit.PropertyNames.Name] = "Address";
            AddressBar.WindowTitles.Add("Save As");

            AddressBar.Text = ExportLocation ;
            Keyboard.SendKeys("{ENTER}");

            WinEdit FileName = new WinEdit(SaveAs);
            FileName.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            FileName.SearchProperties.Add(WinEdit.PropertyNames.Name, "File name:");

            FileName.Text = BeamAnalysisWord;

            WinWindow SaveWindow = new WinWindow(SaveAs);
            SaveWindow.SearchProperties[WinWindow.PropertyNames.ControlId] = "1";
            SaveWindow.WindowTitles.Add("Save As");

            WinButton SaveButton = new WinButton(SaveWindow);
            SaveButton.SearchProperties[WinButton.PropertyNames.Name] = "Save";
            SaveButton.WindowTitles.Add("Save As");
            Thread.Sleep(2000);
            Mouse.Click(SaveButton);

            return (ExportLocation + BeamAnalysisWord);
        }

        public void SetBeamAnalysisParams(string WellName)
        {
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");




            Process SetBeamAnalysisParms = new Process();

            try
            {
                SetBeamAnalysisParms.StartInfo.UseShellExecute = true;
                SetBeamAnalysisParms.StartInfo.FileName = strMcsscrip;


                SetBeamAnalysisParms.StartInfo.Arguments = String.Format("{0} SetBeamAnalysisParameters \"\" \"{1}\" -db beamdb", strMakeBulkWellScript, WellName);

                Trace.WriteLine(strMcsscrip + " " + SetBeamAnalysisParms.StartInfo.Arguments);
                Assert.IsTrue(SetBeamAnalysisParms.Start());
                SetBeamAnalysisParms.WaitForExit();



            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }

            
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
            WinList WellList = GetWellList();
            WellList.SelectedItemsAsString = WellName;
            Trace.WriteLine("Selected Well " + WellName);
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


                    case "EPICLM":
                        parmWellNamePrefix = "EPILM_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 120;
                        parmLastAddress = 120 ;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "EPICRP";
                        parmRtuSubType = "EPICLM";
                        FacName = "BeamFac";
                        Key = "BeamKey";
                        break;

                    case "SAMVS":
                        parmWellNamePrefix = "SAMVS_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 11;
                        parmLastAddress = 11;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "SAMRP2";
                        parmRtuSubType = "SAMVS";
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

        protected void Cleanup(bool DeleteExportedFiles)
        {

            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");

            string[] welltypesToDelete = {"deletebeamwells"};

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
                    
                    string[] txtList = Directory.GetFiles(ExportLocation, "*.txt");

                    foreach (string t in txtList)
                    {
                        if ((t.Contains("BeamAnalysisReport") && t.Contains(".txt")))
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

        public WinList GetWellList()
        {
            WinWindow SearchWellWin = new WinWindow();
            SearchWellWin.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            SearchWellWin.SearchProperties.Add(WinWindow.PropertyNames.ClassName, "#32770");
            SearchWellWin.SearchProperties.Add(WinWindow.PropertyNames.Name, "Search Well");

            WinWindow SearchWellWinEdit = new WinWindow(SearchWellWin);
            SearchWellWinEdit.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            SearchWellWinEdit.SearchProperties.Add(WinWindow.PropertyNames.ControlId, "1008");

            WinList WellList = new WinList(SearchWellWinEdit);
             WellList.WindowTitles.Add("Search Well");

             return WellList;
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

        public WinMenuItem GetAnalysisItem(string GridName)
        {
            WinWindow ItemWin = new WinWindow();
            ItemWin.SearchProperties[WinWindow.PropertyNames.AccessibleName] = "Context";
            ItemWin.SearchProperties[WinWindow.PropertyNames.ClassName] = "#32768";

            WinMenuItem Configuration = new WinMenuItem(ItemWin);
            Configuration.SearchProperties[WinMenuItem.PropertyNames.Name] = ".Analysis";
            WinMenuItem ReturnThis = new WinMenuItem();

            switch (GridName)
            {
                case "BAWB":
                    {
                        WinMenuItem BeamWellGroupConfig = new WinMenuItem(Configuration);
                        BeamWellGroupConfig.SearchProperties[WinMenuItem.PropertyNames.Name] = "Beam Analysis WorkBench";
                        BeamWellGroupConfig.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
                        ReturnThis = BeamWellGroupConfig;
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

        public  WinSplitButton CardTypeCollectionButton()
        {
            WinWindow ItemWindow = new WinWindow(GetMainWindow());
            ItemWindow.SearchProperties[WinWindow.PropertyNames.ControlId] = "59392";
            ItemWindow.SearchProperties[WinWindow.PropertyNames.Instance] = "3";
            ItemWindow.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));

            WinSplitButton CardType = new WinSplitButton(ItemWindow);
            CardType.SearchProperties[WinButton.PropertyNames.Name] = "Card Type Collection";
            CardType.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));
            
            return (CardType);
        }

        public WinMenuItem GetCardItem(string CardName)
        {
            WinWindow ItemWindow = new WinWindow();
            ItemWindow.SearchProperties[WinWindow.PropertyNames.AccessibleName] = "Context";
            ItemWindow.SearchProperties[WinWindow.PropertyNames.ClassName] = "#32768";

            WinMenu Context = new WinMenu(ItemWindow);
            Context.SearchProperties[WinMenu.PropertyNames.Name] = "Context";

            WinMenuItem ReturnThis = new WinMenuItem();

            switch(CardName)
            {
                case "CurrentCard":
                    {
                        WinMenuItem CardToGet = new WinMenuItem(Context);
                        CardToGet.SearchProperties[WinMenuItem.PropertyNames.Name] = "Current Card";
                        CardToGet.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
                        ReturnThis = CardToGet;
                        break;
                    }
                case "LastFullCard":
                    {
                        WinMenuItem CardToGet = new WinMenuItem(Context);
                        CardToGet.SearchProperties[WinMenuItem.PropertyNames.Name] = "Last Full Card";
                        CardToGet.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
                        ReturnThis = CardToGet;
                        break;
                    }
            }
            return ReturnThis;
        }

        public WinListItem CardListItem(string CardName)
        {
            WinWindow CardWin = new WinWindow(GetMainWindow());
            CardWin.SearchProperties[WinWindow.PropertyNames.ControlId] = "211"; 
            CardWin.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));

            WinListItem ReturnThis = new WinListItem();

           switch (CardName)
           {
               case "CurrentCard":
                   {
                       WinListItem CardToGet = new WinListItem(CardWin);
                       CardToGet.SearchProperties[WinMenuItem.PropertyNames.Name] = "Current Card";
                       CardToGet.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
                       ReturnThis = CardToGet;
                       break;
                   }
               case "LastFullCard":
                   {
                       WinListItem CardToGet = new WinListItem(CardWin);
                       CardToGet.SearchProperties[WinMenuItem.PropertyNames.Name] = "Full Card";
                       CardToGet.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
                       ReturnThis = CardToGet;
                       break;
                   }
           }
           return ReturnThis;
       }

        public WinButton GetRunAnalysisButton()
       {
           WinWindow ItemWindow = new WinWindow(GetMainWindow());
           ItemWindow.SearchProperties[WinWindow.PropertyNames.ControlId] = "59392";
           ItemWindow.SearchProperties[WinWindow.PropertyNames.Instance] = "3";
           ItemWindow.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));

           WinButton AnalysisRunButton = new WinButton(ItemWindow);
           AnalysisRunButton.SearchProperties[WinButton.PropertyNames.Name] = "Run Analysis on Selected Card";
           AnalysisRunButton.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));;
           return AnalysisRunButton;
        }

        public WinButton MoveButtonToClick(string Name)
        {
            WinWindow ItemWindow = new WinWindow(GetMainWindow());
            ItemWindow.SearchProperties[WinWindow.PropertyNames.ControlId] = "1";
            ItemWindow.SearchProperties[WinWindow.PropertyNames.Instance] = "2";
            ItemWindow.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));

            WinButton ButtonToClick = new WinButton();

            switch (Name)
            {
                case "Less":
                    {
                        WinButton LessButton = new WinButton(ItemWindow);
                        LessButton.SearchProperties[WinButton.PropertyNames.Name] = "Less";
                        LessButton.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));
                        ButtonToClick = LessButton;
                        break;
                    }
                case "More":
                    {
                        WinButton MoreButton = new WinButton(ItemWindow);
                        MoreButton.SearchProperties[WinButton.PropertyNames.Name] = "More";
                        MoreButton.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString())); ;
                        ButtonToClick = MoreButton;
                        break;
                    }
            }
            return ButtonToClick;

        }

        public WinButton SplitterButton()
        {
            WinWindow ItemWindow = new WinWindow(GetMainWindow());
            ItemWindow.SearchProperties[WinWindow.PropertyNames.ControlId] = "1010";
         
            ItemWindow.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));

            WinButton SplitBut = new WinButton(ItemWindow);
           // AnalysisRunButton.SearchProperties[WinButton.PropertyNames.Name] = "Run Analysis on Selected Card";
            SplitBut.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString())); ;
            return SplitBut;
        }

        public WinWindow WordApp()
        {
            WinWindow WordWindow = new WinWindow();
            WordWindow.SearchConfigurations.Add(SearchConfiguration.AlwaysSearch);
            WordWindow.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.Name, "Microsoft Word", PropertyExpressionOperator.Contains));
            WordWindow.SearchProperties[WinWindow.PropertyNames.ClassName] = "OpusApp";
            
            
           // Wordbutton.SearchProperties.Add(WinButton.PropertyNames.ClassName, "MSTaskListWClass");
            return WordWindow;
        }

        public WinTabPage GetTab(string TabName)
        {
            WinWindow TabWin = new WinWindow(GetMainWindow());
            TabWin.SearchProperties[WinWindow.PropertyNames.ControlId] = "293";
            TabWin.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));

            WinTabList TabList = new WinTabList(TabWin);
            TabList.SearchProperties[WinTabList.PropertyNames.Name] = "Tab1";
            TabList.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));

            WinTabPage TabToClick = new WinTabPage();

            switch(TabName)
            {
                case "AnalysisReport":
                    {
                        WinTabPage AnalysisReport = new WinTabPage(TabList);
                        AnalysisReport.SearchProperties[WinTabPage.PropertyNames.Name] = "Analysis Report";
                        AnalysisReport.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));
                        TabToClick = AnalysisReport;
                        break;
                    }
                case "SurfaceParameters":
                    {
                        WinTabPage SurfaceParameters = new WinTabPage(TabList);
                        SurfaceParameters.SearchProperties[WinTabPage.PropertyNames.Name] = "Surface Parameters";
                        SurfaceParameters.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));
                        TabToClick = SurfaceParameters;
                        break;
                    }
            }
            return TabToClick;
        }

        public void GetChartClient(string Item)
        {
            WinWindow ItemWindow = new WinWindow(GetMainWindow());
            ItemWindow.SearchProperties[WinWindow.PropertyNames.ClassName] = "OlectraChart2D";
            ItemWindow.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString())); ;

            WinClient ChartClient = new WinClient(ItemWindow);
            ChartClient.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString())); ;


            Mouse.Click(ChartClient, MouseButtons.Right);
            Trace.WriteLine("Right Clicked on Chart");

            WinWindow ItemWindow2 = new WinWindow();
            ItemWindow2.SearchProperties[WinWindow.PropertyNames.AccessibleName] = "Context";
            ItemWindow2.SearchProperties[WinWindow.PropertyNames.ClassName] = "#32768";

            WinMenu Context = new WinMenu(ItemWindow2);
            Context.SearchProperties[WinMenu.PropertyNames.Name] = "Context";

            WinMenuItem ShowDownholeCardToGet = new WinMenuItem(Context);
            ShowDownholeCardToGet.SearchProperties[WinMenuItem.PropertyNames.Name] = "Show Downhole Cards";
            ShowDownholeCardToGet.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);

            WinMenuItem HideDownholeCardToGet = new WinMenuItem(Context);
            HideDownholeCardToGet.SearchProperties[WinMenuItem.PropertyNames.Name] = "Hide Downhole Cards";
            HideDownholeCardToGet.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);

           

            switch (Item)
            {
                case "ShowDownHoleCards":
                    {
                        if (ShowDownholeCardToGet.Exists)
                        {
                            Mouse.Click(ShowDownholeCardToGet);
                            Trace.WriteLine("Clicked Show Downhole Cards from context menu");
                        }
                        break;
                    }
                case "HideDownHoleCards":
                    {
                        if (HideDownholeCardToGet.Exists)
                        {
                            Mouse.Click(HideDownholeCardToGet);
                            Trace.WriteLine("Clicked Hide Downhole Cards from context menu");
                        }
                        break;
                    }
                
            }
           
        }

        public HtmlCustom PrintToWord()
        {
            WinControl IES = new WinControl();
            IES.SearchProperties[WinControl.PropertyNames.ClassName] = "Internet Explorer_Server";
            IES.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));


            Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument Doc = new Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument(IES);
            Doc.SearchProperties[Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.Id] = null;
            Doc.SearchProperties[Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.RedirectingPage] = "False";
            Doc.SearchProperties[Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.FrameDocument] = "False";
            Doc.FilterProperties[Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.Title] = "Beam Analysis Report";
            Doc.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString())); 

            HtmlCustom PrintWordButton = new HtmlCustom(Doc);
            PrintWordButton.SearchProperties["TagName"] = "A";
            PrintWordButton.SearchProperties["Id"] = "btnPrint";
            PrintWordButton.SearchProperties[UITestControl.PropertyNames.Name] = null;
            PrintWordButton.FilterProperties["Class"] = "clsTabBtn";
            PrintWordButton.FilterProperties["ControlDefinition"] = "style=\"COLOR: darkblue\" id=btnPrint clas";
            PrintWordButton.FilterProperties["TagInstance"] = "3";
            PrintWordButton.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));

            return PrintWordButton;
        }

        public UITestControl SurfaceParameterControl(string SurfaceParameterControl)
        {
            WinClient Client = new WinClient(GetMainWindow());
            Client.SearchProperties[WinControl.PropertyNames.ClassName] = "Internet Explorer_Server";
            Client.SearchProperties[WinControl.PropertyNames.Instance] = "3";
            Client.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString())); 

            Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument Documento = new Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument(Client);
            Documento.SearchProperties[Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.Id] = "awb_surfaceparams.htm";
            Documento.SearchProperties[Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.RedirectingPage] = "False";
            Documento.SearchProperties[Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.FrameDocument] = "False";
            Documento.FilterProperties[Microsoft.VisualStudio.TestTools.UITesting.HtmlControls.HtmlDocument.PropertyNames.Title] = null;
            Documento.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));

            UITestControl ReturnThis= new UITestControl();

            switch (SurfaceParameterControl)
            {
                case "RecalculateCBT":
                    {
                        HtmlButton Button = new HtmlButton(Documento);
                        Button.SearchProperties[HtmlButton.PropertyNames.Id] = "btnRecalcCBT";
                        Button.SearchProperties[HtmlButton.PropertyNames.Name] = null;
                        Button.SearchProperties[HtmlButton.PropertyNames.DisplayText] = null;
                        Button.SearchProperties[HtmlButton.PropertyNames.Type] = "button";
                        Button.FilterProperties[HtmlButton.PropertyNames.Title] = "Recalculate CBT";
                        Button.FilterProperties[HtmlButton.PropertyNames.Class] = null;
                        Button.FilterProperties[HtmlButton.PropertyNames.ControlDefinition] = "style=\"WIDTH: 30px; HEIGHT: 30px\" id=btn";
                        Button.FilterProperties[HtmlButton.PropertyNames.TagInstance] = "5";
                        Button.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));

                        HtmlImage CBTImageBut = new HtmlImage(Button);
                        CBTImageBut.SearchProperties[HtmlImage.PropertyNames.Id] = null;
                        CBTImageBut.SearchProperties[HtmlImage.PropertyNames.Name] = null;
                        CBTImageBut.SearchProperties[HtmlImage.PropertyNames.Alt] = null;
                        CBTImageBut.FilterProperties[HtmlImage.PropertyNames.Class] = null;
                        CBTImageBut.FilterProperties[HtmlImage.PropertyNames.TagInstance] = "1";
                        CBTImageBut.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));
                        ReturnThis = CBTImageBut;
                        break;
                    }
                case "SaveChanges":
                    {
                        HtmlButton Button = new HtmlButton(Documento);
                        Button.SearchProperties[HtmlButton.PropertyNames.Id] = "btnSave";
                        Button.SearchProperties[HtmlButton.PropertyNames.Name] = null;
                        Button.SearchProperties[HtmlButton.PropertyNames.DisplayText] = null;
                        Button.SearchProperties[HtmlButton.PropertyNames.Type] = "button";
                        Button.FilterProperties[HtmlButton.PropertyNames.Title] = "Save changes";
                        Button.FilterProperties[HtmlButton.PropertyNames.Class] = null;
                        Button.FilterProperties[HtmlButton.PropertyNames.ControlDefinition] = "style=\"WIDTH: 30px; HEIGHT: 30px\" id=btn";
                        Button.FilterProperties[HtmlButton.PropertyNames.TagInstance] = "11";
                        Button.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));

                        HtmlImage SaveImageBut = new HtmlImage(Button);
                        SaveImageBut.SearchProperties[HtmlImage.PropertyNames.Id] = null;
                        SaveImageBut.SearchProperties[HtmlImage.PropertyNames.Name] = null;
                        SaveImageBut.SearchProperties[HtmlImage.PropertyNames.Alt] = null;
                        SaveImageBut.FilterProperties[HtmlImage.PropertyNames.Class] = null;
                        SaveImageBut.FilterProperties[HtmlImage.PropertyNames.TagInstance] = "1";
                        SaveImageBut.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));
                        ReturnThis = SaveImageBut;
                        break;
                    }
                case "CBTTxt":
                    {                 
                        HtmlEdit CBTBox = new HtmlEdit(Documento);
                        CBTBox.SearchProperties[HtmlEdit.PropertyNames.Id] = "eCCBTRQ";
                        CBTBox.SearchProperties[HtmlEdit.PropertyNames.Name] = null;
                        CBTBox.SearchProperties[HtmlEdit.PropertyNames.LabeledBy] = null;
                        CBTBox.SearchProperties[HtmlEdit.PropertyNames.Type] = "SINGLELINE";
                        CBTBox.FilterProperties[HtmlEdit.PropertyNames.Title] = null;
                        CBTBox.FilterProperties[HtmlEdit.PropertyNames.Class] = "clsFloatEdit";
                        CBTBox.FilterProperties[HtmlEdit.PropertyNames.ControlDefinition] = "id=eCCBTRQ dataSrc=#eXml class=clsFloatE";
                        CBTBox.FilterProperties[HtmlEdit.PropertyNames.TagInstance] = "6";
                        CBTBox.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "LOWIS:", PropertyExpressionOperator.Contains).ToString()));
                        ReturnThis = CBTBox;
                        break;
                    }
            }
            return ReturnThis;
        }

        public WinButton OKButOne()
        {
            WinWindow OKButWin = new WinWindow();
            OKButWin.SearchProperties[WinWindow.PropertyNames.ControlId] = "2";
            OKButWin.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Retrieving the COM class factory", PropertyExpressionOperator.Contains).ToString())); ;

            WinButton OkButton = new WinButton(OKButWin);
            OkButton.SearchProperties[WinButton.PropertyNames.Name] = "OK";
            OkButton.WindowTitles.Add((new PropertyExpression(WinWindow.PropertyNames.Name, "Retrieving the COM class factory", PropertyExpressionOperator.Contains).ToString())); ;

            return OkButton;
        }

        public WinButton OKButTwo()
        {
            WinWindow MessWin = new WinWindow();
            MessWin.SearchProperties[WinWindow.PropertyNames.Name] = "Message from webpage";
            MessWin.SearchProperties[WinWindow.PropertyNames.ClassName] = "#32770";
            MessWin.WindowTitles.Add("Message from webpage");


            WinWindow OKButWin = new WinWindow();
            OKButWin.SearchProperties[WinWindow.PropertyNames.ControlId] = "2";
            OKButWin.WindowTitles.Add("Message from webpage");

            WinButton OkButton = new WinButton(OKButWin);
            OkButton.SearchProperties[WinButton.PropertyNames.Name] = "OK";
            OkButton.WindowTitles.Add("Message from webpage");

            return OkButton;
        }

        public WinButton OKWarningButton()
        {
            WinWindow MessWin = new WinWindow();
            MessWin.SearchProperties[WinWindow.PropertyNames.Name] = "LOWIS Warning Message";
            MessWin.SearchProperties[WinWindow.PropertyNames.ClassName] = "#32770";
            MessWin.WindowTitles.Add("LOWIS Warning Message");

            WinWindow OKButWin = new WinWindow(MessWin);
            OKButWin.SearchProperties[WinWindow.PropertyNames.ControlId] = "2";
            OKButWin.WindowTitles.Add("LOWIS Warning Message");

            WinButton OkWarningButton = new WinButton(OKButWin);
            OkWarningButton.SearchProperties[WinButton.PropertyNames.Name] = "OK";
            OkWarningButton.WindowTitles.Add("LOWIS Warning Message");

            return OkWarningButton;
        }

        public void PasteToText()
        {
            string Location = ExportLocation + BeamAnalysisText;
            
            File.WriteAllText(Location, Clipboard.GetText(TextDataFormat.Text));
            Trace.WriteLine("Pasted information from clipboard into text file");
            Trace.WriteLine("Checking if text file exists....");
            CheckIfExportExists(Location);
            
        }

        protected void StartRTUEmu()
        {
            
            try
            {
                string strFolder = ConfigurationManager.AppSettings.Get("RTUEmuFolder");
                string strCommand = strFolder + m_strEmulatorExe;

                Process emulator = new Process();

                emulator.StartInfo.UseShellExecute = true;
                emulator.StartInfo.FileName = strCommand;

                Trace.WriteLine(strCommand);

                Assert.IsTrue(emulator.Start());

                System.Threading.Thread.Sleep(9000);

                if (emulator.Responding)
                {
                    Trace.WriteLine("Emulator " + emulator.ProcessName + " " + "is running at process id  " + emulator.Id);
                }

            }
            catch (Exception e)
            {
                Assert.Fail("Failed to start RTUEmu process: {0}", e.ToString());
            }
           
        }

        protected void StopRTUEmu()
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
                    Trace.WriteLine("Closing emulator  " + emulator.ProcessName + "  at process id " + emulator.Id);

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
                        Trace.WriteLine("   Simulator {0} has exited.", m_strEmulatorExe);
                    }

                    emulator.Dispose();
                }
            }
            catch (Exception e)
            {
                Assert.Fail("Caught exception closing simulator: {0}", e.ToString());
            }
            
        }
                
        protected bool TestBARExportFile()
        {
            string fileName1 = ExpectedResult;
            string fileName2 = (ExportLocation + BeamAnalysisText);
            
            bool bEqual = true;


            if (!File.Exists(fileName1))
            {
                Assert.Fail("File not found: " + (fileName1));
                return false;
            }

            if (!File.Exists(fileName2))
            {
                Assert.Fail("File not found: " + (fileName2));
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
                if (scanA[i].Contains("Pumping Unit :") && scanB[i].Contains("Pumping Unit :")) 
                                                                                                
                {
                     char[] delimiter1 = new char[] { ':' };
                     string[] thingsA = scanA[i].Split(delimiter1, StringSplitOptions.RemoveEmptyEntries);
                     string[] thingsB = scanB[i].Split(delimiter1, StringSplitOptions.RemoveEmptyEntries);
                     Assert.IsTrue(thingsA.Length == thingsB.Length, "Array length for line match failed");
                     Assert.IsTrue(thingsA[0] == thingsB[0]);
                     Assert.IsTrue(thingsA[1] == thingsB[1], "Pumping Unit mismatch  " + thingsA[1] + "  NOT EQUAL TO  " + thingsB[1]);
                     Assert.IsTrue(thingsA[2] == thingsB[2], "Card Type Test mismatch  " + thingsA[2] + "  NOT EQUAL TO  " + thingsB[2]);
                     Assert.IsTrue(thingsA[4] == thingsB[4], "Controller mismatch  " + thingsA[4] + "  NOT EQUAL TO  " + thingsB[4]);

                    Trace.WriteLine(scanA[i] + " !! " + scanB[i]);
                }
                else
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
            }

            Trace.WriteLine("##### END OF FILE COMPARE #####\n\n");



            return bEqual;

        }

        protected void TestBARExportWordDoc(string DocumentFilePath)
        {

            DataTable DT = new DataTable();

            string Doco = DocumentFilePath;

            #region GetUserDefinedValues

            string DownholeAnalysisMethod = ConfigurationManager.AppSettings.Get("DownholeAnalysisMethod");
            string ExistingCBT = ConfigurationManager.AppSettings.Get("ExistingCBT");
            string APITorqueMaxUp = ConfigurationManager.AppSettings.Get("APITorqueMax(Up)%");
            string APITorqueMaxUpK = ConfigurationManager.AppSettings.Get("APITorqueMax(Up)K");
            string OptimumCBT = ConfigurationManager.AppSettings.Get("OptimumCBT");
            string APITorqueMinUp = ConfigurationManager.AppSettings.Get("APITorqueMin(Up)%");
            string APITorqueMinUpK = ConfigurationManager.AppSettings.Get("APITorqueMin(Up)K");
            string APITorqueMaxDown = ConfigurationManager.AppSettings.Get("APITorqueMax(Down)%");
            string APITorqueMaxDownK = ConfigurationManager.AppSettings.Get("APITorqueMax(Down)K");
            string FAPmancal = ConfigurationManager.AppSettings.Get("FAPmancal");
            string APITorqueMinDown = ConfigurationManager.AppSettings.Get("APITorqueMin(Down)%");
            string APITorqueMinDownK = ConfigurationManager.AppSettings.Get("APITorqueMin(Down)K");
            string PerOfRating = ConfigurationManager.AppSettings.Get("%ofRating");
            string PIPmancal = ConfigurationManager.AppSettings.Get("PIPmancal");
            string APITorqueOptimumCBT = ConfigurationManager.AppSettings.Get("APITorqueOptimumCBT%");
            string APITorqueOptimumCBTK = ConfigurationManager.AppSettings.Get("APITorqueOptimumCBTK");


            string AllowableRangePSI1 = ConfigurationManager.AppSettings.Get("AllowableRangePSI1");
            string AllowableRangePSI2 = ConfigurationManager.AppSettings.Get("AllowableRangePSI2");
            string AllowableRangePSI3 = ConfigurationManager.AppSettings.Get("AllowableRangePSI3");
            string AllowableRangePSI4 = ConfigurationManager.AppSettings.Get("AllowableRangePSI4");
            string AllowableRangePSI5 = ConfigurationManager.AppSettings.Get("AllowableRangePSI5");
            string AllowableRangePSI6 = ConfigurationManager.AppSettings.Get("AllowableRangePSI6");
            string AllowableRangePSI7 = ConfigurationManager.AppSettings.Get("AllowableRangePSI7");
            string AllowableRangePSI8 = ConfigurationManager.AppSettings.Get("AllowableRangePSI8");

            string Allow1 = ConfigurationManager.AppSettings.Get("Allow%1");
            string Allow2 = ConfigurationManager.AppSettings.Get("Allow%2");
            string Allow3 = ConfigurationManager.AppSettings.Get("Allow%3");
            string Allow4 = ConfigurationManager.AppSettings.Get("Allow%4");
            string Allow5 = ConfigurationManager.AppSettings.Get("Allow%5");
            string Allow6 = ConfigurationManager.AppSettings.Get("Allow%6");
            string Allow7 = ConfigurationManager.AppSettings.Get("Allow%7");
            string Allow8 = ConfigurationManager.AppSettings.Get("Allow%8");

            string RodFluidLoadValue = ConfigurationManager.AppSettings.Get("RodFluidLoadValue");
            string BuoyRodsLoadValue = ConfigurationManager.AppSettings.Get("BuoyRodsLoadValue");
            string DryRodsLoadValue = ConfigurationManager.AppSettings.Get("DryRodsLoadValue");

            string SurfaceDisplacement = ConfigurationManager.AppSettings.Get("SurfaceDisplacement");
            string DownholeDisplacement = ConfigurationManager.AppSettings.Get("DownholeDisplacement");
            string TotalFluidDisplacement = ConfigurationManager.AppSettings.Get("TotalFluidDisplacement");
            string SurfaceVolEff = ConfigurationManager.AppSettings.Get("SurfaceVolEff");
            string DownholeVolEff = ConfigurationManager.AppSettings.Get("DownholeVolEff");
            string TotalFluidVolEff = ConfigurationManager.AppSettings.Get("TotalFluidVolEff");
            string SurfaceValue = ConfigurationManager.AppSettings.Get("SurfaceValue");
            string DownholeValue = ConfigurationManager.AppSettings.Get("DownholeValue");
            string TotalFluidValue = ConfigurationManager.AppSettings.Get("TotalFluidValue");

            string PolishedRodHP = ConfigurationManager.AppSettings.Get("PolishedRodHP%");
            string PolishedRodHPLoading = ConfigurationManager.AppSettings.Get("PolishedRodHPLoading");
            string Production = ConfigurationManager.AppSettings.Get("Production");

            string EstimatedDailyCost24 = ConfigurationManager.AppSettings.Get("EstimatedDailyCost24");
            string EstimatedDailyCostYest = ConfigurationManager.AppSettings.Get("EstimatedDailyCostYest");
            #endregion

            //quick and dirty , need a lot more work .. Assumes a lot of things that are not handled . 
            //Report must be in word and must be in the same format (document with same rows and columns etc) 
            // there is plenty of work to this that needs to be done . 
            // COMPLETE AND TOTAL HACK , NOT TO BE USED IN THE LONG TERMS OR ATLEAST IMPROVE IT !!!!!!!!

                
                using (DocX doc = DocX.Load(Doco))
                {
                    Trace.WriteLine("Opening Document and Checking only specific fields...Checking Now....\n");
                    for (int i = 0; i < doc.Paragraphs.Count; i++)
                    {
                        if (doc.Paragraphs[i].Text == ("Downhole Analysis Method :"))
                        {
                            if ((doc.Paragraphs[i + 1].Text == DownholeAnalysisMethod))
                            {
                                Trace.WriteLine(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + "................................OK");
                            }
                            else
                            {
                                Assert.Fail(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + "................................FAIL");

                            }
                        }

                       
                        if (doc.Paragraphs[i].Text == ("Existing CBT :"))
                        {
                            if ((doc.Paragraphs[i + 1].Text == ExistingCBT) && ((doc.Paragraphs[i + 2].Text == "Kinchlbs")))
                            {
                                Trace.WriteLine(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 2].Text + " " + " " + ".................................OK");
                            }
                            else
                            {
                                Assert.Fail(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 2].Text + " " + " " + ".................................FAIL");

                            }
                        }

                        else if (doc.Paragraphs[i].Text == ("API Torque Max (Up) :"))
                        {
                            if (doc.Paragraphs[i + 1].Text == APITorqueMaxUpK && doc.Paragraphs[i + 2].Text == "Kinchlbs" && doc.Paragraphs[i + 3].Text == APITorqueMaxUp)
                            {
                                Trace.WriteLine(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 2].Text + " " + doc.Paragraphs[i + 3].Text + " " + " " + "........................OK");
                            }
                            else
                            {
                                Assert.Fail(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 2].Text + " " + doc.Paragraphs[i + 3].Text + " " + " " + "........................FAIL");
                            }
           
                            
                        }

                        else if (doc.Paragraphs[i].Text == ("Optimum CBT :"))
                        {
                            if ((doc.Paragraphs[i + 1].Text == OptimumCBT) && ((doc.Paragraphs[i + 2].Text == "Kinchlbs")))
                            {
                                Trace.WriteLine(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 2].Text + " " + " " + "..................................OK");
                            }
                            else
                            {
                                Assert.Fail(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 2].Text + " " + " " + "..................................FAIL");

                            }
                        }
                        else if (doc.Paragraphs[i].Text == ("API Torque Min (Up) :"))
                        {
                            if (doc.Paragraphs[i + 1].Text == APITorqueMinUpK && doc.Paragraphs[i + 2].Text == "Kinchlbs" && doc.Paragraphs[i + 3].Text == APITorqueMinUp)
                            {
                                Trace.WriteLine(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 2].Text + " " + doc.Paragraphs[i + 3].Text + " " + " " + ".......................OK");
                            }
                            else
                            {
                                Assert.Fail(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 2].Text + " " + doc.Paragraphs[i + 3].Text + " " + " " + ".......................FAIL");
                            }
                        }
                        else if (doc.Paragraphs[i].Text == ("API Torque Max (Down) :"))
                        {
                            if (doc.Paragraphs[i + 1].Text == APITorqueMaxDownK && doc.Paragraphs[i + 2].Text == "Kinchlbs" && doc.Paragraphs[i + 3].Text == APITorqueMaxDown)
                            {
                                Trace.WriteLine(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 2].Text + " " + doc.Paragraphs[i + 3].Text + " " + " " + "......................OK");
                            }
                            else
                            {
                                Assert.Fail(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 2].Text + " " + doc.Paragraphs[i + 3].Text + " " + " " + "......................FAIL");
                            }
                        }
                        else if (doc.Paragraphs[i].Text == ("FAP (man/cal) :"))
                        {
                            if ((doc.Paragraphs[i + 1].Text == FAPmancal) && ((doc.Paragraphs[i + 2].Text == "feet/feet")))
                            {
                                Trace.WriteLine(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 2].Text + " " + " " + ".............................OK");
                            }
                            else
                            {
                                Assert.Fail(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 2].Text + " " + " " + ".............................FAIL");

                            }
                        }
                        else if (doc.Paragraphs[i].Text == ("API Torque Min (Down) :"))
                        {
                            if (doc.Paragraphs[i + 1].Text == APITorqueMinDownK && doc.Paragraphs[i + 2].Text == "Kinchlbs" && doc.Paragraphs[i + 3].Text == APITorqueMinDown)
                            {
                                Trace.WriteLine(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 2].Text + " " + doc.Paragraphs[i + 3].Text + " " + " " + "......................OK");
                            }
                            else
                            {
                                Assert.Fail(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 2].Text + " " + doc.Paragraphs[i + 3].Text + " " + " " + "......................FAIL");
                            }
                        }
                        else if (doc.Paragraphs[i].Text == ("(%) of Rating :"))
                        {
                            if ((doc.Paragraphs[i + 1].Text == PerOfRating) && ((doc.Paragraphs[i + 2].Text == "")))
                            {
                                Trace.WriteLine(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 2].Text + " " + " " + "............................................OK");
                            }
                            else
                            {
                                Assert.Fail(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 2].Text + " " + " " + "............................................FAIL");

                            }
                        }
                        else if (doc.Paragraphs[i].Text == ("PIP (man/cal) :"))
                        {
                            if ((doc.Paragraphs[i + 1].Text == PIPmancal) && ((doc.Paragraphs[i + 2].Text == "PSI/PSI")))
                            {
                                Trace.WriteLine(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 2].Text + " " + " " + "...............................OK");
                            }
                            else
                            {
                                Assert.Fail(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 2].Text + " " + " " + "...............................FAIL");

                            }
                        }
                        else if (doc.Paragraphs[i].Text == ("API Torque W/ Optimum CBT :"))
                        {
                            if (doc.Paragraphs[i + 1].Text == APITorqueOptimumCBTK && doc.Paragraphs[i + 2].Text == "Kinchlbs" && doc.Paragraphs[i + 3].Text == APITorqueOptimumCBT)
                            {
                                Trace.WriteLine(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 2].Text + " " + doc.Paragraphs[i + 3].Text + " " + " " + "..................OK");
                            }
                            else
                            {
                                Assert.Fail(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 2].Text + " " + doc.Paragraphs[i + 3].Text + " " + " " + "..................FAIL");
                            }
                        }
                        else if (doc.Paragraphs[i].Text == ("Allowable Range (PSI)") && doc.Paragraphs[i + 1].Text == ("(%) Allow"))
                        {
                            int myint = 28;
                            List<string> AR = new List<string>();
                            List<string> PerAllow = new List<string>();
                            List<string> ART = new List<string>();
                            List<string> PerAllowT = new List<string>();

                            ART.Add(AllowableRangePSI1); ART.Add(AllowableRangePSI2); ART.Add(AllowableRangePSI3); ART.Add(AllowableRangePSI4); ART.Add(AllowableRangePSI5); ART.Add(AllowableRangePSI6); ART.Add(AllowableRangePSI7); ART.Add(AllowableRangePSI8);
                            PerAllowT.Add(Allow1); PerAllowT.Add(Allow2); PerAllowT.Add(Allow3); PerAllowT.Add(Allow4); PerAllowT.Add(Allow5); PerAllowT.Add(Allow6); PerAllowT.Add(Allow7); PerAllowT.Add(Allow8);

                            DT.Columns.Add(doc.Paragraphs[i].Text, typeof(string));
                            DT.Columns.Add(doc.Paragraphs[i + 1].Text, typeof(string));
                           
                            DT.Rows.Add(doc.Paragraphs[i + myint].Text, doc.Paragraphs[i + myint + 1].Text);
                            for (int j = 1; j <= 7; j++)
                            {
                                int adder = 14;
                                adder = adder * j;
                                DT.Rows.Add(doc.Paragraphs[i + myint + adder].Text, doc.Paragraphs[i + myint + adder + 1].Text);


                            }
                            foreach (DataRow row in DT.Rows)
                            {
                              
                                    AR.Add(row.ItemArray[0].ToString());
                                    PerAllow.Add(row.ItemArray[1].ToString());
                                
                            }
                            Trace.WriteLine("\n" + doc.Paragraphs[i].Text + "   " + doc.Paragraphs[i + 1].Text);
                            
                            if (AR.Count != ART.Count ) { throw new Exception("Expected List count not same as List count from document"); }
                            for (int a = 0; a < AR.Count; a++)
                            {
                                if (AR[a] == ART[a] && PerAllow[a] == PerAllowT[a])
                                {
                                    Trace.WriteLine(AR[a] + "                     " + PerAllow[a] + "  ...................................OK");
                                }
                                else
                                {
                                    Assert.Fail(AR[a] + "                    " + PerAllow[a] + "  ....................................FAIL");
                                }
                            }
                            Trace.WriteLine("\n");
                        }
                          
                        else if (doc.Paragraphs[i].Text == ("Rods + Fluid"))
                        {
                            if ((doc.Paragraphs[i + 1].Text == RodFluidLoadValue))
                            {
                                Trace.WriteLine(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + "..............................................OK");
                            }
                            else
                            {
                                Assert.Fail(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + "..............................................FAIL");

                            }
                        }
                        else if (doc.Paragraphs[i].Text == ("Buoy Rods"))
                        {
                            if ((doc.Paragraphs[i + 1].Text == BuoyRodsLoadValue))
                            {
                                Trace.WriteLine(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + ".................................................OK");
                            }
                            else
                            {
                                Assert.Fail(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + ".................................................FAIL");

                            }
                        }

                        else if (doc.Paragraphs[i].Text == ("Dry Rods"))
                        {
                            if ((doc.Paragraphs[i + 1].Text == DryRodsLoadValue))
                            {
                                Trace.WriteLine(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + "..................................................OK");
                            }
                            else
                            {
                                Assert.Fail(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + "..................................................FAIL");

                            }
                        }
                        else if (doc.Paragraphs[i].Text == ("Surface"))
                        {
                            if (doc.Paragraphs[i + 3].Text == SurfaceDisplacement && doc.Paragraphs[i + 5].Text == SurfaceVolEff && doc.Paragraphs[i + 7].Text == SurfaceValue)
                            {
                                Trace.WriteLine(doc.Paragraphs[i + 3].Text + " " + " " + doc.Paragraphs[i + 5].Text + " " + " " + doc.Paragraphs[i + 7].Text + " " + " " + ".................................................OK");
                            }
                            else
                            {
                                Assert.Fail(doc.Paragraphs[i + 3].Text + " " + " " + doc.Paragraphs[i + 5].Text + " " + " " + doc.Paragraphs[i + 7].Text + " " + " " + ".................................................FAIL");
                            }
                        }
                        else if (doc.Paragraphs[i].Text == ("DownHole"))
                        {
                            if (doc.Paragraphs[i + 3].Text == DownholeDisplacement && doc.Paragraphs[i + 5].Text == DownholeVolEff && doc.Paragraphs[i + 7].Text == DownholeValue)
                            {
                                Trace.WriteLine(doc.Paragraphs[i + 3].Text + " " + " " + doc.Paragraphs[i + 5].Text + " " + " " + doc.Paragraphs[i + 7].Text + " " + " " + ".................................................OK");
                            }
                            else
                            {
                                Assert.Fail(doc.Paragraphs[i + 3].Text + " " + " " + doc.Paragraphs[i + 5].Text + " " + " " + doc.Paragraphs[i + 7].Text + " " + " " + ".................................................FAIL");
                            }
                        }
                        else if (doc.Paragraphs[i].Text == ("Total Fluid"))
                        {
                            if (doc.Paragraphs[i + 3].Text == TotalFluidDisplacement && doc.Paragraphs[i + 5].Text == TotalFluidVolEff && doc.Paragraphs[i + 7].Text == TotalFluidValue)
                            {
                                Trace.WriteLine(doc.Paragraphs[i + 3].Text + " " + " " + doc.Paragraphs[i + 5].Text + " " + " " + doc.Paragraphs[i + 7].Text + " " + " " + ".................................................OK");
                            }
                            else
                            {
                                Assert.Fail(doc.Paragraphs[i + 3].Text + " " + " " + doc.Paragraphs[i + 5].Text + " " + " " + doc.Paragraphs[i + 7].Text + " " + " " + ".................................................FAIL");
                            }
                        }
                        else if (doc.Paragraphs[i].Text == ("Polished Rod HP :"))
                        {
                            if ((doc.Paragraphs[i + 1].Text == PolishedRodHP) && ((doc.Paragraphs[i + 3].Text == PolishedRodHPLoading)))
                            {
                                Trace.WriteLine(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 3].Text + " " + " " + "..................................OK");
                            }
                            else
                            {
                                Assert.Fail(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 3].Text + " " + " " + "...............................FAIL");

                            }
                        }
                        else if (doc.Paragraphs[i].Text == ("Production :"))
                        {
                            if (doc.Paragraphs[i + 1].Text == Production)
                            {
                                Trace.WriteLine(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + "..............................................OK");
                            }
                            else
                            {
                                Assert.Fail(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + "..............................................FAIL");
                            }
                        }
                        else if (doc.Paragraphs[i].Text == ("Estimated Daily Cost :"))
                        {
                            if ((doc.Paragraphs[i + 1].Text == EstimatedDailyCost24) && ((doc.Paragraphs[i + 3].Text == EstimatedDailyCostYest)))
                            {
                                Trace.WriteLine(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 3].Text + " " + " " + ".............................OK");
                            }
                            else
                            {
                                Assert.Fail(doc.Paragraphs[i].Text + " " + " " + doc.Paragraphs[i + 1].Text + " " + " " + doc.Paragraphs[i + 3].Text + " " + " " + ".............................FAIL");

                            }
                        }

                    }
                    Trace.WriteLine("\n\nDone Checking Document Results Above....");
                  
                   
                }

           
                   
         
        
        }


    }
}
