using MLAPI;
using MLAPI.SceneManagement;
using MLAPI.Spawning;
using UnityEngine;

public class HostGame : MonoBehaviour {
    public void JoinGame() {
        NetworkingManager.Singleton.OnClientConnectedCallback += ClientConnected;
        NetworkingManager.Singleton.OnClientDisconnectCallback += ClientDisconnected;
        NetworkingManager.Singleton.StartClient();
    }

    public void CreateServer() {
        NetworkingManager.Singleton.OnClientConnectedCallback += clientId => { Debug.Log($"Client connected {clientId}"); };
        NetworkingManager.Singleton.OnClientDisconnectCallback += clientId => { Debug.Log($"Client disconnected {clientId}"); };
        NetworkingManager.Singleton.OnServerStarted += () => {
            Debug.Log("Server started");
            CreateServerEnvironment();
        };

        NetworkingManager.Singleton.StartHost();
    }

    private void CreateServerEnvironment() {
        var progress = NetworkSceneManager.SwitchScene("Ingame");
        progress.OnClientLoadedScene += id => {
            /*
            var current = 0;
            var bulletsPool = new NetworkedObject[10];

            var bulletPrefab = Resources.Load("Prefabs/Bullet");
            for (var i = bulletsPool.Length - 1; i >= 0; i--) {
                bulletsPool[i] = (Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity) as GameObject)?.GetComponent<NetworkedObject>();
            }

            SpawnManager.RegisterSpawnHandler(SpawnManager.GetPrefabHashFromGenerator(bulletPrefab.name), (position, rotation) => {
                current = (current + 1) % bulletsPool.Length;
                Debug.Log("Taking bullet number " + current);
                return bulletsPool[current];
            });*/

            var asteroidPrefab = Resources.Load("Prefabs/Asteroid");
            for (var i = 0; i < 30; i++) {
                var randPosition = new Vector2(Random.Range(-30f, 30f), Random.Range(-20f, 20f));
                GameObject asteroid = Instantiate(asteroidPrefab, randPosition, Quaternion.identity) as GameObject;
                asteroid.GetComponent<NetworkedObject>().Spawn();
            }
        };
    }

    private void ClientConnected(ulong clientId) {
        Debug.Log($"I'm connected {clientId}");
    }

    private void ClientDisconnected(ulong clientId) {
        Debug.Log($"I'm disconnected {clientId}");
        NetworkingManager.Singleton.OnClientDisconnectCallback -= ClientDisconnected;
        NetworkingManager.Singleton.OnClientConnectedCallback -= ClientConnected;
    }
}
