using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.widgets;

namespace UIScripts.RunPage
{
    public class RunFloatingButtonWidget : StatefulWidget
    {
        internal readonly RunPageState RunPage;

        public RunFloatingButtonWidget(RunPageState runPage)
        {
            RunPage = runPage;
        }

        public override State createState()
        {
            return new RunFloatingButtonState();
        }
    }

    public class RunFloatingButtonState : State<RunFloatingButtonWidget>
    {
        public override Widget build(BuildContext context)
        {
            return new Container(alignment: Alignment.bottomRight,
                margin: EdgeInsets.all(10),
                child: new FloatingActionButton(
                    child: new Icon(icon: IconsExtern.run_fill, size: 30),
                    backgroundColor: Colors.green,
                    onPressed: () =>
                    {
                        HelperWidgets.PushNewRoute(context, new MatchWidget());
                    }
                )
            );
        }
    }
}