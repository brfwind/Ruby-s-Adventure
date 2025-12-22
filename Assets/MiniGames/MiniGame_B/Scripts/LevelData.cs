using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "MyGame/Level Data")]
public class LevelData : ScriptableObject
{
    public string levelName;     // 关卡名
    public int levelIndex;       //关卡数下标
    public float timeLimit;      // 通关时间限制
    public float midTime;
    public float longTime;       //0引用是因为都用else省略了）
    public float bestTime;       // 玩家历史最佳时间
}
