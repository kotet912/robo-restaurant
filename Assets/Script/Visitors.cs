using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visitors : MonoBehaviour
{
    [SerializeField] private float _speedVisitor; // �������� ����������
    [SerializeField] private int _minOrders; // ����������� ���������� �������
    [SerializeField] private int _maxOrders; // ������������ ���������� �������
    [SerializeField] private Transform[] _chairTransforms; // ������ ����������� �������
    [SerializeField] private Color _orderColor = Color.red; // ���� ��� ����������� ������� ������
    [SerializeField] private Color _defaultColor = Color.white; // ���� �� ���������

    private int _ordersNumberVisitor; // ���������� ������� � ����������
    private Vector3 _targetPosition;
    private bool _hasReachedChair = false; // ������ ���������� ��� ������������ ���������� �����
    private SpriteRenderer _spriteRenderer; // ������ �� ��������� SpriteRenderer
    private Transform _targetChair; // ������ �� ������� ����

    void Start()
    {
        // ������������� ���������� ������� ��������� ������� � �������� ���������
        _ordersNumberVisitor = Random.Range(_minOrders, _maxOrders + 1);

        // �������� ��������� SpriteRenderer
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // ����� ��������� ����
        _targetChair = FindRandomFreeChair();
        if (_targetChair != null)
        {
            _targetPosition = _targetChair.position;
        }
        else
        {
            Debug.LogError("No free chair found.");
        }

        // ������������� ���� � ����������� �� ������� ������
        UpdateVisitorAppearance();
    }

    void Update()
    {
        // ������� ���������� � ������� �����, ���� �� ��� �� ������ ���
        if (!_hasReachedChair && _targetChair != null)
        {
            MoveTowardsChair();
        }
    }

    private void MoveTowardsChair()
    {
        // ������������ ����������� � ���������� ������
        Vector3 direction = (_targetPosition - transform.position).normalized;
        transform.position += direction * _speedVisitor * Time.deltaTime;

        // ���������, ������ �� ������ �����
        if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
        {
            Debug.Log("Visitor has reached the chair.");
            _hasReachedChair = true; // ������������� ����, ��� ���������� ������ �����

            // ������ ������
        }
    }

    private void UpdateVisitorAppearance()
    {
        // ���� � ���������� ���� ������, ������ ���� �� �������
        if (_ordersNumberVisitor > 0)
        {
            _spriteRenderer.color = _orderColor;
        }
        else
        {
            _spriteRenderer.color = _defaultColor;
        }
    }

    private Transform FindRandomFreeChair()
    {
        List<Transform> freeChairs = new List<Transform>();

        // �������� ��� ��������� ������
        foreach (Transform chair in _chairTransforms)
        {
            // ��������, � ��� ���� ����� ��� ��������, ������� ���������, ����� �� ����
            Chair chairComponent = chair.GetComponent<Chair>();
            if (chairComponent != null && chairComponent.IsFree)
            {
                freeChairs.Add(chair);
            }
        }

        if (freeChairs.Count > 0)
        {
            // ���������� ��������� ��������� ����
            return freeChairs[Random.Range(0, freeChairs.Count)];
        }

        return null;
    }
}
