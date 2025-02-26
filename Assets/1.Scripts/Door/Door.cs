using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField]
    private int ChapterIndex;

    [SerializeField]
    private Chapter_Logic logic;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            if(SceneManager.GetActiveScene().name.Contains("Chapter"))
            {
                if(logic != null)
                {
                    logic.PasswordUIControll(true);
                }
            }
            else
            {
                SceneChangeManager.Instance.LoadScene(ChapterIndex);
            }

           
        }
    }
}
