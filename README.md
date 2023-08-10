# 专业 Unity 资产系统

xasset 提供了全面可靠的 Unity 资产打包、分包、加密、热更和加载等技术解决方案。借助 xasset，可以让一些 Unity
项目的打包慢、包体大和运行卡顿等问题解决地更轻松。

- 产品官网：https://xasset.cc
- GitHub：https://github.com/xasset/xasset

了解更多 xasset 的产品特性，请前往 [xasset.cc](https://xasset.cc) 查看。

## 推荐服务：使用 UOS CDN 进行资源云端管理

由 xasset 团队和 Unity
团队联手打造的快速资源分发流程和工具，开发阶段可以免费使用。[>>> 前往体验](https://uos.unity.cn/partner/xasset)

<img src="https://uos.unity.cn/images/homepage/xasset-hp.png" alt="xasset-uos" style="zoom:50%;" />

> 提示：使用 UOS CDN 的时候，可以把 Bundles 和 updateinfo.json 放到不同的存储桶。Bundles 下的数据无需使用 Badge
> 来处理内容分发，updateinfo.json 可以用 Badge 来处理开发测试或线上正式环境的内容分发。

## 官方新闻

- 2023.04.24：[xasset 2023.1预览版发布，除了资产热重载外还有哪些改变？](https://mp.weixin.qq.com/s/H2HDtwnp1mG_F4v1TahVJg)
- 2023.03.27：[近10倍IO性能优化的过程和原理](https://mp.weixin.qq.com/s/X0Tc6-UKVqfEXrzSEY17Zw)

## 用法

### 打包资源

1、使用 `Assets>xasset>Create>Build` 创建打包配置。

2、根据游戏的生命周期节点在 Build 配置中添加 Build Group。

3、使用 `xasset>Generate Group Assets Menu Items` 为创建的 Build 配置中的 Groups 生成 Assets 菜单。

4、使用 3 生成的菜单 `Assets>Group To`，在 Unity 的 Project 中选中资产文件或文件夹添加到对应的 Group 中。

5、使用 `xasset>Build Bundles` 对 Build 配置中包含的资产进行打包。

### 运行示例

#### 一、不打包运行

1、勾选 `xasset>Play Mode>Fast Play Without Build`。

2、打开 Startup 场景让 Unity 进入播放模式。

#### 二、打包运行不更新

1、使用 `xasset>Build Bundles` 打包示例的资产(如果已经打包过可以跳过)

2、勾选 `xasset>Play Mode>Play Without Update`。

3、打开 Startup 场景让 Unity 进入播放模式。

#### 三、打包运行并开启更新使用仿真下载

1、使用 `xasset>Build Bundles` 打包示例的资产。(如果已经打包过可以跳过)

2、勾选 `xasset>Play Mode>Play With Update By Simulation`。

3、使用  `xasset>Build Player Assets` 构建安装包资产。选中一个 versions.json。 (如果已经打包过可以跳过)

4、打开 Startup 场景让 Unity 进入播放模式。

#### 四、打包运行并开启更新使用真机模式（从CDN下载打包后的资产）

1、使用 `xasset>Build Bundles` 打包示例的资产。(如果已经打包过可以跳过)

2、勾选 `xasset>Play Mode>Play With Update By Realtime`。

3、在 Settings 中配置好 CDN 的地址，并把打包后的资产部署到 CDN。

4、使用  `xasset>Build Player Assets` 构建安装包资产。选中一个 versions.json。 (如果已经打包过可以跳过)

5、打开 Startup 场景让 Unity 进入播放模式。

## 版本

阅读 [技术规格](https://xasset.cc/price#技术规格) 可以了解不同版本之间的差异。

## 文档

前往 [xasset.cc](https://xasset.cc) 可以了解 xasset 的来龙去脉。

- 2022.1：https://xasset.cc/docs/2022.1/about
- 2022.2：https://xasset.cc/docs/getting-started
- 2023.1：待完成

## 许可

请注意，xasset 具有特殊许可证，并且在某些情况下需要获得公司许可证。阅读 [许可](LICENSE.md) 文档以获取更多信息。

## 创作者

xasset 的主要创作者是：

- [吉缘](https://github.com/mmdnb)
- [马三小伙儿](https://github.com/XINCGer)
- [寒晟](https://github.com/huangchaoqun)

另外，xasset 的广大用户也提供了不少有价值的参考，感谢大家的反馈和建议！我们会继续努力，为大家提供更好的产品和服务。

## 赞助商

成为 xasset 的赞助商可以在这里添加自己的链接，可以带图标或名字：

- Miss_Lynn(人民币50000元)
- mingjava(人民币5000元)
- [马三小伙儿](https://github.com/XINCGer)（人民币4800元）
- 花花 （人民币3848元）
- [Jojohello](https://www.zhihu.com/people/jojohello)（人民币3000元）

赞助商提供的资金将用来扶持更多有创造力的创作，期待未来有更多有潜力的产品的出现。

## 贡献者

为 xasset 提供好的建议或反馈，可以成为 xasset 的贡献者：

- [EasierLu](https://github.com/EasierLu) +10 点声望
- [Connor Aaron Roberts](https://github.com/c0nd3v) +10 点声望
- [xxgamecom](xxgamecom) +10 点声望
- [roki007](https://github.com/roki007) +5 声望
- [Contra]() +5 点声望
- [郑昊](https://github.com/plussign) +5 点声望
- [MR.汪](https://github.com/youyouhx) +5 点声望
- [刘家君](https://github.com/suixin567) +2 点声望
- [李非莬](https://github.com/wynnforthework)  +2 点声望
- [一念永恆](https://github.com/putifeng) +2 点声望
- [小魔女纱代酱](https://github.com/DumoeDss)  +2 点声望
- [夜莺](https://github.com/killop) +5 点声望
- [MrJLY](https://github.com/MrJLY) +5 点声望
- [Leo](https://github.com/liyanlong0885) +5 点声望
- [jakeyluo](https://github.com/jakeyluo) +5 点声望

成为贡献者可以获得一定的声望，声望可以折现，声望和人民币的兑换比例是 1：20，也就是 1 点声望可以兑换 20 元人民币。

## 推荐框架

- [ET](https://github.com/egametang/ET) Unity3D Client And C# Server Framework
- [TinaX Framework](https://tinax.corala.space/) 开箱即用的Unity独立游戏开发工具
- [LuaProfiler-For-Unity](https://github.com/ElPsyCongree/LuaProfiler-For-Unity) Lua Profiler For Unity支持
  XLua、SLua、ToLua
