using UnityEngine;

public class MessageReceiver : MonoBehaviour
{
    public void ReceiveMessage(string message) {
        print("Message received: " + message);
    }
}
