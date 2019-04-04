using System.Collections.Generic;
using Datas;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using Color = Unity.UIWidgets.ui.Color;


namespace UIScripts.ProfilePage
{
    public class LeaderBordPage : StatelessWidget
    {
        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                backgroundColor: new Color(0xffededed),
                appBar: new AppBar(
                    backgroundColor: new Color(0xffededed),
                    leading: new IconButton(color: Colors.black45, icon: new Icon(icon: Icons.arrow_back_ios),
                        onPressed:
                        () => Navigator.pop(context), highlightColor: Colors.transparent,
                        splashColor: Colors.transparent),
                    title: new Text("排行榜", style: new Unity.UIWidgets.painting.TextStyle(color: Colors.black),
                        textAlign: TextAlign.center),
                    centerTitle: true
                ),
                body: _leaderBordBody()
            );
        }

        private Widget _leaderBordBody()
        {
            return new Column(
                children: new List<Widget>
                {
                    //显示冠军
                    new RankingChampionWidget(AppManager.Instance.GetUserData.AvatarUrl,
                        AppManager.Instance.GetUserData.Mottor, AppManager.Instance.GetUserData.NickName),

                    //显示其余排名
                    new Divider(height: 5),
                    new Container(
                        child: new Flexible(
                            child: new ListView(
                                children: new List<Widget>
                                {
                                    new NormalRankingWidget("2", AppManager.Instance.GetUserData.AvatarUrl, "Niubility",
                                        "500"),
                                    new NormalRankingWidget("2", AppManager.Instance.GetUserData.AvatarUrl, "Niubility",
                                        "500")
                                }
                            )
                        )
                    )
                }
            );
        }
    }
}