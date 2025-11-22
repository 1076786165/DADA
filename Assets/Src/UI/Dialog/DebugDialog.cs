using TMPro;
using UnityEngine.UI;

public class DebugDialog : DialogBase
{
    private TMP_Text _debugText;

    protected override void Awake()
    {
        base.Awake();

        _debugText = UIHelper.FindDeepChild(transform, "DebugText").GetComponent<TMP_Text>();

        _debugText.text = "DebugDialog";

        BindBtns(new string[] {
            
        });
    }

    protected override void Start()
    {
        base.Start();

        Refresh();
    }

    public override void OnBtnClick(Button btn)
    {
        Refresh();

        EventManager.SendEvent(Def.EVENT_UPDATE_COINS);
        EventManager.SendEvent(Def.EVENT_UPDATE_UI);
    }

    public void Refresh()
    {
    }
}
