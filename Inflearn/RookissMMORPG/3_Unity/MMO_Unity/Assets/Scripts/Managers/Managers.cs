using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    /* 함수 형태로 호출하면 괄호까지 사용해야 하기 때문에 프로퍼티로 변경
     * static Managers instance; // 유일성이 보장된다
     * public static Managers GetInstance() { Init(); return instance; } // 유일한 매니저를 갖고 온다
     */

    static Managers s_instance; // 유일성이 보장된다
    static Managers instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고 온다

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();

    public static InputManager input { get { return instance._input; } }
    public static ResourceManager resource { get { return instance._resource; } }



    // Start is called before the first frame update
    void Start()
    {
        // 초기화
        /* Manager가 여러개가 있는 경우 덮어쓰는 문제 발생
         instance = this;
         */
        /* Find를 활용하여 Manager를 지정한 경우 해당 이름을 가진 오브젝트가 없는 경우 Null값이 되어 문제 발생
         GameObject go = GameObject.Find("@Managers");
         instance = go.GetComponent<Managers>();
         */
        Init();
    }

    private void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        // 초기화
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }
}
