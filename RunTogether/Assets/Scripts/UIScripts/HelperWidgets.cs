using System;
using System.IO;
using Datas;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.service;
using Unity.UIWidgets.widgets;
using TextField = Unity.UIWidgets.material.TextField;

namespace UIScripts
{
    public class TextFieldHelper : StatelessWidget
    {
        private readonly string HintText;
        private readonly TextInputAction InputAction;
        private readonly TextEditingController EditingController;
        private readonly EdgeInsets Padding;
        private readonly bool ObscureText;

        public TextFieldHelper(string hintText,
            EdgeInsets padding = null,
            bool obscureText = false,
            TextEditingController editingController=null 
        )
        {
            HintText = hintText;
            EditingController = editingController;
            Padding = padding;
            ObscureText = obscureText;
        }

        public override Widget build(BuildContext context)
        {
            return new Padding(
                padding: Padding,
                child: new TextField(
                    controller: EditingController,
                    autofocus: false,
                    maxLines: 1,
                    obscureText: ObscureText,
                    decoration: new InputDecoration(
                        hintText: HintText,
                        contentPadding: EdgeInsets.all(5.0f),
                        fillColor: Colors.transparent,
                        filled: true,
                        focusedBorder: new UnderlineInputBorder(
                            borderSide: new BorderSide(color: Colors.black)
                        )
                    )
                )
            );
        }
    }


    public class HelperWidgets
    {
        
        public static AppBar _buildCloseAppBar(bool isCenterTitle = true, Widget title = null,
            Action closeAction = null)
        {
            return new AppBar(
                leading: new IconButton(icon: new Icon(icon: Icons.close), onPressed: () => { closeAction?.Invoke(); }),
                title: title,
                centerTitle: isCenterTitle,
                backgroundColor: CustomTheme.CustomTheme.EDColor,
                textTheme: CustomTheme.CustomTheme.DefaultTextThemen,
                iconTheme: CustomTheme.CustomTheme.AppbarIconThemen
            );
        }


        public static Widget _buildBaseScaffold(PreferredSizeWidget appBar, Widget body)
        {
            return new Scaffold(appBar: appBar, body: body);
        }


        public static Widget _buildAvatar(ImageProvider imageProvider)
        {
            return new Container(
                width: 64,
                height: 64,
                decoration: new BoxDecoration(
                    shape: BoxShape.circle,
                    image: new DecorationImage(imageProvider, fit: BoxFit.cover)
                )
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