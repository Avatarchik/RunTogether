using Unity.UIWidgets.engine;
using Unity.UIWidgets.widgets;
using Unity.UIWidgets.material;

public class BaseUIPanel : UIWidgetsPanel
{
    protected override Widget createWidget()
    {
        return new MaterialApp(
                showPerformanceOverlay: false,
                home: new PersonalProfile()
            );
    }
}
