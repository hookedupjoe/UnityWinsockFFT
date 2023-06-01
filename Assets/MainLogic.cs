using UnityEngine;
using System.Net.WebSockets;
using System.IO;
using System.Text;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Net.Mime;
using System.Net;
using System.Net.Http.Headers;

public class MainLogic : MonoBehaviour
{
    public GameObject bandBar;
    

    public WsClient WebSocketClient;
    private Task WSInst;
    public EqData musicData;
    public String serverURL = "ws://localhost/eq";
    public GameObject noConnectDialog;

    protected HttpClient client;

    public class EqData
    {
        public long pos;
        public int vol;
        public int sCycle;
        public int kCycle;
        public int bCycle;
        public int sVal;
        public int kVal;
        public int bVal;
        public int B1;
        public int B2;
        public int B3;
        public int B4;
        public int B5;
        public int B6;
        public int B7;
        public int B8;
        public int B9;
        public int B10;
        public int B11;
        public int B12;
        public int B13;
        public int B14;
        public int B15;
        public int B16;
        public int B17;
        public int B18;
        public int B19;
        public int B20;
        public int B21;
        public int B22;
        public int B23;
        public int B24;
        public int B25;
        public int B26;
        public int B27;
        public int B28;
        public int B29;
        public int B30;
    }

    public class WebReply {
        public String title;
        public String type;
        public String action;
        public String song;
        public bool success;
    }

    public int getBandValue(int theBand)
    {
        try
        {

            //--- ToDo: Get dynamic property, can't do this everytime!
            if (theBand == 1)
            {
                return musicData.B1;
            }
            else if (theBand == 2)
            {
                return musicData.B2;
            }
            else if (theBand == 3)
            {
                return musicData.B3;
            }
            else if (theBand == 4)
            {
                return musicData.B4;
            }
            else if (theBand == 5)
            {
                return musicData.B5;
            }
            else if (theBand == 6)
            {
                return musicData.B6;
            }
            else if (theBand == 7)
            {
                return musicData.B7;
            }
            else if (theBand == 8)
            {
                return musicData.B8;
            }
            else if (theBand == 9)
            {
                return musicData.B9;
            }
            else if (theBand == 10)
            {
                return musicData.B10;
            }
            else if (theBand == 11)
            {
                return musicData.B11;
            }
            else if (theBand == 12)
            {
                return musicData.B12;
            }
            else if (theBand == 13)
            {
                return musicData.B13;
            }
            else if (theBand == 14)
            {
                return musicData.B14;
            }
            else if (theBand == 15)
            {
                return musicData.B15;
            }
            else if (theBand == 16)
            {
                return musicData.B16;
            }
            else if (theBand == 17)
            {
                return musicData.B17;
            }
            else if (theBand == 18)
            {
                return musicData.B18;
            }
            else if (theBand == 19)
            {
                return musicData.B19;
            }
            else if (theBand == 20)
            {
                return musicData.B20;
            }
            else if (theBand == 21)
            {
                return musicData.B21;
            }
            else if (theBand == 22)
            {
                return musicData.B22;
            }
            else if (theBand == 23)
            {
                return musicData.B23;
            }
            else if (theBand == 24)
            {
                return musicData.B24;
            }
            else if (theBand == 25)
            {
                return musicData.B25;
            }
            else if (theBand == 26)
            {
                return musicData.B26;
            }
            else if (theBand == 27)
            {
                return musicData.B27;
            }
            else if (theBand == 28)
            {
                return musicData.B28;
            }
            else if (theBand == 29)
            {
                return musicData.B29;
            }
            else if (theBand == 30)
            {
                return musicData.B30;
            }
            return 0;
        } 
        catch (Exception ex)
        {
            Debug.Log("Error");
            Debug.Log(ex);
            return 0;
        }
    }



