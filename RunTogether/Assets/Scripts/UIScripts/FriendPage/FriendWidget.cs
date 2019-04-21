using System.Collections.Generic;
using UIScripts.Externs;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.widgets;

namespace UIScripts.FriendPage
{
    public class FriendWidget : StatelessWidget
    {
        private readonly string AvatarURL;
        private readonly string Name;
        private readonly float DividerHeight;
        private readonly float DividerIndent;

        public FriendWidget(string avatarUrl, string name, float dividerHeight, float dividerIndent)
        {
            AvatarURL = avatarUrl;
            Name = name;
            DividerHeight = dividerHeight;
            DividerIndent = dividerIndent;
        }

        public override Widget build(BuildContext context)
        {
            ImageProvider tmpImageProvider = new NetworkImage(AvatarURL);
            return new Container(
                color: Colors.white,
                child: new Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    mainAxisSize: MainAxisSize.min,
                    children: new List<Widget>
                    {
                        new ListTile(
                            leading: new AvatarWidget(tmpImageProvider, 50, 50),
                            title: new Text(Name), onTap: () => { }, contentPadding: EdgeInsets.only(left: 20)),
                        new Divider(height: DividerHeight, indent: DividerIndent)
                    }
                )
            );
        }
    }
}