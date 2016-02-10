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
using System.Xml.Linq;

namespace IntegrationTests.Commands
{
    [TestClass]
    public class CommandsWF : TestDriverBase
    {
          
		[TestCategory("WTCS7X"), TestMethod]
        public void CommandsTestWTCS7X()
        {
            try
            {
                CheckIfLowisExist();
                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("WTCS7X");
                }

                
                StartRTUEmu();
                CreateMonitorWells("WTCS7X");
              
                SetCSVOutputDir("MONITOR");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
  
                TestContext.WriteLine("\n\n");

                //// Native Well

                CommandRequest("NATIVE Seperator", "stop_separator", "MONITOR"); // stopseperator well #1
                GetCurrentStatus(1, "WTCS7X"); //issue current status since LOWIS in this case does not 
                VerifyState(1, "GN2.csv", "stopseperator", "WTCS7X"); // verify stopseperator state

                CommandRequest("NATIVE Seperator", "start_sep", "MONITOR"); // runseperator well #1
                VerifyState(1, "GN2.csv", "runseperator", "WTCS7X"); // verify runseperator state

                CommandRequest("NATIVE Seperator", "stop_test", "MONITOR"); // stoptest well #1
                GetCurrentStatus(1, "WTCS7X"); //issue current status since LOWIS in this case does not 
                VerifyState(1, "GN2.csv", "stoptest", "WTCS7X"); // verify stoptest state

                CommandRequest("NATIVE Seperator", "EmergencyShutDown", "MONITOR"); // EmergencyShutDown well #1
                GetCurrentStatus(1, "WTCS7X"); //issue current status since LOWIS in this case does not 
                VerifyState(1, "GN2.csv", "EmergencyShutDown", "WTCS7X"); // verify EmergencyShutDown state


            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("WTCS7X");
                }
            }
        }

		[TestCategory("SUGNMB"), TestMethod]
        public void CommandsTestSUGNMB()
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
              

                SetCSVOutputDir("ESP");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
         
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "ESP"); // stop well #1
                VerifyState(1, "SU1.csv", "stop", "REDUNI"); // verify stop state

                CommandRequest(1, 1, "ESP"); // start well #1
                VerifyState(1, "SU1.csv", "run", "REDUNI"); // verify start state

            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("SUGNMB");
                }
            }
        }

        [TestCategory("VORTEX"), TestMethod]
        public void CommandsTestVORTEX()
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
             
                SetCSVOutputDir("ESP");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
             
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "ESP"); // stop well #1
                VerifyState(1, "SU1.csv", "stop", "VORTEX"); // verify stop state

                CommandRequest(1, 1, "ESP"); // start well #1
                VerifyState(1, "SU1.csv", "run", "VORTEX"); // verify start state

             
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("VORTEX");
                }
            }
        }

        [TestCategory("SPDSTR"), TestMethod]
        public void CommandsTestSPDSTR()
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
               
                SetCSVOutputDir("ESP");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
             
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "ESP"); // stop well #1
                VerifyState(1, "SU1.csv", "stop", "SPDSTR"); // verify stop state

                CommandRequest(1, 1, "ESP"); // start well #1
                VerifyState(1, "SU1.csv", "run", "SPDSTR"); // verify start state

            
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("SPDSTR");
                }
            }
        }

        [TestCategory("ROBICN"), TestMethod]
        public void CommandsTestROBICN()
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
           
                SetCSVOutputDir("ESP");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
           
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "ESP"); // stop well #1
                VerifyState(1, "SU1.csv", "stop", "ROBICN"); // verify stop state

                CommandRequest(1, 1, "ESP"); // start well #1
                VerifyState(1, "SU1.csv", "run", "ROBICN"); // verify start state

            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("ROBICN");
                }
            }
        }
        [TestCategory("KLT595"), TestMethod]
        public void CommandsTestKLT595()
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
           
                SetCSVOutputDir("ESP");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
            
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "ESP"); // stop well #1
                VerifyState(1, "SU1.csv", "stop", "KLT595"); // verify stop state

                CommandRequest(1, 1, "ESP"); // start well #1
                VerifyState(1, "SU1.csv", "run", "KLT595"); // verify start state

               
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("KLT595");
                }
            }
        }

        [TestCategory("KELTRN"), TestMethod]
        public void CommandsTestKELTRN()
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
        

                SetCSVOutputDir("ESP");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
       
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "ESP"); // stop well #1
                VerifyState(1, "SU1.csv", "stop", "KELTRN"); // verify stop state

                CommandRequest(1, 1, "ESP"); // start well #1
                VerifyState(1, "SU1.csv", "run", "KELTRN"); // verify start state

               
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("KELTRN");
                }
            }
        }

        [TestCategory("GCSVFD"), TestMethod]
        public void CommandsTestGCSVFD()
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
          
                SetCSVOutputDir("ESP");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
           
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "ESP"); // stop well #1
                VerifyState(1, "SU1.csv", "stop", "GCS"); // verify stop state

                CommandRequest(1, 1, "ESP"); // start well #1
                VerifyState(1, "SU1.csv", "run", "GCS"); // verify start state

           
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("GCSVFD");
                }
            }
        }

        [TestCategory("GCSV84"), TestMethod]
        public void CommandsTestGCSV84()
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
               

                SetCSVOutputDir("ESP");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
          
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "ESP"); // stop well #1
                VerifyState(1, "SU1.csv", "stop", "GCS"); // verify stop state

                CommandRequest(1, 1, "ESP"); // start well #1
                VerifyState(1, "SU1.csv", "run", "GCS"); // verify start state

              
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("GCSV84");
                }
            }
        }

        [TestCategory("GCSV72"), TestMethod]
        public void CommandsTestGCSV72()
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
             

                SetCSVOutputDir("ESP");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
          
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "ESP"); // stop well #1
                VerifyState(1, "SU1.csv", "stop", "GCS"); // verify stop state

                CommandRequest(1, 1, "ESP"); // start well #1
                VerifyState(1, "SU1.csv", "run", "GCS"); // verify start state

           
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("GCSV72");
                }
            }
        }

        [TestCategory("VTXICM"), TestMethod]
        public void CommandsTestVTXICM()
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
             

                SetCSVOutputDir("ESP");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
       
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "ESP"); // stop well #1
                VerifyState(1, "SU1.csv", "stop", "VTXICM"); // verify stop state

                CommandRequest(1, 1, "ESP"); // start well #1
                VerifyState(1, "SU1.csv", "run", "VTXICM"); // verify start state

          
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("VTXICM");
                }
            }
        }

        [TestCategory("YSKAWA"), TestMethod]
        public void CommandsTestYSKAWA()
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
          
                SetCSVOutputDir("PCP");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
              
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "PCP"); // stop well #1
                VerifyState(1, "PC1.csv", "stop", "YSKAWA"); // verify stop state

                CommandRequest(1, 1, "PCP"); // start well #1
                VerifyState(1, "PC1.csv", "run", "YSKAWA"); // verify start state

         
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("YSKAWA");
                }
            }
        }

        [TestCategory("REDUNIFSONLY"), TestMethod]
        public void CommandsTestREDUNIFSONLY()
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
            
                SetCSVOutputDir("ESP");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
           
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "ESP"); // stop well #1
                VerifyState(1, "SU1.csv", "stop", "REDUNI"); // verify stop state

                CommandRequest(1, 1, "ESP"); // start well #1
                VerifyState(1, "SU1.csv", "run", "REDUNI"); // verify start state

            
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
        public void CommandsTestREDUNIFSWPHX()
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
         

                SetCSVOutputDir("ESP");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
              
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "ESP"); // stop well #1
                VerifyState(1, "SU1.csv", "stop", "REDUNI"); // verify stop state

                CommandRequest(1, 1, "ESP"); // start well #1
                VerifyState(1, "SU1.csv", "run", "REDUNI"); // verify start state

              
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
        public void CommandsTestREDUNIVSONLY()
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
             

                SetCSVOutputDir("ESP");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
             
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "ESP"); // stop well #1
                VerifyState(1, "SU1.csv", "stop", "REDUNI"); // verify stop state

                CommandRequest(1, 1, "ESP"); // start well #1
                VerifyState(1, "SU1.csv", "run", "REDUNI"); // verify start state

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
        public void CommandsTestREDUNIVSWPHX()
        {
            try
            {
                CheckIfLowisExist();
                if (ConfigurationManager.AppSettings.Get("CleanBeforeStart") == "true")
                {
                    Cleanup("FSWPHX");
                }

                StartRTUEmu();
                CreateESPWells("VSWPHX");
            
                SetCSVOutputDir("ESP");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
             
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "ESP"); // stop well #1
                VerifyState(1, "SU1.csv", "stop", "REDUNI"); // verify stop state

                CommandRequest(1, 1, "ESP"); // start well #1
                VerifyState(1, "SU1.csv", "run", "REDUNI"); // verify start state

            
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

        [TestCategory("UNILRP"), TestMethod]
        public void CommandsTestUNILRP()
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
               

                SetCSVOutputDir("BEAM");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
              
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "BEAM"); // stop well #1
                WaitForOutputFile("BE1.csv");
                VerifyState(1, "BE1.csv", "stop", "UNILRP"); // verify stop state

                CommandRequest(1, 1, "BEAM"); // start well #1
                WaitForOutputFile("BE1.csv");
                VerifyState(1, "BE1.csv", "run", "UNILRP"); // verify start state


                // test reset alarms command
                TestResetAlarmCommand();               
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("UNILRP");
                    File.Delete(Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("UXRPRESETALARM"))));
                }
            }
        }

        [TestCategory("UNISRPV100"), TestMethod]
        public void CommandsTestUNISRPV100()
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
              

                SetCSVOutputDir("BEAM");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
                
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "BEAM"); // stop well #1
                WaitForOutputFile("BE1.csv");
                VerifyState(1, "BE1.csv", "stop", "UV100"); // verify stop state

                CommandRequest(1, 1, "BEAM"); // start well #1
                WaitForOutputFile("BE1.csv");
                VerifyState(1, "BE1.csv", "run", "UV100"); // verify start state


                // test reset alarms command
                TestResetAlarmCommand();
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("UNISRPV100");
                    File.Delete(Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("UXRPRESETALARM"))));
                }
            }
        }

        [TestCategory("UNISRPV110"), TestMethod]
        public void CommandsTestUNISRPV110()
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
           
                SetCSVOutputDir("BEAM");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
          
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "BEAM"); // stop well #1
                WaitForOutputFile("BE1.csv");
                VerifyState(1, "BE1.csv", "stop", "UV110"); // verify stop state

                CommandRequest(1, 1, "BEAM"); // start well #1
                WaitForOutputFile("BE1.csv");
                VerifyState(1, "BE1.csv", "run", "UV110"); // verify start state

               

                // test reset alarms command
                TestResetAlarmCommand();
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("UNISRPV110");
                    File.Delete(Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("UXRPRESETALARM"))));
                }
            }
        }

        [TestCategory("UNISRPV200"), TestMethod]
        public void CommandsTestUNISRPV200()
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
         

                SetCSVOutputDir("BEAM");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
              TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "BEAM"); // stop well #1
                WaitForOutputFile("BE1.csv");
                VerifyState(1, "BE1.csv", "stop", "UV200"); // verify stop state

                CommandRequest(1, 1, "BEAM"); // start well #1
                WaitForOutputFile("BE1.csv");
                VerifyState(1, "BE1.csv", "run", "UV200"); // verify start state

       

                // test reset alarms command
                TestResetAlarmCommand();
            }
            finally // we always want RTUEmu to exit
            {
                StopRTUEmu();
                if (ConfigurationManager.AppSettings.Get("CleanWhenComplete") == "true")
                {
                    Cleanup("UNISRPV200");
                    File.Delete(Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("UXRPRESETALARM"))));
                }                
            }
        }

        [TestCategory("AEPOC2"), TestMethod]
        public void CommandsTestAEPOC2()
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
          

                SetCSVOutputDir("BEAM");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
               
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "BEAM"); // stop well #1
                VerifyState(1, "BE1.csv", "stop", "AEPOC2"); // verify stop state

                CommandRequest(1, 1, "BEAM"); // start well #1
                VerifyState(1, "BE1.csv", "start", "AEPOC2"); // verify start state

              

                // Native Well

                CommandRequest(1, 3, "BEAM"); // put well #1 in poc state
                VerifyState(1, "BE1.csv", "poc", "AEPOC2"); // verify poc state


                CommandRequest(1, 4, "BEAM"); // start well #1 ipoc state
                VerifyState(1, "BE1.csv", "ipoc", "AEPOC2"); // verify ipoc state

                // Native Well

                CommandRequest(1, 7, "BEAM"); // put well #1 in mtmrrun state
                VerifyState(1, "BE1.csv", "mtmrrun", "AEPOC2"); // verify mtmrrun state

                // Native Well

                CommandRequest(1, 8, "BEAM"); // put well #1 in mtmridle state
                VerifyState(1, "BE1.csv", "mtmridle", "AEPOC2"); // verify mtmridle state

                // Native Well

                CommandRequest(1, 9, "BEAM"); // put well #1 in htmr state
                VerifyState(1, "BE1.csv", "htmr", "AEPOC2"); // verify htmr state

              

                // Native Well

                CommandRequest(1, 10, "BEAM"); // put well #1 in stmr state
                VerifyState(1, "BE1.csv", "stmr", "AEPOC2"); // verify stmr state

              
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

        [TestCategory("EPICFS"), TestMethod]
        public void CommandsTestEPICFS()
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
        


                SetCSVOutputDir("BEAM");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
           
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "BEAM"); // stop well #1
                VerifyState(1, "BE1.csv", "stop", "EPICRP"); // verify stop state

                CommandRequest(1, 1, "BEAM"); // run well #1
                VerifyState(1, "BE1.csv", "running", "EPICRP"); // verify run state

               


                // Native Well

                CommandRequest(1, 3, "BEAM"); // put well #1 in idle state
                VerifyState(1, "BE1.csv", "idle", "EPICRP"); // verify idle state

                CommandRequest(1, 4, "BEAM"); // start well #1
                VerifyState(1, "BE1.csv", "start", "EPICRP"); // verify start state

                           
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
        public void CommandsTestEPICVF()
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
               


                SetCSVOutputDir("BEAM");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
          
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "BEAM"); // stop well #1
                VerifyState(1, "BE1.csv", "stop", "EPICRP"); // verify stop state

                CommandRequest(1, 1, "BEAM"); // run well #1
                VerifyState(1, "BE1.csv", "running", "EPICRP"); // verify run state

         

                // Native Well

                CommandRequest(1, 3, "BEAM"); // put well #1 in idle state
                VerifyState(1, "BE1.csv", "idle", "EPICRP"); // verify idle state

                CommandRequest(1, 4, "BEAM"); // start well #1
                VerifyState(1, "BE1.csv", "start", "EPICRP"); // verify start state

            
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
        public void CommandsTestEPICLM()
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
           


                SetCSVOutputDir("BEAM");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
               
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "BEAM"); // stop well #1
                VerifyState(1, "BE1.csv", "stop", "EPICRP"); // verify stop state

                CommandRequest(1, 1, "BEAM"); // run well #1
                VerifyState(1, "BE1.csv", "running", "EPICRP"); // verify run state

             

                // Native Well

                CommandRequest(1, 3, "BEAM"); // put well #1 in idle state
                VerifyState(1, "BE1.csv", "idle", "EPICRP"); // verify idle state

                CommandRequest(1, 4, "BEAM"); // start well #1
                VerifyState(1, "BE1.csv", "start", "EPICRP"); // verify start state

            

                // Native Well
                //File.Copy(ConfigurationManager.AppSettings.Get("EPICLMRDDHCfgFile"), Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("EPICLMRDDHCfgFile"))), true);
                //File.SetAttributes(Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("EPICLMRDDHCfgFile"))), FileAttributes.Normal); // file originally read only

                //File.Copy(ConfigurationManager.AppSettings.Get("EPICLMExpDH"), Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("EPICLMExpDH"))), true);
                //File.SetAttributes(Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("EPICLMExpDH"))), FileAttributes.Normal); // file originally read only
                
                ConfigBEWellProps(1);
                //CommandRequest(1, 15, "BEAM"); // download downhole configuration for well #1
                //RTURead("EPICRP", 1, strNativeRead, "csarg", System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("EPICLMRDDHCfgFile"))); // read the same registers written in previous command
				
				//System.Threading.Thread.Sleep(10000); // wait to ensure Native Read file has been outputted.
               
                // verify values written in native are same or not
                //Assert.IsTrue(CompareOutputFiles(strNativeRead, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("EPICLMExpDH")), System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("EPICLMRDDHCfgFile"))));

                //Test Downhole Pumpoff Fillage Value command
                TestDownholePumpoffFillageCommand(1); // Native
                
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

        [TestCategory("SAMFS"), TestMethod]
        public void CommandsTestSAMFS()
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
               

                SetCSVOutputDir("BEAM");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
              
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "BEAM"); // stop well #1
                VerifyState(1, "BE1.csv", "stop", "SAMRP2"); // verify stop state

                CommandRequest(1, 1, "BEAM"); // start well #1
                VerifyState(1, "BE1.csv", "start", "SAMRP2"); // verify start state

               

                // Native Well

                CommandRequest(1, 3, "BEAM"); // put well #1 in POC state
                VerifyState(1, "BE1.csv", "poc", "SAMRP2");

                CommandRequest(1, 4, "BEAM"); // put well #1 in ipoc state
                VerifyState(1, "BE1.csv", "ipoc", "SAMRP2");

              

                // Native Well

                CommandRequest(1, 7, "BEAM"); // put well #1 in mtmrrun state
                VerifyState(1, "BE1.csv", "mtmrrun", "SAMRP2");

               

                // Native Well
                //File.Copy(ConfigurationManager.AppSettings.Get("SAMRP2RDDHCfgFile"), Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2RDDHCfgFile"))), true);
                //File.SetAttributes(Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2RDDHCfgFile"))), FileAttributes.Normal); // file originally read only

                 //File.Copy(ConfigurationManager.AppSettings.Get("SAMRP2ExpDH"), Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2ExpDH"))), true);
				   //File.SetAttributes(Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2ExpDH"))), FileAttributes.Normal); // file originally read only
				
				//ConfigBEWellProps(1);
                //CommandRequest(1, 15, "BEAM"); // download downhole configuration for well #1
                //RTURead("SAMRP2", 1, strNativeRead, "csarg", System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2RDDHCfgFile"))); // read the same registers written in previous command

                // verify values written in native  are same or not
                 //Assert.IsTrue(CompareOutputFiles(strNativeRead, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2ExpDH")), System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2RDDHCfgFile")))); 
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
        public void CommandsTestSAMFW()
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
               

                SetCSVOutputDir("BEAM");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
              
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "BEAM"); // stop well #1
                VerifyState(1, "BE1.csv", "stop", "SAMRP2"); // verify stop state

                CommandRequest(1, 1, "BEAM"); // start well #1
                VerifyState(1, "BE1.csv", "start", "SAMRP2"); // verify start state

              

               

                // Native Well

                CommandRequest(1, 3, "BEAM"); // put well #1 in POC state
                VerifyState(1, "BE1.csv", "poc", "SAMRP2");

                CommandRequest(1, 4, "BEAM"); // put well #1 in ipoc state
                VerifyState(1, "BE1.csv", "ipoc", "SAMRP2");

               

               

                // Native Well

                CommandRequest(1, 7, "BEAM"); // put well #1 in mtmrrun state
                VerifyState(1, "BE1.csv", "mtmrrun", "SAMRP2");

               

                // Native Well
                //File.Copy(ConfigurationManager.AppSettings.Get("SAMRP2RDDHCfgFile"), Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2RDDHCfgFile"))), true);
                //File.SetAttributes(Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2RDDHCfgFile"))), FileAttributes.Normal); // file originally read only

                //File.Copy(ConfigurationManager.AppSettings.Get("SAMRP2ExpDH"), Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2ExpDH"))), true);
                //File.SetAttributes(Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2ExpDH"))), FileAttributes.Normal); // file originally read only
				
				//ConfigBEWellProps(1);
                //CommandRequest(1, 15, "BEAM"); // download downhole configuration for well #1
                //RTURead("SAMRP2", 1, strNativeRead, "csarg", System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2RDDHCfgFile"))); // read the same registers written in previous command

               
                // verify values written in native  are same or not
                //Assert.IsTrue(CompareOutputFiles(strNativeRead, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2ExpDH")), System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2RDDHCfgFile"))));  
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
        public void CommandsTestSAMVS()
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
             

                SetCSVOutputDir("BEAM");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
               
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "BEAM"); // stop well #1
                VerifyState(1, "BE1.csv", "stop", "SAMRP2"); // verify stop state

                CommandRequest(1, 1, "BEAM"); // start well #1
                VerifyState(1, "BE1.csv", "start", "SAMRP2"); // verify start state

           

                // Native Well

                CommandRequest(1, 3, "BEAM"); // put well #1 in POC state
                VerifyState(1, "BE1.csv", "poc", "SAMRP2");

                CommandRequest(1, 4, "BEAM"); // put well #1 in ipoc state
                VerifyState(1, "BE1.csv", "ipoc", "SAMRP2");

             

                // Native Well

                CommandRequest(1, 7, "BEAM"); // put well #1 in mtmrrun state
                VerifyState(1, "BE1.csv", "mtmrrun", "SAMRP2");

             

                // Native Well
                //File.Copy(ConfigurationManager.AppSettings.Get("SAMRP2RDDHCfgFile"), Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2RDDHCfgFile"))), true);
                //File.SetAttributes(Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2RDDHCfgFile"))), FileAttributes.Normal); // file originally read only

                //File.Copy(ConfigurationManager.AppSettings.Get("SAMRP2ExpDH"), Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2ExpDH"))), true);
                //File.SetAttributes(Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2ExpDH"))), FileAttributes.Normal); // file originally read only
				
				//ConfigBEWellProps(1);
                //CommandRequest(1, 15, "BEAM"); // download downhole configuration for well #1
                //RTURead("SAMRP2", 1, strNativeRead, "csarg", System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2RDDHCfgFile"))); // read the same registers written in previous command
                // verify values written in native are same or not
                //Assert.IsTrue(CompareOutputFiles(strNativeRead, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2ExpDH")), System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2RDDHCfgFile"))));            
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
        public void CommandsTestSAMVW()
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
             

                SetCSVOutputDir("BEAM");

                TestContext.WriteLine("\n\n");
                TestContext.WriteLine("Well #1 is the Native Well.");
               
                TestContext.WriteLine("\n\n");

                // Native Well

                CommandRequest(1, 2, "BEAM"); // stop well #1
                VerifyState(1, "BE1.csv", "stop", "SAMRP2"); // verify stop state

                CommandRequest(1, 1, "BEAM"); // start well #1
                VerifyState(1, "BE1.csv", "start", "SAMRP2"); // verify start state

               

                

                // Native Well

                CommandRequest(1, 3, "BEAM"); // put well #1 in POC state
                VerifyState(1, "BE1.csv", "poc", "SAMRP2");

                CommandRequest(1, 4, "BEAM"); // put well #1 in ipoc state
                VerifyState(1, "BE1.csv", "ipoc", "SAMRP2");

               
              

                // Native Well

                CommandRequest(1, 7, "BEAM"); // put well #1 in mtmrrun state
                VerifyState(1, "BE1.csv", "mtmrrun", "SAMRP2");

              

                // Native Well
                //File.Copy(ConfigurationManager.AppSettings.Get("SAMRP2RDDHCfgFile"), Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2RDDHCfgFile"))), true);
                //File.SetAttributes(Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2RDDHCfgFile"))), FileAttributes.Normal); // file originally read only

                //File.Copy(ConfigurationManager.AppSettings.Get("SAMRP2ExpDH"), Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2ExpDH"))), true);
                //File.SetAttributes(Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2ExpDH"))), FileAttributes.Normal); // file originally read only
				
				//ConfigBEWellProps(1);
                //CommandRequest(1, 15, "BEAM"); // download downhole configuration for well #1
                //RTURead("SAMRP2", 1, strNativeRead, "csarg", System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2RDDHCfgFile"))); // read the same registers written in previous command

                 // verify values written in native  are same or not
                 //Assert.IsTrue(CompareOutputFiles(strNativeRead, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2ExpDH")), System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("SAMRP2RDDHCfgFile"))));
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

    }

    [TestClass]
    public class TestDriverBase
    {
        public  TestContext testContextInstance;
        public  TestContext TestContext
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
       

        public string m_strLiftDir = GetLiftRunFolder();
        public  string m_strEmulatorExe = "RTUEmu.exe";
        public  int wellCount = 40;
        public  int NumberofWells = Convert.ToInt32(ConfigurationManager.AppSettings.Get("NumberofWells"));
        public  string strNativeRead = "NativeRead.xml";
        public  string strNativeWrite = "NativeWrite.xml";
        public  string strHostName = Dns.GetHostName();
      

        public void RTURead(string rtuType, int wellNumber, string outputFile, string dataType, string reqDef)
        {
            string strRTUReadWrite = Path.Combine(m_strLiftDir, "RTUReadWriteConsole.exe");
            string strReqDefXML = Path.Combine(m_strLiftDir, reqDef);
            string strOutputFile = Path.Combine(m_strLiftDir, outputFile);
            string strHostName = Dns.GetHostName();

            
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
                        Process rtureadwrite = new Process();
                        rtureadwrite.StartInfo.UseShellExecute = true;
                        rtureadwrite.StartInfo.FileName = strRTUReadWrite;
                        rtureadwrite.StartInfo.Arguments = String.Format("/host:" + strHostName + " /pipe:beamdb\\cs_beamp /wellnum:" + wellNumber + " /reqdef:" + strReqDefXML + " /output:" + strOutputFile + " /bufferdatatype:" + dataType);

                        TestContext.WriteLine(strRTUReadWrite + " " + rtureadwrite.StartInfo.Arguments);
                        Assert.IsTrue(rtureadwrite.Start());
                        rtureadwrite.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;

                case "AE6008":
                    try
                    {
                        Process rtureadwrite = new Process();
                        rtureadwrite.StartInfo.UseShellExecute = true;
                        rtureadwrite.StartInfo.FileName = strRTUReadWrite;
                        rtureadwrite.StartInfo.Arguments = String.Format("/host:" + strHostName + " /pipe:injectdb\\cs_injp /wellnum:" + wellNumber + " /reqdef:" + strReqDefXML + " /output:" + strOutputFile + " /bufferdatatype:" + dataType);

                        TestContext.WriteLine(strRTUReadWrite + " " + rtureadwrite.StartInfo.Arguments);
                        Assert.IsTrue(rtureadwrite.Start());
                        rtureadwrite.WaitForExit();
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
                    try
                    {
                        Process rtureadwrite = new Process();
                        rtureadwrite.StartInfo.UseShellExecute = true;
                        rtureadwrite.StartInfo.FileName = strRTUReadWrite;
                        rtureadwrite.StartInfo.Arguments = String.Format("/host:" + strHostName + " /pipe:subsdb\\cs_subp /wellnum:" + wellNumber + " /reqdef:" + strReqDefXML + " /output:" + strOutputFile + " /bufferdatatype:" + dataType);

                        TestContext.WriteLine(strRTUReadWrite + " " + rtureadwrite.StartInfo.Arguments);
                        Assert.IsTrue(rtureadwrite.Start());
                        rtureadwrite.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;

                case "YSKAWA":
                    try
                    {
                        Process rtureadwrite = new Process();
                        rtureadwrite.StartInfo.UseShellExecute = true;
                        rtureadwrite.StartInfo.FileName = strRTUReadWrite;
                        rtureadwrite.StartInfo.Arguments = String.Format("/host:" + strHostName + " /pipe:pcpdb\\cs_pcpp /wellnum:" + wellNumber + " /reqdef:" + strReqDefXML + " /output:" + strOutputFile + " /bufferdatatype:" + dataType);

                        TestContext.WriteLine(strRTUReadWrite + " " + rtureadwrite.StartInfo.Arguments);
                        Assert.IsTrue(rtureadwrite.Start());
                        rtureadwrite.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;
            }
        }

        public void RTUWrite(string rtuType, int wellNumber, string outputFile, string dataType, string reqDef)
        {

            string strRTUReadWrite = Path.Combine(m_strLiftDir, "RTUReadWriteConsole.exe");
            string strReqDefXML = Path.Combine(m_strLiftDir, reqDef);
            string strOutputFile = Path.Combine(m_strLiftDir, outputFile);
            string strHostName = Dns.GetHostName();

            Process rtureadwrite = new Process();

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
                        rtureadwrite.StartInfo.UseShellExecute = true;
                        rtureadwrite.StartInfo.FileName = strRTUReadWrite;
                        rtureadwrite.StartInfo.Arguments = String.Format("/host:" + strHostName + " /pipe:beamdb\\cs_beamp /wellnum:" + wellNumber + " /reqdef:" + strReqDefXML + " /output:" + strOutputFile + " /bufferdatatype:" + dataType + " /mode:write");

                        TestContext.WriteLine(strRTUReadWrite + " " + rtureadwrite.StartInfo.Arguments);
                        Assert.IsTrue(rtureadwrite.Start());
                        rtureadwrite.WaitForExit();

                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;

                case "AE6008":
                    try
                    {
                        rtureadwrite.StartInfo.UseShellExecute = true;
                        rtureadwrite.StartInfo.FileName = strRTUReadWrite;
                        rtureadwrite.StartInfo.Arguments = String.Format("/host:" + strHostName + " /pipe:injectdb\\cs_injp /wellnum:" + wellNumber + " /reqdef:" + strReqDefXML + " /output:" + strOutputFile + " /bufferdatatype:" + dataType + " /mode:write");

                        TestContext.WriteLine(strRTUReadWrite + " " + rtureadwrite.StartInfo.Arguments);
                        Assert.IsTrue(rtureadwrite.Start());
                        rtureadwrite.WaitForExit();
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
                    try
                    {
                        rtureadwrite.StartInfo.UseShellExecute = true;
                        rtureadwrite.StartInfo.FileName = strRTUReadWrite;
                        rtureadwrite.StartInfo.Arguments = String.Format("/host:" + strHostName + " /pipe:subsdb\\cs_subp /wellnum:" + wellNumber + " /reqdef:" + strReqDefXML + " /output:" + strOutputFile + " /bufferdatatype:" + dataType + " /mode:write");

                        TestContext.WriteLine(strRTUReadWrite + " " + rtureadwrite.StartInfo.Arguments);
                        Assert.IsTrue(rtureadwrite.Start());
                        rtureadwrite.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;

                case "YSKAWA":
                    try
                    {
                        rtureadwrite.StartInfo.UseShellExecute = true;
                        rtureadwrite.StartInfo.FileName = strRTUReadWrite;
                        rtureadwrite.StartInfo.Arguments = String.Format("/host:" + strHostName + " /pipe:pcpdb\\cs_pcpp /wellnum:" + wellNumber + " /reqdef:" + strReqDefXML + " /output:" + strOutputFile + " /bufferdatatype:" + dataType + " /mode:write");

                        TestContext.WriteLine(strRTUReadWrite + " " + rtureadwrite.StartInfo.Arguments);
                        Assert.IsTrue(rtureadwrite.Start());
                        rtureadwrite.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;
            }
        }

        public void CreateBeamWells(string rtuType)
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
            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
        }

        public void CreateMonitorWells(string rtuType)
        {
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            string strWellTestScript = ConfigurationManager.AppSettings.Get("WellTestScript");

            int parmChannel = 0;
            int hw_rtu = 0;
            string parmAltAddress = String.Empty;
            string parmRtuName = String.Empty;
            string SepName = String.Empty;
            string SepDesc = String.Empty;
            int parmFirstWellNumber = 0;
            string parmWellNamePrefix = String.Empty;
            int parmFirstAddress = 0;


            Process mbw = new Process();

            try
            {
                mbw.StartInfo.UseShellExecute = true;
                mbw.StartInfo.FileName = strMcsscrip;

                switch (rtuType)
                {
                    case "WTCS7X":
                        parmChannel = 1;
                        hw_rtu = 165;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuName = "WTCS7X_NAT";
                        SepName = "NTVSEP";
                        SepDesc = "NATIVE Seperator";
                        parmFirstWellNumber = 1;
                        parmWellNamePrefix = "WTWEL_";
                        parmFirstAddress = 165;
                        break;
                }

                mbw.StartInfo.Arguments = String.Format("{0} doIt \"\" \"{1}\" \"{2}\" \"{3}\" \"{4}\" \"{5}\" \"{6}\" \"{7}\" \"{8}\" \"{9}\" \"{10}\" -db beamdb", strWellTestScript, parmChannel, hw_rtu, parmAltAddress, parmRtuName, SepName, SepDesc, parmFirstWellNumber, parmWellNamePrefix, parmFirstAddress, wellCount);

                TestContext.WriteLine(strMcsscrip + " " + mbw.StartInfo.Arguments);
                Assert.IsTrue(mbw.Start());
                mbw.WaitForExit();

            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
        }

        public void CreateESPWells(string rtuType)
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
                    case "KLT595":
                        parmWellNamePrefix = "K595_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 131;
                        parmLastAddress = 131;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "KLT595";
                        parmRtuSubType = "KLT595";
                        break;

                    case "KELTRN":
                        parmWellNamePrefix = "KTRN_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 127;
                        parmLastAddress = 127;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "KELTRN";
                        parmRtuSubType = "KELTRN";
                        break;

                    case "GCSVFD":
                        parmWellNamePrefix = "GVFD_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 123;
                        parmLastAddress = 123;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "GCSVFD";
                        parmRtuSubType = "GCSVFD";
                        break;

                    case "GCSV84":
                        parmWellNamePrefix = "GV84_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 119;
                        parmLastAddress = 119;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "GCSESP";
                        parmRtuSubType = "8V04";
                        break;

                    case "GCSV72":
                        parmWellNamePrefix = "GV72_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 115;
                        parmLastAddress = 115;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "GCSESP";
                        parmRtuSubType = "7V20";
                        break;

                    case "VTXICM":
                        parmWellNamePrefix = "VICM_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 111;
                        parmLastAddress = 111;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "VTXICM";
                        parmRtuSubType = "VTXICM";
                        break;

                    case "SUGNMB":
                        parmWellNamePrefix = "SUG_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 149;
                        parmLastAddress = 149;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "SUGNMB";
                        parmRtuSubType = "SUGMB1";
                        break;

                    case "VORTEX":
                        parmWellNamePrefix = "VORTEX_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 161;
                        parmLastAddress = 161;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "VORTEX";
                        parmRtuSubType = "NONE";
                        break;

                    case "SPDSTR":
                        parmWellNamePrefix = "SPDSTR_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 145;
                        parmLastAddress = 145;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "SPDSTR";
                        parmRtuSubType = "NONE";
                        break;

                    case "ROBICN":
                        parmWellNamePrefix = "ROBICN_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 141;
                        parmLastAddress = 141;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "ROBICN";
                        parmRtuSubType = "NONE";
                        break;

                    case "FSONLY":
                        parmWellNamePrefix = "FSONLY_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 200;
                        parmLastAddress = 200;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "REDUNI";
                        parmRtuSubType = "FSONLY";
                        break;

                    case "FSWPHX":
                        parmWellNamePrefix = "FSWPHX_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 211;
                        parmLastAddress = 211;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "REDUNI";
                        parmRtuSubType = "FSWPHX";
                        break;

                    case "VSONLY":
                        parmWellNamePrefix = "VSONLY_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 221;
                        parmLastAddress = 221;
                        parmChannel = 1;
                        parmAltAddress = "127.0.0.1/10000";
                        parmRtuType = "REDUNI";
                        parmRtuSubType = "VSONLY";
                        break;

                    case "VSWPHX":
                        parmWellNamePrefix = "VSWPHX_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 231;
                        parmLastAddress = 231;
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

        public void CreatePCPWells(string rtuType)
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
                    case "YSKAWA":
                        parmWellNamePrefix = "YSK_";
                        parmFirstWellNumber = 1;
                        parmFirstAddress = 241;
                        parmLastAddress = 241;
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

            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
        }

        public void CreateInjectionWells(string rtuType)
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
                        parmLastAddress = 151;
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

        public  bool CompareOutputFiles(string fileName1, string fileName2)
        {
            return CompareOutputFiles(fileName1, fileName2, null);
        }
        public  bool CompareOutputFiles(string fileName1, string fileName2, string reqDefFileName)
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
                TestContext.WriteLine("Comparing " + fileName1 + " (length " + scanA.Length + ")" + " to " + fileName2 + " (length " + scanB.Length + ")" + " Request Definition File: " + reqDefFileName);

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
                    if (scanA[i].Contains("<RawBody"))
                    {
                        TestContext.WriteLine("<RawBody line found, skipping.");
                    }
                    else if (scanA[i].Contains("<RawHdr"))
                    {
                        TestContext.WriteLine("<RawHdr line found, skipping.");
                    }
                    else if (scanA[i].Contains("<Header"))
                    {
                        TestContext.WriteLine("<Header line found, skipping.");
                    }
                    else if (scanA[i].Contains("<ParsedRequestData"))
                    {
                        TestContext.WriteLine("<ParsedRequestData line found, skipping.");
                    }
                    else if (scanA[i].Contains("<BeamProcMessage"))
                    {
                        TestContext.WriteLine("<BeamProcMessage line found, skipping.");
                    }
                    else if (scanA[i].Contains("<Response"))
                    {
                        Assert.IsTrue(scanA[i].Contains("responseCode=\"1\""));
                        Assert.IsTrue(scanB[i].Contains("responseCode=\"1\""));
                        TestContext.WriteLine("responseCode=\"1\" found");
                    }
                    else
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

        protected void StartRTUEmu()
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
        public static string GetLiftRunFolder()
        {
           
            Trace.WriteLine("Locating Lift Run folder...");

            // First check for the development environment variable
            string liftrun = Environment.GetEnvironmentVariable("LIFTRUN");

            if (!String.IsNullOrEmpty(liftrun))
            {
                return liftrun;
            }

            string strLiftRoot = ConfigurationManager.AppSettings.Get("LiftRoot");
            Trace.WriteLine("Provided Lift Root folder is " + strLiftRoot);

            string csLift = Directory.GetParent(strLiftRoot).Parent.Parent.FullName;

            liftrun = Path.Combine(csLift, "liftnt", "run");

           Trace.WriteLine("Found Lift Run folder " + liftrun);

            return liftrun;
        }

        protected void Cleanup(string rtuType)
        {

            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            string strDeleteBulkWellScript = ConfigurationManager.AppSettings.Get("DeleteWellScript");
            string strWellTestScript = ConfigurationManager.AppSettings.Get("WellTestScript");

            Process dbw = new Process();

            switch (rtuType)
            {
                case "UNISRPV100":
                case "UNISRPV110":
                case "UNISRPV200":
                case "UNILRP":
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

                case "KLT595":
                case "KELTRN":
                case "GCSVFD":
                case "GCSV84":
                case "GCSV72":
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

                case "YSKAWA":
                    try
                    {
                        dbw.StartInfo.UseShellExecute = true;
                        dbw.StartInfo.FileName = strMcsscrip;
                        dbw.StartInfo.Arguments = String.Format(strDeleteBulkWellScript + " deletepcpwells \"\" \"\" 1 -db pcpdb");

                        TestContext.WriteLine(strMcsscrip + " " + dbw.StartInfo.Arguments);
                        Assert.IsTrue(dbw.Start());
                        dbw.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;
                case "WTCS7X":
                    try
                    {
                        dbw.StartInfo.UseShellExecute = true;
                        dbw.StartInfo.FileName = strMcsscrip;
                        dbw.StartInfo.Arguments = String.Format("{0} undoIt \"\" \"{1}\" \"{2}\" \"{3}\" \"{4}\" \"{5}\" \"{6}\" -db beamdb", strWellTestScript, "WTCS7X_NAT", "NTVSEP", "NATIVE Seperator", 1, "WTWEL_", wellCount);

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

            // let's also delete the any output test files

            try
            {
                string[] csvList = Directory.GetFiles(m_strLiftDir, "*.csv");
                string[] txtList = Directory.GetFiles(m_strLiftDir, "*.txt");

                foreach (string f in csvList)
                {
                    if (f.Contains("BE") && f.Contains(".csv"))
                    {
                        TestContext.WriteLine("Deleting File " + f);
                        File.Delete(f);
                    }

                    if (f.Contains("SU") && f.Contains(".csv"))
                    {
                        TestContext.WriteLine("Deleting File " + f);
                        File.Delete(f);
                    }

                    if (f.Contains("IJ") && f.Contains(".csv"))
                    {
                        TestContext.WriteLine("Deleting File " + f);
                        File.Delete(f);
                    }

                    if (f.Contains("GL") && f.Contains(".csv"))
                    {
                        TestContext.WriteLine("Deleting File " + f);
                        File.Delete(f);
                    }
                    if (f.Contains("PC") && f.Contains(".csv"))
                    {
                        TestContext.WriteLine("Deleting File " + f);
                        File.Delete(f);
                    }
                    if (f.Contains("GN") && f.Contains(".csv"))
                    {
                        TestContext.WriteLine("Deleting File " + f);
                        File.Delete(f);
                    }
                }

                foreach (string f in txtList)
                {
                    if (f.Contains("test") && f.Contains(".txt"))
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
        protected void CheckIfLowisExist()
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
		 protected void GetCurrentStatus(int wellNumber, string rtuType) // only here because stop_sep,emergency shutdown, and stop test commands do NOT issue a current status after the command as the rest of LOWIS does.
        {
            string strCurrentStatusWT = Path.Combine(m_strLiftDir, "mtstwtst.exe");
            string strHostName = Dns.GetHostName();
            Process mtstwt = new Process();

            switch (rtuType)
            {
                case "WTCS7X":
                    try
                    {
                        mtstwt.StartInfo.UseShellExecute = true;
                        mtstwt.StartInfo.FileName = strCurrentStatusWT;
                        mtstwt.StartInfo.Arguments = String.Format(strHostName + " welltestdb\\\\cs_wtstp 1 " + wellNumber);

                        TestContext.WriteLine(strCurrentStatusWT + " " + mtstwt.StartInfo.Arguments);
                        Assert.IsTrue(mtstwt.Start());
                        mtstwt.WaitForExit();
                    }
                    catch (Exception e)
                    {
                        Assert.Fail("ERROR: {0}", e.ToString());
                    }
                    break;
            }
            System.Threading.Thread.Sleep(10000); //  wait 10 seconds make sure current status output has made it to file 
        }
        protected void CommandRequest(string sepDesc, string command, string lifttype)
        {
            string strHostName = Dns.GetHostName();
            string strCommandRequest = String.Empty;
            string strWellTestScript = ConfigurationManager.AppSettings.Get("WellTestScript");

            switch (lifttype)
            {
                case "MONITOR":
                    strCommandRequest = Path.Combine(m_strLiftDir, "mcsscrip.exe");
                    break;
            }

            Process mtstwtst = new Process();
            try
            {
                mtstwtst.StartInfo.UseShellExecute = true;
                mtstwtst.StartInfo.FileName = strCommandRequest;

                switch (lifttype)
                {
                    case "MONITOR":
                        mtstwtst.StartInfo.Arguments = String.Format("{0} doCommand \"\" \"{1}\" \"{2}\" -db welltestdb", strWellTestScript, sepDesc, command);
                        break;
                }

                TestContext.WriteLine(strCommandRequest + " " + mtstwtst.StartInfo.Arguments);
                Assert.IsTrue(mtstwtst.Start());
                mtstwtst.WaitForExit();
            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }

            System.Threading.Thread.Sleep(20000); // wait 20 seconds after executing command request

        }

        /* 
         * Valid Commands
         * 1 - START
         * 2 - STOP
         * 3 - POC
         * 4 - IPOC
         */

        protected void CommandRequest(int wellNumber, int command, string lifttype)
        {
            string strHostName = Dns.GetHostName();
            string strCommandRequest = String.Empty;
			string strWellTestScript = ConfigurationManager.AppSettings.Get("WellTestScript");

            switch (lifttype)
            {
                case "BEAM":
                case "PCP":
                    strCommandRequest = Path.Combine(m_strLiftDir, "mtstbeam.exe");
                    break;

                case "ESP":
                    strCommandRequest = Path.Combine(m_strLiftDir, "mtstsub.exe");
                    break;
            }

            Process mtstbeam = new Process();
            try
            {
                mtstbeam.StartInfo.UseShellExecute = true;
                mtstbeam.StartInfo.FileName = strCommandRequest;

                switch (lifttype)
                {
                    case "BEAM":
                        mtstbeam.StartInfo.Arguments = String.Format(strHostName + " beamdb\\\\cs_beamp 3 " + wellNumber + " " + command);
                        break;

                    case "ESP":
                        mtstbeam.StartInfo.Arguments = String.Format(strHostName + " subsdb\\\\cs_subp 3 " + wellNumber + " " + command);
                        break;

                    case "PCP":
                        mtstbeam.StartInfo.Arguments = String.Format(strHostName + " pcpdb\\\\cs_pcpp 3 " + wellNumber + " " + command);
                        break;
                }
                
                TestContext.WriteLine(strCommandRequest + " " + mtstbeam.StartInfo.Arguments);
                Assert.IsTrue(mtstbeam.Start());
                mtstbeam.WaitForExit();
            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }

            System.Threading.Thread.Sleep(20000); // wait 20 seconds after executing command request

        }

        protected void VerifyState(int wellnumber, string fileName, string state, string rtuType)
        {

            string strInternalStatusBits = String.Empty;
            string strLowerBitsBinary;
            string strLastTwo;
            int LowerBitsDecimal;

            int YskBitsDecimal;
            string YskBitsBinary;
            int Ysk8thBit;
			string strRegisterOneCheck = String.Empty;
            string strRegisterTwoCheck = String.Empty;
          
            try
            {
                if (!File.Exists(Path.Combine(m_strLiftDir, fileName)))
                {
                    Assert.Fail("Error, File not found: " + Path.Combine(m_strLiftDir, fileName));
                }
                else
                {
                    TestContext.WriteLine("File: {0}", Path.Combine(m_strLiftDir, fileName));
                }

                string[] scan = File.ReadAllLines(Path.Combine(m_strLiftDir, fileName));
                string text = File.ReadAllText(Path.Combine(m_strLiftDir, fileName));

                if (scan.Length < 1)
                {
                    Assert.Fail("Error, Output File " + fileName + " is empty.");
                }

                // each rtuType have different registers that tells what state that RTU is in

                switch(rtuType)
                {
                    case "YSKAWA":

                        // make sure register 44 exist first
                        if (!text.Contains("44, 3,"))
                        {
                            Assert.Fail("Error, Register 44 (Type 3) not found in {0}", fileName);
                        }

                        foreach (string line in scan)
                        {
                            if (line.StartsWith("44, 3,"))
                            {
                                strInternalStatusBits = line;
                                YskBitsDecimal = Int32.Parse(line.Substring(7));
                                YskBitsBinary = Convert.ToString(YskBitsDecimal, 2);
                                Ysk8thBit = YskBitsDecimal & 256;
                                //if 8th bit is 0 then its running
                                if ((YskBitsBinary.Length < 9) || (Ysk8thBit == 0))
                                {
                                    Ysk8thBit = 0;
                                    TestContext.WriteLine("Internal Status Bits (Register, Type, Value): {0}", strInternalStatusBits);
                                    TestContext.WriteLine("Value in Decimal: {0}", YskBitsDecimal);
                                    TestContext.WriteLine("Value in Binary: {0}", YskBitsBinary);
                                    TestContext.WriteLine("8th Bit in Binary: {0}", Ysk8thBit);
                                    TestContext.WriteLine("Decimal Value is: " + YskBitsDecimal + " and 8th bit is not set: " + Ysk8thBit + " Well start command sent and running");

                                }
                                else
                                //if 8th bit is 1 then its stopped
                                {
                                    TestContext.WriteLine("Internal Status Bits (Register, Type, Value): {0}", strInternalStatusBits);
                                    TestContext.WriteLine("Value in Decimal: {0}", YskBitsDecimal);
                                    TestContext.WriteLine("Value in Binary: {0}", YskBitsBinary);
                                    if (Ysk8thBit == 256)
                                    {
                                        Ysk8thBit = 1;
                                        TestContext.WriteLine("Decimal Value is: " + YskBitsDecimal + " and 8th bit is set: " + Ysk8thBit + " Well stop command sent and stopped");
                                    }
                                }
                                /* 
                                 * Register 44
                                 * 
                                 * START (1) - 101
                                 * STOP (2) - 357
                                 
                                 */
                                switch (state)
                                {
                                    case "stop":
                                        TestContext.WriteLine("Verifying Stop state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "44, 3, 357")
                                        {
                                            Assert.Fail("Stop state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Stop state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "start":
                                        TestContext.WriteLine("Verifying Start state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "44, 3, 101")
                                        {
                                            Assert.Fail("Start state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Start state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                }
                            }
                        }
                        break;

                    case "EPICRP":

                    // make sure register 520 exist first
                        if (!text.Contains("520, 4,"))
                    {
                        Assert.Fail("Error, Register 520 (Type 4) not found in {0}", fileName);
                    }

                    foreach (string line in scan)
                    {
                        if (line.Contains("520, 4,"))
                        {
                            strInternalStatusBits = line;
                            LowerBitsDecimal = Int32.Parse(line.Substring(8));
                            strLowerBitsBinary = Convert.ToString(LowerBitsDecimal, 2);

                            // when value is 0 then the lower bits should be 00 by default
                            if (strLowerBitsBinary == "0")
                            {
                                strLastTwo = "00";
                            }
                            else
                            {
                                strLastTwo = strLowerBitsBinary.Substring(strLowerBitsBinary.Length - 2);
                            }

                            TestContext.WriteLine("Internal Status Bits (Register, Type, Value): {0}", strInternalStatusBits);
                            TestContext.WriteLine("Value in Decimal: {0}", LowerBitsDecimal);
                            TestContext.WriteLine("Value in Binary: {0}", strLowerBitsBinary);
                            TestContext.WriteLine("Lower bits in Binary: {0}", strLastTwo);

                            /* 
                             * Lower two bits of register 520
                             * 
                             * 00 - Stop
                             * 01 - Unknown
                             * 10 - Unknown
                             * 11 - Start (Running)
                             */

                            if (strLastTwo.Equals("01") || strLastTwo.Equals("10"))
                            {
                                Assert.Fail("Error, Unknown state detected (10 or 01)");
                            }

                            switch (state)
                            {
                                case "stop":
                                    TestContext.WriteLine("Verifying Stop state for Well #" + wellnumber);
                                    if (strLastTwo != "00")
                                    {
                                        Assert.Fail("Stop state not found on Well #" + wellnumber);
                                    }
                                    TestContext.WriteLine("Stop state found for Well #" + wellnumber);
                                    TestContext.WriteLine("\n\n");
                                    break;

                                case "start":
                                    TestContext.WriteLine("Verifying Start state for Well #" + wellnumber);
                                    if (strLastTwo != "11")
                                    {
                                        Assert.Fail("Start state not found on Well #" + wellnumber);
                                    }
                                    TestContext.WriteLine("Start state found for Well #" + wellnumber);
                                    TestContext.WriteLine("\n\n");
                                    break;

                                case "idle":
                                    TestContext.WriteLine("Verifying Idle state for Well #" + wellnumber);
                                    if (strLastTwo != "00")
                                    {
                                        Assert.Fail("Idle state not found on Well #" + wellnumber);
                                    }
                                    TestContext.WriteLine("Idle state found for Well #" + wellnumber);
                                    TestContext.WriteLine("\n\n");
                                    break;

                                case "running":
                                    TestContext.WriteLine("Verifying Running state for Well #" + wellnumber);
                                    if (strLastTwo != "11")
                                    {
                                        Assert.Fail("Running state not found on Well #" + wellnumber);
                                    }
                                    TestContext.WriteLine("Running state found for Well #" + wellnumber);
                                    TestContext.WriteLine("\n\n");
                                    break;
                                }
                            }
                        }
                        break;

                    case "SAMRP2":

                        // make sure register 2501 exist first
                        if (!text.Contains("2501, 4,"))
                        {
                            Assert.Fail("Error, Register 2501 (Type 4) not found in {0}", fileName);
                        }

                        foreach (string line in scan)
                        {
                            if (line.Contains("2501, 4,"))
                            {
                                strInternalStatusBits = line;

                                TestContext.WriteLine("Status (Register, Type, Value): {0}", strInternalStatusBits);

                                /* 
                                 * Register 32501
                                 * 
                                 * START (1) - 8
                                 * STOP (2) - 36
                                 * POC (3) - 7
                                 * IPOC (4) - 31
                                 * MTMRRUN(7) - 9
                                 */

                                switch (state)
                                {
                                    case "stop":
                                        TestContext.WriteLine("Verifying Stop state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "2501, 4, 36")
                                        {
                                            Assert.Fail("Stop state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Stop state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "start":
                                        TestContext.WriteLine("Verifying Start state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "2501, 4, 8")
                                        {
                                            Assert.Fail("Start state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Start state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "poc":
                                        TestContext.WriteLine("Verifying POC state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "2501, 4, 31")
                                        {
                                            TestContext.WriteLine("Internal Status Bytes being checked are: " + strInternalStatusBits);
                                            Assert.Fail("POC state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("POC state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "ipoc":
                                        TestContext.WriteLine("Verifying IPOC state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "2501, 4, 7")
                                        {
                                            Assert.Fail("IPOC state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("IPOC state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "mtmrrun":
                                        TestContext.WriteLine("Verifying MTMRRUN state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "2501, 4, 9")
                                        {
                                            Assert.Fail("MTMRRUN state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("MTMRRUN state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                }
                            }
                        }

                        break;

                    case "AEPOC2":

                        // make sure register 479 exist first
                        if (!text.Contains("479, 2,"))
                        {
                            Assert.Fail("Error, Register 479 (Type 2) not found in {0}", fileName);
                        }

                        foreach (string line in scan)
                        {
                            if (line.Contains("479, 2,"))
                            {
                                strInternalStatusBits = line;

                                TestContext.WriteLine("Status (Register, Type, Value): {0}", strInternalStatusBits);

                                /* 
                                 * Register 479
                                 * 
                                 * START (1) - 256
                                 * STOP (2) - 128
                                 * POC (3) - 2
                                 * IPOC (4) - 16
                                 * MTMRRUN (7) - 32
                                 * MTMRIDLE (8) - 4
                                 * HTMR (9) - 512
                                 * STMR (10) - 64
                                 */

                                switch (state)
                                {
                                    case "stop":
                                        TestContext.WriteLine("Verifying Stop state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "479, 2, 128")
                                        {
                                            Assert.Fail("Stop state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Stop state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "start":
                                        TestContext.WriteLine("Verifying Start state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "479, 2, 256")
                                        {
                                            Assert.Fail("Start state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Start state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "poc":
                                        TestContext.WriteLine("Verifying POC state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "479, 2, 2")
                                        {
                                            Assert.Fail("POC state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("POC state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "ipoc":
                                        TestContext.WriteLine("Verifying IPOC state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "479, 2, 16")
                                        {
                                            Assert.Fail("IPOC state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("IPOC state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "mtmrrun":
                                        TestContext.WriteLine("Verifying MTMRRUN state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "479, 2, 32")
                                        {
                                            Assert.Fail("MTMRRUN state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("MTMRRUN state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "mtmridle":
                                        TestContext.WriteLine("Verifying MTMRIDLE state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "479, 2, 4")
                                        {
                                            Assert.Fail("MTMRIDLE state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("MTMRIDLE state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "htmr":
                                        TestContext.WriteLine("Verifying HTMR state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "479, 2, 512")
                                        {
                                            Assert.Fail("HTMR state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("HTMR state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "stmr":
                                        TestContext.WriteLine("Verifying STMR state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "479, 2, 64")
                                        {
                                            Assert.Fail("STMR state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("STMR state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                }
                            }
                        }

                        break;

                    case "UV200":
                    case "UNILRP":

                        // make sure register 7045 exist first
                        if (!text.Contains("7045, 4355,"))
                        {
                            Assert.Fail("Error, Register 7045 (Type 4355) not found in {0}", fileName);
                        }

                        foreach (string line in scan)
                        {
                            if (line.Contains("7045, 4355,"))
                            {
                                strInternalStatusBits = line;

                                TestContext.WriteLine("Status (Register, Type, Value): {0}", strInternalStatusBits);

                                /* 
                                 * Register 47045
                                 * 
                                 * RUN (1) - 101
                                 * STOP (2) - 205
                                 */

                                switch (state)
                                {
                                    case "stop":
                                        TestContext.WriteLine("Verifying Stop state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "7045, 4355, 205")
                                        {
                                            Assert.Fail("Stop state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Stop state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "run":
                                        TestContext.WriteLine("Verifying Start state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "7045, 4355, 101")
                                        {
                                            Assert.Fail("Run state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Run state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;
                                }
                                break; // breaking loop after first find of register since there is an extra register of 7045
                            }
                        }

                        break;

                    case "UV100":
                    case "UV110":

                        // make sure register 7045 exist first
                        if (!text.Contains("7045, 4355,"))
                        {
                            Assert.Fail("Error, Register 7045 (Type 4355) not found in {0}", fileName);
                        }

                        foreach (string line in scan)
                        {
                            if (line.Contains("7045, 4355,"))
                            {
                                strInternalStatusBits = line;

                                TestContext.WriteLine("Status (Register, Type, Value): {0}", strInternalStatusBits);

                                /* 
                                 * Register 47045
                                 * 
                                 * RUN (1) - 10
                                 * STOP (2) - 23
                                 */

                                switch (state)
                                {
                                    case "stop":
                                        TestContext.WriteLine("Verifying Stop state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "7045, 4355, 23")
                                        {
                                            Assert.Fail("Stop state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Stop state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "run":
                                        TestContext.WriteLine("Verifying Start state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "7045, 4355, 10")
                                        {
                                            Assert.Fail("Run state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Run state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;
                                } 
                               break; // breaking loop after first find of register since there is an extra register of 7045
                            }
                        }
                        break;
						
					 case "VORTEX":
                        // make sure register 264 exist first
                        if (!text.Contains("264, 2,"))
                        {
                            Assert.Fail("Error, Register 264 (Type 2) not found in {0}", fileName);
                        }

                        foreach (string line in scan)
                        {
                            if (line.Contains("264, 2,"))
                            {
                                strInternalStatusBits = line;

                                TestContext.WriteLine("Status (Register, Type, Value): {0}", strInternalStatusBits);

                                if (line.Contains("266, 2,"))
                                {
                                    strRegisterOneCheck = line;
                                }
                                if (line.Contains("267, 2,"))
                                {
                                    strRegisterTwoCheck = line;
                                }

                                /* 
                                 * Register 10264
                                 * 
                                 * RUN (1) - 1
                                 * STOP (2) - 0
                                 */
                                switch (state)
                                {
                                    case "stop":
                                        TestContext.WriteLine("Verifying Stop state for Well #" + wellnumber);
                                        if ((strInternalStatusBits != "264, 2, 0") && (strRegisterOneCheck != "266, 2, 0") && (strRegisterTwoCheck != "267, 2, 0"))
                                        {
                                            Assert.Fail("Stop state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Stop state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "run":
                                        TestContext.WriteLine("Verifying Start state for Well #" + wellnumber);
                                        if ((strInternalStatusBits != "264, 2, 1") && (strRegisterOneCheck != "266, 2, 1") && (strRegisterTwoCheck != "267, 2, 0"))
                                        {
                                            Assert.Fail("Run state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Run state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;
                                }
                                break;
                            }
                        }
                        break;
						
                    case "SPDSTR":

                        // make sure register 213 exist first
                        if (!text.Contains("213, 4,"))
                        {
                            Assert.Fail("Error, Register 213 (Type 4) not found in {0}", fileName);
                        }

                        foreach (string line in scan)
                        {
                            if (line.Contains("213, 4,"))
                            {
                                strInternalStatusBits = line;

                                TestContext.WriteLine("Status (Register, Type, Value): {0}", strInternalStatusBits);

                                if (line.Contains("112, 4,"))
                                {
                                    strRegisterOneCheck = line;
                                }
                                if (line.Contains("111, 4,"))
                                {
                                    strRegisterTwoCheck = line;
                                }

                                /* 
                                 * Register 30213
                                 * 
                                 * RUN (1) - 4802
                                 * STOP (2) - 0
                                 */
                                switch (state)
                                {
                                    case "stop":
                                        TestContext.WriteLine("Verifying Stop state for Well #" + wellnumber);
                                        if ((strInternalStatusBits != "213, 4, 0") && (strRegisterOneCheck != "112, 4, 0") && (strRegisterTwoCheck != "111, 4, 0"))
                                        {
                                            Assert.Fail("Stop state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Stop state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "run":
                                        TestContext.WriteLine("Verifying Start state for Well #" + wellnumber);
                                        if ((strInternalStatusBits != "213, 4, 4802") && (strRegisterOneCheck != "112, 4, 0") && (strRegisterTwoCheck != "111, 4, 1"))
                                        {
                                            Assert.Fail("Run state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Run state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;
                                }
                                break;
                            }
                        }
                        break;

                    case "ROBICN":

                        // make sure register 1 exist first
                        if (!text.Contains("1, 3,"))
                        {
                            Assert.Fail("Error, Register 1 (Type 3) not found in {0}", fileName);
                        }

                        foreach (string line in scan)
                        {
                            if (line.Contains("1, 3,"))
                            {
                                strInternalStatusBits = line;

                                TestContext.WriteLine("Status (Register, Type, Value): {0}", strInternalStatusBits);

                                /* 
                                 * Register 40001
                                 * 
                                 * RUN (1) - 4
                                 * STOP (2) - 0
                                 */

                                switch (state)
                                {
                                    case "stop":
                                        TestContext.WriteLine("Verifying Stop state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "1, 3, 0")
                                        {
                                            Assert.Fail("Stop state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Stop state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "run":
                                        TestContext.WriteLine("Verifying Start state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "1, 3, 4")
                                        {
                                            Assert.Fail("Run state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Run state found for Well #" + wellnumber);
                                        TestContext.WriteLine("Third Bit is set");
                                        TestContext.WriteLine("\n\n");
                                        break;
                                }
                                break; // breaking loop after first find of register since there is an extra register of 1
                            }
                        }
                        break;
						
                    case "REDUNI":

                        // make sure register 432 exist first
                        if (!text.Contains("432, 2,"))
                        {
                            Assert.Fail("Error, Register 432 (Type 2) not found in {0}", fileName);
                        }

                        foreach (string line in scan)
                        {
                            if (line.Contains("432, 2,"))
                            {
                                strInternalStatusBits = line;

                                TestContext.WriteLine("Status (Register, Type, Value): {0}", strInternalStatusBits);

                                /* 
                                 * Register 10432
                                 * 
                                 * RUN (1) - 1
                                 * STOP (2) - 0
                                 */

                                switch (state)
                                {
                                    case "stop":
                                        TestContext.WriteLine("Verifying Stop state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "432, 2, 0")
                                        {
                                            Assert.Fail("Stop state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Stop state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "run":
                                        TestContext.WriteLine("Verifying Start state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "432, 2, 1")
                                        {
                                            Assert.Fail("Run state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Run state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;
                                }
                                break; // breaking loop after first find of register since there is an extra register of 432
                            }
                        }
                        break;

                    case "VTXICM":

                        // make sure register 6 exist first
                        if (!text.Contains("6, 2,"))
                        {
                            Assert.Fail("Error, Register 6 (Type 2) not found in {0}", fileName);
                        }

                        foreach (string line in scan)
                        {
                            if (line.Contains("6, 2,"))
                            {
                                strInternalStatusBits = line;

                                TestContext.WriteLine("Status (Register, Type, Value): {0}", strInternalStatusBits);

                                /* 
                                 * Register 10432
                                 * 
                                 * RUN (1) - 0
                                 * STOP (2) - 1
                                 */

                                switch (state)
                                {
                                    case "stop":
                                        TestContext.WriteLine("Verifying Stop state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "6, 2, 1")
                                        {
                                            Assert.Fail("Stop state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Stop state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "run":
                                        TestContext.WriteLine("Verifying Start state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "6, 2, 0")
                                        {
                                            Assert.Fail("Run state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Run state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;
                                }
                            }
                        }
                        break;
					 case "WTCS7X":

                        // make sure register 64 exist first
                        if (!text.Contains("64, 2,"))
                        {
                            Assert.Fail("Error, Register 64 (Type 2) not found in {0}", fileName);
                        }

                        foreach (string line in scan)
                        {
                            if (line.Contains("64, 2,"))
                            {
                                strInternalStatusBits = line;

                                TestContext.WriteLine("Status (Register, Type, Value): {0}", strInternalStatusBits);
                            }
                            if (line.Contains("2009, 1539,"))
                            {
                                   strRegisterOneCheck = line;
                                   TestContext.WriteLine("Status (Register, Type, Value): {0}", strRegisterOneCheck);
                             }
                            
                        }

                                
                                switch (state)
                                {
                                    case "stopseperator":
                                        TestContext.WriteLine("Verifying stopseperator state for Well #" + wellnumber);
                                        if ((strInternalStatusBits != "64, 2, 0") || (strRegisterOneCheck != "2009, 1539, 0.00"))
                                        {
                                            Assert.Fail("stopseperator state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("stopseperator state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "runseperator":
                                        TestContext.WriteLine("Verifying runseperator state for Well #" + wellnumber);
                                        if ((strInternalStatusBits != "64, 2, 0") || (strRegisterOneCheck != "2009, 1539, 3.00"))
                                        {
                                            Assert.Fail("runseperator state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("runseperator state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;
                                    case "stoptest":
                                        TestContext.WriteLine("Verifying stoptest state for Well #" + wellnumber);
                                        if ((strInternalStatusBits != "64, 2, 0") || (strRegisterOneCheck != "2009, 1539, 1.00"))
                                        {
                                            Assert.Fail("stoptest state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("stoptest state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;
                                    case "EmergencyShutDown":
                                        TestContext.WriteLine("Verifying stoptest state for Well #" + wellnumber);
                                        if ((strInternalStatusBits != "64, 2, 1") || (strRegisterOneCheck != "2009, 1539, 1.00"))
                                        {
                                            Assert.Fail("EmergencyShutDown state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("EmergencyShutDown state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;
                                }
                                break;
                    case "GCS":

                        // make sure register 321 exist first
                        if (!text.Contains("321, 2,"))
                        {
                            Assert.Fail("Error, Register 321 (Type 2) not found in {0}", fileName);
                        }

                        if (!text.Contains("322, 2,"))
                        {
                            Assert.Fail("Error, Register 322 (Type 2) not found in {0}", fileName);
                        }

                        foreach (string line in scan)
                        {
                            if (line.Contains("321, 2,"))
                            {
                                strInternalStatusBits = line;

                                TestContext.WriteLine("Status (Register, Type, Value): {0}", strInternalStatusBits);

                                /* 
                                 * Register 10321,322
                                 * 
                                 * RUN (10321) - 1, (10322) - 0
                                 * STOP (10322) - 1, (10321) - 0
                                 */

                                switch (state)
                                {
                                    case "stop":
                                        TestContext.WriteLine("Verifying Stop state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "321, 2, 0")
                                        {
                                            Assert.Fail("Stop state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Stop state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "run":
                                        TestContext.WriteLine("Verifying Start state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "321, 2, 1")
                                        {
                                            Assert.Fail("Run state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Run state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;
                                }
                            }

                            if (line.Contains("322, 2,"))
                            {
                                strInternalStatusBits = line;

                                TestContext.WriteLine("Status (Register, Type, Value): {0}", strInternalStatusBits);

                                /* 
                                 * Register 10321,322
                                 * 
                                 * RUN (10321) - 1, (10322) - 0
                                 * STOP (10322) - 1, (10321) - 0
                                 */

                                switch (state)
                                {
                                    case "stop":
                                        TestContext.WriteLine("Verifying Stop state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "322, 2, 1")
                                        {
                                            Assert.Fail("Stop state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Stop state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "run":
                                        TestContext.WriteLine("Verifying Start state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "322, 2, 0")
                                        {
                                            Assert.Fail("Run state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Run state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;
                                }
                            }
                        }
                        break;

                    case "KELTRN":

                        // make sure register 83 exist first
                        if (!text.Contains("83, 2,"))
                        {
                            Assert.Fail("Error, Register 83 (Type 2) not found in {0}", fileName);
                        }

                        if (!text.Contains("96, 2,"))
                        {
                            Assert.Fail("Error, Register 96 (Type 2) not found in {0}", fileName);
                        }

                        foreach (string line in scan)
                        {
                            if (line.Contains("83, 2,"))
                            {
                                strInternalStatusBits = line;

                                TestContext.WriteLine("Status (Register, Type, Value): {0}", strInternalStatusBits);

                                /* 
                                 * Register 10083, 10096
                                 * 
                                 * RUN - 1
                                 * STOP - 0
                                 */

                                switch (state)
                                {
                                    case "stop":
                                        TestContext.WriteLine("Verifying Stop state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "83, 2, 0")
                                        {
                                            Assert.Fail("Stop state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Stop state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "run":
                                        TestContext.WriteLine("Verifying Start state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "83, 2, 1")
                                        {
                                            Assert.Fail("Run state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Run state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;
                                }
                            }

                            if (line.Contains("96, 2,"))
                            {
                                strInternalStatusBits = line;

                                TestContext.WriteLine("Status (Register, Type, Value): {0}", strInternalStatusBits);

                                /* 
                                 * Register 10083, 10096
                                 * 
                                 * RUN - 1
                                 * STOP - 0
                                 */

                                switch (state)
                                {
                                    case "stop":
                                        TestContext.WriteLine("Verifying Stop state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "96, 2, 0")
                                        {
                                            Assert.Fail("Stop state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Stop state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "run":
                                        TestContext.WriteLine("Verifying Start state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "96, 2, 1")
                                        {
                                            Assert.Fail("Run state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Run state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;
                                }
                            }
                        }
                        break;

                    case "KLT595":

                        // make sure register 42 exist first
                        if (!text.Contains("42, 2,"))
                        {
                            Assert.Fail("Error, Register 42 (Type 2) not found in {0}", fileName);
                        }

                        if (!text.Contains("57, 2,"))
                        {
                            Assert.Fail("Error, Register 57 (Type 2) not found in {0}", fileName);
                        }

                        foreach (string line in scan)
                        {
                            if (line.Contains("42, 2,"))
                            {
                                strInternalStatusBits = line;

                                TestContext.WriteLine("Status (Register, Type, Value): {0}", strInternalStatusBits);

                                /* 
                                 * Register 10042, 10057
                                 * 
                                 * RUN - 1
                                 * STOP - 0
                                 */

                                switch (state)
                                {
                                    case "stop":
                                        TestContext.WriteLine("Verifying Stop state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "42, 2, 0")
                                        {
                                            Assert.Fail("Stop state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Stop state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "run":
                                        TestContext.WriteLine("Verifying Start state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "42, 2, 1")
                                        {
                                            Assert.Fail("Run state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Run state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;
                                }
                            }

                            if (line.Contains("57, 2,"))
                            {
                                strInternalStatusBits = line;

                                TestContext.WriteLine("Status (Register, Type, Value): {0}", strInternalStatusBits);

                                /* 
                                  * Register 10042, 10057
                                  * 
                                  * RUN - 1
                                  * STOP - 0
                                  */

                                switch (state)
                                {
                                    case "stop":
                                        TestContext.WriteLine("Verifying Stop state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "57, 2, 0")
                                        {
                                            Assert.Fail("Stop state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Stop state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;

                                    case "run":
                                        TestContext.WriteLine("Verifying Start state for Well #" + wellnumber);
                                        if (strInternalStatusBits != "57, 2, 1")
                                        {
                                            Assert.Fail("Run state not found on Well #" + wellnumber);
                                        }
                                        TestContext.WriteLine("Run state found for Well #" + wellnumber);
                                        TestContext.WriteLine("\n\n");
                                        break;
                                }
                            }
                        }
                        break;
                }
            }
        
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }

        }

        protected void SetCSVOutputDir(string lifttype)
        {

            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            string strSetCsvOption = ConfigurationManager.AppSettings.Get("CSVOptionScript");

            Process setcsv = new Process();

            try
            {
                setcsv.StartInfo.UseShellExecute = true;
                setcsv.StartInfo.FileName = strMcsscrip;
                switch (lifttype)
                {
                    case "BEAM":
                        setcsv.StartInfo.Arguments = String.Format(strSetCsvOption + " setBeamprocCsvFilePath \"\" \"" + m_strLiftDir + "\"");
                        break;
                    case "ESP":
                        setcsv.StartInfo.Arguments = String.Format(strSetCsvOption + " setEspprocCsvFilePath \"\" \"" + m_strLiftDir + "\"");
                        break;
                    case "PCP":
                        setcsv.StartInfo.Arguments = String.Format(strSetCsvOption + " setPCPprocCsvFilePath \"\" \"" + m_strLiftDir + "\"");
                        break;
					case "MONITOR":
                        setcsv.StartInfo.Arguments = String.Format(strSetCsvOption + " setMonitorprocCsvFilePath \"\" \"" + m_strLiftDir + "\"");
                        break;
                }
                TestContext.WriteLine(strMcsscrip + " " + setcsv.StartInfo.Arguments);

                Assert.IsTrue(setcsv.Start());
                setcsv.WaitForExit();
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

        protected void ConfigBEWellProps(int wellNumber)
        {
            string strMcsscrip = Path.Combine(m_strLiftDir, "mcsscrip.exe");
            string strConfigBEWellScript = ConfigurationManager.AppSettings.Get("ConfigBEWellScript");

            Process configBEWell = new Process();

            try
            {
                configBEWell.StartInfo.UseShellExecute = true;
                configBEWell.StartInfo.FileName = strMcsscrip;
                configBEWell.StartInfo.Arguments = String.Format("{0} configBEWellProperties \"\" {1} -db beamdb", strConfigBEWellScript, wellNumber);
                TestContext.WriteLine(strMcsscrip + " " + configBEWell.StartInfo.Arguments);

                Assert.IsTrue(configBEWell.Start());
                configBEWell.WaitForExit();
            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }
        }

        protected void WaitForOutputFile(string fileName)
        {
            System.Threading.Thread.Sleep(30000);
            if (!File.Exists(Path.Combine(m_strLiftDir, fileName)))
            {
                Assert.Fail("After 30 seconds, {0} still does not exist!", fileName);
            }
        }

        protected void TestResetAlarmCommand()
        {
            File.Copy(ConfigurationManager.AppSettings.Get("UXRPRESETALARM"), Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("UXRPRESETALARM"))), true);
            File.SetAttributes(Path.Combine(m_strLiftDir, System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("UXRPRESETALARM"))), FileAttributes.Normal); // file originally read only

            CommandRequest(1, 11, "BEAM"); // issue command reset
            RTURead("UNILRP", 1, strNativeRead, "csarg", System.IO.Path.GetFileName(ConfigurationManager.AppSettings.Get("UXRPRESETALARM"))); // read register 7049 remote fault reset

            string strReadResetAlarm = File.ReadAllText(Path.Combine(m_strLiftDir, strNativeRead));

            Assert.IsTrue(strReadResetAlarm.Contains("<DataEntry index=\"0\" datatype=\"ctSLong(4)\">1</DataEntry>"), "Value of 1 not found for Command Reset!");
        }

        protected void TestDownholePumpoffFillageCommand(int wellNumber)
        {
            string strHostName = Dns.GetHostName();
            string strCommandRequest = String.Empty;
            strCommandRequest = Path.Combine(m_strLiftDir, "mtstbeam.exe");

            try
            {
                Process mtstbeam = new Process();

                mtstbeam.StartInfo.UseShellExecute = false;
                mtstbeam.StartInfo.RedirectStandardOutput = true;
                mtstbeam.StartInfo.FileName = strCommandRequest;
                mtstbeam.StartInfo.Arguments = String.Format(strHostName + " beamdb\\\\cs_beamp 3 " + wellNumber + " " + 21);

                TestContext.WriteLine(strCommandRequest + " " + mtstbeam.StartInfo.Arguments);
                Assert.IsTrue(mtstbeam.Start());

                System.Threading.Thread.Sleep(10000);

                string strOutput = mtstbeam.StandardOutput.ReadToEnd();
                TestContext.WriteLine("Reply from command: {0}", strOutput);

                Assert.IsTrue(strOutput.Contains("Reply Type = 3 Status = 1"), "Downhole pumpoff fillage command was not successful, Status = 1 was not found.");

                mtstbeam.WaitForExit();

            }
            catch (Exception e)
            {
                Assert.Fail("ERROR: {0}", e.ToString());
            }

            System.Threading.Thread.Sleep(10000); // wait 10 seconds after executing command request
        }

    }
}

