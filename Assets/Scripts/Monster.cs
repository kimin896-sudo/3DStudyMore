using UnityEngine;

public class Monster : MonoBehaviour
{
    public AudioClip clip;
    // ´ê¾ÒÀ» °æ¿ì 
    private void OnTriggerEnter(Collider other)
    {
        /*        AudioSource audio =  GetComponent<AudioSource>();
                audio.PlayOneShot(clip); // ºÎµúÇûÀ» ¶§ ÇÃ·¹ÀÌ 
                Loger.Log("´ê´Â´Ù,");*/

        Managers.SoundManager.Play(Define.Sound.Effect, "univ0001");
    }
}
