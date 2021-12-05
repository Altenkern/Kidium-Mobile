using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scenario", menuName = "Scenery/Scenario", order = 1)]
public class Scenario : ScriptableObject
{


    [SerializeField] public List<Command> _commands;
    public int _currentCommand = 0;
    public int maxIdOfCommand;

    private int currentCountOfEndedCommands;
    private int neededCountOfEndedCommands;


    public void InitScenario()
    {
        maxIdOfCommand = -1;
        _currentCommand = 0;
        //commandEnded += CommandEnded;

        foreach (Command c in _commands)
        {
            if (maxIdOfCommand < c.idOfExecution)
            {
                maxIdOfCommand = c.idOfExecution;
            }
        }
    }


    #region CommandMainFunctions
    public void ReplayCommand()
    {
        foreach (Command c in _commands)
        {
            if (c.idOfExecution == _currentCommand - 1)
            {
                c.Stop();
                c.ResetCommand();
            }
        }
        currentCountOfEndedCommands = 0;
        neededCountOfEndedCommands = 0;
        foreach (Command c in _commands)
        {
            if (c.idOfExecution == _currentCommand - 1)
            {
                c.Execute();
                neededCountOfEndedCommands++;
            }
        }
    }


    public void PlayNext()
    {
        if (_currentCommand == maxIdOfCommand+1)
        {
            ScenarioController.NextState_Static();
        }
        else
        {
            currentCountOfEndedCommands = 0;
            neededCountOfEndedCommands = 0;
            foreach (Command c in _commands)
            {
                if (_currentCommand == c.idOfExecution)
                {
                    c.Execute();
                    neededCountOfEndedCommands++;
                }
            }

            _currentCommand++;

            if (neededCountOfEndedCommands == 0)
                PlayNext();
        }
    }

    public void SetCurrentCommandID(int id)
    {
        if(id > maxIdOfCommand)
        {
            Debug.Log("Setted ID is too big.");
            _currentCommand = maxIdOfCommand;
        }    
        else
        {
            _currentCommand = id;
        }
    }
    #endregion


    #region Synchronize


    public void CommandEnded()
    {
        currentCountOfEndedCommands++;
        if (currentCountOfEndedCommands == neededCountOfEndedCommands)
            PlayNext();
    }

    


    #endregion
}

