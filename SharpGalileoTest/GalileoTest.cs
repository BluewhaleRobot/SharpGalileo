using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpGalileo;
using SharpGalileo.models;

namespace SharpGalileoTest
{
    [TestClass]
    public class GalileoTest
    {

        [TestMethod]
        public void testConnectWithCallback()
        {
            GalileoSDK sdk = new GalileoSDK();
            bool cbFlag = false;
            sdk.Connect("F9DF41E6CA1C41CD8ECB510C3EF84A4472191922695EBA5A7514D459FC919608A2EF4FB50622", true, 10000, (status, targetID) =>
            {
                Console.WriteLine("Server connected");
                Console.WriteLine(targetID);
                Console.WriteLine("status: " + status);
                cbFlag = true;
            }, (status, targetID) =>
            {
                Console.WriteLine("Server disconnected");
                Console.WriteLine(targetID);
                cbFlag = true;
            });
            while (!cbFlag)
                Thread.Sleep(1000);
        }

        [TestMethod]
        public void testGetServersOnline()
        {
            GalileoSDK sdk = new GalileoSDK();
            List<ServerInfo> servers = sdk.GetServersOnline();
            foreach (var server in servers)
            {
                Console.WriteLine("server: " + server.ID);
            }
            Thread.Sleep(3000);
            servers = sdk.GetServersOnline();
            Assert.AreNotEqual(servers.Count, 0);
        }

        [TestMethod]
        void testPub()
        {
            GalileoSDK sdk = new GalileoSDK();
            while (true)
            {
                var servers = sdk.GetServersOnline();
                if (servers.Count == 0)
                {
                    Console.WriteLine("No server found");
                }
                foreach (var server in servers)
                {
                    Console.WriteLine("Connect to " + server.ID);
                }
                var res = sdk.Connect("", true, 10000, null, null);
                Console.WriteLine(res);
                sdk.PublishTest();
                Thread.Sleep(1000);
            }
        }

        [TestMethod]
        void testSub()
        {
            GalileoSDK sdk = new GalileoSDK();
            sdk.Connect("", true, 10000, null, null);
            while (true)
            {
                GalileoStatus status = sdk.GetCurrentStatus();
                Console.WriteLine("Power: " + status.power);
                Thread.Sleep(1000);
            }
        }

        static bool connectCallbackFlag = false;
        static bool connected = false;

        [TestMethod]
        void testReconnect()
        {
            GalileoSDK sdk = new GalileoSDK();
            while (true)
            {
                if (!connected)
                {
                    sdk.Connect("", true, 10000, (status, id) => {
                        Console.WriteLine("OnConnect Callback: result " + status);
                        Console.WriteLine("OnConnect Callback: connected to " + id);
                        connectCallbackFlag = true;
                        if (status == GALILEO_RETURN_CODE.OK)
                        {
                            connected = true;
                        }
                    }, (status, id) => {
                        Console.WriteLine("OnDisconnect Callback: result " + status);
                        Console.WriteLine("OnDisconnect Callback: connected to " + id);
                        connected = false;
                    });
                    while (!connectCallbackFlag)
                    {
                        Thread.Sleep(1000);
                    }
                    connectCallbackFlag = false;
                }
                Thread.Sleep(1000);
                Console.WriteLine("Waiting");
            }
        }

        [TestMethod]
        void testSendGalileoCmd()
        {
            GalileoSDK sdk = new GalileoSDK();
            if (sdk.Connect("71329A5B0F2D68364BB7B44F3F125531E4C7F5BC3BCE2694DFE39B505FF9C730A614FF2790C1",
                    true, 10000, null,
                    null) != GALILEO_RETURN_CODE.OK)
            {
                Console.WriteLine("Connect to server failed");
            }
            while (true)
            {
                sdk.SendCMD(new byte[] { 0x01, 0x01, 0x01, 0x01, 0x01, 0x01 });
                Thread.Sleep(1000);
            }
        }

        [TestMethod]
        void testStartNav()
        {
            GalileoSDK sdk = new GalileoSDK();
            if (sdk.Connect("71329A5B0F2D68364BB7B44F3F125531E4C7F5BC3BCE2694DFE39B505FF9C730A614FF2790C1",
                    true, 10000, null,
                    null) != GALILEO_RETURN_CODE.OK)
            {
                Console.WriteLine("Connect to server failed");
            }
            while (true)
            {
                GalileoStatus status = sdk.GetCurrentStatus();
                Console.WriteLine("status: " + status.navStatus);
                Console.WriteLine("start nav");
                sdk.StartNav();
                Thread.Sleep(1000 * 20);
                status = sdk.GetCurrentStatus();
                Console.WriteLine("status: " + status.navStatus);
                Console.WriteLine("stop nav");
                sdk.StopNav();
                Thread.Sleep(1000 * 20);
                status = sdk.GetCurrentStatus();
                Console.WriteLine("status : " + status.navStatus);
            }
        }

