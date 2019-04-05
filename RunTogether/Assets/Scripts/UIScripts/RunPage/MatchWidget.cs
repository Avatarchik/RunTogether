using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts.RunPage
{
    public class MatchWidget : StatefulWidget
    {
        internal readonly RunPageState RunPage;

        public MatchWidget(RunPageState runPage)
        {
            this.RunPage = runPage;
        }

        public override State createState()
        {
            return new MatchWidgetState();
        }
    }

    public class MatchWidgetState : SingleTickerProviderStateMixin<MatchWidget>
    {
        private readonly TabController MatchTabController;

        public MatchWidgetState()
        {
            MatchTabController = new TabController(vsync: this, length: 3);
        }

        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                backgroundColor: new Color(0xffededed),
                appBar: new AppBar(
                    backgroundColor: new Color(0xffededed),
                    leading: new IconButton(icon: new Icon(icon: Icons.arrow_back_ios, color: Colors.grey),
                        onPressed: () =>
                        {
                            widget.RunPage.PositionController.reverse().whenCompleteOrCancel(() =>
                            {
                                widget.RunPage.PagesIndex--;
                                widget.RunPage.Refresh();
                            });
                        }),
                    title: new Text("连接(匹配)", style: new TextStyle(color: Colors.black)),
                    bottom: new TabBar(
                        indicatorColor: Colors.black,
                        controller: MatchTabController,
                        labelColor: Color.black,
                        tabs: new List<Widget>
                        {
                            new Tab(text: "距离"),
                            new Tab(text: "时间"),
                            new Tab(text: "城市"),
                        }
                    ),
                    centerTitle: true
                ),
                body: new TabBarView(
                    controller: MatchTabController,
                    children: new List<Widget>
                    {
                        new MatchConditionWidget("填写您要跑的距离（如5公里）", () => { }, @"^[0-9]*$"),
                        new MatchConditionWidget("填写您要跑的时长（如30分钟）", () => { }, @"^[0-9]*$"),
                        new MatchConditionWidget("填写您想要的城市（如厦门市）", () => { }, "^[\u4e00-\u9fa5]*$"),
                    }
                )
            );
        }
    }
}