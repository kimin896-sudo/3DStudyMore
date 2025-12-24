using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class UI_Button : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> objects = new Dictionary<Type, UnityEngine.Object[]>();

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
    private void Start()
    {
        Bind<Button>(typeof(Buttons)); // 어딘가에 타입을 가져온뒤 그 안에 속한 이름으로 객체를 찾아서 저장해두겠다
        Bind<Text>(typeof(Texts));
    }
    void Bind<T>(Type type) where T : UnityEngine.Object// 컴포넌트를 물고있으려고 만드는거임
    {
        string[] names = Enum.GetNames(type); // enum이 들고있는 모든 엘리먼트에 대한 이름을 배열로 가져오기 


        UnityEngine.Object[] objectsArray = new UnityEngine.Object[names.Length];// 컴포넌트들을 저장하기 위한 배열공간 할당 
        objects.Add(typeof(T), objectsArray); // 키 = class == 타입, 벨류 = 컴퍼넌트가 담겨있는 배열 현재는 빈공간 

        for(int i = 0; i < names.Length; i ++)
        {
            /*    objectsArray[i] = null; // 여기에다가 사용할 함수를 Utill이라는 클래스를 만들어서 넣을 예정
                Util.FindChild<T>(gameObject, names[i],true);*/
            objectsArray[i] = Util.FindChild<T>(gameObject, names[i], true);
        }
    }

    int score  = 0;
    void OnButtonClicked()
    {
        score++;

    }
}
