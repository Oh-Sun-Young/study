using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Canvas의 Sort Order 관리
 * Canvas를 키고 끄는 기능
 */
public class UIManager
{
    int _order = 10;

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _sceneUI = null;

    // Hierarchy 창 정리
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
            canvas.sortingOrder = (_order++);
        else
            canvas.sortingOrder = 0;
    }

    public T MakeSubItem<T>(Transform parent, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.resource.Instantiate($"UI/SubItem/{name}");

        if (parent != null)
            go.transform.SetParent(parent);

        return Util.GetOrAddComponent<T>(go);
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.resource.Instantiate($"UI/Scene/{name}");

        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

        /* Hierarchy 창 정리 
         * ShowPopupUI 와 ShowSceneUI 에서 반복 사용
        GameObject root = GameObject.Find("@UI_Root");
        if (root == null)
            root = new GameObject { name = "@UI_Root" };
        go.transform.SetParent(root.transform);
        */

        go.transform.SetParent(Root.transform);

        return sceneUI;
    }

    /// <summary>
    /// 팝업창 열기
    /// </summary>
    /// <typeparam name="T">컴포넌트 이름</typeparam>
    /// <param name="name">Prefab 이름</param>
    /// <returns></returns>
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.resource.Instantiate($"UI/Popup/{name}");

        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        /* _order를 ShowPopupUI 내에서 카운트할 경우, 게임 시작할 때부터 만든 팝업은 처리가 안 되는 이슈 발생 
        _order++;
         */

        /* Hierarchy 창 정리 
         * ShowPopupUI 와 ShowSceneUI 에서 반복 사용
        GameObject root = GameObject.Find("@UI_Root");
        if (root == null)
            root = new GameObject { name = "@UI_Root" };
        go.transform.SetParent(root.transform);
        */

        go.transform.SetParent(Root.transform);

        return popup;
    }
    /// <summary>
    /// 팝업창 닫기
    /// </summary>
    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.resource.Destroy(popup.gameObject);
        popup = null;

        _order--;
    }

    /// <summary>
    /// 전체 팝업창 닫기
    /// </summary>
    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }
}