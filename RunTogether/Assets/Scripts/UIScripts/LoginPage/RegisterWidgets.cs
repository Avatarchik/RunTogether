using System.Collections.Generic;
using Datas;
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

        public override void initState()
        {
            base.initState();
            AvatarPath = Application.streamingAssetsPath + "/avatar.png";
        }

        public override Widget build(BuildContext context)
        {
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
                            child: HelperWidgets._buildAvatar(
                                HelperWidgets._createImageProvider(AvatarImageType.Memory, AvatarPath)),
                            onTap: () =>
                            {
#if UNITY_EDITOR
                                AvatarPath = Application.dataPath + "/Artwork/Textures/splashscreen_white 10.40.31.jpg";
                                setState();
#elif !UNITY_EDITOR && UNITY_IOS
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

                        new TextFieldHelper("请设置昵称，如一起跑", padding: EdgeInsets.only(left: 20, right: 20, top: 20),
                            editingController: NameEdit),
                        
                        new TextFieldHelper("请填写手机号码", padding: EdgeInsets.only(left: 20, right: 20, top: 20),
                            editingController: PhoneEdit),
                        
                        new ListTile(
                            title: new TextFieldHelper("请填写验证码", padding: EdgeInsets.only(top: 20),
                                editingController: VerfyCodeEdit),
                            trailing: new IconButton(icon: new Icon(icon: Icons.send))),
                        
                        new TextFieldHelper("请设置密码", padding: EdgeInsets.only(left: 20, right: 20, top: 20),
                            obscureText: true,
                            editingController: PasswordEdit),

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
                                        AppManager.Instance.InitUserData(new UserDatas(AvatarPath,"",NameEdit.text));
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