using System;
using System.Collections.Generic;
using System.Text;
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
        private readonly Action<string> SuccessedCallback;
        private readonly Action<string> FailedCallback;
        private readonly Dictionary<string, string> RequestParamaters;

        public LoginRegisterAction(Dictionary<string, string> parmaters, Action<string> successedCallback = null,
            Action<string> failedCallback = null)
        {
            RequestParamaters = parmaters;
            SuccessedCallback = successedCallback;
            FailedCallback = failedCallback;
        }


        public void Request(HTTPMethods httpMethods = HTTPMethods.Post)
        {
            Debug.Assert(RequestParamaters.ContainsKey("url"), "Paramaters is no contain url key");
            HTTPRequest tmpRequest = new HTTPRequest(new Uri(RequestParamaters["url"]), httpMethods, OnFinished);

            if (httpMethods == HTTPMethods.Post)
            {
                string tmpSign = string.Empty;
                //建立post 表单
                foreach (KeyValuePair<string, string> filed in RequestParamaters)
                {
                    if (filed.Key == "url") continue;

                    tmpRequest.AddField(filed.Key, filed.Value);
                    //将字符串转为url code
                    tmpSign += filed.Key + "=" + System.Web.HttpUtility.UrlEncode(filed.Value) + "&";
                }

                //设置签名
                tmpSign = tmpSign.Remove(tmpSign.LastIndexOf('&'));
                tmpSign = HelperWidgets.EncryptString(tmpSign + "Runtogether2018");
                tmpRequest.AddField("sign", tmpSign);
//                Debug.Log(tmpSign);
            }

            tmpRequest.Send();
        }

        private void OnFinished(HTTPRequest originalrequest, HTTPResponse response)
        {
            if (response == null) return;
            if (response.IsSuccess)
            {
                SuccessedCallback?.Invoke(response.DataAsText);
            }
            else
            {
                FailedCallback?.Invoke(response.DataAsText);
            }
        }
    }


    public class SingleStringResult : BaseAction
    {
        public string InputResult;
    }

    public class LoginAction : LoginRegisterAction
    {
        public LoginAction(Dictionary<string, string> parmaters = null, Action<string> successedCallback = null,
            Action<string> failedCallback = null) : base(parmaters, successedCallback, failedCallback)
        {
        }
    }

    public class RegisterAction : LoginRegisterAction
    {
        public RegisterAction(Dictionary<string, string> parmaters = null, Action<string> successedCallback = null,
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