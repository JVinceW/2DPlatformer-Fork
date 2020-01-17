using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class TransportPoint : MonoBehaviour
{
    [Serializable]
    public class Animations
    {
        public List<Sprite> sprites;
        [HideInInspector]
        public List<Sprite> renderSprites;
        public TextAsset Config;
        public float RenderSpeed;
    }

    public Animations anim;
    float m_fcurrentTime = 0;
    int indexFrame = 0;
    SpriteRenderer render;

    XmlDocument Xmldoc;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        InitPivot(anim);
    }

    // Update is called once per frame
    void Update()
    {
        render.sprite = anim.renderSprites[indexFrame];
        if (m_fcurrentTime > anim.RenderSpeed)
        {
            indexFrame++;
            indexFrame = indexFrame % anim.renderSprites.Count;
            m_fcurrentTime = 0;
        }
        else
        {
            m_fcurrentTime += Time.deltaTime;
        }
    }
    void InitPivot(Animations anim)
    {
        //Load XML
        Xmldoc = new XmlDocument();
        Xmldoc.LoadXml(anim.Config.ToString());
        for (int i = 0; i < anim.sprites.Count; i++)
        {
            int positionIndex = Convert.ToInt32(anim.sprites[i].name);
            //Logs.LogD("X = " + GetValueFromAttribute(Xmldoc, positionIndex, "pivotX") + " Y=" + GetValueFromAttribute(Xmldoc, positionIndex, "pivotY"));

            //Get Pivot
            float PivotX = GetValueFromAttribute(Xmldoc, positionIndex, "pivotX");
            float PivotY = GetValueFromAttribute(Xmldoc, positionIndex, "pivotY");
            //Get width/height
            float width = GetValueFromAttribute(Xmldoc, positionIndex, "width");
            float height = GetValueFromAttribute(Xmldoc, positionIndex, "height");

            Sprite mySprite = Sprite.Create(anim.sprites[i].texture, new Rect(0, 0, width, height), new Vector2(PivotX, PivotY));
            if (mySprite != null)
            {
                anim.renderSprites.Add(mySprite);
            }
            else
            {
                Logs.LogE("Can not make sprite");
            }
        }
        anim.sprites.Clear();
    }
    float GetValueFromAttribute(XmlDocument document, int position, string attribute)
    {
        XmlNodeList nos = document.SelectNodes("root");
        XmlElement element = (XmlElement)nos.Item(0);
        return float.Parse(element.GetElementsByTagName("Sprite")[position].Attributes[attribute].Value); ;
    }
}
