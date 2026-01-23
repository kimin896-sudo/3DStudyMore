using System;
using System.Collections.Generic;

//데이터 모델 

#region Stat
[Serializable] // 어트리뷰트 
public class Stat
{
    // json 안에 있는 개체들의 이름과 같아야함 
    public int level;
    public int hp;
    public int attack;
}

[Serializable] // 필수
public class SkillData : ILoader<int, Stat>
{
 
    public List<Stat> stats = new List<Stat>();

    public Dictionary<int, Stat> MakeDic()
    {
        Dictionary<int, Stat> dict = new Dictionary<int, Stat>();

        foreach (Stat stat in stats)
        {
            dict.Add(stat.level, stat);
        }

        return dict;
    }
}

#endregion


