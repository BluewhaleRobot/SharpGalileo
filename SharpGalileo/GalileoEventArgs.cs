using System;
using System.Collections.Generic;
using System.Text;

namespace SharpGalileo
{
    class GalileoEventArgs
    {
        public class ConnectEventArgs : EventArgs {
            public GALILEO_RETURN_CODE Status { get; private set; }
            public String ID { get; private set; }

            public ConnectEventArgs(GALILEO_RETURN_CODE status, String id) {
                Status = status;
                ID = id;
            }
        }
    }
}
