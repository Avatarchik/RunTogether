using System;
using System.Collections.Generic;
using Unity.UIWidgets.widgets;
using Datas;
using UIScripts.LoginPage;
using Unit;
using Unity.UIWidgets.Redux;
using UnityEngine;

namespace UIScripts
{
    public class Home : StatelessWidget
    {
        public override Widget build(BuildContext buildContext)
        {
            bool logined = PlayerPrefs.GetString("logined").Equals("Yes");

            if (logined) AutoLogin(buildContext);

            return new StoreConnector<AppState, AppState>(
                converter: (state) => state,
                builder: ((context, model, dispatcher) =>
                    logined ? new MainPage() :
                    model.Logined ? (Widget) new MainPage() : new WelcomePage()
                )
            );
        }

        private void AutoLogin(BuildContext context)
        {
            Dictionary<string, string> tmpRequestParamaters = new Dictionary<string, string>
            {
                {"url", new Uri(new Uri(APIsInfo.APIGetWay), APIsInfo.User_Login).ToString()},
                {"password", PlayerPrefs.GetString("password")},
                {"phone", PlayerPrefs.GetString("account")}
            };

            WebServerApiRequest tmpRequest = new WebServerApiRequest
            (tmpRequestParamaters, (result) =>
                {
                    RequestUserRespon tmpRequestUserRespon = JsonUtility.FromJson<RequestUserRespon>(result);
                    Debug.Log(result);
                    switch (tmpRequestUserRespon.code)
                    {
                        case 103:
                            HelperWidgets.ShowDialog("密码错误", "输入的密码错误，请重新输入密码", context);
                            break;
                        case 104:
                        case 105:
                        case 106:
                            HelperWidgets.ShowDialog("账户不存在", "该账户不存在，请确认账户后重试", context);
                            break;
                        case 200:
                            AppManager.Instance.InitUserData(new UserDatas(tmpRequestUserRespon.data.headimages,
                                tmpRequestUserRespon.data.address, tmpRequestUserRespon.data.nickname));
                            break;
                        case 400:
                            Debug.LogError("404 not found");
                            break;
                    }
                },
                (msg) => { Debug.Log(msg); });
            tmpRequest.Request();
        }
    }
}