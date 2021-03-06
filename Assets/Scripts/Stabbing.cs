﻿using UnityEngine;
using UnityEngine.Networking;

public class Stabbing : NetworkBehaviour
{
    float ellapsedTime;
    bool canAttack;
    Player player;

    void Awake()
    {
        player = GetComponent<Player>();
        Debug.Log(player.name);
    }

    void Start()
    {
        if (isLocalPlayer)
        {
            canAttack = true;
        }
    }

    void Update()
    {
        if (!canAttack)
        {
            return;
        }
        ellapsedTime += Time.deltaTime;

    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
        if (collision.gameObject.tag == "weapon" && this.gameObject.tag == "Player" /* gameObject.tag == "flesh" */ )
        {
            Debug.Log("if weapon and player");
            player.Die();
            //CmdStabbed(this.gameObject.transform.parent.gameObject);
        }
    }
    /*
    [Command]
    void CmdStabbed(GameObject enemy)
    {
        Debug.Log("CmdStabbed()'a girdi");
        // GameObject enemy = weaponTransform.parent.gameObject;
        PlayerHealth enemyHealth = enemy.GetComponent<PlayerHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage();
        }
    }

    [ClientRpc]
    void RpcProcessStabEffects()
    {
        
    }*/
}
