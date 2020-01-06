using Common;
using MLAPI;
using UnityEngine;

public class Bullet : NetworkedBehaviour {
    public readonly float speed = 250f;

    private void OnCollisionEnter2D(Collision2D other) {
        if (IsServer) {
            if (other.gameObject.layer == LayerMask.NameToLayer(LayerHelper.ASTEROIDS)) {
                Debug.Log("Bullet hit ASTEROIDS");
            } else if (other.gameObject.layer == LayerMask.NameToLayer(LayerHelper.PLAYERS)) {
                Debug.Log("Bullet hit PLAYERS");
                other.gameObject.GetComponent<Player.Player>().ResetPlayer();
            } else {
                Debug.Log("Bullet hit something else");
            }
            Destroy(gameObject);
        }
    }
}
