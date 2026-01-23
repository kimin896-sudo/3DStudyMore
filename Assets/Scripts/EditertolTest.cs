using UnityEngine;

public class EditertolTest : MonoBehaviour
{
    void Start()
    {

        PlayerPrefs.SetString("name", "ȫ�浿");
        string name = PlayerPrefs.GetString("name");

        PlayerPrefs.SetFloat("EXP", 33324f);
        float exp = PlayerPrefs.GetFloat("EXP");

        PlayerPrefs.SetInt("atk", 15454);
        float atk = PlayerPrefs.GetFloat("atk");

        PlayerPrefs.Save();



    }

    void Update()
    {

    }
}
