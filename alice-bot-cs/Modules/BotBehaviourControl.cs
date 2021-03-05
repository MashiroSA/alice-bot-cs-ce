﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using alice_bot_cs.Entity;
using alice_bot_cs.Extensions;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace alice_bot_cs.Modules
{
    public partial class BotBehaviourControl : IBotInvitedJoinGroup, INewFriendApply, IGroupMessage
    {
        /// <summary>
        /// 机器人行为控制插件
        /// </summary>
        private string help;
        private string list;
        private string info;
        private long botqq;

        public BotBehaviourControl() // 无参数构造方法，为方便调试，未设置private
        {
        }

        public BotBehaviourControl(long botqq) // 有参数构造方法
        {
            this.botqq = botqq;
        }
        /*
         * 下列是对机器人的加群和添加好友的行为控制功能实现
         */
        public async Task<bool> BotInvitedJoinGroup(MiraiHttpSession session, IBotInvitedJoinGroupEventArgs e)
        {
            bool flag = GroupRequestChecker();
            if (flag)
            {
                await session.HandleBotInvitedJoinGroupAsync(e, GroupApplyActions.Allow);
            }
            else
            {
                await session.HandleGroupApplyAsync(e, GroupApplyActions.Deny, "根据相关的设置，还不能将机器人拉入群内哦！QAQ");
            }
            LogExtension.Log("", "来自群 " + e.FromGroup + " 的 " + e.NickName + "，QQ为 " + e.FromQQ + "，发送了拉群请求给机器人，处理情况：" + flag);
            return false;
        }

        public async Task<bool> NewFriendApply(MiraiHttpSession session, INewFriendApplyEventArgs e)
        {
            bool flag = FriendRequestChecker();
            if (flag)
            {
                await session.HandleNewFriendApplyAsync(e, FriendApplyAction.Allow);
            }
            else
            {
                await session.HandleNewFriendApplyAsync(e, FriendApplyAction.Deny, "根据相关的设置，还不能添加机器人为好友哦！QAQ");
            }
            LogExtension.Log("", "来自群 " + e.FromGroup + " 的 " + e.NickName + "，QQ为 " + e.FromQQ + "，发送了添加好友请求给机器人，处理情况：" + flag);
            return false;
        }

        private bool GroupRequestChecker()
        {
            LogExtension.Log("", "收到组邀请检查请求");
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            string s = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"/config/BotBehaviourConfig.yaml");
            var c = deserializer.Deserialize<BotBehaviourConfig>(s);
            if(c.request.groupRequest.ToLower().Equals("t"))
            {
                return true;
            }else if (c.request.groupRequest.ToLower().Equals("f"))
            {
                return false;
            }
            else
            {
                return false;
            }
        }

        private bool FriendRequestChecker()
        {
            LogExtension.Log("", "收到好友邀请检查请求");
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            string s = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"/config/BotBehaviourConfig.yaml");
            var c = deserializer.Deserialize<BotBehaviourConfig>(s);
            if (c.request.groupRequest.ToLower().Equals("t"))
            {
                return true;
            }
            else if (c.request.groupRequest.ToLower().Equals("f"))
            {
                return false;
            }
            else
            {
                return false;
            }
        }
        /*
         * 下列是对机器人的指令菜单的控制
         * todo:需要改进，可能需要使用图片发送指令，或对机器人进行排除 @author MashiroSA 
         */
        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            
            string str = string.Join(null, (IEnumerable<IMessageBase>)e.Chain);
            if(e.Sender.Id != botqq)
            {
                if (str.Contains(".help"))
                {
                    BotBehaviourConfigMenuTrans();
                    IMessageBase plainMenuHelp = new PlainMessage(this.help);
                    await session.SendGroupMessageAsync(e.Sender.Group.Id, plainMenuHelp);
                }
                if (str.Contains(".list"))
                {
                    BotBehaviourConfigMenuTrans();
                    IMessageBase plainMenuList = new PlainMessage(this.list);
                    await session.SendGroupMessageAsync(e.Sender.Group.Id, plainMenuList);
                }
                if (str.Contains(".info"))
                {
                    BotBehaviourConfigMenuTrans();
                    IMessageBase plainMenuInfo = new PlainMessage(this.info);
                    await session.SendGroupMessageAsync(e.Sender.Group.Id, plainMenuInfo);
                }
            }
            else
            {
                return true;
            }
            return false; 
        }

        private void BotBehaviourConfigMenuTrans()
        {
            LogExtension.Log("", "菜单检查事件被触发");
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            string s = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"/config/BotBehaviourConfig.yaml");
            var c = deserializer.Deserialize<BotBehaviourConfig>(s);
            this.help = c.menu.help;
            this.info = c.menu.info;
            this.list = c.menu.list;
        }
    }
}
