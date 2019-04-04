using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts.RunPage
{
    public class MatchConditionWidget : StatelessWidget
    {
        private readonly string HintText;
        private readonly TextEditingController CondtioinController;
        private readonly VoidCallback StartMatchAction;
        private readonly string RegexCondition;

        public MatchConditionWidget(string hintText, VoidCallback startMatchAction,string regexCondition)
        {
            HintText = hintText;
            CondtioinController = new TextEditingController("");
            StartMatchAction = startMatchAction;
            RegexCondition = regexCondition;
        }

        public override Widget build(BuildContext context)
        {
            return new Container(
                margin: EdgeInsets.only(top: 20),
                alignment: Alignment.topCenter,
                decoration: new BoxDecoration(color: new Color(0xffededed)),
                child: new Container(
                    decoration: new BoxDecoration(color: new Color(0xffffffff), borderRadius: BorderRadius.all(5)),
                    margin: EdgeInsets.all(10),
                    child: new Column(
                        mainAxisSize: MainAxisSize.min,
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: new List<Widget>
                        {
                            new Padding(
                                padding: EdgeInsets.only(left: 20, top: 20),
                                child: new Text("填写您的匹配条件。", textAlign: TextAlign.left)
                            ),
                            new TextFieldExtern(HintText, padding: EdgeInsets.only(left: 20, right: 20, top: 20),
                                editingController: CondtioinController,regexCondition:RegexCondition),
                            new Container(
                                alignment: Alignment.center,
                                height: 40,
                                margin: EdgeInsets.only(top: 30),
                                decoration: new BoxDecoration(color: Colors.black),
                                child: new GestureDetector(
                                    onTap: StartMatchAction.Invoke,
                                    child: new Text("Run", style: new TextStyle(color: Colors.white))
                                )
                            )
                        }
                    )
                )
            );
        }
    }
}