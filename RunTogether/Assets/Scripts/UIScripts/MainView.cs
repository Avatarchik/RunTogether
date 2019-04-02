using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts
{
    public class MainView:StatefulWidget
    {
        public override State createState()
        {
            return new MainViewState();
        }
    }

    public class MainViewState : State<MainView>
    {
        private readonly List<Widget> Pages = new List<Widget>();
        private int currentSelected = 0;

        public override void initState()
        {
            base.initState();
            
                        
            Pages.Add(new RunPage.RunPage());
            Pages.Add(new FriendPage.FriendPage());
            Pages.Add(new MomentPage.MomentPage());
            Pages.Add(new ProfilePage.ProfilePage());
        }

        public override Widget build(BuildContext context)
        {
            return _BuildBaseElemtns();
        }
        
        private Widget _BuildBaseElemtns()
        {
            return new Scaffold(
                backgroundColor: Color.clear,
                body: Pages[currentSelected],
                bottomNavigationBar: new Material(
                    color: Colors.white,
                    child: new Container(
                        child: new CustomBottomNavigationBar(
                            currentIndex: currentSelected,
                            onTap: OnBottomNavigationItemPressed,
                            items: new List<BottomNavigationBarItem>
                            {
                                new BottomNavigationBarItem(title: new Text("跑步"),
                                    icon: new Icon(icon: IconsExtern.run_outline),
                                    activeIcon: new Icon(icon: IconsExtern.run_fill)),
                                new BottomNavigationBarItem(title: new Text("朋友"), icon: new Icon(icon: Icons.list),
                                    activeIcon: new Icon(icon: Icons.list)),
                                new BottomNavigationBarItem(title: new Text("瞬间"),
                                    icon: new Icon(icon: IconsExtern.moment_outline),
                                    activeIcon: new Icon(icon: IconsExtern.moment_fill)),
                                new BottomNavigationBarItem(title: new Text("我的"),
                                    icon: new Icon(icon: IconsExtern.profile_outline),
                                    activeIcon: new Icon(icon: IconsExtern.profile_fill)),
                            },
                            type: BottomNavigationBarType.fix
                        )
                    )
                )
            );
        }

        private void OnBottomNavigationItemPressed(int index)
        {
            currentSelected = index;
            setState();
        }
    }
}