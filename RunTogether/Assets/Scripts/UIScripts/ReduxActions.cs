using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using BestHTTP;
using UIScripts.LoginPage;
using Unit;
using Unity.UIWidgets;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace UIScripts
{
    public abstract class BaseAction
    {
    }


    public class LoginRegisterAction : BaseAction
    {
        public WebServerApiRequest webServerApiRequest;
    }


    public class SingleStringResult : BaseAction
    {
        public string InputResult;
    }

    public class LoginAction : LoginRegisterAction
    {
    }

    public class RegisterAction : LoginRegisterAction
    {
    }

    public class CountdownAction : BaseAction
    {
        public int CountdownTime;
    }

    public class PasswordAction : SingleStringResult
    {
    }

    public class PasswordAgainAction : SingleStringResult
    {
        public string PasswordAgain;
    }

    public class AccountAction : SingleStringResult
    {
    }

    public class SetNickNameAction : SingleStringResult
    {
    }

    public class SetVerfyCodeAction : SingleStringResult
    {
    }

    public class SetRegisterAvatarAction : SingleStringResult
    {
        public string AvatarBase64;
    }


    public class BaseUserInfoAction : BaseAction
    {
        public string Account;
        public string Password;
    }


    public class RegisterUserInfoAction : BaseUserInfoAction
    {
        public string PasswordAgain;
        public string VerfyCode;
        public string NickName;
        public string Avatart;
    }
}