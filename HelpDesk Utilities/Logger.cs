using System;

namespace HelpDesk_Utilities {
    public class Logger {

        static Form1 form;
        public delegate void InvokeDelegate(string logMessage);

        public Logger(Form1 form1) {

            form = form1;
        }

        /// <summary>
        /// Log will invoke a method to append the given string to the RichTextBox.
        /// </summary>
        /// <param name="logMessage">Message to append to log</param>
        public static void Log(string logMessage) {

            form.richTextBox_logWindow.BeginInvoke(new InvokeDelegate(InvokeMethod),logMessage);
        }

        public static void InvokeMethod(string logMessage) {

            string time = DateTime.Now.ToString() + ": ";
            form.richTextBox_logWindow.AppendText(time + logMessage + Environment.NewLine);
        }
    }
}
