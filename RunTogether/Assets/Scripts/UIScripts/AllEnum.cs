namespace UIScripts
{
    public enum UserOpCodeEnum
    {
        None,
        Close,
        GoToLoginPage,
        GoToRegisterPage,
        TypingAccount,
        TypingPassword,
        TypingNickName,
        TypingVerfyCode,
        SendVerfyCode,
        SetupAvatar,
    }

    public enum RequestOpCodeEnum
    {
        None,
        RequestLogin,
        RequestRegister,
        RequestVerfyCode,
    }

    public enum RequestResultEnum
    {
        None,
        LoginSuccessed,
        LoginFailed,

        VerfyCodeSuccessed,
        VerfyCodeFailed,

        RegisterSuccessed,
        RegisterFailed,
    }

    public enum PageStateEnum
    {
        WelcomePage,
        LoginPage,
        RegisterPage,
        MainPage,
    }
}