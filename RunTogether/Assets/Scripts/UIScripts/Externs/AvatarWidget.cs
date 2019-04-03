using Unity.UIWidgets.painting;
using Unity.UIWidgets.widgets;

namespace UIScripts.Externs
{
    public class AvatarWidget : StatelessWidget
    {
        private readonly ImageProvider ImageProvider;
        private readonly float Width;
        private readonly float Height;

        public AvatarWidget(ImageProvider imageProvider, float width = 64, float height = 64)
        {
            this.ImageProvider = imageProvider;
            this.Width = width;
            this.Height = height;
        }

        public override Widget build(BuildContext context)
        {
            return new Container(
                width: Width,
                height: Height,
                decoration: new BoxDecoration(
                    shape: BoxShape.circle,
                    image: new DecorationImage(ImageProvider, fit: BoxFit.cover)
                )
            );
        }
    }
}