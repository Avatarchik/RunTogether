using System;
using System.Collections.Generic;
using Unity.UIWidgets.widgets;
using Datas;
using UIScripts.LoginPage;
using Unity.UIWidgets.Redux;
using UnityEngine;

namespace UIScripts
{
    public class Home : StatelessWidget
    {
        public override Widget build(BuildContext buildContext)
        {
            bool logined = PlayerPrefs.GetString("logined").Equals("Yes");
            return new StoreConnector<AppState, AppState>(
                converter: (state) => state,
                builder: ((context, model, dispatcher) =>
                    logined ? new MainPage() :
                    model.Logined ? (Widget) new MainPage() : new WelcomePage()
                )
            );
        }
    }
}