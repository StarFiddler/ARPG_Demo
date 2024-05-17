using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject model;
    public Transform player;
    public Transform patrolRoute;
    public List<Transform> locations;
    private int locationIndex = 0;
    private Animator ani;
    private Rigidbody _rb;
    private float dist;//距离玩家单位的距离
    private Vector3 dirt;//距离玩家的方向
    private NavMeshAgent agent;
    private int _lives = 3;
    private float enemyWalkSpeed = 2.0f;
    private float enemyRunSpeed = 10.0f;
    private float targetRunMultiple;
    private bool findPlayer;
    public int EnemyLives
    {
        get {return _lives;}
        private set
        {
            _lives = value;
            if(_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
            }
        }
    }
    void Start()
    {
        _rb = model.GetComponentInParent<Rigidbody>();
        ani = model.GetComponent<Animator>();
        findPlayer = false;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
        GetComponent<NavMeshAgent>().speed = enemyWalkSpeed;
        //dist = Vector3.Distance(_rb.position, player.position);
    }
    void Update()
    {
        dist = Vector3.Distance(_rb.position, player.position);
        dirt = (player.position - _rb.position) / dist;
        float targetRunMultiple = (findPlayer ? 2.0f : 1.0f);
        ani.SetFloat("forward", Mathf.Lerp(ani.GetFloat("forward"), targetRunMultiple, 0.2f));
        if(findPlayer)
        {
            MoveToPlayer();
        }
        if(agent.remainingDistance < 0.2f && !agent.pathPending &&!findPlayer)
        {
            MoveToNextPatrolLocation();
        }
        /*if(findPlayer)
        {
            //_pV = enemyRunSpeed * (findPlayer ? 2.0f : 1.0f);
            
            _rb.position += dirt * enemyRunSpeed * (findPlayer ? 2.0f : 1.0f) * Time.deltaTime;
        }*/
    }
    void InitializePatrolRoute()
    {
        foreach(Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }
    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
        return;
        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }
    
    void MoveToPlayer()
    {
        if (locations.Count == 0)
        return;
        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            findPlayer = true;
            agent.destination = player.position;
            GetComponent<NavMeshAgent>().speed = enemyRunSpeed;
            Debug.Log("Player detected - attack!");
        }
    }

    // Update is called once per frame
    void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
        {
            findPlayer = false;
            GetComponent<NavMeshAgent>().speed = enemyWalkSpeed;
            Debug.Log("Player out of range, resume patrol");
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "BULLET(cLONE)")
        {
            EnemyLives -= 1;
            Debug.Log("Critial hit!");
        }
    }
}
