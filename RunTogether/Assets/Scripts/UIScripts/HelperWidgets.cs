using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.service;
using Unity.UIWidgets.widgets;

namespace UIScripts
{
    public class HelperWidgets
    {
        private static TextEditingController Editing = new TextEditingController("");

       public static Widget _buildTextFieldInputer(string initText)
        {
            return new Container(
                margin:EdgeInsets.all(20),
                child:new TextField(
                    controller:Editing,
                    autofocus: false,
                    focusNode: new FocusNode(),
                    decoration:new InputDecoration(
                        hintText:initText,
                        contentPadding:EdgeInsets.all(10.0f),
                        fillColor:Colors.transparent,
                        filled:true,
                        focusedBorder:new UnderlineInputBorder(
                            borderSide:new BorderSide(color:Colors.black)    
                        )
                    ),
                    keyboardType:TextInputType.text,
                    textInputAction:TextInputAction.search
                )    
            );
        }
    }
}