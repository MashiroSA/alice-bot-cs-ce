﻿using alice_bot_cs.Core; // 核心
using alice_bot_cs.Entity; // 实体类
using alice_bot_cs.Extensions; // 扩展
// 插件
using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Data.SQLite;
using System.IO;
using System.Threading.Tasks;
using alice_bot_cs.Entity.Core;
using alice_bot_cs.Plugins;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace alice_bot_cs
{
    public static class Program
    {
        // Connect to Local SQLite database and keep an static address for other class or method to use.
        public static SQLiteCommand Cmd = new SQLiteCommand();
        
        /// <summary>
        /// Main method for bot action using async.
        /// </summary>
        /// <returns>status</returns>
        public static async Task Main()
        {
            Init init = new Init(); // 初始化参数文件

            /*
             * 下列是对coreconfig反序列化读取各项配置
             * todo:对相关的coreconfig进行重写 @author MashiroSA 
             */
            var deserializer = new DeserializerBuilder().WithNamingConvention(UnderscoredNamingConvention.Instance).Build();
            string s = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"/config/CoreConfig.yaml");
            var c = deserializer.Deserialize<CoreConfig>(s);

            string ip = c.ip; // ip
            int port = int.Parse(c.port); // 端口
            string authkey = c.authkey; // http authkey
            long qq = long.Parse(c.account); // 账号

            MiraiHttpSessionOptions options = new MiraiHttpSessionOptions(ip, port, authkey); //MiraiQQ Http
            await using MiraiHttpSession session = new MiraiHttpSession();

            /*
             * 插件装载
             */
            GoodMorningAndNight goodMoringAndNight = new GoodMorningAndNight(); // 早晚安插件
            GroupEventNotice groupEventNotice = new GroupEventNotice(); // 事件通知插件
            RandomSetu randomSetu = new RandomSetu();
            BotBehaviourControl botBehaviourControl = new BotBehaviourControl(qq); // 机器人行为控制，记得传入机器人QQ号
            RandomCat randomCat = new RandomCat();
            NBTalk nBTalk = new NBTalk();
            OsuOsusig osuOsusig = new OsuOsusig();
            RandomTouhouPic randomTouhouPic = new RandomTouhouPic();

            session.AddPlugin(goodMoringAndNight);
            TraceLog.Log("", "插件装载:早晚安插件装载成功");
            session.AddPlugin(groupEventNotice);
            TraceLog.Log("", "插件装载:事件通知插件装载成功");
            session.AddPlugin(randomSetu);
            TraceLog.Log("", "插件装载:色图插件装载成功");
            session.AddPlugin(botBehaviourControl);
            TraceLog.Log("", "插件装载:机器人行为处理插件装载成功");
            session.AddPlugin(randomCat);
            TraceLog.Log("", "插件装载:随机猫猫插件装载成功");
            session.AddPlugin(nBTalk);
            TraceLog.Log("", "插件装载:随机NB话插件装载成功");
            session.AddPlugin(osuOsusig);
            TraceLog.Log("", "插件装载:OSUSIG插件装载成功");
            session.AddPlugin(randomTouhouPic);
            TraceLog.Log("", "插件装载:随机东方图片插件装载成功");

            TraceLog.Log("", "插件装载成功，Alice已经启动");

            /*
             * QQ
             */
            await session.ConnectAsync(options, qq); // QQ号
            while (true)
            {
                if (await Console.In.ReadLineAsync() == "exit")
                {
                    return;
                }
            }
        }
    }
}