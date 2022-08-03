using UnityEngine;
using DG.Tweening;
using TMPro;

public class RewardDisplay : MonoBehaviour
{
    [SerializeField] private float _step;
    [SerializeField] private float _duration;
    [SerializeField] private TMP_Text _value;

    private Tween _tweenMove;
    private Tween _tweenScale;
    private Vector3 _target;
    private float _scale = 0.04f;

    private void Start()
    {
        transform.LookAt(Camera.main.transform);
        _target = new Vector3(transform.localPosition.x, transform.localPosition.y + _step, transform.localPosition.z);
        _tweenMove = transform.DOLocalMove(_target, _duration).OnComplete(() => Hide());
        _tweenScale = transform.DOScale(_scale, _duration);
    }

    private void Hide()
    {
        transform.DORestart();
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);

        if (_tweenMove != null && _tweenScale != null)
        {
            _tweenMove.Restart();
            _tweenScale.Restart();
        }
    }

    public void SetReward(float reward) => _value.text = "$" + reward.ToString();
}
