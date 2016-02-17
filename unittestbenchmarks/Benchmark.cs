using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TestUtilities
{
    /// <summary>
    /// Class to manage benchmarks for tests.
    /// Usage:
    /// - Derive your test class from this class
    /// - Add this line in "TestInitialize()": InitializeBenchmarks(this.GetType(), TestContext.TestName);  
    /// - Add this line in "TestCleanup()": WriteToResultXML();
    /// - Inside each test, replace the "Assert.AreEqual()" call with a line similar to this:
    ///   AreEqual([benchmarkID], Actual.ToString());
    /// </summary>
    public class BenchmarkedTest
    {
        private TestContext testContext;
        private string testName;
        private string strBenchmarkFile;
        private string strResultFile;

        private Dictionary<string, string> dictBenchmarks;
        private Dictionary<string, string> dictResults;

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContext;
            }
            set
            {
                testContext = value;
            }
        }

        /// <summary>
        /// Reads and initializes benchmarks from an XML file
        /// </summary>
        /// <param name="testClassType">The Type of the TestClass containing the TestMethod to load benchmarks for</param>
        /// <param name="strTestMethodName">The name of the TestMethod to load benchmarks for</param>
        public void InitializeBenchmarks(Type testClassType, string strTestMethodName)
        {
            InitializeBenchmarks(testClassType.Namespace, testClassType.Name, strTestMethodName);
        }

        /// <summary>
        /// Reads and initializes benchmarks from an XML file
        /// </summary>
        /// <param name="strTestNamespace">The namespace that contains the TestClass which contains this TestMethod</param>
        /// <param name="strTestClass">The TestClass which contains this TestMethod</param>
        /// <param name="strTestMethodName">The name of the TestMethod to load benchmarks for</param>
        public void InitializeBenchmarks(string strTestNamespace, string strTestClass, string strTestMethodName)
        {
            // Create directories for benchmarks, the test namespace, and the test class if they don't exist
            string benchmarksDirectory = GetBenchmarksFolder();
            if (!Directory.Exists(benchmarksDirectory))
            {
                Directory.CreateDirectory(benchmarksDirectory);
            }

            string namespaceDirectory = benchmarksDirectory + "\\" + strTestNamespace;
            if (!Directory.Exists(namespaceDirectory))
            {
                Directory.CreateDirectory(namespaceDirectory);
            }

            string classDirectory = namespaceDirectory + "\\" + strTestClass + "\\";
            if (!Directory.Exists(classDirectory))
            {
                Directory.CreateDirectory(classDirectory);
            }

            // Get benchmarks and result file paths
            // The BaseFolder should be \Testing\UQA\AssetsDev\Benchmarks\<TestNS>\<TestClass>\
            this.strBenchmarkFile = classDirectory + strTestMethodName + ".Benchmarks.xml";
            this.strResultFile = classDirectory + strTestMethodName + ".Results.xml";
            this.testName = strTestMethodName;
            this.dictBenchmarks = new Dictionary<string, string>();
            this.dictResults = new Dictionary<string, string>();

            XmlSerializer serializer = new XmlSerializer(typeof(Test));

            // If the benchmarks file exists, load this test's benchmarks
            if (File.Exists(strBenchmarkFile))
            {
                Test t;
                // Read the benchmarks XML file
                using (FileStream fs = new FileStream(strBenchmarkFile, FileMode.Open, FileAccess.Read))
                {
                    t = (Test)serializer.Deserialize(fs);
                }

                // Populate the benchmarks dictionary with the values from the deserialized XML
                t.Benchmarks.ForEach(benchmark => dictBenchmarks.Add(benchmark.Key, benchmark.Value));

                // If there's no results file yet, copy the benchmarks file to the results file
                if (!File.Exists(strResultFile))
                {
                    File.Copy(strBenchmarkFile, strResultFile);
                    File.SetAttributes(strResultFile, FileAttributes.Normal);
                }

                Test resultTest;
                // Read the benchmarks XML file
                using (FileStream fs = new FileStream(strBenchmarkFile, FileMode.Open, FileAccess.Read))
                {
                    resultTest = (Test)serializer.Deserialize(fs);
                }

                // Populate the results dictionary with the values from the deserialized XML
                dictBenchmarks = new Dictionary<string, string>();
                resultTest.Benchmarks.ForEach(benchmark => dictBenchmarks.Add(benchmark.Key, benchmark.Value));
            }
        }

        private string GetBenchmarksFolder()
        {
            string executingAssembly = System.Reflection.Assembly.GetExecutingAssembly().Location;
            DirectoryInfo diExecutingAssembly = new DirectoryInfo(executingAssembly);

            return diExecutingAssembly.Parent.Parent.Parent.FullName + "\\AssetsDev\\Benchmarks";
        }

        /// <summary>
        /// Checks a value against its benchmark
        /// </summary>
        /// <param name="id">Benchmark ID</param>
        /// <param name="value">Value to test against benchmark</param>
        public void AreEqual<T>(string id, T value)
        {
            string valueToTest = value.ToString();

            // Update the results with the value we're testing
            if (dictResults.ContainsKey(id))
            {
                dictResults[id] = valueToTest;
            }
            else
            {
                // If the benchmark couldn't be found in the results, add it to the results
                dictResults.Add(id, valueToTest);
            }

            // Test the input value against the benchmark
            if (dictBenchmarks.ContainsKey(id))
            {
                string benchmarkValue = dictBenchmarks[id];
                
                // Write to output
                WriteToTestReport(benchmarkValue, valueToTest);

                double num = 0.0;
                bool isNumeric = double.TryParse(valueToTest, out num);

                if (isNumeric)
                {
                    Assert.AreEqual(Convert.ToDouble(benchmarkValue), Convert.ToDouble(value), DeltaFraction.Default(Convert.ToDouble(benchmarkValue)));
                }
                else
                {
                    Assert.AreEqual(benchmarkValue.ToUpper(), valueToTest.ToUpper());
                }
            }
            else
            {
                WriteToTestReport("Cannot find the benchmark for: '" + testName + "." + id + "'");
            }
        }

        /// <summary>
        /// Overloaded: Writes to the test report
        /// </summary>
        /// <param name="msg">Message</param>
        public void WriteToTestReport(string msg)
        {
            if(TestContext != null)
                TestContext.WriteLine(msg);
        }

        /// <summary>
        /// Overloaded: Writes to the test report
        /// </summary>
        /// <param name="expected">Expected value</param>
        /// <param name="actual">Actual value</param>
        public void WriteToTestReport(string expected, string actual)
        {
            if (TestContext != null)
            {
                try
                {
                    double e = Convert.ToDouble(expected);
                    double a = Convert.ToDouble(actual);
                    TestContext.WriteLine("Test: " + testName + ".\tExpected: " + expected + ".\tActual: " + actual + ".\tDifference: " + (a - e) / e * 100 + "%.");
                }
                catch
                {
                    TestContext.WriteLine("Test: " + testName + ".\tExpected: " + expected + ".\tActual: " + actual);
                } 
            }
        }

        /// <summary>
        /// Writes the XML object to the result XML file. This file can replace 
        /// the original benchmark XML file in order to make updating benchmark easier.
        /// </summary>
        /// <param name="testClassName">Test class name</param>
        public void WriteToResultXML()
        {
            // Convert the result benchmarks dictionary to a test object
            Test resultTest = new Test();
            resultTest.Name = this.testName;
            resultTest.Benchmarks = new List<Benchmark>();
            foreach(string key in dictResults.Keys)
            {
                Benchmark b = new Benchmark();
                b.Key = key;
                b.Value = dictResults[key];
                resultTest.Benchmarks.Add(b);
            }
            
            XmlSerializer serializer = new XmlSerializer(typeof(Test));
            using (MemoryStream ms = new MemoryStream())
            using (StreamWriter sw = new StreamWriter(File.Open(strResultFile, FileMode.Create), Encoding.UTF8))
            {
                serializer.Serialize(ms, resultTest);
                sw.Write(Encoding.UTF8.GetString(ms.ToArray()));
            }

            // Copy resultfile to the benchmark file if the latter does not exist
            if (!File.Exists(strBenchmarkFile))
            {
                File.Copy(strResultFile, strBenchmarkFile);
            }
        }
    }

    /// <summary>
    /// Defines a test and its Benchmarks
    /// This is the root XML node when this is serialized
    /// </summary>
    [XmlRoot]
    public class Test
    {
        public string Name { get; set; }

        [XmlArray]
        public List<Benchmark> Benchmarks { get; set; }

    }

    /// <summary>
    /// Defines a key-value pair that represents one benchmark in a test
    /// </summary>
    public class Benchmark
    {
        [XmlAttribute]
        public string Key;
        [XmlAttribute]
        public string Value;
    }
}
