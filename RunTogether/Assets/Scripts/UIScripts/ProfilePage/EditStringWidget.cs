using System;
using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.widgets;
using Color = Unity.UIWidgets.ui.Color;
using TextStyle = Unity.UIWidgets.painting.TextStyle;

namespace UIScripts.ProfilePage
{
    public class EditStringWidget : StatelessWidget
    {
        TextEditingController TitleController = new TextEditingController("");
        private string Title;
        private Action<string> FinishedCallback;
        public EditStringWidget(string data,string title,Action<string> finishedCallback=null)
        {
            TitleController.text = data;
            Title = title;
            FinishedCallback = finishedCallback;
        }
        
        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                backgroundColor:new Color(0xffededed),
                appBar:new AppBar(
                    leading:new IconButton(icon: new Icon(color:Colors.black45,icon: Icons.arrow_back_ios),splashColor:Colors.transparent,disableColor:Colors.transparent,highlightColor:Colors.transparent,onPressed:()=>Navigator.pop(context)),
                    title:new Text(Title,
                    style:new TextStyle(color:Colors.black)),
                    backgroundColor:new Color(0xffededed),
                    centerTitle:true,
                    actions:new List<Widget>
                    {
                        new Container(
                            height:30,
                            width:70,
                            margin:EdgeInsets.only(right:10),
                            decoration:new BoxDecoration(color:new Color(0xff0b870c),shape:BoxShape.rectangle,borderRadius:BorderRadius.all(5)),
                            child:new FlatButton(child:new Text("Finish",style:new TextStyle(color:Colors.white)),onPressed:
                                () =>
                                {
                                    FinishedCallback?.Invoke(TitleController.text);
                                    Navigator.pop(context);
                                })
                        )
                    }),
                body:_buildNickNameInputer()
            );
        }
        
        
        Widget _buildNickNameInputer() {
            return new Row(
                children: new List<Widget>
                {
                    new Flexible(child: new Container(
                        color: new Color(0xffffffff),
                        padding: EdgeInsets.fromLTRB(20, 0, 20, 0),
                        child: new EditableText(maxLines: 1,
                            controller: TitleController,
                            selectionControls: MaterialUtils.materialTextSelectionControls,
                            autofocus: true,
                            focusNode: new FocusNode(),
                            style: new TextStyle(
                                fontSize: 18,
                                height: 2.5f,
                                color: new Color(0xff000000)
                            ),
                            selectionColor: Color.fromARGB(255, 255, 0, 0),
                            cursorColor: Color.fromARGB(255, 0, 0, 0))
                    )),
                }
            );
        }
    }
    
    
    
}