    public static async Task<WebReply> RunWebCommand(String theURLBase, String theURI)
    {
        using (var httpClient = new HttpClient())
        {
            httpClient.BaseAddress = new Uri(theURLBase);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await httpClient.GetAsync(theURI);
            if (response.StatusCode != HttpStatusCode.OK)
                return null;
            var resourceJson = await response.Content.ReadAsStringAsync();
            return JsonUtility.FromJson<WebReply>(resourceJson);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        client = new HttpClient();
       
        //--- Connect to WinSock server
        //*** Setup to prompt if blank and have settings option to set this
        startConnection();
        //*** Create the UI
        createUI();
    }

    public async Task<string> runCommandAsync()
    {
        String tmpURL = "http://localhost/home?cmd=musicAction&action=nextsong";
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(tmpURL),
            Content = new StringContent("some json", Encoding.UTF8, MediaTypeNames.Application.Json),
        };

        var response = await client.SendAsync(request).ConfigureAwait(true);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        return responseBody.ToString();


    }
    public void closeGame()
    {
        Application.Quit();
    }


    public void nextSong()
    {
        nextSongRun();
        Debug.Log("Next Song");
    }

    public async void nextSongRun()
    {
        String tmpURL = "http://localhost/";
        WebReply tmpReply = await RunWebCommand(tmpURL, "home?cmd=musicAction&action=nextsong");

        Debug.Log("Next Song");
    }

    public void tryToConnect()
    {
        //--- In case we need other stuff as part of this
        startConnection();
    }


    private void startConnection()
    {
        startConnection(serverURL);
    }

    private void startConnection(String theURL)
    {
        WebSocketClient = new WsClient(theURL);        
        WSInst = WebSocketClient.Connect();
    }
    public void endConnection()
    {
        //ToDo:  End Connection
    }
    private void createUI()
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

    private void HandleMessage(string msg)
    {
        musicData = JsonUtility.FromJson<EqData>(msg);
    }

    private void noConnectionDialog(bool theShowFlag) {
        noConnectDialog.SetActive(theShowFlag);
    }
    // Update is called once per frame
    void Update()
    {
        //int tmpCount = WebSocketClient.receiveQueue.Count;
        //Debug.Log("Count:" + tmpCount.ToString());
        if (WebSocketClient == null) {
            Debug.Log("No WebSocketClient");
            noConnectionDialog(true);
            return;
        }
        var cqueue = WebSocketClient.receiveQueue;
        if (!WebSocketClient.IsConnectionOpen()) {
            Debug.Log("Not connected");
            noConnectionDialog(true);
            return;
        }
        if (noConnectDialog.activeSelf)
        {
            noConnectionDialog(false);
        }
        
        
        string msg;
        while (cqueue.TryPeek(out msg))
        {
            // Parse newly received messages
            cqueue.TryDequeue(out msg);
            HandleMessage(msg);
        }

        /*
          webSocket.ConnectAsync(new Uri(uri), CancellationToken.None);


         Debug.Log(webSocket.State);

         await Receive();
        */

    }

    // WebSocket
    //private ClientWebSocket ws = new ClientWebSocket();

}


// ------------------------------ BOILERPLATE ----------------------------
//ToDo: Move to generic reusable area

