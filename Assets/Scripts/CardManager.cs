using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private static UIManager instance;

    List<GameObject> cards = new List<GameObject> ();
    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(instance);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
