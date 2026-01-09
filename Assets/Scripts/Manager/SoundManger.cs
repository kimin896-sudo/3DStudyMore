using UnityEngine;

public class SoundManger
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    // 음원 재생기 - > AudioSource
    // 음원       - > AudioClip
    // 듣는       - > AudioListenr
    // 나오는 음원의 높낮이를 조절해줌 굵기 얇기       

    public void Init() // 현재로써는 start에서 Init 할 수 가 없는 상황, 앞으로도 발생할 수 있음 
    {
        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            root = new GameObject("@Sound"); // 빈 게임 오브젝트 생성
            Object.DontDestroyOnLoad(root); // 씬이 바뀌어도 파괴되지 않도록 설정

            // 
            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));

            for(int i =0; i < (int)Define.Sound.MaxCount; i ++)
            {
                GameObject go = new GameObject(soundNames[i]);
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }
            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }
    public void Play(Define.Sound type,string path, float pich = 1.0f)
    {
        if(path.Contains("Sounds/") == false) // Sounds가 포함이 안되어있다면
        {
            path = $"Sounds/{path}";
        }

        if(type == Define.Sound.Bgm)
        {
            AudioClip audioClip = Managers.Resources.Load<AudioClip>(path);
            if(audioClip == null)
            {
                Loger.Log($"AudioClip Missing!{path}");
                return;
            }
        }
        else // effect
        {
            AudioClip audioClip = Managers.Resources.Load<AudioClip>(path);
            if (audioClip == null)
            {
                Loger.Log($"AudioClip Missing!{path}");
                return;
            }

            AudioSource audioSource =  _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pich;
            audioSource.PlayOneShot(audioClip);
        }
    }
}
