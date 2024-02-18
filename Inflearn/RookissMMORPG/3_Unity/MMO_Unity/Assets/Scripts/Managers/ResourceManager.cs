using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if(typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);

            GameObject go = Managers.pool.GetOniginal(name);

            if (go != null)
                return go as T;
        }

        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        /* Pooling Object 적용 상황 
         * 1. original 이미 들고 있으면 바로 사용
         * 2. 혹시 Pooling된 얘가 있는지 확인
         */
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if(original == null)
        {
            Debug.LogError($"Failed to load prefab : {path}");
            return null;
        }

        if (original.GetComponent<Poolable>() != null)
            return Managers.pool.Pop(original, parent).gameObject;

        GameObject go = Object.Instantiate(original, parent);
        /*
        int index = go.name.IndexOf("(Clone)");
        if (index > 0)
            go.name = go.name.Substring(0, index);
        */
        go.name = original.name;

        return go;
    }

    public void Destroy(GameObject go, float t = 0.5f)
    {
        if (go == null)
            return;

        /* Pooling Object 적용 상황 
         * 만약 Pooling이 필요한 아이라면 PoolManager에서 처리
         */
        Poolable poolable = go.GetComponent<Poolable>();
        if(poolable != null)
        {
            Managers.pool.Push(poolable);
            return;
        }

        Object.Destroy(go, t);
    }
}
