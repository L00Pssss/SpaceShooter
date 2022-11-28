using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class EntitySpawner : MonoBehaviour
    {
        public enum SpawnMode
        {
            Start,
            Loop
        }
        // ������ �������� ��������
        [SerializeField] private Entity[] m_EntityPrefabs;
        // ���� ��� ����� ����, ������ � ������.
        [SerializeField] private CircleArea m_Area;
        // ����� ��� �������� ��� ������ � ��� ��� ������������.
        [SerializeField] private SpawnMode m_SpawnMode;
        // ���������� 
        [SerializeField] private int m_NumSpawns;
        // ��� �����, ����� �������.
        [SerializeField] private float m_RespwnTime;

        private float m_Timer;

        private void Start()
        {
            if (m_SpawnMode == SpawnMode.Start)
            {
                SpawnEntities();
            }

            m_Timer = m_RespwnTime;
        }
        private void Update()
        {
            if (m_Timer > 0)
                m_Timer -= Time.deltaTime;

            if (m_SpawnMode == SpawnMode.Loop && m_Timer < 0)
            {
                m_Timer = m_RespwnTime;

                SpawnEntities();
            }
        }

        private void SpawnEntities()
        {
            for (int i = 0; i < m_NumSpawns; i++)
            {
                // ��������� �� ������ ������.
                int index = Random.Range(0, m_EntityPrefabs.Length);
                // ������� ������� 
                GameObject entitis = Instantiate(m_EntityPrefabs[index].gameObject);

                entitis.transform.position = m_Area.GetRandomInsideZone();
            }
        }
    }
}