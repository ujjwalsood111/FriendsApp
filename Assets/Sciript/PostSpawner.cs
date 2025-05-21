using UnityEngine;

public class PostSpawner : MonoBehaviour
{
    public GameObject postPrefab;        // The post prefab with PostUI script
    public Transform parentContainer;    // The GameObject with Vertical Layout Group
    public UserData[] userPool;          // List of user ScriptableObjects

    void Start()
    {
        int count = Mathf.Min(userPool.Length, 20);  // or any number you want

        Shuffle(userPool);

        for (int i = 0; i < count; i++)
        {
            GameObject post = Instantiate(postPrefab, parentContainer);
            PostUI postUI = post.GetComponent<PostUI>();
            postUI.SetUser(userPool[i]);

            // Animate post scale in with delay for cascading effect
            post.transform.localScale = Vector3.zero;
            LeanTween.scale(post, Vector3.one, 0.3f)
                .setEaseOutBack()
                .setDelay(i * 0.05f);
        }
    }

    void Shuffle(UserData[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            UserData temp = array[i];
            int rand = Random.Range(i, array.Length);
            array[i] = array[rand];
            array[rand] = temp;
        }
    }
}
