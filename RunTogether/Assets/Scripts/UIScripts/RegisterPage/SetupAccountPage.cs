using System;
using System.Collections.Generic;
using Unity.UIWidgets;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts.RegisterPage
{
    public class SetupAccountPage : RegisterPageBase
    {
        private readonly TextEditingController PasswordEdit = new TextEditingController("");
        private readonly TextEditingController PasswordEditAgain = new TextEditingController("");
        private readonly TextEditingController PhoneEdit = new TextEditingController("");


        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, AppState>(
                converter: (state) => state,
                builder: ((buildContext, model, dispatcher) =>
                    new Scaffold(
                        appBar: HelperWidgets._buildCloseAppBar(isCenterTitle: true, title: new Text("注册账户"),
                            lealding: new IconButton(
                                icon: new Icon(icon: Icons.arrow_back_ios),
                                onPressed: () =>
                                {
                                    HelperWidgets.PopRoute(context);
                                    dispatcher.dispatch(new RegisterUserInfoAction
                                    {
                                        CanRegister = false,
                                        CanGoToUserPage = false,
                                        CanGoToVerfyCodePage = false,
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
                                    child: new Text("用手机号注册",
                                        style: new TextStyle(fontWeight: FontWeight.w500, fontSize: 20))
                                ),
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
                                    errorText: model.PasswordTextFieldErrorText,
                                    obscureText: true, editingController: PasswordEditAgain, maxLength: 16,
                                    regexCondition: @"^[A-Za-z0-9]*$",
                                    onChanged: (text) =>
                                    {
                                        RegisterUserInfoAction.PasswordAgain = text;
                                        dispatcher.dispatch(RegisterUserInfoAction);
                                    }
                                ),
                                new Container(
                                    margin: EdgeInsets.all(20f),
                                    width: MediaQuery.of(context).size.width,
                                    child: new RaisedButton(color: Colors.green,
                                        child: new Text("下一步",
                                            style: CustomTheme.CustomTheme.DefaultTextThemen.display2),
                                        onPressed: model.CanGoToVerfyCodePage ? GoToNextStep(context) : null,
                                        elevation: 0,
                                        disabledColor: Colors.green.withAlpha(125)
                                    )
                                ),
                            }
                        )
                    )
                )
            );
        }


        private VoidCallback GoToNextStep(BuildContext context)
        {
            return () => { HelperWidgets.PushNewRoute(context, new SetupVerfyCode(RegisterUserInfoAction)); };
        }

        public SetupAccountPage(RegisterUserInfoAction registerUserInfoAction) : base(registerUserInfoAction)
        {
            RegisterUserInfoAction = registerUserInfoAction;
        }
    }
}