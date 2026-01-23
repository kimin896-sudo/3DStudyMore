using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DataTableManager : MonoBehaviour
{
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        LoadChapterDataTable();
    }
    private void LoadChapterDataTable()
    {
        List<ChapterData> chapterDataTable = new List<ChapterData>();
        var parseDataTable = CSVReader.Read("Data/ChapterDataTable");

      
        foreach (var row in parseDataTable)
        {
            ChapterData data  = new ChapterData();

            data.chapterNo = (int)row["chapter_no"];
            data.totalStages = (int)row["total_stages"];
            data.chapterReward_gem = (int)row["chapter_reward_gem"];
            data.chapterReward_gold = (int)row["chapter_reward_gold"];

            chapterDataTable.Add(data);
        }
    }
}

// µ•¿Ã≈Õ ∏µ®
public class ChapterData
{
    public int chapterNo;
    public int totalStages;
    public int chapterReward_gem;
    public int chapterReward_gold;

}
