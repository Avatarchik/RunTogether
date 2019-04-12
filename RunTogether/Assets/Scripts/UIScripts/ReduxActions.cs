using System;
using BestHTTP;
using UIScripts.LoginPage;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts
{
    public abstract class BaseState
    {
        public UserOpCodeEnum UserOpCode;
        public RequestOpCodeEnum RequestOpCode;
        public RequestResultEnum RequestResult;
        public PageStateEnum PageState;
        public BuildContext Context;
        public string FailedMsg;
    }


    public abstract class LoginRegisterBaseState : BaseState
    {
    }


    public class SingleStringResult : BaseState
    {
        public string InputResult;
    }

    public class LoginState : LoginRegisterBaseState
    {
        public void Request(string account,string password)
        {
            HTTPRequest tmpRequest = new HTTPRequest(new Uri(""),OnFinished);
            tmpRequest.Send();
        }

        private void OnFinished(HTTPRequest originalrequest, HTTPResponse response)
        {
            
        }
    }

    public class RegisterState : LoginRegisterBaseState
    {
        
    }

    public class CountdownState : BaseState
    {
        public int CountdownTime;
    }

    public class PasswordState : SingleStringResult
    {
    }

    public class AccountState : SingleStringResult
    {
    }

    public class SetNickNameState : SingleStringResult
    {
    }

    public class SetVerfyCodeState : SingleStringResult
    {
    }

    public class SetRegisterAvatarState : SingleStringResult
    {
    }
}