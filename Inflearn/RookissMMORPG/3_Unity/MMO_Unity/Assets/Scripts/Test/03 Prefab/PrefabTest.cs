using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour
{
    // public GameObject prefab;
    GameObject prefab;
    GameObject tank;

    void Start()
    {
        
        /* 큰 규모의 게임을 제작할 경우 모든 객체를 하나씩 연결하기 힘듬
         * 외부 파일의 데이터를 갖고 오는 방식으로 수정
        tank = Instantiate(prefab);
        Destroy(tank, 3.0f);
         */

        /* 우리가 필요할 때마다 직접 호출할 수도 있겠지만, 이런 코드가 산개하기 시작하면 관리하기 힘들어짐
         * 누가 도대체 뭘 언제 만드는 지 추적하기 힘듬
         * 리소스를 관리하는 매니저 추가 → ResourceManager
        prefab = Resources.Load<GameObject>("Prefabs/Tank");
        tank = Instantiate(prefab);
        Destroy(tank, 3.0f);
         */

        tank = Managers.resource.Instantiate("Tank");
        Managers.resource.Destroy(tank, 3.0f);
    }
}
