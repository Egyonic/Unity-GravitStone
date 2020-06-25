using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//道具类
[System.Serializable]
public class Item : System.Object
{
    public int id;
    public string name;
    public bool status; //是否启用
    public Sprite sprite;   // sprite资源

   
}
