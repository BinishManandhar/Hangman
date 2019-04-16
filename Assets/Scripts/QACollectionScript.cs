using System;
using UnityEngine;

[Serializable]
public class QACollectionScript
{
    public string result { get; set; }

    public Data[] data;
}

[Serializable]
public class Data
{
    public Ranking Rank;
    public string URI;
}

[Serializable]
public class Ranking{
    public string Global;
    public string US;
}

