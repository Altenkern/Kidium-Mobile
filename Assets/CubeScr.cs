using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScr : MonoBehaviour
{
    float startPosX, startPosY;
    bool isBeingHeld = false;
    Vector2 offset;
    Rigidbody2D rb;

    [SerializeField] string cubeTag;

    public bool isCubeUp = false;
    public bool isUpper = false;
    public bool isDragable = true;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    private void OnMouseDown()
    {
        if (isDragable)
        {
            isBeingHeld = true;
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rb.freezeRotation = true;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragable)
        {
            Vector2 held = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            transform.position = held + offset;
        }
    }
    private void OnMouseUp()
    {
        if (isDragable)
        {
            isBeingHeld = false;
            rb.freezeRotation = false;
            rb.velocity = Vector2.zero;
        }
    }
    public string GetTag()
    {
        return cubeTag;
    }
    public void SetIsCubbed(bool b) => isCubeUp = b;

    public Vector3 GetUpperPos() { return transform.GetChild(0).position; }
        
}
