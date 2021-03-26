using System;
using System.IO;
using System.Threading;
using alice_bot_cs.Core;
using alice_bot_cs.Tools;

namespace alice_bot_cs.Extensions.Osu
{
    public class OsuOussigLoliconExtension
    {
        private string _username = "";
        private string _dataPath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "data/OsusigPic");
        private string _dataFile = "";

        public string GetOsuSig(string username)
        {
            this._username = username;
            bool flag = DownloadOusSigPic();
            TraceLog.Log("", "OSUSIG:调用下载返回了:" + flag); 
            return _dataFile;
        }

        private bool DownloadOusSigPic()
        {
            string color = "black";
            Random rd = new Random();
            int n = rd.Next(1, 5);
            switch (n)
            {
                case 1:
                    color = "yellow";
                    break;
                case 2:
                    color = "green";
                    break;
                case 3:
                    color = "blue";
                    break;
                case 4:
                    color = "pink";
                    break;
                case 5:
                    color = "purple";
                    break;
                default:
                    color = "black";
                    break;
            }

            TraceLog.Log("", $"OSUSIG:将调用OSU个人资料，{_username}");
            string api = $"https://osusig.lolicon.app/sig.php?colour={color}&uname={_username}&pp=1&countryrank&removeavmargin&rankedscore&xpbar";
            _dataFile = Path.Combine(_dataPath, _username + ".png");
            byte[] pic = HttpTool.GetBytesFromUrl(api);
            HttpTool.WriteBytesToFile(_dataFile, _dataPath, pic);
            return true;
        }
    }
}