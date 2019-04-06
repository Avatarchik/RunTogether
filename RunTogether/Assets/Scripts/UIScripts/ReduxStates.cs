using UIScripts.LoginPage;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts
{
    public class LoginRegisterBaseState
    {
        public bool ClickedNextButton;
        public bool Successed;
        public bool Falied;
        public BuildContext Context;
    }

    public class LoginState : LoginRegisterBaseState
    {
    }

    public class RegisterState : LoginRegisterBaseState
    {
    }

    public class CountdownState
    {
        public int CountdownTime;
    }

    public struct SendVerfyCodeState
    {
        public bool SendVerfyCode;
    }
}