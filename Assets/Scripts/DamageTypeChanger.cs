using UnityEngine;

public class DamageTypeChanger : Movable
{
    [field: SerializeField] public DamageType DamageType { get; private set; }
}