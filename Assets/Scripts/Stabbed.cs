using UnityEngine;
//using UnityEngine.Networking;

public class Stabbed : MonoBehaviour
{
    float ellapsedTime;
    //bool canAttack;
    Player player;
    GameObject NeuronRobot;
    void Awake()
    {
        NeuronRobot = gameObject.transform.root.gameObject;
        player = NeuronRobot.GetComponent<Player>();
        //Debug.Log(player.name);
    }

    void Start()
    {
    //    if (isLocalPlayer)
    //    {
    //        canAttack = true;
    //    }
    }

    void Update()
    {
    //    if (!canAttack)
    //    {
    //        return;
    //    }
        ellapsedTime += Time.deltaTime;

    }

    public void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("OnCollisionEnter");
        if (collision.gameObject.tag == "weapon" && this.gameObject.tag == "Player" /* gameObject.tag == "flesh" */ )
        {
            Debug.Log("if weapon and player");
            //Debug.Log(collision.impulse);  // This is probably a better measure
            Debug.Log(collision.rigidbody.velocity.magnitude);
            Debug.Log(collision.rigidbody.angularVelocity.magnitude);
            //Debug.Log(Time.fixedDeltaTime);
            if (player.Alive == true)
            {
                player.Die();
            }
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
