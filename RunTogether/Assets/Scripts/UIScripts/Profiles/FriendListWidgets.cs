using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts.Profiles
{
    public class FriendListWidgets : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                backgroundColor: new Color(0xffededed),
                appBar: new AppBar(
                    backgroundColor: new Color(0xffededed),                    
                    title: new Text("Contact", style: new Unity.UIWidgets.painting.TextStyle(color: Colors.black),
                        textAlign: TextAlign.center),
                    centerTitle: true
                ),
                body:
                new Container(
                    child: new ListView(
                        children: new List<Widget>
                        {
                            FriendItem(name:"NSWell"),
                            FriendItem(name:"帅比"),
                            FriendItem(name:"坦克彪"),
                            FriendItem(name:"上海雨"),
                            FriendItem(name:"厕所林",indent:0),
                        }
                    )    
                )
            );
        }


        private Widget FriendItem(float height=5,string avatarUrl=null,string name=null,float indent=80)
        {
            return new Container(
                child:new Column(
                    children:new List<Widget>
                    {
                        new ListTile(leading:_buildAvatar(avatarUrl,40,40),title:new Text(name),onTap: () => { },contentPadding:EdgeInsets.only(left:20)),
                        new Divider(height:height,indent:indent)
                    }    
                ),
                color:Colors.white
            );
        }
        
        private Widget _buildAvatar(string avatarUrl,float width,float height)
        {
            return new Container(
                width: width,
                height: height,
                decoration: new BoxDecoration(
                    shape: BoxShape.circle,
                    image: new DecorationImage(new NetworkImage(Datas.DataManager.Instance.GetUserData.AvatarUrl),
                        fit: BoxFit.cover)
                )
            );
        }
    }
}