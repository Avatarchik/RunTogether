using Datas;
using Unity.UIWidgets.engine;
using Unity.UIWidgets.material;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;

namespace UIScripts
{
    public class App : UIWidgetsPanel
    {
        public static MultiLanguage Language;

        protected override Widget createWidget()
        {
            return new MaterialApp(
                showPerformanceOverlay: false,
                home: new Home()
            );
        }
        protected override void OnEnable()
        {           
            Application.targetFrameRate = 60; 
            base.OnEnable();
            FontManager.instance.addFont(Resources.Load<Font>(path: "MaterialIcons-Regular"),familyName:"Material Icons");
            FontManager.instance.addFont(Resources.Load<Font>(path: "MaterialIcons-Expand"),familyName:"Material Icons expand");
            Language = Resources.Load<MultiLanguage>("MultiLanguage");
        }
    }
}
