using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_InputField ipInputField;
    public Button connectButton;
    public Button skipButton;
    public GameObject canvas;
    public GameObject panel;
    public ClientManager clientManager;

    public Image hpBar1;
    public Image hpBar2;
    public TMP_Text tourText;



    void Start()
    {
        connectButton.onClick.AddListener(OnConnectButtonClicked);
        skipButton.onClick.AddListener(OnSkipButtonClicked);
    }

    void OnConnectButtonClicked()
    {
        string ipAddress = ipInputField.text;
        clientManager.ConnectToServer(ipAddress, () => SetCanvasActive(false));
    }

    void OnSkipButtonClicked()
    {
        clientManager.SkipConnection();
    }

    void SetCanvasActive(bool isActive)
    {
        canvas.SetActive(isActive);
        panel.SetActive(true);
    }

    public void SetHPs(int hp1, int hp2)
    {
        //Debug.Log(hp1 + ", " + hp2);
        hpBar1.fillAmount = hp1 / 100f;
        hpBar2.fillAmount = hp2 / 100f;
    }
    public void SetTour(int tour)
    {
        if (tour == 1)
        {
            tourText.text = "Tour: P1";
        }
        else if(tour == 2)
        {
            tourText.text = "Tour: P2";
        }
        else
        {
            Debug.Log("Tour Error!!! " + tour);
        }
    }
}
