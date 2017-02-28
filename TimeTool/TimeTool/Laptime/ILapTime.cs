namespace TimeTool.Laptime {
    internal interface ILapTime {
        bool HoursUsed { get; }

        string ToString();
        void Divide(float laps);
    }
}
