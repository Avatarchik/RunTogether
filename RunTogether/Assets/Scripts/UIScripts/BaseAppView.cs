using System.Collections.Generic;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using Color = Unity.UIWidgets.ui.Color;
using Material = Unity.UIWidgets.material.Material;


public class BaseAppViewState : SingleTickerProviderStateMixin<BaseAppView>
{
    TabController tabController;
    PageController pageController;
    int currentSelectedIndex = 0;
    public override void initState()
    {
        base.initState();
        tabController = new TabController(initialIndex:0,vsync: this, length: 4);
        pageController = new PageController(initialPage:0);
    }
    public override Widget build(BuildContext context)
    {
        return _BuildBaseElemtns();
    }

    private Widget _BuildBaseElemtns()
    {
        List<BottomNavigationBarItem> bottomNavigationBars = new List<BottomNavigationBarItem>();
        bottomNavigationBars.Add(new BottomNavigationBarItem(icon: new Icon(icon: Icons.directions_run), title: new Text("Run")));
        bottomNavigationBars.Add(new BottomNavigationBarItem(icon: new Icon(icon: Icons.camera), title: new Text("社区")));
        bottomNavigationBars.Add(new BottomNavigationBarItem(icon: new Icon(icon: Icons.person), title: new Text("Run")));
        return new Scaffold(
           backgroundColor: Color.clear,
           body: new PageView(
                    controller: pageController,
                    physics: new NeverScrollableScrollPhysics(),
                    children: new List<Widget> {
                        new Container(color:Colors.transparent),
                        new Container(color:Colors.green),
                        new PersonalProfile(),
                    }
               ),
                  bottomNavigationBar:new BottomAppBar(
                        clipBehavior:Clip.antiAlias,
                        color:Colors.blue,
                        child:new Row(
                                mainAxisAlignment:Unity.UIWidgets.rendering.MainAxisAlignment.center,
                                children:new List<Widget>
                                {
                                    //new IconButton(icon:new Icon(Icons.directions_run),onPressed:()=>{pageController.jumpToPage(0);},iconSize:18,alignment:Alignment.centerLeft),
                                    //new IconButton(icon:new Icon(Icons.camera),onPressed:()=>{pageController.jumpToPage(1);},iconSize:18,alignment:Alignment.center),
                                    //new IconButton(icon:new Icon(Icons.person),onPressed:()=>{pageController.jumpToPage(2);},iconSize:18,alignment:Alignment.centerRight),
                                }
                            )
                  )
                  //bottomNavigationBar: new BottomNavigationBar(
                      //  fixedColor: Colors.blue,
                      //  currentIndex:currentSelectedIndex,
                      //  onTap: OnBottomNavigationItemPressed,
                      //  items: bottomNavigationBars,
                      //  type:BottomNavigationBarType.fix
                      //)
        );
    }
    private void OnBottomNavigationItemPressed(int _index)
    {
        currentSelectedIndex = _index;
        this.setState();
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