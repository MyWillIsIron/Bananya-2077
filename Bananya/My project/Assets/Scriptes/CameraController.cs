using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z); //transform.position.z - Z самой камеры, говорим ее сохранить, а не менять, хотя можно и жоско заменить просто написав цифру
    }
}
