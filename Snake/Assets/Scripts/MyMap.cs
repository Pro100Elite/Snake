using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyMap : MonoBehaviour
{
    public Sprite[] img;
    public int indexImg = 0;

    int currentIndex = 0;

    void Update()
    {
        ChangeImg();
    }

    void ChangeImg()
    {
        if (currentIndex != indexImg)
        {
            int listsize = img.Length;

            if (listsize > indexImg)
            {
                currentIndex = indexImg;
            }
        }
        SpriteRenderer S = gameObject.GetComponent<SpriteRenderer>();
        S.sprite = img[indexImg];
    }

}
