using System;
using System.Collections.Generic;
using Unity.UIWidgets;
using Unity.UIWidgets.animation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.Redux;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts.LoginPage
{
    public class CountdownWidget : StatefulWidget
    {
        public int Count { get; private set; }

        public CountdownWidget(int count)
        {
            Count = count;
        }


        public override State createState()
        {
            return new CountdownWidgetState();
        }
    }


    public class CountdownWidgetState : SingleTickerProviderStateMixin<CountdownWidget>
    {
        private AnimationController Controller;
        private Animation<int> CountValue;
        private Dispatcher Dispatcher;
        public override void initState()
        {
            base.initState();
            Controller = new AnimationController(vsync: this, duration: new TimeSpan(0,1,0));
            Controller.addListener(() =>
            {
                Dispatcher.dispatch(new CountdownProperty(CountValue.value));
            });
            Controller.addStatusListener((status) =>
            {
                if (status == AnimationStatus.completed)
                {
                    Controller.reset();
                }
            });
            CountValue = new IntTween(60, 0).animate(Controller);
        }

        public override Widget build(BuildContext context)
        {
            var tmpStore = new Store<CountdownProperty>(Reducer, new CountdownProperty(CountValue.value));
            return new StoreProvider<CountdownProperty>(store: tmpStore, new SizedBox(
                child:
                
                new Row(
                    
                    children:new List<Widget>
                    {
                        new StoreConnector<CountdownProperty, string>
                        (
                            converter: (state) => $"{state.Count}S",
                            builder: ((buildContext, model, dispatcher) => new Text(model,
                                style: new TextStyle(fontSize: 40, fontWeight: FontWeight.w700))),
                            pure: true
                        ),
                        new StoreConnector<CountdownProperty, VoidCallback>(
                            converter:(store)=>null,
                            builder:build                            
                        )
                    }                        
                )
            ));
        }

        private CountdownProperty Reducer(CountdownProperty previousstate, object action)
        {
            return !(action is CountdownProperty tmpState)
                ? previousstate
                : new CountdownProperty(tmpState.Count);
        }

        private Widget build<ViewModel>(BuildContext context, ViewModel viewModel, Dispatcher dispatcher)
        {
            Dispatcher = dispatcher;
            return new FloatingActionButton(
                child: new Icon(icon: Icons.star),
                onPressed: () => { Controller.forward(); });
        }
    }


    public class CountdownProperty
    {
        public int Count { get; private set; }

        public CountdownProperty(int count = 0)
        {
            Count = count;
        }
    }
}