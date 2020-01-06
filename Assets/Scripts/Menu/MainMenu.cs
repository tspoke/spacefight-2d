using MLAPI;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartHostMode()
    {
        NetworkingManager.Singleton.StartHost();
    }

    public void StartClientMode()
    {
        NetworkingManager.Singleton.StartClient();
    }

    public void StartServerMode()
    {
        NetworkingManager.Singleton.StartServer();
    }
}
