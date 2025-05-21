using UnityEngine;

[CreateAssetMenu(fileName = "UserData", menuName = "SocialPost/User Data", order = 1)]
public class UserData : ScriptableObject
{
    public string postId;          // Add this!
    public string username;
    public Sprite profilePic;
    public Sprite contactImage;
    public string contactText;
}
