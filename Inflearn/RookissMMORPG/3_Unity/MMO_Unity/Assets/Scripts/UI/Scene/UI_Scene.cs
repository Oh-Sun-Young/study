using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene : UI_Base
{
    /* Start로 할 경우 PlayerController에서 팝업을 생성 후 팝업 내의 Start가 걸리지 않음
     * Start를 이용하는 것은 안 좋은 방법 → Init() 함수
    void Start()
    {
        Managers.ui.SetCanvas(gameObject, false);
    }
     */

    public override void Init()
    {
        Managers.ui.SetCanvas(gameObject, false);
    }
}
