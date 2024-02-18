using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    #region Coroutine Test
    /* Coroutine
     * 함수를 일시정지 시켰다가 값들이 복원된 상태에서 다시 재생
     * C# 지원 / C++ 지원 X
    int i = 0;
    void VeryComplicated()
    {
        for (; i < 1000000; i++)
        {
            // 어마어마하게 힘든 작업
            Debug.Log($"Hello : {i}");
            if (i % 10000 == 0)
                break;
        }
    }
     */
    class Test
    {
        public int id = 0;
    }
    /* Coroutine 개념정리
     * 1. 함수의 상태를 저장/복원 가능!
     *    → 엄청 오래 걸리는 작업을 잠시 끊는 경우
     *    → 원하는 타이밍에 함수를 잠시 Stop/복원하는 경우
     * 2. return
     *    → 우리가 원하는 타입으로 가능 (class도 가능)
     */
    class CoroutineTest : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < 1000000; i++)
            {
                if (i % 10000 == 0)
                    yield return null;
            }
            yield return new Test() { id = 1 };
            yield return new Test() { id = 2 };
            yield return null; // 일시 정지
            yield break; // 완전 종료 일반 함수의 return
            yield return new Test() { id = 3 };
            yield return new Test() { id = 4 };
            yield return new Test() { id = 5 };
        }

        void GenerateItem()
        {
            // 아이템을 만들어준다
            // DB 저장 → DB 요청 실패한 경우 다양한 이슈 발생

            // 멈춤 → DB 성공 요청 여부 확인 → Coroutine 응용
            // 로직
        }

        // Coroutine이 아닌 시간을 계산하여 로직 구현할 경우
        // 규모가 커지면 성능 과부화 발생
        // 중앙에서 시간을 관리하는 타임 시스템 필요 → 공용 매니저
        float deltaTime = 0;
        void ExplodAfter2Seconds()
        {
            deltaTime += Time.deltaTime;
            if(deltaTime >= 4)
            {
                // 로직
            }
        }
    }
    #endregion

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        //Managers.resource.Instantiate("UI/UI_Test");

        //UI_Test popup = Managers.ui.ShowPopupUI<UI_Test>();
        //Managers.ui.ClosePopupUI(popup);


        /* Pooling Object Test
        Managers.ui.ShowSceneUI<UI_Inven>();

        for (int i = 0; i < 5; i++)
            Managers.resource.Instantiate("UnityChan");
         */

        /* Coroutine Test
        CoroutineTest test = new CoroutineTest();
        foreach (System.Object t in test)
        {
            //int value = (int)t;
            Test value = (Test)t;
            Debug.Log(value.id);
        }
         */

        co = StartCoroutine("ExplodeAfterSeconds", 4.0f);
        // StopCoroutine(co);
        StartCoroutine("CoStopExplode", 2.0f);
    }
    Coroutine co;
    IEnumerator CoStopExplode(float seconds)
    {
        Debug.Log("Stop Enter");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Stop Execute!!");
        if(co != null)
        {
            StopCoroutine(co);
            co = null;
        }
    }

    IEnumerator ExplodeAfterSeconds(float seconds)
    {
        Debug.Log("Explode Enter");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Explode Execute!!");
        co = null;
    }

    public override void Clear()
    {
        
    }
}
