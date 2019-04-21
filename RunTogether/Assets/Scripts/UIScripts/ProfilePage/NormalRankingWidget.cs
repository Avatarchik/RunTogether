using System.Collections.Generic;
using UIScripts.Externs;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.widgets;

namespace UIScripts.ProfilePage
{
    public class NormalRankingWidget : StatelessWidget
    {
        private readonly string RankingId;
        private readonly string NickName;
        private readonly string RunDistance;
        private readonly string AvatarUrl;

        public NormalRankingWidget(string rankingId, string avatarUrl, string nickName, string runDistance)
        {
            RankingId = rankingId;
            NickName = nickName;
            AvatarUrl = avatarUrl;
            RunDistance = runDistance;
        }

        public override Widget build(BuildContext context)
        {
            return new Column(
                mainAxisSize: MainAxisSize.min,
                children: new List<Widget>
                {
                    new ListTile(
                        leading: new Row(
                            mainAxisSize: MainAxisSize.min,
                            children: new List<Widget>
                            {
                                new Text(RankingId),
                                new Padding(padding: EdgeInsets.only(left: 30)),
                                new AvatarWidget(Image.network(AvatarUrl),
                                    40,
                                    40),
                                new Padding(padding: EdgeInsets.only(left: 20)),
                                new Text(NickName),
                            }
                        ),
                        title: new Text(RunDistance + " KM"),
                        trailing: new Icon(icon: Icons.dock)),
                    new Divider(indent: 18, height: 5),
                }
            );
        }
    }
}