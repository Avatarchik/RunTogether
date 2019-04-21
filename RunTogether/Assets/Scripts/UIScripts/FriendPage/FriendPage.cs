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
                    centerTitle: true,
                    elevation: 0
                ),
                body: new Container(
                    color: new Color(0xffededed),
                    child: new ListView(
                        children: new List<Widget>
                        {
                            new FriendWidget(
                                "https://together-run.oss-cn-beijing.aliyuncs.com/upload/image/20190419/WechatIMG66.png",
                                "Test", 16, 80),
                            new FriendWidget(
                                "https://together-run.oss-cn-beijing.aliyuncs.com/upload/image/20190419/india_thanjavur_market.png",
                                "Test", 16, 80),
                            new FriendWidget(
                                "https://together-run.oss-cn-beijing.aliyuncs.com/upload/image/20190419/india_thanjavur_market.png",
                                "Test", 16, 80),
                            new FriendWidget(
                                "https://together-run.oss-cn-beijing.aliyuncs.com/upload/image/20190419/india_thanjavur_market.png",
                                "Test", 16, 80),
                        }
                    )
                )
            );
        }
    }
}