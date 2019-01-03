using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

[System.Serializable]
public class ToggleEvent : UnityEvent<bool>{}

public class Player : NetworkBehaviour
{
    [SerializeField] ToggleEvent onToggleShared;
    [SerializeField] ToggleEvent onToggleLocal;
    [SerializeField] ToggleEvent onToggleRemote;
    [SerializeField] float respawnTime = 3f;
    public bool Alive = false;
    [SyncVar]
    public bool ThisIsTheServerPlayer = false;

    GameObject mainCamera;

    void Start()
    {
        if (isLocalPlayer && isServer)
        {
            ThisIsTheServerPlayer = true;
        }
        Debug.Log(ThisIsTheServerPlayer);
        if (ThisIsTheServerPlayer)
        {
            transform.position = new Vector3(0, 0, 0);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.position = new Vector3(0, 0, 2);
            transform.eulerAngles = new Vector3(0, 180, 0);

        }
        //mainCamera = Camera.main.gameObject;
        //mainCamera = Camera.main.gameObject;

        EnablePlayer();
        //mainCamera = Camera.main.gameObject;

    }

    void DisablePlayer()
    {
        if (isLocalPlayer)
        {
            GameObject cameraHolder = gameObject.transform.GetChild(1).gameObject;
            GameObject neuronCamera = cameraHolder.transform.GetChild(0).gameObject;
            neuronCamera.SetActive(false);
            //Transform neuronCameraHolder = transform.Find("Camera Holder");
            //Transform neuronCamera = neuronCameraHolder.Find("Camera");
            //neuronCamera.
            //mainCamera.SetActive(true);
        }
        gameObject.GetComponent<NeuronAnimatorInstance>().enabled = false;

        //gameObject.GetComponent<NeuronAnimatorInstance>().enabled = false;
        onToggleShared.Invoke(false);
        if (isLocalPlayer)
        {
            //if (gameObject.GetComponent<NeuronAnimatorInstance>().enabled == true)
            //{
         //       gameObject.GetComponent<NeuronAnimatorInstance>().enabled = false;
            //}
            //onToggleLocal.Invoke(false);
        }
        else
        {
            onToggleRemote.Invoke(false);
        }
    }

    void EnablePlayer()
    {
        if (isLocalPlayer)
        {
            GameObject cameraHolder = gameObject.transform.GetChild(1).gameObject;
            GameObject neuronCamera = cameraHolder.transform.GetChild(0).gameObject;
            neuronCamera.SetActive(true);
            //mainCamera.SetActive(false);
        }
        Alive = true;

        gameObject.GetComponent<NeuronAnimatorInstance>().enabled = true;
        gameObject.GetComponent<NeuronAnimatorInstance>().connectToAxis = true;
        //onToggleShared.Invoke(true);
        if (isLocalPlayer)
        {
            //GlobalVar.axis_port++;
            //if(gameObject.GetComponent<NeuronAnimatorInstance>().enabled == false)
            //{
            //    gameObject.GetComponent<NeuronAnimatorInstance>().enabled = true;
            //}
            //onToggleLocal.Invoke(true);
            //NeuronAnimatorInstance.
        }
        else
        {
            onToggleRemote.Invoke(true);
        }
        gameObject.GetComponent<NeuronAnimatorInstance>().connectToAxis = false;

        Invoke("HorribleFix", 0.5f);
    }

    void HorribleFix()
    {
        gameObject.GetComponent<NeuronAnimatorInstance>().connectToAxis = true;
    }

    void ReEnablePlayer()
    {
        if (isLocalPlayer)
        {
            GameObject cameraHolder = gameObject.transform.GetChild(1).gameObject;
            GameObject neuronCamera = cameraHolder.transform.GetChild(0).gameObject;
            neuronCamera.SetActive(true);
            //mainCamera.SetActive(false);
        }
        Alive = true;

        gameObject.GetComponent<NeuronAnimatorInstance>().enabled = true;
        onToggleShared.Invoke(true);
        /*if (isLocalPlayer)
        {
            //GlobalVar.axis_port++;
            //if(gameObject.GetComponent<NeuronAnimatorInstance>().enabled == false)
            //{
            //    gameObject.GetComponent<NeuronAnimatorInstance>().enabled = true;
            //}
            //onToggleLocal.Invoke(true);
            //NeuronAnimatorInstance.
        }
        else
        {
            onToggleRemote.Invoke(true);
        }*/
    }

    public void Die()
    {
        Alive = false;
        Debug.Log("Die()");
        DisablePlayer();
        Invoke("Respawn", respawnTime);
    }

    void Respawn()
    {
        //if (isLocalPlayer)
        //{
        //    Transform spawn = NetworkManager.singleton.GetStartPosition();
        //    transform.position = spawn.position;
        //    transform.rotation = spawn.rotation;
        //}
        if (ThisIsTheServerPlayer)
        {
            transform.position = new Vector3(0, 0, 0);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.position = new Vector3(0, 0, 2);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        ReEnablePlayer();
        Debug.Log("Respawn()");
    }
}
