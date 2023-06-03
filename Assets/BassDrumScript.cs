using HookedupLED;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BassDrumScript : MonoBehaviour
{
    public HookedupLEDAPI logic;
    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<HookedupLEDAPI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (logic.musicData != null)
        {
            //Debug.Log("Vol:" + logic.musicData.vol.ToString());
            //Debug.Log(logic.musicData.vol);
            float tmpAt = 0;
            float tmpVal = 0;
            int tmpType = 0;
            if (gameObject.name == "KickDrum")
            {
                tmpVal = logic.musicData.kVal;
            }
            else if (gameObject.name == "SnareDrum")
            {
                tmpVal = logic.musicData.sVal;
            }
            else if (gameObject.name == "BassDrum")
            {
                tmpVal = logic.musicData.bVal;
            } 
            else if (gameObject.name == "KickDrumRotate")
            {
                tmpVal = logic.musicData.kCycle;
                tmpType = 1;
            }
            else if (gameObject.name == "SnareDrumRotate")
            {
                tmpVal = logic.musicData.sCycle;
                tmpType = 1;
            }
            else if (gameObject.name == "BassDrumRotate")
            {
                tmpVal = logic.musicData.bCycle;
                tmpType = 1;
            }


            if (tmpType == 0)
            {
                //--- Beat
                if (tmpVal > 0)
                {
                    tmpAt = tmpVal / 800f;
                }
                transform.localScale = new Vector3(1, tmpAt, 1);
            }
            else
            {
                //Debug.Log("tmpVal" + tmpVal.ToString());
                //--- Rotate
                
                if (tmpVal > 0)
                {
                    
                    tmpAt = (255 - tmpVal) / 255f;
                }
                

                SpriteRenderer sprite = GetComponent<SpriteRenderer>();
                //sprite.color = new Color(1, 0, 0, tmpAt);
                sprite.color = Color.HSVToRGB(tmpAt, 1, 1);

                Vector3 newPosition = new Vector3(0, 0, (tmpAt * 360));
                transform.eulerAngles = newPosition;

            }
            

        }
    }
}
