using System;
using System.Xml;
using UnityEngine;

public class IAnimations
{
    public static void InitPivot(ref BaseEnemy.Animations anim, bool isfloatRight)
    {
        //Load XML
        XmlDocument Xmldoc = new XmlDocument();
        Xmldoc.LoadXml(anim.Config.ToString());
        for (int i = 0; i < anim.sprites.Count; i++)
        {
            int positionIndex = Convert.ToInt32(anim.sprites[i].name);
            //Logs.LogD("X = " + GetValueFromAttribute(Xmldoc, positionIndex, "pivotX") + " Y=" + GetValueFromAttribute(Xmldoc, positionIndex, "pivotY"));

            //Get Pivot
            float PivotX;
            if (isfloatRight)
            {
                PivotX = 1 - GetValueFromAttribute(Xmldoc, positionIndex, "pivotX");
            }
            else
            {
                PivotX = GetValueFromAttribute(Xmldoc, positionIndex, "pivotX");
            }
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
    static float GetValueFromAttribute(XmlDocument document, int position, string attribute)
    {
        XmlNodeList nos = document.SelectNodes("root");
        XmlElement element = (XmlElement)nos.Item(0);
        return float.Parse(element.GetElementsByTagName("Sprite")[position].Attributes[attribute].Value); ;
    }
}
