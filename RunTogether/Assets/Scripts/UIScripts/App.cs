using System;
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
            var tmpStore = new Store<AppState>(Reducer, new AppState());
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
                    RegisterLoginAction(state, ((LoginState) action));
                    break;
                case "RegisterState":
                    RegisterLoginAction(state, ((RegisterState) action));
                    break;
                case "CountdownState":
                    CountdownState tmpCountdownState = ((CountdownState) action);
                    state.CountdownTime = tmpCountdownState.CountdownTime;
                    break;
                case "AccountState":
                    AccountState tmpAccountState = ((AccountState) action);
                    state.Account = tmpAccountState.InputResult;
                    break;
                case "PasswordState":
                    PasswordState tmpPasswordState = ((PasswordState) action);
                    state.Password = tmpPasswordState.InputResult;
                    break;
                case "SetAvatarState":
                    SetRegisterAvatarState tmpSetRegisterAvatarState = ((SetRegisterAvatarState) action);
                    state.RegisterAvatar = tmpSetRegisterAvatarState.RegisterAvatar;
                    break;
                case "SetNickNameState":
                    SetNickNameState tmpSetNickNameState = ((SetNickNameState) action);
                    state.NickName = tmpSetNickNameState.InputResult;
                    break;
                case "SetVerfyCodeState":
                    SetVerfyCodeState tmpSetVerfyCodeState = ((SetVerfyCodeState) action);
                    state.VerfyCode = tmpSetVerfyCodeState.InputResult;
                    break;
            }


            return state;
        }


        private void RegisterLoginAction(AppState state, LoginRegisterBaseState registerLoginState)
        {
            state.SigInOrSignUpOpCode = registerLoginState.SigInOrSignUpOpCode;
            state.RequestOpCode = registerLoginState.RequestOpCode;

            switch (registerLoginState.SigInOrSignUpOpCode)
            {
                case SigInOrSignUpOpCodeEnum.None: break;
                case SigInOrSignUpOpCodeEnum.Close:
                    HelperWidgets.PopRoute(registerLoginState.Context);
                    break;
                case SigInOrSignUpOpCodeEnum.GoToLoginPage:
                    HelperWidgets.PushNewRoute(registerLoginState.Context, new LoginPage.LoginPage());
                    break;
                case SigInOrSignUpOpCodeEnum.GoToRegisterPage:
                    HelperWidgets.PushNewRoute(registerLoginState.Context, new RegisterPage());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (registerLoginState.RequestOpCode)
            {
                case RequestOpCodeEnum.None:
                    break;
                case RequestOpCodeEnum.RequestLogin:
                    //TODO:登陆请求
                    break;
                case RequestOpCodeEnum.RequestRegister:
                    //TODO:注册请求
                    break;
                case RequestOpCodeEnum.RequestVerfyCode:
                    //TODO:发送验证码请求
                    Debug.Log("Sent! " + state.RequestOpCode);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}