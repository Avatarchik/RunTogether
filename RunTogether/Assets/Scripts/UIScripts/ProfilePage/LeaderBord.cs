using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using Color = Unity.UIWidgets.ui.Color;


namespace UIScripts.ProfilePage
{
    public class LeaderBord:StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                backgroundColor: new Color(0xffededed),
                appBar: new AppBar(
                    backgroundColor: new Color(0xffededed),
                    leading: new IconButton(color:Colors.black45,icon: new Icon(icon: Icons.arrow_back_ios), onPressed:
                        ()=>Navigator.pop(context),highlightColor:Colors.transparent,splashColor:Colors.transparent),
                    title: new Text("LeaderBord", style: new Unity.UIWidgets.painting.TextStyle(color: Colors.black), textAlign: TextAlign.center),
                    centerTitle: true

                ),
                body:_leaderBordBody()
            );
        }

        private Widget _leaderBordBody()
        {
            return new Column(
                children: new List<Widget>{
                    new Container(
                        height:120,
                        child: new Center(
                            child:new Column(
                                children:new List<Widget>
                                {
                                    _buildAvatar(80,80),
                                    new Text(Datas.AppManager.Instance.GetUserData.NickName),
                                    new Text(Datas.AppManager.Instance.GetUserData.Mottor),
                                }
                            )
                        )
                        
                    ), 
                    new Divider(height:5),
                    new Container(
                        child:new Flexible(
                            child:new ListView(
                                children: new List<Widget>
                                {
                                    _buildRakingItem(),
                                    _buildRakingItem(),
                                    _buildRakingItem(),
                                    _buildRakingItem(),
                                    _buildRakingItem(),
                                    _buildRakingItem(),
                                    _buildRakingItem(),
                                    _buildRakingItem(),
                                    _buildRakingItem(),
                                    _buildRakingItem(),
                                    _buildRakingItem(),

                                }
                            )    
                        )
                    )
                }
            );
        }   
        
        private Widget _buildAvatar(int width,int height)
        {
            return  new Container(
                width: width,
                height: height,
                decoration: new BoxDecoration(
                    shape: BoxShape.circle,
                    image: new DecorationImage(new NetworkImage(Datas.AppManager.Instance.GetUserData.AvatarUrl), fit: BoxFit.cover)
                )
            );
        }

        private Widget _buildRakingItem()
        {
            return new Column(
                children: new List<Widget>
                {
                    new ListTile(
                        leading: new Row(
                                children:new List<Widget>
                                {
                                    new Text("1"),
                                    new Padding(padding:EdgeInsets.only(left:30)),
                                    _buildAvatar(40, 40),
                                    new Padding(padding:EdgeInsets.only(left:20)),
                                    new Text("12"),
                                    new Padding(padding:EdgeInsets.only(left:20)),                                    
                                    new Text("32323 KM"),
                                }
                            ),                        
                        trailing: new Icon(icon: Icons.hearing)),
                    new Divider(indent:18,height:5),
                }
            );
        }
    }
}