using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GumbaCtrl : MonoBehaviour, IDamageable
{
    public enum State { TRACE, ATTACK, DIE}
    public State enemyState;
    public float attackRate;

    private NavMeshAgent _navMeshAgent;
    private int health = 2;
    private bool isDie;
    private float ws = 1.0f;
    private Transform target;
    private Animator _animator;
    private float nextAttack;
    private int hashMove = Animator.StringToHash("Move");
    private int hashAttack = Animator.StringToHash("Attack");
    private int hashGetHit = Animator.StringToHash("GetHit");
    private int hashIsDie = Animator.StringToHash("IsDie");

    private void OnEnable()
    {
        //GameManager.Instance.OnPlyaerDie += PlayerDie;
    }
    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        SetDestination();
        StartCoroutine(CheckState());
        StartCoroutine(Action());
    }

    private void SetDestination()
    {
        //target = GameManager.Instance.destination;
        _navMeshAgent.destination = target.position;
    }

    private IEnumerator CheckState()
    {
        yield return new WaitForSeconds(1.0f);
        while (!isDie)
        {
            if (enemyState == State.DIE) yield break;
            float dist = Vector3.Distance(target.position, transform.position);

            if (dist <= 1.0f)
            {
                enemyState = State.ATTACK;
            }
            yield return ws;
        }
    }

    private IEnumerator Action()
    {
        while (!isDie)
        {
            yield return ws;
            switch (enemyState)
            {
                case State.TRACE:
                    _animator.SetBool(hashMove, true);
                    break;
                case State.ATTACK:
                    if(Time.time > nextAttack)
                    {
                        _animator.SetBool(hashMove, false);
                        _animator.SetTrigger(hashAttack);
                        //GameManager.Instance.Damage();
                        nextAttack = Time.time + attackRate;
                    }
                    break;
                case State.DIE:
                    _animator.SetTrigger(hashIsDie);
                    isDie = true;
                    break;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
        if (collision.gameObject.GetComponent<ToDamageEnemy>() != null)
        {
            _animator.SetTrigger(hashGetHit);
            if ((--health) == 0)
            {
                enemyState = State.DIE;
            }
        }
        else
        {
            return;
        }
    }

    private void PlayerDie()
    {
        StopAllCoroutines();
    }
}
