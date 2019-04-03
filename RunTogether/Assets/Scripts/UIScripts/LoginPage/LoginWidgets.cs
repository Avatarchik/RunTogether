using Datas;
using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts.LoginPage
{
    public class LoginWidgets : StatelessWidget
    {

        private TextEditingController PasswordEdit = new TextEditingController("");
        private TextEditingController PhoneEdit = new TextEditingController("");

        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                appBar: HelperWidgets._buildCloseAppBar(isCenterTitle: true, title: new Text(""),
                    () => { Navigator.pop(context); }),
                backgroundColor: CustomTheme.CustomTheme.EDColor,
                body: _buildBody(context)
            );
        }

        private Widget _buildBody(BuildContext buildContext)
        {
            return new Container(
                margin: EdgeInsets.only(top: 50),
                child: new Column(
                    children: new List<Widget>
                    {
                        new Container(
                            alignment: Alignment.center,
                            margin: EdgeInsets.only(bottom: 50),
                            child: new Text("使用手机号码登录", style: new TextStyle(fontWeight: FontWeight.bold, fontSize: 20))
                        ),

                        new Padding(padding: EdgeInsets.only(top: 20)),

                        new TextFieldExtern("请填写手机号码", padding: EdgeInsets.only(left: 20, right: 20, top: 20),
                        editingController:PhoneEdit,onEditingComplete:OnEditCompletedIsPhoneNumbers,maxLength:11,regexCondition:@"^[0-9]*$"),

                        new TextFieldExtern("请填写密码(英文字符、数字)", padding: EdgeInsets.only(left: 20, right: 20, top: 20),
                        obscureText: true,editingController:PasswordEdit,maxLength:16,regexCondition:@"^[A-Za-z0-9]+$"),

                        new SizedBox(
                            width:340,
                            height:60,
                            child:new Padding(
                                padding:EdgeInsets.only(top:20),
                                child:new FlatButton(color: Colors.green,
                                    child: new Text("登录", style: CustomTheme.CustomTheme.DefaultTextThemen.display2),
                                    onPressed: () =>
                                    {
                                        //防止用户漏空提交
                                        if (string.IsNullOrEmpty(PasswordEdit.text) || string.IsNullOrEmpty(PhoneEdit.text))
                                        {
                                            return;
                                        }

                                        OnLoginedCallback(buildContext);
                                    }
                                )
                            )
                        ),
                        
                        new Padding(
                            padding:EdgeInsets.all(120),
                            child:new GestureDetector(
                                onTap:()=>{},
                                child:new Text("登陆遇到问题？")
                            )
                        )
                    }
                )
            );
        }


        private void OnEditCompletedIsPhoneNumbers()
        {
            if (HelperWidgets.IsCellphoneNumber(PhoneEdit.text))
                UnityEngine.Debug.Log("it is telephone numbers");
            else
                UnityEngine.Debug.Log("it is not telephone numbers");
        }


        private void OnLoginedCallback(BuildContext buildContext)
        {
            Route tmpEditProfileRoute = new PageRouteBuilder(
                pageBuilder:(context, animation, secondaryAnimation) =>  new MainView(),
                transitionsBuilder:(context, animation, secondaryAnimation, child) =>
                    new PageTransition(routeAnimation:animation,child:child,beginDirection:new Offset(0f,1f),endDirection:Offset.zero)
            ); 
                
            //TODO:Login
            AppManager.Instance.SetLoginState(true);
            Navigator.pop(buildContext);
            Navigator.pop(buildContext);
            Navigator.push(buildContext,tmpEditProfileRoute);
        }
    }
}