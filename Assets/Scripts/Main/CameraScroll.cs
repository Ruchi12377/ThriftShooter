using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float endPosX;
    private Transform _transform;
    public bool stop;
    
    private void Start()
    {
        _transform = transform;
    }
    private void Update()
    {
        //ボタンが押されてないときだけスクロール
        if (Input.GetButton("Fire2") == false && stop == false)
        {
            Scroll();
        }
    }

    private void Scroll()
    {
        var pos = _transform.position;
        pos.x += speed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, 0, endPosX);

        _transform.position = pos;
    }
}
