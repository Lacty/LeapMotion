using UnityEngine;
using System.Collections;
using Leap;


public class LeapSample : MonoBehaviour {

  Controller controller = new Controller();

  public int FingerCount;
  [SerializeField]
  private GameObject[] fingerObj;

  private GestureList gestures;

  void Start() {
    controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
  }

  void Update() {
    Frame frame = controller.Frame();
    gestures = frame.Gestures();
    FingerCount = frame.Fingers.Count;

    InteractionBox interactionBox = frame.InteractionBox;

    for (int i = 0; i < FingerCount; ++i) {
      var finger = frame.Fingers[i];
      var obj = fingerObj[i];
      Vector normPos = interactionBox.NormalizePoint(finger.TipPosition);
      normPos *= 10;
      normPos.z = -normPos.z;
      obj.transform.localPosition = new UnityEngine.Vector3(normPos.x, normPos.y, normPos.z);
    }
    ChangeColor();
  }

  void ChangeColor() {
    for (int i = 0; i < FingerCount; ++i) {
      if (gestures[i].Type == Gesture.GestureType.TYPE_CIRCLE) {
        var obj = fingerObj[i];
        obj.transform.Rotate(100, 100, 0);
      }
    }
  }
}