/// <summary>
/// WebSocket Client class.
/// Responsible for handling communication between server and client.
/// </summary>
public class WsClient
{
    // WebSocket
    private ClientWebSocket ws = new ClientWebSocket();
    private UTF8Encoding encoder; // For websocket text message encoding.
    private const UInt64 MAXREADSIZE = 1 * 1024 * 1024;
    // Server address
    private Uri serverUri;
    // Queues
    public ConcurrentQueue<String> receiveQueue { get; }
    public BlockingCollection<ArraySegment<byte>> sendQueue { get; }
    // Threads
    private Thread receiveThread { get; set; }
    private Thread sendThread { get; set; }
    /// <summary>
    /// Initializes a new instance of the <see cref="T:WsClient"/> class.
    /// </summary>
    /// <param name="serverURL">Server URL.</param>
    public WsClient(string serverURL)
    {
        encoder = new UTF8Encoding();
        ws = new ClientWebSocket();
        serverUri = new Uri(serverURL);
        receiveQueue = new ConcurrentQueue<string>();
        receiveThread = new Thread(RunReceive);
        receiveThread.Start();
        sendQueue = new BlockingCollection<ArraySegment<byte>>();
        sendThread = new Thread(RunSend);
        sendThread.Start();
    }
    /// <summary>
    /// Method which connects client to the server.
    /// </summary>
    /// <returns>The connect.</returns>
    public async Task Connect()
    {
        Debug.Log("Connecting to: " + serverUri);
        await ws.ConnectAsync(serverUri, CancellationToken.None);
        while (IsConnecting())
        {
            Debug.Log("Waiting to connect...");
            Task.Delay(50).Wait();
        }
        Debug.Log("Connect status: " + ws.State);
    }
    #region [Status]
    /// <summary>
    /// Return if is connecting to the server.
    /// </summary>
    /// <returns><c>true</c>, if is connecting to the server, <c>false</c> otherwise.</returns>
    public bool IsConnecting()
    {
        return ws.State == WebSocketState.Connecting;
    }
    /// <summary>
    /// Return if connection with server is open.
    /// </summary>
    /// <returns><c>true</c>, if connection with server is open, <c>false</c> otherwise.</returns>
    public bool IsConnectionOpen()
    {
        return ws.State == WebSocketState.Open;
    }
    #endregion
    #region [Send]
    /// <summary>
    /// Method used to send a message to the server.
    /// </summary>
    /// <param name="message">Message.</param>
    public void Send(string message)
    {
        byte[] buffer = encoder.GetBytes(message);
        //Debug.Log("Message to queue for send: " + buffer.Length + ", message: " + message);
        var sendBuf = new ArraySegment<byte>(buffer);
        sendQueue.Add(sendBuf);
    }
    /// <summary>
    /// Method for other thread, which sends messages to the server.
    /// </summary>
    private async void RunSend()
    {
        Debug.Log("WebSocket Message Sender looping.");
        ArraySegment<byte> msg;
        while (true)
        {
            while (!sendQueue.IsCompleted)
            {
                msg = sendQueue.Take();
                //Debug.Log("Dequeued this message to send: " + msg);
                await ws.SendAsync(msg, WebSocketMessageType.Text, true /* is last part of message */, CancellationToken.None);
            }
        }
    }
    #endregion
    #region [Receive]
    /// <summary>
    /// Reads the message from the server.
    /// </summary>
    /// <returns>The message.</returns>
    /// <param name="maxSize">Max size.</param>
    private async Task<string> Receive(UInt64 maxSize = MAXREADSIZE)
    {
        // A read buffer, and a memory stream to stuff unknown number of chunks into:
        byte[] buf = new byte[4 * 1024];
        var ms = new MemoryStream();
        ArraySegment<byte> arrayBuf = new ArraySegment<byte>(buf);
        WebSocketReceiveResult chunkResult = null;
        if (IsConnectionOpen())
        {
            do
            {
                chunkResult = await ws.ReceiveAsync(arrayBuf, CancellationToken.None);
                ms.Write(arrayBuf.Array, arrayBuf.Offset, chunkResult.Count);
                //Debug.Log("Size of Chunk message: " + chunkResult.Count);
                if ((UInt64)(chunkResult.Count) > MAXREADSIZE)
                {
                    Console.Error.WriteLine("Warning: Message is bigger than expected!");
                }
            } while (!chunkResult.EndOfMessage);
            ms.Seek(0, SeekOrigin.Begin);
            // Looking for UTF-8 JSON type messages.
            if (chunkResult.MessageType == WebSocketMessageType.Text)
            {
                return CommunicationUtils.StreamToString(ms, Encoding.UTF8);
            }
        }
        return "";
    }
    /// <summary>
    /// Method for other thread, which receives messages from the server.
    /// </summary>
    private async void RunReceive()
    {
        Debug.Log("WebSocket Message Receiver looping.");
        string result;
        while (true)
        {
            //Debug.Log("Awaiting Receive...");
            result = await Receive();
            if (result != null && result.Length > 0)
            {
                receiveQueue.Enqueue(result);
            }
            else
            {
                Task.Delay(50).Wait();
            }
        }
    }
    #endregion
}
/// <summary>
/// Static class with additional methods used in communication.
/// </summary>
public static class CommunicationUtils
{
    /// <summary>
    /// Converts memory stream into string.
    /// </summary>
    /// <returns>The string.</returns>
    /// <param name="ms">Memory Stream.</param>
    /// <param name="encoding">Encoding.</param>
    public static string StreamToString(MemoryStream ms, Encoding encoding)
    {
        string readString = "";
        if (encoding == Encoding.UTF8)
        {
            using (var reader = new StreamReader(ms, encoding))
            {
                readString = reader.ReadToEnd();
            }
        }
        return readString;
    }
}
