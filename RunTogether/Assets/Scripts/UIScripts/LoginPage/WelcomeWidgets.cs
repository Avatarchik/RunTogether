using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using Image = Unity.UIWidgets.widgets.Image;

namespace UIScripts.LoginPage
{
    public class WelcomeWidgets : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new Stack(
                fit: StackFit.expand,
                children: new List<Widget>
                {
                    new Container(
                        child: Image.asset("splashscreen", fit: BoxFit.cover)
                    ),
                    new Container(
                        margin: EdgeInsets.only(bottom: 35),
                        child: new Row(
                            mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                            crossAxisAlignment: CrossAxisAlignment.end,
                            children: new List<Widget>
                            {
                                new FlatButton(color: Colors.white,
                                    child: new Text("登录",
                                        style: CustomTheme.CustomTheme.DefaultTextThemen.display3),
                                    onPressed: () =>
                                    {
                                        Route tmpRoute = new PageRouteBuilder(
                                            pageBuilder: ((buildContext, animation, secondaryAnimation) =>
                                                new LoginWidgets()),
                                            transitionsBuilder: ((buildContext, animation, secondaryAnimation, child) =>
                                                new PageTransition(routeAnimation: animation, child: child,
                                                    beginDirection: new Offset(2f, 0f), endDirection: Offset.zero))
                                        );
                                        Navigator.push(context: context, route: tmpRoute);
                                    }),
                                new FlatButton(color: Colors.green,                                    child: new Text("注册",
                                        style: CustomTheme.CustomTheme.DefaultTextThemen.display2),
                                    onPressed: () =>
                                    {
                                        Route tmpRoute = new PageRouteBuilder(
                                            pageBuilder: ((buildContext, animation, secondaryAnimation) =>
                                                new RegisterWidgets()),
                                            transitionsBuilder: ((buildContext, animation, secondaryAnimation, child) =>
                                                new PageTransition(routeAnimation: animation, child: child,
                                                    beginDirection: new Offset(2f, 0f), endDirection: Offset.zero))
                                        );
                                        Navigator.push(context: context, route: tmpRoute);
                                    }
                                ),
                            }
                        )
                    ),
                }
            );
        }
    }
}