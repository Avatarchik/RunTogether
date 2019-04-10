using System;
using UIScripts.LoginPage;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace UIScripts
{
    public class AppState
    {
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

        public string FialedMsg;
        public float RegisterOrLoginButtonOpacity = 0.6f;

        #endregion

        #region User Option State

        public UserOpCodeEnum UserOpCode;
        public RequestOpCodeEnum RequestOpCode;
        public RequestResultEnum RequestResult;

        #endregion


        public BuildContext buildContext;

        public AppState()
        {
            RegisterAvatar = Application.streamingAssetsPath + "/avatar.png";
        }

        public AppState Clone()
        {
            return new AppState
            {
                RegisterOrLoginButtonOpacity = RegisterOrLoginButtonOpacity,
                UserOpCode = UserOpCode,
                RequestOpCode = RequestOpCode,
                RequestResult = RequestResult,
                buildContext = buildContext,
            };
        }
    }
}