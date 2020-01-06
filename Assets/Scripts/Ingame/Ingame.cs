using System.Linq;
using MLAPI;
using ScriptableObjectArchitecture;
using UnityEngine;

namespace Ingame {
    public class Ingame : MonoBehaviour {
        public GameObjectGameEvent changeCameraTarget;

        private void Start() {
            SetupCamera();
        }

        private void SetupCamera() {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            var player = players.First(p => p.GetComponent<NetworkedBehaviour>().IsLocalPlayer);
            changeCameraTarget.Raise(player);
        }
    }
}
