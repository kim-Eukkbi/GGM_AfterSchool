using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Logic : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private Transform spawnPoint;



    private void Awake()
    {
        spawnPoint = transform.GetChild(0);
        GameObject _player = GameObject.FindWithTag("Player");

        if (_player != null)
        {
            _player.transform.position = spawnPoint.position;
        }
        else
        {
            _player = Instantiate(player, spawnPoint);

            _player.transform.SetParent(null);
            DontDestroyOnLoad(_player);
        }

    }
}
