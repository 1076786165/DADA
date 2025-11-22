using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonExt:Button
    {
        [Header("是否播放按钮音效")]
        public bool playSound = true;
        [SerializeField]
        public Color childrenPressedColor = new(0.7f, 0.7f, 0.7f);
        
        private List<Graphic> childGraphics = new ();
        private Dictionary<Graphic, Color> originalColors = new();


    protected override void Start()
    {
        base.Start();
        FindChildGraphics();
        onClick.AddListener(PlaySound);
    }

    private void PlaySound()
    {
        if (playSound)
        {
            AudioManager.I.PlaySound("common_anniuyinxiao");
        }
    }

    void FindChildGraphics()
    {
        foreach (Transform child in transform)
        {
            Graphic[] graphics = child.GetComponentsInChildren<Graphic>();
            foreach (Graphic graphic in graphics)
            {
                // 跳过按钮自身的图像
                if (graphic.gameObject == gameObject) continue;
                
                childGraphics.Add(graphic);
                originalColors.Add(graphic, graphic.color);
            }
        }
    }

        // 状态变化时更新子节点颜色
    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);
        
        switch (state)
        {
            case SelectionState.Pressed:
                SetChildrenColor(childrenPressedColor);
                break;
            default:
                ResetChildrenColor();
                break;
        }
    }

    void SetChildrenColor(Color color)
    {
        foreach (Graphic graphic in childGraphics)
        {
            if (graphic != null) graphic.color = color;
        }
    }

    void ResetChildrenColor()
    {
        foreach (Graphic graphic in childGraphics)
        {
            if (graphic != null && originalColors.ContainsKey(graphic))
            {
                graphic.color = originalColors[graphic];
            }
        }
    }
}