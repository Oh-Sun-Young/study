using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* InputManager : 키보드 입력을 체크하는 매니저 
 * 싱글톤으로 만든 Managers가 있으니 컴포넌트로 만들 필요 X
 * 리스터 패턴
 * 이벤트를 전파하는 방식
 */
public class InputManager
{
    public Action KeyAction = null; // Action은 일종의 델리게이트

    public void OnUpdate()
    {
        if (Input.anyKey == false)
            return;

        if (KeyAction != null)
            KeyAction.Invoke();
    }
}
