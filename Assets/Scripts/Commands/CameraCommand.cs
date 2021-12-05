using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "CameraCommand", menuName = "Scenery/Camera Command", order = 2)]
public class CameraCommand : Command
{
    public enum CameraCommandTypes
    {
        move,
        zoom
    }

    public CameraCommandTypes _commandType;

    public Vector3 _vectorStartOfMove, _vectorEndOfMove;
    public int _speed, _percentOfZoom;

    public override void Execute()
    {
        
    }

    public override void ResetCommand()
    {

    }

    public override void Stop()
    {

    }
}