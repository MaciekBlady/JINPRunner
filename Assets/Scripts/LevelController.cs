using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private Transform m_PlayerSpawnPositionMarker = null;

    [SerializeField]
    private Transform m_PlatformSpawnPositionMarker = null;

    [SerializeField]
    private PlatformSpawner m_PlatformSpawner = null;

    [SerializeField]
    private PlayerController m_PlayerPrefab = null;

    private PlayerController m_PlayerInstance = null;

    [SerializeField]
    private CameraController m_Camera = null;

    private void Start()
    {
        m_PlayerInstance = Instantiate(m_PlayerPrefab, m_PlayerSpawnPositionMarker.position, Quaternion.identity);
        m_Camera.SetTarget(m_PlayerInstance.transform);

        m_PlatformSpawner.ResetToPosition(m_PlatformSpawnPositionMarker.position);
        m_PlatformSpawner.StartSpawning();
    }
}

