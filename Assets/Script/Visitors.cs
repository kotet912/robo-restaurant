using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visitors : MonoBehaviour
{
    [SerializeField] private float _speedVisitor; // Скорость посетителя
    [SerializeField] private int _minOrders; // Минимальное количество заказов
    [SerializeField] private int _maxOrders; // Максимальное количество заказов
    [SerializeField] private Transform _tableTransform; // Трансформ стола
    [SerializeField] private Color _orderColor = Color.red; // Цвет для обозначения наличия заказа
    [SerializeField] private Color _defaultColor = Color.white; // Цвет по умолчанию

    private int _ordersNumberVisitor; // Количество заказов у посетителя
    private Vector3 _targetPosition;
    private bool _hasReachedTable = false; // Булева переменная для отслеживания достижения стола
    private SpriteRenderer _spriteRenderer; // Ссылка на компонент SpriteRenderer

    void Start()
    {
        // Инициализация количества заказов случайным образом в заданном диапазоне
        _ordersNumberVisitor = Random.Range(_minOrders, _maxOrders + 1);

        // Получаем компонент SpriteRenderer
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // Проверка и установка цели
        if (_tableTransform != null)
        {
            _targetPosition = _tableTransform.position;
        }
        else
        {
            Debug.LogError("Table Transform is not assigned in the inspector.");
        }

        // Устанавливаем цвет в зависимости от наличия заказа
        UpdateVisitorAppearance();
    }

    void Update()
    {
        // Двигаем посетителя в сторону стола, если он еще не достиг его
        if (!_hasReachedTable)
        {
            MoveTowardsTable();
        }
    }

    private void MoveTowardsTable()
    {
        if (_tableTransform != null)
        {
            // Рассчитываем направление и перемещаем объект
            Vector3 direction = (_targetPosition - transform.position).normalized;
            transform.position += direction * _speedVisitor * Time.deltaTime;

            // Проверяем, достиг ли объект стола
            if (Vector3.Distance(transform.position, _targetPosition) < 0.5f)
            {
                Debug.Log("Visitor has reached the table.");
                _hasReachedTable = true; // Устанавливаем флаг, что посетитель достиг стола
            }
        }
    }

    private void UpdateVisitorAppearance()
    {
        // Если у посетителя есть заказы, меняем цвет на красный
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
