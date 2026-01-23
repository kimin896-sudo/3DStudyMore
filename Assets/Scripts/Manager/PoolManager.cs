using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PoolManager
{
    #region Pool 
    class Pool // 자식개체 최상위 배열에 속한 하위 배열 
    {
        public GameObject Original { get; private set; } // 풀이 들고 있는 원본 프리팹

        public Transform Root { get; set; }  // 각자의 오브젝트 루트  

        Stack<Poolable> _poolStack = new Stack<Poolable>();

        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for (int i = 0; i < count; i++)
            {
                Push(Create());
            }
        }

        Poolable Create()
        {
            // 여기서 ResourceManager.Instantiate를 사용하지 않은 이유는 
            // 리소스 매니저에서 풀매니저에 오브젝트를 사용하는데 
            // 풀매니저에 오브젝트가 없으면 또 매니저에서 받아오기를 실행 도돌이표가 발생 무한루프
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();

        }

        public void Push(Poolable poolable) // 풀에 넣는다.
        {
            if (poolable == null)
            {
                return;
            }

            poolable.transform.parent = Root; // 생성을 시키면 외부에 다 빠져있지만 푸쉬를 누르면 자식개체로 이동함
            poolable.gameObject.SetActive(false);
            poolable.IsUsing = false;

            _poolStack.Push(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (_poolStack.Count > 0)
                poolable = _poolStack.Pop();
            else // 아무것도 없다면 생성을 시켜줘야함
            {
                poolable = Create();
            }

            if (parent == null) // DontDestroyOnLoad 해제를 위해서 씬 오브젝트에 자식으로 붙이기
                poolable.transform.parent = Managers.Scene.CurentScene.transform;

            poolable.gameObject.SetActive(true);

            poolable.transform.parent = parent; // null
            poolable.IsUsing = true;

            return poolable;
        }
    }
    #endregion 

    // @Pool_Root
    //  ㄴ> Player_Root << 뭔가를 꺼낼 예정
    //      ㄴ> Player
    //      ㄴ> Player
    //      ㄴ> Player
    //      ㄴ> Player
    //  ㄴ> Monster_Root
    //      ㄴ> Monster
    //      ㄴ> Monster
    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>(); // 최상위 배열 이란 느낌 

    Transform _root; // 가장 최상위 루트

    public void Init() //  초기화  //
    {
        if (_root == null)
        {
            //_root = new GameObject{name = "@Pool_Root"}.transform; // 같은 뜻이다.
            _root = new GameObject("@Pool_Root").transform;

            Object.DontDestroyOnLoad(_root);// 씬을 넘겨도 삭제가 안되고 남아있음 
        }
    }
    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();// 모노비헤이비어를 속받았다면 뉴 생성자가 안됨

        pool.Init(original, count);
        pool.Root.parent = _root;// pool.Root.parent 부모는 _root다

        _pool.Add(original.name, pool); // _pool에 뭔가 하나 들어감
    }
    public void Push(Poolable poolable) // 오브젝트를 풀매니저에 넣어줌
    {
        string name = poolable.gameObject.name;

        if (_pool.ContainsKey(poolable.name) == false)// 꺼낼때 없으면 
        {
            GameObject.Destroy(poolable.gameObject); // 지우기
            return;
        }
        _pool[name].Push(poolable);
    }


    public Poolable Pop(GameObject origina, Transform parent = null) // 오브젝트를 매니저에서 삭제 
    {
        if (_pool.ContainsKey(origina.name) == false) // 만약 딕셔너리에 origina.name key가 있나? 없다면 실행
        {
            CreatePool(origina); // 없다면 pool생성
        }
        return _pool[origina.name].Pop(parent); // 오리지날을 꺼내고 parent가 있다면 
    }

    public GameObject GetOriginal(string name) // 리소스에서 로드를 할때 원본을 찾을 때 사용 
    {
        if (_pool.ContainsKey(name) == false)
            return null; // 없다면 null

        return _pool[name].Original;
    }

    public void clear() // 오브젝트 삭제 
    {
        foreach(Transform child in _root)
        {
            GameObject.Destroy(child.gameObject);
        }

        _pool.Clear();
    }
}

