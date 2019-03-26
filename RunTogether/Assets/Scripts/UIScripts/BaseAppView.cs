using System.Collections.Generic;
using UIScripts.Profiles;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.gestures;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts
{
    public class BaseAppViewState : SingleTickerProviderStateMixin<BaseAppView>
    {
        int currentSelected = 0;
        private List<Widget> pages = new List<Widget>();

        public override void initState()
        {
            base.initState();

            pages.Add(new Container(color: Colors.transparent));
            pages.Add(new FriendListWidgets());
            pages.Add(new MomentWidgets());
            pages.Add(new PersonalProfile());
        }

        public override Widget build(BuildContext context)
        {
            return _BuildBaseElemtns();
        }

        private Widget _BuildBaseElemtns()
        {
            return new Scaffold(
                backgroundColor: Color.clear,
                body: pages[currentSelected],
                bottomNavigationBar: new Material(
                    color: Colors.white,
                    child: new Container(
                        child: new BottomNavigationBar(
                            currentIndex: currentSelected,
                            onTap: OnBottomNavigationItemPressed,
                            items: new List<BottomNavigationBarItem>
                            {
                                new BottomNavigationBarItem(title: new Text("Run"),
                                    icon: new Icon(icon: Icons.run_outline),
                                    activeIcon: new Icon(icon: Icons.run_fill)),
                                new BottomNavigationBarItem(title: new Text("Friend"), icon: new Icon(icon: Icons.list),
                                    activeIcon: new Icon(icon: Icons.list)),
                                new BottomNavigationBarItem(title: new Text("Moment"),
                                    icon: new Icon(icon: Icons.moment_outline),
                                    activeIcon: new Icon(icon: Icons.moment_fill)),
                                new BottomNavigationBarItem(title: new Text("Profile"),
                                    icon: new Icon(icon: Icons.profile_outline),
                                    activeIcon: new Icon(icon: Icons.profile_fill)),
                            },
                            type: BottomNavigationBarType.fix
                        )
                    )
                )
            );
        }

        private void OnBottomNavigationItemPressed(int _index)
        {
            currentSelected = _index;
            this.setState();
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