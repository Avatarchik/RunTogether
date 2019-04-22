using Unity.UIWidgets.material;
using Unity.UIWidgets.widgets;

namespace UIScripts.RegisterPage
{
    public abstract class RegisterPageBase:StatelessWidget
    {
        internal RegisterUserInfoAction RegisterUserInfoAction;

        internal RegisterPageBase(RegisterUserInfoAction registerUserInfoAction)
        {
            RegisterUserInfoAction = registerUserInfoAction;
        }
        public override Widget build(BuildContext context)
        {
            return null;
        }
    }
}