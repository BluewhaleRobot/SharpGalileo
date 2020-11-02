using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static SharpGalileo.GalileoDelegates;

namespace SharpGalileo
{

    public enum GALILEO_RETURN_CODE
    {
        OK,
        NOT_CONNECTED,
        INVALIDE_STATE,
        NO_SERVER_FOUND,
        MULTI_SERVER_FOUND,
        NETWORK_ERROR,
        ALREADY_CONNECTED,
        TIMEOUT,
        SERVER_ERROR,
        GOAL_CANCELLED,
        INVALIDE_GOAL,
        INVALIDE_PARAMS
    };

    internal static class GalileoFunctions
    {
#if POSIX
        const string dll = "libGalileoSDK.so";
#else
        const string dll = "GalileoSDK.dll";
#endif
        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateInstance")]
        internal static extern IntPtr CreateInstance();

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ReleaseInstance")]
        internal static extern void ReleaseInstance(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "Dispose")]
        internal static extern void Dispose(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "Connect")]
        internal static extern GALILEO_RETURN_CODE Connect(IntPtr sdk, byte[] targetID, long length, bool auto_connect, int timeout, OnConnectDelegate onConnect, OnDisconnectDelegate OnDisconnect);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ConnectIOT")]
        internal static extern GALILEO_RETURN_CODE ConnectIOT(IntPtr sdk, byte[] targetID, long length, int timeout, byte[] password, long passLength, OnConnectDelegate onConnect, OnDisconnectDelegate OnDisconnect);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "Disconnect")]
        internal static extern void Disconnect(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "KeepConnection")]
        internal static extern GALILEO_RETURN_CODE KeepConnection(IntPtr sdk, bool flag, int maxRetry);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetRetryCount")]
        internal static extern int GetRetryCount(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetServersOnline")]
        internal static extern void GetServersOnline(IntPtr sdk, byte[] servers, ref long length);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetCurrentServer")]
        internal static extern void GetCurrentServer(IntPtr sdk, byte[] servers, ref long length);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PublishTest")]
        internal static extern GALILEO_RETURN_CODE PublishTest(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetInstance")]
        internal static extern IntPtr GetInstance();

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SendCMD")]
        internal static extern GALILEO_RETURN_CODE SendCMD(IntPtr sdk, byte[] cmd, long length);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "StartNav")]
        internal static extern GALILEO_RETURN_CODE StartNav(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "StopNav")]
        internal static extern GALILEO_RETURN_CODE StopNav(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetGoal")]
        internal static extern GALILEO_RETURN_CODE SetGoal(IntPtr sdk, int goal);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "PauseGoal")]
        internal static extern GALILEO_RETURN_CODE PauseGoal(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ResumeGoal")]
        internal static extern GALILEO_RETURN_CODE ResumeGoal(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CancelGoal")]
        internal static extern GALILEO_RETURN_CODE CancelGoal(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "InsertGoal")]
        internal static extern GALILEO_RETURN_CODE InsertGoal(IntPtr sdk, float x, float y);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ResetGoal")]
        internal static extern GALILEO_RETURN_CODE ResetGoal(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetSpeed")]
        internal static extern GALILEO_RETURN_CODE SetSpeed(IntPtr sdk, float vLinear, float vAngle);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "Shutdown")]
        internal static extern GALILEO_RETURN_CODE Shutdown(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetAngle")]
        internal static extern GALILEO_RETURN_CODE SetAngle(IntPtr sdk, byte sign, byte angle);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "StartLoop")]
        internal static extern GALILEO_RETURN_CODE StartLoop(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "StopLoop")]
        internal static extern GALILEO_RETURN_CODE StopLoop(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetLoopWaitTime")]
        internal static extern GALILEO_RETURN_CODE SetLoopWaitTime(IntPtr sdk, byte time);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "StartMapping")]
        internal static extern GALILEO_RETURN_CODE StartMapping(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "StopMapping")]
        internal static extern GALILEO_RETURN_CODE StopMapping(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SaveMap")]
        internal static extern GALILEO_RETURN_CODE SaveMap(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "UpdateMap")]
        internal static extern GALILEO_RETURN_CODE UpdateMap(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "StartChargeLocal")]
        internal static extern GALILEO_RETURN_CODE StartChargeLocal(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "StopChargeLocal")]
        internal static extern GALILEO_RETURN_CODE stopChargeLocal(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SaveChargeBasePosition")]
        internal static extern GALILEO_RETURN_CODE SaveChargeBasePosition(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "StartCharge")]
        internal static extern GALILEO_RETURN_CODE StartCharge(IntPtr sdk, float x, float y);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "StopCharge")]
        internal static extern GALILEO_RETURN_CODE StopCharge(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "MoveTo")]
        internal static extern GALILEO_RETURN_CODE MoveTo(IntPtr sdk, float x, float y, ref byte goalNum);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetGoalNum")]
        internal static extern GALILEO_RETURN_CODE GetGoalNum(IntPtr sdk, ref byte goalNum);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetCurrentStatus")]
        internal static extern GALILEO_RETURN_CODE GetCurrentStatus(IntPtr sdk, byte[] statusJson, ref long length);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetCurrentStatusCallback")]
        internal static extern GALILEO_RETURN_CODE SetCurrentStatusCallback(IntPtr sdk, StatusUpdatedDelegate statusUpdate);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetGoalReachedCallback")]
        internal static extern GALILEO_RETURN_CODE SetGoalReachedCallback(IntPtr sdk, GoalReachedDelegate statusUpdate);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WaitForGoal")]
        internal static extern GALILEO_RETURN_CODE WaitForGoal(IntPtr sdk, byte goalID);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SendAudio")]
        internal static extern GALILEO_RETURN_CODE SendAudio(IntPtr sdk, byte[] audio, long length);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SendRawAudio")]
        internal static extern GALILEO_RETURN_CODE SendRawAudio(IntPtr sdk, byte[] audio, long length);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "EnableGreeting")]
        internal static extern GALILEO_RETURN_CODE EnableGreeting(IntPtr sdk, bool flag);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CheckServerOnline")]
        internal static extern GALILEO_RETURN_CODE CheckServerOnline(IntPtr sdk, byte[] targetID, long length);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "IsConnecting")]
        internal static extern bool IsConnecting(IntPtr sdk);

        [DllImport(dll, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SendGalileoBridgeRequest")]
        internal static extern GALILEO_RETURN_CODE SendGalileoBridgeRequest(IntPtr sdk, byte[] method, long length1,
            byte[] url, long length2,
            byte[] body, long length3,
            byte[] response, ref long length4, int timeout);
    }
}
