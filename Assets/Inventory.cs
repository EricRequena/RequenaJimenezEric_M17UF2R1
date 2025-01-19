using UnityEngine;
using System.Collections;

public class InventorySelector : MonoBehaviour
{
    public RectTransform cursor;
    public RectTransform [] slots;
    public float moveDuration = 0.2f;

    void Update ()
    {
        if (Input.GetKeyDown (KeyCode.Alpha1))
        {
            StartCoroutine (MoveCursor (0));
        }
        else if (Input.GetKeyDown (KeyCode.Alpha2))
        {
            StartCoroutine (MoveCursor (1));
        }
        else if (Input.GetKeyDown (KeyCode.Alpha3))
        {
            StartCoroutine (MoveCursor (2));
        }
    }

    IEnumerator MoveCursor (int slotIndex)
    {
        if (slotIndex >= 0 && slotIndex < slots.Length)
        {
            Vector3 startPosition = cursor.position;
            Vector3 targetPosition = slots [slotIndex].position;
            float elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                cursor.position = Vector3.Lerp (startPosition, targetPosition, elapsedTime / moveDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            cursor.position = targetPosition;
        }
    }
}
