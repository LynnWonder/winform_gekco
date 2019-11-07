#  .net winform程序下使用firefox作为Web浏览器
##tips:
1. 添加引用：Geckofx-Core、Geckofx-Winforms；
2. 把xulrunner放在项目的bin目录下；
3. 如果运行程序报错：“加载程序集错误”
**解决**：将Debug设置的目标平台改为“x86”
**注意**：
- geckofx的版本要和xulrunner的版本对应，不然会有问题。xulrunner是firefox官网提供的。
- 若需添加引用：ComponentFactory.Krypton.Toolkit.dll
（需要先安装KryptonSuite440.msi，安装后会在安装目录下找到该dll)
- html+js代码放在项目的bin/debug下
