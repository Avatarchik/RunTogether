using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts.CustomTheme
{
    public static class CustomTheme
    {
        public static readonly IconThemeData AppbarIconThemen = new IconThemeData(color: Colors.grey, size: 24);

        public static readonly TextTheme DefaultTextThemen = new TextTheme(
            display1: new TextStyle(color: Colors.grey, fontSize: 18, fontWeight: FontWeight.bold),
            display2: new TextStyle(color: Colors.white, fontSize: 18, fontWeight: FontWeight.w600),
            display3: new TextStyle(color: Colors.black, fontSize: 18, fontWeight: FontWeight.w600),
            display4: new TextStyle(color: Colors.grey, fontSize: 13, fontWeight: FontWeight.normal),
            headline: new TextStyle(color: Colors.grey, fontSize: 18, fontWeight: FontWeight.bold),
            title: new TextStyle(color: Colors.grey, fontSize: 18, fontWeight: FontWeight.bold),
            button: new TextStyle(color: Colors.white, fontSize: 18, fontWeight: FontWeight.w600));

        public static readonly Color EDColor = new Color(0xffededed);
    }
}