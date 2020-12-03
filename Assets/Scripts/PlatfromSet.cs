using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlatfromSet", menuName = "ScriptableObjects/PlatfromSet")]
public class PlatfromSet : ScriptableObject
{
    [SerializeField]
    private List<GameObject> m_PlatformPrefabs = new List<GameObject>();

    public GameObject GetRandom()
    {
        int randomPrefabIndex = Random.Range(0, m_PlatformPrefabs.Count);
        return m_PlatformPrefabs[randomPrefabIndex];
    }
}

