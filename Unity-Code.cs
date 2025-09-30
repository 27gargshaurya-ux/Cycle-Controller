using UnityEngine;
using WebSocketSharp;
using System.Collections.Generic;

public class Cycle : MonoBehaviour
{
    private WebSocket ws;
    public Transform handle;  // Handle reference (for rotation)
    public Transform cycle;   // Reference to the cycle object
    private float rotationAngle;  // Handle rotation angle (Y-axis)
    
    private Queue<string> messageQueue = new Queue<string>();
    private float speed;
    private float circumference = 0.88f;  // Wheel circumference in meters (88 cm)
    
    private float debounceTime = 0.2f;  // Debounce interval (200 ms)
    private float lastMagnetTime = 0f;   // Last time the magnet was detected
    private float turnSpeed = 2f;        // How fast the cycle turns (adjustable)

    private bool isBraking = false;  // Tracks if the brake is applied

    void Start()
    {
        ws = new WebSocket("ws://192.168.29.169:81/");
        ws.OnMessage += (sender, e) =>
        {
            lock (messageQueue)
            {
                messageQueue.Enqueue(e.Data);  // Add incoming messages to the queue
            }
        };
        ws.Connect();
    }

    void Update()
    {
        // Process WebSocket messages from the queue
        lock (messageQueue)
        {
            while (messageQueue.Count > 0)
            {
                string message = messageQueue.Dequeue();
                ProcessSensorData(message);
            }
        }

        // Rotate handle on the Y-axis (realistic rotation)
        handle.localRotation = Quaternion.Euler(0, rotationAngle, 0);

        // If the brake is applied (button pressed), stop the cycle
        if (isBraking)
        {
            speed = 0;  // Apply brake by setting speed to zero
        }

        // Move the cycle forward and simulate turning based on handle rotation
        cycle.Translate(Vector3.forward * speed * Time.deltaTime);  // Move forward
        cycle.Rotate(0, rotationAngle * turnSpeed * Time.deltaTime, 0);  // Simulate turning

        if (Application.isPlaying)  // Log in Play mode to reduce console load
        {
            Debug.Log($"Applying Rotation: {rotationAngle} degrees");
            Debug.Log($"Speed: {speed:F2} m/s");
            Debug.Log($"Brake Applied: {isBraking}");
        }
    }

    void ProcessSensorData(string message)
    {
        if (message.Contains("Encoder Value:"))
        {
            string[] parts = message.Split(',');
            string encoderPart = parts[0].Replace("Encoder Value:", "").Trim();
            string sensorPart = parts[1].Replace("Hall Sensor:", "").Trim();
            string buttonPart = parts[2].Replace("Button:", "").Trim();  // Button data

            // Handle rotation logic
            if (int.TryParse(encoderPart, out int encoderValue))
            {
                rotationAngle = Mathf.Lerp(-45, 45, (encoderValue - 100) / 800f);  // Handle can turn between -45° and +45°
            }

            // Magnet detection logic for speed calculation
            if (int.TryParse(sensorPart, out int sensorValue))
            {
                float currentTime = Time.time;

                // Debounce logic for magnet detection
                if (sensorValue == 0 && currentTime - lastMagnetTime > debounceTime)
                {
                    Debug.Log("Magnet Detected! Calculating Speed...");

                    // Calculate speed using the time difference between magnet detections
                    speed = circumference / (currentTime - lastMagnetTime);
                    lastMagnetTime = currentTime;
                }
            }
            else if (Application.isPlaying)
            {
                Debug.LogError("Failed to parse sensor value: " + sensorPart);
            }

            // Button logic to apply or release brake
            if (int.TryParse(buttonPart, out int buttonValue))
            {
                isBraking = (buttonValue == 1);  // Apply brake if button value is 1, release if 0
            }
            else if (Application.isPlaying)
            {
                Debug.LogError("Failed to parse button value: " + buttonPart);
            }
        }
        else if (Application.isPlaying)
        {
            Debug.LogError("Received data does not contain 'Encoder Value'. Received: " + message);
        }
    }

    void OnApplicationQuit()
    {
        if (ws != null)
        {
            ws.Close();  // Close WebSocket on app quit
        }
    }
}
