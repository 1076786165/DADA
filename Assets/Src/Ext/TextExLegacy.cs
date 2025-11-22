using Slot;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class TextExLegacy:TextBase
    {
        [Header("传统文本框")]
        [SerializeField]
        private Text _text;

        public override string GetText()
        {
            return _text.text;
        }

        public override float GetWidth()
        {
            return _text.preferredWidth;
        }

        public override void SetText(string text)
        {
            _text.text = text;
            AdjustTextSize();
        }
    }
}