using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.widgets;

namespace UIScripts.LoginPage
{
    public class ForgetPasswordPage : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                appBar: HelperWidgets._buildCloseAppBar(true,
                    null,
                    new Text("更改密码"),
                    () => { HelperWidgets.PopRoute(context); }),
                body: _buildBody()
            );
        }


        private Widget _buildBody()
        {
            return new Center(
                child: new Column(
                    children: new List<Widget>
                    {
                        new TextFieldExtern(hintText: "请输入手机号", margin: EdgeInsets.all(20)),
                        new Padding(
                            padding: EdgeInsets.only(top: 20),
                            child: new RaisedButton(child: new Text("确定"), onPressed: null, elevation: 0)
                        ),
//                        new Row(children: new List<Widget>
//                        {
//                            new TextField(),
//                            new RaisedButton(child: new Text("发送")),
//                        }),
                  
                    }
                )
            );
        }
    }
}