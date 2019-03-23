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
using Unity.UIWidgets.painting;

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



public class PersonalProfileState : State<PersonalProfile>
{


    public override void initState()
    {
        base.initState();

    }
    public override Widget build(BuildContext context)
    {
        return _BuildBaseElemtns();
    }

    private Widget _BuildBaseElemtns()
    {
        return new Container(
                decoration: new BoxDecoration(color: new Color(0xffededed)),
                child:
                new Flex(
                    children: new List<Widget>
                    {
                        new Container(
                            //padding:EdgeInsets.only(top:50,left:20,right:20),
                            child:new Container(
                                padding:EdgeInsets.only(left:20,top:80,bottom:10),
                                decoration:new BoxDecoration(color:Color.white,shape:BoxShape.rectangle,borderRadius:BorderRadius.all(5)),
                                child:
                                new ListTile(leading:_buildAvatar(),title:_buildNickName(),subtitle:_buildMotto(),trailing:_buildTrailing(),onTap:OnTapProfileListTile)
                            )
                        ),
                        new Container(padding:EdgeInsets.only(top:20)),
                        new Container(
                            decoration:new BoxDecoration(color:Color.white),
                            child:new Column(
                                    children:new List<Widget>
                                    {
                                        new ListTile(title:new Text("leaderboard"),leading:new Icon(Icons.leaderboard)),
                                        new Divider(color:Color.black,height:5),
                                        new ListTile(title:new Text("Setting"),leading:new Icon(Icons.setting)),
                                    }
                                )
                        ),
                    }
                )
            );
    }

    private Widget _buildAvatar()
    {
        return new Container(
                width: 64,
                height: 64,
                decoration: new BoxDecoration(
                    shape: BoxShape.circle,
                    image: new DecorationImage(new NetworkImage("https://ss0.bdstatic.com/70cFuHSh_Q1YnxGkpoWK1HF6hhy/it/u=2150992516,3202248268&fm=26&gp=0.jpg"), fit: BoxFit.cover)
                )
            );
    }

    private Widget _buildNickName()
    {
        return new Container(
                child: new Text("NSWell", style: new Unity.UIWidgets.painting.TextStyle(fontSize: 18, fontWeight: FontWeight.w700), textAlign: TextAlign.left)
        );
    }


    private Widget _buildMotto()
    {
        return new Container(
                padding: EdgeInsets.only(top: 5),
                child: new Text("Change the world!", style: new Unity.UIWidgets.painting.TextStyle(fontSize: 10), textAlign: TextAlign.left)
        );
    }


    private Widget _buildTrailing()
    {
        return new IconButton(icon: new Icon(icon: Icons.arrow_forward_ios));
    }

    private void OnTapProfileListTile()
    {

    }

}