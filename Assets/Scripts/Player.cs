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

    GameObject mainCamera;

    void Start()
    {
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
        onToggleShared.Invoke(false);
        if (isLocalPlayer)
        {
            //if (gameObject.GetComponent<NeuronAnimatorInstance>().enabled == true)
            //{
                gameObject.GetComponent<NeuronAnimatorInstance>().enabled = false;
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
        gameObject.GetComponent<NeuronAnimatorInstance>().enabled = true;
        onToggleShared.Invoke(true);
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
    }

    public void Die()
    {
        Debug.Log("Die()");
        DisablePlayer();
        Invoke("Respawn", respawnTime);
    }

    void Respawn()
    {
        if (isLocalPlayer)
        {
            Transform spawn = NetworkManager.singleton.GetStartPosition();
            transform.position = spawn.position;
            transform.rotation = spawn.rotation;
        }
        EnablePlayer();
        Debug.Log("Respawn()");
    }
}
