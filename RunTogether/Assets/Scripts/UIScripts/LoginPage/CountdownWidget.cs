using System;
using Unity.UIWidgets;
using Unity.UIWidgets.animation;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace UIScripts.LoginPage
{
    public class CountdownWidget : StatefulWidget
    {
        private readonly TimeSpan? TimeSpan;
        private readonly Action OnCompleted;
        private readonly int Counter;

        public CountdownWidget(TimeSpan? timeSpan = null, Action onCompleted = null, int Counter = 60)
        {
            TimeSpan = timeSpan;
            this.Counter = Counter;
            OnCompleted = onCompleted;
        }

        public override State createState()
        {
            return new CountdownWidgetState(TimeSpan, OnCompleted, Counter);
        }
    }


    public class CountdownWidgetState : SingleTickerProviderStateMixin<CountdownWidget>
    {
        private readonly AnimationController Controller;
        private readonly Animation<int> CountValue;
        private readonly Action OnCompleted;
        private readonly int CurrentCoundown;
        private Dispatcher Dispatcher;

        public CountdownWidgetState(TimeSpan? timeSpan, Action onCompleted, int Counter)
        {
            base.initState();
            OnCompleted = onCompleted;
            Controller = new AnimationController(vsync: this, duration: timeSpan);
            Controller.addListener(() => { Dispatcher.dispatch(new CountdownState() {CountdownTime = CountValue.value}); });
            Controller.addStatusListener((status) =>
            {
                if (status == AnimationStatus.completed)
                {
                    OnCompleted?.Invoke();
                    Controller.reset();
                }
            });
            CountValue = new IntTween(Counter, 0).animate(Controller);
            Controller.forward();
        }

        public override Widget build(BuildContext context)
        {
            return new StoreConnector<AppState, string>
            (
                converter: (state) => $"{state.CountdownTime}s",
                builder: ((buildContext, model, dispatcher) =>
                {
                    Dispatcher = dispatcher;
                    return new Text(model,
                        style: new TextStyle(fontSize: 40, fontWeight: FontWeight.w700));
                }),
                pure: true
            );
        }

        public override void dispose()
        {
            Debug.Log(CountValue.value);
            Controller.stop();
            Controller.dispose();
            base.dispose();
        }
    }
}