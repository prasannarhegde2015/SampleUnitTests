using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WELLPERFORMANCESERVERLib;
using System.Runtime.InteropServices;
using System.IO;
using System.Configuration;
using TestUtilities;

namespace WPSUnitTest
{

    [TestClass]
    public class WPS_1_LayerCollection : BenchmarkedTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            InitializeBenchmarks(this.GetType(), TestContext.TestName);
            System.Threading.Monitor.Enter(WellFloFileLocation.GlobalSyncObject);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            WriteToResultXML();
            System.Threading.Monitor.Exit(WellFloFileLocation.GlobalSyncObject);
        }
        [TestMethod]
        public void LayerCollection_A1_Count()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layers active.wflx");

                WFInt.OpenFile(sfile);

                short Actual = WFInt.WellModel.AddRef().Layers.AddRef().Count;

                //short Expected = 1;

                WFInt.EndWellFlo();
                //Assert.AreEqual(Expected, Actual);
                AreEqual("Count", Actual);
            }
        }

        [TestMethod]
        public void LayerCollection_A2_CountAll()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layers active.wflx");

                WFInt.OpenFile(sfile);

                short Actual = WFInt.WellModel.AddRef().Layers.AddRef().CountAll;

                //short Expected = 2;

                WFInt.EndWellFlo();
                //Assert.AreEqual(Expected, Actual);
                AreEqual("CountAll", Actual);
            }
        }

        [TestMethod]
        public void LayerCollection_A3_Item()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer.wflx");

                WFInt.OpenFile(sfile);

                double Actual = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().DietzFactor, 1);

                //double Expected = 31.6;

                WFInt.EndWellFlo();
                //Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
                AreEqual("DietzFactor", 31.6);
            }
        }

        [TestMethod]
        public void Layer_Put_General_Data()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer.wflx");
                string OutPutfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layer.wflx");

                WFInt.OpenFile(sfile);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().Name = "First Layer";
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().Pressure = 7000;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().Temperature = 200;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().MeasuredDepth = 14500;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().HorizontalPermeability = 210;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().Thickness = 110;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WellBoreRadius = 0.45;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WGR = 160;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().CGR = 170;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().UseCalculatedSkin = true;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().VerticalPermeability = 220;

                WFInt.SaveFile(OutPutfile);

                WFloInterface WFInt1 = new WFloInterface();
                WFInt1.AddRef();

                WFInt1.OpenFile(OutPutfile);

                string ActualName = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().Name;
                double ActualPressure = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().Pressure;
                double ActualTemperature = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().Temperature;
                double ActualMeasuredDepth = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().MeasuredDepth;
                double ActualHorizontalPermeability = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().HorizontalPermeability;
                double ActualThickness = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().Thickness;
                double ActualWellBoreRadius = Math.Round(WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WellBoreRadius, 2);
                double ActualWGR = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WGR;
                double ActualCGR = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().CGR;
                bool ActualUseCalculatedSkin = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().UseCalculatedSkin;
                //double ActualDarcyFlowCoeff = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().DarcyFlowCoeff;
                //double ActualNonDarcyFlowCoeff = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().NonDarcyFlowCoeff;
                double ActualAOF = Math.Round(WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().AOF, 4);
                double ActualVerticalPermeability = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().VerticalPermeability;

                string ExpectedName = "First Layer";
                double ExpectedPressure = 7000;
                double ExpectedTemperature = 200;
                double ExpectedMeasuredDepth = 14500;
                double ExpectedHorizontalPermeability = 210;
                double ExpectedThickness = 110;
                double ExpectedWellBoreRadius = 0.45;
                double ExpectedWGR = 160;
                double ExpectedCGR = 170;
                bool ExpectedUseCalculatedSkin = true;
                double ExpectedAOF = 812.6196;
                double ExpectedVerticalPermeability = 220;

                Assert.AreEqual(ExpectedName, ActualName);
                Assert.AreEqual(ExpectedPressure, ActualPressure, DeltaFraction.Default(ExpectedPressure));
                Assert.AreEqual(ExpectedTemperature, ActualTemperature, DeltaFraction.Default(ExpectedTemperature));
                Assert.AreEqual(ExpectedMeasuredDepth, ActualMeasuredDepth, DeltaFraction.Default(ExpectedMeasuredDepth));
                Assert.AreEqual(ExpectedHorizontalPermeability, ActualHorizontalPermeability, DeltaFraction.Default(ExpectedHorizontalPermeability));
                Assert.AreEqual(ExpectedThickness, ActualThickness, DeltaFraction.Default(ExpectedThickness));
                Assert.AreEqual(ExpectedWellBoreRadius, ActualWellBoreRadius, DeltaFraction.Default(ExpectedWellBoreRadius));
                Assert.AreEqual(ExpectedWGR, ActualWGR, DeltaFraction.Default(ExpectedWGR));
                Assert.AreEqual(ExpectedCGR, ActualCGR, DeltaFraction.Default(ExpectedCGR));
                Assert.AreEqual(ActualUseCalculatedSkin, ExpectedUseCalculatedSkin);
                Assert.AreEqual(ExpectedAOF, ActualAOF, DeltaFraction.Default(ExpectedAOF));
                Assert.AreEqual(ExpectedVerticalPermeability, ActualVerticalPermeability, DeltaFraction.Default(ExpectedVerticalPermeability));
            }
        }

        [TestMethod]
        public void Layer_Put_Manual_Fetkovich()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer/BlackOil_Manual_Fetkovich.wflx");

                WFInt.OpenFile(sfile);

                double PI = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().ProductivityIndex;
                Assert.AreEqual(6000, PI, DeltaFraction.Default(6000));
            }
        }

        [TestMethod]
        public void Layer_Put_LayerParameters_Vogel()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer rate - set 4.wflx");
                string OutPutfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layer rate - set 4.wflx");

                WFInt.OpenFile(sfile);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WaterCut = 0.3;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().VogelPCoefficient = 0.25;

                WFInt.SaveFile(OutPutfile);

                WFloInterface WFInt1 = new WFloInterface();
                WFInt1.AddRef();

                WFInt1.OpenFile(OutPutfile);

                double ActualWaterCut = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WaterCut;
                double ActualVogelPCoefficient = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().VogelPCoefficient;
                double ActualProductivityIndex = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().ProductivityIndex;
                double ActualAOF = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().AOF;

                double ExpectedWaterCut = 0.3;
                double ExpectedVogelPCoefficient = 0.25;
                double ExpectedProductivityIndex = 7.8599;
                double ExpectedAOF = 37637.9218;

                Assert.AreEqual(ExpectedWaterCut, ActualWaterCut, DeltaFraction.Default(ExpectedWaterCut));
                Assert.AreEqual(ExpectedVogelPCoefficient, ActualVogelPCoefficient, DeltaFraction.Default(ExpectedVogelPCoefficient));
                Assert.AreEqual(ExpectedProductivityIndex, ActualProductivityIndex, DeltaFraction.Default(ExpectedProductivityIndex));
                Assert.AreEqual(ExpectedAOF, ActualAOF, DeltaFraction.Default(ExpectedAOF));
            }
        }

        [TestMethod]
        public void Layer_Put_TestData_Vogel()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer rate - set 4.wflx");
                string OutPutfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layer rate - set 4.wflx");

                WFInt.OpenFile(sfile);
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().IPREntryModel = 1; //1 - Test Entry Model
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WaterCut = 0.3;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().VogelPCoefficient = 0.25;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().SetTestPoint1Data(5000, 1250);

                WFInt.SaveFile(OutPutfile);

                WFloInterface WFInt1 = new WFloInterface();
                WFInt1.AddRef();

                WFInt1.OpenFile(OutPutfile);

                double ActualWaterCut = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WaterCut;
                double ActualVogelPCoefficient = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().VogelPCoefficient;
                double ActualProductivityIndex = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().ProductivityIndex;
                double ActualAOF = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().AOF;

                double ExpectedWaterCut = 0.3;
                double ExpectedVogelPCoefficient = 0.25;
                double ExpectedProductivityIndex = 1.2500;
                double ExpectedAOF = 5985.7;

                Assert.AreEqual(ExpectedWaterCut, ActualWaterCut, DeltaFraction.Default(ExpectedWaterCut));
                Assert.AreEqual(ExpectedVogelPCoefficient, ActualVogelPCoefficient, DeltaFraction.Default(ExpectedVogelPCoefficient));
                Assert.AreEqual(ExpectedProductivityIndex, ActualProductivityIndex, DeltaFraction.Default(ExpectedProductivityIndex));
                Assert.AreEqual(ExpectedAOF, ActualAOF, DeltaFraction.Default(ExpectedAOF));
            }
        }

        [TestMethod]
        public void Layer_Put_Manual_Vogel()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer rate - set 4.wflx");
                string OutPutfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layer rate - set 4.wflx");

                WFInt.OpenFile(sfile);
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().IPREntryModel = 2; //2 - Manual Entry Model
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WaterCut = 0.3;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().VogelPCoefficient = 0.25;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().ProductivityIndex = 322.5;

                WFInt.SaveFile(OutPutfile);

                WFloInterface WFInt1 = new WFloInterface();
                WFInt1.AddRef();

                WFInt1.OpenFile(OutPutfile);

                double ActualWaterCut = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WaterCut;
                double ActualVogelPCoefficient = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().VogelPCoefficient;
                double ActualProductivityIndex = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().ProductivityIndex;
                double ActualAOF = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().AOF;

                double ExpectedWaterCut = 0.3;
                double ExpectedVogelPCoefficient = 0.25;
                double ExpectedProductivityIndex = 322.5;
                double ExpectedAOF = 1544322.1;

                Assert.AreEqual(ExpectedWaterCut, ActualWaterCut, DeltaFraction.Default(ExpectedWaterCut));
                Assert.AreEqual(ExpectedVogelPCoefficient, ActualVogelPCoefficient, DeltaFraction.Default(ExpectedVogelPCoefficient));
                Assert.AreEqual(ExpectedProductivityIndex, ActualProductivityIndex, DeltaFraction.Default(ExpectedProductivityIndex));
                Assert.AreEqual(ExpectedAOF, ActualAOF, DeltaFraction.Default(ExpectedAOF));
            }
        }

        [TestMethod]
        public void Layer_Put_IPRModel()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer rate - set 4.wflx");
                string OutPutfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layer rate - set 4.wflx");

                WFInt.OpenFile(sfile);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().IPRModel = 2;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().FetkovichCCoeff = 1.1;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().FetkovichNExponent = 0.9;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().DietzFactor = 27.6;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().DrainageArea = 3100000.0;

                WFInt.SaveFile(OutPutfile);

                WFloInterface WFInt1 = new WFloInterface();
                WFInt1.AddRef();

                WFInt1.OpenFile(OutPutfile);

                short ActualIPRModel = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().IPRModel;
                double ActualFetkovichCCoeff = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().FetkovichCCoeff;
                double ActualFetkovichNExponent = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().FetkovichNExponent;
                double ActualDietzFactor = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().DietzFactor;
                double ActualDrainageArea = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().DrainageArea;
                double ActualExternalRadius = Math.Round(WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().ExternalRadius, 2);

                short ExpectedIPRModel = 2;
                double ExpectedFetkovichCCoeff = 1.1;
                double ExpectedFetkovichNExponent = 0.9;
                double ExpectedDietzFactor = 27.6;
                double ExpectedDrainageArea = 3100000.0;
                double ExpectedExternalRadius = 993.36;

                Assert.AreEqual(ExpectedIPRModel, ActualIPRModel);
                Assert.AreEqual(ExpectedFetkovichCCoeff, ActualFetkovichCCoeff, DeltaFraction.Default(ExpectedFetkovichCCoeff));
                Assert.AreEqual(ExpectedFetkovichNExponent, ActualFetkovichNExponent, DeltaFraction.Default(ExpectedFetkovichNExponent));
                Assert.AreEqual(ExpectedDietzFactor, ActualDietzFactor, DeltaFraction.Default(ExpectedDietzFactor));
                Assert.AreEqual(ExpectedDrainageArea, ActualDrainageArea, DeltaFraction.Default(ExpectedDrainageArea));
                Assert.AreEqual(ExpectedExternalRadius, ActualExternalRadius, DeltaFraction.Default(ExpectedExternalRadius));
            }
        }

        [TestMethod]
        public void Layer_Put_Darcy_NonDarcy_Skin()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer.wflx");
                string OutPutfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layer.wflx");

                WFInt.OpenFile(sfile);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().DarcySkin = 0.9;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().NonDarcySkin = 0.1;

                WFInt.SaveFile(OutPutfile);

                WFloInterface WFInt1 = new WFloInterface();
                WFInt1.AddRef();

                WFInt1.OpenFile(OutPutfile);

                double ActualDarcySkin = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().DarcySkin;
                double ActualNonDarcySkin = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().NonDarcySkin;

                //double ExpectedDarcySkin = 0.9;
                //double ExpectedNonDarcySkin = 0.1;

                AreEqual("DarcySkin", ActualDarcySkin);
                AreEqual("NonDarcySkin", ActualNonDarcySkin);
            }
        }

        [TestMethod]
        public void Layer_Put_DrainageShape()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer.wflx");
                string OutPutfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layer.wflx");

                WFInt.OpenFile(sfile);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().DrainageShape = 1;

                WFInt.SaveFile(OutPutfile);

                WFloInterface WFInt1 = new WFloInterface();
                WFInt1.AddRef();

                WFInt1.OpenFile(OutPutfile);

                short ActualDrainageShape = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().DrainageShape;

                short ExpectedDrainageShape = 1;

                Assert.AreEqual(ExpectedDrainageShape, ActualDrainageShape);
            }
        }

        [TestMethod]
        public void Layer_Put_FluidParameter_API()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer.wflx");
                string OutPutfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layer.wflx");

                WFInt.OpenFile(sfile);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().OilAPIGravity = 50;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GasGravity = 0.8;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().Salinity = 31000;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().CO2MolePct = 0.1;//Per Cent in Wellflo
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().N2MolePct = 0.2;//Per Cent in Wellflo
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().H2SMolePct = 0.3;//Per Cent in Wellflo

                WFInt.SaveFile(OutPutfile);

                WFloInterface WFInt1 = new WFloInterface();
                WFInt1.AddRef();

                WFInt1.OpenFile(OutPutfile);

                double ActualOilAPIGravity = Math.Round(WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().OilAPIGravity, 4);
                double ActualGasGravity = Math.Round(WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GasGravity, 4);
                double ActualSalinity = Math.Round(WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().Salinity, 4);
                double ActualCO2MolePct = Math.Round(WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().CO2MolePct, 4);
                double ActualN2MolePct = Math.Round(WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().N2MolePct, 4);
                double ActualH2SMolePct = Math.Round(WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().H2SMolePct, 4);

                double ExpectedOilAPIGravity = 50;
                double ExpectedGasGravity = 0.8;
                double ExpectedSalinity = 31000;
                double ExpectedCO2MolePct = 0.1;
                double ExpectedN2MolePct = 0.2;
                double ExpectedH2SMolePct = 0.3;

                Assert.AreEqual(ExpectedOilAPIGravity, ActualOilAPIGravity, DeltaFraction.Default(ExpectedOilAPIGravity));
                Assert.AreEqual(ExpectedGasGravity, ActualGasGravity, DeltaFraction.Default(ExpectedGasGravity));
                Assert.AreEqual(ExpectedSalinity, ActualSalinity, DeltaFraction.Default(ExpectedSalinity));
                Assert.AreEqual(ExpectedCO2MolePct, ActualCO2MolePct, DeltaFraction.Default(ExpectedCO2MolePct));
                Assert.AreEqual(ExpectedN2MolePct, ActualN2MolePct, DeltaFraction.Default(ExpectedN2MolePct));
                Assert.AreEqual(ExpectedH2SMolePct, ActualH2SMolePct, DeltaFraction.Default(ExpectedH2SMolePct));
            }
        }

        [TestMethod]
        public void Layer_Put_FluidParameter_Specific()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer.wflx");
                string OutPutfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layer.wflx");

                WFInt.OpenFile(sfile);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().OilGravity = 0.85;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WaterGravity = 1.1;

                WFInt.SaveFile(OutPutfile);

                WFloInterface WFInt1 = new WFloInterface();
                WFInt1.AddRef();

                WFInt1.OpenFile(OutPutfile);

                double ActualOilGravity = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().OilGravity;
                double ActualWaterGravity = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WaterGravity;

                double ExpectedOilGravity = 0.85;
                double ExpectedWaterGravity = 1.1;

                Assert.AreEqual(ExpectedOilGravity, ActualOilGravity, DeltaFraction.Default(ExpectedOilGravity));
                Assert.AreEqual(ExpectedWaterGravity, ActualWaterGravity, DeltaFraction.Default(ExpectedWaterGravity));

            }
        }

        [TestMethod]
        public void Layer_Put_PartingPressure()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\injection well.wflx");
                string OutPutfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved injection well.wflx");

                WFInt.OpenFile(sfile);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().PartingPressure = 7100;

                WFInt.SaveFile(OutPutfile);

                WFloInterface WFInt1 = new WFloInterface();
                WFInt1.AddRef();

                WFInt1.OpenFile(OutPutfile);

                double ActualOilGravity = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().PartingPressure;

                double ExpectedPartingPressure = 7100;

                Assert.AreEqual(ExpectedPartingPressure, ActualOilGravity, DeltaFraction.Default(ExpectedPartingPressure));
            }
        }

        [TestMethod]
        public void LayerCollection_LayerStatus()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layers active.wflx");
                string sfileOutPut = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layers active.wflx");

                WFInt.OpenFile(sfile);

                LayerCollection pLayerCollection = new LayerCollection();
                pLayerCollection.AddRef();

                WFInt.WellModel.AddRef().GetLayerCollectionData(pLayerCollection);

                pLayerCollection.SetLayerStatus(2, true);
                pLayerCollection.SetLayerStatus(1, true);

                WFInt.WellModel.AddRef().SetLayerCollectionData(pLayerCollection);

                WFInt.SaveFile(sfileOutPut);

                WFloInterface WFInt1 = new WFloInterface();
                WFInt1.AddRef();
                WFInt1.OpenFile(sfileOutPut);

                LayerCollection pLayerCollection1 = new LayerCollection();
                pLayerCollection1.AddRef();

                WFInt1.WellModel.AddRef().GetLayerCollectionData(pLayerCollection1);

                bool ActualStatus1 = pLayerCollection1.GetLayerStatus(1);
                string ActualName1 = pLayerCollection1.GetLayerName(1);
                bool ActualStatus2 = pLayerCollection1.GetLayerStatus(2);
                string ActualName2 = pLayerCollection1.GetLayerName(2);

                Assert.AreEqual(true, ActualStatus1);
                Assert.AreEqual("Layer 1", ActualName1);
                Assert.AreEqual(true, ActualStatus2);
                Assert.AreEqual("New Layer *1", ActualName2);
            }
        }

        [TestMethod]
        public void LayerCollection_ReservoirType_UnConventional()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layers active.wflx");
                string sfileOutPut = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layers active.wflx");

                WFInt.OpenFile(sfile);

                short beforeChange = WFInt.WellModel.AddRef().Layers.AddRef().ReservoirType;
                Assert.AreEqual(0, beforeChange);

                WFInt.WellModel.AddRef().Layers.AddRef().ReservoirType = 1;  // UnConventional Reservoir
                WFInt.SaveFile(sfileOutPut);

                WFloInterface WFInt1 = new WFloInterface();
                WFInt1.AddRef();

                WFInt1.OpenFile(sfileOutPut);
                short afterChange = WFInt1.WellModel.AddRef().Layers.AddRef().ReservoirType;

                Assert.AreEqual(1, afterChange);
            }
        }
    }

    [TestClass]
    public class WPS_2_Layer
    {
        [TestInitialize]
        public void TestInitialize()
        {
            System.Threading.Monitor.Enter(WellFloFileLocation.GlobalSyncObject);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            System.Threading.Monitor.Exit(WellFloFileLocation.GlobalSyncObject);
        }

        [TestMethod]
        public void Layer_A1_CGR()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer.wflx");

                WFInt.OpenFile(sfile);

                double ActualCGR = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().CGR;
                double ActualCO2MolePct = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().CO2MolePct;
                double ActualDarcySkin = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().DarcySkin;
                double ActualDietzFactor = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().DietzFactor, 2);
                double ActualDrainageArea = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().DrainageArea;
                double ActualGasGravity = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GasGravity, 1);
                double ActualH2SMolePct = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().H2SMolePct;
                double ActualHorizontalPermeability = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().HorizontalPermeability;
                double ActualIPREntryModel = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().IPREntryModel;
                double ActualIPRModel = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().IPRModel;
                double ActualMD = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().MeasuredDepth;
                double ActualN2MolePct = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().N2MolePct;
                double ActualNonDarcySkin = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().NonDarcySkin;
                double ActualOilGravity = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().OilGravity, 3);
                double ActualPressure = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().Pressure;
                double ActualSalinity = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().Salinity;
                double ActualTemperature = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().Temperature;
                double ActualThickness = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().Thickness;
                double ActualTrueVerticalDepth = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().TrueVerticalDepth, 2);
                double ActualVerticalPermeability = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().VerticalPermeability, 0);
                double ActualWaterGravity = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WaterGravity, 4);
                double ActualWellBoreRadius = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WellBoreRadius, 1);
                double ActualWGR = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WGR;
                bool ActualNonDarcyModel = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().NonDarcyModel;
                int ActualSaturationModel = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().SaturationModel;
                short ActualNumber = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().Number;
                string ActualName = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().Name;

                double ExpectedCGR = 150;
                double ExpectedCO2MolePct = 0;
                double ExpectedDarcySkin = 1;
                double ExpectedDietzFactor = 31.62;
                double ExpectedDrainageArea = 27878006;
                double ExpectedGasGravity = 0.7;
                double ExpectedH2SMolePct = 0;
                double ExpectedHorizontalPermeability = 200;
                double ExpectedIPREntryModel = 0;
                double ExpectedIPRModel = 3;
                double ExpectedMD = 14680;
                double ExpectedN2MolePct = 0;
                double ExpectedNonDarcySkin = 0;
                double ExpectedOilGravity = 0.785;
                double ExpectedPressure = 8050;
                double ExpectedSalinity = 30000;
                double ExpectedTemperature = 230;
                double ExpectedThickness = 100;
                double ExpectedTrueVerticalDepth = 13299.66;
                double ExpectedVerticalPermeability = 200;
                double ExpectedWaterGravity = 1.0198;
                double ExpectedWellBoreRadius = 0.4;
                double ExpectedWGR = 150;
                bool ExpectedNonDarcyModel = true;
                int ExpectedSaturationModel = 0;
                short ExpectedNumber = 1;
                string ExpectedName = "Layer 1";

                Assert.AreEqual(ExpectedCGR, ActualCGR, DeltaFraction.Default(ExpectedCGR));
                Assert.AreEqual(ExpectedCO2MolePct, ActualCO2MolePct, DeltaFraction.Default(ExpectedCO2MolePct));
                Assert.AreEqual(ExpectedDarcySkin, ActualDarcySkin, DeltaFraction.Default(ExpectedDarcySkin));
                Assert.AreEqual(ExpectedDietzFactor, ActualDietzFactor, DeltaFraction.Default(ExpectedDietzFactor));
                Assert.AreEqual(ExpectedDrainageArea, ActualDrainageArea, DeltaFraction.Default(ExpectedDrainageArea));
                Assert.AreEqual(ExpectedGasGravity, ActualGasGravity, DeltaFraction.Default(ExpectedGasGravity));
                Assert.AreEqual(ExpectedH2SMolePct, ActualH2SMolePct, DeltaFraction.Default(ExpectedH2SMolePct));
                Assert.AreEqual(ExpectedHorizontalPermeability, ActualHorizontalPermeability, DeltaFraction.Default(ExpectedHorizontalPermeability));
                Assert.AreEqual(ExpectedIPREntryModel, ActualIPREntryModel, DeltaFraction.Default(ExpectedIPREntryModel));
                Assert.AreEqual(ExpectedIPRModel, ActualIPRModel, DeltaFraction.Default(ExpectedIPRModel));
                Assert.AreEqual(ExpectedMD, ActualMD, DeltaFraction.Default(ExpectedMD));
                Assert.AreEqual(ExpectedN2MolePct, ActualN2MolePct, DeltaFraction.Default(ExpectedN2MolePct));
                Assert.AreEqual(ExpectedNonDarcySkin, ActualNonDarcySkin, DeltaFraction.Default(ExpectedNonDarcySkin));
                Assert.AreEqual(ExpectedOilGravity, ActualOilGravity, DeltaFraction.Default(ExpectedOilGravity));
                Assert.AreEqual(ExpectedPressure, ActualPressure, DeltaFraction.Default(ExpectedPressure));
                Assert.AreEqual(ExpectedSalinity, ActualSalinity, DeltaFraction.Default(ExpectedSalinity));
                Assert.AreEqual(ExpectedTemperature, ActualTemperature, DeltaFraction.Default(ExpectedTemperature));
                Assert.AreEqual(ExpectedThickness, ActualThickness, DeltaFraction.Default(ExpectedThickness));
                Assert.AreEqual(ExpectedTrueVerticalDepth, ActualTrueVerticalDepth, DeltaFraction.Default(ExpectedTrueVerticalDepth));
                Assert.AreEqual(ExpectedVerticalPermeability, ActualVerticalPermeability, DeltaFraction.Default(ExpectedVerticalPermeability));
                Assert.AreEqual(ExpectedWaterGravity, ActualWaterGravity, DeltaFraction.Default(ExpectedWaterGravity));
                Assert.AreEqual(ExpectedWellBoreRadius, ActualWellBoreRadius, DeltaFraction.Default(ExpectedWellBoreRadius));
                Assert.AreEqual(ExpectedWGR, ActualWGR, DeltaFraction.Default(ExpectedWGR));
                Assert.AreEqual(ExpectedNonDarcyModel, ActualNonDarcyModel);
                Assert.AreEqual(ExpectedSaturationModel, ActualSaturationModel);
                Assert.AreEqual(ExpectedNumber, ActualNumber);
                Assert.AreEqual(ExpectedName, ActualName);
            }
        }

        [TestMethod]
        public void Layer_A1_TrueVerticalDepthfromReference()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layerWithReferenceDepth.wflx");

                WFInt.OpenFile(sfile);

                double ActualTrueVerticalDepth1 = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().TrueVerticalDepth, 2);
                double ActualTrueVerticalDepthfromReference1 = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().TrueVerticalDepthfromReference, 2);
                double ActualTrueVerticalDepth2 = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(2).AddRef().TrueVerticalDepth, 2);
                double ActualTrueVerticalDepthfromReference2 = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(2).AddRef().TrueVerticalDepthfromReference, 2);

                Assert.AreEqual(13319.66, ActualTrueVerticalDepth1, DeltaFraction.Default(13319.66));
                Assert.AreEqual(13299.66, ActualTrueVerticalDepthfromReference1, DeltaFraction.Default(13299.66));
                Assert.AreEqual(14269.96, ActualTrueVerticalDepth2, DeltaFraction.Default(14269.96));
                Assert.AreEqual(14249.96, ActualTrueVerticalDepthfromReference2, DeltaFraction.Default(14249.96));
            }
        }

        [TestMethod]
        public void Layer_A1_GetViscosityModelingData()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\PCP-ViscosityModeling.wflx");
                string OutPutfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layer.wflx");

                WFInt.OpenFile(sfile);


                Array psaTemperature = Array.CreateInstance(typeof(double), 20);
                Array psaViscosity = Array.CreateInstance(typeof(double), 20);
                short ActualNo = 0;

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetViscosityModelingData(ref ActualNo, ref psaTemperature, ref psaViscosity);
                double TempratureAt0 = (double)psaTemperature.GetValue(0);
                double TempratureAt1 = (double)psaTemperature.GetValue(1);

                double ViscosityAt0 = (double)psaViscosity.GetValue(0);
                double ViscosityAt1 = (double)psaViscosity.GetValue(1);

                Assert.AreEqual(80, TempratureAt0, DeltaFraction.Default(80));
                Assert.AreEqual(85, TempratureAt1, DeltaFraction.Default(85));
                Assert.AreEqual(17, ViscosityAt0, DeltaFraction.Default(17));
                Assert.AreEqual(15, ViscosityAt1, DeltaFraction.Default(15));


                psaTemperature.SetValue(90, 2);
                psaViscosity.SetValue(13, 2);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().SetViscosityModelingData(3, psaTemperature, psaViscosity);
                WFInt.SaveFile(OutPutfile);
            }
        }

        [TestMethod]
        public void Layer_A1_MultiLayer_Both_Active()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_ReoForecast\\before active layer.wflx");

                WFInt.OpenFile(sfile);

                bool firstLayer = WFInt.WellModel.AddRef().Layers.AddRef().GetLayerStatus(1);

                bool secondLayer = WFInt.WellModel.AddRef().Layers.AddRef().GetLayerStatus(2);

                Assert.AreEqual(false, firstLayer);
                Assert.AreEqual(true, secondLayer);

                //make both layer active
                WFInt.WellModel.AddRef().Layers.AddRef().SetLayerStatus(1, true);

                WFInt.AddRef().GetOpPtCalculator().AddRef().CalculateOperatingPoint(50, 0);

                double LiqRateAfterSet = WFInt.GetOpPtCalculator().AddRef().OilRate + WFInt.GetOpPtCalculator().AddRef().WaterRate;

                double PressureAfterSet = WFInt.GetOpPtCalculator().AddRef().OperatingPressure;

                Assert.AreEqual(4778.4, LiqRateAfterSet, DeltaFraction.Default(4778.4));
                Assert.AreEqual(3655.1, PressureAfterSet, DeltaFraction.Default(3655.1));

                bool firstLayerAfterChange = WFInt.WellModel.AddRef().Layers.AddRef().GetLayerStatus(1);
                bool secondLayerAfterChange = WFInt.WellModel.AddRef().Layers.AddRef().GetLayerStatus(2);

                Assert.AreEqual(true, firstLayerAfterChange);
                Assert.AreEqual(true, secondLayerAfterChange);

                // just making it unchanges beacuse many times changes in global variable causes a problem
                WFInt.WellModel.AddRef().Layers.AddRef().SetLayerStatus(2, true);
                WFInt.WellModel.AddRef().Layers.AddRef().SetLayerStatus(1, false);
            }
        }

        [TestMethod]
        public void Layer_A1_MultiLayer_Active_InActive()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_ReoForecast\\before active layer.wflx");

                WFInt.OpenFile(sfile);

                //make first layer active and second inactive
                WFInt.WellModel.AddRef().Layers.AddRef().SetLayerStatus(1, true);
                WFInt.WellModel.AddRef().Layers.AddRef().SetLayerStatus(2, false);

                WFInt.AddRef().GetOpPtCalculator().AddRef().CalculateOperatingPoint(50, 0);

                double LiqRateAfterSet1 = WFInt.GetOpPtCalculator().AddRef().OilRate + WFInt.GetOpPtCalculator().AddRef().WaterRate;

                double PressureAfterSet1 = WFInt.GetOpPtCalculator().AddRef().OperatingPressure;

                Assert.AreEqual(3392.9, LiqRateAfterSet1, DeltaFraction.Default(3392.9));
                Assert.AreEqual(3285.59, PressureAfterSet1, DeltaFraction.Default(3285.59));

                bool firstLayerAfterChange = WFInt.WellModel.AddRef().Layers.AddRef().GetLayerStatus(1);
                bool secondLayerAfterChange = WFInt.WellModel.AddRef().Layers.AddRef().GetLayerStatus(2);

                Assert.AreEqual(true, firstLayerAfterChange);
                Assert.AreEqual(false, secondLayerAfterChange);

                // just making it unchanges beacuse many times changes in global variable causes a problem  
                WFInt.WellModel.AddRef().Layers.AddRef().SetLayerStatus(2, true);
                WFInt.WellModel.AddRef().Layers.AddRef().SetLayerStatus(1, false);
            }
        }

        [TestMethod]
        public void Layer_A1_SetViscosityModelingData()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\PCP-ViscosityModeling.wflx");
                string OutPutfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layer.wflx");

                WFInt.OpenFile(sfile);


                Array psaTemperatureOld = Array.CreateInstance(typeof(double), 20);
                Array psaViscosityOld = Array.CreateInstance(typeof(double), 20);
                short ActualNo = 0;

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetViscosityModelingData(ref ActualNo, ref psaTemperatureOld, ref psaViscosityOld);

                psaTemperatureOld.SetValue(90, 2);
                psaViscosityOld.SetValue(13, 2);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().SetViscosityModelingData(3, psaTemperatureOld, psaViscosityOld);
                WFInt.SaveFile(OutPutfile);

                Array psaTemperatureNew = Array.CreateInstance(typeof(double), 20);
                Array psaViscosityNew = Array.CreateInstance(typeof(double), 20);

                WFloInterface WFIntSaved = new WFloInterface();
                WFIntSaved.AddRef();

                WFIntSaved.OpenFile(OutPutfile);

                WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetViscosityModelingData(ref ActualNo, ref psaTemperatureNew, ref psaViscosityNew);

                double TempratureAt0 = (double)psaTemperatureNew.GetValue(0);
                double TempratureAt1 = (double)psaTemperatureNew.GetValue(1);
                double TempratureAt2 = (double)psaTemperatureNew.GetValue(2);

                double ViscosityAt0 = (double)psaViscosityNew.GetValue(0);
                double ViscosityAt1 = (double)psaViscosityNew.GetValue(1);
                double ViscosityAt2 = (double)psaViscosityNew.GetValue(2);

                Assert.AreEqual(80, TempratureAt0, DeltaFraction.Default(80));
                Assert.AreEqual(85, TempratureAt1, DeltaFraction.Default(85));
                Assert.AreEqual(90, TempratureAt2, DeltaFraction.Default(90));
                Assert.AreEqual(17, ViscosityAt0, DeltaFraction.Default(17));
                Assert.AreEqual(15, ViscosityAt1, DeltaFraction.Default(15));
                Assert.AreEqual(13, ViscosityAt2, DeltaFraction.Default(13));
            }
        }

        [TestMethod]
        public void Layer_A1_CalculateProductivityIndex()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\PCP-ViscosityModeling.wflx");

                WFInt.OpenFile(sfile);
                double dIndex = 0;

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().CalculateProductivityIndex(400, 400, ref dIndex);

                double ActualIndex = Math.Round(dIndex, 4);
                Assert.AreEqual(0.3075, ActualIndex, DeltaFraction.Default(0.3077));
            }
        }

        [TestMethod]
        public void Layer_A1A_1_WellOrientation()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\vertical.wflx");

                WFInt.OpenFile(sfile);

                short Actual = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WellOrientation;

                short Expected = 0;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual);
            }
        }

        [TestMethod]
        public void Layer_A1A_2_WellOrientation_Input()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\vertical.wflx");
                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\vertical_Output.wflx");

                WFInt.OpenFile(sfile);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WellOrientation = 1;

                short Actual = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WellOrientation;

                short Expected = 1;

                WFInt.SaveFile(sfile1);

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual);
            }
        }

        [TestMethod]
        public void Layer_A1B_1_WellOrientation()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\horizontal.wflx");

                WFInt.OpenFile(sfile);

                short Actual = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WellOrientation;

                short Expected = 1;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual);
            }
        }

        [TestMethod]
        public void Layer_A1B_2_WellOrientation_Input()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\horizontal.wflx");
                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\horizontal_Output.wflx");

                WFInt.OpenFile(sfile);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WellOrientation = 0;

                short Actual = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WellOrientation;

                short Expected = 0;

                WFInt.SaveFile(sfile1);

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual);
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A2_GOR()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer.wflx");

                WFInt.OpenFile(sfile1);

                double Actual = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GOR;

                double Expected = 500;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A2_ProductivityIndex()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer.wflx");

                WFInt.OpenFile(sfile1);

                double Actual = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().ProductivityIndex, 2);

                double Expected = 1.25;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void Layer_A2_RelInjectivity()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer.wflx");

                WFInt.OpenFile(sfile1);

                double Actual = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(2).AddRef().RelInjectivity, 4);

                double Expected = 0.2100;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void Layer_A2_TestPoint1Pressure()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer.wflx");

                WFInt.OpenFile(sfile1);

                double Actual = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(2).AddRef().TestPoint1Pressure, 0);

                double Expected = 5000;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void Layer_A2_TestPoint1Rate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer.wflx");

                WFInt.OpenFile(sfile1);

                double Actual = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(2).AddRef().TestPoint1Rate, 0);

                double Expected = 1250;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A2_VogelPCoefficient()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer.wflx");

                WFInt.OpenFile(sfile1);

                double Actual = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().VogelPCoefficient, 2);

                double Expected = 0.22;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }
        [TestMethod]
        public void Layer_New_VogelPCoefficient()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layers active.wflx");

                WFInt.OpenFile(sfile1);

                double Actual = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(2).AddRef().VogelPCoefficient, 2);

                double Expected = 0.2;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A2_WaterCut()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer.wflx");

                WFInt.OpenFile(sfile1);

                double Actual = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WaterCut, 1);

                double Expected1 = 0.30;
                double Expected = Math.Round(Expected1, 1);

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }
        [TestMethod]
        [Ignore]
        public void Layer_A1_WaterCut()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer.wflx");

                WFInt.OpenFile(sfile1);

                double Actual = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(2).AddRef().WaterCut, 1);

                double Expected1 = 0.0;
                double Expected = Math.Round(Expected1, 1);

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void Layer_A3_PartingPressure()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\injection well.wflx");

                WFInt.OpenFile(sfile1);

                double Actual = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().PartingPressure, 0);

                double Expected = 7000;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void Layer_A4_CalcBHPOfGasWell()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\gas well BHP.wflx");

                WFInt.OpenFile(sfile1);

                double fQGas = 1.98;
                double fCGR = 0;
                double fWGR = 350;
                double fTHP = 200;

                double Actual = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().CalcBHPOfGasWell(fQGas, fCGR, fWGR, fTHP), 2);

                double Expected = 378.71;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void Layer_A5_CalcBHPOfOilWell()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\oil well BHP.wflx");

                WFInt.OpenFile(sfile1);

                double fQOil = 2696.4;
                double fGOR = 500;
                double fWCT = 0.25;
                double fTHP = 100;

                double Actual = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().CalcBHPOfOilWell(fQOil, fGOR, fWCT, fTHP), 3);

                double Expected1 = 4043.613;
                double Expected = Math.Round(Expected1, 3);

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void Layer_A6_GetRelPermParmsOilWater_1_psaRelPerms_0()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms = Array.CreateInstance(typeof(double), 2);
                Array psaIrrSat = Array.CreateInstance(typeof(double), 2);
                Array psaExp = Array.CreateInstance(typeof(double), 2);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermParmsOilWater(ref psaRelParms, ref psaIrrSat, ref psaExp);

                string i = psaRelParms.GetValue(0).ToString();

                double Actual = Math.Round(System.Convert.ToDouble(i), 0);

                double Expected = 1;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void Layer_A6_GetRelPermParmsOilWater_1_psaRelPerms_1()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms = Array.CreateInstance(typeof(double), 2);
                Array psaIrrSat = Array.CreateInstance(typeof(double), 2);
                Array psaExp = Array.CreateInstance(typeof(double), 2);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermParmsOilWater(ref psaRelParms, ref psaIrrSat, ref psaExp);

                string i = psaRelParms.GetValue(1).ToString();

                double Actual = Math.Round(System.Convert.ToDouble(i), 1);

                double Expected = 0.5;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void Layer_A6_GetRelPermParmsOilWater_2_psaIrrSat_0()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms = Array.CreateInstance(typeof(double), 2);
                Array psaIrrSat = Array.CreateInstance(typeof(double), 2);
                Array psaExp = Array.CreateInstance(typeof(double), 2);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermParmsOilWater(ref psaRelParms, ref psaIrrSat, ref psaExp);

                string i = psaIrrSat.GetValue(0).ToString();

                double Actual = Math.Round(System.Convert.ToDouble(i), 2);

                double Expected = 0.25;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void Layer_A6_GetRelPermParmsOilWater_2_psaIrrSat_1()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms = Array.CreateInstance(typeof(double), 2);
                Array psaIrrSat = Array.CreateInstance(typeof(double), 2);
                Array psaExp = Array.CreateInstance(typeof(double), 2);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermParmsOilWater(ref psaRelParms, ref psaIrrSat, ref psaExp);

                string i = psaIrrSat.GetValue(1).ToString();

                double Actual = Math.Round(System.Convert.ToDouble(i), 2);

                double Expected = 0.3;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void Layer_A6_GetRelPermParmsOilWater_3_psaExp_0()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms = Array.CreateInstance(typeof(double), 2);
                Array psaIrrSat = Array.CreateInstance(typeof(double), 2);
                Array psaExp = Array.CreateInstance(typeof(double), 2);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermParmsOilWater(ref psaRelParms, ref psaIrrSat, ref psaExp);

                string i = psaExp.GetValue(0).ToString();

                double Actual = Math.Round(System.Convert.ToDouble(i), 1);

                double Expected = 3.5;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void Layer_A6_GetRelPermParmsOilWater_3_psaExp_1()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms = Array.CreateInstance(typeof(double), 2);
                Array psaIrrSat = Array.CreateInstance(typeof(double), 2);
                Array psaExp = Array.CreateInstance(typeof(double), 2);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermParmsOilWater(ref psaRelParms, ref psaIrrSat, ref psaExp);

                string i = psaExp.GetValue(1).ToString();

                double Actual = Math.Round(System.Convert.ToDouble(i), 0);

                double Expected = 2;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void Layer_A7_GetRelPermParmsGasWater_1_psaRelPerms_0()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms1 = Array.CreateInstance(typeof(double), 2);
                Array psaIrrSat1 = Array.CreateInstance(typeof(double), 2);
                Array psaExp1 = Array.CreateInstance(typeof(double), 2);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermParmsGasWater(ref psaRelParms1, ref psaIrrSat1, ref psaExp1);

                string i = psaRelParms1.GetValue(0).ToString();

                double Actual = Math.Round(System.Convert.ToDouble(i), 0);

                double Expected = 1;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void Layer_A7_GetRelPermParmsGasWater_1_psaRelPerms_1()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms1 = Array.CreateInstance(typeof(double), 2);
                Array psaIrrSat1 = Array.CreateInstance(typeof(double), 2);
                Array psaExp1 = Array.CreateInstance(typeof(double), 2);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermParmsGasWater(ref psaRelParms1, ref psaIrrSat1, ref psaExp1);

                string i = psaRelParms1.GetValue(1).ToString();

                double Actual = Math.Round(System.Convert.ToDouble(i), 2);

                double Expected = 0.5;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A7_GetRelPermParmsGasWater_2_psaIrrSat_0()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms1 = Array.CreateInstance(typeof(double), 2);
                Array psaIrrSat1 = Array.CreateInstance(typeof(double), 2);
                Array psaExp1 = Array.CreateInstance(typeof(double), 2);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermParmsGasWater(ref psaRelParms1, ref psaIrrSat1, ref psaExp1);

                string i = psaIrrSat1.GetValue(0).ToString();

                double Actual = Math.Round(System.Convert.ToDouble(i), 2);

                double Expected = 0.3;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A7_GetRelPermParmsGasWater_2_psaIrrSat_1()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms1 = Array.CreateInstance(typeof(double), 2);
                Array psaIrrSat1 = Array.CreateInstance(typeof(double), 2);
                Array psaExp1 = Array.CreateInstance(typeof(double), 2);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermParmsGasWater(ref psaRelParms1, ref psaIrrSat1, ref psaExp1);

                string i = psaIrrSat1.GetValue(1).ToString();

                double Actual = Math.Round(System.Convert.ToDouble(i), 2);

                double Expected = 0.25;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void Layer_A7_GetRelPermParmsGasWater_3_psaExp_0()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms1 = Array.CreateInstance(typeof(double), 2);
                Array psaIrrSat1 = Array.CreateInstance(typeof(double), 2);
                Array psaExp1 = Array.CreateInstance(typeof(double), 2);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermParmsGasWater(ref psaRelParms1, ref psaIrrSat1, ref psaExp1);

                string i = psaExp1.GetValue(0).ToString();

                double Actual = Math.Round(System.Convert.ToDouble(i), 2);

                double Expected = 3.5;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void Layer_A7_GetRelPermParmsGasWater_3_psaExp_1()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms1 = Array.CreateInstance(typeof(double), 2);
                Array psaIrrSat1 = Array.CreateInstance(typeof(double), 2);
                Array psaExp1 = Array.CreateInstance(typeof(double), 2);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermParmsGasWater(ref psaRelParms1, ref psaIrrSat1, ref psaExp1);

                string i = psaExp1.GetValue(1).ToString();

                double Actual = Math.Round(System.Convert.ToDouble(i), 0);

                double Expected = 2.0;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A8_GetRelPermParmsGasOil_1_psaRelPerms_0()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms2 = Array.CreateInstance(typeof(double), 2);
                Array psaIrrSat2 = Array.CreateInstance(typeof(double), 2);
                Array psaExp2 = Array.CreateInstance(typeof(double), 2);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermParmsGasOil(ref psaRelParms2, ref psaIrrSat2, ref psaExp2);

                string i = psaRelParms2.GetValue(0).ToString();

                double Actual = Math.Round(System.Convert.ToDouble(i), 3);

                double Expected = 0.85;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A8_GetRelPermParmsGasOil_1_psaRelPerms_1()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms2 = Array.CreateInstance(typeof(double), 2);
                Array psaIrrSat2 = Array.CreateInstance(typeof(double), 2);
                Array psaExp2 = Array.CreateInstance(typeof(double), 2);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermParmsGasOil(ref psaRelParms2, ref psaIrrSat2, ref psaExp2);

                string i = psaRelParms2.GetValue(1).ToString();

                double Actual = Math.Round(System.Convert.ToDouble(i), 2);

                double Expected = 0.75;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void Layer_A8_GetRelPermParmsGasOil_2_psaIrrSat_0()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms2 = Array.CreateInstance(typeof(double), 2);
                Array psaIrrSat2 = Array.CreateInstance(typeof(double), 2);
                Array psaExp2 = Array.CreateInstance(typeof(double), 2);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermParmsGasOil(ref psaRelParms2, ref psaIrrSat2, ref psaExp2);

                string i = psaIrrSat2.GetValue(0).ToString();

                double Actual = Math.Round(System.Convert.ToDouble(i), 2);

                double Expected = 0.15;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void Layer_A8_GetRelPermParmsGasOil_2_psaIrrSat_1()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms2 = Array.CreateInstance(typeof(double), 2);
                Array psaIrrSat2 = Array.CreateInstance(typeof(double), 2);
                Array psaExp2 = Array.CreateInstance(typeof(double), 2);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermParmsGasOil(ref psaRelParms2, ref psaIrrSat2, ref psaExp2);

                string i = psaIrrSat2.GetValue(1).ToString();

                double Actual = Math.Round(System.Convert.ToDouble(i), 2);

                double Expected = 0.15;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A8_GetRelPermParmsGasOil_3_psaExp_0()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms2 = Array.CreateInstance(typeof(double), 2);
                Array psaIrrSat2 = Array.CreateInstance(typeof(double), 2);
                Array psaExp2 = Array.CreateInstance(typeof(double), 2);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermParmsGasOil(ref psaRelParms2, ref psaIrrSat2, ref psaExp2);

                string i = psaExp2.GetValue(0).ToString();

                double Actual = Math.Round(System.Convert.ToDouble(i), 2);

                double Expected = 2.4;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A8_GetRelPermParmsGasOil_3_psaExp_1()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms2 = Array.CreateInstance(typeof(double), 2);
                Array psaIrrSat2 = Array.CreateInstance(typeof(double), 2);
                Array psaExp2 = Array.CreateInstance(typeof(double), 2);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermParmsGasOil(ref psaRelParms2, ref psaIrrSat2, ref psaExp2);

                string i = psaExp2.GetValue(1).ToString();

                double Actual = Math.Round(System.Convert.ToDouble(i), 2);

                double Expected = 1.7;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_000()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);
                short errorStat = WFInt.ErrorStatus;

                string i = psaRelParms3.GetValue(0, 0, 0).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_001()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);
                short errorStat = WFInt.ErrorStatus;

                string i = psaRelParms3.GetValue(0, 0, 1).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_002()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(0, 0, 2).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_003()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(0, 0, 3).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_004()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(0, 0, 4).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_005()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(0, 0, 5).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_010()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(0, 1, 0).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_011()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(0, 1, 1).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_012()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(0, 1, 2).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_013()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(0, 1, 3).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_014()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(0, 1, 4).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_015()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(0, 1, 5).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_020()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(0, 2, 0).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_021()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(0, 2, 1).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_022()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(0, 2, 2).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_023()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(0, 2, 3).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_024()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(0, 2, 4).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_025()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(0, 2, 5).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_100()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);
                short errorStat = WFInt.ErrorStatus;

                string i = psaRelParms3.GetValue(1, 0, 0).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_101()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);
                short errorStat = WFInt.ErrorStatus;

                string i = psaRelParms3.GetValue(1, 0, 1).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_102()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(1, 0, 2).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_103()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(1, 0, 3).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_104()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(1, 0, 4).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_105()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(1, 0, 5).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_110()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(1, 1, 0).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_111()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(1, 1, 1).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_112()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(1, 1, 2).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_113()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(1, 1, 3).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_114()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(1, 1, 4).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_115()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(1, 1, 5).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_120()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(1, 2, 0).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_121()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(1, 2, 1).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_122()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(1, 2, 2).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_123()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(1, 2, 3).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_124()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(1, 2, 4).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_125()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(1, 2, 5).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_200()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);
                short errorStat = WFInt.ErrorStatus;

                string i = psaRelParms3.GetValue(2, 0, 0).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_201()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);
                short errorStat = WFInt.ErrorStatus;

                string i = psaRelParms3.GetValue(2, 0, 1).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_202()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(2, 0, 2).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_203()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(2, 0, 3).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_204()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(2, 0, 4).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_205()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(2, 0, 5).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_210()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(2, 1, 0).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_211()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(2, 1, 1).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_212()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(2, 1, 2).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_213()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(2, 1, 3).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_214()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(2, 1, 4).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_215()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(2, 1, 5).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_220()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(2, 2, 0).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_221()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(2, 2, 1).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_222()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(2, 2, 2).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_223()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(2, 2, 3).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_224()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(2, 2, 4).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_225()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(2, 2, 5).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void Layer_B1_SetTabulatedIPRData()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\no tabulated data.wflx");

                string sfileOutputSetTabulatedIPRData = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\OutputSetTabulatedIPRData.wflx");

                WFInt.OpenFile(sfile1);

                int indexArray = 21;

                Array psaPress1 = Array.CreateInstance(typeof(double), indexArray);
                Array psaFlowRate1 = Array.CreateInstance(typeof(double), indexArray);
                Array psaPhaseRate1 = Array.CreateInstance(typeof(double), indexArray);
                Array psaWaterRate1 = Array.CreateInstance(typeof(double), indexArray);

                psaPress1.SetValue(14.650, 0);
                psaPress1.SetValue(416.418, 1);
                psaPress1.SetValue(818.185, 2);
                psaPress1.SetValue(1219.953, 3);
                psaPress1.SetValue(1621.720, 4);
                psaPress1.SetValue(2023.488, 5);
                psaPress1.SetValue(2425.255, 6);
                psaPress1.SetValue(2827.022, 7);
                psaPress1.SetValue(3228.790, 8);
                psaPress1.SetValue(3630.558, 9);
                psaPress1.SetValue(4032.325, 10);
                psaPress1.SetValue(4434.093, 11);
                psaPress1.SetValue(4835.860, 12);
                psaPress1.SetValue(5237.637, 13);
                psaPress1.SetValue(5639.395, 14);
                psaPress1.SetValue(6041.163, 15);
                psaPress1.SetValue(6442.930, 16);
                psaPress1.SetValue(6844.698, 17);
                psaPress1.SetValue(7246.465, 18);
                psaPress1.SetValue(7648.233, 19);
                psaPress1.SetValue(8050.000, 20);

                psaFlowRate1.SetValue(0.1239, 0);
                psaFlowRate1.SetValue(0.1177, 1);
                psaFlowRate1.SetValue(0.1115, 2);
                psaFlowRate1.SetValue(0.1053, 3);
                psaFlowRate1.SetValue(0.0991, 4);
                psaFlowRate1.SetValue(0.0929, 5);
                psaFlowRate1.SetValue(0.0867, 6);
                psaFlowRate1.SetValue(0.0805, 7);
                psaFlowRate1.SetValue(0.0743, 8);
                psaFlowRate1.SetValue(0.0681, 9);
                psaFlowRate1.SetValue(0.0620, 10);
                psaFlowRate1.SetValue(0.0558, 11);
                psaFlowRate1.SetValue(0.0496, 12);
                psaFlowRate1.SetValue(0.0434, 13);
                psaFlowRate1.SetValue(0.0372, 14);
                psaFlowRate1.SetValue(0.0310, 15);
                psaFlowRate1.SetValue(0.0248, 16);
                psaFlowRate1.SetValue(0.0186, 17);
                psaFlowRate1.SetValue(0.0124, 18);
                psaFlowRate1.SetValue(0.0062, 19);
                psaFlowRate1.SetValue(0, 20);

                psaPhaseRate1.SetValue(6666.6665, 0);
                psaPhaseRate1.SetValue(6666.6665, 1);
                psaPhaseRate1.SetValue(6666.6665, 2);
                psaPhaseRate1.SetValue(6666.6665, 3);
                psaPhaseRate1.SetValue(6666.6665, 4);
                psaPhaseRate1.SetValue(6666.6665, 5);
                psaPhaseRate1.SetValue(6666.6665, 6);
                psaPhaseRate1.SetValue(6666.6665, 7);
                psaPhaseRate1.SetValue(6666.6665, 8);
                psaPhaseRate1.SetValue(6666.6665, 9);
                psaPhaseRate1.SetValue(6666.6665, 10);
                psaPhaseRate1.SetValue(6666.6665, 11);
                psaPhaseRate1.SetValue(6666.6665, 12);
                psaPhaseRate1.SetValue(6666.6665, 13);
                psaPhaseRate1.SetValue(6666.6665, 14);
                psaPhaseRate1.SetValue(6666.6665, 15);
                psaPhaseRate1.SetValue(6666.6665, 16);
                psaPhaseRate1.SetValue(6666.6665, 17);
                psaPhaseRate1.SetValue(6666.6665, 18);
                psaPhaseRate1.SetValue(6666.6665, 19);
                psaPhaseRate1.SetValue(6666.6665, 20);

                psaWaterRate1.SetValue(0.5000, 0);
                psaWaterRate1.SetValue(0.5000, 1);
                psaWaterRate1.SetValue(0.5000, 2);
                psaWaterRate1.SetValue(0.5000, 3);
                psaWaterRate1.SetValue(0.5000, 4);
                psaWaterRate1.SetValue(0.5000, 5);
                psaWaterRate1.SetValue(0.5000, 6);
                psaWaterRate1.SetValue(0.5000, 7);
                psaWaterRate1.SetValue(0.5000, 8);
                psaWaterRate1.SetValue(0.5000, 9);
                psaWaterRate1.SetValue(0.5000, 10);
                psaWaterRate1.SetValue(0.5000, 11);
                psaWaterRate1.SetValue(0.5000, 12);
                psaWaterRate1.SetValue(0.5000, 13);
                psaWaterRate1.SetValue(0.5000, 14);
                psaWaterRate1.SetValue(0.5000, 15);
                psaWaterRate1.SetValue(0.5000, 16);
                psaWaterRate1.SetValue(0.5000, 17);
                psaWaterRate1.SetValue(0.5000, 18);
                psaWaterRate1.SetValue(0.5000, 19);
                psaWaterRate1.SetValue(0.5000, 20);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().SetTabulatedIPRData(indexArray, ref psaPress1, ref psaFlowRate1, ref psaPhaseRate1, ref psaWaterRate1);

                WFInt.SaveFile(sfileOutputSetTabulatedIPRData);

                short Actual = WFInt.ErrorStatus;

                short Expected = 0;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual);

            }
        }

        [TestMethod]
        public void Layer_B2_SetTestPoint1Data_TestPoint1Pressure()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\test rate.wflx");

                string sfileOutput1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\SetTestPoint1Data_AfterSetting_1.wflx");

                WFInt.OpenFile(sfile);

                double dBHP = 4500;
                double dQLiq = 1050;

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().SetTestPoint1Data(dBHP, dQLiq);

                WFInt.SaveFile(sfileOutput1);

                double Actual = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().TestPoint1Pressure;

                double Expected = 4500;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void Layer_B2_SetTestPoint1Data_TestPoint1Rate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\test rate.wflx");

                string sfileOutput2 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\SetTestPoint1Data_AfterSetting_2.wflx");

                WFInt.OpenFile(sfile);

                double dBHP = 4500;
                double dQLiq = 1050;

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().SetTestPoint1Data(dBHP, dQLiq);

                WFInt.SaveFile(sfileOutput2);

                double Actual = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().TestPoint1Rate;

                double Expected = 1050;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_A9_GetRelPermTable_225_A()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\real perm table.wflx");

                WFInt.OpenFile(sfile1);

                Array psaRelParms3 = Array.CreateInstance(typeof(double), 3, 3, 10);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetRelPermTable(ref psaRelParms3);

                string i = psaRelParms3.GetValue(2, 1, 0).ToString();

                double Actual = System.Convert.ToDouble(i);

                double Expected = 0.0000085;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void Layer_Darcy_NonDarcy_Coeff()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\gas well BHP.wflx");

                WFInt.OpenFile(sfile);

                double ActualDarcyFlowCoeff = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().DarcyFlowCoeff, 4);
                double ActualNonDarcyFlowCoeff = Math.Round(WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().NonDarcyFlowCoeff, 4);

                Assert.AreEqual(6536874.000, ActualDarcyFlowCoeff, DeltaFraction.Default(6536874.000));
                Assert.AreEqual(5943880.000, ActualNonDarcyFlowCoeff, DeltaFraction.Default(5943880.000));
            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_GetSurfaceVolumeFractions()
        {
            //Need to do: Don't know where to find in UI
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("ExampleModelFiles\\Black Oil.wflx");

                WFInt.OpenFile(sfile);

                double dOilFrac = 0;
                double dWaterFrac = 0;
                double dGasFrac = 0;

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetSurfaceVolumeFractions(ref dOilFrac, ref dWaterFrac, ref dGasFrac);

            }
        }

        [TestMethod]
        [Ignore]
        public void Layer_NonDarcyModel()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("ExampleModelFiles\\Black Oil.wflx");
                string sfileOutput = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layer.wflx");

                WFInt.OpenFile(sfile);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().NonDarcyModel = true;
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().NonDarcyFlowCoeff = 0.1;
                //WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().DarcyFlowCoeff = 0.21; // Do not know where to check in UI
                WFInt.SaveFile(sfileOutput);

                WFloInterface WFInt1 = new WFloInterface();
                WFInt1.AddRef();

                WFInt1.OpenFile(sfileOutput);

                bool NonDarcyModel = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().NonDarcyModel;
                double NonDarcyFlowCoeff = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().NonDarcyFlowCoeff;

                Assert.AreEqual(true, NonDarcyModel);
                Assert.AreEqual(0.1, NonDarcyFlowCoeff, DeltaFraction.Default(0.1));
            }
        }

        [TestMethod]
        public void Layer_LayerConfig_Multiple_Fracture()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layers active.wflx");
                string sfileOutPut = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layers active.wflx");

                WFInt.OpenFile(sfile);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().LayerConfig = 3; //LAYER_MULTIPLE_FRACTURES
                WFInt.SaveFile(sfileOutPut);

                WFloInterface WFInt1 = new WFloInterface();
                WFInt1.AddRef();
                WFInt1.OpenFile(sfileOutPut);

                short afterChange = WFInt1.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().LayerConfig;

                Assert.AreEqual(3, afterChange);
            }
        }
    }

    [TestClass]
    public class WPS_3_LayerRateCalculator
    {
        [TestInitialize]
        public void TestInitialize()
        {
            System.Threading.Monitor.Enter(WellFloFileLocation.GlobalSyncObject);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            System.Threading.Monitor.Exit(WellFloFileLocation.GlobalSyncObject);
        }
        [TestMethod]
        public void LayerRateCalculator_A0_Calculate_GasRate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 37100;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                int index1 = 0;

                double Actual = Math.Round(WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().GetAt(index1).AddRef().GasRate, 2);

                double Expected = 13.91;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void LayerRateCalculator_A0_Calculate_OilRate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 37100;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                int index1 = 0;

                double Actual = Math.Round(WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().GetAt(index1).AddRef().OilRate, 0);

                double Expected = 27825.0;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void LayerRateCalculator_A0_Calculate_WaterRate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 37100;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                int index1 = 0;

                double Actual = Math.Round(WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().GetAt(index1).AddRef().WaterRate, 0);

                double Expected = 9275.0;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void LayerRateCalculator_GetLayerRatesData()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                LayerRates pVal = new LayerRates();
                pVal.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 37178.3;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                WFInt.GetLayerRateCalculator().AddRef().GetLayerRatesData(pVal);

                double WaterRate = Math.Round(pVal.GetAt(0).AddRef().WaterRate, 1);
                double OilRate = Math.Round(pVal.GetAt(0).AddRef().OilRate, 1);

                Assert.AreEqual(9294.6, WaterRate, DeltaFraction.Default(9294.6));
                Assert.AreEqual(27883.8, OilRate, DeltaFraction.Default(27883.8));
            }
        }

        [TestMethod]
        public void LayerRateCalculator_GetMultipleLayerRatesData()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                LayerRates pVal = new LayerRates();
                pVal.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer/PCP-Multiphase.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 150.0;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                WFInt.GetLayerRateCalculator().AddRef().GetLayerRatesData(pVal);

                double WaterRate = Math.Round(pVal.GetAt(0).AddRef().WaterRate, 1);
                double OilRate = Math.Round(pVal.GetAt(0).AddRef().OilRate, 1);

                Assert.AreEqual(30, WaterRate, DeltaFraction.Default(30));
                Assert.AreEqual(119.9999, OilRate, DeltaFraction.Default(119.9999));
            }
        }

        [TestMethod]
        public void LayerRateCalculator_GetFlowRateDataByIndex()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                FlowRate pVal = new FlowRate();
                pVal.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer.wflx");

                WFInt.OpenFile(sfile);

                WFInt.GetLayerRateCalculator().AddRef().Calculate(37178.3);

                WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().GetFlowRateDataByIndex(0, pVal);

                double OilRate = pVal.OilRate;
                double WaterRate = pVal.WaterRate;
                string Name = pVal.Name;

                Assert.AreEqual(9294.6, WaterRate, DeltaFraction.Default(9294.6));
                Assert.AreEqual(27883.8, OilRate, DeltaFraction.Default(27883.8));
                Assert.AreEqual("Layer 1", Name);
            }
        }

        [TestMethod]
        public void LayerRateCalculator_GetFlowRateDataByName()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                FlowRate pVal = new FlowRate();
                pVal.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 37178.3;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().GetFlowRateDataByName("Layer 1", pVal);

                double WaterRate = pVal.WaterRate;
                double OilRate = pVal.OilRate;

                Assert.AreEqual(9294.6, WaterRate, DeltaFraction.Default(9294.6));
                Assert.AreEqual(27883.8, OilRate, DeltaFraction.Default(27883.8));
            }
        }

        [TestMethod]
        public void LayerRateCalculator_A0_LayerRates()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 37100;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                int Actual = WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Count;

                int Expected = 1;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual);
            }
        }
    }

    [TestClass]
    public class WPS_4_LayerRate
    {
        [TestInitialize]
        public void TestInitialize()
        {
            System.Threading.Monitor.Enter(WellFloFileLocation.GlobalSyncObject);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            System.Threading.Monitor.Exit(WellFloFileLocation.GlobalSyncObject);
        }
        [TestMethod]
        public void LayerRate_Set2_A0_Count()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 37100;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                int Actual = WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Count;

                int Expected = 1;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual);
            }
        }

        [TestMethod]
        public void LayerRate_Set2_A1_Add()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer.wflx");
                //string sfile2 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer_Save1.wflx");(model doesnot exist!!)

                WFInt.OpenFile(sfile1);

                double dRate = 37100;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                int Count = WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Count;

                string sName = "Layer 2";

                WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Add(sName);

                int Actual = WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Count;

                int Expected = 2;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual);
            }
        }

        [TestMethod]
        public void LayerRate_Set2_A2_GetAt_Name()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 37100;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                int i = 0;

                string Actual = WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().GetAt(i).AddRef().Name;

                string Expected = "Layer 1";

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual);
            }
        }

        [TestMethod]
        public void LayerRate_Set2_A2_GetAt_GasRate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 37100;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                int i = 0;

                double Actual = Math.Round(WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().GetAt(i).AddRef().GasRate, 2);

                double Expected = 13.91;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void LayerRate_Set2_A2_GetAt_OilRate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 37100;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                int i = 0;

                double Actual = Math.Round(WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().GetAt(i).AddRef().OilRate, 0);

                double Expected = 27825.0;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void LayerRate_Set2_A2_GetAt_WaterRate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 37100;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                int i = 0;

                double Actual = Math.Round(WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().GetAt(i).AddRef().WaterRate, 0);

                double Expected = 9275.0;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void LayerRate_Set2_A3_Item_GasRate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 37100;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                string sName = "Layer 1";

                double Actual = Math.Round(WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Item(sName).AddRef().GasRate, 2);

                double Expected = 13.91;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void LayerRate_Set2_A3_Item_OilRate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 37100;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                string sName = "Layer 1";

                double Actual = Math.Round(WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Item(sName).AddRef().OilRate, 0);

                double Expected = 27825.0;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void LayerRate_Set2_A3_Item_WaterRate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 37100;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                string sName = "Layer 1";

                double Actual = Math.Round(WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Item(sName).AddRef().WaterRate, 0);

                double Expected = 9275.0;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void LayerRate_Set2_A4_Remove_1()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer.wflx");

                //string sfile2 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer_Remove_1.wflx");

                WFInt.OpenFile(sfile1);

                double dRate = 37100;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                string sName = "Layer 2";

                WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Add(sName);

                WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Remove(sName);

                int Actual = WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Count;

                int Expected = 1;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual);
            }
        }

        [TestMethod]
        public void LayerRate_Set2_A4_Remove_2()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer.wflx");

                // string sfile2 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer_Remove_2.wflx");

                WFInt.OpenFile(sfile1);

                double dRate = 37100;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                string sName = "Layer 1";

                WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Remove(sName);

                int Actual = WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Count;

                int Expected = 0;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual);
            }
        }

        [TestMethod]
        public void LayerRate_Set2_A5_RemoveAll()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer.wflx");

                string sfile2 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\get-one layer_RemoveAll.wflx");

                WFInt.OpenFile(sfile1);

                WFInt.SaveFile(sfile2);

                double dRate = 37100;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().RemoveAll();

                WFInt.SaveFile(sfile2);

                WFInt.OpenFile(sfile2);

                int Actual = WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Count;

                int Expected = 0;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual);
            }
        }

        [TestMethod]
        public void LayerRate_Set3_A1_GetAt_Name()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\one layer active and one is inactive.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 92987.929;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                int i = 0;

                string Actual = WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().GetAt(i).AddRef().Name;

                string Expected = "Layer 2";

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual);
            }
        }

        [TestMethod]
        public void LayerRate_Set3_A1_GetAt_GasRate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\one layer active and one is inactive.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 92987.929;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                int i = 0;

                double Actual = Math.Round(WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().GetAt(i).AddRef().GasRate, 2);

                double Expected = 34.87;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void LayerRate_Set3_A1_GetAt_OilRate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\one layer active and one is inactive.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 92987.929;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                int i = 0;

                double Actual = Math.Round(WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().GetAt(i).AddRef().OilRate, 2);

                double Expected = 69740.94;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void LayerRate_Set3_A1_GetAt_WaterRate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\one layer active and one is inactive.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 92987.929;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                int i = 0;

                double Actual = Math.Round(WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().GetAt(i).AddRef().WaterRate, 2);

                double Expected = 23246.98;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void LayerRate_Set3_A2_Item_GasRate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\one layer active and one is inactive.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 92987.929;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                string sName = "Layer 2";

                double Actual = Math.Round(WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Item(sName).AddRef().GasRate, 2);

                double Expected = 34.87;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void LayerRate_Set3_A2_Item_OilRate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\one layer active and one is inactive.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 92987.929;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                string sName = "Layer 2";

                double Actual = Math.Round(WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Item(sName).AddRef().OilRate, 2);

                double Expected = 69740.94;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void LayerRate_Set3_A2_Item_WaterRate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\one layer active and one is inactive.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 92987.929;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                string sName = "Layer 2";

                double Actual = Math.Round(WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Item(sName).AddRef().WaterRate, 2);

                double Expected = 23246.98;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void LayerRate_Set3_A3_Count()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\one layer active and one is inactive.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 92987.929;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                int Actual = WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Count;

                int Expected = 1;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual);
            }
        }

        [TestMethod]
        public void LayerRate_Set4_A1_GetAt_Name()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer rate - set 4.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 21780.34;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                int i = 0;

                string Actual = WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().GetAt(i).AddRef().Name;

                string Expected = "Layer 1";

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual);
            }
        }

        [TestMethod]
        public void LayerRate_Set4_A1_GetAt_GasRate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer rate - set 4.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 21780.34;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                int i = 0;

                double Actual = Math.Round(WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().GetAt(i).AddRef().GasRate, 2);

                double Expected = 8.17;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void LayerRate_Set4_A1_GetAt_OilRate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer rate - set 4.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 21780.34;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                int i = 0;

                double Actual = Math.Round(WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().GetAt(i).AddRef().OilRate, 2);

                double Expected = 16335.25;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void LayerRate_Set4_A1_GetAt_WaterRate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer rate - set 4.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 21780.34;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                int i = 0;

                double Actual = Math.Round(WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().GetAt(i).AddRef().WaterRate, 2);

                double Expected = 5445.08;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void LayerRate_Set4_A2_Item_GasRate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer rate - set 4.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 21780.34;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                string sName = "Layer 1";

                double Actual = Math.Round(WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Item(sName).AddRef().GasRate, 2);

                double Expected = 8.17;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void LayerRate_Set4_A2_Item_OilRate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer rate - set 4.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 21780.34;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                string sName = "Layer 1";

                double Actual = Math.Round(WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Item(sName).AddRef().OilRate, 2);

                double Expected = 16335.25;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void LayerRate_Set4_A2_Item_WaterRate()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer rate - set 4.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 21780.34;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                string sName = "Layer 1";

                double Actual = Math.Round(WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Item(sName).AddRef().WaterRate, 2);

                double Expected = 5445.08;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual, DeltaFraction.Default(Expected));
            }
        }

        [TestMethod]
        public void LayerRate_Set4_A3_Count()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();


                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layer rate - set 4.wflx");

                WFInt.OpenFile(sfile);

                double dRate = 92987.929;

                WFInt.GetLayerRateCalculator().AddRef().Calculate(dRate);

                int Actual = WFInt.GetLayerRateCalculator().AddRef().LayerRates.AddRef().Count;

                int Expected = 1;

                WFInt.EndWellFlo();
                Assert.AreEqual(Expected, Actual);
            }
        }

        [TestMethod]
        public void WPS_Crashes_for_Longer_Layer_Name_DT38611()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\LongLayerName.wflx");

                WFInt.OpenFile(sfile);

                // without the fix WPS will crash while executing the below line
                string ActualName = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().Name;
            }
        }
    }

    /*[TestClass]
     public class WPS_5_MultiFractureData
     {
         [TestInitialize]
         public void TestInitialize()
         {
             System.Threading.Monitor.Enter(WellFloFileLocation.GlobalSyncObject);
         }

         [TestCleanup]
         public void TestCleanup()
         {
             System.Threading.Monitor.Exit(WellFloFileLocation.GlobalSyncObject);
         }
         [TestMethod]
         public void MultiFractureData_AddFracture_ToFirstLayer()
         {
             using (new LifeTimeScope())
             {
                 WFloInterface WFInt = new WFloInterface();
                 WFInt.AddRef();

                 string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layers active.wflx");
                 string sSavedfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layers active.wflx");

                 WFInt.OpenFile(sfile);

                 int initialCount = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().NumberOfFractures;

                 Assert.AreEqual(initialCount, 0);

                 FractureData firstFracture = new FractureData();
                 firstFracture.AddRef();

                 firstFracture.bUseFracFaceSkin = true;
                 firstFracture.bUseCalcdFracDmgSkin = true;
                 firstFracture.bUseFracChokedSkin = true;
                 firstFracture.bUseCalcdFracChokedSkin  = true;
                 firstFracture.bUseCalcdSkin  = true;
                 firstFracture.FracSkin = 0.5;
                 firstFracture.FracHalfSpacing = 200; 
                 firstFracture.FracWidth = 5;
                 firstFracture.FracHalfLength = 250;
                 firstFracture.FracHeight = 2;
                 firstFracture.FracPermNearWB = 0.2;
                 firstFracture.FracWidthNearWB = 0.25;
                 firstFracture.FracPerm = 0.5;
                 firstFracture.FracDmgPerm = 0.74;
                 firstFracture.FracDmgThick = 52;
                 firstFracture.FracDmgSkinCalcd = 0.6;
                 firstFracture.FracDmgSkinMeas = 75;
                 firstFracture.FracChokedHalfLength = 300;
                 firstFracture.FracChokedSkinCalcd = 0.65;
                 firstFracture.FracChokedSkinMeas = 95;
                 firstFracture.DarcySkinCalcd = 0.7;
                 firstFracture.DarcySkinManual = 0.15;
                
                 WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().AddFractureData(firstFracture);
                 WFInt.SaveFile(sSavedfile);

                 WFloInterface WFIntSaved = new WFloInterface();
                 WFIntSaved.AddRef();

                 WFIntSaved.OpenFile(sSavedfile);


                 int afterAddingCount = WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().NumberOfFractures;

                 Assert.AreEqual(afterAddingCount, 1);

                 FractureData savedFracture = new FractureData();
                 savedFracture.AddRef();

                 WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().GetFractureData(0, savedFracture);

                 Assert.AreEqual(true, savedFracture.bUseFracFaceSkin);
                 Assert.AreEqual(true, savedFracture.bUseCalcdFracDmgSkin);
                 Assert.AreEqual(true, savedFracture.bUseFracChokedSkin);
                 Assert.AreEqual(true, savedFracture.bUseCalcdFracChokedSkin);
                 Assert.AreEqual(true, savedFracture.bUseCalcdSkin);
                 Assert.AreEqual(0.5, savedFracture.FracSkin, DeltaFraction.Default(0.5));
                 Assert.AreEqual(200, savedFracture.FracHalfSpacing, DeltaFraction.Default(200));
                 Assert.AreEqual(5, savedFracture.FracWidth, DeltaFraction.Default(5));
                 Assert.AreEqual(250, savedFracture.FracHalfLength, DeltaFraction.Default(250));
                 Assert.AreEqual(2, savedFracture.FracHeight, DeltaFraction.Default(2));
                 Assert.AreEqual(0.2, savedFracture.FracPermNearWB, DeltaFraction.Default(0.2));
                 Assert.AreEqual(0.25, savedFracture.FracWidthNearWB, DeltaFraction.Default(0.25));
                 Assert.AreEqual(0.5, savedFracture.FracPerm, DeltaFraction.Default(0.5));
                 Assert.AreEqual(0.74, savedFracture.FracDmgPerm, DeltaFraction.Default(0.74));
                 Assert.AreEqual(52, savedFracture.FracDmgThick, DeltaFraction.Default(52));
                 Assert.AreEqual(0.6, savedFracture.FracDmgSkinCalcd, DeltaFraction.Default(0.6));
                 Assert.AreEqual(75, savedFracture.FracDmgSkinMeas, DeltaFraction.Default(75));
                 Assert.AreEqual(300, savedFracture.FracChokedHalfLength, DeltaFraction.Default(300));
                 Assert.AreEqual(0.65, savedFracture.FracChokedSkinCalcd, DeltaFraction.Default(0.65));
                 Assert.AreEqual(95, savedFracture.FracChokedSkinMeas, DeltaFraction.Default(95));
                 Assert.AreEqual(0.7, savedFracture.DarcySkinCalcd, DeltaFraction.Default(0.7));
                 Assert.AreEqual(0.15, savedFracture.DarcySkinManual, DeltaFraction.Default(0.15));
             }
         }

         [TestMethod]
         public void MultiFractureData_AddFracture_ToSecondLayer()
         {
             using (new LifeTimeScope())
             {
                 WFloInterface WFInt = new WFloInterface();
                 WFInt.AddRef();

                 string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layers active.wflx");
                 string sSavedfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layers active.wflx");

                 WFInt.OpenFile(sfile);

                 int initialCountFisrtLayer = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().NumberOfFractures;
                 int initialCountSecondLayer = WFInt.WellModel.AddRef().Layers.AddRef().Item(2).AddRef().GetMultiFractureData().AddRef().NumberOfFractures;

                 Assert.AreEqual(initialCountFisrtLayer, 0);
                 Assert.AreEqual(initialCountSecondLayer, 0);

                 FractureData firstFracture = new FractureData();
                 firstFracture.AddRef();

                 firstFracture.bUseFracFaceSkin = true;
                 firstFracture.bUseCalcdFracDmgSkin = true;
                 firstFracture.bUseFracChokedSkin = true;
                 firstFracture.bUseCalcdFracChokedSkin = true;
                 firstFracture.bUseCalcdSkin = true;
                 firstFracture.FracSkin = 0.5;
                 firstFracture.FracHalfSpacing = 200;
                 firstFracture.FracWidth = 5;
                 firstFracture.FracHalfLength = 250;
                 firstFracture.FracHeight = 2;
                 firstFracture.FracPermNearWB = 0.2;
                 firstFracture.FracWidthNearWB = 0.25;
                 firstFracture.FracPerm = 0.5;
                 firstFracture.FracDmgPerm = 0.74;
                 firstFracture.FracDmgThick = 52;
                 firstFracture.FracDmgSkinCalcd = 0.6;
                 firstFracture.FracDmgSkinMeas = 75;
                 firstFracture.FracChokedHalfLength = 300;
                 firstFracture.FracChokedSkinCalcd = 0.65;
                 firstFracture.FracChokedSkinMeas = 95;
                 firstFracture.DarcySkinCalcd = 0.7;
                 firstFracture.DarcySkinManual = 0.15;

                 WFInt.WellModel.AddRef().Layers.AddRef().Item(2).AddRef().GetMultiFractureData().AddRef().AddFractureData(firstFracture);
                 WFInt.SaveFile(sSavedfile);

                 WFloInterface WFIntSaved = new WFloInterface();
                 WFIntSaved.AddRef();

                 WFIntSaved.OpenFile(sSavedfile);

                 int afterAddingCount1 = WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().NumberOfFractures;
                 int afterAddingCount2 = WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(2).AddRef().GetMultiFractureData().AddRef().NumberOfFractures;
                 Assert.AreEqual(afterAddingCount1, 0);
                 Assert.AreEqual(afterAddingCount2, 1);

                 FractureData savedFracture = new FractureData();
                 savedFracture.AddRef();

                 WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(2).AddRef().GetMultiFractureData().AddRef().GetFractureData(0, savedFracture);

                 Assert.AreEqual(true, savedFracture.bUseFracFaceSkin);
                 Assert.AreEqual(true, savedFracture.bUseCalcdFracDmgSkin);
                 Assert.AreEqual(true, savedFracture.bUseFracChokedSkin);
                 Assert.AreEqual(true, savedFracture.bUseCalcdFracChokedSkin);
                 Assert.AreEqual(true, savedFracture.bUseCalcdSkin);
                 Assert.AreEqual(0.5, savedFracture.FracSkin, DeltaFraction.Default(0.5));
                 Assert.AreEqual(200, savedFracture.FracHalfSpacing, DeltaFraction.Default(200));
                 Assert.AreEqual(5, savedFracture.FracWidth, DeltaFraction.Default(5));
                 Assert.AreEqual(250, savedFracture.FracHalfLength, DeltaFraction.Default(250));
                 Assert.AreEqual(2, savedFracture.FracHeight, DeltaFraction.Default(2));
                 Assert.AreEqual(0.2, savedFracture.FracPermNearWB, DeltaFraction.Default(0.2));
                 Assert.AreEqual(0.25, savedFracture.FracWidthNearWB, DeltaFraction.Default(0.25));
                 Assert.AreEqual(0.5, savedFracture.FracPerm, DeltaFraction.Default(0.5));
                 Assert.AreEqual(0.74, savedFracture.FracDmgPerm, DeltaFraction.Default(0.74));
                 Assert.AreEqual(52, savedFracture.FracDmgThick, DeltaFraction.Default(52));
                 Assert.AreEqual(0.6, savedFracture.FracDmgSkinCalcd, DeltaFraction.Default(0.6));
                 Assert.AreEqual(75, savedFracture.FracDmgSkinMeas, DeltaFraction.Default(75));
                 Assert.AreEqual(300, savedFracture.FracChokedHalfLength, DeltaFraction.Default(300));
                 Assert.AreEqual(0.65, savedFracture.FracChokedSkinCalcd, DeltaFraction.Default(0.65));
                 Assert.AreEqual(95, savedFracture.FracChokedSkinMeas, DeltaFraction.Default(95));
                 Assert.AreEqual(0.7, savedFracture.DarcySkinCalcd, DeltaFraction.Default(0.7));
                 Assert.AreEqual(0.15, savedFracture.DarcySkinManual, DeltaFraction.Default(0.15));
             }
         }

         [TestMethod]
         public void MultiFractureData_Add_TwoFracture()
         {
             using (new LifeTimeScope())
             {
                 WFloInterface WFInt = new WFloInterface();
                 WFInt.AddRef();

                 string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layers active.wflx");
                 string sSavedfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layers active.wflx");

                 WFInt.OpenFile(sfile);

                 int initialCount = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().NumberOfFractures;

                 Assert.AreEqual(initialCount, 0);

                 FractureData firstFracture = new FractureData();
                 firstFracture.AddRef();

                 firstFracture.bUseFracFaceSkin = true;
                 firstFracture.bUseCalcdFracDmgSkin = true;
                 firstFracture.bUseFracChokedSkin = true;
                 firstFracture.bUseCalcdFracChokedSkin = true;
                 firstFracture.bUseCalcdSkin = true;
                 firstFracture.FracSkin = 0.5;
                 firstFracture.FracHalfSpacing = 200;
                 firstFracture.FracWidth = 5;
                 firstFracture.FracHalfLength = 250;
                 firstFracture.FracHeight = 2;
                 firstFracture.FracPermNearWB = 0.2;
                 firstFracture.FracWidthNearWB = 0.25;
                 firstFracture.FracPerm = 0.5;
                 firstFracture.FracDmgPerm = 0.74;
                 firstFracture.FracDmgThick = 52;
                 firstFracture.FracDmgSkinCalcd = 0.6;
                 firstFracture.FracDmgSkinMeas = 75;
                 firstFracture.FracChokedHalfLength = 300;
                 firstFracture.FracChokedSkinCalcd = 0.65;
                 firstFracture.FracChokedSkinMeas = 95;
                 firstFracture.DarcySkinCalcd = 0.7;
                 firstFracture.DarcySkinManual = 0.15;

                 WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().AddFractureData(firstFracture);

                 firstFracture.bUseFracFaceSkin = false;
                 firstFracture.FracSkin = 0.8;
                 firstFracture.FracDmgThick = 57;
                 WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().AddFractureData(firstFracture);

                 WFInt.SaveFile(sSavedfile);

                 WFloInterface WFIntSaved = new WFloInterface();
                 WFIntSaved.AddRef();

                 WFIntSaved.OpenFile(sSavedfile);

                 int fractureCount = WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().NumberOfFractures;

                 Assert.AreEqual(2, fractureCount);

                 #region Get First Fracture and compare results
                 FractureData savedFracture1 = new FractureData();
                 savedFracture1.AddRef();

                 WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().GetFractureData(0, savedFracture1);

                 Assert.AreEqual(true, savedFracture1.bUseFracFaceSkin);
                 Assert.AreEqual(0.5, savedFracture1.FracSkin, DeltaFraction.Default(0.5));
                 Assert.AreEqual(52, savedFracture1.FracDmgThick, DeltaFraction.Default(52)); 
                 #endregion

                 #region Get Second Fracture and compare results
                 FractureData savedFracture2 = new FractureData();
                 savedFracture2.AddRef();

                 WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().GetFractureData(1, savedFracture2);

                 Assert.AreEqual(false, savedFracture2.bUseFracFaceSkin);
                 Assert.AreEqual(0.8, savedFracture2.FracSkin, DeltaFraction.Default(0.8));
                 Assert.AreEqual(57, savedFracture2.FracDmgThick, DeltaFraction.Default(57)); 
                 #endregion
             }
         }

         [TestMethod]
         public void MultiFractureData_SetFracture()
         {
             using (new LifeTimeScope())
             {
                 WFloInterface WFInt = new WFloInterface();
                 WFInt.AddRef();

                 string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layers active.wflx");
                 string sSavedfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layers active.wflx");

                 WFInt.OpenFile(sfile);

                 int initialCount = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().NumberOfFractures;

                 Assert.AreEqual(initialCount, 0);

                 FractureData firstFracture = new FractureData();
                 firstFracture.AddRef();

                 firstFracture.bUseFracFaceSkin = true;
                 firstFracture.bUseCalcdFracDmgSkin = true;
                 firstFracture.bUseFracChokedSkin = true;
                 firstFracture.bUseCalcdFracChokedSkin = true;
                 firstFracture.bUseCalcdSkin = true;
                 firstFracture.FracSkin = 0.5;
                 firstFracture.FracHalfSpacing = 200;
                 firstFracture.FracWidth = 5;
                 firstFracture.FracHalfLength = 250;
                 firstFracture.FracHeight = 2;
                 firstFracture.FracPermNearWB = 0.2;
                 firstFracture.FracWidthNearWB = 0.25;
                 firstFracture.FracPerm = 0.5;
                 firstFracture.FracDmgPerm = 0.74;
                 firstFracture.FracDmgThick = 52;
                 firstFracture.FracDmgSkinCalcd = 0.6;
                 firstFracture.FracDmgSkinMeas = 75;
                 firstFracture.FracChokedHalfLength = 300;
                 firstFracture.FracChokedSkinCalcd = 0.65;
                 firstFracture.FracChokedSkinMeas = 95;
                 firstFracture.DarcySkinCalcd = 0.7;
                 firstFracture.DarcySkinManual = 0.15;

                 WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().AddFractureData(firstFracture);

                 firstFracture.bUseFracFaceSkin = false;
                 firstFracture.FracSkin = 0.8;
                 firstFracture.FracDmgThick = 57;
                 WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().SetFractureData(0,firstFracture);

                 WFInt.SaveFile(sSavedfile);

                 WFloInterface WFIntSaved = new WFloInterface();
                 WFIntSaved.AddRef();

                 WFIntSaved.OpenFile(sSavedfile);

                 int fractureCount = WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().NumberOfFractures;

                 Assert.AreEqual(1, fractureCount);

                 #region Get First Fracture and compare results
                 FractureData savedFracture1 = new FractureData();
                 savedFracture1.AddRef();

                 WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().GetFractureData(0, savedFracture1);

                 Assert.AreEqual(false, savedFracture1.bUseFracFaceSkin);
                 Assert.AreEqual(0.8, savedFracture1.FracSkin, DeltaFraction.Default(0.8));
                 Assert.AreEqual(57, savedFracture1.FracDmgThick, DeltaFraction.Default(57));
                 #endregion
             }
         }

         [TestMethod]
         public void MultiFractureData_RemoveAllFracture()
         {
             using (new LifeTimeScope())
             {
                 WFloInterface WFInt = new WFloInterface();
                 WFInt.AddRef();

                 string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layers active.wflx");
                 string sSavedfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layers active.wflx");

                 WFInt.OpenFile(sfile);

                 int initialCount = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().NumberOfFractures;

                 Assert.AreEqual(initialCount, 0);

                 FractureData firstFracture = new FractureData();
                 firstFracture.AddRef();

                 firstFracture.bUseFracFaceSkin = true;
                 firstFracture.FracSkin = 0.5;
                 firstFracture.FracDmgThick = 52;

                 WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().AddFractureData(firstFracture);

                 firstFracture.bUseFracFaceSkin = false;
                 firstFracture.FracSkin = 0.8;
                 firstFracture.FracDmgThick = 57;
                 WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().AddFractureData(firstFracture);

                 int afterAdd = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().NumberOfFractures;
                 Assert.AreEqual(2, afterAdd);
                 WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().RemoveAllFractureData();
                 int afterRemoveAll = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().NumberOfFractures;
                 Assert.AreEqual(0, afterRemoveAll);
             }
         }

         [TestMethod]
         public void MultiFractureData_Properties()
         {
             using (new LifeTimeScope())
             {
                 WFloInterface WFInt = new WFloInterface();
                 WFInt.AddRef();

                 string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layers active.wflx");
                 string sSavedfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layers active.wflx");

                 WFInt.OpenFile(sfile);

                 WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().HorizontalSectionLenght = 4500;
                 WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().OffsetFirstFracture = 100;
                 WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().EquivalentOuterRadius = 0.25;

                 WFInt.SaveFile(sSavedfile);

                 WFloInterface WFIntSaved = new WFloInterface();
                 WFIntSaved.AddRef();

                 WFIntSaved.OpenFile(sSavedfile);

                 double HorizontalSectionLenght = WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().HorizontalSectionLenght;
                 double OffsetFirstFracture = WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().OffsetFirstFracture;
                 double EquivalentOuterRadius = WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().EquivalentOuterRadius;

                 Assert.AreEqual(4500, HorizontalSectionLenght, DeltaFraction.Default(4500));
                 Assert.AreEqual(100, OffsetFirstFracture, DeltaFraction.Default(100));
                 Assert.AreEqual(0.25, EquivalentOuterRadius, DeltaFraction.Default(0.25));
             }
         }

         [TestMethod]
         public void MultiFractureData_ChokedModel()
         {
             using (new LifeTimeScope())
             {
                 WFloInterface WFInt = new WFloInterface();
                 WFInt.AddRef();

                 string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\layers active.wflx");
                 string sSavedfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Saved layers active.wflx");

                 WFInt.OpenFile(sfile);

                 WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().ChokedModel = 2;

                 WFInt.SaveFile(sSavedfile);

                 WFloInterface WFIntSaved = new WFloInterface();
                 WFIntSaved.AddRef();

                 WFIntSaved.OpenFile(sSavedfile);

                 short ChokedModel = WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiFractureData().AddRef().ChokedModel;

                 Assert.AreEqual(2, ChokedModel);
             }
         }

         [TestMethod]
         public void MultiFractureData_CompletionModel()
         {
             using (new LifeTimeScope())
             {
                 WFloInterface WFInt = new WFloInterface();
                 WFInt.AddRef();

                 string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer/layers active.wflx");
                 string sSavedfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer/Saved layers active.wflx");

                 WFInt.OpenFile(sfile);

                 WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().CompletionModel = 7;

                 WFInt.SaveFile(sSavedfile);

                 WFloInterface WFIntSaved = new WFloInterface();
                 WFIntSaved.AddRef();

                 WFIntSaved.OpenFile(sSavedfile);

                 short ChokedModel = WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().CompletionModel;

                 Assert.AreEqual(7, ChokedModel);
             }
         }
     } */

    [TestClass]
    public class WPS_6_MultiStageData
    {
        [TestInitialize]
        public void TestInitialize()
        {
            System.Threading.Monitor.Enter(WellFloFileLocation.GlobalSyncObject);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            System.Threading.Monitor.Exit(WellFloFileLocation.GlobalSyncObject);
        }
        [TestMethod]
        public void MultiStageData_AddOneStage()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer/layers active.wflx");
                string sSavedfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer/Saved layers active.wflx");

                WFInt.OpenFile(sfile);

                int initialCount = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().NumberOfFractureStages;


                Assert.AreEqual(initialCount, 0);

                MultiFractureData firstStage = new MultiFractureData();

                firstStage.StageLength = 300;

                firstStage.TopMD = 10000;

                FractureData firstFracture = new FractureData();
                firstFracture.AddRef();

                firstFracture.bUseFracFaceSkin = true;
                firstFracture.bUseCalcdFracDmgSkin = true;
                firstFracture.bUseFracChokedSkin = true;
                firstFracture.bUseCalcdFracChokedSkin = true;
                firstFracture.bUseCalcdSkin = true;
                firstFracture.FracSkin = 0.5;
                firstFracture.FracHalfSpacing = 200;
                firstFracture.FracWidth = 5;
                firstFracture.FracHalfLength = 250;
                firstFracture.FracHeight = 2;
                firstFracture.FracPermNearWB = 0.2;
                firstFracture.FracWidthNearWB = 0.25;
                firstFracture.FracPerm = 0.5;
                firstFracture.FracDmgPerm = 0.74;
                firstFracture.FracDmgThick = 52;
                firstFracture.FracDmgSkinCalcd = 0.6;
                firstFracture.FracDmgSkinMeas = 75;
                firstFracture.FracChokedHalfLength = 300;
                firstFracture.FracChokedSkinCalcd = 0.65;
                firstFracture.FracChokedSkinMeas = 95;
                firstFracture.DarcySkinCalcd = 0.7;
                firstFracture.DarcySkinManual = 0.15;

                firstStage.AddRef().AddFractureData(firstFracture);

                FractureData secondFracture = new FractureData();
                secondFracture.AddRef();

                secondFracture.bUseFracFaceSkin = true;
                secondFracture.bUseCalcdFracDmgSkin = true;
                secondFracture.bUseFracChokedSkin = true;
                secondFracture.bUseCalcdFracChokedSkin = true;
                secondFracture.bUseCalcdSkin = true;
                secondFracture.FracSkin = 0.345;
                secondFracture.FracHalfSpacing = 234;
                secondFracture.FracWidth = 5;
                secondFracture.FracHalfLength = 250;
                secondFracture.FracHeight = 2;
                secondFracture.FracPermNearWB = 0.2;
                secondFracture.FracWidthNearWB = 0.25;
                secondFracture.FracPerm = 0.5;
                secondFracture.FracDmgPerm = 0.74;
                secondFracture.FracDmgThick = 52;
                secondFracture.FracDmgSkinCalcd = 0.6;
                secondFracture.FracDmgSkinMeas = 75;
                secondFracture.FracChokedHalfLength = 300;
                secondFracture.FracChokedSkinCalcd = 0.65;
                secondFracture.FracChokedSkinMeas = 95;
                secondFracture.DarcySkinCalcd = 0.7;
                secondFracture.DarcySkinManual = 0.15;

                firstStage.AddRef().AddFractureData(secondFracture);


                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().AddMultiFractureData(firstStage);

                WFInt.SaveFile(sSavedfile);

                WFloInterface WFIntSaved = new WFloInterface();
                WFIntSaved.AddRef();

                WFIntSaved.OpenFile(sSavedfile);


                int afterAddingCount = WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().NumberOfFractureStages;

                //  int afterAddingCount = WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().;

                Assert.AreEqual(afterAddingCount, 1);

                #region read the number of fractures from the WellTech
                MultiFractureData savedMultiFracture = new MultiFractureData();
                savedMultiFracture.AddRef();

                FractureData savedFracture = new FractureData();
                savedFracture.AddRef();

                WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().GetMultiFractureData(0, savedMultiFracture, 0, savedFracture);


                double Stagelength_1 = savedMultiFracture.StageLength;
                double TopMD_1 = savedMultiFracture.TopMD;

                int CountFractures = savedMultiFracture.NumberOfFractures;

                Assert.AreEqual(CountFractures, 2);

                Assert.AreEqual(300, savedMultiFracture.StageLength, DeltaFraction.Default(300));
                Assert.AreEqual(10000, savedMultiFracture.TopMD, DeltaFraction.Default(10000));

                #endregion
            }
        }

        [TestMethod]
        public void MultiStageData_GetOneStage()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer/layers active.wflx");
                string sSavedfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer/Saved layers active.wflx");

                WFInt.OpenFile(sfile);

                int initialCount = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().NumberOfFractureStages;


                Assert.AreEqual(initialCount, 0);

                MultiFractureData firstStage = new MultiFractureData();

                firstStage.StageLength = 300;
                firstStage.TopMD = 10000;

                FractureData firstFracture = new FractureData();
                firstFracture.AddRef();

                firstFracture.bUseFracFaceSkin = true;
                firstFracture.bUseCalcdFracDmgSkin = true;
                firstFracture.bUseFracChokedSkin = true;
                firstFracture.bUseCalcdFracChokedSkin = true;
                firstFracture.bUseCalcdSkin = true;
                firstFracture.FracSkin = 0.5;
                firstFracture.FracHalfSpacing = 200;
                firstFracture.FracWidth = 5;
                firstFracture.FracHalfLength = 250;
                firstFracture.FracHeight = 2;
                firstFracture.FracPermNearWB = 0.2;
                firstFracture.FracWidthNearWB = 0.25;
                firstFracture.FracPerm = 0.5;
                firstFracture.FracDmgPerm = 0.74;
                firstFracture.FracDmgThick = 52;
                firstFracture.FracDmgSkinCalcd = 0.6;
                firstFracture.FracDmgSkinMeas = 75;
                firstFracture.FracChokedHalfLength = 300;
                firstFracture.FracChokedSkinCalcd = 0.65;
                firstFracture.FracChokedSkinMeas = 95;
                firstFracture.DarcySkinCalcd = 0.7;
                firstFracture.DarcySkinManual = 0.15;

                firstStage.AddRef().AddFractureData(firstFracture);

                FractureData secondFracture = new FractureData();
                secondFracture.AddRef();

                secondFracture.bUseFracFaceSkin = true;
                secondFracture.bUseCalcdFracDmgSkin = true;
                secondFracture.bUseFracChokedSkin = true;
                secondFracture.bUseCalcdFracChokedSkin = true;
                secondFracture.bUseCalcdSkin = true;
                secondFracture.FracSkin = 0.345;
                secondFracture.FracHalfSpacing = 234;
                secondFracture.FracWidth = 0.5;
                secondFracture.FracHalfLength = 350;
                secondFracture.FracHeight = 2;
                secondFracture.FracPermNearWB = 0.2;
                secondFracture.FracWidthNearWB = 0.25;
                secondFracture.FracPerm = 0.5;
                secondFracture.FracDmgPerm = 0.74;
                secondFracture.FracDmgThick = 52;
                secondFracture.FracDmgSkinCalcd = 0.6;
                secondFracture.FracDmgSkinMeas = 75;
                secondFracture.FracChokedHalfLength = 300;
                secondFracture.FracChokedSkinCalcd = 0.65;
                secondFracture.FracChokedSkinMeas = 95;
                secondFracture.DarcySkinCalcd = 0.7;
                secondFracture.DarcySkinManual = 0.15;

                firstStage.AddRef().AddFractureData(secondFracture);


                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().AddMultiFractureData(firstStage);

                WFInt.SaveFile(sSavedfile);

                WFloInterface WFIntSaved = new WFloInterface();
                WFIntSaved.AddRef();

                WFIntSaved.OpenFile(sSavedfile);


                int afterAddingCount = WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().NumberOfFractureStages;

                //  int afterAddingCount = WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().;

                Assert.AreEqual(afterAddingCount, 1);

                #region read the number of fractures from the WellTech
                MultiFractureData savedMultiFracture = new MultiFractureData();
                savedMultiFracture.AddRef();

                FractureData savedFirstFracture = new FractureData();
                savedFirstFracture.AddRef();


                WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().GetMultiFractureData(0, savedMultiFracture, 0, savedFirstFracture);


                double Stagelength_1 = savedMultiFracture.StageLength;
                double TopMD_1 = savedMultiFracture.TopMD;

                int CountFractures = savedMultiFracture.NumberOfFractures;

                Assert.AreEqual(CountFractures, 2);

                Assert.AreEqual(300, savedMultiFracture.StageLength, DeltaFraction.Default(300));
                Assert.AreEqual(10000, savedMultiFracture.TopMD, DeltaFraction.Default(0.5));
                #endregion

                # region get firstfracture properties

                Assert.AreEqual(0.5, savedFirstFracture.FracSkin, DeltaFraction.Default(0.5));
                Assert.AreEqual(200, savedFirstFracture.FracHalfSpacing, DeltaFraction.Default(200));
                Assert.AreEqual(5, savedFirstFracture.FracWidth, DeltaFraction.Default(5));
                Assert.AreEqual(250, savedFirstFracture.FracHalfLength, DeltaFraction.Default(250));
                Assert.AreEqual(2, savedFirstFracture.FracHeight, DeltaFraction.Default(2));
                Assert.AreEqual(0.2, savedFirstFracture.FracPermNearWB, DeltaFraction.Default(0.2));
                Assert.AreEqual(0.25, savedFirstFracture.FracWidthNearWB, DeltaFraction.Default(0.25));
                Assert.AreEqual(0.5, savedFirstFracture.FracPerm, DeltaFraction.Default(0.5));
                Assert.AreEqual(0.74, savedFirstFracture.FracDmgPerm, DeltaFraction.Default(0.74));
                Assert.AreEqual(52, savedFirstFracture.FracDmgThick, DeltaFraction.Default(52));
                Assert.AreEqual(0.6, savedFirstFracture.FracDmgSkinCalcd, DeltaFraction.Default(0.6));
                Assert.AreEqual(75, savedFirstFracture.FracDmgSkinMeas, DeltaFraction.Default(75));
                Assert.AreEqual(300, savedFirstFracture.FracChokedHalfLength, DeltaFraction.Default(300));

                Assert.AreEqual(0.65, savedFirstFracture.FracChokedSkinCalcd, DeltaFraction.Default(0.65));
                Assert.AreEqual(95, savedFirstFracture.FracChokedSkinMeas, DeltaFraction.Default(95));

                Assert.AreEqual(0.7, savedFirstFracture.DarcySkinCalcd, DeltaFraction.Default(0.7));
                Assert.AreEqual(0.15, savedFirstFracture.DarcySkinManual, DeltaFraction.Default(0.15));

                #endregion

                #region get secondfracture properties
                WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().GetMultiFractureData(0, savedMultiFracture, 1, savedFirstFracture);

                // WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().GetMultiFractureData(0, savedMultiFracture);

                // Fractures are added to the stage before writing to the WellTech. Both stage and fracture properties will be gotten from the WellTech when calling the GetMultiFractureData,
                // then fracture properties will be read based on the Stage class. Thus, no GetFractureData method is used here.


                Assert.AreEqual(0.345, savedFirstFracture.FracSkin, DeltaFraction.Default(0.345));
                Assert.AreEqual(234, savedFirstFracture.FracHalfSpacing, DeltaFraction.Default(234));
                Assert.AreEqual(0.5, savedFirstFracture.FracWidth, DeltaFraction.Default(0.5));
                Assert.AreEqual(350, savedFirstFracture.FracHalfLength, DeltaFraction.Default(350));
                Assert.AreEqual(2, savedFirstFracture.FracHeight, DeltaFraction.Default(2));
                Assert.AreEqual(0.2, savedFirstFracture.FracPermNearWB, DeltaFraction.Default(0.2));
                Assert.AreEqual(0.25, savedFirstFracture.FracWidthNearWB, DeltaFraction.Default(0.25));
                Assert.AreEqual(0.5, savedFirstFracture.FracPerm, DeltaFraction.Default(0.5));
                Assert.AreEqual(0.74, savedFirstFracture.FracDmgPerm, DeltaFraction.Default(0.74));
                Assert.AreEqual(52, savedFirstFracture.FracDmgThick, DeltaFraction.Default(52));
                Assert.AreEqual(0.6, savedFirstFracture.FracDmgSkinCalcd, DeltaFraction.Default(0.6));
                Assert.AreEqual(75, savedFirstFracture.FracDmgSkinMeas, DeltaFraction.Default(75));
                Assert.AreEqual(300, savedFirstFracture.FracChokedHalfLength, DeltaFraction.Default(300));

                Assert.AreEqual(0.65, savedFirstFracture.FracChokedSkinCalcd, DeltaFraction.Default(0.65));
                Assert.AreEqual(95, savedFirstFracture.FracChokedSkinMeas, DeltaFraction.Default(95));

                Assert.AreEqual(0.7, savedFirstFracture.DarcySkinCalcd, DeltaFraction.Default(0.7));
                Assert.AreEqual(0.15, savedFirstFracture.DarcySkinManual, DeltaFraction.Default(0.15));
                #endregion
            }
        }

        [TestMethod]
        public void MultiStageData_SetOneStage()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer/layers active.wflx");
                string sSavedfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer/Saved layers active.wflx");

                WFInt.OpenFile(sfile);

                int initialCount = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().NumberOfFractureStages;


                Assert.AreEqual(initialCount, 0);

                MultiFractureData firstStage = new MultiFractureData();

                firstStage.StageLength = 300;
                firstStage.TopMD = 10000;

                FractureData firstFracture = new FractureData();
                firstFracture.AddRef();

                firstFracture.bUseFracFaceSkin = true;
                firstFracture.bUseCalcdFracDmgSkin = true;
                firstFracture.bUseFracChokedSkin = true;
                firstFracture.bUseCalcdFracChokedSkin = true;
                firstFracture.bUseCalcdSkin = true;
                firstFracture.FracSkin = 0.5;
                firstFracture.FracHalfSpacing = 200;
                firstFracture.FracWidth = 5;
                firstFracture.FracHalfLength = 250;
                firstFracture.FracHeight = 2;
                firstFracture.FracPermNearWB = 0.2;
                firstFracture.FracWidthNearWB = 0.25;
                firstFracture.FracPerm = 0.5;
                firstFracture.FracDmgPerm = 0.74;
                firstFracture.FracDmgThick = 52;
                firstFracture.FracDmgSkinCalcd = 0.6;
                firstFracture.FracDmgSkinMeas = 75;
                firstFracture.FracChokedHalfLength = 300;
                firstFracture.FracChokedSkinCalcd = 0.65;
                firstFracture.FracChokedSkinMeas = 95;
                firstFracture.DarcySkinCalcd = 0.7;
                firstFracture.DarcySkinManual = 0.15;

                firstStage.AddRef().AddFractureData(firstFracture);



                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().AddMultiFractureData(firstStage);

                firstStage.StageLength = 400;
                firstStage.TopMD = 12345;

                firstFracture.FracSkin = 1.8;
                firstFracture.FracHalfSpacing = 100;

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().SetMultiFractureData(0, firstStage, 0, firstFracture);


                WFInt.SaveFile(sSavedfile);

                WFloInterface WFIntSaved = new WFloInterface();
                WFIntSaved.AddRef();

                WFIntSaved.OpenFile(sSavedfile);


                int afterAddingCount = WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().NumberOfFractureStages;

                //  int afterAddingCount = WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().;

                Assert.AreEqual(afterAddingCount, 1);

                #region read the number of fractures from the WellTech
                MultiFractureData savedMultiFracture = new MultiFractureData();
                savedMultiFracture.AddRef();

                FractureData savedFirstFracture = new FractureData();
                savedFirstFracture.AddRef();


                WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().GetMultiFractureData(0, savedMultiFracture, 0, savedFirstFracture);


                double Stagelength_1 = savedMultiFracture.StageLength;
                double TopMD_1 = savedMultiFracture.TopMD;

                int CountFractures = savedMultiFracture.NumberOfFractures;

                Assert.AreEqual(CountFractures, 1);

                Assert.AreEqual(400, savedMultiFracture.StageLength, DeltaFraction.Default(400));
                Assert.AreEqual(12345, savedMultiFracture.TopMD, DeltaFraction.Default(12345));
                #endregion

                # region get firstfracture properties

                Assert.AreEqual(1.8, savedFirstFracture.FracSkin, DeltaFraction.Default(1.8));       // Value has been changed.
                Assert.AreEqual(100, savedFirstFracture.FracHalfSpacing, DeltaFraction.Default(100)); // Value has been changed.
                Assert.AreEqual(5, savedFirstFracture.FracWidth, DeltaFraction.Default(5));
                Assert.AreEqual(250, savedFirstFracture.FracHalfLength, DeltaFraction.Default(250));
                Assert.AreEqual(2, savedFirstFracture.FracHeight, DeltaFraction.Default(2));
                Assert.AreEqual(0.2, savedFirstFracture.FracPermNearWB, DeltaFraction.Default(0.2));
                Assert.AreEqual(0.25, savedFirstFracture.FracWidthNearWB, DeltaFraction.Default(0.25));
                Assert.AreEqual(0.5, savedFirstFracture.FracPerm, DeltaFraction.Default(0.5));
                Assert.AreEqual(0.74, savedFirstFracture.FracDmgPerm, DeltaFraction.Default(0.74));
                Assert.AreEqual(52, savedFirstFracture.FracDmgThick, DeltaFraction.Default(52));
                Assert.AreEqual(0.6, savedFirstFracture.FracDmgSkinCalcd, DeltaFraction.Default(0.6));
                Assert.AreEqual(75, savedFirstFracture.FracDmgSkinMeas, DeltaFraction.Default(75));
                Assert.AreEqual(300, savedFirstFracture.FracChokedHalfLength, DeltaFraction.Default(300));

                Assert.AreEqual(0.65, savedFirstFracture.FracChokedSkinCalcd, DeltaFraction.Default(0.65));
                Assert.AreEqual(95, savedFirstFracture.FracChokedSkinMeas, DeltaFraction.Default(95));

                Assert.AreEqual(0.7, savedFirstFracture.DarcySkinCalcd, DeltaFraction.Default(0.7));
                Assert.AreEqual(0.15, savedFirstFracture.DarcySkinManual, DeltaFraction.Default(0.15));
                #endregion

            }
        }

        [TestMethod]
        public void MultiStageData_RemoveOneStage()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer/layers active.wflx");
                string sSavedfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer/Saved layers active.wflx");

                WFInt.OpenFile(sfile);

                int initialCount = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().NumberOfFractureStages;


                Assert.AreEqual(initialCount, 0);

                MultiFractureData firstStage = new MultiFractureData();

                firstStage.StageLength = 300;
                firstStage.TopMD = 10000;

                FractureData firstFracture = new FractureData();
                firstFracture.AddRef();

                firstFracture.bUseFracFaceSkin = true;
                firstFracture.bUseCalcdFracDmgSkin = true;
                firstFracture.bUseFracChokedSkin = true;
                firstFracture.bUseCalcdFracChokedSkin = true;
                firstFracture.bUseCalcdSkin = true;
                firstFracture.FracSkin = 0.5;
                firstFracture.FracHalfSpacing = 200;
                firstFracture.FracWidth = 5;
                firstFracture.FracHalfLength = 250;
                firstFracture.FracHeight = 2;
                firstFracture.FracPermNearWB = 0.2;
                firstFracture.FracWidthNearWB = 0.25;
                firstFracture.FracPerm = 0.5;
                firstFracture.FracDmgPerm = 0.74;
                firstFracture.FracDmgThick = 52;
                firstFracture.FracDmgSkinCalcd = 0.6;
                firstFracture.FracDmgSkinMeas = 75;
                firstFracture.FracChokedHalfLength = 300;
                firstFracture.FracChokedSkinCalcd = 0.65;
                firstFracture.FracChokedSkinMeas = 95;
                firstFracture.DarcySkinCalcd = 0.7;
                firstFracture.DarcySkinManual = 0.15;

                firstStage.AddRef().AddFractureData(firstFracture);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().AddMultiFractureData(firstStage);

                int afterAddingCount = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().NumberOfFractureStages;

                //  int afterAddingCount = WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().;

                Assert.AreEqual(afterAddingCount, 1);

                #region read the number of fractures from the WellTech
                MultiFractureData savedMultiFracture = new MultiFractureData();
                savedMultiFracture.AddRef();

                FractureData savedFirstFracture = new FractureData();
                savedFirstFracture.AddRef();


                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().GetMultiFractureData(0, savedMultiFracture, 0, savedFirstFracture);


                double Stagelength_1 = savedMultiFracture.StageLength;
                double TopMD_1 = savedMultiFracture.TopMD;

                int CountFractures = savedMultiFracture.NumberOfFractures;

                Assert.AreEqual(CountFractures, 1);

                Assert.AreEqual(300, savedMultiFracture.StageLength, DeltaFraction.Default(300));
                Assert.AreEqual(10000, savedMultiFracture.TopMD, DeltaFraction.Default(10000));

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().RemoveAllOneStageData();

                int afterRemoveAll = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().NumberOfFractureStages;

                Assert.AreEqual(afterRemoveAll, 0);

                #endregion
            }
        }

        [TestMethod]
        public void MultiStageData_ChokedModel()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer/layers active.wflx");
                string sSavedfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer/Saved layers active.wflx");

                WFInt.OpenFile(sfile);

                MultiFractureData firstStage = new MultiFractureData();

                firstStage.StageLength = 300;
                firstStage.TopMD = 10000;
                firstStage.ChokedModel = 1;

                FractureData firstFracture = new FractureData();
                firstFracture.AddRef();

                firstFracture.bUseFracFaceSkin = true;
                firstFracture.bUseCalcdFracDmgSkin = true;
                firstFracture.bUseFracChokedSkin = true;
                firstFracture.bUseCalcdFracChokedSkin = true;
                firstFracture.bUseCalcdSkin = true;
                firstFracture.FracSkin = 0.5;
                firstFracture.FracHalfSpacing = 200;
                firstFracture.FracWidth = 5;
                firstFracture.FracHalfLength = 250;
                firstFracture.FracHeight = 2;
                firstFracture.FracPermNearWB = 0.2;
                firstFracture.FracWidthNearWB = 0.25;
                firstFracture.FracPerm = 0.5;
                firstFracture.FracDmgPerm = 0.74;
                firstFracture.FracDmgThick = 52;
                firstFracture.FracDmgSkinCalcd = 0.6;
                firstFracture.FracDmgSkinMeas = 75;
                firstFracture.FracChokedHalfLength = 300;
                firstFracture.FracChokedSkinCalcd = 0.65;
                firstFracture.FracChokedSkinMeas = 95;
                firstFracture.DarcySkinCalcd = 0.7;
                firstFracture.DarcySkinManual = 0.15;

                firstStage.AddRef().AddFractureData(firstFracture);

                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().AddMultiFractureData(firstStage);

                WFInt.SaveFile(sSavedfile);

                WFloInterface WFIntSaved = new WFloInterface();
                WFIntSaved.AddRef();

                WFIntSaved.OpenFile(sSavedfile);

                MultiFractureData savedMultiFracture = new MultiFractureData();
                savedMultiFracture.AddRef();

                FractureData savedFracture = new FractureData();
                savedFracture.AddRef();

                WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().GetMultiFractureData(0, savedMultiFracture, 0, savedFracture);

                short ChokedModel = savedMultiFracture.ChokedModel;

                Assert.AreEqual(1, ChokedModel);
            }
        }

        [TestMethod]
        public void MultiStageData_MultiFractureProductivities()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer/Multiple Fractures.wflx");
                string sSavedfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer/Saved Multiple Fractures.wflx");

                //open wellflo
                WFInt.OpenFile(sfile);

                int initialCount = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().NumberOfFractureStages;
                bool bMatrix = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().UseReservoirMatrixCorrection;

                double ReservoirPI = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().ReservoirProductivityIndex;
                double FracturePI = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().FractureProductivityIndex;
                double WellborePI = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WellboreProductivityIndex;
                double MatrixPI = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().MatrixProductivityIndex;

                double PI = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().ProductivityIndex;

                ReservoirPI = Math.Round(ReservoirPI, 4);
                FracturePI = Math.Round(FracturePI, 4);
                WellborePI = Math.Round(WellborePI, 4);
                MatrixPI = Math.Round(MatrixPI, 1);
                PI = Math.Round(PI, 4);

                Assert.AreEqual(ReservoirPI, 1.0072, DeltaFraction.Default(ReservoirPI));
                Assert.AreEqual(FracturePI, 0.4215, DeltaFraction.Default(FracturePI));
                Assert.AreEqual(WellborePI, 0.0354, DeltaFraction.Default(WellborePI));
                Assert.AreEqual(MatrixPI, 0.0, DeltaFraction.Default(MatrixPI));
                Assert.AreEqual(PI, 0.0317, DeltaFraction.Default(PI));

                // Get the first stage first fracture pro
                MultiFractureData savedMultiFracture = new MultiFractureData();
                savedMultiFracture.AddRef();
                FractureData savedFirstFracture = new FractureData();
                savedFirstFracture.AddRef();
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().GetMultiStageData().AddRef().GetMultiFractureData(0, savedMultiFracture, 0, savedFirstFracture);

                double Stagelength_1 = savedMultiFracture.StageLength;
                double TopMD_1 = savedMultiFracture.TopMD;
                int CountFractures = savedMultiFracture.NumberOfFractures;

                Assert.AreEqual(CountFractures, 1);
                Assert.AreEqual(313, Stagelength_1, DeltaFraction.Default(313));
                Assert.AreEqual(5000, TopMD_1, DeltaFraction.Default(5000));

                // Use the Reservoir Matrix Correction
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().UseReservoirMatrixCorrection = true; //true
                bool bMatrix_1 = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().UseReservoirMatrixCorrection;
                //save wellflo
                WFInt.SaveFile(sSavedfile);

                WFloInterface WFIntSaved = new WFloInterface();
                WFIntSaved.AddRef();
                //open the saved wellflo
                WFIntSaved.OpenFile(sSavedfile);

                bool savedbMatrix = WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().UseReservoirMatrixCorrection;
                Assert.AreEqual(savedbMatrix, true);

                double NewReservoirPI = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().ReservoirProductivityIndex;
                double NewFracturePI = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().FractureProductivityIndex;
                double NewWellborePI = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().WellboreProductivityIndex;
                double NewMatrixPI = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().MatrixProductivityIndex;

                double NewPI = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().ProductivityIndex;

                NewReservoirPI = Math.Round(NewReservoirPI, 4);
                NewFracturePI = Math.Round(NewFracturePI, 4);
                NewWellborePI = Math.Round(NewWellborePI, 4);
                NewMatrixPI = Math.Round(NewMatrixPI, 4);
                NewPI = Math.Round(NewPI, 4);

                Assert.AreEqual(NewReservoirPI, 1.0072, DeltaFraction.Default(NewReservoirPI));
                Assert.AreEqual(NewFracturePI, 0.4215, DeltaFraction.Default(NewFracturePI));
                Assert.AreEqual(NewWellborePI, 0.0354, DeltaFraction.Default(NewWellborePI));
                Assert.AreEqual(NewMatrixPI, 0.0122, DeltaFraction.Default(NewMatrixPI));
                Assert.AreEqual(NewPI, 0.0439, DeltaFraction.Default(NewPI));


            }
        }

        [TestMethod]
        public void MultiStageData_CalculatedDietZFactor()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer/Multiple Fractures.wflx");
                string sSavedfile = WellFloFileLocation.BaselineWPSModel("WPS_Layer/Saved Multiple Fractures.wflx");

                //open wellflo
                WFInt.OpenFile(sfile);

                bool bMatrix = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().UseCalcDietz;
                Assert.AreEqual(bMatrix, true);

                // Use the Reservoir Matrix Correction
                WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().UseCalcDietz = false;
                //save wellflo
                WFInt.SaveFile(sSavedfile);

                WFloInterface WFIntSaved = new WFloInterface();
                WFIntSaved.AddRef();
                //open the saved wellflo
                WFIntSaved.OpenFile(sSavedfile);

                bool savedbMatrix = WFIntSaved.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().UseCalcDietz;
                Assert.AreEqual(savedbMatrix, false);

            }
        }

        [TestMethod]
        public void Layer_Cond_AoF()
        {
            using (new LifeTimeScope())
            {
                WFloInterface WFInt = new WFloInterface();
                WFInt.AddRef();

                string sfile1 = WellFloFileLocation.BaselineWPSModel("WPS_Layer\\Cond-2Layers.wflx");
                WFInt.OpenFile(sfile1);

                WFInt.WellModel.AddRef().Layers.AddRef().SetLayerStatus(1, true);
                double ActualAoF = WFInt.WellModel.AddRef().Layers.AddRef().Item(1).AddRef().AOF;
                double ExpectedAoF = 4.846700668;
                Assert.IsTrue(!Double.IsNaN(ActualAoF), "Failed to calculate AoF of Layer 1");
                Assert.AreEqual(ExpectedAoF, ActualAoF, 1E-5);

                ActualAoF = WFInt.WellModel.AddRef().Layers.AddRef().Item(2).AddRef().AOF;
                ExpectedAoF = 1.183389544;
                Assert.IsTrue(!Double.IsNaN(ActualAoF), "Failed to calculate AoF of Layer 2");
                Assert.AreEqual(ExpectedAoF, ActualAoF, 1E-5);

                WFInt.EndWellFlo();
            }
        }


    }

}

