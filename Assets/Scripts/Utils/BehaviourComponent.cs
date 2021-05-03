using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviourComponent
{
    public BehaviourComponent (GameObject gameObject)
    {
        this.gameObject = gameObject;
        this.transform = gameObject.transform;
    }

    protected GameObject gameObject;
    protected Transform transform;
}
