using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelVarTransfer : MonoBehaviour
{
    public static LevelVarTransfer Instance;

    public List<Gun> staticGunsArray;
    public bool hasDash;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
