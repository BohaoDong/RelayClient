using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SentMsgEvent
    {
        //声明委托类型的myeventargs EventHandler事件。 
        public event EventHandler<ThresholdReachedEventArgs> ThresholdReached;
         
        public void Add(string ip,string msg)//方法
        {
            if (ip != null && msg != null)
            {
                ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
                args.Ip = ip;
                args.Msg = msg;
                OnThresholdReached(args);
            }
        }
        protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
        {
            EventHandler<ThresholdReachedEventArgs> handler = ThresholdReached;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
