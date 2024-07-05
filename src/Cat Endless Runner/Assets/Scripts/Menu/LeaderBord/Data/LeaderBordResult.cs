using System;
using UnityEngine;

namespace Menu.LeaderBord.Data
{
    [Serializable]
    public class LeaderBordResult
    {
        [SerializeField] public long Date;
        [SerializeField] public int Points;
    }
}
