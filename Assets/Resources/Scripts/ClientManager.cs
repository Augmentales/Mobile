using System;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [DllImport("client")]
    private static extern IntPtr createClientInstance();

    [DllImport("client")]
    private static extern void destroyClientInstance(IntPtr instance);

    [DllImport("client")]
    private static extern IntPtr getPlatformABI();

    [DllImport("client")]
    private static extern bool connectToServer(IntPtr instance, string serverUri);

    [DllImport("client")]
    private static extern void startReceivingData(IntPtr instance);

    [DllImport("client")]
    private static extern void stopReceivingData(IntPtr instance);

    [DllImport("client")]
    private static extern void disconnect(IntPtr instance);

    [DllImport("client")]
    private static extern IntPtr getReceivedData(IntPtr instance);

    [DllImport("client")]
    private static extern bool hasNewData(IntPtr instance);

    private IntPtr clientInstance;
    private Thread dataReceptionThread;
    private Thread dataProcessingThread;
    private bool isRunning;
    private UnityMainThreadDispatcher mainThreadDispatcher;
    public Cards[] cards;
    public UIManager uiManager;

    public string[] ParsedData { get; private set; }

    void Start()
    {
        UnityMainThreadDispatcher.Initialize(); // Initialize the dispatcher on the main thread
        mainThreadDispatcher = UnityMainThreadDispatcher.Instance();
    }

    void OnDestroy()
    {
        if (clientInstance != IntPtr.Zero)
        {
            Debug.Log("Stopping data reception and disconnecting...");
            StopDataReception();
            disconnect(clientInstance);
            destroyClientInstance(clientInstance);
            Debug.Log("Client instance destroyed");
        }
    }

    public void ConnectToServer(string serverIp, Action onConnected)
    {
        Debug.Log("Creating client instance...");
        clientInstance = createClientInstance();

        string abi = Marshal.PtrToStringAnsi(getPlatformABI());
        Debug.Log("Platform ABI: " + abi);

        Debug.Log("Attempting to connect to server...");
        Debug.Log(serverIp);

        // Run the connection attempt in a separate thread
        Thread connectionThread = new Thread(() =>
        {
            bool isConnected = connectToServer(clientInstance, "ws://" + serverIp + ":8080");

            // Ensure that further actions after connection attempt are executed on the main thread
            mainThreadDispatcher.Enqueue(() =>
            {
                if (isConnected)
                {
                    Debug.Log("Connected to server");
                    StartDataReception();
                    onConnected?.Invoke(); // Invoke the callback if connected
                }
                else
                {
                    Debug.Log("Failed to connect to server");
                }
            });
        });

        connectionThread.Start();
    }

    public void SkipConnection()
    {
        Debug.Log("Skipping server connection as per user choice.");
        // You can add any other logic you need here if the user chooses to skip the connection
    }

    private void StartDataReception()
    {
        isRunning = true;

        // Start the thread that will call startReceivingData
        dataReceptionThread = new Thread(() => startReceivingData(clientInstance));
        dataReceptionThread.Start();

        // Start the thread that will process the received data
        dataProcessingThread = new Thread(DataProcessingLoop);
        dataProcessingThread.Start();
    }

    private void StopDataReception()
    {
        if (dataReceptionThread != null && dataReceptionThread.IsAlive)
        {
            stopReceivingData(clientInstance); // Signal the native code to stop
            dataReceptionThread.Join(); // Wait for the thread to finish
        }

        if (dataProcessingThread != null && dataProcessingThread.IsAlive)
        {
            isRunning = false;
            dataProcessingThread.Join(); // Wait for the thread to finish
        }
    }

    private void DataProcessingLoop()
    {
        while (isRunning)
        {
            if (hasNewData(clientInstance))
            {
                IntPtr dataPtr = getReceivedData(clientInstance);
                string data = Marshal.PtrToStringAnsi(dataPtr);

                // Process data on the main thread to ensure thread safety
                mainThreadDispatcher.Enqueue(() =>
                {
                    //Debug.Log("Data received from native code: " + data);
                    ProcessData(data);
                });
            }

            // Sleep for a short time to avoid busy-waiting
            //Thread.Sleep(100);
        }
    }

    void ProcessData(string data)
    {
        // Parse the data
        ParsedData = ParseData(data);

        if (ParsedData.Length != 28 || ParsedData == null)
        {
            //Debug.Log("Data is inappropriate!!!");
            //Debug.Log(ParsedData.Length);
            return;
        }
        else if (ParsedData[0] == "ark")
        {
            {
                uiManager.SetHPs(int.Parse(ParsedData[1].ToString()), int.Parse(ParsedData[2].ToString()));
                uiManager.SetTour(int.Parse(ParsedData[3].ToString()));
                for (int i = 4; i < 28; i++)
                {
                    cards[i - 4].setData(ParsedData[i][0] == '1', ParsedData[i][1] == '1');
                }
            }
            Debug.Log("Raw data: " + data);
            // Implement your data processing logic here
        }
    }

    private string[] ParseData(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            return new string[0];
        }

        return data.Split(',');
    }
}
