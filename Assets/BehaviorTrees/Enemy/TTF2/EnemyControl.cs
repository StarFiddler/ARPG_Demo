using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyControl : MonoBehaviour
{
    public GameObject obj;
    public Transform patrolRoute;
    public Rigidbody _rb;
    //public List<Transform> locations;
    //private int locationIndex = 0;
    private Vector3 pos;
    private Animator ani;
    //private NavMeshAgent agent;
    private bool lockPlaneVector = false;
    // Start is called before the first frame update
    void Awake()
    {
        ani = obj.GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        pos = Vector3.zero;
        //agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(lockPlaneVector == true)
        {
            obj.transform.position = Vector3.zero;
        }
        else
        {
            obj.transform.position = pos;
        }*/
    }

    public void OnAttackEnter()
    {
        pos = _rb.velocity;
        _rb.velocity = Vector3.zero;
        print(_rb.velocity);
        //lockPlaneVector = true;
    }

    public void OnAttackExit()
    {
        _rb.velocity = pos;
        //lockPlaneVector = false;
    }
}
