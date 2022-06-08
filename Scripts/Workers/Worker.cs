using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Data))]
[RequireComponent(typeof(Movement))]
public class Worker : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private ParticleSystem _particleSystem;

    private float _speed = 2;
    private float _raiseSpeed = 5f;

    private Data _data;
    private ValueHandler _player;
    private Transform _pointStart;
    private Transform _pointFinish;
    private Transform _junkyard;
    private Movement _movement;
    private Garbage _target;

    private List<Garbage> _garbage = new List<Garbage>();

    public Garbage Target => _target;
    public ParticleSystem ParticleSystem => _particleSystem;
    public bool _startPoint { get; private set; } = false;
    public bool _finishPoint { get; private set; } = false;

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

    private void Start()
    {
        _data = GetComponent<Data>();
        _speed = _data.GetSaveFloat(SpeedWorker, _speed);
        _raiseSpeed = _data.GetSaveFloat(RaiseSpeed, _raiseSpeed);

        _movement = GetComponent<Movement>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetDirection();
    }

    public void Init(Transform pointStart, Transform pointFinish, Transform junkyard,ValueHandler player)
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
        EnableMove();
    }

    public void SettingSpeed()
    {
        _speed += 0.2f;
        PlayerPrefs.SetFloat("SpeedWorker", _speed);
        PlayerPrefs.SetFloat("RaiseSpeed", _raiseSpeed);
    }

    private void SetDirection()
    {
        if (_startPoint == false)
        {
            _animator.SetBool(IsRun, false);
            _animator.SetBool(IsSlowRun, true);
            _movement.SetTarget(_pointStart,_speed,_raiseSpeed, this);
        }

        if(_target !=null)
        {
            Vector3 distans = transform.position - _target.transform.position;
            var diff = distans.magnitude;

            if (diff <=1.5f)
            {
                _animator.SetBool(IsSlowRun, false);
                _animator.SetBool(IsRun, true);
                _target.transform.SetParent(gameObject.transform);
                _target.Rigidbody.isKinematic = true;
                _target.transform.localPosition = new Vector3(PointX, PointY, PointZ);
                _target.transform.localRotation = new Quaternion(0f, 0f, 0f,0f);
                _movement.SetTarget(_pointFinish,_speed, _raiseSpeed, this);
            }
        }

        if (_finishPoint)
        {
            _startPoint = false;
            Cast(_junkyard.position);
        }
    }

    private void Cast(Vector3 target)
    {
        _target.transform.parent = null;
        _target.transform.DOJump(target, JumpPower, QuantityJump, Duration);
    }

    private void EnableMove()
    {
        _finishPoint = false;
        if (_startPoint)
        {
            _target = GetGarbagePosition(_garbage);
            _target.BeenSelected();
            _movement.SetTarget(_target.transform, _speed, _raiseSpeed,this);

            if (_target == null|| _target.Removed)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PointFinish pointFinish))
        {
            _finishPoint = true;
            _player.AddMoney(this);
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
