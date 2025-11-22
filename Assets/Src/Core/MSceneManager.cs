

using UnityEngine.SceneManagement;

public class MSceneManager : MonoBehaviourSingleton<MSceneManager>
{
    public void EnterLobby()
    {
        LoadSceneByName(SCENES.SCENE_LOBBY);
    }

    public void LoadSceneByName(SCENES scene)
    {
        SceneManager.LoadScene((int)scene , LoadSceneMode.Single);
    }
}
