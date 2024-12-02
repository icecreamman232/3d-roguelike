using System;
using DG.Tweening;
using JustGame.Scripts.ScriptableEvent;
using SGGames.Scripts.Data;
using SGGames.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class ExperienceBarUI : MonoBehaviour
    {
        [SerializeField] private Image m_experienceBar;
        [SerializeField] private TextMeshProUGUI m_levelText;
        [SerializeField] private PlayerExpChangedEvent m_playerExpChangedEvent;
        [SerializeField] private IntEvent m_levelChangedEvent;

        private void Awake()
        {
            m_playerExpChangedEvent.AddListener(OnUpdateExpBar);
            m_levelChangedEvent.AddListener(OnLevelUp);
        }

        private void OnDestroy()
        {
            m_playerExpChangedEvent.RemoveListener(OnUpdateExpBar);
            m_levelChangedEvent.RemoveListener(OnLevelUp);
        }

        private void OnLevelUp(int currentLevel)
        {
            m_levelText.text = currentLevel.ToString();
        }

        private void OnUpdateExpBar(float current,float max)
        {
            m_experienceBar.fillAmount = MathHelpers.Remap(current, 0, max, 0, 1);
        }
    }
}

