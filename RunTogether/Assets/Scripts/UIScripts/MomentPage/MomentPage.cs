using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using TextStyle = Unity.UIWidgets.painting.TextStyle;


namespace UIScripts.MomentPage
{
    public class MomentPage:StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                backgroundColor: new Color(0xffededed),
                appBar: new AppBar(
                    backgroundColor: new Color(0xffededed),
                    title: new Text("Friends", style: new TextStyle(color: Colors.black),
                        textAlign: TextAlign.center),
                    centerTitle: true,
                    actions:new List<Widget>
                    {
                        new IconButton(icon:new Icon(icon:Icons.camera_alt))
                    }
                ),
                body: new Container(
                    color:Colors.white,
                    child: new ListView(
                        children: new List<Widget>
                        {
                            _MomentItem("https://ss1.baidu.com/-4o3dSag_xI4khGko9WTAnF6hhy/image/h%3D300/sign=9036c05b3aa85edfe58cf823795409d8/d31b0ef41bd5ad6ef1c3cde18fcb39dbb6fd3c86.jpg"),
                            _MomentItem("https://ss3.baidu.com/-fo3dSag_xI4khGko9WTAnF6hhy/image/h%3D300/sign=ef5a84c15c66d01661199828a72ad498/8601a18b87d6277f48ae353426381f30e924fc7a.jpg"),
                            _MomentItem("http://b-ssl.duitang.com/uploads/item/201704/21/20170421085045_d4aAs.jpeg"),
                            _MomentItem("http://c.hiphotos.baidu.com/image/pic/item/eac4b74543a98226f5d6a9268482b9014a90eb98.jpg"),
                            _MomentItem("http://g.hiphotos.baidu.com/image/pic/item/d1160924ab18972b0aa9c1d2e8cd7b899e510a13.jpg"),
                        }
                    )
                )
            );
        }

        private Widget _MomentItem(string _imgUrl)
        {
            return new Column(
                children:new List<Widget>
                {
                    new Padding(
                        padding:EdgeInsets.only(left:20,right:20),
                        child:new Column(
                            children:new List<Widget>
                            {
                                new ListTile(leading:_buildAvatar(Datas.AppManager.Instance.GetUserData.AvatarUrl,40,40),title:new Text("NSWell")),
                                new Container(
                                    alignment:Alignment.centerLeft,
                                    margin:EdgeInsets.only(left:70),
                                    padding:EdgeInsets.only(bottom:10,top:10),
                                    child: new Text(textAlign:TextAlign.left,data:"Come with us,Change the world!" +
                                                                                  "Come with us,Change the world!" +
                                                                                  "Come with us,Change the world!" +
                                                                                  "Come with us,Change the world!"+
                                                                                  "Come with us,Change the world!"+
                                                                                  "Come with us,Change the world!"+
                                                                                  "Come with us,Change the world!")
                                ),
                                Unity.UIWidgets.widgets.Image.network(src:_imgUrl,fit:BoxFit.cover,width:200,height:200),
                                new Row(
                                    mainAxisAlignment:MainAxisAlignment.start,
                                    children:new List<Widget>
                                    {
                                        new Container(
                                            margin:EdgeInsets.only(left:70),
                                            padding:EdgeInsets.only(bottom:10,top:10),
                                            alignment:Alignment.centerLeft,
                                            child: new Text(textAlign:TextAlign.left,data:System.DateTime.Now.ToString("u"),style:new TextStyle(fontSize:10))

                                        ),
                                        new Container(
                                            width:15,
                                            color:Colors.transparent,
                                            margin:EdgeInsets.only(left:90,bottom:2.5f),
                                            alignment:Alignment.centerRight,
                                            child: new IconButton(icon:new Icon(icon:Icons.thumb_up,size:15f))
                                        ),
                                        new Container(
                                            margin:EdgeInsets.only(left:15,bottom:2.5f),
                                            alignment:Alignment.centerRight,
                                            child: new Text("599999")
                                        )
                                    }
                                )
                            }    
                        )
                    ),
                    new Divider()
                }
            );
        }
        
        private Widget _buildAvatar(string _avatarUrl,float _width,float _height)
        {
            return new Container(
                width: _width,
                height: _height,
                decoration: new BoxDecoration(
                    shape: BoxShape.circle,
                    image: new DecorationImage(new NetworkImage(Datas.AppManager.Instance.GetUserData.AvatarUrl),
                        fit: BoxFit.cover)
                )
            );
        }
    }
}