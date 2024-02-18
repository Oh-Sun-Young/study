using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;

        List<GameObject> list = new List<GameObject>();

        for (int i = 0; i < 10; i++)
            list.Add(Managers.resource.Instantiate("UnityChan"));

        foreach (GameObject obj in list)
            Managers.resource.Destroy(obj);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Managers.scene.LoadScene(Define.Scene.Game);
        }
    }

    public override void Clear()
    {
        Debug.Log("Login Scene Clear!");
    }
}
