using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_Velocity;

    [SerializeField]
    private float m_SpawnInterval = 2f;

    [SerializeField]
    private PlatfromSet m_PlatfromSet = null;

    private Coroutine m_SpawningCoroutine = null;

    private List<GameObject> m_SpawnedPlatforms = new List<GameObject>();

    private void Update()
    {
        if (m_SpawningCoroutine == null)
        {
            return;
        }

        transform.position += m_Velocity;
    }

    public void StartSpawning()
    {
        StopSpawning();
        m_SpawningCoroutine = StartCoroutine(PlatformSpawningCoroutine());
    }

    public void StopSpawning()
    {
        if (m_SpawningCoroutine != null)
        {
            StopCoroutine(m_SpawningCoroutine);
            m_SpawningCoroutine = null;
        }
    }

    private IEnumerator PlatformSpawningCoroutine()
    {
        while (true)
        {
            SpawnPlatfrom();
            yield return new WaitForSeconds(m_SpawnInterval);
        }
    }

    private void SpawnPlatfrom()
    {
        GameObject platformPrefab = m_PlatfromSet.GetRandom();
        GameObject platformInstance = Instantiate(platformPrefab, transform.position, Quaternion.identity);

        m_SpawnedPlatforms.Add(platformInstance);
    }

    public void ResetToPosition(Vector3 position, bool destroyAllPlatfroms = true)
    {
        transform.position = position;

        if (destroyAllPlatfroms)
        {
            for (int i = 0; i < m_SpawnedPlatforms.Count; i++)
            {
                Destroy(m_SpawnedPlatforms[i]);
            }

            m_SpawnedPlatforms.Clear();
        }
    }
}

