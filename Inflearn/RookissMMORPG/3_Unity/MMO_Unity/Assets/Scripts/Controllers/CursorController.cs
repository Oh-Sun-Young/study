using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    Texture2D _handIcon;
    Texture2D _attackIcon;

    enum CursorType
    {
        None,
        Attack,
        Hand
    }

    CursorType _cursorType = CursorType.None;

    void Start()
    {
        _handIcon = Managers.resource.Load<Texture2D>("Textures/Cursor/Hand");
        _attackIcon = Managers.resource.Load<Texture2D>("Textures/Cursor/Attack");
    }


    void Update()
    {
        UpdateMouseCursor();
    }

    void UpdateMouseCursor()
    {
        if (Input.GetMouseButton(0))
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, _mask))
        {
            switch (hit.collider.gameObject.layer)
            {
                case (int)Define.Layer.Ground:
                    if (_cursorType != CursorType.Hand)
                    {
                        Cursor.SetCursor(_handIcon, new Vector2(_handIcon.width / 3, 0), CursorMode.Auto);
                        _cursorType = CursorType.Hand;
                    }
                    break;
                case (int)Define.Layer.Monster:
                    if (_cursorType != CursorType.Attack)
                    {
                        Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.Auto);
                        _cursorType = CursorType.Attack;
                    }
                    break;
            }

        }
    }
}
