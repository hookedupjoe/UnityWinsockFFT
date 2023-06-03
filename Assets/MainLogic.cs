using UnityEngine;
using System;
using System.Threading.Tasks;
using HookedupLED;

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
    // Start is called before the first frame update
    void Start()
    {
        ledAPI = GameObject.FindGameObjectWithTag("Logic").GetComponent<HookedupLEDAPI>();
        ledAPI.OnConnected += OnConnected;
        ledAPI.OnDisconnected += OnDisconnected;
        //--- Connect to WinSock server
        //*** Setup to prompt if blank and have settings option to set this
        StartConnection();
        //*** Create the UI
        CreateUI();
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

