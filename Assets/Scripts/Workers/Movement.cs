using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
    private Transform _target;
    private Worker _worker;
    private float _speed;
    private float _raiseSpeed;
    private float _elapsedTime = 0;

    private Coroutine _coroutine = null;

    public const float Delay = 0.7f;

    private void Update()
    {
        RunTarget(_target, _speed);
    }

    public void SetTarget(Transform target,float speed,float raiseSpeed,Worker worker)
    {
        _target = target;
        _speed = speed;
        _raiseSpeed = raiseSpeed;
        _worker = worker;
    }

    public void AccelerationEmployee()
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(IncreaseSpeed());
        }
        else
        {
            StopCoroutine(_coroutine);
            StartCoroutine(IncreaseSpeed());
        }
    }

    private void RunTarget(Transform target, float speed)
    {
        if(_target == null)
        {
            return;
        }
        else
        {
            float pointX = target.position.x - transform.position.x;
            float pointZ = target.position.z - transform.position.z;
            transform.position += new Vector3(pointX, 0f, pointZ).normalized * speed * Time.deltaTime;
            Tracking();
        }
    }

    private IEnumerator IncreaseSpeed()
    {
        _worker.ParticleSystem.Play();
        _elapsedTime = 0;
       
        while(_elapsedTime <= Delay)
        {
            RunTarget(_target, _raiseSpeed);
            _elapsedTime += Time.deltaTime;
            yield return null;
        }
        _worker.ParticleSystem.Stop();
    }

    private void Tracking()
    {
        Vector3 targetDistance = _target.position - transform.position;
        Vector3 workerRotation = new Vector3(targetDistance.x, 0f, targetDistance.z);
        transform.rotation = Quaternion.LookRotation(workerRotation, Vector3.up);
    }
}
