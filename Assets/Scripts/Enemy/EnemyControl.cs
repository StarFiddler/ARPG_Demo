using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyControl : MonoBehaviour
{
    public GameObject obj;
    public GameObject target;
    public Transform patrolRoute;
    public Rigidbody _rb;
    public SkillManager _sm;
    //public List<Transform> locations;
    //private int locationIndex = 0;
    private Vector3 pos;
    private Animator ani;
    //private NavMeshAgent agent;
    private bool lockPlaneVector = false;
    private float currentTime;
    private float skillTime;
    private float skillOntime;
    // Start is called before the first frame update
    void Awake()
    {
        ani = obj.GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
        pos = Vector3.zero;
        //_sm = GetComponet<SkillManager>();
        //agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        skillTime = Time.time - skillOntime;
        print(skillTime);
        if(skillTime >= _sm.skillCD)
        {
            ani.SetBool("canAttack"+"1", true);
        }
        else
        {
            ani.SetBool("canAttack"+"1", false);
        }
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
        
        //pos = _rb.velocity;
        //_rb.velocity = Vector3.zero;
        //print(_rb.velocity);
        //lockPlaneVector = true;
    }

    public void OnAttackUpdate()
    {
        currentTime = ani.GetCurrentAnimatorStateInfo(0).normalizedTime;
        if(ani.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.55f && ani.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.15f)
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, target.transform.position, 10.0f * Time.deltaTime);
        }
        
        obj.transform.forward = target.transform.localPosition - obj.transform.position;
    }

    public void OnAttackExit()
    {
        skillOntime = Time.time;
        //lockPlaneVector = false;
    }
}
