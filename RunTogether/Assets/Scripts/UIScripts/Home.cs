using System;
using System.Collections.Generic;
using Unity.UIWidgets.widgets;
using Datas;
using UIScripts.LoginPage;
using Unity.UIWidgets.Redux;

namespace UIScripts
{
    public class Home : StatelessWidget
    {
//        private readonly List<Widget> Pages = new List<Widget>()
//        {
//            new MainPage(),
//            new WelcomePage(),
//        };
        public override Widget build(BuildContext buildContext)
        {
            return new StoreConnector<AppState, RequestResultEnum>(
                converter: (state) => state.RequestResult,
                builder: ((context, model, dispatcher) =>
                    model == RequestResultEnum.LoginSuccessed
                        ? new SizedBox(child: new MainPage())
                        : new SizedBox(child: new WelcomePage())
                )
            );
        }
    }
}