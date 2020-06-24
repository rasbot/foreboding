using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerIntro : MonoBehaviour
{

    public SteamVR_Controller.Device controller;
    public Rigidbody rb;
    public GameObject symbolPrefab;
    public GameObject title;
    public GameObject levelSwitcher;
    public GameObject[] audio;
    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private bool a;
    private bool b;

    void Start()
    {
        a = true;
        b = true;
        symbolPrefab.SetActive(false);
        title.SetActive(false);
        rb = GetComponent<Rigidbody>();
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        foreach (GameObject go in audio)
        {
            go.SetActive(false);
        }
        StartCoroutine(Delay(3, title, 0, a));
    }

    IEnumerator Delay(float waitTime, GameObject gameObj, int audioInt, bool onlyOnce)
    {
        if (onlyOnce)
        {
            yield return new WaitForSeconds(waitTime);
            gameObj.SetActive(true);
            audio[audioInt].SetActive(true);
            onlyOnce = false;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);

        if (other.CompareTag("Journal"))                   
        {
            StartCoroutine(Delay(2, symbolPrefab, 1, b));
        }

        if (other.CompareTag("CthulhuSign"))
        {
            levelSwitcher.GetComponent<LevelSwitch>().LevelSwitcher();
        }
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);

        if (other.CompareTag("Journal"))
        {
            if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                ThrowObject(other);
            }
            else if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                GrabObject(other);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        collidingObject = null;
    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }

        collidingObject = col.gameObject;
    }

    private void GrabObject(Collider otherCol)
    {
        otherCol.transform.SetParent(gameObject.transform);
        otherCol.GetComponent<Rigidbody>().isKinematic = true;
        collidingObject = null;
    }

    private void ThrowObject(Collider otherCol)
    {
        otherCol.transform.SetParent(null);
        Rigidbody rigidbody = otherCol.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.velocity = controller.velocity;
        rigidbody.angularVelocity = controller.angularVelocity;
    }

    void Update()
    {
        controller = SteamVR_Controller.Input((int)trackedObj.index);
    }
}
