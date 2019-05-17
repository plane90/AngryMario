using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick_power : Block
{
    public Material hitBlockMaterial;
    public delegate void HitPower();
    public static event HitPower OnHitPower;

    private Rigidbody _rigidbody;
    private Collider _collider;
    private Renderer _renderer;
    private bool isReact = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();

        _rigidbody.isKinematic = true;
    }

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
    
    // OnTriggerEnter가 호출되면 코인 생성, 머티리얼 교체 그리고 사라짐을 처리합니다.
    private IEnumerator HitReaction()
    {
        isReact = true;
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _collider.isTrigger = false;
        OnHitPower();
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
}
