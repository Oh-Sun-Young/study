using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    /* Start로 할 경우 PlayerController에서 팝업을 생성 후 팝업 내의 Start가 걸리지 않음
     * Start를 이용하는 것은 안 좋은 방법 → Init() 함수
    void Start()
    {
        Managers.ui.SetCanvas(gameObject, true);
    }
     */

    public override void Init()
    {
        Managers.ui.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopupUI()
    {
        Managers.ui.ClosePopupUI(this);
    }
}
