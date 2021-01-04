using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public Animator transitionDeath;

    public float transitionTime = 1f;
    public float transitionTimeDeath = 1f;

    public static bool isDeathAnimation = false;

    private void Awake()
    {
        resetAnimators();
    }

    private void resetAnimators()
    {
        if (isDeathAnimation)
        {
            transition.enabled = false;
            transitionDeath.enabled = true;
        }
        else
        {
            transition.enabled = true;
            transitionDeath.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        isDeathAnimation = false;
        if (SceneManager.GetActiveScene().buildIndex + 1 == 3)
        {
            StartCoroutine(LoadLevel(0));
        } else
        {
            StartCoroutine((LoadLevel(SceneManager.GetActiveScene().buildIndex + 1)));
        }
        
    }

    public void ReloadLevelDeath()
    {
        isDeathAnimation = true;
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex, true));

    }

    IEnumerator LoadLevel(int levelIndex, bool isDeath = false)
    {
        resetAnimators();
        if (isDeath)
        {
            transitionDeath.SetTrigger("start");
        } else
        {
            transition.SetTrigger("start");
        }
        
        yield return new WaitForSeconds(isDeath ? transitionTimeDeath : transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
