using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.widgets;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.ui;
using Color = Unity.UIWidgets.ui.Color;
using Unity.UIWidgets.material;
using Material = Unity.UIWidgets.material.Material;

public class PersonalProfile : StatefulWidget
{
    public PersonalProfile(Key key = null) : base(key)
    {
    }

    public override State createState()
    {
        return new PersonalProfileState();
    }
}



public class PersonalProfileState : SingleTickerProviderStateMixin<PersonalProfile>
{    
    TabController tabController;


    public override void initState()
    {
        base.initState();
        tabController = new TabController(vsync: this, length: 4);      
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
                        new Container(color:Colors.redAccent),
                    }
               ),
           bottomNavigationBar: new Material(
                color: Colors.black,
                child: new TabBar(
                  controller: tabController,
                  tabs: new List<Widget>
                  {
                       new Tab(text:"测试",icon: new Icon(Icons.directions_run)),
                       new Tab(text:"社区",icon: new Icon(Icons.camera)),
                       new Tab(text:"我的",icon: new Icon(Icons.person_outline)),
                  },
                 indicatorWeight: 0.1f
                )
              )
        );
    }
}