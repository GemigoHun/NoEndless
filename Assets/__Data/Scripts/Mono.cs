using UnityEngine;

public class Mono : MonoBehaviour
{
    protected virtual void Awake()
    {
        LoadComponents();
        ResetValues();
    }

    protected virtual void Reset()
    {
        LoadComponents();
    }

    protected virtual void LoadComponents() {}

    protected virtual void ResetValues() {}

    protected virtual void Start() {}

    protected virtual void OnEnable() {}

    protected virtual void OnDisable() {}
}
