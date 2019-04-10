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
            BaseState tmpBaseState = (action) as BaseState;
            if (tmpBaseState == null) return state;
            var tmpAppState = state;
            switch (tmpBaseState.UserOpCode)
            {
                case UserOpCodeEnum.None: break;
                case UserOpCodeEnum.Close:
                    HelperWidgets.PopRoute(tmpBaseState.Context);
                    break;
                case UserOpCodeEnum.GoToLoginPage:
                    HelperWidgets.PushNewRoute(tmpBaseState.Context, new LoginPage.LoginPage());
                    break;
                case UserOpCodeEnum.GoToRegisterPage:
                    HelperWidgets.PushNewRoute(tmpBaseState.Context, new RegisterPage());
                    break;
                case UserOpCodeEnum.SendVerfyCode:
                    tmpAppState.UserOpCode = tmpBaseState.UserOpCode;
                    CountdownState tmpCountdownState = ((CountdownState) action);
                    tmpAppState.CountdownTime = tmpCountdownState.CountdownTime;
                    tmpAppState.VerfyCodeWasSent = true;
                    break;
                case UserOpCodeEnum.TypingAccount:
                    AccountState tmpAccountState = ((AccountState) action);
                    tmpAppState.Account = tmpAccountState.InputResult;
                    break;
                case UserOpCodeEnum.TypingPassword:
                    PasswordState tmpPasswordState = ((PasswordState) action);
                    tmpAppState.Password = tmpPasswordState.InputResult;
                    break;
                case UserOpCodeEnum.TypingNickName:
                    SetNickNameState tmpSetNickNameState = ((SetNickNameState) action);
                    tmpAppState.NickName = tmpSetNickNameState.InputResult;
                    break;
                case UserOpCodeEnum.TypingVerfyCode:
                    SetVerfyCodeState tmpSetVerfyCodeState = ((SetVerfyCodeState) action);
                    tmpAppState.VerfyCode = tmpSetVerfyCodeState.InputResult;
                    break;
                case UserOpCodeEnum.SetupAvatar:
                    SetRegisterAvatarState tmpSetRegisterAvatarState = ((SetRegisterAvatarState) action);
                    tmpAppState.RegisterAvatar = tmpSetRegisterAvatarState.InputResult;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            switch (tmpBaseState.RequestResult)
            {
                case RequestResultEnum.None:
                    break;
                case RequestResultEnum.LoginSuccessed:
                    HelperWidgets.PopRoute(tmpBaseState.Context);
                    HelperWidgets.PopRoute(tmpBaseState.Context);
                    HelperWidgets.PushNewRoute(tmpBaseState.Context, new MainPage());
                    break;
                case RequestResultEnum.LoginFailed:
                    state.FialedMsg = tmpBaseState.FailedMsg;
                    break;
                case RequestResultEnum.VerfyCodeFailed:
                    state.FialedMsg = tmpBaseState.FailedMsg;
                    break;
                case RequestResultEnum.RegisterFailed:
                    state.FialedMsg = tmpBaseState.FailedMsg;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return tmpAppState;
        }
    }
}