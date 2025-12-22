using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float lastOffsetY;
    public GameObject spawnObject;

    void Start()
    {
        StartCoroutine(startPush());
    }

    void Update()
    {
        if(BirdController.isDead)
        {
            CancelInvoke("Spawn");
        }
    }

    //这里使用协程
    //是因为要等待isStart变为true后，开启重复生成障碍物，所以需要一直监测isStart
    //但是又不能放在周期函数里，因为InvokeRepeating只能执行一次
    //所以用这个协程，在Start()就开启此协程，此后直到isStart = true，才执行InvokeRepeating
    IEnumerator startPush()
    {
        yield return new WaitUntil(() => UIManager.isStart);

        InvokeRepeating("Spawn",0f,2f);
    }
    //此外InvokeRepeating相当于启动了一个重复执行Spawn方法的Invoke延时调用
    //关闭协程是不顶用的，得关闭这个Invoke：CancelInvoke("Spawn")

    private void Spawn()
    {
        float offsetY = Random.Range(-2.41f,2.18f);

        while(Mathf.Abs(lastOffsetY - offsetY) > 3 || Mathf.Abs(lastOffsetY - offsetY) < 1)
        {
            offsetY = Random.Range(-2.41f,2.18f);
        }

        lastOffsetY = offsetY;

        Vector3 position = new Vector3(transform.position.x,transform.position.y + offsetY,0);

        Instantiate(spawnObject,position,Quaternion.identity,transform);
    }
}
