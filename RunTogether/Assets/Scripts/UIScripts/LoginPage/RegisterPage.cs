using System;
using System.Collections.Generic;
using System.Timers;
using Datas;
using UIScripts.Externs;
using Unity.UIWidgets.animation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace UIScripts.LoginPage
{
    public class RegisterPage : StatelessWidget
    {
        private TextEditingController NameEdit = new TextEditingController("");
        private TextEditingController PasswordEdit = new TextEditingController("");
        private TextEditingController PhoneEdit = new TextEditingController("");
        private TextEditingController VerfyCodeEdit = new TextEditingController("");

        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                appBar: HelperWidgets._buildCloseAppBar(isCenterTitle: true, title: new Text(""),
                    lealding: new StoreConnector<AppState, AppState>(
                        converter: state => state,
                        builder: ((builderContext, model, dispatcher) => new IconButton(
                            icon: new Icon(icon: Icons.close),
                            onPressed: () =>
                            {
                                dispatcher.dispatch(new RegisterState()
                                {
                                    UserOpCode = UserOpCodeEnum.Close,
                                    RequestOpCode = model.RequestOpCode,
                                    Context = context
                                });
                            })),
                        pure: true
                    )),
                backgroundColor: CustomTheme.CustomTheme.EDColor,
                body: _buildBody(context)
            );
        }


        private Widget _buildBody(BuildContext context)
        {
            return new ListView(
                physics: new AlwaysScrollableScrollPhysics(),
                children: new List<Widget>
                {
                    new Container(
                        alignment: Alignment.center,
                        margin: EdgeInsets.only(top: 100, bottom: 10),
                        child: new Text("用手机号注册", style: new TextStyle(fontWeight: FontWeight.bold, fontSize: 20))
                    ),

                    new StoreConnector<AppState, string>(
                        converter: (state) => state.RegisterAvatar,
                        builder: ((buildContext, model, dispatcher) =>
                            new GestureDetector(
                                child: new AvatarWidget(
                                    HelperWidgets._createImageProvider(AvatarImageType.Memory, model)),
                                onTap: () =>
                                {
#if !UNITY_EDITOR && UNITY_IOS
                                    NativeGallery.GetImageFromGallery((path) =>
                                    {
                                        if (string.IsNullOrEmpty(path)) return;
                                        using (WindowProvider.of(context).getScope())
                                        {
                                            dispatcher.dispatch(new SetRegisterAvatarState()
                                            {
                                                InputResult = path,
                                                UserOpCode = UserOpCodeEnum.SetupAvatar,
                                            });
                                        }
                                    });
#endif
                                }
                            )
                        )
                    ),

                    new Padding(padding: EdgeInsets.only(top: 15)),

                    new StoreConnector<AppState, AppState>(
                        converter: (state) => state,
                        builder: ((buildContext, model, dispatcher) =>
                            new TextFieldExtern("请设置昵称，如一起跑",
                                margin: EdgeInsets.only(left: 20, right: 20, top: 20),
                                editingController: NameEdit,
                                onChanged: (text) =>
                                {
                                    dispatcher.dispatch(new SetNickNameState()
                                    {
                                        UserOpCode = UserOpCodeEnum.TypingNickName,
                                        InputResult = text,
                                        PageState = PageStateEnum.RegisterPage
                                    });
                                }))
                    ),
                    new StoreConnector<AppState, AppState>(
                        converter: (state) => state,
                        builder: ((buildContext, model, dispatcher) =>
                            new TextFieldExtern("请填写手机号码",
                                margin: EdgeInsets.only(left: 20, right: 20, top: 20),
                                editingController: PhoneEdit, maxLength: 11, regexCondition: @"^[0-9]*$",
                                onChanged: (text) =>
                                {
                                    dispatcher.dispatch(new AccountState()
                                    {
                                        UserOpCode = UserOpCodeEnum.TypingAccount,
                                        InputResult = text,
                                        PageState = PageStateEnum.RegisterPage
                                    });
                                }
                            )
                        )
                    ),
                    new StoreConnector<AppState, AppState>(
                        converter: (state) => state,
                        builder: ((buildContext, model, dispatcher) =>
                            new TextFieldExtern("请填写密码(英文字符、数字)",
                                margin: EdgeInsets.only(left: 20, right: 20, top: 20),
                                obscureText: true, editingController: PasswordEdit, maxLength: 16,
                                regexCondition: @"^[A-Za-z0-9]+$",
                                onChanged: (text) =>
                                {
                                    dispatcher.dispatch(new PasswordState()
                                    {
                                        UserOpCode = UserOpCodeEnum.TypingPassword,
                                        InputResult = text,
                                        PageState = PageStateEnum.RegisterPage
                                    });
                                }
                            )
                        )
                    ),
                    new Stack(
                        children: new List<Widget>
                        {
                            new StoreConnector<AppState, AppState>(
                                converter: (state) => state,
                                builder: ((buildContext, model, dispatcher) =>
                                    new TextFieldExtern("请填写验证码", margin: EdgeInsets.only(left: 20, right: 20, top: 20),
                                        maxLength: 6, editingController: VerfyCodeEdit,
                                        onChanged: (text) =>
                                        {
                                            dispatcher.dispatch(new SetVerfyCodeState()
                                            {
                                                Context = buildContext,
                                                UserOpCode = UserOpCodeEnum.TypingVerfyCode,
                                                InputResult = text
                                            });
                                        })
                                )
                            ),

                            new StoreConnector<AppState, AppState>(
                                converter: (state) => state,
                                builder: ((context1, model, dispatcher) =>
                                    model.VerfyCodeWasSent
                                        ? new Container(
                                            alignment: Alignment.centerRight,
                                            padding: EdgeInsets.only(right: 25, bottom: 5),
                                            child: new CountdownWidget(
                                                timeSpan: new TimeSpan(0, 0, model.CountdownTime),
                                                () =>
                                                {
                                                    dispatcher.dispatch(new RegisterState()
                                                    {
                                                        Context = context,
                                                        UserOpCode = UserOpCodeEnum.None
                                                    });
                                                },
                                                Counter: model.CountdownTime
                                            )
                                        )
                                        : new Container(
                                            alignment: Alignment.centerRight,
                                            padding: EdgeInsets.only(right: 25, bottom: 5),
                                            child: new GestureDetector(child: new Icon(icon: Icons.send, size: 20),
                                                onTap: () =>
                                                {
                                                    dispatcher.dispatch(new CountdownState()
                                                    {
                                                        CountdownTime = 60,
                                                        UserOpCode = UserOpCodeEnum.SendVerfyCode
                                                    });
                                                }
                                            )
                                        )
                                )
                            )
                        }
                    ),
                    new StoreConnector<AppState, AppState>(
                        converter: (state) => state,
                        builder: ((buildContext, model, dispatcher) => new Container(
                                margin: EdgeInsets.all(20f),
                                width: MediaQuery.of(context).size.width,
                                child: new FlatButton(color: Colors.green,
                                    child: new Text("注册",
                                        style: CustomTheme.CustomTheme.DefaultTextThemen.display2),
                                    onPressed: () =>
                                    {
                                        //TODO:Register
                                        //防止用户漏空提交
                                        if (string.IsNullOrEmpty(model.Account)
                                            || string.IsNullOrEmpty(model.Password)
                                            || string.IsNullOrEmpty(model.NickName)
                                            || string.IsNullOrEmpty(model.VerfyCode)) return;

                                        dispatcher.dispatch(new RegisterState()
                                        {
                                            RequestOpCode = RequestOpCodeEnum.RequestRegister,
                                            UserOpCode = UserOpCodeEnum.None,
                                            Context = context
                                        });
                                    }
                                )
                            )
                        )
                    ),
                }
            );
        }
    }
}