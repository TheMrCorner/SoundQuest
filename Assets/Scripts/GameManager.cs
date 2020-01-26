using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Stack<GameObject> openBoxes;
    bool correctMelody;
    bool doorsOnTop;
    float initialPositionY;
    int finalDoor;

    //Instrumentos puestos al azar de momento para probar
    string[] instruments = { "event:/violin", "event:/viola", "event:/acordeon", "event:/contrabajo", "event:/piano" };

    //Doors y position
    Vector3 yChangeE;
    Vector3 yChangeN;
    Vector3 yChangeW;
    Vector3 yChangeS;

    [Header("Doors")]
    public GameObject doorEast;
    public GameObject doorNorth;
    public GameObject doorWest;
    public GameObject doorSouth;

    [Header("ExitTriggers")]
    public Collider eastTrigger;
    public Collider northTrigger;
    public Collider westTrigger;
    public Collider southTrigger;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<FMODUnity.StudioBankLoader>().Load();
        correctMelody = false;
        doorsOnTop = false;
        initialPositionY = doorEast.transform.position.y;
        finalDoor = Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        //Para probar
        if (Input.GetKeyDown(KeyCode.P))
        {
            correctMelody = true; 
        }

        if (correctMelody && !doorsOnTop)
        {
            doorsToTop();
        }
    }

    public void addOpenBox(GameObject box)
    {
        openBoxes.Push(box);

        if(openBoxes.Count >= 5)
        {
            compareMelody();
        }
    }

    //Revisa si los instrumentos de las cajas son los mismos que los almacenados y en ese orden
    public bool compareMelody()
    {
        bool equals = true;

        int n = openBoxes.Count;
        for (int i = 0; i < n; i++)
        {
            if(instruments[i] != openBoxes.Peek().transform.GetChild(0).GetChild(0).GetComponent<OcclusionScript>().getAudio())
            {
                equals = false;
            }

            openBoxes.Peek().GetComponent<Animation>().CrossFade("Crate_Close");
            openBoxes.Pop();
        }

        if (equals)
        {
            //Genera un emiter en la puerta seleccionada
            GameObject emiter = new GameObject("MelodyEmiter");

            if (finalDoor == 0)
            {
                Instantiate(emiter, eastTrigger.transform);
            }
            else if (finalDoor == 1)
            {
                Instantiate(emiter, northTrigger.transform);
            }
            else if (finalDoor == 2)
            {
                Instantiate(emiter, westTrigger.transform);
            }
            if (finalDoor == 3)
            {
                Instantiate(emiter, southTrigger.transform);
            }

            emiter.AddComponent<OcclusionScript>();
        }

        return equals;
    }

    public void doorsToTop ()
    {
        yChangeE = new Vector3(doorEast.transform.position.x, doorEast.transform.position.y + 0.1f, doorEast.transform.position.z);
        yChangeN = new Vector3(doorNorth.transform.position.x, doorNorth.transform.position.y + 0.1f, doorNorth.transform.position.z);
        yChangeW = new Vector3(doorWest.transform.position.x, doorWest.transform.position.y + 0.1f, doorWest.transform.position.z);
        yChangeS = new Vector3(doorSouth.transform.position.x, doorSouth.transform.position.y + 0.1f, doorSouth.transform.position.z);

        doorEast.transform.position = yChangeE;
        doorNorth.transform.position = yChangeN;
        doorWest.transform.position = yChangeW;
        doorSouth.transform.position = yChangeS;

        if (yChangeE.y >= initialPositionY + 10)
        {
            doorsOnTop = true;
        }
    }

}
