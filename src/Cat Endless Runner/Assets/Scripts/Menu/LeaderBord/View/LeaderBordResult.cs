using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Menu.LeaderBord.View
{
    public class LeaderBordResult : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _pointsText;
        [SerializeField] private TextMeshProUGUI _dateText;

        public void SetPointsText(string text)
        {
            _pointsText.text = $"distance: {text}";
        }
        
        public void SetDateText(string text)
        {
            _dateText.text = $"date: {text}";
        }
    }
}