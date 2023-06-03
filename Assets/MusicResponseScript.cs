using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HookedupLED;

public class MusicResponseScript : MonoBehaviour
{
    public HookedupLEDAPI ledAPI;
    // Start is called before the first frame update
    void Start()
    {
        ledAPI = GameObject.FindGameObjectWithTag("Logic").GetComponent<HookedupLEDAPI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ledAPI.musicData != null)
        {
            float tmpAt = 0;
            float tmpVal = 0;

            

            //--- ToDo Make look for the 30 when it works :)
            if (gameObject.name == "Volume")
            {
                tmpVal = ledAPI.musicData.vol;
            }
            else 
            {
                for (int i = 1; i <= 30; i++)
                {
                    var tmpName = "Bar" + i;
                    if (gameObject.name == tmpName)
                    {
                        tmpVal = ledAPI.getBandValue(i);
                    }
                }
            }

            if (tmpVal > 0)
            {
                tmpAt = tmpVal / 5f;
            }
            transform.localScale = new Vector3(1, tmpAt, 1);

        }
    }
}
