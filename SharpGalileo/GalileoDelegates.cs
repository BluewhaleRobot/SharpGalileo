using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpGalileo
{
    public class GalileoDelegates
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void OnConnectDelegate(GALILEO_RETURN_CODE status, IntPtr id, int length);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void OnDisconnectDelegate(GALILEO_RETURN_CODE status, IntPtr id, int length);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void StatusUpdatedDelegate(GALILEO_RETURN_CODE status, IntPtr id, int length);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GoalReachedDelegate(GALILEO_RETURN_CODE status, IntPtr id, int length);
    }
}
