using System;
using System.Collections.Generic;
using BestHTTP;
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
            BaseAction tmpBaseAction = (action) as BaseAction;

            if (tmpBaseAction == null) return state;
            var tmpAppState = state;
            switch (tmpBaseAction.UserOpCode)
            {
                case UserOpCodeEnum.None: break;
                case UserOpCodeEnum.Close:
                    HelperWidgets.PopRoute(tmpBaseAction.Context);
                    break;
                case UserOpCodeEnum.GoToLoginPage:
                    HelperWidgets.PushNewRoute(tmpBaseAction.Context, new LoginPage.LoginPage());
                    break;
                case UserOpCodeEnum.GoToRegisterPage:
                    HelperWidgets.PushNewRoute(tmpBaseAction.Context, new RegisterPage());
                    break;
                case UserOpCodeEnum.SendVerfyCode:
                    tmpAppState.UserOpCode = tmpBaseAction.UserOpCode;
                    CountdownAction tmpCountdownAction = ((CountdownAction) action);
                    tmpAppState.CountdownTime = tmpCountdownAction.CountdownTime;
                    tmpAppState.VerfyCodeWasSent = true;
                    break;
                case UserOpCodeEnum.TypingAccount:
                    AccountAction tmpAccountAction = ((AccountAction) action);
                    tmpAppState.Account = tmpAccountAction.InputResult;
                    tmpAppState.AccountTextFieldErrorText =
                        HelperWidgets.IsCellphoneNumber(tmpAppState.Account)
                        || string.IsNullOrEmpty(tmpAppState.Account)
                            ? null
                            : "";
                    break;
                case UserOpCodeEnum.TypingPassword:
                    PasswordAction tmpPasswordAction = ((PasswordAction) action);
                    tmpAppState.Password = tmpPasswordAction.InputResult;
                    break;
                case UserOpCodeEnum.TypingNickName:
                    SetNickNameAction tmpSetNickNameAction = ((SetNickNameAction) action);
                    tmpAppState.NickName = tmpSetNickNameAction.InputResult;
                    break;
                case UserOpCodeEnum.TypingVerfyCode:
                    SetVerfyCodeAction tmpSetVerfyCodeAction = ((SetVerfyCodeAction) action);
                    tmpAppState.VerfyCode = tmpSetVerfyCodeAction.InputResult;
                    break;
                case UserOpCodeEnum.SetupAvatar:
                    SetRegisterAvatarAction tmpSetRegisterAvatarAction = ((SetRegisterAvatarAction) action);
                    tmpAppState.RegisterAvatar = tmpSetRegisterAvatarAction.InputResult;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (tmpBaseAction.RequestOpCode)
            {
                case RequestOpCodeEnum.None:
                    break;
                case RequestOpCodeEnum.RequestLogin:
                    tmpAppState.LoginState = "登陆中";
                    LoginAction tmpLoginAction = (tmpBaseAction as LoginAction);
                    tmpLoginAction?.Request();
                    tmpAppState.RequestResult = RequestResultEnum.LoginSuccessed;
                    tmpAppState.HideCircularProgressIndicator = false;
                    break;
                case RequestOpCodeEnum.RequestRegister:
                    RegisterAction tmpRegisterAction = (tmpBaseAction as RegisterAction);
                    tmpRegisterAction?.Request();
                    break;
                case RequestOpCodeEnum.RequestVerfyCode:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (tmpBaseAction.RequestResult)
            {
                case RequestResultEnum.None:
                    break;
                case RequestResultEnum.LoginSuccessed:
                    tmpAppState.LoginState = "登陆成功";
                    tmpAppState.HideCircularProgressIndicator = true;
                    HelperWidgets.PopRoute(tmpBaseAction.Context);
                    break;
                case RequestResultEnum.LoginFailed:
                    tmpAppState.LoginState = "登陆";
                    tmpAppState.HideCircularProgressIndicator = true;
                    break;
                case RequestResultEnum.VerfyCodeSuccessed:
                    break;
                case RequestResultEnum.VerfyCodeFailed:
                    break;
                case RequestResultEnum.RegisterSuccessed:
                    break;
                case RequestResultEnum.RegisterFailed:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

//       


            return tmpAppState;
        }
    }
}