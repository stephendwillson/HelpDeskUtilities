/* All credit to Megan R. Kelley for this class.
 * Used/modified with permission. Source can be found at:
 * https://github.com/meganrkelley/TestbedManager/blob/master/TestBedManager/TestBedManager/DomainModel/AppStats.cs
 * */

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace HelpDesk_Utilities {
    internal class AppStats {

        private BackgroundWorker worker = new BackgroundWorker();
        private Label statusText;
        private string totalMemory;

        public AppStats(Label statusTextObj) {

            statusText = statusTextObj;
            totalMemory = GetTotalMemory();

            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;

            worker.RunWorkerAsync();
        }


        private void worker_DoWork(object sender, DoWorkEventArgs e) {

            while (true) {
                List<object> userStateObj = new List<object>(2);
                userStateObj.Add(Process.GetCurrentProcess().PrivateMemorySize64 / 1048576);
                userStateObj.Add(0);

                worker.ReportProgress(0, userStateObj);
                Thread.Sleep(1000);
            }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e) {

            List<object> userStateObj = (List<object>)e.UserState;
            long memorySize = (long)userStateObj[0];

            statusText.Text = "Memory usage: " + memorySize + " MB / ~" + totalMemory + " GB";
        }

        private string GetTotalMemory() {

            int bytesPerMebibyte = (1 << 20);
            Microsoft.VisualBasic.Devices.ComputerInfo myCompInfo = new Microsoft.VisualBasic.Devices.ComputerInfo();
            string physicalMemory = "" + (myCompInfo.TotalPhysicalMemory / (ulong)bytesPerMebibyte / 1024);

            return physicalMemory;
        }
    }
}