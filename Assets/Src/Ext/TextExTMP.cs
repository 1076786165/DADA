using Slot;
using TMPro;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(TMP_Text))]

    public class TextExTMP: TextBase
    {
        [Header("专业文本框")]
        [SerializeField]
        private TMP_Text _tmpText;

        public override string GetText()
        {
            return _tmpText.text;
        }

        public override float GetWidth()
        {
            return _tmpText.preferredWidth;
        }

        public override void SetText(string text)
        {
            _tmpText.text = text;
            AdjustTextSize();
        }

    }
}