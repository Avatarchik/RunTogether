using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.widgets;

namespace UIScripts.Externs
{
    public class AvatarWidget : StatelessWidget
    {
        private readonly Image ImageProvider;
        private readonly float Width;
        private readonly float Height;

        public AvatarWidget(Image imageProvider, float width = 64, float height = 64)
        {
            this.ImageProvider = imageProvider;
            this.Width = width;
            this.Height = height;
        }

        public override Widget build(BuildContext context)
        {
            return
                new Container(
                    child: new ClipRRect(
                        borderRadius: BorderRadius.circular(5),
                        child: new Container(
                            width: Width,
                            height: Height,
//                            decoration: new BoxDecoration(
//                                shape: BoxShape.rectangle,
//                                image: new DecorationImage(ImageProvider, fit: BoxFit.cover),
//                                borderRadius: BorderRadius.circular(5)
//                            )
                            child: ImageProvider
                        )
                    )
                );
        }
    }
}