using System.Collections.Generic;
using Datas;
using UIScripts.Externs;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace UIScripts.LoginPage
{
    public class RegisterWidgets : StatefulWidget
    {
        public override State createState()
        {
            return new RegisterState();
        }
    }


    public class RegisterState : State<RegisterWidgets>
    {
        private string AvatarPath;
        private TextEditingController NameEdit = new TextEditingController("");
        private TextEditingController PasswordEdit = new TextEditingController("");
        private TextEditingController PhoneEdit = new TextEditingController("");
        private TextEditingController VerfyCodeEdit = new TextEditingController("");
        private BuildContext buildContext;
        public override void initState()
        {
            base.initState();
            AvatarPath = Application.streamingAssetsPath + "/avatar.png";
        }

        public override Widget build(BuildContext context)
        {
            buildContext = context;
            return new Scaffold(
                appBar: HelperWidgets._buildCloseAppBar(isCenterTitle: true, title: new Text(""),
                    () => { Navigator.pop(context); }),
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
                            child: new AvatarWidget(HelperWidgets._createImageProvider(AvatarImageType.Memory, AvatarPath)),
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

                        new ListTile(
                            title: new TextFieldExtern("请填写验证码", padding: EdgeInsets.only(top: 20),
                                editingController: VerfyCodeEdit),
                            trailing: new IconButton(icon: new Icon(icon: Icons.send))),
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
    }
}