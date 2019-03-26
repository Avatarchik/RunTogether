using System.Collections.Generic;
using RSG;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using Image = Unity.UIWidgets.widgets.Image;
using TextStyle = Unity.UIWidgets.painting.TextStyle;

namespace UIScripts.Profiles
{
    public class EditAvatar : StatefulWidget
    {
        public override State createState()
        {
            return new EditAvatarState();
        }
    }
    public class EditAvatarState : State<EditAvatar>
    {
        GlobalKey<ScaffoldState> _scaffoldKey = GlobalKey<ScaffoldState>.key();
        VoidCallback _showBottomSheetCallback;

        public override void initState()
        {
            base.initState();
            _showBottomSheetCallback = _showBottomSheet;
        }

        public override Widget build(BuildContext buildContext)
        {
            return new Scaffold(
                key:_scaffoldKey,
                backgroundColor:new Color(0xff000000),
                appBar:new AppBar(
                    leading:new IconButton(color:Colors.white,icon: new Icon(icon: Icons.arrow_back_ios),splashColor:Colors.transparent,disableColor:Colors.transparent,highlightColor:Colors.transparent,onPressed:()=>Navigator.pop(buildContext)),
                    title:new Text("Edit Avatar",
                        style:new TextStyle(color:Colors.white)),
                    backgroundColor:new Color(0xff000000),
                    centerTitle:true,
                    actions:new List<Widget>
                    {
                        new Container(
                            child:new FlatButton(child:new Text("···",style:new TextStyle(color:Colors.white)),onPressed:
                                () =>
                                {
                                    _showBottomSheetCallback?.Invoke();
                                })
                        )
                    }),
                body:_buildAvatar()
            );
        }


        private Widget _buildAvatar()
        {
            return new Center(
                child:new Container(
                    width:256,
                    height:256,
                    child:Image.network(Datas.DataManager.Instance.GetUserData.AvatarUrl)
                )
            );
        }
        
        void _showBottomSheet() {
            setState(() => { _showBottomSheetCallback = null; });

            _scaffoldKey.currentState.showBottomSheet(subContext => {
                ThemeData themeData = Theme.of(subContext);
                return new Container(
                    decoration: new BoxDecoration(
                        border: new Border(
                            top: new BorderSide(
                                color: themeData.disabledColor))),
                    child:  new Padding(
                            padding: EdgeInsets.only(top:20),
                            child: new Column(
                                mainAxisSize: MainAxisSize.min,
                                crossAxisAlignment:CrossAxisAlignment.center,
                                children: new List<Widget>
                                {
                                    new ListTile(title: new Text("Select")),
                                    new Divider(height:2),
                                    new ListTile(title: new Text("Canele")),
                                }
                            )
                    )
                );
            }).closed.Then(obj => {
                if (mounted) {
                    setState(() => { _showBottomSheetCallback = this._showBottomSheet; });
                }

                return new Promise();
            });
        }

    }
}
