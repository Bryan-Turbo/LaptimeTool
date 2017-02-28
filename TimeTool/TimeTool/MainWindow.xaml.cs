using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TimeTool.Laptime;

namespace TimeTool {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private ILapTime LapTime { get; set; }

        public MainWindow() {
            InitializeComponent();
            this.Title = "Laptime Converter";

            this.LapTime = new LapTime(0, 0, 0, 0);

            this.EnterButton.Content = "Show Average Laptime";
            this.AverageLapLabel.Content = "Laptime";
            this.EnterLabel1.Content = "Enter Racetime";

            this.ComboBox.Items.Add(AppStyle.Light);
            this.ComboBox.SelectedIndex = 0;
        }

        private void ClickLaptime(object sender, RoutedEventArgs e) {
            this.EnterLaptime();
        }

        private void EnterLaptime(object sender, KeyEventArgs e) {
            if (e.Key == Key.Enter) {
                this.EnterLaptime();
            }
        }

        private void EnterLaptime() {
            string[] times = this.InputLaptime.Text.Split(':', ',', '.');

            int[] laptimes = new int[times.Length];
            for (int i = times.Length - 1; i >= 0; i--) {
                if (int.TryParse(times[i], out laptimes[i])) {
                    continue;
                }
                this.ShowError();
                return;
            }
            if (laptimes[laptimes.Length - 1] >= 1000) {
                this.ShowError();
                return;
            }

            this.LapTime = laptimes.Length == 4
                ? new LapTime (laptimes[0], laptimes[1], laptimes[2], laptimes[3])
                : new LapTime (laptimes[0], laptimes[1], laptimes[2]);

            this.LaptimeBox.Text = this.LapTime.ToString();
        }

        private void ShowError() {
            MessageBox.Show("The entered Lap time has an invalid format\n" +
                            "Please enter your laptime in the following format:\n" +
                            "00:00.000 or 0:00:00.000", "Format Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}