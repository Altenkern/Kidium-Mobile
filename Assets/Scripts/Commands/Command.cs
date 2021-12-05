using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Command : ScriptableObject
{
    [SerializeField] public int idOfExecution;

    private MonoBehaviour _mb; // The surrogate MonoBehaviour that we'll use to manage this coroutine.
    public abstract void Execute();
    public abstract void ResetCommand();
    public abstract void Stop();
    //public abstract void Pause();
    //public abstract void Continue();

    public virtual void Done()
    {
        ScenarioController.static_CommandEndedEvent();
    }

    public void StartCoroutine(IEnumerator enumerator)
    {
        _mb = GameObject.FindObjectOfType<MonoBehaviour>();
        if (_mb != null)
        {
            Debug.Log("Found a MonoBehaviour.");
            _mb.StartCoroutine(enumerator);
        }
        else
            Debug.Log("No MonoBehaviour object was found in the scene (which should basically be impossible).");
    }

    public void stopCoroutines(IEnumerator enumerator)
    {
        _mb.StopCoroutine(enumerator);
    }
}

