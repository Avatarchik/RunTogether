using System.Collections.Generic;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using Color = Unity.UIWidgets.ui.Color;
using Datas;
using Unity.UIWidgets.rendering;

namespace UIScripts.Profiles
{
    public class SettingWidgets:StatelessWidget
    {
        
        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                backgroundColor: new Color(0xffededed),
                appBar: new AppBar(
                    backgroundColor: new Color(0xffededed),
                    leading: new IconButton(color:Colors.black45,icon: new Icon(icon: Icons.arrow_back_ios), onPressed:
                        ()=>Navigator.pop(context),highlightColor:Colors.transparent,splashColor:Colors.transparent),
                    title: new Text("Setting", style: new Unity.UIWidgets.painting.TextStyle(color: Colors.black), textAlign: TextAlign.center),
                    centerTitle: true
                ),
                body: new ListView(
                    children:new List<Widget>
                    {
                        ListItem(_title:"General"),
                        ListItem(0,_title:"Privacy"),
                    }      
                )
            );
        }


        private Widget ListItem(int _height=5,string _title=null)
        {
            return new Container(
                decoration:new BoxDecoration(color:Colors.white),
                child:new Column(
                    children:new List<Widget>
                    {
                        new ListTile(title:new Text(_title),trailing:new Icon(icon:Icons.arrow_forward_ios)),
                        new Divider(indent:18,height:_height),
                    }  
                )
            );   
        }
    }
}