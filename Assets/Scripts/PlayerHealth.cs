using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour
{
    [SerializeField] int maxHealth = 1;

    Player player;
    int health;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    [ServerCallback]
    void OnEnable()
    {
        health = maxHealth;
    }

    [Server]
    public bool TakeDamage()
    {
        Debug.Log("Server: TakeDamage");
        Debug.Log(health);
        bool died = false;

        if (health <= 0)
            return died;

        health--;
        died = health <= 0;

        RpcTakeDamage(died);
        Debug.Log(health);

        return died;
    }

    [ClientRpc]
    void RpcTakeDamage(bool died)
    {
        Debug.Log("Server: TakeDamage");
        if (died)
        {
            player.Die();
        }
    }
}