        [TestMethod]
        void testSetSpeed()
        {
            GalileoSDK sdk = new GalileoSDK();
            if (sdk.Connect("71329A5B0F2D68364BB7B44F3F125531E4C7F5BC3BCE2694DFE39B505FF9C730A614FF2790C1",
                    true, 10000, null,
                    null) != GALILEO_RETURN_CODE.OK)
            {
                Console.WriteLine("Connect to server failed");
            }

            while (true)
            {
                GalileoStatus status = sdk.GetCurrentStatus();
                Console.WriteLine("currentSpeedX: " + status.currentSpeedX);
                Console.WriteLine("currentSpeedTheta: " + status.currentSpeedTheta);
                sdk.SetSpeed(0.1f, 0f);
                Thread.Sleep(5 * 1000);
                status = sdk.GetCurrentStatus();
                Console.WriteLine("currentSpeedX: " + status.currentSpeedX);
                Console.WriteLine("currentSpeedTheta: " + status.currentSpeedTheta);
                sdk.SetSpeed(0f, 1f);
                Thread.Sleep(5 * 1000);
                status = sdk.GetCurrentStatus();
                Console.WriteLine("currentSpeedX: " + status.currentSpeedX);
                Console.WriteLine("currentSpeedTheta: " + status.currentSpeedTheta);
                sdk.SetSpeed(-0.1f, 0f);
                Thread.Sleep(5 * 1000);
                status = sdk.GetCurrentStatus();
                Console.WriteLine("currentSpeedX: " + status.currentSpeedX);
                Console.WriteLine("currentSpeedTheta: " + status.currentSpeedTheta);
                sdk.SetSpeed(0f, -1f);
                Thread.Sleep(5 * 1000);
                status = sdk.GetCurrentStatus();
                Console.WriteLine("currentSpeedX: " + status.currentSpeedX);
                Console.WriteLine("currentSpeedTheta: " + status.currentSpeedTheta);
                sdk.SetSpeed(0f, 0f);
                Thread.Sleep(5 * 1000);
                status = sdk.GetCurrentStatus();
                Console.WriteLine("currentSpeedX: " + status.currentSpeedX);
                Console.WriteLine("currentSpeedTheta: " + status.currentSpeedTheta);

            }
        }

        [TestMethod]
        static void testSetAngle()
        {

        }

