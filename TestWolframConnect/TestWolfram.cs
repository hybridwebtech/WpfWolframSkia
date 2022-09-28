using System;
using System.Drawing.Imaging;
using NUnit.Framework;
using Wolfram.NETLink;

namespace TestWolframConnect
{
    [TestFixture]
    public class TestWolfram
    {
        private IKernelLink _ml;
        
        [OneTimeSetUp]
        public void Setup()
        {
            // This launches the Mathematica kernel
            string[] mlArgs =
                { "-linkmode", "launch", "-linkname", "c:/program files/wolfram research/wolfram engine/13.0/wolfram.exe" };
            _ml = MathLinkFactory.CreateKernelLink(mlArgs);

            // Discard the initial InputNamePacket the kernel will send when launched.
            _ml.WaitAndDiscardAnswer();
        }
        
        [Test]
        public void Test_Simple()
        {
            string result = _ml.EvaluateToOutputForm("2 + 2", 0);
            Assert.AreEqual("4", result);
        }

        [Test]
        public void Test_Compound()
        {
            string toEvaluate = @"rgbImage = Import[""C:\\test_data\\7YB7PM62.png""];
            img = ChanVeseBinarize[ColorSeparate[rgbImage, ""R""]];
            Export[""C:\\temp\\wound.jpg"", img];";

            try
            {
                _ml.Evaluate(toEvaluate);
                _ml.WaitAndDiscardAnswer();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}