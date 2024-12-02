using JustGame.Scripts.ScriptableEvent;
using UnityEngine;

public class LevelUpFXPlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_fx;
    [SerializeField] private IntEvent m_levelUpEvent;

    private void Start()
    {
        m_levelUpEvent.AddListener(OnLevelUp);
    }
    
    private void OnLevelUp(int currentLv)
    {
        //Will not player fx on first init level
        if (currentLv == 1) return;
        m_fx.gameObject.SetActive(true);
        m_fx.Play();
    }
    
    private void OnDestroy()
    {
        m_levelUpEvent.RemoveListener(OnLevelUp);
    }
}
