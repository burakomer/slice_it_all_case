using UnityEngine;

public class CuttableObject : MonoBehaviour, ICuttable
{
    [SerializeField] private GameObject _intactModel;
    [SerializeField] private GameObject _brokenModel;
    [SerializeField] private Rigidbody _brokenModelLeftRb;
    [SerializeField] private Rigidbody _brokenModelRightRb;

    private Rigidbody _intactModelRb;

    private bool _isBroken;

    private void Awake()
    {
        _intactModelRb = _intactModel.GetComponent<Rigidbody>();
        _intactModelRb.isKinematic = true;
        
        gameObject.tag = GameTags.Cuttable;
        _brokenModel.gameObject.SetActive(false);
    }

    public void Cut(Vector3 hitPosition)
    {
        if (_isBroken) return;
        _intactModel.gameObject.SetActive(false);
        _brokenModel.gameObject.SetActive(true);

        var forcePosition = transform.position;
        _brokenModelLeftRb.AddForceAtPosition(Vector3.back * 500f, forcePosition);
        _brokenModelRightRb.AddForceAtPosition(Vector3.forward * 500f, forcePosition);
        _isBroken = true;

#if !UNITY_EDITOR && UNITY_IOS
        IOSNative.StartHapticFeedback(HapticFeedbackTypes.MEDIUM);
#endif
    }
}
