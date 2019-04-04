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
                    title: new Text("瞬间", style: new TextStyle(color: Colors.black),
                        textAlign: TextAlign.center),
                    centerTitle: true,
                    actions: new List<Widget>
                    {
                        new IconButton(icon: new Icon(icon: Icons.camera_alt))
                    }
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