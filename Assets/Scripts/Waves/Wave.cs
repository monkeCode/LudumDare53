using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wave : ScriptableObject
{
    public abstract void Spawn(Vector2 position, float radius);
}