        [TestMethod]
        void testGoals()
        {
            GalileoSDK sdk = new GalileoSDK();
            if (sdk.Connect("71329A5B0F2D68364BB7B44F3F125531E4C7F5BC3BCE2694DFE39B505FF9C730A614FF2790C1",
                    true, 10000, null,
                    null) != GALILEO_RETURN_CODE.OK)
            {
                Console.WriteLine("Connect to server failed");
            }
            while (true)
            {
                sdk.StartNav();
                GalileoStatus status = sdk.GetCurrentStatus();
                while (status.visualStatus != 1 || status.navStatus != 1)
                {
                    Console.WriteLine("Wait for navigation initialization");
                    Console.WriteLine("status.visualStatus: " + status.visualStatus);
                    Console.WriteLine("status.navStatus: " + status.navStatus);
                    status = sdk.GetCurrentStatus();
                    Thread.Sleep(1000);
                }
                // 设置目标点
                sdk.SetGoal(0);
                // 等待goal status
                status = sdk.GetCurrentStatus();
                while (status.targetStatus != 1)
                {
                    Console.WriteLine("Wait for goal start");
                    status = sdk.GetCurrentStatus();
                    Thread.Sleep(1000);
                }

                Console.WriteLine("Goal started");
                // 暂停目标
                Thread.Sleep(2 * 1000);
                sdk.PauseGoal();
                status = sdk.GetCurrentStatus();
                while (status.targetStatus != 2)
                {
                    Console.WriteLine("Wait for goal pause");
                    status = sdk.GetCurrentStatus();
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Goal paused");
                // 继续目标
                Thread.Sleep(2 * 1000);
                sdk.ResumeGoal();
                status = sdk.GetCurrentStatus();
                while (status.targetStatus != 1)
                {
                    Console.WriteLine("Wait for goal resume");
                    status = sdk.GetCurrentStatus();
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Goal resumed");
                // 取消目标
                Thread.Sleep(2 * 1000);
                sdk.CancelGoal();
                status = sdk.GetCurrentStatus();
                while (status.targetStatus != 0 || status.targetNumID != -1)
                {
                    Console.WriteLine("Wait for goal cancel");
                    status = sdk.GetCurrentStatus();
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Goal cancelled");
                // 再次设置目标
                Thread.Sleep(2 * 1000);
                Console.WriteLine("Set goal again");
                sdk.SetGoal(0);
                // 完成目标
                status = sdk.GetCurrentStatus();
                while (status.targetStatus != 1)
                {
                    Console.WriteLine("Wait for goal start");
                    status = sdk.GetCurrentStatus();
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Goal started");
                while (status.targetStatus != 0 || status.targetNumID != 0)
                {
                    Console.WriteLine("Wait for goal complete");
                    status = sdk.GetCurrentStatus();
                    Thread.Sleep(1000);
                }
                // 移动到特定目标
                // 获取当前坐标
                status = sdk.GetCurrentStatus();
                float pos0_x = status.currentPosX;
                float pos0_y = status.currentPosY;
                // 再次设置目标 ，移动至1号目标点
                Thread.Sleep(2 * 1000);
                Console.WriteLine("Set goal again");
                sdk.SetGoal(1);
                // 完成目标
                status = sdk.GetCurrentStatus();
                while (status.targetStatus != 1)
                {
                    Console.WriteLine("Wait for goal start");
                    status = sdk.GetCurrentStatus();
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Goal started");
                while (status.targetStatus != 0 || status.targetNumID != 1)
                {
                    Console.WriteLine("Wait for goal complete");
                    status = sdk.GetCurrentStatus();
                    Thread.Sleep(1000);
                }
                // 再次获取坐标
                status = sdk.GetCurrentStatus();
                float pos1_x = status.currentPosX;
                float pos1_y = status.currentPosY;
                // 然后移动至0号和1号之间
                byte goalNum = 0;
                Console.WriteLine("Start move to " + (pos0_x + pos1_x) / 2 + " " + (pos0_y + pos1_y) / 2);
                sdk.MoveTo((pos0_x + pos1_x) / 2, (pos0_y + pos1_y) / 2, ref goalNum);
                // 等待移动完成
                status = sdk.GetCurrentStatus();
                while (status.targetStatus != 1)
                {
                    Console.WriteLine("Wait for goal start");
                    status = sdk.GetCurrentStatus();
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Goal started");
                while (status.targetStatus != 0 || status.targetNumID != goalNum)
                {
                    Console.WriteLine("Wait for goal complete");
                    Console.WriteLine("status.targetStatus: " + status.targetStatus);
                    Console.WriteLine("status.targetNumID: " + status.targetNumID);
                    Console.WriteLine("goalNum: " + goalNum);
                    status = sdk.GetCurrentStatus();
                    Thread.Sleep(1000);
                }
                Console.WriteLine("Move to " + (pos0_x + pos1_x) / 2 + " " + (pos0_y + pos1_y) / 2 + " complete");
                Thread.Sleep(10 * 1000);
                Console.WriteLine("All complete");
            }
        }

        [TestMethod]
        void testAudioIOT()
        {
            GalileoSDK sdk = new GalileoSDK();
            sdk.ConnectIOT("71329A5B0F2D68364BB7B44F3F125531E4C7F5BC3BCE2694DFE39B505FF9C730A614FF2790C1", 10000, "xiaoqiang", null, null);
            while (true)
            {
                var res = sdk.SendAudio("测试");
                if (res == GALILEO_RETURN_CODE.OK)
                {
                    Console.WriteLine("Send audio message succeed");
                }
                else
                {
                    Console.WriteLine("Send audio message failed");
                }

                Thread.Sleep(4000);
            }
        }

        [TestMethod]
        public void testKeepConnection() {
            GalileoSDK sdk = new GalileoSDK();
            sdk.Connect("8FB56D27D6C961E9036F62182ADE9544D71E23C31E5DF4C7DD692B9E4296A131434B1066D365",true, 10000, null, null);
            int count = 0;
            while (count < 30) {
                GalileoStatus status = sdk.GetCurrentStatus();
                if (status != null)
                    Console.WriteLine("Power: " + status.power);
                else
                    Console.WriteLine("Get power failed");
                Thread.Sleep(1000);
                count += 1;
            }
        }
    }
}

