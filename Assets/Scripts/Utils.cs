using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Utils
{

    public static Transform Canvas { get => GameObject.FindGameObjectWithTag("Canvas").transform; }
    /// <summary>
    /// 鼠标是否在ui1上
    /// </summary>
    public static bool IsMouseOverUI()
    {
        return ScreenPointGameObjects(Input.mousePosition).Count > 0;
    }


    /// <summary>
    /// 得到屏幕某点的ui物体
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static List<GameObject> ScreenPointGameObjects(Vector2 pos)
    {
        List<GameObject> gameObjects = new List<GameObject>();

        if (EventSystem.current != null)
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = pos;
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            results.ForEach(item => gameObjects.Add(item.gameObject));
        }

        return gameObjects;
    }
}
