using System;
using System.Collections.Generic;
using UIScripts.Externs;
using Unity.UIWidgets;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts.RegisterPage
{
    public class SetupVerfyCode : RegisterPageBase
    {
        private readonly TextEditingController VerfyCodeEdit = new TextEditingController("");
        private Dispatcher Dispatcher;

        internal SetupVerfyCode(RegisterUserInfoAction registerUserInfoAction) : base(registerUserInfoAction)
        {
            RegisterUserInfoAction = registerUserInfoAction;
        }

        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, AppState>(
                converter: (state) => state,
                builder: ((buildContext, model, dispatcher) =>
                    new Scaffold(
                        appBar: HelperWidgets._buildCloseAppBar(isCenterTitle: true, title: new Text("注册新账户"),
                            lealding: new IconButton(
                                icon: new Icon(icon: Icons.arrow_back_ios, size: 18),
                                onPressed: () =>
                                {
                                    HelperWidgets.PopRoute(context);
                                    dispatcher.dispatch(new RegisterUserInfoAction
                                    {
                                        CanRegister = false,
                                        CanGoToUserPage = false,
                                        CanGoToVerfyCodePage = true,
                                        IsPop = true
                                    });
                                })
                        ),
                        backgroundColor: CustomTheme.CustomTheme.EDColor,
                        body: new Column(
                            children: new List<Widget>
                            {
                                new Container(
                                    margin: EdgeInsets.only(top: 100, left: 20, bottom: 20),
                                    alignment: Alignment.centerLeft,
                                    child: new Text("验证你的信息",
                                        style: new TextStyle(fontWeight: FontWeight.w500, fontSize: 20))
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
                                        child: new Text("验证",
                                            style: CustomTheme.CustomTheme.DefaultTextThemen.display2),
                                        onPressed: model.CanGoToUserPage ? VerfyCode(context, dispatcher) : null,
                                        elevation: 0,
                                        disabledColor: Colors.green.withAlpha(125)
                                    )
                                ),
                            }
                        ))
                )
            );
        }

        private VoidCallback VerfyCode(BuildContext context, Dispatcher dispatcher)
        {
            return () => { HelperWidgets.PushNewRoute(context, new SetupUserPage(RegisterUserInfoAction)); };
        }
    }
}