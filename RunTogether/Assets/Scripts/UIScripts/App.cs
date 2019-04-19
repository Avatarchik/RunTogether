using System;
using System.Collections.Generic;
using BestHTTP;
using Datas;
using UIScripts.LoginPage;
using UIScripts.RunPage;
using Unity.UIWidgets;
using Unity.UIWidgets.animation;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace UIScripts
{
    public class App : UIWidgetsPanel
    {
        protected override Widget createWidget()
        {
            var tmpStore = new Store<AppState>(Reducer, new AppState());
            return new StoreProvider<AppState>(tmpStore, child: new MaterialApp(
                    showPerformanceOverlay: false,
                    home: new Home()
                )
            );
        }

        protected PageRouteFactory pageRouteBuilder
        {
            get
            {
                return (RouteSettings settings, WidgetBuilder builder) =>
                    new PageRouteBuilder(
                        settings: settings,
                        pageBuilder: (BuildContext context, Animation<float> animation,
                            Animation<float> secondaryAnimation) => builder(context),
                        transitionsBuilder: (BuildContext context, Animation<float>
                                animation, Animation<float> secondaryAnimation, Widget child) =>
                            new _FadeUpwardsPageTransition(
                                routeAnimation: animation,
                                child: child
                            )
                    );
            }
        }

        protected override void OnEnable()
        {
            Application.targetFrameRate = 60;
            base.OnEnable();
            FontManager.instance.addFont(Resources.Load<Font>(path: "MaterialIcons-Regular"),
                familyName: "Material Icons");
            FontManager.instance.addFont(Resources.Load<Font>(path: "MaterialIcons-Expand"),
                familyName: "Material Icons expand");
        }


        private AppState Reducer(AppState state, object action)
        {
            BaseAction tmpBaseAction = (action) as BaseAction;
            if (tmpBaseAction == null) return state;

            AppState tmpAppState = new AppState();


            if (tmpBaseAction.GetType() == typeof(RegisterAction))
            {
                RegisterAction tmpRegisterAction = (tmpBaseAction as RegisterAction);
                tmpRegisterAction?.webServerApiRequest.Request();
            }
            else if (tmpBaseAction.GetType() == typeof(LoginAction))
            {
                LoginAction tmpLoginAction = (tmpBaseAction as LoginAction);
                if (tmpLoginAction?.webServerApiRequest.RequestResult == RequestResultEnum.LoginSuccessed)
                    tmpAppState.Logined = true;
                else
                    tmpLoginAction?.webServerApiRequest.Request();
            }
            else if (tmpBaseAction.GetType() == typeof(AccountAction))
            {
                AccountAction tmpAccountAction = (tmpBaseAction as AccountAction);
                tmpAppState.Account = tmpAccountAction.InputResult;
            }
            else if (tmpBaseAction.GetType() == typeof(PasswordAction))
            {
                PasswordAction tmpPasswordAction = (tmpBaseAction as PasswordAction);
                tmpAppState.Password = tmpPasswordAction.InputResult;
            }

            return tmpAppState;
        }
    }
}