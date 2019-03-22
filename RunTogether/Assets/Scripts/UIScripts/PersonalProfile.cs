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
                color:Color.white,
                child:
                new Flex(
                       children: new List<Widget>
                       {
                           new Container(
                               color:Color.white,
                               padding:EdgeInsets.only(bottom:100)
                               ),
                           new Container(
                               color:Color.white,
                               width:64,
                               height:64,
                               decoration:new BoxDecoration(
                                    color:Color.white,
                                    shape:BoxShape.circle,
                                    image:new DecorationImage(new NetworkImage("http://img.52z.com/upload/news/image/20180108/20180108080908_15279.jpg"),fit:BoxFit.cover)
                                   )
                               )
                       }
                    )            
            );
    }
}