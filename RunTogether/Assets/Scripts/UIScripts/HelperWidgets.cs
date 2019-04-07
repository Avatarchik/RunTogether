using System;
using System.IO;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;

namespace UIScripts
{
    public class HelperWidgets
    {
        public static bool IsCellphoneNumber(string str_handset)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_handset, @"^[1]+[3,4,5,8]+\d{9}");
        }

        public static bool IsTelephone(string str_telephone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_telephone, @"^(\d{3,4}-)?\d{6,8}$");
        }


        public static void PushNewRoute(BuildContext context, Widget widget)
        {
            Route tmpRoute = new PageRouteBuilder(
                pageBuilder: ((pageContext, animation, secondaryAnimation) => widget),
                transitionsBuilder: ((transContext, animation, secondaryAnimation,
                        child) =>
                    new PageTransition(routeAnimation: animation, child: child,
                        beginDirection: new Offset(2f, 0f),
                        endDirection: Offset.zero))
            );
            Navigator.push(context: context, route: tmpRoute);
        }

        public static void PopRoute(BuildContext context)
        {
            Navigator.pop(context: context);
        }


        public static AppBar _buildCloseAppBar(bool isCenterTitle = true, Widget lealding = null, Widget title = null,
            Action closeAction = null)
        {
            D.assert(lealding == null || closeAction == null);
            return new AppBar(
                leading: lealding ?? new IconButton(icon: new Icon(icon: Icons.close),
                             onPressed: () => { closeAction?.Invoke(); }),
                title: title,
                centerTitle: isCenterTitle,
                backgroundColor: CustomTheme.CustomTheme.EDColor,
                textTheme: CustomTheme.CustomTheme.DefaultTextThemen,
                iconTheme: CustomTheme.CustomTheme.AppbarIconThemen
            );
        }


        public static ImageProvider _createImageProvider(AvatarImageType imageType, string _imagePath)
        {
            ImageProvider tmp_ImageProvider = null;

            switch (imageType)
            {
                case AvatarImageType.NetWork:
                    tmp_ImageProvider = new NetworkImage(_imagePath);
                    break;
                case AvatarImageType.Asset:
                    tmp_ImageProvider = new AssetImage(_imagePath);
                    break;
                case AvatarImageType.Memory:
                    byte[] bytes = File.ReadAllBytes(_imagePath);
                    tmp_ImageProvider = new MemoryImage(bytes: bytes);
                    break;
            }

            return tmp_ImageProvider;
        }
    }

    public enum AvatarImageType
    {
        NetWork,
        Asset,
        Memory
    }
}