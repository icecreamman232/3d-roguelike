using JustGame.Scripts.ScriptableEvent;
using SGGames.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class AbilitySlotUI : MonoBehaviour
    {
        [SerializeField] private AbilityCooldownChangedEvent m_abilityCooldownChangedEvent;
        [SerializeField] private Image m_cooldownProgressBar;
        [SerializeField] private TextMeshProUGUI m_cooldownText;

        private void Start()
        {
            m_abilityCooldownChangedEvent.AddListener(UpdateCooldownProgressBar);
        }

        private void OnDestroy()
        {
            m_abilityCooldownChangedEvent.RemoveListener(UpdateCooldownProgressBar);
        }

        private void UpdateCooldownProgressBar(float current,float max)
        {
            m_cooldownProgressBar.fillAmount = MathHelpers.Remap(current,0,max,0,1);
            m_cooldownText.text = current.ToString("0.0");
        }
    }
}
