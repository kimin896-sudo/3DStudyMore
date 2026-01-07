using UnityEngine;

public class UI_Inven : UI_Scene
{
   enum Gameobjects
    {
        GridPanal,
    }
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(Gameobjects));
        GameObject gridPanal = GetGameObject((int)Gameobjects.GridPanal);

        foreach(Transform child in gridPanal.transform) // 그리드패널에 자식오브젝트 전체 삭제
        {
            Managers.Destroy(child.gameObject);
        }
        // TODO : 실제 데이터 참고해서 인벤토리 채우기
        for (int i = 0; i < 10; i++)
        {
            GameObject item = Managers.Resources.Instantiate("UI/SubItem/UI_InvenItem");
            item.transform.SetParent(gridPanal.transform); // 아이템 생성후 부모설정

            Sprite newSprite = Managers.Resources.Load<Sprite>($"Texturs/Icon2");

            // TODO : 실제 데이터 참고해서 아이템의 내용 채우기
            UI_InvenItem invenItem = item.GetComponent<UI_InvenItem>();
            if (invenItem != null)
            {
                invenItem.SetInfo(newSprite, $"집행검_{i}");
            }
        }
    }
}
