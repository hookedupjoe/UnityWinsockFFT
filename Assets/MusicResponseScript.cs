using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicResponseScript : MonoBehaviour
{
    public MainLogic logic;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<MainLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (logic.musicData != null)
        {
            float tmpAt = 0;
            float tmpVal = 0;

            

            //--- ToDo Make look for the 30 when it works :)
            if (gameObject.name == "Volume")
            {
                tmpVal = logic.musicData.vol;
            }
            else 
            {
                for (int i = 1; i <= 30; i++)
                {
                    var tmpName = "Bar" + i;
                    if (gameObject.name == tmpName)
                    {
                        tmpVal = logic.getBandValue(i);
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
