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
    public class RegisterPage : StatefulWidget
    {
        public override State createState()
        {
            return new RegisterTcikerProviderState();
        }
    }


    public class RegisterTcikerProviderState : SingleTickerProviderStateMixin<RegisterPage>
    {
        private string AvatarPath;
        private TextEditingController NameEdit = new TextEditingController("");
        private TextEditingController PasswordEdit = new TextEditingController("");
        private TextEditingController PhoneEdit = new TextEditingController("");
        private TextEditingController VerfyCodeEdit = new TextEditingController("");
        private BuildContext buildContext;
        private bool SendVerfyCode;

        private Animation<int> CountDown;
        private AnimationController CountDownController;

        public override void initState()
        {
            base.initState();
            AvatarPath = Application.streamingAssetsPath + "/avatar.png";
            CountDownController = new AnimationController(vsync: this, duration: new TimeSpan(0, 1, 0));
            CountDownController.addListener(Refresh);
            CountDownController.addStatusListener(Reset);
            CountDown = new IntTween(60, 0).animate(CountDownController);
        }


        public override Widget build(BuildContext context)
        {
            buildContext = context;
            return new Scaffold(
                appBar: HelperWidgets._buildCloseAppBar(isCenterTitle: true, title: new Text(""),
                    lealding: new StoreConnector<AppState, object>(
                        converter: state => null,
                        builder: ((BuildContext, model, dispatcher) => new IconButton(
                            icon: new Icon(icon: Icons.close),
                            onPressed: () =>
                            {
                                dispatcher.dispatch(new RegisterState()
                                {
                                    ClickedNextButton = false,
                                    Context = context
                                });
                            })),
                        pure: true
                    )),
                backgroundColor: CustomTheme.CustomTheme.EDColor,
                body: _buildBody()
            );
        }


        private Widget _buildBody()
        {
            return new Container(
                margin: EdgeInsets.only(top: 50),
                child: new Column(
                    children: new List<Widget>
                    {
                        new Container(
                            alignment: Alignment.center,
                            margin: EdgeInsets.only(bottom: 50),
                            child: new Text("用手机号注册", style: new TextStyle(fontWeight: FontWeight.bold, fontSize: 20))
                        ),

                        new GestureDetector(
                            child: new AvatarWidget(
                                HelperWidgets._createImageProvider(AvatarImageType.Memory, AvatarPath)),
                            onTap: () =>
                            {
#if !UNITY_EDITOR && UNITY_IOS
                                NativeGallery.GetImageFromGallery((path) =>
                                {
                                    if(string.IsNullOrEmpty(path))return;
                                    using(WindowProvider.of(context).getScope()) 
                                    {
                                        AvatarPath = path;

                                        setState();
                                    }
                                });
#endif
                            }
                        ),

                        new Padding(padding: EdgeInsets.only(top: 5)),

                        new TextFieldExtern("请设置昵称，如一起跑", padding: EdgeInsets.only(left: 20, right: 20, top: 20),
                            editingController: NameEdit),

                        new TextFieldExtern("请填写手机号码", padding: EdgeInsets.only(left: 20, right: 20, top: 20),
                            editingController: PhoneEdit, maxLength: 11, regexCondition: @"^[0-9]*$"),

                        new TextFieldExtern("请填写密码(英文字符、数字)", padding: EdgeInsets.only(left: 20, right: 20, top: 20),
                            obscureText: true, editingController: PasswordEdit, maxLength: 16,
                            regexCondition: @"^[A-Za-z0-9]+$"),

                        new Stack(
                            children: new List<Widget>
                            {
                                new TextFieldExtern("请填写验证码", padding: EdgeInsets.only(left: 20, right: 20, top: 20),
                                    maxLength: 6, editingController: VerfyCodeEdit),

                                new StoreConnector<AppState, AppState>(
                                    converter: (state) => state,
                                    builder: ((context1, model, dispatcher) =>
                                        model.SendVerfyCode
                                            ? new Container(
                                                alignment: Alignment.centerRight,
                                                padding: EdgeInsets.only(right: 25, bottom: 5),
                                                child: new CountdownWidget(
                                                    timeSpan: new TimeSpan(0, 0, 60),
                                                    () =>
                                                    {
                                                        dispatcher.dispatch(new SendVerfyCodeState()
                                                            {SendVerfyCode = false});
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
                                                        dispatcher.dispatch(new CountdownState() {CountdownTime = 60});
                                                        dispatcher.dispatch(new SendVerfyCodeState()
                                                            {SendVerfyCode = true});
                                                    }
                                                )
                                            )
                                    )
                                )
                            }
                        ),


                        new SizedBox(
                            width: 340,
                            height: 60,
                            child: new Padding(
                                padding: EdgeInsets.only(top: 20),
                                child: new FlatButton(color: Colors.green,
                                    child: new Text("注册",
                                        style: CustomTheme.CustomTheme.DefaultTextThemen.display2),
                                    onPressed: () =>
                                    {
                                        //TODO:Register

                                        //防止用户漏空提交
                                        if (string.IsNullOrEmpty(NameEdit.text)
                                            || string.IsNullOrEmpty(PhoneEdit.text)
                                            || string.IsNullOrEmpty(PasswordEdit.text)
                                            || string.IsNullOrEmpty(VerfyCodeEdit.text))
                                        {
                                            return;
                                        }

                                        AppManager.Instance.InitUserData(new UserDatas(AvatarPath, "", NameEdit.text));
                                        Navigator.pop(buildContext);
                                    }
                                )
                            )
                        )
                    }
                )
            );
        }

        public override void dispose()
        {
            CountDownController.removeListener(Refresh);
            CountDownController.removeStatusListener(Reset);
            CountDownController.stop();
            CountDownController.dispose();
            base.dispose();
        }

        private void Reset(AnimationStatus status)
        {
            if (status != AnimationStatus.completed) return;
            SendVerfyCode = false;
            CountDownController.reset();
            setState();
        }

        private void Refresh()
        {
            setState();
        }

        //private void CountDown()
        //{
        //    tmp_Timer = new Timer(60000);
        //    tmp_Timer.Elapsed += new ElapsedEventHandler(OnCountDownElapsed);
        //    tmp_Timer.Interval = 1000;
        //    tmp_Timer.Start();
        //}

        //private void OnCountDownElapsed(object sender, ElapsedEventArgs e)
        //{
        //    using (WindowProvider.of(context).getScope())
        //    {
        //        CurrentCoundown -= 1;
        //        if (CurrentCoundown <= 0)
        //        {
        //            SendVerfyCode = false;
        //            CurrentCoundown = 60;
        //            tmp_Timer.Stop();
        //        }
        //        setState();
        //    }
        //}
    }
}