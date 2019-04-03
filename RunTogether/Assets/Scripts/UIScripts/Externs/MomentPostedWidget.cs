using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts.Externs
{
    public class MomentPostedWidget:StatelessWidget
    {
        private readonly string AvatarURL;
        private readonly string Name;
        private readonly string PostedImageUrl;
        private readonly string LikeCount;
        public MomentPostedWidget(string avatarUrl,string name,string postedImageUrl,string likeCount)
        {
            AvatarURL = avatarUrl;
            Name = name;
            PostedImageUrl = postedImageUrl;
            LikeCount = likeCount;
        }
         public override Widget build(BuildContext context)
         {
               return new Column(
                children:new List<Widget>
                {
                    new Padding(
                        padding:EdgeInsets.only(left:20,right:20),
                        child:new Column(
                            children:new List<Widget>
                            {
                                new ListTile(leading:new AvatarWidget(HelperWidgets._createImageProvider(AvatarImageType.NetWork,AvatarURL),
                                    40,40),title:new Text(Name)),
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
                                Unity.UIWidgets.widgets.Image.network(src:PostedImageUrl,fit:BoxFit.cover,width:200,height:200),
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
                                            child: new Text(LikeCount)
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
    }
}