using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SharpGalileo.models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using static SharpGalileo.GalileoDelegates;

namespace SharpGalileo
{
    public class GalileoSDK : IDisposable
    {
        private IntPtr instance;
<<<<<<< Updated upstream
        private bool _disposed = false;
        OnConnectDelegate onConnectCB = null;
        OnDisconnectDelegate onDisconnectCB = null;
=======
>>>>>>> Stashed changes

        public GalileoSDK()
        {
            instance = GalileoFunctions.CreateInstance();
        }

        ~GalileoSDK()
        {
            Release();
        }
        
        public void Dispose()
        {
            if (instance != null)
                GalileoFunctions.Dispose(instance);
        }

        public void Release()
        {
            if (instance != null)
                GalileoFunctions.ReleaseInstance(instance);
        }


        public GALILEO_RETURN_CODE Connect(String targetID, bool autoConnect, int timeout, Action<GALILEO_RETURN_CODE, String> onConnect = null, Action<GALILEO_RETURN_CODE, String> onDisconnect = null)
        {
            byte[] targetIDBytes = Encoding.ASCII.GetBytes(targetID);
            if (onConnect != null)
            {
                onConnectCB = (status, id, length) =>
                {
                    byte[] result = new byte[length];
                    Marshal.Copy(id, result, 0, (int)length);
                    onConnect?.Invoke(status, Encoding.ASCII.GetString(result, 0, (int)length));
                };
            }
            if(onDisconnect != null)
            {
                onDisconnectCB = (status, id, length) =>
                {
                    byte[] result = new byte[length];
                    Marshal.Copy(id, result, 0, (int)length);
                    onDisconnect?.Invoke(status, Encoding.ASCII.GetString(result, 0, (int)length));
                };
            }
            return GalileoFunctions.Connect(instance, Encoding.ASCII.GetBytes(targetID), targetIDBytes.Length, autoConnect, timeout, onConnectCB, onDisconnectCB);
        }

        public GALILEO_RETURN_CODE ConnectIOT(string targetID, int timeout, string password, Action<GALILEO_RETURN_CODE, String> onConnect = null, Action<GALILEO_RETURN_CODE, String> onDisconnect = null)
        {
            byte[] targetIDBytes = Encoding.ASCII.GetBytes(targetID);
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            OnConnectDelegate onConnectCB = null;
            OnDisconnectDelegate onDisconnectCB = null;
            if (onConnect != null)
            {
                onConnectCB = (status, id, length) =>
                {
                    byte[] result = new byte[length];
                    Marshal.Copy(id, result, 0, (int)length);
                    onConnect?.Invoke(status, Encoding.ASCII.GetString(result, 0, (int)length));
                };
            }
            if (onDisconnect != null)
            {
                onDisconnectCB = (status, id, length) =>
                {
                    byte[] result = new byte[length];
                    Marshal.Copy(id, result, 0, (int)length);
                    onDisconnect?.Invoke(status, Encoding.ASCII.GetString(result, 0, (int)length));
                };
            }
            return GalileoFunctions.ConnectIOT(instance, targetIDBytes, targetIDBytes.Length, timeout, passwordBytes, passwordBytes.Length, onConnectCB, onDisconnectCB);
        }

        public List<ServerInfo> GetServersOnline()
        {
            byte[] servers = new byte[1024 * 1024];
            long length = 0;
            GalileoFunctions.GetServersOnline(instance, servers, ref length);
            string serversJsonString = Encoding.UTF8.GetString(servers, 0, (int)length);
            if (serversJsonString == "null")
                return new List<ServerInfo>();
            JArray serversJson = JArray.Parse(serversJsonString);
            List<ServerInfo> serversObj = new List<ServerInfo>(); ;
            foreach (var server in serversJson)
            {
                serversObj.Add(server.ToObject<ServerInfo>());
            }
            return serversObj;
        }


        public ServerInfo GetCurrentServer()
        {
            byte[] currentServer = new byte[1024 * 1024];
            long length = 0;
            GalileoFunctions.GetCurrentServer(instance, currentServer, ref length);
            string currentServerJsonString = Encoding.UTF8.GetString(currentServer, 0, (int)length);
            return JsonConvert.DeserializeObject<ServerInfo>(currentServerJsonString);
        }

        public GALILEO_RETURN_CODE PublishTest()
        {
            return GalileoFunctions.PublishTest(instance);
        }

        public GALILEO_RETURN_CODE SendCMD(byte[] data)
        {
            return GalileoFunctions.SendCMD(instance, data, data.Length);
        }

        public GALILEO_RETURN_CODE StartNav()
        {
            return GalileoFunctions.StartNav(instance);
        }

        public GALILEO_RETURN_CODE StopNav()
        {
            return GalileoFunctions.StopNav(instance);
        }

        public GALILEO_RETURN_CODE SetGoal(int goalIndex)
        {
            return GalileoFunctions.SetGoal(instance, goalIndex);
        }

        public GALILEO_RETURN_CODE PauseGoal()
        {
            return GalileoFunctions.PauseGoal(instance);
        }

        public GALILEO_RETURN_CODE ResumeGoal()
        {
            return GalileoFunctions.ResumeGoal(instance);
        }

