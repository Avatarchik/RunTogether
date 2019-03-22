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
        List<BoxShadow> avatarShadows = new List<BoxShadow>();
        avatarShadows.Add(new BoxShadow(color: Colors.black12, offset: new Offset(2, 0), blurRadius: 5));
        avatarShadows.Add(new BoxShadow(color: Colors.black12, offset: new Offset(-2, 0), blurRadius: 5));
        avatarShadows.Add(new BoxShadow(color: Colors.black12, offset: new Offset(0, -2), blurRadius: 5));
        avatarShadows.Add(new BoxShadow(color: Colors.black12, offset: new Offset(0, 2), blurRadius: 5));

        return new Container(                
                decoration: new BoxDecoration(color: new Color(0xfff0f2f4)),
                child:
                new Flex(
                    children: new List<Widget>
                    {
                        new Container(
                            padding:EdgeInsets.only(top:50,left:20,right:20),
                            child:new Container(
                                padding:EdgeInsets.only(left:20,top:10,bottom:10),
                                decoration:new BoxDecoration(color:Color.white,shape:BoxShape.rectangle,boxShadow:avatarShadows,borderRadius:BorderRadius.all(5)),
                                child:
                                new Row(
                                        mainAxisAlignment:Unity.UIWidgets.rendering.MainAxisAlignment.start,
                                        children:new List<Widget>{
                                            new Container(
                                                width:64,
                                                height:64,
                                                decoration:new BoxDecoration(
                                                    color:Color.white,
                                                    shape:BoxShape.circle,
                                                    boxShadow:avatarShadows,
                                                    image:new DecorationImage(new NetworkImage("http://img.52z.com/upload/news/image/20180108/20180108080908_15279.jpg"),fit:BoxFit.cover)
                                                )
                                            ),
                                            new Container(padding:EdgeInsets.only(left:20)),
                                            new Column(
                                                    mainAxisAlignment:Unity.UIWidgets.rendering.MainAxisAlignment.start,
                                                    mainAxisSize:Unity.UIWidgets.rendering.MainAxisSize.min,
                                                    crossAxisAlignment:Unity.UIWidgets.rendering.CrossAxisAlignment.start,
                                                    children:new List<Widget>
                                                    {
                                                        new Container(
                                                            child:new Text("NSWell",style:new Unity.UIWidgets.painting.TextStyle(fontSize:18,fontWeight:FontWeight.w700),textAlign:TextAlign.left)
                                                        ),
                                                        new Container(
                                                            padding:EdgeInsets.only(top:5),
                                                            child:new Text("Change the world!",style:new Unity.UIWidgets.painting.TextStyle(fontSize:10),textAlign:TextAlign.left)
                                                        )
                                                    }
                                                )
                                        })
                                    )
                        ),
                        new Container(padding:EdgeInsets.only(top:20)),
                        new Container(
                            decoration:new BoxDecoration(color:Color.white,boxShadow:avatarShadows),
                            child:new Column(
                                    children:new List<Widget>
                                    {
                                        new ListTile(title:new Text("title"),leading:new Icon(Icons.book)),
                                        new Divider(color:Color.black,height:5),
                                        new ListTile(title:new Text("title"),leading:new Icon(Icons.book)),
                                        new Divider(color:Color.black,height:5),
                                        new ListTile(title:new Text("title"),leading:new Icon(Icons.book)),
                                        new Divider(color:Color.black,height:5),
                                        new ListTile(title:new Text("title"),leading:new Icon(Icons.book)),
                                        new Divider(color:Color.black,height:5),
                                        new ListTile(title:new Text("title"),leading:new Icon(Icons.book)),
                                        new Divider(color:Color.black,height:5),
                                        new ListTile(title:new Text("title"),leading:new Icon(Icons.book)),
                                        new Divider(color:Color.black,height:5),
                                    }
                                )
                        ),
                    }
                )
            );
    }
}