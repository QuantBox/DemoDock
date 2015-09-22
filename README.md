# DemoDock示例

##介绍
OpenQuant的界面功能比较简单，不能满足部分用户的特殊需求。而向国外官方反馈加功能由于沟通不畅，理解偏差，基本上不可能满足中国区的需求。所以我们提供一个示例，指导大家如何开发内置的Dock界面。<br/>
示例由两个项目组成

1. MenuLoader:预先加载，然后根据配置文件生成相应的菜单项
2. DemoDock:自己定义的Dock窗体，演示运行策略时输出对应合约的Trade信息，可直接运行SMACrossover观察效果

## 如何运行
1. 将MenuLoader和DemoDock都编译到OpenQuant2014目录下
2. 运行Install下的两个vbs中的一个或都运行也可，做用是分别向configuration.xml中写入两种菜单加载器，多次写入只会执行一次
3. 复制menu.json文件到与configuration.xml同目录，文件中设置的是DemoDock中某菜单加载项的类型信息供菜单加载器使用，当有新菜单项需要添加时编辑menu.json

## 如何自开发Dock
1. 创建一个Class Library项目，设置成.NET 4.5.1
2. 创建一个Dock窗体,至少要创建两个UserControl,可通过VS中的向导创建
3. 分别要修改成继承于DockWindow和FrameworkControl
4. 注意修改成继承于DockWindow后，对应的设计器无法使用，建议先做好必要的设计后再改。继承于FrameworkControl的不受影响
5. DockWindow子类中base.Control一定要设置，否测出现空引用错误
6. OpenQuant下除了SmartQuant.dll混淆过，其它界面部分都没有混淆，可以学习参考界面功能如何实现

## 发展
1. 鼓励大家进行界面开发、交流、开源。
2. 鼓励大家通过各种方式获得收益，如销售、有偿技术支持、捐助、赞助等。
