using UnityEngine;

public class PlayerManager : MonoBehaviourSingleton<PlayerManager>, IArchiveAble, IInitable
{
    PlayerData PlayerData;

    string savePath = "SaveFile_PlayerData.es3";

    public bool _isInited { get; set; }

    protected override void Start()
    {
        ((IInitable)this).Init();
        ((IInitable)this).MarkInitEnd();
    }

    public void Init()
    {
    }


    /// <summary>
    /// save & load
    /// </summary>
    public void LoadData()
    {
        if (PlayerData == null)
        {
            if (ES3.FileExists(savePath))
            {
                string jsonData = ES3.Load<string>("PlayerData", savePath);
                if (jsonData == null || jsonData == "")
                {
                    Tools.Log("Failed To Load Archive!" , this);
                    PlayerData = new PlayerData();
                    return;
                }

                PlayerData = JsonUtility.FromJson<PlayerData>(jsonData);

                if (PlayerData == null)
                {
                    PlayerData = new PlayerData();
                }

                Tools.Log("Archive Loaded!" , this);

            }
            else
            {
                Tools.Log("Create New Archive!" , this);
                PlayerData = new PlayerData();
            }

        }

    }

    public void SaveData()
    {
        string jsonData = JsonUtility.ToJson(PlayerData, prettyPrint: true);
        ES3.Save("PlayerData", jsonData, savePath);
        Tools.Log("Archive Saved!" , this);

    }
    
    public void DeleteUser()
    {
        PlayerData = new PlayerData();
        SaveData();
    }


}
