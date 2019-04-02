﻿using System.Collections.Generic;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts
{
    public class BaseAppViewState : State<BaseAppView>
    {
        public static int CurrentSelected = 0;
        private readonly  List<Widget> Views = new List<Widget>();
        
        public override void initState()
        {
            base.initState();
            Views.Add(new LoginPage.LoginPage());
            Views.Add(new MainView());
        }

        public override Widget build(BuildContext buildContext)
        {
            return Views[CurrentSelected];
        }

    }

    public class BaseAppView : StatefulWidget
    {
        public BaseAppView(Key key = null) : base(key)
        {
        }

        public override State createState()
        {
            return new BaseAppViewState();
        }
    }
}