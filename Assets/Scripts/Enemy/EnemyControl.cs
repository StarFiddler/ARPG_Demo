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

    struct Skills
    {
    public float skillTime;
    public float skillOntime;
    };

    Skills skill1;
    Skills skill2;
    Skills skill3;
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
        // skill1.skillTime = Time.time - skill1.skillOntime;
        // if(skill1.skillTime >= _sm.skillCD)
        // {
        //     ani.SetBool("canAttack1", true);
        //     skill1.skillOntime = Time.time;
        // }
        // else
        // {
        //     ani.SetBool("canAttack1", false);
        // }
        skill2.skillTime = Time.time - skill2.skillOntime;
        if(skill2.skillTime >= _sm.skillCD)
        {
            ani.SetBool("canAttack2", true);
            //skill2.skillOntime = Time.time;
        }
        else
        {
            ani.SetBool("canAttack2", false);
        }
        // skill3.skillTime = Time.time - skill3.skillOntime;
        // if(skill3.skillTime >= _sm.skillCD)
        // {
        //     ani.SetBool("canAttack3", true);
        //     skill3.skillOntime = Time.time;
        // }
        // else
        // {
        //     ani.SetBool("canAttack3", false);
        // }

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

        obj.transform.forward = new Vector3(target.transform.localPosition.x - obj.transform.position.x, 0f, target.transform.localPosition.z - obj.transform.position.z);
    }

    // public void OnAttack1Exit()
    // {
    //     skill1.skillOntime = Time.time;

    //     //lockPlaneVector = false;
    // }
    public void OnAttack2Exit()
    {
        skill2.skillOntime = Time.time;

    //     //lockPlaneVector = false;
     }
    // public void OnAttack3Exit()
    // {
    //     skill3.skillOntime = Time.time;

    //     //lockPlaneVector = false;
    // }
}
