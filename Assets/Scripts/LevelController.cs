using System.Collections;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private Transform m_PlayerSpawnPositionMarker = null;

    [SerializeField]
    private Transform m_PlatformSpawnPositionMarker = null;

    [SerializeField]
    private Transform m_SawSpawnPositionMarker = null;

    [SerializeField]
    private PlatformSpawner m_PlatformSpawner = null;

    [SerializeField]
    private PlayerController m_PlayerPrefab = null;

    private PlayerController m_PlayerInstance = null;

    [SerializeField]
    private Saw m_SawPrefab = null;

    private Saw m_SawInstance = null;

    [SerializeField]
    private CameraController m_Camera = null;

    private void Start()
    {
        RestartLevel();
    }

    private void HandlePlayerKilled(PlayerController player)
    {
        Unsubscribe();
        StartCoroutine(RestartLevelCoroutine());
        GameController.Instance.Reset();
        GameController.Instance.MainInterfacView.ShowKilledMessage();
    }
    
    private void Unsubscribe()
    {
        if (m_PlayerInstance != null)
        {
            m_PlayerInstance.OnPlayerKilled -= HandlePlayerKilled;
        }
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void RestartLevel()
    {
        m_PlayerInstance = Instantiate<PlayerController>(m_PlayerPrefab, m_PlayerSpawnPositionMarker.position, Quaternion.identity);
        m_PlayerInstance.OnPlayerKilled += HandlePlayerKilled;

        m_Camera.SetTarget(m_PlayerInstance.transform);

        m_PlatformSpawner.ResetToPosition(m_PlatformSpawnPositionMarker.position);
        m_PlatformSpawner.StartSpawning();

        if (m_SawInstance == null)
        {
            m_SawInstance = Instantiate<Saw>(m_SawPrefab, m_SawSpawnPositionMarker.position, Quaternion.identity);
        }
        else
        {
            m_SawInstance.transform.position = m_SawSpawnPositionMarker.position;
        }
    }

    private IEnumerator RestartLevelCoroutine()
    {
        m_Camera.SetTarget(null);

        yield return new WaitForSeconds(1f);
        Destroy(m_PlayerInstance.gameObject);

        yield return new WaitForSeconds(1f);
        RestartLevel();
    }
}

