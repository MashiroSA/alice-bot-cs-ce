# alice-bot-cs-ce
A simple QQ bot running with MiraiHttp and MiraiCSharp & 一个基于MiraiHttp以及MiraiCSharp构建的基于C#编写的QQ机器人

## 概述
- `AliceBot`是继`Urara`后由很菜很菜的`MashiroSA`发起的一个自研QQ机器人项目，使用`c#`编写，基于`Mirai(Mirai Core、Mirai Console、Mirai Http、Mirai CSharp)`。目前正在由`MeowCatZ`团队开发中
- `AliceBot`是运行在非盈利的基础上，一切行为仅供交流学习
- 该repo的`AliceBot`为社区发行版，开源构建，请勿贩售代码，不建议盈利行为
- 请注意，该bot依赖MiraiHttp作为中间件，版本为1.x，当前切勿使用2.x，sdk尚未适配
> AliceBot需要借助Mirai套件下运行，且其为一个独立的机器人

## 工程文件
- `Core`：核心功能类
- `Entity`：实体类，用于实例化参数
- `Extensions`：与Modules耦合的外部功能
- `Plugins`：机器人插件
- `Tools`：机器人可能会使用到的非核心的外部功能
- `Program.cs`：程序入口点
> 请注意设置.gitignore来排除.vs与.git参数文件以及/obj与/bin等输出文件夹

## 分支信息
| 分支名 | 功能 |
| ---- | ---- |
| master | 主分支 |
| develop | 开发分支 |
| debug | 测试分支，用以测试开发分支将要加入的代码 | 

## 核心功能
| 功能名 | 命令 | 作用 |
| ---- | ---- | ---- |
|  | .help | 查看机器人详细功能 |

## 关联项目
- [Mirai](https://github.com/mamoe/mirai)
- [Mirai Console](https://github.com/mamoe/mirai-console)
- [mirai-api-http](https://github.com/project-mirai/mirai-api-http)
- [Mirai CSharp](https://github.com/Executor-Cheng/Mirai-CSharp)
- [Lolicon API](https://api.lolicon.app/)
- [Elbot](https://github.com/YunYouJun/el-bot)

## 注意事项
- 一切仅供交流学习
- 请使用c# >= 8.0建构项目
- 请使用`develop`分支进行开发，`master`为稳定运行分支，请勿直接推送`master`分支commit
