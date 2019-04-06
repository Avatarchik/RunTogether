using UIScripts.LoginPage;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts
{
    public class LoginState
    {
        public bool ClickedLogin;
        public BuildContext Context;
    }

    public class RegisterState
    {
        public bool ClickedRegister;
        public BuildContext Context;
    }

    public class CountdownState
    {
        public int Countdown;
    }

    public struct SendVerfyCodeState
    {
        public bool SendVerfyCode;
    }
}