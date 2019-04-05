using System.Collections.Generic;
using Unity.UIWidgets.widgets;
using Datas;
using UIScripts.LoginPage;
using Unity.UIWidgets;
using Unity.UIWidgets.Redux;

namespace UIScripts
{
    public class Home : StatelessWidget
    {
        private readonly List<Widget> Views = new List<Widget>();

        public Home()
        {
            Views.Add(new WelcomeWidgets());
            Views.Add(new MainView());
            AppManager.Instance.SetLoginState(true);
        }

        public override Widget build(BuildContext buildContext)
        {
            var tmp_Store = new Store<AppState>(Reduce, new AppState(), ReduxLogging.create<AppState>());
//            return AppManager.Instance.WasLogined ? Views[1] : Views[0];
            return new StoreProvider<AppState>(tmp_Store, child: new WelcomeWidgets());
        }


        private AppState Reduce(AppState state, object action)
        {
            return new AppState();
        }
    }
}