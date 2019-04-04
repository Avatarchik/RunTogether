using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts.FriendPage
{
    public class FriendPage : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                backgroundColor: new Color(0xffededed),
                appBar: new AppBar(
                    backgroundColor: new Color(0xffededed),
                    title: new Text("好友", style: new Unity.UIWidgets.painting.TextStyle(color: Colors.black),
                        textAlign: TextAlign.center),
                    centerTitle: true
                ),
                body: new Container(
                    color: new Color(0xffededed),
                    child: new ListView(
                        children: new List<Widget>
                        {
                        }
                    )
                )
            );
        }
    }
}