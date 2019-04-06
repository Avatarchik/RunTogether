using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Timers;
using Unity.UIWidgets;
using Unity.UIWidgets.animation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEditor.AppleTV;
using UnityEngine;

namespace UIScripts.LoginPage
{
    public class CountdownWidget : StatefulWidget
    {
        private readonly TimeSpan? TimeSpan;
        private readonly Action OnCompleted;

        public CountdownWidget(TimeSpan? timeSpan = null, Action onCompleted = null)
        {
            TimeSpan = timeSpan;

            OnCompleted = onCompleted;
        }

        public override State createState()
        {
            return new CountdownWidgetState(TimeSpan, OnCompleted);
        }
    }


    public class CountdownWidgetState : SingleTickerProviderStateMixin<CountdownWidget>
    {
        private readonly AnimationController Controller;
        private readonly Animation<int> CountValue;
        private readonly Action OnCompleted;
        private int CurrentCoundown = 60;
        private Dispatcher Dispatcher;

        public CountdownWidgetState(TimeSpan? timeSpan, Action onCompleted)
        {
            base.initState();
            OnCompleted = onCompleted;
            Controller = new AnimationController(vsync: this, duration: timeSpan);
            Controller.addListener(() => { Dispatcher.dispatch(new CountdownState() {Countdown = CountValue.value}); });
            Controller.addStatusListener((status) =>
            {
                if (status == AnimationStatus.completed)
                {
                    Controller.reset();
                    OnCompleted?.Invoke();
                }
            });
            CountValue = new IntTween(60, 0).animate(Controller);
            Controller.forward();
        }

        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, string>
            (
                converter: (state) =>
                {
                    Debug.Log(state.CountdownString);
                    return $"{state.CountdownString}s";
                },
                builder: ((buildContext, model, dispatcher) =>
                {
                    Dispatcher = dispatcher;
                    return new Text(model,
                        style: new TextStyle(fontSize: 40, fontWeight: FontWeight.w700));
                }),
                pure: true
            );
        }
    }
}