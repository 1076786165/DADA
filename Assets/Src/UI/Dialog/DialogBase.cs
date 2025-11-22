using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections.Generic;

public class DialogBase : UIBehaviour
{
    protected bool needMask = true;

    protected Dictionary<string, Button> _btns;
    protected Action<string> _onCloseCallback;

    protected GameObject _root;
    protected Image _mask;
    protected Sequence _dialogAniSeq;

    protected override void Awake()
    {
        try{
            _root = UIHelper.FindDeepChild(transform, "Root").gameObject;
        }catch (Exception)
        {
            Tools.LogError($"{GetType().Name}: _root is null");
        }

        if (needMask) CreateMask();
    }

    public virtual void Show()
    {
        if (gameObject == null) return;
        if (_root == null) return;

        _root.transform.localScale = Vector3.zero;
        //设置_mask的透明度为0
        if (_mask != null) _mask.color = new Color(0, 0, 0, 0);

        _dialogAniSeq = DOTween.Sequence();
        _dialogAniSeq.SetLink(_root);
        if (_mask!= null) _dialogAniSeq.Join(_mask.DOFade(0.9f, 0.2f));
        _dialogAniSeq.Join(_root.transform.DOScale(Vector3.one, 0.2f));

        OnShow();
    }
    
    protected virtual void OnShow(){

    }

    public virtual void Close(string cmd = "")
    {
        OnClose();

        if(_dialogAniSeq != null) _dialogAniSeq.Kill();
        _dialogAniSeq = DOTween.Sequence();
        _dialogAniSeq.SetLink(_root);
        if (_mask!= null) _dialogAniSeq.Join(_mask.DOFade(0f, 0.1f));
        _dialogAniSeq.Join(_root.transform.DOScale(Vector3.zero, 0.1f));
        _dialogAniSeq.AppendCallback(() =>
        {
            _onCloseCallback?.Invoke(cmd);
            Destroy(_root);
        });

    }

    protected virtual void OnClose()
    { 
        
    }

    public virtual void SetCloseCallback(Action<string> callback)
    {
        _onCloseCallback = callback;
    }

    public virtual void OnBtnClick(Button btn)
    {

    }

    protected void BindBtns(string[] btn_names)
    {
        _btns = new Dictionary<string, Button>();
        foreach (string name in btn_names)
        {
            Transform trans = UIHelper.FindDeepChild(transform, name);
            if (trans == null) continue;

            Button btn = trans.GetComponent<Button>();
            if (btn != null)
            {
                _btns[name] = btn;
                btn.onClick.AddListener(() => { OnBtnClick(btn); });
            }
        }
    }

    protected void EnableAllBtns(bool enable = true)
    {
        if (_btns == null) return;

        foreach (var btn in _btns.Values)
        {
            btn.interactable = enable;
        }
    }

    public void CreateMask()
    {
        GameObject mask = new GameObject("mask");
        mask.transform.SetParent(transform, false);
        // mask.transform.localScale = Vector3.one * 100f;

        _mask = mask.AddComponent<Image>();
        _mask.color = new Color(0, 0, 0, 0.99f);

        RectTransform rect = mask.GetComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.offsetMin = Vector2.zero;
        rect.offsetMax = Vector2.zero;

        mask.transform.SetAsFirstSibling();
    }

    protected override void OnDestroy()
    {
        StopAllCoroutines();
        DOTween.Kill(_root);
    }


}
