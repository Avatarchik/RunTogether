using System;
using UIScripts.LoginPage;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace UIScripts
{
    public class AppState
    {
        public RequestResultEnum RequestResult;

        #region Register State

        //用户设置的头像
        public string RegisterAvatar;

        //验证码
        public string VerfyCode;

        //用户设置的昵称
        public string NickName;

        //倒计时
        public int CountdownTime;

        //验证码是否已发送
        public bool VerfyCodeWasSent;

        #endregion

        #region Login State

        //密码
        public string Password;

        //账户
        public string Account;
        public string LoginState = "登陆";
        public bool Logined;

        #endregion


        public bool HideSmallLoadingIndicator = true;
        public bool CanGoToVerfyCodePage = false;
        public bool CanGoToUserPage = false;
        public bool CanRegister = false;
        public string AccountTextFieldErrorText;
        public string PasswordTextFieldErrorText;


        public AppState()
        {
            RegisterAvatar = Application.streamingAssetsPath + "/avatar.png";
        }
    }
}