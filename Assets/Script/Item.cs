using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//道具类
[System.Serializable]
public class Item : System.Object
{
    public int id;
    public string name;
    //判断对自身使用还是对环境
    // true为对自己使用，false为对环境使用
    public bool status;
    public bool isUsing;    //是否正在使用
    public Sprite sprite;   // sprite资源

   
}
