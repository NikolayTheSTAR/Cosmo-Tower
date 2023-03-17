using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Data/Battle", fileName = "BattleConfig")]
public class BattleConfig : ScriptableObject
{
    [SerializeField] private BattleWavesData wavesData = new();

    public BattleWavesData WavesData => wavesData;
}

[Serializable]
public class BattleWavesData
{
    [LabelText("attackPhaseTime (s)")]
    [SerializeField] private float attackPhaseTime;

    [LabelText("restPhaseTime (s)")]
    [SerializeField] private float restPhaseTime;

    [SerializeField] private BattleWaveData[] waveDatas = new BattleWaveData[0];

    public float AttackPhaseTime => attackPhaseTime;
    public float RestPhaseTime => restPhaseTime;
    public BattleWaveData[] WaveDatas => waveDatas;
}

[Serializable]
public class BattleWaveData
{
    [SerializeField] private float enemyForce;
    [SerializeField] private float enemyHp;

    public float EnemyForce => enemyForce;
    public float EnemyHp => enemyHp;
}