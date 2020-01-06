using Common;
using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class Asteroid : NetworkedBehaviour {
    private Vector2 direction;
    private Rigidbody2D rb;

    private const float impulseForceMultiplier = 5f;

    public override void NetworkStart() {
        base.NetworkStart();
        if (IsServer) {
            rb = GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(Random.Range(-1, 1f), Random.Range(-1f, 1f)).normalized * impulseForceMultiplier, ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(-1, 1f) * 3f, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (IsServer) {
            if (other.gameObject.layer == LayerMask.NameToLayer(LayerHelper.WORLD_LIMITS)) {
                rb.velocity = Vector2.Reflect(rb.velocity, transform.right);
                rb.AddRelativeForce(Vector2.up * 1.2f);
            } else if (other.gameObject.layer == LayerMask.NameToLayer(LayerHelper.PLAYERS)) {
                InvokeClientRpcOnEveryone(CmdAsteroidHitPlayer, other.gameObject);
            }
        }
    }

    [ClientRPC]
    public void CmdAsteroidHitPlayer(GameObject player) {
        player.GetComponent<Player.Player>().ResetPlayer();
    }
}
