using System;
using System.Collections.Generic;
using Datas;
using UIScripts.Externs;
using Unit;
using Unity.UIWidgets;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using DialogUtils = Unity.UIWidgets.material.DialogUtils;
using Image = Unity.UIWidgets.widgets.Image;

namespace UIScripts.LoginPage
{
    public class RegisterPage : StatefulWidget
    {
        public override State createState()
        {
            return new _RegisterPage();
        }
    }

    public class _RegisterPage : State<RegisterPage>
    {
        private readonly TextEditingController NameEdit = new TextEditingController("");
        private readonly TextEditingController PasswordEdit = new TextEditingController("");
        private readonly TextEditingController PasswordEditAgain = new TextEditingController("");

        private readonly TextEditingController PhoneEdit = new TextEditingController("");
        private readonly TextEditingController VerfyCodeEdit = new TextEditingController("");
        private string AvatarBase64 = null;
        private RegisterUserInfoAction RegisterUserInfoAction = new RegisterUserInfoAction();

        public override void initState()
        {
            base.initState();

            AvatarBase64 = "data:image/jpeg;base64," +
                           Convert.ToBase64String(
                               System.IO.File.ReadAllBytes(Application.streamingAssetsPath + "/avatar.png"));
        }


        public override void dispose()
        {
            AvatarBase64 = null;
            base.dispose();
        }

        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                appBar: HelperWidgets._buildCloseAppBar(isCenterTitle: true, title: new Text(""),
                    lealding: new IconButton(
                        icon: new Icon(icon: Icons.close),
                        onPressed: () => { HelperWidgets.PopRoute(context); })
                ),
                backgroundColor: CustomTheme.CustomTheme.EDColor,
                body: _buildBody(context)
            );
        }


        private Widget _buildBody(BuildContext context)
        {
            return new StoreConnector<AppState, AppState>(
                converter: (state) => state,
                builder: ((buildContext, model, dispatcher) => new ListView(
                    children: new List<Widget>
                    {
                        new Container(
                            alignment: Alignment.center,
                            margin: EdgeInsets.only(top: 100, bottom: 10),
                            child: new Text("用手机号注册", style: new TextStyle(fontWeight: FontWeight.bold, fontSize: 20))
                        ),

                        new GestureDetector(
                            child: new AvatarWidget(
                               Image.file(file:model.RegisterAvatar)),
                            onTap: () =>
                            {
#if !UNITY_EDITOR && UNITY_IOS
                                NativeGallery.GetImageFromGallery((path) =>
                                {
                                    if (string.IsNullOrEmpty(path)) return;
                                    ;
                                    using (WindowProvider.of(context).getScope())
                                    {
                                        AvatarBase64 =
                                            "data:image/jpeg;base64," +
                                            Convert.ToBase64String(System.IO.File.ReadAllBytes(path));
                                        dispatcher.dispatch(new SetRegisterAvatarAction {InputResult = path});
                                    }
                                });
#endif
                            }
                        ),

                        new Padding(padding: EdgeInsets.only(top: 15)),

                        new TextFieldExtern("请设置昵称，如一起跑",
                            margin: EdgeInsets.only(left: 20, right: 20, top: 20),
                            editingController: NameEdit,
                            focusNode: new FocusNode(),
                            onChanged: (text) =>
                            {
                                RegisterUserInfoAction.NickName = text;
                                dispatcher.dispatch(RegisterUserInfoAction);
                            }),
                        new TextFieldExtern("请填写手机号码",
                            margin: EdgeInsets.only(left: 20, right: 20, top: 20),
                            editingController: PhoneEdit, maxLength: 11,
                            regexCondition: @"^[0-9]*$",
                            errorText: string.IsNullOrEmpty(model.Account) ? null :
                            HelperWidgets.IsCellphoneNumber(model?.Account) ? null : string.Empty,
                            onChanged: (text) =>
                            {
                                RegisterUserInfoAction.Account = text;
                                dispatcher.dispatch(RegisterUserInfoAction);
                            }
                        ),
                        new TextFieldExtern("请填写密码(字母、数字至少8位)",
                            margin: EdgeInsets.only(left: 20, right: 20, top: 20),
                            obscureText: true, editingController: PasswordEdit, maxLength: 16,
                            regexCondition: @"^[A-Za-z0-9]*$",
                            onChanged: (text) =>
                            {
                                RegisterUserInfoAction.Password = text;
                                dispatcher.dispatch(RegisterUserInfoAction);
                            }
                        ),
                        new TextFieldExtern("重新输入密码",
                            margin: EdgeInsets.only(left: 20, right: 20, top: 20),
                            focusNode: new FocusNode(),
                            errorText: model.PasswordTextFieldErrorText,
                            obscureText: true, editingController: PasswordEditAgain, maxLength: 16,
                            regexCondition: @"^[A-Za-z0-9]*$",
                            onChanged: (text) =>
                            {
                                RegisterUserInfoAction.PasswordAgain = text;
                                dispatcher.dispatch(RegisterUserInfoAction);
                            }
                        ),
                        new Stack(
                            children: new List<Widget>
                            {
                                new TextFieldExtern("请填写验证码",
                                    margin: EdgeInsets.only(left: 20, right: 20, top: 20),
                                    maxLength: 6, editingController: VerfyCodeEdit,
                                    regexCondition: @"^[0-9]*$",
                                    onChanged: (text) =>
                                    {
                                        RegisterUserInfoAction.VerfyCode = text;
                                        dispatcher.dispatch(RegisterUserInfoAction);
                                    }
                                ),

                                model.VerfyCodeWasSent
                                    ? new Container(
                                        alignment: Alignment.centerRight,
                                        padding: EdgeInsets.only(right: 25, bottom: 5),
                                        child: new CountdownWidget(
                                            timeSpan: new TimeSpan(0, 0, model.CountdownTime),
                                            () =>
                                            {
                                                dispatcher.dispatch(new RegisterAction()
                                                {
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
                                                dispatcher.dispatch(new CountdownAction()
                                                {
                                                    CountdownTime = 60,
                                                });
                                            }
                                        )
                                    )
                            }
                        ),
                        new Container(
                            margin: EdgeInsets.all(20f),
                            width: MediaQuery.of(context).size.width,
                            child: new RaisedButton(color: Colors.green,
                                child: new Text("注册",
                                    style: CustomTheme.CustomTheme.DefaultTextThemen.display2),
                                onPressed: model.IsInteractableOfButton ? OnPressRegister(dispatcher, context) : null,
                                elevation: 0,
                                disabledColor: Colors.green.withAlpha(125)
                            )
                        ),
                    }
                ))
            );
        }


        /// <summary>
        /// 响应注册按钮
        /// </summary>
        /// <param name="dispatcher"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private VoidCallback OnPressRegister(Dispatcher dispatcher, BuildContext context)
        {
            return () =>
            {
                if (!HelperWidgets.IsValidPassword(PasswordEdit.text)
                    || string.IsNullOrEmpty(NameEdit.text)
                    || !HelperWidgets.IsCellphoneNumber(PhoneEdit.text))
                {
                    HelperWidgets.ShowDialog("注册错误", "请完善注册信息", context);
                    return;
                }

                if (string.IsNullOrEmpty(VerfyCodeEdit.text) || VerfyCodeEdit.text.Length < 6)
                {
                    HelperWidgets.ShowDialog("注册错误", "验证码格式错误", context);
                    return;
                }

                dispatcher.dispatch(Register(context));
            };
        }


        /// <summary>
        /// 发起注册请求
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private RegisterAction Register(BuildContext context)
        {
            Dictionary<string, string> tmpParamaters = new Dictionary<string, string>
            {
                {"url", new Uri(new Uri(APIsInfo.APIGetWay), APIsInfo.User_Register).ToString()},
                {"headimages", AvatarBase64},
                {"nickname", NameEdit.text},
                {"password", PasswordEdit.text},
                {"phone", PhoneEdit.text},
            };
            WebServerApiRequest tmpWebServerApiRequest =
                new WebServerApiRequest(tmpParamaters, (result) =>
                    {
                        RequestUserRespon tmpRequestUserRespon = JsonUtility.FromJson<RequestUserRespon>(result);
                        Debug.Log(result);
                        using (WindowProvider.of(context).getScope())
                        {
                            switch (tmpRequestUserRespon.code)
                            {
                                case 103:
                                case 104:
                                case 105:
                                case 106:
                                case 400:
                                    HelperWidgets.ShowDialog("注册", "注册账户失败，请更换账户重试", context);
                                    break;
                                case 200:
                                case 1:
                                    HelperWidgets.ShowDialog("注册", "注册成功", context);
                                    break;
                            }
                        }
                    },
                    (result) => { Debug.Log(result); }
                );
            return new RegisterAction() {webServerApiRequest = tmpWebServerApiRequest};
        }
    }
}