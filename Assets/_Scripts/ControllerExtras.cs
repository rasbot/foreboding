using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerExtras : MonoBehaviour
{

    public GameObject flies;
    public GameObject[] blobbyStuff;
    public GameObject TV;
    public GameObject firstBlood;
    public GameObject mainParticles;
    public GameObject upperParticles;
    public GameObject scaleCol;
    public GameObject chamberDoors;
    public GameObject endingMovingAudio;
    public GameObject endChanting;
    public GameObject drumsHorn;
    public GameObject flockingOrbs;
    public GameObject[] pagesCollected;
    public GameObject[] journalPagesCollected;
    public GameObject[] pillars;
    public GameObject[] pagesEnd;
    public SteamVR_Controller.Device controller;
    public Rigidbody rb;
    public GameObject staticObjects;        // these two contain the entire visible level
    public GameObject dynamicObjects;
    public GameObject plane;
    //public Animator anim;
    //public string fogBox;
    //public GameObject fogBoxGO;
    public GameObject Cthulhu;
    public GameObject nexus;
    public GameObject corpses;
    public GameObject endSign;
    public GameObject cam;

    public static bool painting1;
    public static bool painting2;
    public static bool painting3;

    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private GameObject objectInHand;
    private GameObject thisPage;
    private bool page2Grabbed;
    private int count;
    private int pageCount;
    private int pageCountPillar;
    private int journalCount;
    private float pillarOffset;
    private bool end1;
    private bool end2;
    private bool fogON;
    private bool fogOFF;
    private bool lastFog;
    private float fog_i;
    private bool toggle;
    private bool occCull;

    void Start()
    {
        //toggle = false;
        //scaleCol.SetActive(toggle);
        staticObjects.SetActive(true);
        dynamicObjects.SetActive(true);
        endSign.SetActive(false);
        fog_i = RenderSettings.fogDensity;      // get the initial density of the fog
        lastFog = false;
        plane.SetActive(false);
        nexus.SetActive(false);
        corpses.SetActive(false);
        pillarOffset = 1.836f;
        count = 0;
        pageCount = 0;
        pageCountPillar = 0;
        journalCount = 0;
        end1 = false;
        end2 = false;
        painting1 = false;
        painting2 = false;
        painting3 = false;
        fogON = false;
        fogOFF = false;
        TV.SetActive(false);        // we were always told as kids to make sure the TV was off if we weren't in the same room. So here you go mom and dad, I'm being responsible!
        flies.SetActive(false);
        blobbyStuff[0].SetActive(false);
        blobbyStuff[1].SetActive(false);
        firstBlood.SetActive(false);
        endingMovingAudio.SetActive(false);
        drumsHorn.SetActive(false);
        Cthulhu.SetActive(false);
        rb = GetComponent<Rigidbody>();
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        cam.GetComponent<Camera>().useOcclusionCulling = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);

        if (other.CompareTag("MainSection"))
        {
            mainParticles.SetActive(true);
        }

        if (other.CompareTag("UpperRooms"))
        {
            upperParticles.SetActive(true);
        }
        //if (other.CompareTag("ChamberHall"))
        //{
        //    particles.SetActive(false);
        //}

        if (other.CompareTag("SymCol"))     // turn on TV
        {
            TV.SetActive(true);
        }

        if (other.CompareTag("FliesCol"))
        {
            flies.SetActive(true);
            blobbyStuff[0].SetActive(true);
            blobbyStuff[1].SetActive(true);
        }

        if (other.CompareTag("Painting1"))
        {
            painting1 = true;
        }

        if (other.CompareTag("Painting2"))
        {
            painting2 = true;
        }

        if (other.CompareTag("Painting3"))
        {
            painting3 = true;
        }

        if (other.CompareTag("Pillar"))
        {
            bool onePage = true;
            if (other.name == "Pillar (1)" && pagesCollected[0].activeSelf && onePage)     // check if we are touching the first pillar and if the first page was collected
            {
                GameObject pg;
                print("first pillar touched");
                Vector3 pagePos = new Vector3(other.transform.position.x, other.transform.position.y + pillarOffset, other.transform.position.z);   // instantiate page a little above the pillar
                pg = Instantiate(pagesEnd[0], pagePos, Quaternion.identity);
                pg.transform.parent = other.transform;
                pageCountPillar++;
                onePage = false;

            }
            if (other.name == "Pillar (2)" && pagesCollected[1].activeSelf && onePage)
            {
                GameObject pg;
                print("second pillar touched");
                Vector3 pagePos = new Vector3(other.transform.position.x, other.transform.position.y + pillarOffset, other.transform.position.z);
                pg = Instantiate(pagesEnd[1], pagePos, Quaternion.identity);
                pg.transform.parent = other.transform;
                pageCountPillar++;
                onePage = false;
            }
            if (other.name == "Pillar (3)" && pagesCollected[2].activeSelf && onePage)
            {
                GameObject pg;
                print("third pillar touched");
                Vector3 pagePos = new Vector3(other.transform.position.x, other.transform.position.y + pillarOffset, other.transform.position.z);
                pg = Instantiate(pagesEnd[2], pagePos, Quaternion.identity);
                pg.transform.parent = other.transform;
                pageCountPillar++;
                onePage = false;
            }
            if (other.name == "Pillar (4)" && pagesCollected[3].activeSelf && onePage)
            {
                GameObject pg;
                print("forth pillar touched");
                Vector3 pagePos = new Vector3(other.transform.position.x, other.transform.position.y + pillarOffset, other.transform.position.z);
                pg = Instantiate(pagesEnd[3], pagePos, Quaternion.identity);
                pg.transform.parent = other.transform;
                pageCountPillar++;
                onePage = false;
            }
        }

        if (other.CompareTag("ChamberCol"))
        {
            CheckPages();   // see how many pages have been found

            if (pageCountPillar == 4)   // check if all 4 pages have been placed on the pillars
            {
                chamberDoors.SetActive(true);
                other.GetComponent<Collider>().enabled = false;     // turn off the chamber collider

                if (journalCount == 8)
                {
                    StartCoroutine(Ending2());  // Yog/Az
                }
                else
                {
                    StartCoroutine(Ending1());  // Cth
                }
            }

        }
        if (other.CompareTag("Page"))
        {
            thisPage = other.gameObject;                    // get a reference of the page just touched
            if (other.name == "NecroPage1")                 // check which page was collected, and set the respective gameObject active to reference it
            {
                pagesCollected[0].SetActive(true);
            }
            else if (other.name == "NecroPage3")
            {
                pagesCollected[2].SetActive(true);
            }
            else if (other.name == "NecroPage4")
            {
                pagesCollected[3].SetActive(true);
            }

            if (other.name != "NecroPage2")                 // page 2 will start things when other conditions are met so exclude it for now
            {
                StartCoroutine(PagePickedUp());             // start timing for animation and audio
            }
        }

        if (other.CompareTag("Journal"))                    // keep track of how many journal pages are collected
        {
            if (other.name == "JournalPage1")
            {
                journalPagesCollected[0].SetActive(true);
                firstBlood.SetActive(true);
            }
            else if (other.name == "JournalPage2")
            {
                journalPagesCollected[1].SetActive(true);
            }
            else if (other.name == "JournalPage3")
            {
                journalPagesCollected[2].SetActive(true);
            }
            else if (other.name == "JournalPage4")
            {
                journalPagesCollected[3].SetActive(true);
            }
            else if (other.name == "JournalPage5")
            {
                journalPagesCollected[4].SetActive(true);
            }
            else if (other.name == "JournalPage6")
            {
                journalPagesCollected[5].SetActive(true);
            }
            else if (other.name == "JournalPage7")
            {
                journalPagesCollected[6].SetActive(true);
            }
            else if (other.name == "JournalPage8")
            {
                journalPagesCollected[7].SetActive(true);
            }
        }
    }

    IEnumerator PagePickedUp()
    {
        thisPage.GetComponent<AudioSource>().enabled = true;
        yield return new WaitForSeconds(2);
        thisPage.GetComponent<Animator>().enabled = true;
        yield return new WaitForSeconds(4);
        thisPage.GetComponent<Animator>().enabled = false;
        thisPage.GetComponent<AudioSource>().enabled = false;
        thisPage.SetActive(false);
    }

    IEnumerator Ending1() //Cth
    {
        endChanting.SetActive(true);
        yield return new WaitForSeconds(8);
        //anim.SetBool("FogBoxOn", true);
        fogON = true;
        yield return new WaitForSeconds(8);
        TurnOffVisibleLevel();
        plane.SetActive(true);
        cam.GetComponent<Camera>().useOcclusionCulling = false;
        yield return new WaitForSeconds(2.5f);
        //anim.SetBool("FogBoxOn", false);
        fogOFF = true;
        yield return new WaitForSeconds(5);
        Cthulhu.SetActive(true);
        yield return new WaitForSeconds(40);
        lastFog = true;                         // this will change the value that the fog density increases to
        fogON = true;
        yield return new WaitForSeconds(9);
        lastFog = false;
        fogON = true;
        endSign.SetActive(true);
        //END END
    }

    IEnumerator Ending2()   //Az
    {
        yield return new WaitForSeconds(0.4f);
        drumsHorn.SetActive(true);
        yield return new WaitForSeconds(7.1f);
        flockingOrbs.SetActive(true);
        endingMovingAudio.SetActive(true);
        yield return new WaitForSeconds(8);
        fogON = true;
        yield return new WaitForSeconds(8);
        TurnOffVisibleLevel();
        plane.SetActive(true);
        cam.GetComponent<Camera>().useOcclusionCulling = false;
        fogOFF = true;
        corpses.SetActive(true);
        nexus.SetActive(true);
        yield return new WaitForSeconds(8);
        //lastFog = true;
        fogON = true;
        yield return new WaitForSeconds(9);
        endSign.SetActive(true);
        flockingOrbs.SetActive(false);
        nexus.SetActive(false);
        //END END
    }

    void IncreaseFog(float i)
    {
        if (RenderSettings.fogDensity < i)  // increase fog density until it is at max
        {
            RenderSettings.fogDensity += 0.15f * Time.deltaTime;
        }
        else fogON = false; // stop calling this method in update once we are at full fog
    }

    void DecreaseFog(float i)
    {
        if (RenderSettings.fogDensity > i)
        {
            RenderSettings.fogDensity -= 0.15f * Time.deltaTime;
        }
        else fogOFF = false;
    }

    void TurnOffVisibleLevel()
    {
        staticObjects.SetActive(false);
        dynamicObjects.SetActive(false);
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);

        if (other.CompareTag("Journal") || other.CompareTag("Page"))
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

        if (other.CompareTag("Blob1") || other.CompareTag("Blob2"))     // use haptic feedback when "digging" through the blobby thing
        {
            SteamVR_Controller.Input((int)trackedObj.index).TriggerHapticPulse(500);
        }
    }

    public void OnTriggerExit(Collider other)
    {

        collidingObject = null;

        //if (other.CompareTag("Scale2Col"))          // turns on / off the collider that scales the player
        //{
        //    toggle = !toggle;
        //    scaleCol.SetActive(toggle);
        //}

        if (other.CompareTag("FliesCol"))
        {
            flies.SetActive(false);
            blobbyStuff[0].SetActive(false);
            blobbyStuff[1].SetActive(false);
        }

        if (other.CompareTag("SymCol"))
        {
            TV.SetActive(false);
        }

        if (other.CompareTag("MainSection"))
        {
            mainParticles.SetActive(false);
        }

        if (other.CompareTag("UpperRooms"))
        {
            upperParticles.SetActive(false);
        }

        if (page2Grabbed)
        {
            if (other.CompareTag("Blob1") || other.CompareTag("Blob2"))
            {
                StartCoroutine(PagePickedUp());     // if page 2 is being grabbed and the controllers are leaving the blob colliders, start animation / audio
                pagesCollected[1].SetActive(true);  // mark page as collected
            }
        }
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
        objectInHand = collidingObject;
        if (objectInHand.name == "NecroPage2")  // check if we are grabbing the second page, which is in the blobby thing
        {
            page2Grabbed = true;
        }
        else page2Grabbed = false;
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

    void CheckPages()
    {
        foreach (GameObject gam in pagesCollected)
        {
            if (gam.activeSelf)
            {
                count++;
            }
        }
        pageCount = count;

        count = 0;
        foreach (GameObject gam in journalPagesCollected)
        {
            if (gam.activeSelf)
            {
                count++;
            }
        }
        journalCount = count;
        count = 0;
    }

    void Update()
    {
        controller = SteamVR_Controller.Input((int)trackedObj.index);

        if (fogON)
        {
            if (lastFog)
            {
                IncreaseFog(0.2f);      // don't increase the fog too much at the end
            }
            else
            {
                IncreaseFog(1f);        // increase fog to max density
            }
        }

        if (fogOFF)
        {
            DecreaseFog(fog_i);         // decrease the fog back to the original density 
        }
    }
}
