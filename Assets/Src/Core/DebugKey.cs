using UnityEngine;

public class DebugKey : MonoBehaviour
{
    private DebugDialog _debugDialog;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (_debugDialog == null)
            {
                _debugDialog = (DebugDialog)DialogManager.I.CreateDialog("DebugDialog");
                _debugDialog.Show();
            }
            else
            {
                _debugDialog.Close();
                _debugDialog = null;
            }
        }
      
    }
}
