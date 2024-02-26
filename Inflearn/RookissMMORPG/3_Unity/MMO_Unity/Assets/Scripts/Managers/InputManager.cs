using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/* InputManager : 키보드 입력을 체크하는 매니저 
* 싱글톤으로 만든 Managers가 있으니 컴포넌트로 만들 필요 X
* 리스터 패턴
* 이벤트를 전파하는 방식
*/
public class InputManager
{
    public Action KeyAction = null; // Action은 일종의 델리게이트
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;
    float _pressedTime = 0;

    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke();

        if(MouseAction != null)
        {
            if (Input.GetMouseButton(0))
            {
                if (!_pressed)
                {
                    MouseAction.Invoke(Define.MouseEvent.PointerDown);
                    _pressedTime = Time.time;
                }
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed)
                {
                    if (Time.time < _pressedTime + 0.2f)
                        MouseAction.Invoke(Define.MouseEvent.PointerUp);
                    MouseAction.Invoke(Define.MouseEvent.Click);
                }
                _pressed = false;
                _pressedTime = 0;
            }
        }
    }

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }
}
