using UnityEngine;

public abstract class ASingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T s_Instance = null; 

    public static T Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType<T>();
            }

            return s_Instance;
        }
    }

    private void Awake()
    {
        if (s_Instance != null && s_Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            s_Instance = this as T;
            Initalize();
        }
    }

    protected abstract void Initalize();
}
