using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visitors : MonoBehaviour
{
    [SerializeField] private float _speedVisitor; // �������� ����������
    [SerializeField] private int _minOrders; // ����������� ���������� �������
    [SerializeField] private int _maxOrders; // ������������ ���������� �������
    [SerializeField] private Transform _tableTransform; // ��������� �����
    [SerializeField] private Color _orderColor = Color.red; // ���� ��� ����������� ������� ������
    [SerializeField] private Color _defaultColor = Color.white; // ���� �� ���������

    private int _ordersNumberVisitor; // ���������� ������� � ����������
    private Vector3 _targetPosition;
    private bool _hasReachedTable = false; // ������ ���������� ��� ������������ ���������� �����
    private SpriteRenderer _spriteRenderer; // ������ �� ��������� SpriteRenderer

    void Start()
    {
        // ������������� ���������� ������� ��������� ������� � �������� ���������
        _ordersNumberVisitor = Random.Range(_minOrders, _maxOrders + 1);

        // �������� ��������� SpriteRenderer
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // �������� � ��������� ����
        if (_tableTransform != null)
        {
            _targetPosition = _tableTransform.position;
        }
        else
        {
            Debug.LogError("Table Transform is not assigned in the inspector.");
        }

        // ������������� ���� � ����������� �� ������� ������
        UpdateVisitorAppearance();
    }

    void Update()
    {
        // ������� ���������� � ������� �����, ���� �� ��� �� ������ ���
        if (!_hasReachedTable)
        {
            MoveTowardsTable();
        }
    }

    private void MoveTowardsTable()
    {
        if (_tableTransform != null)
        {
            // ������������ ����������� � ���������� ������
            Vector3 direction = (_targetPosition - transform.position).normalized;
            transform.position += direction * _speedVisitor * Time.deltaTime;

            // ���������, ������ �� ������ �����
            if (Vector3.Distance(transform.position, _targetPosition) < 0.5f)
            {
                Debug.Log("Visitor has reached the table.");
                _hasReachedTable = true; // ������������� ����, ��� ���������� ������ �����
            }
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
}
