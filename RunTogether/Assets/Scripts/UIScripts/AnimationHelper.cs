using Unity.UIWidgets.animation;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts
{
    class PageTransition : StatelessWidget {
                
        private  readonly Animatable<float> FastOutSlowInTween = new CurveTween(curve: Curves.fastOutSlowIn);
        private  readonly Animatable<float> EaseInTween = new CurveTween(curve: Curves.fastOutSlowIn);

        private readonly Animation<Offset> PositionAnimation;
        private readonly Animation<float> OpacityAnimation;
        private readonly Widget Child;
        
        internal PageTransition(
            Key key = null,
            Animation<float> routeAnimation = null, // The route's linear 0.0 - 1.0 animation.
            Offset beginDirection=null,
            Offset endDirection=null,
            Widget child = null
        
        ) : base(key: key) {
            this.PositionAnimation = new OffsetTween(begin: beginDirection,end: endDirection).chain(FastOutSlowInTween).animate(routeAnimation);
            this.OpacityAnimation = EaseInTween.animate(routeAnimation);
            this.Child = child;
        }

        public override Widget build(BuildContext context) {
            return new SlideTransition(
                position: this.PositionAnimation,
                child: new FadeTransition(
                    opacity: this.OpacityAnimation,
                    child: this.Child
                )
            );
        }
    }
}