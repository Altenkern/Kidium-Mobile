using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRotator : MonoBehaviour
{
    RectTransform rectTransform;

    [SerializeField] private float _speed;

    float doMove => _speed * Time.deltaTime;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        rectTransform.Rotate(Vector3.forward, doMove);
    }
}
