using System;

namespace TimeTool.Laptime {
    internal class LapTime : ILapTime{
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public int Milliseconds { get; set; }
        
        public bool HoursUsed => this.Hours > 0;

        private float TrueMilliseconds => this.Milliseconds / 1000f;

        public LapTime(int minutes, int seconds, int milliseconds) {
            this.Hours = 0;
            this.Minutes = minutes;
            this.Seconds = seconds;
            this.Milliseconds = milliseconds;
        }

        public LapTime(int hours, int minutes, int seconds, int milliseconds) {
            this.Hours = hours;
            this.Minutes = minutes;
            this.Seconds = seconds;
            this.Milliseconds = milliseconds;
        }

        private void CorrectValues() {
            while (this.Milliseconds >= 1000) {
                this.Seconds += 1;
                this.Milliseconds -= 1000;
            }
            while (this.Seconds >= 60) {
                this.Minutes += 1;
                this.Seconds -= 60;
            }
            while (this.Minutes >= 60) {
                this.Hours += 1;
                this.Minutes -= 60;
            }
        }

        public override string ToString() {
            this.CorrectValues();
            return this.HoursUsed ? 
                $"{this.Hours}:{this.Minutes.ToString("00")}:{this.Seconds.ToString("00")}.{this.Milliseconds.ToString("000")}" :
                $"{this.Minutes.ToString("00")}:{this.Seconds.ToString("00")}.{this.Milliseconds.ToString("000")}";
        }

        public void Divide(float laps) {
            float time = this.Hours * 60 * 60 + this.Minutes * 60 + this.Seconds + this.TrueMilliseconds;

            float dividedTime = time / laps;

            this.Hours = (int) (dividedTime / 60f / 60f);
            dividedTime -= this.Hours * 60 * 60;

            this.Minutes = (int) (dividedTime / 60f);
            dividedTime -= this.Minutes * 60;

            this.Seconds = (int) dividedTime;
            dividedTime -= this.Seconds;

            this.Milliseconds = (int) (Math.Round(dividedTime, 3) * 1000);
        }
    }
}
