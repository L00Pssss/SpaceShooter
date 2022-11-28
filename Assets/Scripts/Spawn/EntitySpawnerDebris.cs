using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class EntitySpawnerDebris : MonoBehaviour
    {

        [SerializeField] private Destructible[] m_DebrisPrefabs;
        //����������
        [SerializeField] private CircleArea m_Area;

        // ���������� ������
        [SerializeField] private int m_NumDebris;
        // ������� ���������.
        [SerializeField] private float m_RandomSpeed;

        private void Start()
        {
            for (int i = 0; i < m_NumDebris; i++)
            {
                SpawnDebris();
            }
        }

        private void SpawnDebris()
        {
            // ��������� �� ������ ������.
            int index = Random.Range(0, m_DebrisPrefabs.Length);
            // ������� ������� 
            GameObject debris = Instantiate(m_DebrisPrefabs[index].gameObject);

            debris.transform.position = m_Area.GetRandomInsideZone();
            // ��� ����������� ������� ��������� ����� EventOnDeath � ������� ���� ��� ���������� ���������� OnDebrisDead
            debris.GetComponent<Destructible>().EventOnDeath.AddListener(OnDebrisDead);

            Rigidbody2D rigidbody = debris.GetComponent<Rigidbody2D>();

            if (rigidbody != null && m_RandomSpeed > 0)
            {
                rigidbody.velocity = (Vector2) UnityEngine.Random.insideUnitSphere * m_RandomSpeed;
            }

        }

        private void OnDebrisDead()
        {
            SpawnDebris();
        }


        //private void Start()
        //{
        //    player = GameObject.FindGameObjectWithTag("Enem").transform;
        //    target = new Vector2(player.position.x, player.position.y);
        //}
        //private void Update()
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        //    if (transform.position.x == target.x && transform.position.y == target.y)
        //    {
        //        DestroyProjectile();
        //    }
        //}
        //private void OnTriggerEnter2D(Collider2D other)
        //{
        //    if (other.CompareTag("Player"))
        //    {
        //        DestroyProjectile();
        //    }
        //}
        //private void DestroyProjectile()
        //{
        //    Destroy(gameObject);
        //}








    }
}
