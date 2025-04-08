using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour //фиксированная камера на объект
{
 
    [SerializeField] private Transform m_Target; //ссылка на того за кем будет следить камера

    [SerializeField] private float m_InterpolationLinear; //плавная скорость слежки камеры

    [SerializeField] private float m_InterpolationAngualar; // плавная поворачивания слежки камеры

    [SerializeField] private float m_CameraZOffset; //смещение по оси z

    [SerializeField] private float m_ForwardOffset; // смещение по напрвалению

    private void FixedUpdate() //проверка на состояние, каждый кадр
    {
        if (m_Target == null) return; //если игрок поигрывает, то выходим из метода

        //логика слежения
        Vector2 camPos = transform.position; //позиция самой камеры
        Vector2 targetPos = m_Target.position + m_Target.transform.up * m_ForwardOffset;//вычесление целевой позиции(точка это корабль)

        Vector2 newCamPos = Vector2.Lerp(camPos, targetPos, m_InterpolationLinear * Time.deltaTime); //новая позиция камеры

        transform.position = new Vector3(newCamPos.x, newCamPos.y, m_CameraZOffset); //задать позицию камеры

        if (m_InterpolationAngualar > 0) //проверки скорости поворота
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,m_Target.rotation, m_InterpolationAngualar * Time.deltaTime); //плавный поворот камеры
        }
    }

    public void SetTarget(Transform newTarget) //позволяет задать новый трансформ для слежения
    {
        m_Target = newTarget;
    }
}
