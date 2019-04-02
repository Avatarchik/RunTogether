using Datas;
using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts.LoginPage
{
    public class LoginWidgets : StatelessWidget
    {

        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                appBar: HelperWidgets._buildCloseAppBar(isCenterTitle: true, title: new Text(""),
                    () => { Navigator.pop(context); }),
                backgroundColor: CustomTheme.CustomTheme.EDColor,
                body: _buildBody(context)
            );
        }

        private Widget _buildBody(BuildContext buildContext)
        {
            return new Container(
                margin: EdgeInsets.only(top: 50),
                child: new Column(
                    children: new List<Widget>
                    {
                        new Container(
                            alignment: Alignment.center,
                            margin: EdgeInsets.only(bottom: 50),
                            child: new Text("使用手机号码登录", style: new TextStyle(fontWeight: FontWeight.bold, fontSize: 20))
                        ),

                        new Padding(padding: EdgeInsets.only(top: 20)),
                        new TextFieldHelper("请填写手机号码", padding: EdgeInsets.only(left: 20, right: 20, top: 20)),
                        new TextFieldHelper("请填写密码", padding: EdgeInsets.only(left: 20, right: 20, top: 20),
                            obscureText: true),

                        new SizedBox(
                            width:340,
                            height:60,
                            child:new Padding(
                                padding:EdgeInsets.only(top:20),
                                child:new FlatButton(color: Colors.green,
                                    child: new Text("登录", style: CustomTheme.CustomTheme.DefaultTextThemen.display2),
                                    onPressed: () =>
                                    {
                                         Route tmpEditProfileRoute = new PageRouteBuilder(
                                            pageBuilder:(context, animation, secondaryAnimation) =>  new MainView(),
                                            transitionsBuilder:(context, animation, secondaryAnimation, child) =>
                                            new PageTransition(routeAnimation:animation,child:child,beginDirection:new Offset(1f,0f),endDirection:Offset.zero)
                                        ); 
                
                                        //TODO:Login
                                        AppManager.Instance.SetLoginState(true);
                                        Navigator.pop(buildContext);
                                        Navigator.pop(buildContext);
                                        Navigator.push(buildContext,tmpEditProfileRoute);
                                    }
                                )
                            )
                        )
                    }
                )
            );
        }
    }
}