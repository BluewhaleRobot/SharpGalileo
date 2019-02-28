# SharpGalileo [![Build Status](https://travis-ci.org/BluewhaleRobot/SharpGalileo.svg)](https://travis-ci.org/BluewhaleRobot/SharpGalileo)

Galileo navigation C# SDK

## 安装

下载此项目

```bash
git clone https://github.com/bluewhalerobot/SharpGalileo
```

将SharpGalileo项目添加到自己的项目依赖中。对于Windows用户请到[这里](https://github.com/BluewhaleRobot/GalileoSDK/releases)下载GalileoSDK.dll，并放置到项目的项目文件生成目录。对于Linux用户，请参照[这里](https://github.com/BluewhaleRobot/GalileoSDK)安装GalileoSDK。

## 使用例子

```csharp
using System;
using System.Collections.Generic;
using System.Threading;
using SharpGalileo;
using SharpGalileo.models;

namespace SharpGalileoTest
{
    class Program
    {
        static void Main(string[] args)
        {
            GalileoSDK sdk = new GalileoSDK(); // 创建SDK对象
            while (true)
            {
                var servers = sdk.GetServersOnline(); // 获取局域网内的机器人
                if(servers.Count == 0)
                {
                    Console.WriteLine("No server found");
                }
                foreach(var server in servers)
                {
                    Console.WriteLine("Connect to " + server.ID); // 输出机器人ID
                }
                var res = sdk.Connect("", true, 10000, null, null); // 连接机器人
                Console.WriteLine(res);
                sdk.PublishTest(); // 向机器人发送消息，此时机器人应当在/pub_test话题接收到消息
                Thread.Sleep(1000);
            }
            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
```