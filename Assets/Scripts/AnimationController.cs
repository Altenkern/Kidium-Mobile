using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AnimationController : MonoBehaviour
{
    static AnimationController Instance;

    [SerializeReference] private GameObject spriteAnimationPrefab;
    [SerializeReference] private GameObject canvasGO;

    public delegate void doneEvent();


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

    }

    public static void StopAnimation_static()
    {
        Instance.StopAllCoroutines();
    }

    private void MakeAnimation(Sprite[] sheet, Vector2 ofsett, float delay, doneEvent done)
    {
        GameObject go = Instantiate(spriteAnimationPrefab, canvasGO.transform);
        go.transform.position = canvasGO.transform.position;
        StartCoroutine(playAnimation(go.GetComponent<Image>(), sheet, delay, done));
    }

    public static void MakeAnimation_static(Sprite[] sheet, Vector2 ofsett, float delay, doneEvent done)
    {
        Instance.MakeAnimation(sheet, ofsett, delay, done);
    }
    
    IEnumerator playAnimation(Image spriteGO, Sprite[] sheet, float delay, doneEvent done)
    {
        spriteGO.gameObject.SetActive(false);
        yield return new WaitForSeconds(3);
        spriteGO.gameObject.SetActive(true);
        spriteGO.sprite = sheet[0];
        yield return new WaitForSeconds(delay);
        for (int i = 1; i < sheet.Length; i++)
        {
            spriteGO.sprite = sheet[i];
            yield return new WaitForSeconds(delay);
        }
        spriteGO.transform.DOScale(Vector3.zero, delay * 10);
        spriteGO.transform.DORotate(new Vector3(0, 0, 360), delay * 10);
        yield return new WaitForSeconds(delay*10);
        done();
        Destroy(spriteGO.gameObject, delay * 10+5);
        yield return null;
    }
}
