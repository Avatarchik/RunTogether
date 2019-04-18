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
using DialogUtils = Unity.UIWidgets.material.DialogUtils;

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
        private readonly TextEditingController PhoneEdit = new TextEditingController("");
        private readonly TextEditingController VerfyCodeEdit = new TextEditingController("");
        private string AvatarBase64 = null;

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
                                HelperWidgets._createImageProvider(AvatarImageType.Memory, model.RegisterAvatar)),
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
                                            Convert.ToBase64String(System.IO.File.ReadAllBytes(path))
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
                                dispatcher.dispatch(new SetNickNameAction()
                                {
                                    InputResult = text,
                                });
                            }),
                        new TextFieldExtern("请填写手机号码",
                            margin: EdgeInsets.only(left: 20, right: 20, top: 20),
                            editingController: PhoneEdit, maxLength: 11,
                            focusNode: new FocusNode(),
                            regexCondition: @"^[0-9]*$",
                            errorText: string.IsNullOrEmpty(model.Account) ? null :
                            HelperWidgets.IsCellphoneNumber(model?.Account) ? null : string.Empty,
                            onChanged: (text) =>
                            {
                                dispatcher.dispatch(new AccountAction() {InputResult = text.Trim()});
                            }
                        ),
                        new TextFieldExtern("请填写密码(字母、数字至少8位)",
                            margin: EdgeInsets.only(left: 20, right: 20, top: 20),
                            focusNode: new FocusNode(),
                            obscureText: true, editingController: PasswordEdit, maxLength: 16,
                            regexCondition: @"^[A-Za-z0-9]*$",
                            onChanged: (text) =>
                            {
                                dispatcher.dispatch(new PasswordAction()
                                {
                                    InputResult = text.Trim(),
                                });
                            }
                        ),
                        new Stack(
                            children: new List<Widget>
                            {
                                new TextFieldExtern("请填写验证码",
                                    margin: EdgeInsets.only(left: 20, right: 20, top: 20),
                                    maxLength: 6, editingController: VerfyCodeEdit,
                                    focusNode: new FocusNode(),
                                    regexCondition: @"^[0-9]*$",
                                    onChanged: (text) =>
                                    {
                                        dispatcher.dispatch(new SetVerfyCodeAction()
                                        {
                                            Context = buildContext,

                                            InputResult = text.Trim()
                                        });
                                    }),

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
                                                    Context = context,
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
                            child: new FlatButton(color: Colors.green,
                                child: new Text("注册",
                                    style: CustomTheme.CustomTheme.DefaultTextThemen.display2),
                                onPressed: () =>
                                {
                                    //防止用户漏空提交

                                    if (!HelperWidgets.IsValidPassword(PasswordEdit.text)
                                        || string.IsNullOrEmpty(NameEdit.text)
                                        || !HelperWidgets.IsCellphoneNumber(PhoneEdit.text)) return;

                                    if (string.IsNullOrEmpty(VerfyCodeEdit.text) || VerfyCodeEdit.text.Length < 6)
                                        return;

                                    dispatcher.dispatch(Register(context));
                                }
                            )
                        ),
                    }
                ))
            );
        }


        private void ShowDialog(string title, string content, BuildContext context)
        {
            DialogUtils.showDialog(context: context, builder: (buildContext => new AlertDialog(
                title: new Text(title),
                content: new Text(content),
                actions: new List<Widget>
                {
                    new FlatButton(child: new Text("Ok"), onPressed: () => { Navigator.pop(context); })
                }
            )));
        }

        private RegisterAction Register(BuildContext context)
        {
            Debug.Log("Do");
            Dictionary<string, string> tmpParamaters = new Dictionary<string, string>
            {
                {"url", new Uri(new Uri(APIsInfo.APIGetWay), APIsInfo.User_Register).ToString()},
                {"headimages", AvatarBase64},
                {"nickname", NameEdit.text},
                {"password", PasswordEdit.text},
                {"phone", PhoneEdit.text},
            };
            return new RegisterAction(tmpParamaters, (result) =>
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
                                ShowDialog("注册", "注册账户失败，请更换账户重试", context);
                                break;
                            case 200:
                            case 1:
                                ShowDialog("注册", "注册成功", context);
                                break;
                        }
                    }
                },
                (result) => { Debug.Log(result); }
            );
        }
    }
}