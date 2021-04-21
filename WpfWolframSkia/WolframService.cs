using System;
using System.ComponentModel;
using Wolfram.NETLink;

namespace WpfWolframSkia
{
    public class WolframService
    {
        public void Execute()
        {
            // This launches the Mathematica kernel:
            IKernelLink ml = MathLinkFactory.CreateKernelLink();
        
            // Discard the initial InputNamePacket the kernel will send when launched.
            ml.WaitAndDiscardAnswer();
        
            // Now compute 2+2 in several different ways.
        
            // The easiest way. Send the computation as a string and get the result in a single call:
            string result = ml.EvaluateToOutputForm("2+2", 0);
            Console.WriteLine("2 + 2 = " + result);
        
            // Use Evaluate() instead of EvaluateToXXX() if you want to read the result as a native type
            // instead of a string.
            ml.Evaluate("2+2");
            ml.WaitForAnswer();
            int intResult = ml.GetInteger();
            Console.WriteLine("2 + 2 = " + intResult);
        
            // You can also get down to the metal by using methods from IMathLink:
            ml.PutFunction("EvaluatePacket", 1);
            ml.PutFunction("Plus", 2);
            ml.Put(2);
            ml.Put(2);
            ml.EndPacket();
            ml.WaitForAnswer();
            intResult = ml.GetInteger();
            Console.WriteLine("2 + 2 = " + intResult);
        
            // Always Close when done:
            ml.Close();
        }

        private BackgroundWorker _backgroundWorker = null;
        
        public void ExecuteBackground()
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.WorkerReportsProgress = true;
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += BackgroundWorkerMethod;
            _backgroundWorker.ProgressChanged += worker_ProgressChanged;
            _backgroundWorker.RunWorkerCompleted += worker_RunWorkerCompleted;
            
            _backgroundWorker.RunWorkerAsync();
        }
        
        private void CancelExport()
        {
            _backgroundWorker?.CancelAsync();
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void BackgroundWorkerMethod(object sender, DoWorkEventArgs e)
        {
            try
            {
                Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }        
    }
}