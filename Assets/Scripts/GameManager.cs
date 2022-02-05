using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DemoCommon;

public class GameManager : DemoBase
{
    [Header("Enemy properties")]
    public float enemySpeed = 0.0f;
    public int enemyAmount = 0;

    [Header("Player properties")]
    public float mySpeed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_EnemySpeed = enemySpeed;
        m_EnemyAmount = enemyAmount;
        m_MySpeed = mySpeed;
    }

}
