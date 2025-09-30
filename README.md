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
  - Smooth mapped rotation in Unity.  

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
