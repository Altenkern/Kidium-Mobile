using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest2 : Quest
{
    [SerializeField] GameObject redCube, greenCube;

    [SerializeField] GameObject line;

    CubeScr[] redCubes = new CubeScr[3];
    CubeScr[] greenCubes = new CubeScr[3];

    

    bool hasBeenBecome2ndStage = false;

    [SerializeField] AudioClip davaiPostroim, naidiRed, postav1na2, uraPostroil, DavaiBashnyaRed, PostavRed, uraRed, ZelenBigger, krasnBigger;

    bool isGameEnd = true;
    public override void StartQuest()
    {
        SpawnCubes(false);
        isGameEnd = false;
        AudioController.ClearQueue_Static();
        line.SetActive(true);
        AudioController.AddSoundAndPlay_Static(new List<AudioClip>() { davaiPostroim, naidiRed, postav1na2});
    }

    void SpawnCubes(bool isRed)
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject go = Instantiate(isRed ? redCube : greenCube, transform);
            if(i==0)
                go.transform.position = new Vector2(2, 10) + Random.insideUnitCircle;
            else
                go.transform.position = new Vector2(-2, 10) + Random.insideUnitCircle;

            go.transform.localScale = new Vector2(0.3f - 0.05f * i * (isRed ? -1 : 1), 0.3f - 0.05f * i * (isRed ? -1 : 1));
            go.GetComponent<Rigidbody2D>().MovePosition(new Vector2(10, 10) + Random.insideUnitCircle * 5);
            go.GetComponent<SpriteRenderer>().sortingOrder = i * (isRed ? -1 : 1);
            if(isRed)
                redCubes[i] = go.GetComponent<CubeScr>();
            else
                greenCubes[i] = go.GetComponent<CubeScr>();

            if (isRed)
            {
                if (i == 0)
                    redCubes[i].isUpper = true;
            }
            else
                if (i == 2)
                greenCubes[i].isUpper = true;
        }
    }

    private void Update()
    {
        if(!isGameEnd)
            if(!hasBeenBecome2ndStage)
            {
                for (int i = 0; i < greenCubes.Length; i++)
                {
                    if (greenCubes[i].isCubeUp || greenCubes[i].isUpper)
                    {

                    }
                    else
                        return;
                }
                Make2ndStage();
            }
            else
            {
                for (int i = 0; i < redCubes.Length; i++)
                {
                    if (redCubes[i].isCubeUp || redCubes[i].isUpper)
                    {

                    }
                    else
                        return;
                }
                OnComplete();
            }
    }
    void Make2ndStage()
    {
        AudioController.ClearQueue_Static();
        AudioController.AddSoundAndPlay_Static(new List<AudioClip>() { uraPostroil, DavaiBashnyaRed, PostavRed });
        for (int i = 0; i < greenCubes.Length; i++)
        {
            greenCubes[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            greenCubes[i].isDragable = false;
        }
        greenCubes[2].transform.position = greenCubes[1].GetUpperPos();
        SpawnCubes(true);
        hasBeenBecome2ndStage = true;
    }

    public override void OnComplete()
    {
        foreach (CubeScr c in redCubes)
        {
            c.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            c.isDragable = false;
        }
        redCubes[0].transform.position = redCubes[1].GetUpperPos();
        isGameEnd = true;

        AudioController.ClearQueue_Static();
        AudioController.AddSoundAndPlay_Static(new List<AudioClip>() { uraRed, ZelenBigger, krasnBigger });
        StartCoroutine(waitForEnd());

    }
    IEnumerator waitForEnd()
    {
        yield return new WaitForSeconds(14f);
        line.SetActive(false);
        foreach (CubeScr c in redCubes)
        {
            Destroy(c.gameObject);
        }
        foreach (CubeScr c in greenCubes)
        {
            Destroy(c.gameObject);
        }

        next.StartQuest();
    }
}
