using UnityEngine;
using System.Collections.Generic;

public class FrogTongue : MonoBehaviour
{
    public Transform tongueTip; // Attach an empty GameObject at the tip of the tongue
    public LineRenderer tongueRenderer; // LineRenderer component for visualizing the tongue
    public float extendSpeed = 5f; // Speed at which the tongue extends
    public float retractSpeed = 20f; // Speed at which the tongue retracts
    public float maxTongueLength = 10f; // Maximum length the tongue can extend
    public float positionThreshold = 0.1f; // Minimum distance between two stored positions

    private Vector3 targetPosition; // Position towards which the tongue extends
    private bool isExtending; // Flag to control if the tongue is extending
    private bool isRetracting; // Flag to control if the tongue is retracting
    private List<Vector3> tonguePathPositions; // List to store positions along the tongue's path

    void Awake()
    {
        // Ensure tongueTip is assigned and find it if necessary
        if (tongueTip == null)
        {
            tongueTip = transform.Find("tongueTip");
            if (tongueTip == null)
            {
                Debug.LogError("Tongue Tip is not assigned or found!");
                return;
            }
        }

        // Ensure LineRenderer is assigned
        if (tongueRenderer == null)
        {
            tongueRenderer = tongueTip.GetComponent<LineRenderer>();
            if (tongueRenderer == null)
            {
                Debug.LogError("LineRenderer component is missing on tongueTip!");
                return;
            }
        }

        tongueRenderer.enabled = false;
        tonguePathPositions = new List<Vector3>();
    }

    void Update()
    {
        // Handle tongue extension and retraction
        if (isExtending)
        {
            ExtendTongue();
        }
        else if (isRetracting)
        {
            RetractTongue();
        }
        // Example: Change direction based on player input
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangeDirection(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangeDirection(Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeDirection(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeDirection(Vector3.right);
        }
    }

    public void StartExtendingTongue()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().setfrogsClicable(false);
        // Reset the path positions
        tonguePathPositions.Clear();

        // Set initial positions of LineRenderer
        tongueRenderer.positionCount = 2;
        tongueRenderer.SetPosition(0, transform.position);
        tongueRenderer.SetPosition(1, tongueTip.position);
        tongueRenderer.enabled = true;

        // Calculate target position based on maximum tongue length
        targetPosition = transform.position + -transform.right * maxTongueLength;
        isExtending = true;
        isRetracting = false;
    }

    void ExtendTongue()
    {
        Debug.DrawRay(transform.position, -transform.right * maxTongueLength, Color.red, 0.1f);
        float step = extendSpeed * Time.deltaTime;
        Vector3 currentPos = tongueTip.position;

        // Move tongue tip towards target position
        tongueTip.position = Vector3.MoveTowards(currentPos, targetPosition, step);

        // Store current position in the path if it has moved significantly
        if (tonguePathPositions.Count == 0 || Vector3.Distance(tonguePathPositions[tonguePathPositions.Count - 1], tongueTip.position) > positionThreshold)
        {
            tonguePathPositions.Add(tongueTip.position);
        }

        // Update LineRenderer positions to visualize extension
        if (tongueRenderer.positionCount < tonguePathPositions.Count)
        {
            tongueRenderer.positionCount = tonguePathPositions.Count;
        }
        tongueRenderer.SetPosition(tongueRenderer.positionCount - 1, tongueTip.position);

        // Check if tongue reached maximum length
        if (Vector3.Distance(transform.position, tongueTip.position) >= maxTongueLength)
        {
            isExtending = false;
            isRetracting = true;
        }
    }
    public void startRetracting()
    {
        isExtending = false;
        isRetracting = true;
    }
    public void RetractTongue()
    {
        float step = retractSpeed * Time.deltaTime;

        if (tonguePathPositions.Count > 0)
        {
            Vector3 currentPos = tongueTip.position;
            Vector3 nextPos = tonguePathPositions[tonguePathPositions.Count - 1];

            // Move tongue tip towards the last stored position
            tongueTip.position = Vector3.MoveTowards(currentPos, nextPos, step);

            // Update LineRenderer positions to visualize retraction
            tongueRenderer.SetPosition(tongueRenderer.positionCount - 1, tongueTip.position);

            // Remove the last stored position if reached
            if (Vector3.Distance(currentPos, nextPos) < 0.001f)
            {
                tonguePathPositions.RemoveAt(tonguePathPositions.Count - 1);
                tongueRenderer.positionCount--;
            }
        }
        else
        {
            // Once all positions are retracted, disable the retraction
            isRetracting = false;
            tongueRenderer.enabled = false;
        }
        Invoke("setFrogsClickable", 2f);
    }
    //void setFrogsClickable()
    //{
    //    GameObject.Find("GameManager").GetComponent<GameManager>().setfrogsClicable(true);
    //}

    public void ChangeDirection(Vector3 newDirection)
    {
        // Add new segment point to LineRenderer
        tongueRenderer.positionCount++;
        tongueRenderer.SetPosition(tongueRenderer.positionCount - 1, tongueTip.position);

        // Store the current position before changing direction if it has moved significantly
        if (tonguePathPositions.Count == 0 || Vector3.Distance(tonguePathPositions[tonguePathPositions.Count - 1], tongueTip.position) > positionThreshold)
        {
            tonguePathPositions.Add(tongueTip.position);
        }

        // Set new target position based on the new direction
        targetPosition = tongueTip.position + newDirection.normalized * maxTongueLength;
        isExtending = true;
        isRetracting = false;
    }
}
