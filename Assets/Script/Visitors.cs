using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visitors : MonoBehaviour
{
    [SerializeField] private float _speedVisitor; // Скорость посетителя
    [SerializeField] private int _minOrders; // Минимальное количество заказов
    [SerializeField] private int _maxOrders; // Максимальное количество заказов
    [SerializeField] private Transform[] _chairTransforms; // Массив трансформов стульев
    [SerializeField] private Color _orderColor = Color.red; // Цвет для обозначения наличия заказа
    [SerializeField] private Color _defaultColor = Color.white; // Цвет по умолчанию

    private int _ordersNumberVisitor; // Количество заказов у посетителя
    private Vector3 _targetPosition;
    private bool _hasReachedChair = false; // Булева переменная для отслеживания достижения стула
    private SpriteRenderer _spriteRenderer; // Ссылка на компонент SpriteRenderer
    private Transform _targetChair; // Ссылка на целевой стул

    void Start()
    {
        // Инициализация количества заказов случайным образом в заданном диапазоне
        _ordersNumberVisitor = Random.Range(_minOrders, _maxOrders + 1);

        // Получаем компонент SpriteRenderer
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // Найти свободный стул
        _targetChair = FindRandomFreeChair();
        if (_targetChair != null)
        {
            _targetPosition = _targetChair.position;
        }
        else
        {
            Debug.LogError("No free chair found.");
        }

        // Устанавливаем цвет в зависимости от наличия заказа
        UpdateVisitorAppearance();
    }

    void Update()
    {
        // Двигаем посетителя в сторону стула, если он еще не достиг его
        if (!_hasReachedChair && _targetChair != null)
        {
            MoveTowardsChair();
        }
    }

    private void MoveTowardsChair()
    {
        // Рассчитываем направление и перемещаем объект
        Vector3 direction = (_targetPosition - transform.position).normalized;
        transform.position += direction * _speedVisitor * Time.deltaTime;

        // Проверяем, достиг ли объект стула
        if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
        {
            Debug.Log("Visitor has reached the chair.");
            _hasReachedChair = true; // Устанавливаем флаг, что посетитель достиг стула

            // логика заказа
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

    private Transform FindRandomFreeChair()
    {
        List<Transform> freeChairs = new List<Transform>();

        // Собираем все свободные стулья
        foreach (Transform chair in _chairTransforms)
        {
            // Допустим, у вас есть метод или свойство, которое проверяет, занят ли стул
            Chair chairComponent = chair.GetComponent<Chair>();
            if (chairComponent != null && chairComponent.IsFree)
            {
                freeChairs.Add(chair);
            }
        }

        if (freeChairs.Count > 0)
        {
            // Возвращаем случайный свободный стул
            return freeChairs[Random.Range(0, freeChairs.Count)];
        }

        return null;
    }
}
