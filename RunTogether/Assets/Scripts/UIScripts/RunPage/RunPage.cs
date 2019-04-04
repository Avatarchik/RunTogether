using System;
using System.Collections.Generic;
using Unity.UIWidgets.animation;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts.RunPage
{
    public class RunPage : StatefulWidget
    {
        public override State createState()
        {
            return new RunPageState();
        }
    }

    public class RunPageState : TickerProviderStateMixin<RunPage>
    {
        internal int PagesIndex;
        internal readonly AnimationController PositionController;

        
        private readonly List<Widget> Pages = new List<Widget>();
        private readonly Animation<float> PositionAnimation;

        public RunPageState()
        {
            PositionController = new AnimationController(vsync: this, duration: new TimeSpan(0, 0, 0, 0, 300));
            PositionAnimation = new FloatTween(begin: 0, end: 1).animate(PositionController);

            Pages.Add(new RunFloatingButtonWidget(this));
            Pages.Add(new PageTransition(routeAnimation: PositionAnimation, beginDirection: new Offset(0, 5),
                endDirection: new Offset(0, 0), child: new MatchWidget(this)));
        }

        public override Widget build(BuildContext context)
        {
            return Pages[PagesIndex];
        }

        public void Refresh()
        {
            setState();
        }
    }
}