# 一.使用方法
1.全局刷新DoozyUI，项目无报错 <br>
2.导入本插件

<br>

# 二.UITip系统
使用方法: 
- 导入本Sample
- 实体需添加包裹住自身的Collider.

<br>

~~~
//一直显示Tip
transform.ShowEntityTip("测试", transform.GetChild(0));

//隐藏Tip
transform.HideEntityTip();

//给实体添加触发事件,达到触发条件时自动显示/隐藏
//若之前是一直显示Tip,也可通过该方法改变其触发类型.
transform.AddAndRefreshEntityTip("测试", transform.GetChild(0));
~~~


