using UnityEngine;
using UnityEngine.UI;

public class PostUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Text usernameText;
    public Image profileImage;
    public Image contactImage;
    public Text contactText;
    public Button likeButton;
    public Text likeCountText;

    private bool isLiked = false;
    private int likeCount = 0;
    private UserData user;

    private string prefsKey => "liked_" + (user != null ? user.postId : "null");

    void Start()
    {
        likeButton.onClick.AddListener(ToggleLike);
    }

    public void SetUser(UserData userData)
    {
        user = userData;

        if (user == null)
        {
            Debug.LogError("UserData not assigned to PostUI.");
            return;
        }

        usernameText.text = user.username;
        profileImage.sprite = user.profilePic;
        contactImage.sprite = user.contactImage;
        contactText.text = user.contactText;

        // Animate profile and contact images fade in
        profileImage.color = new Color(1, 1, 1, 0);
        LeanTween.alpha(profileImage.rectTransform, 1f, 0.5f);

        contactImage.color = new Color(1, 1, 1, 0);
        LeanTween.alpha(contactImage.rectTransform, 1f, 0.5f);

        LoadLikedState();
        UpdateLikeUI();
    }

    void ToggleLike()
    {
        isLiked = !isLiked;
        likeCount += isLiked ? 1 : -1;
        SaveLikedState();
        UpdateLikeUI();

        // Animate like button scale for smooth pop effect
        LeanTween.cancel(likeButton.gameObject);
        likeButton.gameObject.transform.localScale = Vector3.one;

        LeanTween.scale(likeButton.gameObject, Vector3.one * 1.2f, 0.15f)
            .setEaseInOutQuad()
            .setOnComplete(() =>
            {
                LeanTween.scale(likeButton.gameObject, Vector3.one, 0.15f)
                    .setEaseInOutQuad();
            });
    }

    void LoadLikedState()
    {
        // Load liked state
        isLiked = PlayerPrefs.GetInt(prefsKey, 0) == 1;

        // Load like count or set random initial value
        likeCount = PlayerPrefs.GetInt(prefsKey + "_count", Random.Range(5, 50));
    }

    void SaveLikedState()
    {
        PlayerPrefs.SetInt(prefsKey, isLiked ? 1 : 0);
        PlayerPrefs.SetInt(prefsKey + "_count", likeCount);
        PlayerPrefs.Save();
    }

    void UpdateLikeUI()
    {
        likeCountText.text = likeCount.ToString();

        // Animate like count text scale pop on update
        LeanTween.cancel(likeCountText.gameObject);
        likeCountText.gameObject.transform.localScale = Vector3.one;

        LeanTween.scale(likeCountText.gameObject, Vector3.one * 1.3f, 0.2f)
            .setEaseInOutQuad()
            .setOnComplete(() =>
            {
                LeanTween.scale(likeCountText.gameObject, Vector3.one, 0.2f)
                    .setEaseInOutQuad();
            });

        likeButton.image.color = isLiked ? Color.blue : Color.white;
    }
}
