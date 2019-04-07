using System;
using UIScripts.LoginPage;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts
{
    public class AppState
    {
        public void PushNewRoute(BuildContext context, Widget widget)
        {
            Route tmpRoute = new PageRouteBuilder(
                pageBuilder: ((pageContext, animation, secondaryAnimation) => widget),
                transitionsBuilder: ((transContext, animation, secondaryAnimation,
                        child) =>
                    new PageTransition(routeAnimation: animation, child: child,
                        beginDirection: new Offset(2f, 0f),
                        endDirection: Offset.zero))
            );
            Navigator.push(context: context, route: tmpRoute);
        }

        public void PopRoute(BuildContext context)
        {
            Navigator.pop(context: context);
        }



        public int CountdownTime;
        public bool SendVerfyCode;
        public bool WasLogined;
        
        public string Password;
        public string Account;
    }
}