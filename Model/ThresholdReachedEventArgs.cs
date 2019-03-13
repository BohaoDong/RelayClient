using System;

namespace Model
{
    public class ThresholdReachedEventArgs : EventArgs
    {
        public string Ip { get; set; }
        public string Msg { get; set; }
    }
}
