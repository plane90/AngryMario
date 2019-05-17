using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : Block
{
    public Material hitBlockMaterial;

    private Rigidbody _rigidbody;
    private Collider _collider;
    private Renderer _renderer;
    private bool isReact = false;

    private void OnEnable()
    {
        Brick_power.OnHitPower += PowerHitReaction;
    }

    private void OnDisable()
    {
        Brick_power.OnHitPower -= PowerHitReaction;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();

        _rigidbody.isKinematic = true;
        //GameManager.Instance.IncreaseBlockCount();
    }

    /// <summary>
    /// OnCollisoinEnter블록의 Rigidbody컴포넌트의 속성을 참조합니다.
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ToCrashWithBlock>() != null && !isReact && other.GetComponent<ToCrashWithBlock>().isFromMachine)
        {
            StartCoroutine(HitReaction());
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// OnTriggerEnter가 호출되면 코인 생성, 머티리얼 교체 그리고 사라짐을 처리합니다.
    /// </summary>
    /// <returns></returns>
    private IEnumerator HitReaction()
    {
        Brick_power.OnHitPower -= PowerHitReaction;
        isReact = true;
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _collider.isTrigger = false;
        yield return new WaitForSeconds(0.2f);
        Instantiate(coin, transform.position, Quaternion.identity);
        _renderer.sharedMaterial = hitBlockMaterial;
        GameManager.Instance.AddCoin();
        yield return new WaitForSeconds(5.0f);
        _collider.isTrigger = true;
        yield return new WaitForSeconds(3.0f);
        GameManager.Instance.DecreaseBlockCount();
        Destroy(this.gameObject);
    }

    private void PowerHitReaction()
    {
        StartCoroutine(PowerHitReactionAction());
    }

    private IEnumerator PowerHitReactionAction()
    {
        isReact = true;
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _collider.isTrigger = false;
        _rigidbody.AddExplosionForce(200.0f, transform.position + (Vector3.one * Random.Range(1.0f, 3.0f)), 10.0f);
        yield return new WaitForSeconds(0.2f);
        Instantiate(coin, transform.position, Quaternion.identity);
        _renderer.sharedMaterial = hitBlockMaterial;
        GameManager.Instance.AddCoin();
        yield return new WaitForSeconds(5.0f);
        _collider.isTrigger = true;
        yield return new WaitForSeconds(3.0f);
        GameManager.Instance.DecreaseBlockCount();
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
