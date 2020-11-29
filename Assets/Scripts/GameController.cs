using UnityEngine;

public class GameController : ASingleton<GameController>
{
    private ulong m_Points = 0;

    public void HandleCoinPickedUp(Coin coin)
    {
        if (coin == null)
        {
            return;
        }

        m_Points += coin.Points;
        Debug.LogFormat("Total points: {0}", m_Points);
    }
}
