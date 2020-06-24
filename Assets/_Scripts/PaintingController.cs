using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PaintingController : MonoBehaviour
{
    public GameObject roomContainer;
    public GameObject orderedContainer;
    public GameObject[] paintings;
    public GameObject[] audioFiles;
    public Collider room2Page;
    public bool toggle;

    private Collider[] roomPieces;
    
    private Rigidbody[] rb;
    //private Transform[] trans;
    private bool runOnce;
    private bool p1;
    private bool p2;
    private bool p3;

    // Use this for initialization
    void Start()
    {
        ResetBools();
        rb = roomContainer.GetComponentsInChildren<Rigidbody>();
        roomPieces = roomContainer.GetComponentsInChildren<Collider>();
        //trans = GetComponentsInChildren<Transform>();
        room2Page.GetComponent<Collider>().enabled = false;     // make sure the page cannot be grabbed unless the puzzle is solved
        foreach (Collider col in roomPieces)
        {
            col.GetComponent<Collider>().enabled = false;
        }
        foreach (Rigidbody rigid in rb)
        {
            rigid.useGravity = true;
            rigid.isKinematic = true;
        }
    }

    IEnumerator AudioTiming(int i)
    {
        audioFiles[i].SetActive(true);
        yield return new WaitForSeconds(5);
        audioFiles[i].SetActive(false);
    }

    void ResetBools()
    {
        runOnce = true;
        p1 = true;
        p2 = true;
        p3 = true;
    }

    IEnumerator WrongOrder()
    {
        orderedContainer.transform.DetachChildren(); // clear the container
        StartCoroutine(paintings[0].GetComponent<PaintingEffects>().IncorrectSequence());
        StartCoroutine(paintings[1].GetComponent<PaintingEffects>().IncorrectSequence());
        StartCoroutine(paintings[2].GetComponent<PaintingEffects>().IncorrectSequence());
        StartCoroutine(AudioTiming(1));
        yield return new WaitForSeconds(2);     // wait a few seconds so the player doesn't accidentally hit a painting 
        ResetBools();
    }

    IEnumerator CorrectOrder()
    {
        foreach (Collider col in roomPieces) 
        {
            col.GetComponent<Collider>().enabled = true;    // make the box explode and everything in the room move
        }
        foreach (Rigidbody rigid in rb)
        {
            rigid.useGravity = false;
            rigid.isKinematic = false;
        }
        StartCoroutine(AudioTiming(0));
       
        yield return new WaitForSeconds(0.1f);

        room2Page.GetComponent<Collider>().enabled = true;  // turn on collider for page so it can be picked up

        //foreach (Collider col in roomPieces)  // disable colliders so they don't interact with player
        //{
        //    col.GetComponent<Collider>().enabled = false;
        //}
    }

    void CheckOrder()
    {
        // correct order is 2,1,3
        if (orderedContainer.transform.GetChild(0).tag == "Painting2")
        {
            if (orderedContainer.transform.GetChild(1).tag == "Painting1")
            {
                StartCoroutine(CorrectOrder());
            }
            else
            {
                StartCoroutine(WrongOrder());
            }
        }
        else
        {
            StartCoroutine(WrongOrder());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (paintings[0].GetComponent<PaintingEffects>().touched && paintings[0].transform.parent != orderedContainer.transform && p1)               // if any painting is touched, add the name of the painting to the paintingOrder list
        {
            audioFiles[2].SetActive(false);
            paintings[0].transform.parent = orderedContainer.transform;         // make painting a child of orderedContainer
            audioFiles[2].SetActive(true);
            paintings[0].GetComponent<PaintingEffects>().touched = false;
            //print("p1 touched = " + paintings[0].GetComponent<PaintingEffects>().touched);
            p1 = false;
        }
        if (paintings[1].GetComponent<PaintingEffects>().touched && paintings[1].transform.parent != orderedContainer.transform && p2)
        {
            audioFiles[2].SetActive(false);
            paintings[1].transform.parent = orderedContainer.transform;
            audioFiles[2].SetActive(true);
            paintings[1].GetComponent<PaintingEffects>().touched = false;
            p2 = false;
        }
        if (paintings[2].GetComponent<PaintingEffects>().touched && paintings[2].transform.parent != orderedContainer.transform && p3)
        {
            audioFiles[2].SetActive(false);
            paintings[2].transform.parent = orderedContainer.transform;
            audioFiles[2].SetActive(true);
            paintings[2].GetComponent<PaintingEffects>().touched = false;
            p3 = false;
        }

        if (orderedContainer.transform.childCount == 3 && runOnce)
        {
            CheckOrder();
            //print("container full");
            runOnce = false;
        }

    }
}
        


