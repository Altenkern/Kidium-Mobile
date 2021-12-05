using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeSide : MonoBehaviour
{
    public CubeScr cube;
    string cubeTag;
    void Awake()
    {
        cubeTag = cube.GetTag();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(cubeTag))
            cube.SetIsCubbed(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(cubeTag))
            cube.SetIsCubbed(false);
    }

}
