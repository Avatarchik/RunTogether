using System;
using System.Collections.Generic;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts.ProfilePage
{
    public class EditPersonalProfile : StatefulWidget
    {
        public EditPersonalProfile(Key key = null) : base(key)
        {
        }
        public override State createState()
        {
            return new EditPersonalState();  
        }
    }


    public class EditPersonalState : State<EditPersonalProfile>
    {
        public override Widget build(BuildContext buildContext)
        {
            return _buildBaseView();
        }


        private Widget _buildBaseView()
        {
            return new Scaffold(
                backgroundColor: new Color(0xffededed),
                appBar: new AppBar(
                    backgroundColor: new Color(0xffededed),
                    leading: new IconButton(color:Colors.black45,icon: new Icon(icon: Icons.arrow_back_ios), onPressed: BackToPrevious,highlightColor:Colors.transparent,splashColor:Colors.transparent),
                    title: new Text("Edit profile", style: new Unity.UIWidgets.painting.TextStyle(color: Colors.black), textAlign: TextAlign.center),
                    centerTitle: true
                ),
                body: _buildBaseViewBody()
            );
        }


        private Widget _buildBaseViewBody()
        {
            return new Padding(padding: EdgeInsets.only(top: 50),
                child: new Column(
                    children: new List<Widget> {
                        _itemList(new ListTile(title:new Text("Avatar"),trailing:_buildAvatar(),onTap:GoToAvatarEdit)),
                        _itemList(new ListTile(title:new Text("NickName"),trailing:_buildNickName(),
                            onTap:()=>GoToStringEditor(Datas.DataManager.Instance.GetUserData.NickName,"Edit Name",(result => Datas.DataManager.Instance.GetUserData.NickName=result)))),
                        _itemList(new ListTile(title:new Text("Mottor"),trailing:_buildMottor(),
                            onTap:()=>GoToStringEditor(Datas.DataManager.Instance.GetUserData.Mottor,title:"Edit Mottor",(result => Datas.DataManager.Instance.GetUserData.Mottor=result))),dividerHeight:0),

                    }
                )
            );
        }

        private Widget _itemList(Widget widget=null,float dividerHeight=5,float dividerIndent=18)
        {
            return new Container(decoration: new BoxDecoration(color: Colors.white),
                child:
                new Column(
                    children: new List<Widget>
                    {
                        widget,
                        new Divider(indent: dividerIndent, height:dividerHeight)
                    }
                )
            );
        }
        
        
        private Widget _buildAvatar()
        {
            return  _ListTitleTrailingExpand(
                trailingFirstWidget: new Container(
                    width: 32,
                    height: 32,
                    decoration: new BoxDecoration(
                        shape: BoxShape.circle,
                        image: new DecorationImage(new NetworkImage(Datas.DataManager.Instance.GetUserData.AvatarUrl), fit: BoxFit.cover)
                    )
                ),
                trailingSecondWidget:  new Icon(icon: Icons.arrow_forward_ios)
            );
        }

        private Widget _buildNickName()
        {
            return _ListTitleTrailingExpand(
                trailingFirstWidget:new Text(Datas.DataManager.Instance.GetUserData.NickName),
                trailingSecondWidget: new Icon(icon: Icons.arrow_forward_ios)
            );
        }
        
        private Widget _buildMottor()
        {
            return _ListTitleTrailingExpand(
                trailingFirstWidget:new Text(Datas.DataManager.Instance.GetUserData.Mottor),
                trailingSecondWidget: new Icon(icon: Icons.arrow_forward_ios)
            );
        }


        private Widget _ListTitleTrailingExpand(Widget trailingFirstWidget=null, Widget trailingSecondWidget=null)
        {
            return new Row(
                mainAxisSize: Unity.UIWidgets.rendering.MainAxisSize.min,
                children: new List<Widget>
                {
                    trailingFirstWidget,
                    trailingSecondWidget
                }
            );
        }



        private void BackToPrevious()
        {
            Navigator.pop(context);
        }


        private void GoToAvatarEdit()
        {
            Route tmpEditNickNameRoute = new PageRouteBuilder(
                pageBuilder:((buildContext, animation, secondaryAnimation) => new EditAvatar()),
                transitionsBuilder:((buildContext, animation, secondaryAnimation, child) => 
                    new PageTransition(
                        routeAnimation:animation,
                        child:child,
                        beginDirection:new Offset(2f,0),endDirection:Offset.zero))
            );
            Navigator.push(context: context, route: tmpEditNickNameRoute);
        }

        private void GoToStringEditor(string data,string title,Action<string> editResultCallback)
        {
            Route tmpStringEditorRoute = new PageRouteBuilder(
                pageBuilder:((buildContext, animation, secondaryAnimation) => new EditStringWidget(data,title,editResultCallback)),
                transitionsBuilder:((buildContext, animation, secondaryAnimation, child) => 
                    new PageTransition(
                        routeAnimation:animation,
                        child:child,
                        beginDirection:new Offset(0,2f),endDirection:Offset.zero))
            );
            Navigator.push(context: context, route: tmpStringEditorRoute);
        }
    }
}