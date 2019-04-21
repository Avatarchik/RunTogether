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
                switch (tmpLoginAction?.webServerApiRequest.RequestResult)
                {
                    case RequestResultEnum.LoginSuccessed:
                        tmpAppState.Logined = true;
                        tmpAppState.HideCircularProgressIndicator = true;
                        break;
                    case RequestResultEnum.LoginFailed:
                        tmpAppState.HideCircularProgressIndicator = true;
                        tmpAppState.IsInteractableOfButton = true;
                        break;
                    default:
                        tmpLoginAction?.webServerApiRequest.Request();
                        tmpAppState.HideCircularProgressIndicator = false;
                        tmpAppState.IsInteractableOfButton = false;
                        break;
                }
            }
            else if (tmpBaseAction.GetType() == typeof(BaseUserInfoAction))
            {
                BaseUserInfoAction tmpAccountAction = (tmpBaseAction as BaseUserInfoAction);
                tmpAppState.Account = tmpAccountAction.Account;
                tmpAppState.IsInteractableOfButton = HelperWidgets.IsCellphoneNumber(tmpAccountAction.Account);
            }

            else if (tmpBaseAction.GetType() == typeof(RegisterUserInfoAction))
            {
                RegisterUserInfoAction tmpRegisterUserInfoAction = (tmpBaseAction as RegisterUserInfoAction);
                tmpAppState.Account = tmpRegisterUserInfoAction?.Account;
                tmpAppState.Password = tmpRegisterUserInfoAction?.Password;
                tmpAppState.VerfyCode = tmpRegisterUserInfoAction?.VerfyCode;
                tmpAppState.NickName = tmpRegisterUserInfoAction?.NickName;
                if (!string.IsNullOrEmpty(tmpRegisterUserInfoAction.Account))
                    tmpAppState.IsInteractableOfButton =
                        HelperWidgets.IsCellphoneNumber(tmpRegisterUserInfoAction?.Account);
                tmpAppState.PasswordTextFieldErrorText =
                    String.Compare(tmpRegisterUserInfoAction?.Password,
                        tmpRegisterUserInfoAction?.PasswordAgain, StringComparison.Ordinal) != 0
                        ? "两次输入密码不一致"
                        : null;
            }

            return tmpAppState;
        }
    }
}