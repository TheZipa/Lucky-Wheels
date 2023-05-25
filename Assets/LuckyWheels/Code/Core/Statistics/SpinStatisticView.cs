using LuckyWheels.Code.Data.Statistics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LuckyWheels.Code.Core.Statistics
{
    public class SpinStatisticView : MonoBehaviour
    {
        [SerializeField] private Image _headIcon;
        [SerializeField] private TextMeshProUGUI _questionText;
        [SerializeField] private TextMeshProUGUI _sectorResultText;
        
        public void Configure(HistoryViewData historyViewData)
        {
            _headIcon.sprite = historyViewData.HeadIcon;
            _questionText.text = historyViewData.Question;
            _sectorResultText.text = historyViewData.SectorResult;
        }

        public void Show() => gameObject.SetActive(true);
    }
}