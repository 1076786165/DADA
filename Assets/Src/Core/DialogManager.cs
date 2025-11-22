using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogManager : MonoBehaviourSingleton<DialogManager>
{
    public Transform _dialogRoot;
    private List<GameObject> _dialogsPrefabs;

    public Dictionary<string, GameObject> _dialogsPrefabsDict = new Dictionary<string, GameObject>();

    public void SetRoot(Transform root)
    {
        _dialogRoot = root;
    }

    protected override void Start(){
        _dialogsPrefabs = Resources.LoadAll<GameObject>("Prefab/Dialogs").ToList();
        
        foreach (var dialog_prefab in _dialogsPrefabs)
        {
            _dialogsPrefabsDict[dialog_prefab.name] = dialog_prefab;
        }
    }

    public DialogBase CreateDialog(string dialog_name)
    {
        if (_dialogRoot != null &&_dialogsPrefabsDict.ContainsKey(dialog_name))
        {
            var dialog_prefab = _dialogsPrefabsDict[dialog_name];
            var dialog_obj = Instantiate(dialog_prefab, _dialogRoot);
            return dialog_obj.GetComponent<DialogBase>();
        }

        return null;
    }
    
}
