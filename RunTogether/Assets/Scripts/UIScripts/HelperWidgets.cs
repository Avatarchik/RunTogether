using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Datas;
using JetBrains.Annotations;
using Unit;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using DialogUtils = Unity.UIWidgets.material.DialogUtils;

namespace UIScripts
{
    public class HelperWidgets
    {
        public static bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, @"^[A-Za-z_0-9]{8,16}$");
        }

        public static bool IsCellphoneNumber(string str_handset)
        {
            return Regex.IsMatch(str_handset, @"^[1]+[3,4,5,8]+\d{9}");
        }

        public static bool IsTelephone(string str_telephone)
        {
            return Regex.IsMatch(str_telephone, @"^(\d{3,4}-)?\d{6,8}$");
        }


        public static void PushNewRoute(BuildContext context, Widget widget)
        {
            Route tmpRoute = new PageRouteBuilder(
                pageBuilder: ((pageContext, animation, secondaryAnimation) => widget),
                transitionsBuilder: ((transContext, animation, secondaryAnimation,
                        child) =>
                    new PageTransition(routeAnimation: animation, child: child,
                        beginDirection: new Offset(2f, 0f),
                        endDirection: Offset.zero))
            );
            Navigator.push(context: context, route: tmpRoute);
        }

        public static void PopRoute(BuildContext context)
        {
            Navigator.pop(context: context);
        }


        public static AppBar _buildCloseAppBar(bool isCenterTitle = true, Widget lealding = null, Widget title = null,
            Action closeAction = null)
        {
            D.assert(lealding == null || closeAction == null);
            return new AppBar(
                leading: lealding ?? new IconButton(icon: new Icon(icon: Icons.close),
                             onPressed: () => { closeAction?.Invoke(); }),
                title: title,
                centerTitle: isCenterTitle,
                backgroundColor: CustomTheme.CustomTheme.EDColor,
                textTheme: CustomTheme.CustomTheme.DefaultTextThemen,
                iconTheme: CustomTheme.CustomTheme.AppbarIconThemen,
                elevation: 0
            );
        }


        public static ImageProvider _createImageProvider(AvatarImageType imageType, string _imagePath)
        {
            ImageProvider tmp_ImageProvider = null;

            switch (imageType)
            {
                case AvatarImageType.NetWork:
                    tmp_ImageProvider = new NetworkImage(_imagePath);
                    break;
                case AvatarImageType.Asset:
                    tmp_ImageProvider = new AssetImage(_imagePath);
                    break;
                case AvatarImageType.Memory:
                    byte[] bytes = File.ReadAllBytes(_imagePath);
                    tmp_ImageProvider = new MemoryImage(bytes: bytes);
                    break;
            }

            return tmp_ImageProvider;
        }


        public static void ShowDialog(string title, string content, BuildContext context)
        {
            DialogUtils.showDialog(context: context, builder: (buildContext => new AlertDialog(
                        title: new Text(title),
                        content: new Text(content),
                        actions: new List<Widget>
                        {
                            new FlatButton(child: new Text("Ok"), onPressed: () => { Navigator.pop(context); })
                        }
                    )
                )
            );
        }


        public static WebServerApiRequest Login(BuildContext context, bool autoRequest = true,
            string account = null, string password = null, [CanBeNull] Action<RequestUserRespon> successedResult = null,
            [CanBeNull] Action<string> failedResult = null)
        {
            Dictionary<string, string> tmpRequestParamaters = new Dictionary<string, string>
            {
                {"url", new Uri(new Uri(APIsInfo.APIGetWay), APIsInfo.User_Login).ToString()},
                {"password", password ?? PlayerPrefs.GetString("password")},
                {"phone", account ?? PlayerPrefs.GetString("account")}
            };

            WebServerApiRequest tmpRequest = new WebServerApiRequest
            (tmpRequestParamaters, (result) =>
                {
                    try
                    {
                        RequestUserRespon tmpRequestUserRespon = JsonUtility.FromJson<RequestUserRespon>(result);
                        using (WindowProvider.of(context).getScope())
                        {
                            switch (tmpRequestUserRespon.code)
                            {
                                case 103:
                                    ShowDialog("密码错误", "输入的密码错误，请重新输入密码", context);
                                    break;
                                case 104:
                                case 105:
                                case 106:
                                    ShowDialog("账户不存在", "该账户不存在，请确认账户后重试", context);
                                    break;
                                case 200:
                                    AppManager.Instance.InitUserData(new UserDatas(tmpRequestUserRespon.data.headimages,
                                        tmpRequestUserRespon.data.address, tmpRequestUserRespon.data.nickname));
                                    successedResult?.Invoke(tmpRequestUserRespon);
                                    break;
                                case 400:
                                    Debug.LogError("404 not found");
                                    break;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        failedResult?.Invoke(e.Message);
                    }
                },
                failedCallback: failedResult
            );
            if (autoRequest)
            {
                tmpRequest.Request();
                return null;
            }

            else
                return tmpRequest;
        }
    }

    public enum AvatarImageType
    {
        NetWork,
        Asset,
        Memory
    }
}