using System;
using System.ComponentModel;
using UnityEngine;

public static class GameobjectExtensions
{

    public static T FindChild<T>(this GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
          if (go == null)
            return null;
        if(recursive == false)// 재귀적으로 탐색할지 여부 분기 처리 
        {
            // 재귀적으로 탐색하지 않는 경우 == 내 자식에 한해서만 탐색하겠다. 
            go.transform.GetChild(0);// ui자식들중 0번째

            for(int i = 0; i < go.transform.childCount; i++)  // go.transform.childCount 내가 가지고 있는 자식들 개수 만큼
            {
                Transform tr = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || tr.name == name)
                {
                    T component = tr.GetComponent<T>();
                    if (component != null)
                    {
                        return component;
                    }
                }
            }
        }
        else
        {
            // 재귀적으로 탐색하는 경우 == 내 자손모두를 탐색하겠다. 

            foreach(T component in go.GetComponentsInChildren<T>()) // 우리가 받은 게임오브젝트에서 GetComponentsInChildren T컴포넌트 얘 자식들을 모두 찾기 
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }
        }
        return null; // 탐색은 해봤지만 해당 컴퍼넌트가 없을경우 
    }

}

public class Util
{
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;
        return transform.gameObject;
    }
    public static T FindChild<T>(GameObject go,string name = null,bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;
        if(recursive == false)// 재귀적으로 탐색할지 여부 분기 처리 
        {
            // 재귀적으로 탐색하지 않는 경우 == 내 자식에 한해서만 탐색하겠다. 
            go.transform.GetChild(0);// ui자식들중 0번째

            for(int i = 0; i < go.transform.childCount; i++)  // go.transform.childCount 내가 가지고 있는 자식들 개수 만큼
            {
                Transform tr = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || tr.name == name)
                {
                    T component = tr.GetComponent<T>();
                    if (component != null)
                    {
                        return component;
                    }
                }
            }
        }
        else
        {
            // 재귀적으로 탐색하는 경우 == 내 자손모두를 탐색하겠다. 

            foreach(T component in go.GetComponentsInChildren<T>()) // 우리가 받은 게임오브젝트에서 GetComponentsInChildren T컴포넌트 얘 자식들을 모두 찾기 
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                {
                    return component;
                }
            }
        }
        return null; // 탐색은 해봤지만 해당 컴퍼넌트가 없을경우 
    }
}
