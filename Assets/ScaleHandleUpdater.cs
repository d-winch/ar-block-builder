using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleHandleUpdater : MonoBehaviour
{
    [SerializeField]
    List<Sprite> sprites;
    Image img;

    void Start()
    {
        img = GetComponent<Image>();
    }

    public void SetSprite(int blockId)
    {
        //Sprite GRASS = Resources.Load<Sprite>("Sprites/Grass");
        //Sprite TNT = Resources.Load<Sprite>("Sprites/TNT");
        //Texture2D TNT = Resources.Load("Assets/Demo/Sprites/TNT.png", typeof(Sprite)) as Texture2D;
        //Sprite TNT = Resources.Load<Texture2D>("Sprites/TNT") as Sprite;

        //mySprite = Sprite.Create(myTexture2D, new Rect(0.0f, 0.0f, myTexture2D.width, myTexture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
        //myImage.sprite = mySprite; // apply the new sprite to the image
        img.sprite = sprites[blockId];
    }

}
