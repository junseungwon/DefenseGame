using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip BGMclip;

    public static ScenesManager Instance { get; private set; }

    public Image fadeImage;
    public float fadeDuration = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // �̺�Ʈ ���� ����
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(PlayBGMWithDelay());
    }

    private IEnumerator PlayBGMWithDelay()
    {
        yield return new WaitForSeconds(0.1f);
        PlayBGM();
    }

    private void PlayBGM()
    {
        if (audioSource != null && BGMclip != null)
        {
            audioSource.clip = BGMclip;
            audioSource.Play();
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneWithFade(sceneName));
    }

    private IEnumerator LoadSceneWithFade(string sceneName)
    {
        StopBGM();

        // ���̵� �ƿ�
        yield return StartCoroutine(Fade(0f, 1f));

        // �񵿱� �� �ε�
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }

        // ���̵� ��
        yield return StartCoroutine(Fade(1f, 0f));
    }

    private void StopBGM()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = endAlpha;
        fadeImage.color = color;
    }

    // ���� ���� �޼��� �߰�
    public void QuitGame()
    {
        StartCoroutine(QuitGameWithFade());
    }

    private IEnumerator QuitGameWithFade()
    {
        // ���̵� �ƿ�
        yield return StartCoroutine(Fade(0f, 1f));

        // ���� ����
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
