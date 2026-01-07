using UnityEngine;

public class Monster : MonoBehaviour
{
    public AudioClip clip;
    // ´ê¾ÒÀ» °æ¿ì 
    private void OnTriggerEnter(Collider other)
    {
        AudioSource audio =  GetComponent<AudioSource>();
        audio.PlayOneShot(clip);
        Loger.Log("´ê´Â´Ù,");
    }
}
