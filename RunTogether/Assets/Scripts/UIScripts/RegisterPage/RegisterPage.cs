using System;
using System.Collections.Generic;
using Datas;
using UIScripts.Externs;
using Unit;
using Unity.UIWidgets;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Image = Unity.UIWidgets.widgets.Image;

namespace UIScripts.RegisterPage
{
    public class RegisterPage : StatelessWidget
    {
        private static readonly RegisterUserInfoAction RegisterUserInfoAction = new RegisterUserInfoAction();

        public override Widget build(BuildContext context)
        {
            return new SetupAccountPage(RegisterUserInfoAction);
        }
    }
}