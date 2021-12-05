using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(fileName = "SpriteCommand", menuName = "Scenery/Sprite Command", order = 7)]
public class SpruteAnimationCommand : Command
{

    [SerializeReference] private float delay;
    [SerializeReference] private Sprite[] spritesheet;
    [SerializeReference] private Vector2 offset;

    public override void Execute()
    {
        AnimationController.MakeAnimation_static(spritesheet, offset, delay, Done);
    }

    public override void ResetCommand()
    {
        AnimationController.StopAnimation_static();
        AnimationController.MakeAnimation_static(spritesheet, offset, delay, Done);
    }

    public override void Stop()
    {
        AnimationController.StopAnimation_static();
    }
}
