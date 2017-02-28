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
using TimeTool.Errors;
using TimeTool.Laptime;
using TimeTool.Styles;

namespace TimeTool {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private ILapTime LapTime { get; set; }
        private AppStyle CurrentStyle { get; set; }

        public MainWindow() {
            InitializeComponent();
            this.Title = "Laptime Converter";

            this.LapTime = new LapTime(0, 0, 0, 0);

            this.EnterButton.Content = "Show Average Laptime";
            this.AverageLapLabel.Content = "Average Laptime";
            this.EnterLabel1.Content = "Enter Racetime";

            this.CurrentStyle = AppStyle.Dark;
            this.SetToDark();
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
                Error.ShowFormatError();
                return;
            }

            if (laptimes.Length != 3 && laptimes.Length != 4) {
                Error.ShowFormatError();
                return;
            }

            if (laptimes[laptimes.Length - 1] >= 1000) {
                Error.ShowFormatError();
                return;
            }

            int laps;
            if (!int.TryParse(this.LapAmountBox.Text, out laps)) {
                Error.ShowLapamountError();
                return;
            }

            this.LapTime = laptimes.Length == 4
                ? new LapTime (laptimes[0], laptimes[1], laptimes[2], laptimes[3])
                : new LapTime (laptimes[0], laptimes[1], laptimes[2]);

            this.LapTime.Divide(laps);

            this.LaptimeBox.Text = this.LapTime.ToString();
        }

        private void ChangeStyleButton_Click(object sender, RoutedEventArgs e) {
            if (this.CurrentStyle == AppStyle.Dark) {
                this.CurrentStyle = AppStyle.Light;
                this.SetToLight();
            } else {
                this.CurrentStyle = AppStyle.Dark;
                this.SetToDark();
            }
        }

        private void SetToDark() {
            this.EnterButton.Style = (Style) Application.Current.Resources["DarkButtonStyle"];
            this.ChangeStyleButton.Style = (Style) Application.Current.Resources["DarkButtonStyle"];
            this.InputLaptime.Style = (Style) Application.Current.Resources["DarkTextBoxStyle"];
            this.LapAmountBox.Style = (Style) Application.Current.Resources["DarkTextBoxStyle"];
            this.LaptimeBox.Style = (Style) Application.Current.Resources["DarkTextBoxStyle"];
            this.Window.Background = new SolidColorBrush(Color.FromRgb(56, 56, 56));
            this.AverageLapLabel.Foreground = new SolidColorBrush(Color.FromRgb(239, 239, 239));
            this.LapLabel.Foreground = new SolidColorBrush(Color.FromRgb(239, 239, 239));
            this.EnterLabel1.Foreground = new SolidColorBrush(Color.FromRgb(239, 239, 239));
            this.NodoBanner.Background = new SolidColorBrush(Color.FromRgb(86, 86, 86));
            this.NodoBanner.BorderBrush = new SolidColorBrush(Color.FromRgb(239, 239, 239));
        }

        private void SetToLight() {
            this.EnterButton.Style = (Style) Application.Current.Resources["LightButtonStyle"];
            this.ChangeStyleButton.Style = (Style) Application.Current.Resources["LightButtonStyle"];
            this.InputLaptime.Style = (Style) Application.Current.Resources["LightTextBoxStyle"];
            this.LapAmountBox.Style = (Style) Application.Current.Resources["LightTextBoxStyle"];
            this.LaptimeBox.Style = (Style) Application.Current.Resources["LightTextBoxStyle"];
            this.Window.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            this.AverageLapLabel.Foreground = new SolidColorBrush(Color.FromRgb(86, 86, 86));
            this.LapLabel.Foreground = new SolidColorBrush(Color.FromRgb(86, 86, 86));
            this.EnterLabel1.Foreground = new SolidColorBrush(Color.FromRgb(86, 86, 86));
            this.NodoBanner.Background = new SolidColorBrush(Color.FromRgb(239, 239, 239));
            this.NodoBanner.BorderBrush = new SolidColorBrush(Color.FromRgb(86, 86, 86));
        }
    }
}