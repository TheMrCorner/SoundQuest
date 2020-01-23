using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<GameObject> openBoxes;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<FMODUnity.StudioBankLoader>().Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addOpenBox(GameObject box)
    {
        openBoxes.Add(box);
    }

    public void removeOpenBox(GameObject box)
    {
        if (openBoxes.Contains(box)){
            openBoxes.Remove(box);
        }
    }

}
