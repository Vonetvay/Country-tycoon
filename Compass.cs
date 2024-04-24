using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{

    private Transform player;
    private Transform _target;

    [SerializeField] private RectTransform arrow; // изображение UI, указатель цели, дочерний объект фона
    [SerializeField] private RectTransform compassBG; // изображение UI, фон компаса, в пределах которого будет двигаться указатель

    [SerializeField] private Color arrowIn = Color.white;
    [SerializeField] private Color arrowOut = Color.gray;

    private Color barColor;

    private float minSize;
    private float maxSize;

    public static Compass Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        arrow.anchoredPosition = new Vector2(0, 0);
        maxSize = arrow.sizeDelta.x;
        minSize = maxSize / 2;

        player = VehicleController.Instance.transform;
        barColor = compassBG.GetComponent<Image>().color;
        compassBG.GetComponent<Image>().color = new Color(barColor.r, barColor.g, barColor.b, 0);
        arrow.GetComponent<Image>().color = new Color(arrowIn.r, arrowIn.g, arrowIn.b, 0);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        arrow.gameObject.SetActive(true);
        if (target != null)
        {
            StartCoroutine(targetSetAnimation());
        }
        else
        {
            StartCoroutine(targetRemoveAnimation());
        }
    }


    void LateUpdate()
    {
        if (_target == null) { return; }

        float posX = Camera.main.WorldToScreenPoint(_target.position).x; // находим позицию цели в пространстве экрана, по оси Х
        float center = Screen.width / 2; // определяем центр экрана

        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);
        Vector3 toOther = _target.position - Camera.main.transform.position;
        if (Vector3.Dot(forward, toOther) < 0) posX = 0; // если цель позади нас - позиция равна нулю

        float minPos = center - compassBG.sizeDelta.x / 2;
        float maxPos = center + compassBG.sizeDelta.x / 2;
        posX = Mathf.Clamp(posX, minPos, maxPos); // фиксируем позицию цели в приделах бэкграунда компаса

        posX = center - posX; // корректируем позицию, относительно центра
        arrow.anchoredPosition = new Vector2(-posX, 0); // инвертируем

        Color tmp = Color.Lerp(arrowIn, arrowOut, Mathf.Abs(posX) / (compassBG.sizeDelta.x / 2));
        arrow.GetComponent<Image>().color = tmp; // переключаем цвета, значения от 0 до 1, центр экрана = 0

        // определяем размер указателя, относительно расстояния до цели
        float dis = Vector3.Distance(player.position, _target.position);
        float size = maxSize - dis / 4;
        size = Mathf.Clamp(size, minSize, maxSize);
        arrow.sizeDelta = new Vector2(size, arrow.sizeDelta.y);
    }


    private IEnumerator targetSetAnimation()
    {
        for (float t = 0; t < 1f; t += Time.deltaTime / 2.5f)
        {
            compassBG.GetComponent<Image>().color = Color.Lerp(new Color(barColor.r, barColor.g, barColor.b, 0), new Color(barColor.r, barColor.g, barColor.b, barColor.a), t);
            arrow.GetComponent<Image>().color = Color.Lerp(new Color(arrowIn.r, arrowIn.g, arrowIn.b, 0), new Color(arrowIn.r, arrowIn.g, arrowIn.b, arrowIn.a), t);
            yield return null;
        }
    }

    private IEnumerator targetRemoveAnimation()
    {
        for (float t = 0; t < 1f; t += Time.deltaTime / 2.5f)
        {
            compassBG.GetComponent<Image>().color = Color.Lerp(new Color(barColor.r, barColor.g, barColor.b, barColor.a), new Color(barColor.r, barColor.g, barColor.b, 0), t);
            arrow.GetComponent<Image>().color = Color.Lerp(new Color(arrowIn.r, arrowIn.g, arrowIn.b, arrowIn.a), new Color(arrowIn.r, arrowIn.g, arrowIn.b, 0), t);
            yield return null;
        }
    }
}
