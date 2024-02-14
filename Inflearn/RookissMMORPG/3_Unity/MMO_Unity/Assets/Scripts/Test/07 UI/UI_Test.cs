using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Test : UI_Popup
{
    /* UI 자동화 필요
     */
    [SerializeField]
    TextMeshProUGUI _text;

    enum Buttons
    {
        PointButton
    }

    enum Texts
    {
        PointText,
        ScoreText
    }

    enum GameObjects
    {
        TestObject,
        TestObject2
    }

    enum Images
    {
        ItemIcon
    }

    int _score = 0;

    private void Start()
    {
        Init();

        /*
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        GetText((int)Texts.ScoreText).text = "Bind Text";

        //이벤트 코드 축약
        //GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        //UI_EventHandler evt = go.GetComponent<UI_EventHandler>();
        //evt.OnDragHandler += ((PointerEventData data) => { go.transform.position = data.position; });

        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);

        //GetImage((int)Images.ItemIcon).gameObject.BindEvent((PointerEventData data) => { transform.position = data.position; }, Define.UIEvent.Drag); // extension method 적용

        GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked); // extension method 적용
         */
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        GetText((int)Texts.ScoreText).text = "Bind Text";

        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);

        GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked);
    }

    public void OnButtonClicked(PointerEventData data)
    {
        // Debug.Log("Button Click!");
        GetText((int)Texts.ScoreText).text = $"Score : {++_score}";
    }
}