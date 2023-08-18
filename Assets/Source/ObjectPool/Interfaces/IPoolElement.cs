using UnityEngine;

public interface IPoolElement
{
    public int Level { get; }

    public bool IsAlive { get; }

    public Transform GetTransform();   
}