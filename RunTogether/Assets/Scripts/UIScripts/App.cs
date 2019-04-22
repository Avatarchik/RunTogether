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
                switch (tmpRegisterAction?.webServerApiRequest.RequestResult)
                {
                    case RequestResultEnum.VerfyCodeSuccessed:
                        break;
                    case RequestResultEnum.VerfyCodeFailed:
                        break;
                    case RequestResultEnum.RegisterSuccessed:
                        tmpAppState.HideSmallLoadingIndicator = true;
                        break;
                    case RequestResultEnum.RegisterFailed:
                        tmpAppState.CanRegister = true;
                        tmpAppState.HideSmallLoadingIndicator = true;
                        break;
                    default:
                        tmpAppState.CanRegister = false;
                        tmpAppState.HideSmallLoadingIndicator = false;
                        tmpRegisterAction?.webServerApiRequest?.Request();
                        break;
                }
            }
            else if (tmpBaseAction.GetType() == typeof(LoginAction))
            {
                LoginAction tmpLoginAction = (tmpBaseAction as LoginAction);
                switch (tmpLoginAction?.webServerApiRequest.RequestResult)
                {
                    case RequestResultEnum.LoginSuccessed:
                        tmpAppState.Logined = true;
                        tmpAppState.HideSmallLoadingIndicator = true;
                        break;
                    case RequestResultEnum.LoginFailed:
                        tmpAppState.HideSmallLoadingIndicator = true;
                        tmpAppState.CanGoToVerfyCodePage = true;
                        break;
                    default:
                        tmpLoginAction?.webServerApiRequest.Request();
                        tmpAppState.HideSmallLoadingIndicator = false;
                        tmpAppState.CanGoToVerfyCodePage = false;
                        break;
                }
            }
            else if (tmpBaseAction.GetType() == typeof(BaseUserInfoAction))
            {
                BaseUserInfoAction tmpAccountAction = (tmpBaseAction as BaseUserInfoAction);
                tmpAppState.Account = tmpAccountAction.Account;
                tmpAppState.CanGoToVerfyCodePage = HelperWidgets.IsCellphoneNumber(tmpAccountAction.Account);
            }

            else if (tmpBaseAction.GetType() == typeof(RegisterUserInfoAction))
            {
                RegisterUserInfoAction tmpRegisterUserInfoAction = (tmpBaseAction as RegisterUserInfoAction);                            
                
                tmpAppState.Account = tmpRegisterUserInfoAction?.Account;
                tmpAppState.Password = tmpRegisterUserInfoAction?.Password;
                tmpAppState.VerfyCode = tmpRegisterUserInfoAction?.VerfyCode;
                tmpAppState.NickName = tmpRegisterUserInfoAction?.NickName;
                tmpAppState.Avatar = tmpRegisterUserInfoAction?.Avatart;


                tmpAppState.PasswordTextFieldErrorText =
                    String.Compare(tmpRegisterUserInfoAction?.Password,
                        tmpRegisterUserInfoAction?.PasswordAgain, StringComparison.Ordinal) != 0
                        ? "两次输入密码不一致"
                        : null;

                if (tmpRegisterUserInfoAction.IsPop)
                {
                    tmpAppState.CanRegister = tmpRegisterUserInfoAction.CanRegister;
                    tmpAppState.CanGoToUserPage = tmpRegisterUserInfoAction.CanGoToUserPage;
                    tmpAppState.CanGoToVerfyCodePage = tmpRegisterUserInfoAction.CanGoToVerfyCodePage;
                }
                else
                {
                    tmpAppState.CanGoToUserPage = !string.IsNullOrEmpty(tmpRegisterUserInfoAction?.VerfyCode);
                    if (!string.IsNullOrEmpty(tmpRegisterUserInfoAction.Account)
                        && !string.IsNullOrEmpty(tmpRegisterUserInfoAction.Password)
                        && string.IsNullOrEmpty(tmpAppState.PasswordTextFieldErrorText))
                        tmpAppState.CanGoToVerfyCodePage =
                            HelperWidgets.IsCellphoneNumber(tmpRegisterUserInfoAction?.Account);


                    if (!string.IsNullOrEmpty(tmpAppState.NickName)
                        && tmpAppState.CanGoToUserPage
                        && tmpAppState.CanGoToVerfyCodePage)
                    {
                        tmpAppState.CanRegister = true;
                    }
                }
            }

            return tmpAppState;
        }
    }
}