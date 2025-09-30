#include <ESP8266WiFi.h>
#include <WebSocketsServer.h>

// Wi-Fi Credentials
const char* ssid = "JioFiber-5G";            
const char* password = "11223344";           

WebSocketsServer webSocket = WebSocketsServer(81);  

// Pin Definitions
#define ENCODER_PIN A0              
#define HALL_SENSOR_PIN D1          
#define BUTTON_PIN D2               

// Timing Variables
unsigned long lastSendTime = 0;  
const unsigned int sendInterval = 10;  
unsigned long lastDebounceTime = 0;  
const unsigned int debounceDelay = 50;  

int lastButtonState = HIGH;  
int buttonState = HIGH;  

void webSocketEvent(uint8_t num, WStype_t type, uint8_t *payload, size_t length) {
  switch (type) {
    case WStype_CONNECTED:
      Serial.println("Client connected!");
      break;
    case WStype_DISCONNECTED:
      Serial.println("Client disconnected!");
      break;
  }
}

void setup() {
  Serial.begin(115200);

  WiFi.begin(ssid, password);
  Serial.print("Connecting to Wi-Fi");
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }
  Serial.println("\nConnected to Wi-Fi!");
  Serial.println("ESP8266 IP Address: " + WiFi.localIP().toString());

  pinMode(HALL_SENSOR_PIN, INPUT);
  pinMode(BUTTON_PIN, INPUT_PULLUP);  

  webSocket.begin();
  webSocket.onEvent(webSocketEvent);
}

void loop() {
  webSocket.loop();  

  int reading = digitalRead(BUTTON_PIN);

  if (reading != lastButtonState) {
    lastDebounceTime = millis();
  }

  if ((millis() - lastDebounceTime) > debounceDelay) {
    if (reading != buttonState) {
      buttonState = reading;
    }
  }

  lastButtonState = reading;

  if (millis() - lastSendTime >= sendInterval) {
    int encoderValue = analogRead(ENCODER_PIN);              
    int hallSensorValue = digitalRead(HALL_SENSOR_PIN);      

    String message = "Encoder Value: " + String(encoderValue) + 
                     ", Hall Sensor: " + String(hallSensorValue) + 
                     ", Button: " + String(buttonState);

    Serial.println(message);
    webSocket.broadcastTXT(message);  

    lastSendTime = millis();  
  }
}

