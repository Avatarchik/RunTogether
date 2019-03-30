using System.Collections.Generic;
using UIScripts;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Color = Unity.UIWidgets.ui.Color;
using Image = Unity.UIWidgets.widgets.Image;
using TextStyle = Unity.UIWidgets.painting.TextStyle;

public class LoginPage : StatefulWidget
{
    public override State createState()
    {
        return new LoginState();
    }
}


public class LoginState : SingleTickerProviderStateMixin<LoginPage>
{
    private List<Widget> widgets = new List<Widget>();
    private int ViewIndex = 2;
    private TextEditingController PasswordEditing = new TextEditingController("");
    public override void initState()
    {
        base.initState();
        widgets.Add(_buildEntry());
        widgets.Add(_buildBaseScaffold(new Text("Login"), _buildLogin()));
        widgets.Add(_buildBaseScaffold(new Text("Regiset"), _buildReigset()));
    }

    public override Widget build(BuildContext context)
    {
        return widgets[ViewIndex];
    }



    private Widget _buildEntry()
    {
       return  new Stack(
            fit:StackFit.expand,
            children:new List<Widget>
            {
                new Container(
                    child:Image.asset("splashscreen",fit:BoxFit.cover)
                ),
                new Container(
                    margin:EdgeInsets.only(bottom:20),
                    child:new Row(
                        mainAxisAlignment:MainAxisAlignment.spaceEvenly,
                        crossAxisAlignment:CrossAxisAlignment.end,
                        children:new List<Widget>
                        {
                            new FlatButton(disabledColor:Colors.white,child:new Text("Sign up")),
                            new FlatButton(disabledColor:Colors.green,child:new Text("Sign In")),
                        }
                    )   
                ),
            }    
        );
    }


    private Widget _buildReigset()
    {
        return new Container(
            margin:EdgeInsets.only(top:50),
            child:new Column(
                children:new List<Widget>
                {
                    new Container(
                        child:new Text("Register",style:new TextStyle(fontWeight:FontWeight.bold,fontSize:20,color:Colors.grey))
                    ),
                    new Container(
                        child:HelperWidgets._buildTextFieldInputer("Phone number")
                    ),
                    new Container(
                        child:HelperWidgets._buildTextFieldInputer("Verfy code")
                    ),
                    new Container(
                        width:340,
                        height:40,
                        child:new FlatButton(disabledColor:Colors.green,child:new Text("Sign up"))
                    ),
                }
            )    
        );
    }



    private Widget _buildLogin()
    {
        return new Container(
            child:new Column(
            )    
        );
    }



    private Widget _buildBaseScaffold(Widget title,Widget body,bool isCenterTitle=true)
    {
        return new Scaffold(
            backgroundColor: new Color(0xffededed),
            appBar:new AppBar(
                leading:new CloseButton(),
                title:title,
                centerTitle:isCenterTitle,
                backgroundColor: new Color(0xffededed)
            ),
            body:body
        );
    }
}