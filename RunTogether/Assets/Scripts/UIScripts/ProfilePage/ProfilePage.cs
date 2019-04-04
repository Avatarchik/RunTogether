using System.Collections.Generic;
using Datas;
using UIScripts.Externs;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using Color = Unity.UIWidgets.ui.Color;

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
                            padding: EdgeInsets.only(left: 20, top: 80, bottom: 10),
                            decoration: new BoxDecoration(color: Color.white, shape: BoxShape.rectangle,
                                borderRadius: BorderRadius.all(5)),
                            child: new ListTile(
                                leading: new AvatarWidget(HelperWidgets._createImageProvider(AvatarImageType.NetWork,
                                    AppManager.Instance.GetUserData.AvatarUrl)),
                                title: _buildNickName(), subtitle: _buildMotto(), trailing: _buildTrailing())
                        ),
                        new Container(padding: EdgeInsets.only(top: 20)),
                        new Container(
                            decoration: new BoxDecoration(color: Color.white),
                            child: new Column(
                                mainAxisSize: MainAxisSize.min,
                                children: new List<Widget>
                                {
                                    new TapItemWidget(title: "Leader board", iconData: IconsExtern.leaderboaard,
                                        GoToLeaderBord),
                                    new TapItemWidget(title: "Setting", iconData: IconsExtern.setting,
                                        GoToSettingPanel, isLast: true),
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
                child: new Text(AppManager.Instance.GetUserData.NickName,
                    style: new Unity.UIWidgets.painting.TextStyle(fontSize: 18, fontWeight: FontWeight.w700),
                    textAlign: TextAlign.left)
            );
        }


        private Widget _buildMotto()
        {
            return new Container(
                padding: EdgeInsets.only(top: 5),
                child: new Text(AppManager.Instance.GetUserData.Mottor,
                    style: new Unity.UIWidgets.painting.TextStyle(fontSize: 10), textAlign: TextAlign.left)
            );
        }


        private Widget _buildTrailing()
        {
            return new IconButton(icon: new Icon(icon: Icons.arrow_forward_ios), highlightColor: Colors.transparent,
                onPressed: GoToEditProfile);
        }

        private void GoToEditProfile()
        {
            Route tmpEditProfileRoute = new PageRouteBuilder(
                pageBuilder: ((buildContext, animation, secondaryAnimation) => new EditProfilePage()),
                transitionsBuilder: ((buildContext, animation, secondaryAnimation, child) =>
                    new PageTransition(routeAnimation: animation, child: child, beginDirection: new Offset(1f, 0f),
                        endDirection: Offset.zero))
            );

            Navigator.push(context: context, route: tmpEditProfileRoute);
        }


        private void GoToLeaderBord()
        {
            Route tmpRoute = new PageRouteBuilder(
                pageBuilder: ((buildContext, animation, secondaryAnimation) => new LeaderBordPage()),
                transitionsBuilder: ((buildContext, animation, secondaryAnimation, child) =>
                    new PageTransition(routeAnimation: animation, child: child, beginDirection: new Offset(2f, 0f),
                        endDirection: Offset.zero))
            );
            Navigator.push(context: context, route: tmpRoute);
        }

        private void GoToSettingPanel()
        {
            Route tmpRoute = new PageRouteBuilder(
                pageBuilder: ((buildContext, animation, secondaryAnimation) => new SettingPage()),
                transitionsBuilder: ((buildContext, animation, secondaryAnimation, child) =>
                    new PageTransition(routeAnimation: animation, child: child, beginDirection: new Offset(2f, 0f),
                        endDirection: Offset.zero))
            );
            Navigator.push(context: context, route: tmpRoute);
        }
    }
}