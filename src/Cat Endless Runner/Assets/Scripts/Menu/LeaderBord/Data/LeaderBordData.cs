using System;
using System.Collections.Generic;
using UnityEngine;

namespace Menu.LeaderBord.Data
{
    [Serializable]
    public class LeaderBordData
    {
        [SerializeField] public List<LeaderBordResult> LeaderBordResults;
    }
}