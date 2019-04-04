using System.Collections.Generic;
using Unity.UIWidgets.widgets;

namespace UIScripts.Externs
{
    public class MultipTrailingWidget : StatelessWidget
    {
        private readonly List<Widget> Trailings = null;


        public MultipTrailingWidget(List<Widget> trailings)
        {
            Trailings = trailings;
        }

        public override Widget build(BuildContext context)
        {
            return new Row(
                mainAxisSize: Unity.UIWidgets.rendering.MainAxisSize.min,
                children: Trailings
            );
        }
    }
}