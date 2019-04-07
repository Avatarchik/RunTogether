using System;
using UIScripts.LoginPage;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace UIScripts
{
    public class AppState
    {
        public int CountdownTime;
     
        public string Password;
        public string Account;
        public string VerfyCode;
        public string NickName;
        public bool WasLogined;
        public string RegisterAvatar;

        public SigInOrSignUpOpCodeEnum SigInOrSignUpOpCode;
        public RequestOpCodeEnum RequestOpCode;

        public AppState()
        {
            RegisterAvatar = Application.streamingAssetsPath + "/avatar.png";
        }
    }
}