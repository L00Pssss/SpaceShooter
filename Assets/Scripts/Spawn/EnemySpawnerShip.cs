using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class EnemySpawnerShip : MonoBehaviour
    {
        [SerializeField] private Destructible m_EnemyShips;
        //Точки
        [SerializeField] private Transform[] m_spawnPoints;


        [SerializeField] private Transform[] m_targetPoint;

        [SerializeField] AIPointPatrol m_AIPointPatorl;

        // Количество врагов
        [SerializeField] private int m_NumEnemyShips;
        // скрость появления.
        [SerializeField] private float m_RandomSpeed;



        private void Start()
        {
            for (int i = 0; i < m_NumEnemyShips; i++)
            {
                SpawnEnemy();
            }
        }
        private int TeamID = 3;
        private int SpawnPoint = 0;
        private void SpawnEnemy()
        {
            m_EnemyShips.GetComponent<AIController>().SetPatrolPoint(m_AIPointPatorl);
            
        //    m_EnemyShips[0].GetComponent<AIController>().Set

            // переменая из масива рандом.
        //   int index = Random.Range(0, m_EnemyShips.Length);
            // спавним объекты 
            GameObject EnemyShip = Instantiate(m_EnemyShips.gameObject, m_spawnPoints[SpawnPoint].position, Quaternion.identity);

            EnemyShip.GetComponent<Destructible>().TeamID = TeamID;

            TeamID += 3;
            SpawnPoint++;

            if (SpawnPoint == 4)
            {
                SpawnPoint = 0;
            }





            // при уничтажение объекта вызвается метод EventOnDeath и слушает если это происходит вызваеться OnDebrisDead
            EnemyShip.GetComponent<Destructible>().EventOnDeath.AddListener(OnDebrisDead);

            Rigidbody2D rigidbody = EnemyShip.GetComponent<Rigidbody2D>();

            if (rigidbody != null && m_RandomSpeed > 0)
            {
                rigidbody.velocity = (Vector2)UnityEngine.Random.insideUnitSphere * m_RandomSpeed;
            }

        }

        private void OnDebrisDead()
        {
            SpawnEnemy();
        }
    }
}