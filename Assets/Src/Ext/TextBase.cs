using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Slot
{
    public abstract class TextBase : MonoBehaviour, IText
    {
        [SerializeField]
        protected bool KeepWidth;

        [SerializeField]
        protected bool LimitWidth;

        [SerializeField]
        protected float MaxWidth;

        [SerializeField]
        protected UnityEvent<float> widthChange;

        public string text
        {
            get
            {
                return GetText();
            }
            set
            {
                SetText(value);
            }
        }


        protected decimal _number;

        protected Coroutine _scrollCoroutine;


        public abstract float GetWidth();

        public abstract void SetText(string text);
        public abstract string GetText();

        protected void OnEnable()
        {
            AdjustTextSize();
        }

        public virtual void AdjustTextSize()
        {
            if (!LimitWidth) return;

            float width = GetWidth();
            float scaleFactor = MaxWidth / width;
            if (scaleFactor < 1f || KeepWidth)
            {
                transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
            
            widthChange?.Invoke(width);
        }

        public virtual decimal GetNumber()
        {
            return _number;
        }

        // public virtual void ScrollTo(decimal num, float during = 1, string format = "0,00", Action cb = null)
        // {
        //     StopScroll();
        //     _scrollCoroutine = StartCoroutine(DoScrollTo(_number, num, during, format, cb));
        // }

        // protected IEnumerator DoScrollTo(decimal from,decimal to, float during, string format, Action cb)
        // {
        //     float eclipsed = 0;

        //     if(to > from)
        //     {
        //         while (eclipsed < during)
        //         {
        //             eclipsed += Time.deltaTime;
        //             float t = Mathf.Min(eclipsed / during,1f);
        //             _number = from.Lerp(to,t);
        //             SetNumber(_number, format);
        //             yield return null;
        //         }
        //     }
        //     SetNumber(to, format);
        //     _number = to;
        //     cb?.Invoke();

        // }

        public virtual void StopScroll()
        {
            if(_scrollCoroutine != null)
            {
                StopCoroutine(_scrollCoroutine);
                _scrollCoroutine = null;
            }
        }


        public virtual void SetNumber(decimal number = 0, string format = "0,00")
        {
            _number = number;
            SetText(number.ToString(format));
        }

        // public virtual void SetTime(float time = 0, string format = "${h}:${m}:${s}")
        // {
        //     SetText(TimeUtil.FormatTime(time, format));
        // }

        public virtual void CountDown(float seconds = 0, string foramt = "", Action onComplete = null)
        {
            // TODO 还没实现，先别用
            throw new System.NotImplementedException(); 
        }

        public void ScrollTo(decimal num, float during = 1, string format = "0,00", Action cb = null)
        {
            throw new NotImplementedException();
        }

        public void SetTime(float time = 0, string format = "")
        {
            throw new NotImplementedException();
        }
    }
}