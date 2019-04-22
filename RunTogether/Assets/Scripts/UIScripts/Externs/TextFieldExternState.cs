using System.Collections.Generic;
using UIScripts.Externs;
using Unity.UIWidgets.foundation;
using Unity.UIWidgets.material;
using Unity.UIWidgets.painting;
using Unity.UIWidgets.service;
using Unity.UIWidgets.ui;
using Unity.UIWidgets.widgets;
using UnityEngine;


namespace UIScripts
{
    public class TextFieldExtern : StatefulWidget
    {
        internal readonly string HintText;
        internal readonly string ErrorText;
        internal readonly TextInputAction InputAction;
        internal readonly TextEditingController EditingController;
        internal readonly EdgeInsets Margin;
        internal readonly bool ObscureText;
        internal readonly ValueChanged<string> OnChanged;
        internal readonly ValueChanged<string> OnSubmitted;
        internal RegexMatchTextFormatter RegexMatch;
        internal readonly List<TextInputFormatter> TextInputFormatter;
        internal readonly FocusNode FocusNode;
        private readonly string RegexCondition;
        private readonly int MaxLength;


        public TextFieldExtern(string hintText,
            EdgeInsets margin = null,
            bool obscureText = false,
            TextEditingController editingController = null,
            ValueChanged<string> onChanged = null,
            ValueChanged<string> onSubmitted = null,
            int maxLength = 32,
            string regexCondition = null,
            string errorText = null            
        )
        {           
            ErrorText = errorText;
            HintText = hintText;
            EditingController = editingController;
            Margin = margin;
            ObscureText = obscureText;
            OnChanged = onChanged;
            OnSubmitted = onSubmitted;
            MaxLength = maxLength;
            RegexCondition = regexCondition;
            TextInputFormatter = new List<TextInputFormatter>();
            TextInputFormatter.Add(new LengthLimitingTextInputFormatter(MaxLength));
            if (regexCondition != null)
                TextInputFormatter.Add(new RegexMatchTextFormatter(RegexCondition));
        }


        public override State createState()
        {
            return new TextFieldExternState();
        }
    }


    public class TextFieldExternState : State<TextFieldExtern>
    {
        public override Widget build(BuildContext buildContext)
        {
            return new Container(
                margin: widget.Margin,
                child: new TextField(
                    controller: widget.EditingController,
                    autofocus: false,
                    maxLines: 1,
                    obscureText: widget.ObscureText,
                    decoration: new InputDecoration(
                        errorText: widget.ErrorText,
                        labelText: widget.HintText,                       
                        contentPadding: EdgeInsets.only(2.0f),
                        fillColor: Colors.transparent,
                        filled: false,
                        focusedBorder: new UnderlineInputBorder(
                            borderSide: new BorderSide(color: Colors.black)
                        )
                    ),
                    inputFormatters: widget.TextInputFormatter,
                    onChanged: widget?.OnChanged,
                    onSubmitted: widget?.OnSubmitted,
                    keyboardAppearance: Brightness.light
                )
            );
        }
    }
}