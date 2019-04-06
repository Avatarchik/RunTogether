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
                                new StoreConnector<AppState, object>(
                                    converter: state => null,
                                    builder: ((ctx, _, dispatcher) =>
                                        new FlatButton(color: Colors.white,
                                            child: new Text("登录",
                                                style: CustomTheme.CustomTheme.DefaultTextThemen.display3),
                                            onPressed: () =>
                                            {
                                                dispatcher.dispatch(new LoginState()
                                                {
                                                    ClickedNextButton = true,
                                                    Context = context
                                                });
                                            })
                                    ),
                                    pure: true
                                ),
                                new StoreConnector<AppState, object>(
                                    converter: state => null,
                                    builder: ((ctx, _, dispatcher) =>
                                        new FlatButton(color: Colors.green, child: new Text("注册",
                                                style: CustomTheme.CustomTheme.DefaultTextThemen.display2),
                                            onPressed: () =>
                                            {
                                                dispatcher.dispatch(new RegisterState()
                                                {
                                                    ClickedNextButton = true,
                                                    Context = context
                                                });
                                            })
                                    ),
                                    pure: true
                                ),
                            }
                        )
                    ),
                }
            );
        }
    }
}