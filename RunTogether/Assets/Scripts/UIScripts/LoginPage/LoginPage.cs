using System;
using System.Collections.Generic;
using Unity.UIWidgets.animation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.rendering;
using Unity.UIWidgets.service;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Image = Unity.UIWidgets.widgets.Image;
using Transform = Unity.UIWidgets.widgets;

namespace UIScripts.LoginPage
{
    public class LoginPage : StatefulWidget
    {
        internal bool logined = false;
        public override State createState()
        {
            return new LoginState();
        }
    }


    public class LoginState : TickerProviderStateMixin<LoginPage>
    {
        public override Widget build(BuildContext buildContext)
        {
            return new Stack(
                children: new List<Widget>
                {
                    new WelcomeWidgets()
                }
            );
        }
    }
}