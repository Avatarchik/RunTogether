using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts.Externs
{
    public class ListTileWithDividerWidget : StatelessWidget
    {
        private readonly int DividerIndent;
        private readonly int DividerHeight;
        private readonly Color BackgroundColor = Colors.white;
        private readonly Widget TitleWidget;
        private readonly Widget TrailingWidget;
        private readonly VoidCallback OnTap;


        public ListTileWithDividerWidget(Widget titleWidget = null,
            Widget trailingWidget = null,
            VoidCallback onTap = null,
            Color backgroundColor = null,
            int dividerHeight = 5,
            int dividerIndent = 18)
        {
            BackgroundColor = backgroundColor ?? BackgroundColor;
            TrailingWidget = trailingWidget;
            TitleWidget = titleWidget;
            OnTap = onTap;
            DividerHeight = dividerHeight;
            DividerIndent = dividerIndent;
        }

        public override Widget build(BuildContext context)
        {
            return new Container(decoration: new BoxDecoration(color: BackgroundColor),
                child:
                new Column(
                    children: new List<Widget>
                    {
                        new ListTile(title: TitleWidget, trailing: TrailingWidget, onTap: () => OnTap?.Invoke()),
                        new Divider(indent: DividerIndent, height: DividerHeight)
                    }
                )
            );
        }
    }
}