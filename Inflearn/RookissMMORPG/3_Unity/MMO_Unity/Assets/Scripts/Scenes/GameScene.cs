using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        //Managers.resource.Instantiate("UI/UI_Test");

        //UI_Test popup = Managers.ui.ShowPopupUI<UI_Test>();
        //Managers.ui.ClosePopupUI(popup);

        Managers.ui.ShowSceneUI<UI_Inven>();

        for (int i = 0; i < 5; i++)
            Managers.resource.Instantiate("UnityChan");
    }

    public override void Clear()
    {
        
    }
}
