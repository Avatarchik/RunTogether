using System.Collections.Generic;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using Color = Unity.UIWidgets.ui.Color;
using Material = Unity.UIWidgets.material.Material;


public class BaseAppViewState : SingleTickerProviderStateMixin<BaseAppView>
{
    TabController tabController;

    public override void initState()
    {
        base.initState();
        tabController = new TabController(initialIndex:2,vsync: this, length: 4);
    }
    public override Widget build(BuildContext context)
    {
        return _BuildBaseElemtns();
    }

    private Widget _BuildBaseElemtns()
    {
        return new Scaffold(
           backgroundColor: Color.clear,
           body: new TabBarView(
                    controller: tabController,
                    children: new List<Widget> {
                        new Container(color:Color.clear),
                        new Container(color:Colors.green),
                        new PersonalProfile(),

                    }
               ),
           bottomNavigationBar: new Material(
                color: Colors.blue,
                child: new TabBar(
                  labelStyle: new Unity.UIWidgets.painting.TextStyle(fontWeight: FontWeight.w400, fontSize: 10),
                  controller: tabController,
                  tabs: new List<Widget>
                  {
                       new Tab(text:"跑步",icon: new Icon(Icons.directions_run)),
                       new Tab(text:"社区",icon: new Icon(Icons.camera)),
                       new Tab(text:"我的",icon: new Icon(Icons.person_outline)),
                  },
                 indicatorWeight: 2f,
                 indicatorSize: TabBarIndicatorSize.label,
                 indicatorColor: Color.clear
                )
              )

        );
    }
}
public class BaseAppView : StatefulWidget
{
    public BaseAppView(Key key = null) : base(key)
    {
    }

    public override State createState()
    {
        return new BaseAppViewState();
    }
}