        public GALILEO_RETURN_CODE CancelGoal()
        {
            return GalileoFunctions.CancelGoal(instance);
        }

        public GALILEO_RETURN_CODE InsertGoal(float x, float y)
        {
            return GalileoFunctions.InsertGoal(instance, x, y);
        }

        public GALILEO_RETURN_CODE ResetGoal()
        {
            return GalileoFunctions.ResetGoal(instance);
        }

        public GALILEO_RETURN_CODE SetSpeed(float vLinear, float xAngle)
        {
            return GalileoFunctions.SetSpeed(instance, vLinear, xAngle);
        }

        public GALILEO_RETURN_CODE Shutdown()
        {
            return GalileoFunctions.Shutdown(instance);
        }

        public GALILEO_RETURN_CODE SetAngle(byte sign, byte angle)
        {
            return GalileoFunctions.SetAngle(instance, sign, angle);
        }

        public GALILEO_RETURN_CODE StartLoop()
        {
            return GalileoFunctions.StartLoop(instance);
        }

        public GALILEO_RETURN_CODE StopLoop()
        {
            return GalileoFunctions.StopLoop(instance);
        }

        public GALILEO_RETURN_CODE SetLoopWaitTime(byte time)
        {
            return GalileoFunctions.SetLoopWaitTime(instance, time);
        }

        public GALILEO_RETURN_CODE StartMapping()
        {
            return GalileoFunctions.StartMapping(instance);
        }

        public GALILEO_RETURN_CODE StopMapping()
        {
            return GalileoFunctions.StopMapping(instance);
        }

        public GALILEO_RETURN_CODE SaveMap()
        {
            return GalileoFunctions.SaveMap(instance);
        }

        public GALILEO_RETURN_CODE UpdateMap()
        {
            return GalileoFunctions.UpdateMap(instance);
        }

        public GALILEO_RETURN_CODE StartChargeLocal()
        {
            return GalileoFunctions.StartChargeLocal(instance);
        }

        public GALILEO_RETURN_CODE StopChargeLocal()
        {
            return GalileoFunctions.stopChargeLocal(instance);
        }

        public GALILEO_RETURN_CODE SaveChargeBasePosition()
        {
            return GalileoFunctions.SaveChargeBasePosition(instance);
        }

        public GALILEO_RETURN_CODE StartCharge(float x, float y)
        {
            return GalileoFunctions.StartCharge(instance, x, y);
        }

        public GALILEO_RETURN_CODE StopCharge()
        {
            return GalileoFunctions.StopCharge(instance);
        }

        public GALILEO_RETURN_CODE MoveTo(float x, float y, ref byte goalNum)
        {
            return GalileoFunctions.MoveTo(instance, x, y, ref goalNum);
        }

        public GALILEO_RETURN_CODE GetGoalNum(ref byte goalNum)
        {
            return GalileoFunctions.GetGoalNum(instance, ref goalNum);
        }

        public GalileoStatus GetCurrentStatus()
        {
            byte[] currentStatus = new byte[1024 * 1024];
            long length = 0;
            GalileoFunctions.GetCurrentStatus(instance, currentStatus, ref length);
            string currentStatusJsonString = Encoding.UTF8.GetString(currentStatus, 0, (int)length);
            return JsonConvert.DeserializeObject<GalileoStatus>(currentStatusJsonString);
        }

        public void SetCurrentStatusCallback(Action<GalileoStatus> statusCB = null)
        {
            GalileoFunctions.SetCurrentStatusCallback(instance, (status, statusJson, length)=> {
                byte[] result = new byte[length];
                Marshal.Copy(statusJson, result, 0, (int)length);
                var statusStr = Encoding.ASCII.GetString(result, 0, (int)length);
                statusCB?.Invoke(JsonConvert.DeserializeObject<GalileoStatus>(statusStr));
            });
        }

        public void SetGoalReachedCallback(Action<int, GalileoStatus> goalCB)
        {
            GalileoFunctions.SetGoalReachedCallback(instance, (goalid, statusJson, length) =>{
                byte[] result = new byte[length];
                Marshal.Copy(statusJson, result, 0, (int)length);
                var statusStr = Encoding.ASCII.GetString(result, 0, (int)length);
                goalCB?.Invoke(goalid, JsonConvert.DeserializeObject<GalileoStatus>(statusStr));
            });
        }

        public void WaitForGoal(byte goalIndex)
        {
            GalileoFunctions.WaitForGoal(instance, goalIndex);
        }

        public GALILEO_RETURN_CODE SendAudio(string audio)
        {
            var audioBytes = Encoding.UTF8.GetBytes(audio);
            return GalileoFunctions.SendAudio(instance, audioBytes, audioBytes.Length);
        }

        public GALILEO_RETURN_CODE SendRawAudio(byte[] data) {
            return GalileoFunctions.SendRawAudio(instance, data, data.Length);
        }

        public GALILEO_RETURN_CODE EnableGreeting(bool flag)
        {
            return GalileoFunctions.EnableGreeting(instance, flag);
        }

        public GALILEO_RETURN_CODE CheckServerOnline(string targetID)
        {
            var targetIDBytes = Encoding.UTF8.GetBytes(targetID);
            return GalileoFunctions.CheckServerOnline(instance, targetIDBytes, targetIDBytes.Length);
        }
    }
}
