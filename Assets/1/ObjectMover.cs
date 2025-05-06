using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.Events;

public class ObjectMover : MonoBehaviour
{
    [Header("punkty")]
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private Transform movingObject;
    [SerializeField] private float moveSpeed = 2.0f;

    [Header("przycisk")]
    [SerializeField] private GameObject buttonObject;

    [SerializeField] private XRSimpleInteractable xrButton;
    [SerializeField] private bool useCollisionDetection = false;
    [SerializeField] private bool useRaycastDetection = false;
    [SerializeField] private KeyCode testKey = KeyCode.Space;

    public UnityEvent onButtonPressed;

    private Vector3 startPosition;
    private bool isMoving = false;
    private int currentWaypointIndex = 0;

    private void Start()
    {
        if (movingObject != null)
        {
            startPosition = movingObject.position;
        }

     if (onButtonPressed == null)
        {
            onButtonPressed = new UnityEvent();
        }

        onButtonPressed.AddListener(StartMovingObject);

        if (xrButton != null)
        {
            xrButton.selectEntered.AddListener(OnXRButtonPressed);
            Debug.Log("wykryto przycisk xr");
        }

        if (waypoints.Length == 0)
        {
            Debug.Log("nie przypisano punktow");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(testKey))
        {
            Debug.Log("testKey pressed");
            onButtonPressed.Invoke();
        }

        if (useRaycastDetection && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == buttonObject)
            {
                Debug.Log("kliknieto przycisk za pomoca raycast");
                onButtonPressed.Invoke();
            }
        }
    }

    private void OnXRButtonPressed(SelectEnterEventArgs args)
    {
        Debug.Log("wcisnieto przycisk xr");
        onButtonPressed.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (useCollisionDetection && other.CompareTag("VRController") || other.CompareTag("Player"))
        {
            Debug.Log("kontroller vr dotknal przycisku");
            onButtonPressed.Invoke();
        }
    }

    public void PressButton()
    {
        Debug.Log("wywolano PressButton");
        onButtonPressed.Invoke();
    }

    private void StartMovingObject()
    {
        if (!isMoving && waypoints.Length > 0)
        {
            Debug.Log("start ruchu objectu");
            StartCoroutine(MoveObjectThroughWaypoints());
        }
    }

    private IEnumerator MoveObjectThroughWaypoints()
    {
        isMoving = true;
        currentWaypointIndex = 0;

        Debug.Log("start coroutine ruchu obiektu");

        while (currentWaypointIndex < waypoints.Length)
        {
            Debug.Log("ruch do punktu " + currentWaypointIndex);

            while (Vector3.Distance(movingObject.position, waypoints[currentWaypointIndex].position) > 0.1f)
            {
                Vector3 direction = (waypoints[currentWaypointIndex].position - movingObject.position).normalized;
                movingObject.position += direction * moveSpeed * Time.deltaTime;

                yield return null;
            }

            Debug.Log("osiagnieto punkt " + currentWaypointIndex);
            currentWaypointIndex++;

            yield return new WaitForSeconds(0.1f);
        }

        Debug.Log("ukonczono wsystkie punkty, powrot na start");

        movingObject.position = startPosition;
        isMoving = false;
    }
}
