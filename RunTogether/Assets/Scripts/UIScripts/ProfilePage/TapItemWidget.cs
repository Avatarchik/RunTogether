using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts.ProfilePage
{
    public class TapItemWidget : StatelessWidget
    {
        private readonly string Title;
        private readonly IconData IconData;
        private readonly VoidCallback Callback;
        private readonly bool IsLast;

        public TapItemWidget(string title, IconData iconData, VoidCallback callback, bool isLast = false)
        {
            Title = title;
            IconData = iconData;
            Callback = callback;
            IsLast = isLast;
        }

        public override Widget build(BuildContext context)
        {
            return new SizedBox(
                child: new Column(
                    mainAxisSize: MainAxisSize.min,
                    children: new List<Widget>
                    {
                        new ListTile(title: new Text(Title), leading: new Icon(icon: IconData),
                            onTap: () => { Callback?.Invoke(); }),

                        new Divider(indent: 70, height: IsLast ? 0 : 16),
                    }
                )
            );
        }
    }
}