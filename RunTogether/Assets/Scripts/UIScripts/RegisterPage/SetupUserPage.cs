using System;
using System.Collections.Generic;
using Datas;
using UIScripts.Externs;
using Unit;
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
using Image = Unity.UIWidgets.widgets.Image;

namespace UIScripts.RegisterPage
{
    public class SetupUserPage : RegisterPageBase
    {
        private readonly TextEditingController NameEdit = new TextEditingController("");
        private readonly Animation<Color> CircularProgressIndicatorColor =
            new AlwaysStoppedAnimation<Color>(Colors.white);

        public override Widget build(BuildContext context)
        {
            return new Scaffold(
                appBar: HelperWidgets._buildCloseAppBar(isCenterTitle: true, title: new Text("注册新账户"),
                    lealding: new IconButton(
                        icon: new Icon(icon: Icons.arrow_back_ios, size: 18),
                        onPressed: () => { HelperWidgets.PopRoute(context); })
                ),
                backgroundColor: CustomTheme.CustomTheme.EDColor,
                body: new StoreConnector<AppState, AppState>(
                    converter: (state) => state,
                    builder: ((buildContext, model, dispatcher) =>
                        new Column(
                            children: new List<Widget>
                            {
                                new Container(
                                    margin: EdgeInsets.only(top: 100, left: 20, bottom: 20),
                                    alignment: Alignment.centerLeft,
                                    child: new Text("请完善个人资料,方便朋友认出你",
                                        style: new TextStyle(fontWeight: FontWeight.w500, fontSize: 20))
                                ),

                                new Padding(
                                    padding: EdgeInsets.only(right: 20),
                                    child: new GestureDetector(
                                        child: new AvatarWidget(
                                            Image.network(
                                                "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1555910735058&di=4bb68aece7cdbb41e3119001852bb670&imgtype=0&src=http%3A%2F%2Fb-ssl.duitang.com%2Fuploads%2Fitem%2F201706%2F22%2F20170622131955_h4eZS.thumb.700_0.jpeg"),
                                            50, 50),
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
                                    )
                                ),

                                new Flexible(
                                    child: new TextFieldExtern("请设置昵称，如一起跑",
                                        margin: EdgeInsets.all(20f),
                                        editingController: NameEdit,
                                        onChanged: (text) =>
                                        {
                                            RegisterUserInfoAction.NickName = text;
                                            dispatcher.dispatch(RegisterUserInfoAction);
                                        }
                                    )
                                ),


                                new Container(
                                    margin: EdgeInsets.all(20f),
                                    width: MediaQuery.of(context).size.width,
                                    child: new RaisedButton(color: Colors.green,
                                        child: model.HideSmallLoadingIndicator
                                            ? (Widget) new Text("注册",
                                                style: CustomTheme.CustomTheme.DefaultTextThemen.display2)
                                            : new Row(
                                                mainAxisAlignment: MainAxisAlignment.center,
                                                children: new List<Widget>
                                                {
                                                    new CustomCircularProgressIndicator(
                                                        valueColor: CircularProgressIndicatorColor),
                                                    new SizedBox(width: 10),
                                                    new Text("注册中",
                                                        style: CustomTheme.CustomTheme.DefaultTextThemen
                                                            .display2)
                                                }
                                            ),
                                        onPressed: model.CanRegister
                                            ? GoToRegister(model, context, dispatcher)
                                            : null,
                                        elevation: 0,
                                        disabledColor: Colors.green.withAlpha(125)
                                    )
                                )
                            }
                        ))
                )
            );
        }

        /// <summary>
        /// 注册按钮事件
        /// </summary>
        /// <param name="state"></param>
        /// <param name="context"></param>
        /// <param name="dispatcher"></param>
        /// <returns></returns>
        private VoidCallback GoToRegister(AppState state, BuildContext context, Dispatcher dispatcher)
        {
            return () =>
            {
                Debug.Log(state.Account + " " + state.NickName + " " + state.Account+" "+state.VerfyCode);

                if (!HelperWidgets.IsValidPassword(state.Password)
                    || string.IsNullOrEmpty(state.NickName)
                    || !HelperWidgets.IsCellphoneNumber(state.Account))
                {
                    HelperWidgets.ShowDialog("注册错误", "请完善注册信息", context);
                    return;
                }

                if (string.IsNullOrEmpty(state.VerfyCode) || state.VerfyCode.Length < 6)
                {
                    HelperWidgets.ShowDialog("注册错误", "验证码格式错误", context);
                    return;
                }

                dispatcher.dispatch(Register(context, state, dispatcher));
            };
        }


        /// <summary>
        /// 发起注册请求
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private RegisterAction Register(BuildContext context, AppState appState, Dispatcher dispatcher)
        {
            Dictionary<string, string> tmpParamaters = new Dictionary<string, string>
            {
                {"url", new Uri(new Uri(APIsInfo.APIGetWay), APIsInfo.User_Register).ToString()},
                {"headimages", appState.RegisterAvatar},
                {"nickname", appState.NickName},
                {"password", appState.Password},
                {"phone", appState.Account},
            };
            WebServerApiRequest tmpWebServerApiRequest =
                new WebServerApiRequest(tmpParamaters, (result) =>
                    {
                        RequestUserRespon tmpRequestUserRespon = JsonUtility.FromJson<RequestUserRespon>(result);
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
                    failedCallback: (msg) =>
                    {
                        using (WindowProvider.of(context).getScope())
                        {
                            HelperWidgets.ShowDialog("登陆错误", "发生未知错误，无法登陆请稍后重试", context);

                            dispatcher.dispatch(new RegisterAction
                            {
                                webServerApiRequest = new WebServerApiRequest
                                {
                                    RequestResult = RequestResultEnum.RegisterFailed
                                }
                            });
                        }
                    }
                );
            return new RegisterAction {webServerApiRequest = tmpWebServerApiRequest};
        }

        public SetupUserPage(RegisterUserInfoAction registerUserInfoAction) : base(registerUserInfoAction)
        {
            RegisterUserInfoAction = registerUserInfoAction;
        }
    }
}