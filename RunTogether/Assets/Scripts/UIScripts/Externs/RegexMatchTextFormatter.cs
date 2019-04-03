using System.Text.RegularExpressions;
using Unity.UIWidgets.service;

namespace UIScripts.Externs
{
    public class RegexMatchTextFormatter : TextInputFormatter
    {
        private readonly string regexPattern;
        public bool IsMatch { get; private set; }

        public RegexMatchTextFormatter(string regexPattern)
        {
            this.regexPattern = regexPattern;
        }

        public override TextEditingValue formatEditUpdate(TextEditingValue oldValue, TextEditingValue newValue)
        {
            if (!Regex.IsMatch(newValue.text, regexPattern))
            {
                IsMatch = false;
                return new TextEditingValue(oldValue.text,
                    selection: oldValue.selection,
                    composing: TextRange.empty);
            }

            IsMatch = true;
            return newValue;
        }
    }
}