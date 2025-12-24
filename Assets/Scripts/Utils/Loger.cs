using System.Diagnostics;


// 로그를 찍을때 주로 하는거
// 현재 이 로그가 발생된 시간 
// 메세지


public class Loger // 래핑클래스를 만든다. 기능 위에 껍질을 씌운다.  
{
    [Conditional("DEV_VER")]
    public static void Log(string message)
    {
        UnityEngine.Debug.LogFormat("[{0}] , [{1}]", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), message); // 스트링포멧과 유사ㅡ


    }
    [Conditional("DEV_VER")]
    public static void LogWarning(string message)
    {
        UnityEngine.Debug.LogWarningFormat("[{0}] , [{1}]", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), message); // 스트링포멧과 유사ㅡ


    }

    [Conditional("DEV_VER")]
    public static void LogError(string message)
    {
        UnityEngine.Debug.LogErrorFormat("[{0}] , [{1}]", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), message); // 스트링포멧과 유사ㅡ
    }
}
