using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model.SessionEventArgs
{
    public class SessionShowMsg_EventArgs :EventArgs
    {
        public IpInfo info;
        public SessionShowMsg_EventArgs(IpInfo info) 
        {
            this.info = info;
        } 
    }
}
