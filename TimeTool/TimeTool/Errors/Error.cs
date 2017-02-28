using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimeTool.Errors {
    internal static class Error {
        public static void ShowFormatError() {
            MessageBox.Show("The entered Lap time has an invalid format\n" +
                            "Please enter your laptime in the following format:\n" +
                            "00:00.000 or 0:00:00.000", "Format Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
