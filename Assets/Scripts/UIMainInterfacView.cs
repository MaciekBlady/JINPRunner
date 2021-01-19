using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIMainInterfacView : UIBehaviour
{
    [SerializeField]
    private GameObject m_PlayerKilledPopup;

    [SerializeField]
    private TextMeshProUGUI m_CoinText;
    [SerializeField]
    private TextMeshProUGUI m_TimeText;

    [SerializeField]
    private Button m_PauseButton;

    public void Initialize(UnityAction onPause)
    {
        m_PauseButton.onClick.AddListener(onPause);
    }

    public void Configure(ulong coinNumber, float time)
    {
        m_CoinText.text = coinNumber.ToString();
        m_TimeText.text = time.ToString();
    }

    public void ShowKilledMessage()
    {
        m_PlayerKilledPopup.SetActive(true);
        Invoke("HideKilledMessage", 1.0f);
    }

    private void HideKilledMessage()
    {
        m_PlayerKilledPopup.SetActive(false);
    }
}
