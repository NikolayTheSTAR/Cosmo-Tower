using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BattleWaves
{
    private BattlePhaseType battlePhaseType;

    private const string BattleConfigPath = "Configs/BattleConfig";

    private BattleConfig _battleConfig;
    public BattleConfig BattleConfig
    {
        get
        {
            if (_battleConfig == null) _battleConfig = Resources.Load<BattleConfig>(BattleConfigPath);
            return _battleConfig;
        }
    }

    private int _waveIndex = -1;
    private int _counterLTID = -1;
    private bool _isSimulate = false;

    public BattleWaves(List<IWaveReactable> waveReactables)
    {
        _waveReactables = waveReactables;
    }

    private List<IWaveReactable> _waveReactables;

    public void StartSimulate()
    {
        if (_isSimulate) return;

        _isSimulate = true;

        StartPhase(BattlePhaseType.Attack);
    }

    public void StopSimulate()
    {
        if (!_isSimulate) return;

        _isSimulate = false;

        if (_counterLTID != -1)
        {
            LeanTween.cancel(_counterLTID);
            _counterLTID = -1;
        }

        _waveIndex = -1;
    }

    private void StartPhase(BattlePhaseType phaseType)
    {
        battlePhaseType = phaseType;

        float phaseTime;
        if (battlePhaseType == BattlePhaseType.Attack)
        {
            _waveIndex++;
            phaseTime = BattleConfig.WavesData.AttackPhaseTime;
        }
        else phaseTime = BattleConfig.WavesData.RestPhaseTime;

        foreach (var wr in _waveReactables) wr.OnStartWave(_waveIndex, battlePhaseType);

        _counterLTID =
        LeanTween.value(0, 1, phaseTime)
        .setOnUpdate(value =>
        {
            foreach (var wr in _waveReactables) wr.OnSetWaveProgress(value);
        })
        .setOnComplete(() =>
        {
            StartPhase(phaseType == BattlePhaseType.Attack ? BattlePhaseType.Rest : BattlePhaseType.Attack);
        }).id;
    }
}

public enum BattlePhaseType
{
    Attack,
    Rest
}

public interface IWaveReactable
{
    void OnSetWaveProgress(float progress);
    void OnStartWave(int waveIndex, BattlePhaseType battlePhaseType);
}