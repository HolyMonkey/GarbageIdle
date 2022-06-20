using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;

[RequireComponent(typeof(Data))]
[RequireComponent(typeof(Movement))]
public class Worker : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private TMP_Text _textMoney;

    private float _speed = 1.5f;
    private float _raiseSpeed = 3f;

    private Data _data;
    private ValueHandler _player;
    private PointStart _pointStart;
    private Transform _pointFinish;
    private Transform _junkyard;
    private Movement _movement;
    private Garbage _target;
    private float _elepsedTim = 0;
    private float _elepsedTimPoinfinish = 0;
    private bool _moneyUpStop = false;
    private bool _isDead = false;

    private const string SpeedWorker = "SpeedWorker";
    private const string RaiseSpeed = "RaiseSpeed";
    private const float JumpPower = 1f;
    private const int QuantityJump = 1;
    private const float Duration = 0.5f;
    private const string IsRun = "IsRun";
    private const string IsSlowRun = "IsSlowRun";
    private const float PointX = -0.019f;
    private const float PointY = 0.888f;
    private const float PointZ = 0.288f;

    private List<Garbage> _garbage = new List<Garbage>();

    public event UnityAction<int> AddedMoney;
    public event UnityAction<Worker> Died;

    public bool IsDead => _isDead;
    public Movement Movement => _movement;
    public Garbage Target => _target;
    public ParticleSystem ParticleSystem => _particleSystem;
    public bool _startPoint { get; private set; } = false;
    public bool _finishPoint { get; private set; } = false;

   

    private void Start()
    {
        _textMoney.alpha = 0;
        _data = GetComponent<Data>();
        _speed = _data.GetSaveFloat(SpeedWorker, _speed);
        _raiseSpeed = _data.GetSaveFloat(RaiseSpeed, _raiseSpeed);
        _movement = GetComponent<Movement>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SpecifyingGoal();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PointFinish pointFinish))
        {
            _elepsedTim = 0;
            _finishPoint = true;
            _moneyUpStop = true;

            AddedMoney?.Invoke(Target.Price);

            //_textMoney.alpha = 1;
            //_textMoney.text = "+$" + Target.Price.ToString();

            _player.AddMoney(this);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out PointFinish pointFinish))
        {
            _elepsedTimPoinfinish += Time.deltaTime;
            if(_elepsedTimPoinfinish>= 0.5)
            {
                Cast(_junkyard.position);
                _finishPoint = true;
                _movement.SetTarget(_pointStart.transform, _speed, _raiseSpeed, this);
                _elepsedTimPoinfinish = 0;
            }
        }
    }

    public void Init(PointStart pointStart, Transform pointFinish, Transform junkyard,ValueHandler player)
    {
        _pointStart = pointStart;
        _pointFinish = pointFinish;
        _junkyard = junkyard;
        _player = player;
    }

    public void ListGarbage(List<Garbage> garbage)
    {
        _garbage = garbage;
        _startPoint = true;
        SetDirection();
    }

    public void SettingSpeed()
    {
        _speed += 0.1f;
        PlayerPrefs.SetFloat("SpeedWorker", _speed);
        PlayerPrefs.SetFloat("RaiseSpeed", _raiseSpeed);
    }

    private void SpecifyingGoal()
    {

        if (_moneyUpStop)
        {
            DeactivateAccountReplenishmentAnimation();
        }

        if (_startPoint == false)
        {
            _animator.SetBool(IsRun, false);
            _animator.SetBool(IsSlowRun, true);
            _movement.SetTarget(_pointStart.transform,_speed,_raiseSpeed, this);
        }

        if(_target != null && _target.Removed == false)
        {
            DistanceGarbageCollection();
        }

        if(_finishPoint)
            Cast(_junkyard.position);
    }

    private void DistanceGarbageCollection()
    {
        Vector3 distans = transform.position - _target.transform.position;
        var diff = distans.magnitude;
        
        if (diff <= 1.8f)
        {
            _animator.SetBool(IsSlowRun, false);
            _animator.SetBool(IsRun, true);
            _target.transform.SetParent(gameObject.transform);
            _target.Rigidbody.isKinematic = true;
            _target.transform.localPosition = new Vector3(PointX, PointY, PointZ);
            _target.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
            _movement.SetTarget(_pointFinish, _speed, _raiseSpeed, this);
        }
    }

    private void Cast(Vector3 target)
    {
        _target.transform.parent = null;
        _target.transform.DOJump(target, JumpPower, QuantityJump, Duration);
        Check();
    }

    private void Check()
    {
        _startPoint = false;
    }

    private void SetDirection()
    {
        _finishPoint = false;
        if (_startPoint)
        {
            _target = GetGarbagePosition(_garbage);
            if(_target == null)
            {
                _isDead = true;
                Died?.Invoke(this);
                gameObject.SetActive(false);
            }
            else
            {
                _target.BeenSelected();
                _movement.SetTarget(_target.transform, _speed, _raiseSpeed, this);
            }
        }
    }

    private void DeactivateAccountReplenishmentAnimation()
    {
        _elepsedTim += Time.deltaTime;

        if (_elepsedTim > 0.7f)
        {
            _textMoney.alpha = 0;
            _moneyUpStop = false;
            _finishPoint = false;
        }
    }

    private Garbage GetGarbagePosition(List<Garbage> garbages)
    {
        float distance = Mathf.Infinity;
        Garbage point = null;
        Vector3 position = transform.position;
        foreach (var item in garbages)
        {
            if(item.Highlighted == false)
            {
                Vector3 diff = item.transform.position - position;
                float currentDistance = diff.sqrMagnitude;
                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    point = item;
                }

            }
        }
        return point;
    }
}
