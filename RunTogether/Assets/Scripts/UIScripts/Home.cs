using System;
using System.Collections.Generic;
using Unity.UIWidgets.widgets;
using Datas;
using UIScripts.LoginPage;
using Unity.UIWidgets.Redux;

namespace UIScripts
{
    public class Home : StatelessWidget
    {
        private readonly List<Widget> Views = new List<Widget>();
        public readonly StoreProvider<AppState> StoreProvider;
        private BuildContext BuildContext;

        public Home()
        {
            Views.Add(new WelcomePage());
            Views.Add(new MainView());
//            AppManager.Instance.SetLoginState(true);
        }

        public override Widget build(BuildContext buildContext)
        {
//            return AppManager.Instance.WasLogined ? Views[1] : Views[0];
            return new WelcomePage();            
        }
    }
}