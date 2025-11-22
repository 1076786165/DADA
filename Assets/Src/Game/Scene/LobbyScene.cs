using UnityEngine;

public class LobbyScene : MonoBehaviour
{
    public Transform _dialogRoot;
    void Start()
    {
        if (_dialogRoot != null){
            DialogManager.I.SetRoot(_dialogRoot);
        }
        else{
            Tools.LogError("DialogRoot is null");
        }
        
    }

}
