using System;
using UIScripts.LoginPage;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts
{
    public class LoginRegisterBaseState
    {
        public BuildContext Context;
        public SigInOrSignUpOpCodeEnum SigInOrSignUpOpCode;
        public RequestOpCodeEnum RequestOpCode;
    }


    public class SingleStringResult
    {
        public string InputResult;
    }

    public class LoginState : LoginRegisterBaseState
    {
    }

    public class RegisterState : LoginRegisterBaseState
    {
    }

    public struct CountdownState
    {
        public int CountdownTime;
    }

    public class PasswordState:SingleStringResult
    {       
    }

    public class AccountState:SingleStringResult
    {        
    }

    public class SetNickNameState:SingleStringResult
    {        
    }
    public class SetVerfyCodeState:SingleStringResult
    {        
    }
    public class SetRegisterAvatarState
    {
        public string RegisterAvatar;
    }
}