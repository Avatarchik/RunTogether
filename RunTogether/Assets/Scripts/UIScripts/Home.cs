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
        public override Widget build(BuildContext buildContext)
        {
            return new StoreConnector<AppState, AppState>(
                converter: (state) => state,
                builder: ((context, model, dispatcher) =>
                    model.RequestResult == RequestResultEnum.LoginSuccessed
                        ? (Widget) new MainPage()
                        : new WelcomePage()
                )
            );
        }
    }
}