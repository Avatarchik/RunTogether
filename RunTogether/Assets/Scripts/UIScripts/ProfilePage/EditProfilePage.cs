using System;
using System.Collections.Generic;
using UIScripts.Externs;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using Image = Unity.UIWidgets.widgets.Image;

namespace UIScripts.ProfilePage
{
    public class EditProfilePage : StatefulWidget
    {
        public EditProfilePage(Key key = null) : base(key)
        {
        }

        public override State createState()
        {
            return new EditPersonalState();
        }
    }


    public class EditPersonalState : State<EditProfilePage>
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
                    leading: new IconButton(color: Colors.black45, icon: new Icon(icon: Icons.arrow_back_ios),
                        onPressed: BackToPrevious, highlightColor: Colors.transparent, splashColor: Colors.transparent),
                    title: new Text("Edit profile", style: new Unity.UIWidgets.painting.TextStyle(color: Colors.black),
                        textAlign: TextAlign.center),
                    centerTitle: true
                ),
                body: _buildBaseViewBody()
            );
        }


        private Widget _buildBaseViewBody()
        {
            return new Padding(padding: EdgeInsets.only(top: 50),
                child: new Column(
                    children: new List<Widget>
                    {
                        new ListTileWithDividerWidget(titleWidget: new Text("Avatar"),
                            trailingWidget: new AvatarWidget(
                                Image.network(Datas.AppManager.Instance.GetUserData.AvatarUrl)),
                            onTap: GoToAvatarEdit),


                        new ListTileWithDividerWidget(titleWidget: new Text("NickName"),
                            trailingWidget: _buildNickNameTrailing(),
                            onTap: () => GoToStringEditor(Datas.AppManager.Instance.GetUserData.NickName, "Edit Name",
                                (result => Datas.AppManager.Instance.GetUserData.NickName = result))),


                        new ListTileWithDividerWidget(titleWidget: new Text("Mottor"),
                            trailingWidget: _buildMottorTrailing(),
                            onTap: () => GoToStringEditor(Datas.AppManager.Instance.GetUserData.Mottor,
                                title: "Edit Mottor",
                                (result => Datas.AppManager.Instance.GetUserData.Mottor = result)), dividerHeight: 0),
                    }
                )
            );
        }


        private Widget _buildNickNameTrailing()
        {
            List<Widget> tmpTrailings = new List<Widget>()
            {
                new Text(Datas.AppManager.Instance.GetUserData.NickName),
                new Icon(icon: Icons.arrow_forward_ios)
            };
            return new MultipTrailingWidget(tmpTrailings);
        }

        private Widget _buildMottorTrailing()
        {
            List<Widget> tmpTrailings = new List<Widget>()
            {
                new Text(Datas.AppManager.Instance.GetUserData.Mottor),
                new Icon(icon: Icons.arrow_forward_ios)
            };
            return new MultipTrailingWidget(tmpTrailings);
        }


        private void BackToPrevious()
        {
            Navigator.pop(context);
        }


        private void GoToAvatarEdit()
        {
            Route tmpEditNickNameRoute = new PageRouteBuilder(
                pageBuilder: ((buildContext, animation, secondaryAnimation) => new EditAvatarPage()),
                transitionsBuilder: ((buildContext, animation, secondaryAnimation, child) =>
                    new PageTransition(
                        routeAnimation: animation,
                        child: child,
                        beginDirection: new Offset(2f, 0), endDirection: Offset.zero))
            );
            Navigator.push(context: context, route: tmpEditNickNameRoute);
        }

        private void GoToStringEditor(string data, string title, Action<string> editResultCallback)
        {
            Route tmpStringEditorRoute = new PageRouteBuilder(
                pageBuilder: ((buildContext, animation, secondaryAnimation) =>
                    new EditStringPage(data, title, editResultCallback)),
                transitionsBuilder: ((buildContext, animation, secondaryAnimation, child) =>
                    new PageTransition(
                        routeAnimation: animation,
                        child: child,
                        beginDirection: new Offset(0, 2f), endDirection: Offset.zero))
            );
            Navigator.push(context: context, route: tmpStringEditorRoute);
        }
    }
}