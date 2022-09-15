using UnityEngine;

[RequireComponent(typeof(Animator), typeof(BoxCollider))]  
public class Money : MonoBehaviour
{
    [SerializeField] private int _amountCoins;
    [SerializeField] private GameObject _particleAfterSelection;

    private Animator _animator;
    private float _rotation = 0;

    #region Mono
    private void OnValidate()
    {
        if (_amountCoins < 0)
            _amountCoins *= -1;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    #endregion

    #region CallBacks

    private void Update()
    {
        if (_rotation >= 360)
            _rotation = 0;
        else
            _rotation += Time.deltaTime * 100;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, _rotation, 0), Time.deltaTime);
    }

    #endregion

    #region Public Method

    public void SelectionCoins()
    {
        Bank.AddCoins(_amountCoins);
        _animator.enabled = true;
        _animator.SetBool("DisableCoin", true);
    }

    #endregion

    #region Private Method
    //used at the end of the animation
    private void DestroyMoney()
    {
        Vector3 vector = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        Instantiate(_particleAfterSelection, vector, Quaternion.identity);
        Destroy(this.gameObject);
    }
    #endregion
}
