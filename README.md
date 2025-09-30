Ah, I see why you’re getting confused.
The problem isn’t the README itself — it’s my explanations *outside* the code block that make it look like some parts are “outside.”

So let’s fix this cleanly:

👉 Below is the **entire README**, from the first line to the last, inside **one single copyable block**.
No text outside. Just copy it straight into your `README.md`:

````markdown
# 🚴 Cycle Controller with Custom Handle & Unity Integration

A smart **exercise cycle controller** built using **ESP8266 + Unity3D**, featuring:  
- **Real-time speed tracking** with a magnet + reed switch (hall sensor).  
- **Custom steering handle** using a rotary encoder.  
- **Brake control** via a physical button.  
- **Unity simulation** for immersive cycling experience.  

---

## ✨ Features
- **Speed Measurement** 🚦  
  - Uses a magnet attached to the wheel + reed switch (hall sensor).  
  - Each rotation calculates speed using wheel circumference and time difference.  

- **Handle Steering** 🕹️  
  - Rotary encoder detects left/right turns.  
  - Smooth mapped rotation from **-45° to +45°** in Unity.  

- **Brake Control** 🛑  
  - Push button toggles braking.  
  - When pressed, Unity cycle instantly stops.  

- **Unity Integration** 🎮  
  - Real-time WebSocket communication between ESP8266 and Unity.  
  - Smooth visualization of speed, handle movement, and braking.  

---

## 🛠️ Hardware Requirements
- ESP8266 (NodeMCU)  
- Magnet + Hall Effect Sensor (or reed switch sensor)  
- Rotary Encoder (for handle rotation)  
- Push Button (for braking)  
- Jumper wires + breadboard  
- Exercise cycle (with custom handle setup)  

---

## 🔌 Circuit Connections
| Component       | ESP8266 Pin |
|-----------------|-------------|
| Hall Sensor     | **D1**      |
| Button (Brake)  | **D2**      |
| Encoder (Analog)| **A0**      |
| VCC / GND       | 3.3V / GND  |

📷 *See `Hardware/circuit_image.png` for the full wiring diagram.*  

---

## 💻 Software Setup
### 1. ESP8266 Code
- Open `Code/ESP8266_Cycle.ino` in Arduino IDE.  
- Update Wi-Fi credentials:  
  ```cpp
  const char* ssid = "YOUR_WIFI";
  const char* password = "YOUR_PASS";
````

* Upload to ESP8266.

### 2. Unity Code

* Import `Code/CycleController.cs` into your Unity project.
* Assign `handle` and `cycle` transforms in the Unity Inspector.
* Update WebSocket IP (your ESP8266 IP) inside the script.

---

## 🚴 How It Works

1. When the wheel rotates, the **hall sensor** detects the magnet → calculates speed.
2. Turning the **handle** changes the encoder value → mapped to steering in Unity.
3. Pressing the **brake button** sets speed to `0`.
4. ESP8266 sends all sensor data via WebSocket → Unity visualizes in real time.

---

## 🗓 Project Timeline

* **March 2025** – Project created and first working prototype built.
* **September 2025** – Uploaded to GitHub.

---

## 📂 Project Structure

```
Cycle-Controller/
│
├── Code/
│   ├── CycleController.cs     # Unity code
│   └── ESP8266_Cycle.ino      # ESP8266 firmware
│
├── Hardware/
│   └── circuit_image.png      # Circuit diagram
│
├── Media/
│   ├── photos/                # Build & setup photos
│   └── videos/                # Demo videos
│
└── README.md                  # Project documentation
```

---

## 📸 Media

### Demo Photos

![Cycle Setup](Media/photos/cycle_setup.jpg)
![Custom Handle](Media/photos/handle.jpg)

---

## 🚀 Future Improvements

* Add **RPM & distance tracking**.
* Implement **gear shifting simulation**.
* Add **Bluetooth support** for wireless mobile connectivity.
* Use **3D Unity environment** for a more immersive cycling experience.

---

## 📜 License

This project is licensed under the MIT License — free to use and modify.

```

```
