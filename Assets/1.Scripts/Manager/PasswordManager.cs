using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordManager : MonoBehaviour
{
    public static PasswordManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }


    public int GetPassword()
    {
        int pass = 0;

        pass = Mathf.FloorToInt(Random.Range(1000, 9999));

        return pass;
    }
}
