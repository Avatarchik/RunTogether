using System.Collections.Generic;
using UIScripts.Externs;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using TextStyle = Unity.UIWidgets.painting.TextStyle;


namespace UIScripts.MomentPage
{
    public class MomentPage : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                backgroundColor: new Color(0xffededed),
                appBar: new AppBar(
                    backgroundColor: new Color(0xffededed),
                    title: new Text("好友", style: new TextStyle(color: Colors.black),
                        textAlign: TextAlign.center),
                    centerTitle: true,
                    actions: new List<Widget>
                    {
                        new IconButton(icon: new Icon(icon: Icons.camera_alt))
                    }
                ),
                body: new Container(
                    color: Colors.white,
                    child: new ListView(
                        children: new List<Widget>
                        {
//                            _MomentItem("https://ss1.baidu.com/-4o3dSag_xI4khGko9WTAnF6hhy/image/h%3D300/sign=9036c05b3aa85edfe58cf823795409d8/d31b0ef41bd5ad6ef1c3cde18fcb39dbb6fd3c86.jpg"),
//                            _MomentItem("https://ss3.baidu.com/-fo3dSag_xI4khGko9WTAnF6hhy/image/h%3D300/sign=ef5a84c15c66d01661199828a72ad498/8601a18b87d6277f48ae353426381f30e924fc7a.jpg"),
//                            _MomentItem("http://b-ssl.duitang.com/uploads/item/201704/21/20170421085045_d4aAs.jpeg"),
//                            _MomentItem("http://c.hiphotos.baidu.com/image/pic/item/eac4b74543a98226f5d6a9268482b9014a90eb98.jpg"),
//                            _MomentItem("http://g.hiphotos.baidu.com/image/pic/item/d1160924ab18972b0aa9c1d2e8cd7b899e510a13.jpg"),                        
                        }
                    )
                )
            );
        }
    }
}