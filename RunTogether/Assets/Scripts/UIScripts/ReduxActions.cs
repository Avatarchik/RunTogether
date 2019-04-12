using System;
using System.Collections.Generic;
using BestHTTP;
using UIScripts.LoginPage;
using Unity.UIWidgets;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace UIScripts
{
    public abstract class BaseAction
    {
        public UserOpCodeEnum UserOpCode;
        public RequestOpCodeEnum RequestOpCode;
        public RequestResultEnum RequestResult;
        public PageStateEnum PageState;
        public BuildContext Context;
        public string FailedMsg;
    }


    public class LoginRegisterAction : BaseAction
    {
        private readonly Action SuccessedCallback;
        private readonly Action<string> FailedCallback;
        private readonly Dictionary<string, string> RequestParamaters;

        public LoginRegisterAction(Dictionary<string, string> parmaters, Action successedCallback = null,
            Action<string> failedCallback = null)
        {
            RequestParamaters = parmaters;
            SuccessedCallback = successedCallback;
            FailedCallback = failedCallback;
        }


        internal void Request()
        {
            Debug.Log("Request");
            Debug.Assert(RequestParamaters.ContainsKey("url"), "Paramaters is no contain url key");
            HTTPRequest tmpRequest = new HTTPRequest(new Uri(RequestParamaters["url"]), OnFinished);
            tmpRequest.Send();
        }

        private void OnFinished(HTTPRequest originalrequest, HTTPResponse response)
        {
            if (response == null) return;
            if (response.IsSuccess)
            {
                SuccessedCallback?.Invoke();
            }
            else
            {
                FailedCallback?.Invoke(response.Message);
            }
        }
    }


    public class SingleStringResult : BaseAction
    {
        public string InputResult;
    }

    public class LoginAction : LoginRegisterAction
    {
        public LoginAction(Dictionary<string, string> parmaters = null, Action successedCallback = null,
            Action<string> failedCallback = null) : base(parmaters, successedCallback, failedCallback)
        {
        }
    }

    public class RegisterAction : LoginRegisterAction
    {
        public RegisterAction(Dictionary<string, string> parmaters = null, Action successedCallback = null,
            Action<string> failedCallback = null) : base(parmaters, successedCallback, failedCallback)
        {
        }
    }

    public class CountdownAction : BaseAction
    {
        public int CountdownTime;
    }

    public class PasswordAction : SingleStringResult
    {
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
    }
}