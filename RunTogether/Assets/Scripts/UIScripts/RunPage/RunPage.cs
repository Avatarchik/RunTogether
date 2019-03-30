using System;
using System.Collections.Generic;
using Unity.UIWidgets.animation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.service;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using Color = Unity.UIWidgets.ui.Color;
using TextStyle = Unity.UIWidgets.painting.TextStyle;

namespace UIScripts.RunPage
{
    public class RunPage:StatefulWidget
    {
        public override State createState()
        {
            return new RunPageState();
        }
    }

    internal class RunPageState : TickerProviderStateMixin<RunPage>
    {
        private List<Widget> Pages = new List<Widget>();
        private int PagesIndex = 0;
        private Animation<float> PositionAnimation;
        private AnimationController PositionController;
        private TabController ConnectTabController;
        private TextEditingController TitleController;

        public override void initState()
        {
            base.initState();
            ConnectTabController = new TabController(vsync:this,length:3);
            PositionController = new AnimationController(vsync:this,duration:new TimeSpan(0,0,0,0,300));
            PositionAnimation = new FloatTween(begin:0,end:1).animate(PositionController);
            
            Pages.Add(_buldFloatingButton());
            Pages.Add(new PageTransition(routeAnimation:PositionAnimation,beginDirection:new Offset(0,5),endDirection:new Offset(0,0),child:_connectView()));
        }

        public override Widget build(BuildContext context)
        {
            return Pages[PagesIndex];
        }


        private Widget _buldFloatingButton()
        {
            return new Container(alignment: Alignment.bottomRight,
                margin: EdgeInsets.all(10),
                child: new FloatingActionButton(
                    child: new Icon(icon: IconsExtern.run_fill, size: 30),
                    backgroundColor: Colors.green,
                    onPressed: () =>
                    {
                        PagesIndex = 1;
                        PositionController.forward();
                        setState();
                    }
                )
            );
        }

        private Widget _connectView()
        {
            return new Scaffold(
                backgroundColor:new Color(0xffededed),
                appBar: new AppBar(
                    backgroundColor:new Color(0xffededed),
                    leading:new IconButton(icon:new Icon(icon:Icons.arrow_back_ios,color:Colors.grey),onPressed: () =>
                        {
                            PositionController.reverse().whenCompleteOrCancel(() =>
                            {
                                PagesIndex = 0;
                                setState();
                            });
                        }),
                    title:new Text("Connect",style:new TextStyle(color:Colors.black)),
                    bottom:new TabBar(
                        indicatorColor:Colors.black,
                        controller: ConnectTabController,
                        labelColor:Color.black,
                        tabs:new List<Widget>
                        {
                            new Tab(text:"Distance"),    
                            new Tab(text:"Time"),    
                            new Tab(text:"City"),    
                        }
                    ),
                    centerTitle:true
                ),
                body:new TabBarView(
                    controller:ConnectTabController,
                    children:new List<Widget>
                    {
                        _buildDistanceCondition("1000",()=>{}),
                        _buildDistanceCondition("15",()=>{}),
                        _buildDistanceCondition("Xiamen",()=>{}),
                    }
                )
            );
        }



        private Widget _buildDistanceCondition(string initText,Action startRun)
        {
            return new Container(
                margin:EdgeInsets.only(top:20),
                alignment:Alignment.topCenter,
                decoration:new BoxDecoration(color:new Color(0xffededed)),
                child:new Container(
                    decoration:new BoxDecoration(color:new Color(0xffffffff),borderRadius:BorderRadius.all(5)),
                    margin:EdgeInsets.all(10),
                    child:new Column(
                        mainAxisSize:MainAxisSize.min,
                        crossAxisAlignment:CrossAxisAlignment.start,
                        children:new List<Widget>
                        {
                            new Padding(
                                padding:EdgeInsets.only(left:20,top:20),
                                child:new Text("Setting your condition.",textAlign:TextAlign.left)
                            ),
                            _buildTextFieldInputer(initText),
                            new Container(
                                alignment:Alignment.center,
                                height:40,
                                margin:EdgeInsets.only(top:30),
                                decoration:new BoxDecoration(color:Colors.black),
                                child: new GestureDetector(
                                    onTap:startRun.Invoke,
                                    child:new Text("Run",style:new TextStyle(color:Colors.white))
                                )   
                            )
                        }    
                    )    
                )
            );
        }


        Widget _buildTextFieldInputer(string initText)
        {
            TitleController = new TextEditingController("");
            return new Container(
                margin:EdgeInsets.all(20),
                child:new TextField(
                    controller:TitleController,
                    autofocus: false,
                    focusNode: new FocusNode(),
                    decoration:new InputDecoration(
                        hintText:initText,
                        contentPadding:EdgeInsets.all(10.0f),
                        fillColor:Colors.white,
                        filled:true,
                        focusedBorder:new UnderlineInputBorder(
                            borderSide:new BorderSide(color:Colors.black)    
                        )
                    ),
                    keyboardType:TextInputType.text,
                    textInputAction:TextInputAction.search
                )    
            );
        }
    }
    
}