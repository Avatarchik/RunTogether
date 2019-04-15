using System;
using System.Collections.Generic;
using BestHTTP.JSON;
using Datas;
using UIScripts.Externs;
using Unity.UIWidgets;
using Unity.UIWidgets.animation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Color = Unity.UIWidgets.ui.Color;
using DialogUtils = Unity.UIWidgets.material.DialogUtils;

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
        private Animation<Color> CircularProgressIndicatorColor;

        public override void initState()
        {
            base.initState();
            AnimController = new AnimationController(vsync: this,
                duration: new TimeSpan(0, 0, 0, 0, 100));
            Animation = new FloatTween(.7f, 1f).animate(AnimController);
            CircularProgressIndicatorColor = new AlwaysStoppedAnimation<Color>(Colors.white);
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
            return new Container(
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
                                        child: new StoreConnector<AppState, AppState>(
                                            converter: (state) => state,
                                            builder: ((buildContext, viewModel, dispatcher1) =>
                                                viewModel.HideCircularProgressIndicator
                                                    ? (Widget) new Text(viewModel.LoginState,
                                                        style: CustomTheme.CustomTheme.DefaultTextThemen.display2)
                                                    : new Row(
                                                        mainAxisAlignment: MainAxisAlignment.center,
                                                        children: new List<Widget>
                                                        {
                                                            new CustomCircularProgressIndicator(
                                                                valueColor: CircularProgressIndicatorColor),
                                                            new SizedBox(width: 10),
                                                            new Text(viewModel.LoginState,
                                                                style: CustomTheme.CustomTheme.DefaultTextThemen
                                                                    .display2)
                                                        }
                                                    )
                                            )
                                        ),
                                        onPressed: () =>
                                        {
                                            //防止用户漏空提交
                                            if (string.IsNullOrEmpty(model.Account) ||
                                                string.IsNullOrEmpty(model.Password))
                                            {
                                                return;
                                            }

                                            dispatcher.dispatch(RequestLogin(model, context, dispatcher));
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
            );
        }


        private void _showDialog()
        {
            DialogUtils.showDialog(context: context, builder: (buildContext => new AlertDialog(
                title: new Text("Error tips!"),
                content: new Text("Error: your password or account was no right!"),
                actions: new List<Widget>
                {
                    new FlatButton(child: new Text("Ok"), onPressed: () => { Navigator.pop(context); })
                }
            )));
        }

        private LoginAction RequestLogin(AppState appState, BuildContext context, Dispatcher dispatcher)
        {
            Dictionary<string, string> tmpRequestParamaters = new Dictionary<string, string>
            {
                {"url", new Uri(new Uri(APIsInfo.APIGetWay), APIsInfo.User_Login).ToString()},
                {"password", appState.Password},
                {"phone", appState.Account}
            };

            LoginAction tmpLoginAction = new LoginAction(tmpRequestParamaters, (result) =>
                {
                    RequestUserRespon tmpRequestUserRespon = JsonUtility.FromJson<RequestUserRespon>(result);
                    Debug.Log(result);
                    using (WindowProvider.of(context).getScope())
                    {
                        switch (tmpRequestUserRespon.code)
                        {
                            case 103:
                                OnPasswordError(dispatcher);
                                break;
                            case 104:
                            case 105:
                            case 106:

                                break;
                            case 200:
                                OnLoginSuccessed(dispatcher);
                                break;
                            case 400:
                                Debug.LogError("404 not found");
                                break;
                        }
                    }
                },
                (msg) => { Debug.Log(msg); });
            tmpLoginAction.Context = context;
            tmpLoginAction.UserOpCode = UserOpCodeEnum.None;
            tmpLoginAction.RequestOpCode = RequestOpCodeEnum.RequestLogin;
            return tmpLoginAction;
        }


        private void OnLoginSuccessed(Dispatcher dispatcher)
        {
            dispatcher.dispatch(new LoginRegisterAction(null)
            {
                Context = context,
                RequestResult = RequestResultEnum.LoginSuccessed
            });
        }


        private void OnPasswordError(Dispatcher dispatcher)
        {
            _showDialog();
            LoginAction tmpLoginAction = new LoginAction(null, null, null);
            tmpLoginAction.Context = context;
            tmpLoginAction.UserOpCode = UserOpCodeEnum.None;
            tmpLoginAction.RequestOpCode = RequestOpCodeEnum.None;
            tmpLoginAction.RequestResult = RequestResultEnum.LoginFailed;
            dispatcher.dispatch(tmpLoginAction);
        }


        private void AccountDoesNotExist(Dispatcher dispatcher)
        {
            _showDialog();
        }
    }
}