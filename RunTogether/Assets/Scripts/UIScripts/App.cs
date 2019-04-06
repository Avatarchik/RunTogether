using Datas;
using UIScripts.LoginPage;
using Unity.UIWidgets;
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
            var tmpStore = new Store<AppState>(Reducer, new AppState(), ReduxLogging.create<AppState>());
            return new StoreProvider<AppState>(tmpStore, child: new MaterialApp(
                    showPerformanceOverlay: false,
                    home: new Home()
                )
            );
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
            var type = action.GetType().Name;
            switch (type)
            {
                case "LoginState":
                    LoginState tmpLoginState = ((LoginState) action);
                    if (tmpLoginState.ClickedLogin)
                    {
                        state.PushNewRoute(tmpLoginState.Context, new LoginPage.LoginPage());
                    }
                    else
                    {
                        state.PopRoute(tmpLoginState.Context);
                    }

                    break;
                case "RegisterState":
                    RegisterState tmpRegisterState = ((RegisterState) action);
                    if (tmpRegisterState.ClickedRegister)
                    {
                        state.PushNewRoute(tmpRegisterState.Context, new RegisterPage());
                    }
                    else
                    {
                        state.PopRoute(tmpRegisterState.Context);
                    }

                    break;
                case "CountdownState":
                    CountdownState tmpCountdownState = ((CountdownState) action);
                    state.CountdownString = tmpCountdownState.Countdown;
                    break;
                case "SendVerfyCodeState":
                    SendVerfyCodeState tmpSendVerfyCodeState = ((SendVerfyCodeState) action);
                    state.SendVerfyCode = tmpSendVerfyCodeState.SendVerfyCode;
                    break;
            }


            return state;
        }
    }
}