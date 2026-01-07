using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> objects = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();

    // 리스트와 유사 
    // [1],[2],[3],[4]

    // 딕셔너리
    // [key , value] [key , value] [key , value]
    /*    enum Buttons
        {
            PointButton
        }
        enum Texts
        {
            ScoreText,
        }
        enum GameObjets
        {
            Test,
        }*/
    // 랩핑 함수
    public Text GetText(int index) 
    {
        return Get<Text>(index); 
    }
    // GetText(int index) <- 텍스트를 찾는 함수
    public TMP_Text GetTMP_Text(int index)
    {
        return Get<TMP_Text>(index);
    }

    // GetButton(int index) <- 버튼를 찾는 함수
   public  Button GetButtin(int index)
    {
        return Get<Button>(index);
    }
    // 랩핑 함수

    // GetImage(int index) <- 이미지를 찾는 함수
    public Image GetImage(int index)
    {
        return Get<Image>(index);
    }
    // GetGameObject(int index) <- 게임오브젝트를 찾는 함수
   public GameObject GetGameObject(int index)
    {
        return Get<GameObject>(index);
    }


    public void Bind<T>(Type type) where T : UnityEngine.Object// 컴포넌트를 물고있으려고 만드는거임
    {
        string[] names = Enum.GetNames(type); // enum이 들고있는 모든 엘리먼트에 대한 이름을 배열로 가져오기 


        UnityEngine.Object[] objectsArray = new UnityEngine.Object[names.Length];// 컴포넌트들을 저장하기 위한 배열공간 할당 
        objects.Add(typeof(T), objectsArray); // 키 = class == 타입, 벨류 = 컴퍼넌트가 담겨있는 배열 현재는 빈공간 

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
            {
                objectsArray[i] = Util.FindChild(gameObject, names[i], true);
            }
            else
            {
                /*  objectsArray[i] = null; // 여기에다가 사용할 함수를 Utill이라는 클래스를 만들어서 넣을 예정
                  Util.FindChild<T>(gameObject, names[i],true);*/
                //objectsArray[i] = Util.FindChild<T>(gameObject, names[i], true);
                objectsArray[i] = gameObject.FindChild<T>(names[i], true);
            }

        }
    }


    public T Get<T>(int index) where T : UnityEngine.Object
    {

        UnityEngine.Object[] objectsArray = null;

        // 선택한 타입이 있으면 넣어준다,
        if (objects.TryGetValue(typeof(T), out objectsArray) == false)
        {
            return null;
        }
        else
        {
            return objectsArray[index] as T; // T형식으로 변환해서 내보내 주세요
        }
    }

    public static void AddUIEvent(GameObject go,Action<PointerEventData> action,Define.UIEvent type = Define.UIEvent.Click)// 개체가 필요없는 정적인 함수 
    {
        UI_EventHandler ev = Util.GetOrAddComponent<UI_EventHandler>(go);
        switch (type)
        {
            case Define.UIEvent.Click:
                ev.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                ev.OnDragHandler += action;
                break;
            default:
                break;
        }
        //ev.OnDragHandler += ((PointerEventData data) => { go.transform.position = data.position; });
    }
}
