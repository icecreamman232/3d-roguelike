using JustGame.Scripts.ScriptableEvent;
using SGGames.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class PlayerHealthBarUI : MonoBehaviour
    {
        [SerializeField] private PlayerHealthChangedEvent m_playerHealthChangedEvent;
        [SerializeField] private Image m_healthBar;
        [SerializeField] private TextMeshProUGUI m_healthText;

        private void Start()
        {
            m_playerHealthChangedEvent.AddListener(OnPlayerHealthChanged);
        }
        
        private void OnDestroy()
        {
            m_playerHealthChangedEvent.RemoveListener(OnPlayerHealthChanged);
        }
        
        private void OnPlayerHealthChanged(float curValue,float maxValue)
        {
            m_healthBar.fillAmount = MathHelpers.Remap(curValue,0,maxValue,0,1);
            m_healthText.text = $"{curValue}/{maxValue}";
        }
    }
}

