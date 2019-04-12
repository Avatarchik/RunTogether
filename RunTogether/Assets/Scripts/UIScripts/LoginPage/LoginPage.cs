using System;
using System.Collections.Generic;
using Unity.UIWidgets;
using Unity.UIWidgets.animation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace UIScripts.LoginPage
{
    public class LoginPage : StatefulWidget
    {
        public override State createState()
        {
            return new _LoginPage();
        }
    }


    public class _LoginPage : SingleTickerProviderStateMixin<LoginPage>
    {
        private readonly TextEditingController PasswordEdit = new TextEditingController("");
        private readonly TextEditingController PhoneEdit = new TextEditingController("");
        private Animation<float> Animation;
        private AnimationController AnimController;

        public override void initState()
        {
            base.initState();
            AnimController = new AnimationController(vsync: this,
                duration: new TimeSpan(0, 0, 0, 0, 100));
            Animation = new FloatTween(.7f, 1f).animate(AnimController);
        }

        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                appBar: HelperWidgets._buildCloseAppBar(isCenterTitle: true, title: new Text(""),
                    lealding: new StoreConnector<AppState, object>(
                        converter: (state) => null,
                        builder: ((buildContext, model, dispatcher) => new IconButton(
                            icon: new Icon(icon: Icons.close),
                            onPressed: () =>
                            {
                                dispatcher.dispatch(new LoginAction()
                                {
                                    UserOpCode = UserOpCodeEnum.Close,
                                    Context = context
                                });
                            })),
                        pure: true
                    )),
                body: _buildBody()
            );
        }

        private Widget _buildBody()
        {
            return new Stack(
                children: new List<Widget>
                {
                    new Container(
                        color: CustomTheme.CustomTheme.EDColor,
                        child: new ListView(
                            children: new List<Widget>
                            {
                                new Container(
                                    margin: EdgeInsets.only(top: 100, left: 20),
                                    child: new Text("使用手机号码登录",
                                        style: new TextStyle(fontWeight: FontWeight.w500, fontSize: 20))
                                ),

                                new Padding(padding: EdgeInsets.only(top: 20)),

                                new StoreConnector<AppState, string>(
                                    converter: (state) => state.Account,
                                    builder: ((context, model, dispatcher) =>
                                        new TextFieldExtern("请填写手机号码",
                                            margin: EdgeInsets.all(20),
                                            obscureText: false, editingController: PhoneEdit, maxLength: 11,
                                            regexCondition: @"^[0-9]*$",
                                            onChanged: (text) =>
                                            {
                                                dispatcher.dispatch(new AccountAction()
                                                {
                                                    InputResult = text,
                                                    UserOpCode = UserOpCodeEnum.TypingAccount
                                                });
                                            })
                                    )
                                ),


                                new StoreConnector<AppState, string>(
                                    converter: (state) => state.Password,
                                    builder: ((context, model, dispatcher) =>
                                        new TextFieldExtern("请填写密码(英文字符、数字)",
                                            margin: EdgeInsets.all(20),
                                            obscureText: true, editingController: PasswordEdit, maxLength: 16,
                                            regexCondition: @"^[A-Za-z0-9]*$",
                                            onChanged: (text) =>
                                            {
                                                dispatcher.dispatch(new PasswordAction()
                                                {
                                                    InputResult = text,
                                                    UserOpCode = UserOpCodeEnum.TypingPassword
                                                });
                                            }))
                                ),


                                new StoreConnector<AppState, AppState>(
                                    converter: (state) => state,
                                    builder: ((context, model, dispatcher) =>
                                        new Container(
                                            margin: EdgeInsets.all(20f),
                                            width: MediaQuery.of(context).size.width,
                                            child: new FlatButton(color: Colors.green,
                                                child: new Text("登录",
                                                    style: CustomTheme.CustomTheme.DefaultTextThemen.display2),
                                                onPressed: () =>
                                                {
                                                    //防止用户漏空提交
                                                    if (string.IsNullOrEmpty(model.Account) ||
                                                        string.IsNullOrEmpty(model.Password))
                                                    {
                                                        return;
                                                    }

                                                    Debug.Log("dispathcer");
                                                    dispatcher.dispatch(RequestLogin(context, dispatcher));
                                                }
                                            )
                                        )
                                    ),
                                    pure: true
                                ),

                                new StoreConnector<AppState, object>(
                                    converter: (state) => null,
                                    builder: ((context, model, dispatcher) => new Container(
                                            margin: EdgeInsets.all(40),
                                            alignment: Alignment.center,
                                            child: new GestureDetector(
                                                onTap: () =>
                                                {
                                                    //TODO:找回密码
                                                },
                                                child: new Text("登陆遇到问题？")
                                            )
                                        )
                                    )
                                )
                            }
                        )
                    ),
                    new StoreConnector<AppState, AppState>(
                        converter: (state) => state,
                        builder: ((buildContext, model, dispatcher) =>
                            new Offstage(
                                offstage: model.HideCircularProgressIndicator,
                                child: new Container(
                                    alignment: Alignment.center,
                                    width: MediaQuery.of(context).size.width,
                                    height: MediaQuery.of(context).size.height,
                                    color: Colors.black26,
                                    child: new CircularProgressIndicator()
                                )
                            )
                        )
                    )
                }
            );
        }


        private LoginAction RequestLogin(BuildContext context, Dispatcher dispatcher)
        {
            Dictionary<string, string> tmpRequestParamaters = new Dictionary<string, string>();
            tmpRequestParamaters.Add("url", "https://baidu.com");

            LoginAction tmpLoginAction = new LoginAction(tmpRequestParamaters, () =>
                {
                    using (WindowProvider.of(context).getScope())
                    {
                        dispatcher.dispatch(new LoginRegisterAction(null)
                        {
                            Context = context,
                            RequestResult = RequestResultEnum.LoginSuccessed
                        });
                    }
                },
                (msg) => { Debug.Log(msg); });
            tmpLoginAction.Context = context;
            tmpLoginAction.UserOpCode = UserOpCodeEnum.None;
            tmpLoginAction.RequestOpCode = RequestOpCodeEnum.RequestLogin;
            return tmpLoginAction;
        }
    }
}