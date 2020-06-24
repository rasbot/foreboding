using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingEffects : MonoBehaviour
{
    public GameObject[] meshEffects;
    public bool touched;
    public Animator anim;

    public IEnumerator IncorrectSequence()
    {
        meshEffects[1].GetComponent<PSMeshRendererUpdater>().IsActive = true;
        yield return new WaitForSeconds(2);
        meshEffects[1].GetComponent<PSMeshRendererUpdater>().IsActive = false;
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("GameController"))
        {
            meshEffects[0].SetActive(true);
            touched = true;
            anim.enabled = true;
        }
    }

    void Start()
    {
        //meshEffects[1].GetComponent<PSMeshRendererUpdater>().IsActive = false;
        touched = false;
        anim = GetComponent<Animator>();
        anim.enabled = false;
        //StartCoroutine(IncorrectSequence());
    }

}
