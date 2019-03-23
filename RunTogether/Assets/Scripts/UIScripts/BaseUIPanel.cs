using Unity.UIWidgets.engine;
using Unity.UIWidgets.widgets;
using Unity.UIWidgets.material;
using Unity.UIWidgets.ui;
using UnityEngine;

public class BaseUIPanel : UIWidgetsPanel
{
    protected override Widget createWidget()
    {
        return new MaterialApp(
                showPerformanceOverlay: false,
                home: new BaseAppView()
            );
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        FontManager.instance.addFont(Resources.Load<Font>(path: "MaterialIcons-Regular"));
        FontManager.instance.addFont(Resources.Load<Font>(path: "MaterialIcons_Expand"));
    }
}
