using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Text itemText;   //道具名文字显示
    public static Item currentItem; //当前的物品,需要在玩家控制器那边初始化值
    private Image itemImage;

    // Start is called before the first frame update
    void Start()
    {
        itemImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        itemImage.sprite = currentItem.sprite;  //设置图像
        //根据状态值设置道具的作用对象
        string status = currentItem.status ? "自身":"环境";
        itemText.text = currentItem.name+" "+status;
    }
}
