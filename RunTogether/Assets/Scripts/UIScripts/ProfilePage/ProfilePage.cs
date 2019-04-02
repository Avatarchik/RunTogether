using System.Collections.Generic;
using Datas;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using Color = Unity.UIWidgets.ui.Color;
using TextStyle = Unity.UIWidgets.painting.TextStyle;

namespace UIScripts.ProfilePage
{
    public class ProfilePage : StatefulWidget
    {
        public ProfilePage(Key key = null) : base(key)
        {
        }

        public override State createState()
        {
            return new PersonalProfileState();
        }
    }



    public class PersonalProfileState : State<ProfilePage>
    {
        public override Widget build(BuildContext buildContext)
        {
            return _BuildBaseElemtns();
        }

        private Widget _BuildBaseElemtns()
        {
            return new Container(
                decoration: new BoxDecoration(color: new Color(0xffededed)),
                child:
                new ListView(
                    children: new List<Widget>
                    {
                        new Container(
                            padding:EdgeInsets.only(left:20,top:80,bottom:10),
                            decoration:new BoxDecoration(color:Color.white,shape:BoxShape.rectangle,borderRadius:BorderRadius.all(5)),
                            child:new ListTile(leading:HelperWidgets._buildAvatar(HelperWidgets._createImageProvider(AvatarImageType.NetWork,DataManager.Instance.GetUserData.AvatarUrl)),
                                title:_buildNickName(),subtitle:_buildMotto(),trailing:_buildTrailing())
                        ),
                        new Container(padding:EdgeInsets.only(top:20)),
                        new Container(
                            decoration:new BoxDecoration(color:Color.white),
                            child:new Column(
                                children:new List<Widget>
                                {
                                    new ListTile(title:new Text("Leaderboard"),leading:new Icon(icon:IconsExtern.leaderboaard),onTap:
                                        () =>
                                        {
                                            Route tmpRoute = new PageRouteBuilder(
                                                pageBuilder:((buildContext, animation, secondaryAnimation) =>  new LeaderBord()),
                                                transitionsBuilder:((buildContext, animation, secondaryAnimation, child) => 
                                                    new PageTransition(routeAnimation:animation,child:child,beginDirection:new Offset(2f,0f),endDirection:Offset.zero))
                                            ); 
                                            Navigator.push(context: context, route:tmpRoute);
                                        }),
                                    new Divider(indent:70),
                                    new ListTile(title:new Text("Setting"),leading:new Icon(IconsExtern.setting),onTap: () =>
                                    {
                                        Route tmpRoute = new PageRouteBuilder(
                                            pageBuilder:((buildContext, animation, secondaryAnimation) =>  new SettingWidgets()),
                                            transitionsBuilder:((buildContext, animation, secondaryAnimation, child) => 
                                                new PageTransition(routeAnimation:animation,child:child,beginDirection:new Offset(2f,0f),endDirection:Offset.zero))
                                        ); 
                                        Navigator.push(context: context, route:tmpRoute);
                                    }),
                                }
                            )
                        ),
                    }
                )
            );
        }

        private Widget _buildNickName()
        {
            return new Container(
                child: new Text(DataManager.Instance.GetUserData.NickName, style: new Unity.UIWidgets.painting.TextStyle(fontSize: 18, fontWeight: FontWeight.w700), textAlign: TextAlign.left)
            );
        }


        private Widget _buildMotto()
        {
            return new Container(
                padding: EdgeInsets.only(top: 5),
                child: new Text(DataManager.Instance.GetUserData.Mottor, style: new Unity.UIWidgets.painting.TextStyle(fontSize: 10), textAlign: TextAlign.left)
            );
        }


        private Widget _buildTrailing()
        {
            return new IconButton(icon: new Icon(icon: Icons.arrow_forward_ios),highlightColor:Colors.transparent,onPressed:OnTapProfileListTile);
        }

        private void OnTapProfileListTile()
        {
            Route tmpEditProfileRoute = new PageRouteBuilder(
                pageBuilder:((buildContext, animation, secondaryAnimation) =>  new EditPersonalProfile()),
                transitionsBuilder:((buildContext, animation, secondaryAnimation, child) => 
                new PageTransition(routeAnimation:animation,child:child,beginDirection:new Offset(1f,0f),endDirection:Offset.zero))
            ); 
                
            Navigator.push(context: context, route:tmpEditProfileRoute);
        }

    }
}