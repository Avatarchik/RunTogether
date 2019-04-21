using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using Image = Unity.UIWidgets.widgets.Image;

namespace UIScripts.LoginPage
{
    public class WelcomePage : StatelessWidget
    {
        private Widget WelcomeView;


        public WelcomePage()
        {
            WelcomeView = Image.asset("WelcomePage", fit: BoxFit.fitWidth);
        }

        public override Widget build(BuildContext buildContext)
        {
            return new Stack(
                fit: StackFit.expand,
                children: new List<Widget>
                {
                    new Container(
                        child: WelcomeView ?? new Text("None content")
                    ),
                    new Container(
                        margin: EdgeInsets.only(bottom: 35),
                        child: new Row(
                            mainAxisAlignment: MainAxisAlignment.spaceAround,
                            crossAxisAlignment: CrossAxisAlignment.end,
                            children: new List<Widget>
                            {
                                new SizedBox(width: MediaQuery.of(buildContext).size.width * .4f,
                                    child: new FlatButton(color: Colors.white,
                                        child: new Text("登录",
                                            style: CustomTheme.CustomTheme.DefaultTextThemen.display3),
                                        onPressed: () => { HelperWidgets.PushNewRoute(buildContext, new LoginPage()); }
                                    )
                                ),
                                new SizedBox(width: MediaQuery.of(buildContext).size.width * .4f,
                                    child: new FlatButton(color: Colors.green, child: new Text("注册",
                                            style: CustomTheme.CustomTheme.DefaultTextThemen.display2),
                                        onPressed: () =>
                                        {
                                            HelperWidgets.PushNewRoute(buildContext, new RegisterPage());
                                        }
                                    )
                                )
                            }
                        )
                    ),
                }
            );
        }
    }
}