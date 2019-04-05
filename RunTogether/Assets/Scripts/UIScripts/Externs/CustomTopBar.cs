using System.Collections;
using System.Collections.Generic;
using Unity.UIWidgets.material;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;
using Canvas = Unity.UIWidgets.ui.Canvas;
using Color = Unity.UIWidgets.ui.Color;
public class CustomTopBar : StatelessWidget
{
    public override Widget build(BuildContext context)
    {
        return new CustomPaint(
            size:new Size(2000,200),
            painter:new CurvePainter()
        );
    }
}

public class CurvePainter : CustomPainter
{
    public void addListener(VoidCallback listener)
    {
       
    }

    public void paint(Canvas canvas, Size size)
    {
        Path path = new Path();
        Paint paint = new Paint();
        path.moveTo(0,0);
        path.lineTo(0, size.height *0.75f);
        path.quadTo(size.width* 0.1f, size.height*0.7f,   size.width*0.17f, size.height*0.9f);
        path.quadTo(size.width*0.2f, size.height, size.width*0.25f, size.height*0.9f);
        path.quadTo(size.width*0.4f, size.height*0.4f, size.width*0.5f, size.height*0.7f);
        path.quadTo(size.width*0.6f, size.height*0.85f, size.width*0.85f, size.height*0.65f);
        path.quadTo(size.width*0.7f, size.height*0.9f, size.width, 0);
        path.close();
        
        paint.color = new Color(0xfaff0000);
        canvas.drawPath(path, paint);
        
        path = new Path();
        path.moveTo(0,0);
        path.lineTo(0, size.height*0.5f);
        path.quadTo(size.width*0.1f, size.height*0.8f, size.width*0.15f, size.height*0.6f);
        path.quadTo(size.width*0.2f, size.height*0.45f, size.width*0.27f, size.height*0.6f);
        path.quadTo(size.width*0.45f, size.height, size.width*0.5f, size.height*0.8f);
        path.quadTo(size.width*0.55f, size.height*0.45f, size.width*0.75f, size.height*0.75f);
        path.quadTo(size.width*0.85f, size.height*0.93f, size.width, size.height*0.6f);
        path.lineTo(size.width, 0);
        path.close();
        paint.color = new Color(0xfcff0000);
        canvas.drawPath(path, paint);

        path =new Path();
        path.moveTo(0,0);
        path.lineTo(0, size.height*0.75f);
        path.quadTo(size.width*0.1f, size.height*0.55f, size.width*0.22f, size.height*0.7f);
        path.quadTo(size.width*0.3f, size.height*0.9f, size.width*0.4f, size.height*0.75f);
        path.quadTo(size.width*0.52f, size.height*0.5f, size.width*0.65f, size.height*0.7f);
        path.quadTo(size.width*0.75f, size.height*0.85f, size.width, size.height*0.6f);
        path.lineTo(size.width, 0);
        path.close();
        paint.color = new Color(0xffff0000);
        canvas.drawPath(path, paint);
    }

    public void removeListener(VoidCallback listener)
    {
        
    }

    public bool shouldRepaint(CustomPainter oldDelegate)
    {
        return oldDelegate != this;
    }

    public bool? hitTest(Offset position)
    {
        return true;
    }
}