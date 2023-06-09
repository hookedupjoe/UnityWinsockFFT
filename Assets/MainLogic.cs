using UnityEngine;
using System;
using System.Threading.Tasks;
using HookedupLED;
using System.Collections.Generic;


public class MainLogic : MonoBehaviour
{
    public GameObject bandBar;

    

    public String serverURL = "ws://localhost/eq";
    public GameObject noConnectDialog;
    public HookedupLEDAPI ledAPI;

    private void OnConnected()
    {
        Debug.Log("OnConnected");
        NoConnectionDialog(false);
    }
    private void OnDisconnected()
    {
        Debug.Log("OnDisconnected");
        NoConnectionDialog(true);
        
    }

    bool DoneAlready(IDictionary<int, bool> theDictionary, int theItem)
    {
        bool tmpExists = false;
        theDictionary.TryGetValue(theItem, out tmpExists);
        return tmpExists;
    }


    void Start()
    {

        IDictionary<int, bool> doneList = new Dictionary<int, bool>();
        doneList.Add(3, true);

        Debug.Log("3");
        Debug.Log(DoneAlready(doneList, 3));
        Debug.Log("2");
        Debug.Log(DoneAlready(doneList, 2));


        //--- Get instance of running LEDAPI object
        ledAPI = GameObject.FindGameObjectWithTag("Logic").GetComponent<HookedupLEDAPI>();
        //--- Attach to connection handlers
        ledAPI.OnConnected += OnConnected;
        ledAPI.OnDisconnected += OnDisconnected;

        CreateUI();
        StartConnection();
    }

    public void CloseGame()
    {
        Application.Quit();
    }


    public void NextSong()
    {
        ledAPI.RunCommand("musicAction", "nextsong");
    }

    public void TryToConnect()
    {
        //--- For now, just start connection
        StartConnection();
    }


    private void StartConnection()
    {
        ledAPI.StartConnection(serverURL);
    }

    private void CreateUI()
    {
        float tmpBuffer = .25f;
        float tmpColorOffset = 1f / 30f;
        float tmpBase = 12f;
            
        for (int i = 1; i <= 30; i++)
        {
            float tmpSpot = (i * tmpBuffer);
            float tmpHue = ((float)i * tmpColorOffset);
            GameObject tmpNew = Instantiate(bandBar, new Vector3(tmpBase + tmpSpot, 2, 0), transform.rotation);
            tmpNew.name = "Bar" + i.ToString();
            SpriteRenderer sprite = tmpNew.GetComponent<SpriteRenderer>();
            sprite.color = Color.HSVToRGB(tmpHue, 1f, 1f);
        }

    }

    private void NoConnectionDialog(bool theShowFlag) {
        ledAPI.isNoConnectOpen = theShowFlag;
        noConnectDialog.SetActive(theShowFlag);
    }
    

}

