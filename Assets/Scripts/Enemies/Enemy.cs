using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public float hp = 100;
    public GameObject healthSlider;
    protected Slider slider;
    
     public void Start()
    {
        GetComponentInChildren<Canvas>().worldCamera = Camera.main;
        slider = healthSlider.GetComponent<Slider>();
        slider.maxValue = hp;
        slider.value = hp;

        if (slider == null)
        {
            Debug.LogError("No se ha asignado un Slider en el Inspector");
        }

    }

    //FUNCIONES

    public void FollowPlayer (Transform target, float speed, float stopDistance)
    {
        if (target != null)
        {
       
            float distanceToTarget = Vector3.Distance (transform.position, target.position);

      
            if (distanceToTarget > stopDistance)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
        }
    }







}
