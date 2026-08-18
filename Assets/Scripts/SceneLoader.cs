using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [Header ("Main Manu")]
    [SerializeField] private GameObject StarterMenu;
    [Header("Prefab References")]
    [SerializeField] private GameObject environmentPrefab;
    [SerializeField] private GameObject trackableObject;

    [Header("Camera Cover")]
    [SerializeField] private GameObject fader;
    [SerializeField] private float fadeTime = 1f;

    private Image faderImage;

    private void Awake()
    {
        faderImage = fader.GetComponent<Image>();
    }

    public void InitLoadScene()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        // Turn fader ON
        fader.SetActive(true);

        // Make sure fader starts completely opaque
        SetAlpha(1f);

        StarterMenu.SetActive(false);
        
        // Load Environment
        if (environmentPrefab != null)
        {
            Instantiate(
                environmentPrefab,
                Vector3.zero,
                Quaternion.identity
            );
        }
        yield return null;

        // Load Trackable Object
        if (trackableObject != null)
        {
            Instantiate(
                trackableObject,
                Vector3.zero,
                Quaternion.identity
            );
        }
        yield return null;

        Debug.Log("Scene loading completed.");

        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(FadeOut());

        fader.SetActive(false);
    }

    private IEnumerator FadeOut()
    {
        float timer = 0f;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;

            float t = Mathf.Clamp01(timer / fadeTime);

            // 1 -> 0
            float alpha = Mathf.Lerp(1f, 0f, t);

            SetAlpha(alpha);

            yield return null;
        }

        SetAlpha(0f);

        Destroy(StarterMenu);
    }

    private void SetAlpha(float alpha)
    {
        Color color = faderImage.color;
        color.a = alpha;
        faderImage.color = color;
    }
}