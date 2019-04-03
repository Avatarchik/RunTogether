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
        internal readonly TextInputAction InputAction;
        internal readonly TextEditingController EditingController;
        internal readonly EdgeInsets Padding;
        internal readonly bool ObscureText;
        internal readonly VoidCallback onEditingComplete;
        internal readonly ValueChanged<string> onSubmitted;
        internal RegexMatchTextFormatter RegexMatch;
        internal readonly List<TextInputFormatter> TextInputFormatter;

        private readonly string RegexCondition;
        private readonly int MaxLength;

        
        public TextFieldExtern(string hintText,
            EdgeInsets padding = null,
            bool obscureText = false,
            TextEditingController editingController = null,
            VoidCallback onEditingComplete = null,
            ValueChanged<string> onSubmitted = null,
            int maxLength = 32,
            string regexCondition = null
        )
        {
            HintText = hintText;
            EditingController = editingController;
            Padding = padding;
            ObscureText = obscureText;
            this.onEditingComplete = onEditingComplete;
            this.onSubmitted = onSubmitted;
            this.MaxLength = maxLength;
            this.RegexCondition = regexCondition;
            this.TextInputFormatter = new List<TextInputFormatter>();
            TextInputFormatter.Add(new LengthLimitingTextInputFormatter(MaxLength));
            if(regexCondition!=null)
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
            return new Padding(
                padding: widget.Padding,
                child: new TextField(
                    controller: widget.EditingController,
                    autofocus: false,
                    maxLines: 1,
                    obscureText: widget.ObscureText,
                    focusNode: new FocusNode(),
                    decoration: new InputDecoration(
                        labelText: widget.HintText,
                        contentPadding: EdgeInsets.all(5.0f),
                        fillColor: Colors.transparent,
                        filled: true,
                        focusedBorder: new UnderlineInputBorder(
                            borderSide: new BorderSide(color: Colors.black)
                        )                        
                    ),
                    onEditingComplete: widget.onEditingComplete,
                    onSubmitted: widget.onSubmitted,
                    inputFormatters: widget.TextInputFormatter
                )
            );
        }
    }
}