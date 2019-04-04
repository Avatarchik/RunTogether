using System.Collections.Generic;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.widgets;
using Datas;
using UIScripts.LoginPage;
using Unity.UIWidgets;

namespace UIScripts
{
    public class BaseAppViewState : State<BaseAppView>
    {        
        private readonly List<Widget> Views = new List<Widget>();

        public override void initState()
        {
            base.initState();
            Views.Add(new LoginPage.WelcomeWidgets());
            Views.Add(new MainView());
            AppManager.Instance.SetLoginState(true);
        }

        public override Widget build(BuildContext buildContext)
        {
            return AppManager.Instance.WasLogined ? Views[1] : Views[0];
        }
    }

    public class BaseAppView : StatefulWidget
    {
        public BaseAppView(Key key = null) : base(key)
        {
        }

        public override State createState()
        {
            return new BaseAppViewState();
        }
    }
}