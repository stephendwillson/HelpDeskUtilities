using System;
using System.Drawing;

namespace HelpDesk_Utilities {
    public class Logger {

        static Form1 form;
        public delegate void InvokeDelegate(string logMessage, Color color, bool bold);

        public Logger(Form1 form1) {

            form = form1;
        }

        /// <summary>
        /// Log will invoke a method to append the given string to the RichTextBox.
        /// </summary>
        /// <param name="logMessage">Message to append to log</param>
        public static void Log(string logMessage,Color color,bool bold) {

            form.richTextBox_logWindow.BeginInvoke(new InvokeDelegate(InvokeMethod), new object[] { logMessage, color, bold });
        }

        public static void InvokeMethod(string logMessage, Color color, bool bold) {

            form.richTextBox_logWindow.SelectionFont = new Font(form.richTextBox_logWindow.Font, bold ? FontStyle.Bold : FontStyle.Regular);
            form.richTextBox_logWindow.SelectionColor = color;

            string time = DateTime.Now.ToString() + ": ";
            form.richTextBox_logWindow.AppendText(time + logMessage + Environment.NewLine);

            //autoscroll to bottom of text
            form.richTextBox_logWindow.SelectionStart = form.richTextBox_logWindow.Text.Length;
            form.richTextBox_logWindow.ScrollToCaret();
        }
    }
}
