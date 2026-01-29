using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerPrefab;

    void Start()
    {
        if (PlayerPrefab == null)
            return;

        var playerStarts = FindObjectsByType<PlayerStart>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        if (playerStarts.Length <= 0)
        {
            return;
        }

        var randomStart = playerStarts[Random.Range(0, playerStarts.Length - 1)];
        Instantiate(PlayerPrefab, randomStart.GetStartTransform().position, randomStart.transform.rotation);
    }
}
