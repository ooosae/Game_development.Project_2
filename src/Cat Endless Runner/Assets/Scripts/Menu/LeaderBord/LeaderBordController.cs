using System;
using Menu.LeaderBord.View;
using UnityEngine;

namespace Menu.LeaderBord
{
    public class LeaderBordController : MonoBehaviour
    {
        [SerializeField] private LeaderBordDataController _leaderBordDataController;
        [SerializeField] private LeaderBordView _leaderBordView;

        private void Awake()
        {
            UpdateLeaderBord();
        }

        private void UpdateLeaderBord()
        {
            var leaderBordData = _leaderBordDataController.GetLeaderBordData();
            _leaderBordView.DeactivateAll();
            if (leaderBordData.LeaderBordResults.Count > 3)
            {
                Debug.LogError("huh?");
            }
            
            for (int i = 0; i < leaderBordData.LeaderBordResults.Count; i++)
            {
                var date = new DateTime(leaderBordData.LeaderBordResults[i].Date);
                var value = leaderBordData.LeaderBordResults[i].Points.ToString();
                _leaderBordView.SetResultActive(i, true);
                _leaderBordView.SetResultValues(i, date.ToString(), value);
            }
        }
    }
}