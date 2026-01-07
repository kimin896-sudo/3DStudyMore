using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class UI_InvenItem : UI_Base
{
    Sprite _texture;
    string _name;
    bool _init = false; // 초기화 여부를 체크할 변수

    Button _button;
    enum Texts
    {
        ItemNameTextLeagcy // 오타 주의: Legacy 인지 확인해보세요!
    }
    enum Images
    {
        ItemIcon,
    }

    enum Buttons
    {
        ItemButton,
    }

    public override void Init()
    {
        if (_init) return; // ★ 이미 초기화 됐다면 여기서 함수 종료!

        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));

        Bind<Button>(typeof(Buttons));
        _button = GetButtin((int)Buttons.ItemButton);

        GetButtin((int)Buttons.ItemButton).gameObject.AddUIEvent(OnButtonClicked);

        _init = true; // 초기화 완료 표시
    }

    public void SetInfo(Sprite texture, string itemName)
    {
        Init(); // 여기서 위쪽의 if(_init)에 걸려서 두 번째부터는 통과함

        _texture = texture;
        _name = itemName;

        RefreshUI();
    }

    public void RefreshUI()
    {
        if (_init == false) return; // 초기화 안됐으면 실행 방지

        // 여기서 GetImage나 GetText가 에러난다면 
        // 하이러키 창의 오브젝트 이름과 Enum 이름이 똑같은지 다시 확인!
        Image iconImage = GetImage((int)Images.ItemIcon);
        iconImage.sprite = _texture;

        Text nameText = GetText((int)Texts.ItemNameTextLeagcy);
        if (nameText != null) nameText.text = _name;
    }

    public void OnButtonClicked(PointerEventData data)
    {
        Sprite newSprite = Managers.Resources.Load<Sprite>($"Texturs/Icon1");
        Image iconImage = GetImage((int)Images.ItemIcon);

        iconImage.sprite = newSprite;

        Loger.Log($"클릭{_name}");
    }

}