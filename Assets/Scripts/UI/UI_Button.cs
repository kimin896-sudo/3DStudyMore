using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Base
{
  //  Dictionary<Type, UnityEngine.Object[]> objects = new Dictionary<Type, UnityEngine.Object[]>();
    TMP_Text text;
    Button button;
    // 리스트와 유사 
    // [1],[2],[3],[4]

    // 딕셔너리
    // [key , value] [key , value] [key , value]
    enum Buttons
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
    }
    enum Images
    {
        ItemIcon,
    }
    private void Start()
    {
        Bind<Button>(typeof(Buttons)); // 어딘가에 타입을 가져온뒤 그 안에 속한 이름으로 객체를 찾아서 저장해두겠다
        Bind<TMP_Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjets));
        Bind<Image>(typeof(Images));


        text = GetTMP_Text((int)Texts.ScoreText);
        //text = Get<TMP_Text>((int)Texts.ScoreText);
        button = GetButtin((int)Buttons.PointButton);
        //button = Get<Button>((int)Buttons.PointButton);

        //--------------------------------------------------------------------------------
        /*   GameObject go = GetImage((int)Images.ItemIcon).gameObject;
           UI_EventHandler ev = go.GetComponent<UI_EventHandler>();
           ev.OnDragHandler += ((PointerEventData data) => { go.transform.position = data.position; });
   */

        GetButtin((int)Buttons.PointButton).gameObject.AddUIEvent(OnButtonClicked);

        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        AddUIEvent(go, (PointerEventData data) => { go.transform.position = data.position; },Define.UIEvent.Drag);
        //--------------------------------------------------------------------------------
        // button.onClick.AddListener(OnButtonClicked);
        GetGameObject((int)GameObjets.Test);
        // GameObject go =  Get<GameObject>((int)GameObjets.Test);
    }

    // GetText(int index) <- 텍스트를 찾는 함수
/*    TMP_Text GetTMP_Text(int index)
    {
        return Get<TMP_Text>(index);
    }*/

    // GetButton(int index) <- 버튼를 찾는 함수
/*    Button GetButtin(int index)
    {
        return Get<Button>(index);
    }*/
    // 랩핑 함수

/*    // GetImage(int index) <- 이미지를 찾는 함수
    UnityEngine.UI.Image GetImage(int index)
    {
        return Get<UnityEngine.UI.Image>(index);
    }
    // GetGameObject(int index) <- 게임오브젝트를 찾는 함수
    GameObject GetGameObject(int index)
    {
        return Get<GameObject>(index);
    }*/

/*
    void Bind<T>(Type type) where T : UnityEngine.Object// 컴포넌트를 물고있으려고 만드는거임
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
                *//*  objectsArray[i] = null; // 여기에다가 사용할 함수를 Utill이라는 클래스를 만들어서 넣을 예정
                  Util.FindChild<T>(gameObject, names[i],true);*//*
                //objectsArray[i] = Util.FindChild<T>(gameObject, names[i], true);
                objectsArray[i] = gameObject.FindChild<T>(names[i], true);
            }

        }
    }*/
/*    T Get<T>(int index) where T : UnityEngine.Object
    {

        UnityEngine.Object[] objectsArray = null;

        if (objects.TryGetValue(typeof(T), out objectsArray) == false)
        {
            return null;
        }
        else
        {
            return objectsArray[index] as T; // T형식으로 변환해서 내보내 주세요
        }
    }*/
    int score = 0;
    void OnButtonClicked(PointerEventData data)
    {
        score++;
        text.text = $"Score : {score}";
    }
}
