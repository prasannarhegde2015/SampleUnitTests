!---LOWIS ATS system setup --!     (GUI = UI automation tests ,  Script Tests = css script test) 
*** Current ATS envoirment is clean(all wells in LOWIS are created at the specific test run and deleted after)
*** COM2 is IP , COM3 is CygNet Port, COM4 is MODBUS
*** RTUEmu xml setup , LOWIS ProcessList XML , can all be found at \\slodat01\Dept_shares\QA\LOWIS 7.0.1R\Current\
*** Process list can be added to,exported,and replaced in the above current location if need be 
*** Currently build 1041 is used and all APU/hotfixes to current date are applied after  
*** Current automation in the LOWIS ATS project of out Katy is checked in at 7_0R_UQA under LowisRel in perforce. 
*** LLISPE team builds and delivers their own tests to the LOWIS Release ATS Project since LLISPE became part of 7.0.2 LOWIS. 
 

LOWIS Release(7_0R) Automation Tests Coverage Summary -- *For additional details on each test, see ATS test run logs 
---------------------------------------------------------------------------------------------------------------------

1. LLISPE non-GUI tests (script tests) are run in this ATS project (i.e Connector,BasicComp,BeamValve,LLISPE API -- all delivered by LLISPE team) 
2. LOWIS Commands non-GUI tests (script tests) for select BEAM,ESP,PCP,Injection wells
3. LOWIS Commands GUI test in ATS core project(vstest) for BEAM(EPIC,SAM,AEPOC2) wells.
4. LOWIS CurrentStatus non-GUI tests (script tests) for select BEAM,ESP,PCP,Injection wells
5. LOWIS CurrentStatus GUI test in ATS core project(vstest) for BEAM(EPIC,SAM,AEPOC2) wells.
6. LOWIS RTUReadWrite non-GUI tests (script tests) for select BEAM,ESP,PCP,Injection wells
7. LOWIS RTUReadWrite GUI test in ATS core project(vstest) for BEAM(EPIC,SAM,AEPOC2) wells for only select registers.
8. LOWIS VerifyParameters non-GUI tests (script tests) for select BEAM,ESP,PCP,Injection wells
9. LOWIS Dynacard non-GUI tests (script tests) for select BEAM,ESP,PCP,Injection wells for all last cards for all types 
10.LOWIS Dynacard GUI test in ATS core project(vstest) for BEAM(EPIC,SAM,AEPOC2) wells for CurrentCard.
11.LOWIS ControllerCharts non-GUI tests (script tests) for select BEAM wells
12.LOWIS Admin2, AE Scan Task, Etulz, Weatherford core logging system developer tests
13.LOWIS MOP(Method of Production change) mixed GUI (MS CodedUI) and non-GUI(script test) tests.
14.LOWIS checkdb and checkreflists non-GUI(script) test. 
15.LOWIS Beam Analysis Report Test for SAMRP2(Full+Downhole Card) Gibbs calculation. 

