using System.Collections.Generic;
using UIScripts.Externs;
using Unity.UIWidgets.widgets;

namespace UIScripts.ProfilePage
{
    public class RankingChampionWidget : StatelessWidget
    {
        private readonly string AvatarUrl;
        private readonly string Mottor;
        private readonly string NickName;

        public RankingChampionWidget(string avatarUrl, string mottor, string nickName)
        {
            AvatarUrl = avatarUrl;
            Mottor = mottor;
            NickName = nickName;
        }

        public override Widget build(BuildContext context)
        {
            return new Container(
                height: 120,
                child: new Center(
                    child: new Column(
                        children: new List<Widget>
                        {
                            new AvatarWidget(Image.network(AvatarUrl), 80, 80),
                            new Text(NickName),
                            new Text(Mottor),
                        }
                    )
                )
            );
        }
    }
}