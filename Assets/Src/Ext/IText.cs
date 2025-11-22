using System;

namespace Slot
{
    public interface IText
    {
        string GetText();
        void SetText(string text);
        void SetNumber(decimal number = 0m, string format = "0,00");

        decimal GetNumber();

        void ScrollTo(decimal num, float during = 1f, string format  = "0,00", Action cb = null);

        float GetWidth();

        void AdjustTextSize();

        /// <summary>
        /// 设置为时间显示
        /// </summary>
        /// <param name="time">时间戳</param>
        /// <param name="format">格式y,m,d</param>
        void SetTime(float time = 0, string format = "");

        void CountDown(float seconds = 0, string foramt = "", Action onComplete = null);
    }
}