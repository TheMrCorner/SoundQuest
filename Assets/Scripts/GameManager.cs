using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Stack<GameObject> openBoxes;
    bool correctMelody;
    bool doorsOnTop;
    float initialPositionY;

    Vector3 yChangeE;
    Vector3 yChangeN;
    Vector3 yChangeW;
    Vector3 yChangeS;

    [Header("Doors")]
    public GameObject doorEast;
    public GameObject doorNorth;
    public GameObject doorWest;
    public GameObject doorSouth;


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<FMODUnity.StudioBankLoader>().Load();
        correctMelody = false;
        doorsOnTop = false;
        initialPositionY = doorEast.transform.position.y;
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
            yChangeE = new Vector3(doorEast.transform.position.x, doorEast.transform.position.y + 0.1f, doorEast.transform.position.z);
            yChangeN = new Vector3(doorNorth.transform.position.x, doorNorth.transform.position.y + 0.1f, doorNorth.transform.position.z);
            yChangeW = new Vector3(doorWest.transform.position.x, doorWest.transform.position.y + 0.1f, doorWest.transform.position.z);
            yChangeS = new Vector3(doorSouth.transform.position.x, doorSouth.transform.position.y + 0.1f, doorSouth.transform.position.z);

            doorEast.transform.position = yChangeE;
            doorNorth.transform.position = yChangeN;
            doorWest.transform.position = yChangeW;
            doorSouth.transform.position = yChangeS;

            if(yChangeE.y >= initialPositionY + 10)
            {
                doorsOnTop = true;
            }
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


    public bool compareMelody()
    {
        bool equals = false;

        //Revisa si los instrumentos de las cajas son los mismos que los almacenados y en ese orden

        return equals;
    }

}
