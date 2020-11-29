using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField]
    private ulong m_Points = 0;

    public ulong Points
    {
        get
        {
            return m_Points;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            HandlePickedUp();
        }
    }

    private void HandlePickedUp()
    {
        GameController.Instance.HandleCoinPickedUp(this);
        Destroy(gameObject);
    }
}
