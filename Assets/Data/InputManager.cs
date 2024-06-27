using UnityEngine;

public class InputManager : GMono
{
    private static InputManager instance;

    public static InputManager Instance => instance;

    [SerializeField] private Vector3 mousePos;

    public Vector3 MousePos => mousePos;

    protected override void Awake()
    {
        base.Awake();
        if(instance != null) Debug.Log("Only 1 InputManager is allowed to exist!");

        instance = this;
    }

    protected void Update()
    {
        GetMousePos();
    }

    public void GetMousePos()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}