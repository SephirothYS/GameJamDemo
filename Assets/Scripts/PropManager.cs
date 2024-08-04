using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    private int beginPropNum = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetBeginPropNum()
    {
        return beginPropNum;
    }

    public void SetBeginPropNum(int num)
    {
        beginPropNum = num;
    }
}
