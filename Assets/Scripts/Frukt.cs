using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Object", menuName = "Scenery/Object", order = 3)]
public class Frukt : ScriptableObject
{
    [SerializeField] public Frukt[] _fruktColor; //color of sprite
    [SerializeField] public Sprite[] _sprites;
    [SerializeField] public bool _isFemineRod;
    [SerializeField] public enum FruktType { fruct, color }
    [SerializeField] public FruktType _fruktType;
    [SerializeField] public AudioClip _bonyImenPadezj, _bonyRodPadezj, _archiImenPadezj, _archiRodPadezj, _partAudio, _shadowAudio, _describeAudio, _colorAudio;
    [SerializeField] public string _name;
    [SerializeField] public Color _color; //if frukt is color

    [SerializeField] public Sprite _shadow, _partSprite, _colorSprite;
}
