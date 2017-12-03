#include <Wire.h>

int relayOne = 2;
int relayTwo = 5;
int relayThree = 8;
int relayFour = 11;
int relayFive = 22;
int relaySix = 25;
int relaySeven = 28;
int relayEight = 31;
int relayNine = 34;
int relayTen = 37;
int relayEleven = 40;
int relayTwelve = 43;

String data = "";
char charBuf[4];

void setup() 
{
  Serial.begin(9600);
  Wire.begin(0x40);
  Wire.onReceive(receiveEvent);

  pinMode(relayOne, OUTPUT);
  pinMode(relayTwo, OUTPUT);
  pinMode(relayThree, OUTPUT);
  pinMode(relayFour, OUTPUT);
  pinMode(relayFive, OUTPUT);
  pinMode(relaySix, OUTPUT);
  pinMode(relaySeven, OUTPUT);
  pinMode(relayEight, OUTPUT);
  pinMode(relayNine, OUTPUT);
  pinMode(relayTen, OUTPUT);
  pinMode(relayEleven, OUTPUT);
  pinMode(relayTwelve, OUTPUT);
}

void loop() 
{
}

void receiveEvent(int howMany)
{
  data = "";
  while( Wire.available())
  {
    data += (char)Wire.read();
  }
  char* charArray = data.c_str();
  strcpy(charBuf, charArray);
  handleLightFromValue(charBuf[0], charBuf[1], charBuf[3]);
}

void handleLightFromValue(char lightPosition, char lightSecondPosition, char lightValue)
{
  String stringValue = String(lightPosition) + String(lightSecondPosition);
  int finalValue = stringValue.toInt();
  
  switch (finalValue)
  {
    case 1:
    Serial.println("Baño abajo interior");
      HandleLight(lightValue, relayOne);
      break;
    case 2:
    Serial.println("Baño arriba interior");
      HandleLight(lightValue, relayTwo);
      break;
    case 3:
    Serial.println("Sala");
      HandleLight(lightValue, relayThree);
      break;
    case 4:
    Serial.println("Oscar");
      HandleLight(lightValue, relayFour);
      break;
    case 5:
    Serial.println("Baño arriba exterior");
      HandleLight(lightValue, relayFive);
      break;
    case 6:
    Serial.println("Baño abajo exterior");
      HandleLight(lightValue, relaySix);
      break;
    case 7:
    Serial.println("Cuarto cosas");
      HandleLight(lightValue, relaySeven);
      break;
    case 8:
    Serial.println("led ocho");
      HandleLight(lightValue, relayEight);
      break;
    case 9:
    Serial.println("led nueve");
      HandleLight(lightValue, relayNine);
      break;
    case 10:
    Serial.println("Taller");
      HandleLight(lightValue, relayTen);
      break;
    case 11:
    Serial.println("Polli");
      HandleLight(lightValue, relayEleven);
      break;
    case 12:
    Serial.println("Patio");
      HandleLight(lightValue, relayTwelve);
      break;
    case 13:
      TriggerAlarm();
      break;
    default:
    break;
  }
}

void HandleLight(int lightStatus, int relayNumber)
{
  if(lightStatus == '0')
  {
    digitalWrite(relayNumber, LOW);
  }
  else if(lightStatus == '1')
  {
    digitalWrite(relayNumber, HIGH);
  }
}

void TriggerAlarm()
{
  for(int i = 0; i < 8; i++ )
  {
    digitalWrite(relayOne, HIGH);
    digitalWrite(relayTwo, HIGH);
    digitalWrite(relayThree, HIGH);
    digitalWrite(relayFour, HIGH);
    digitalWrite(relayFive, HIGH);
    digitalWrite(relaySix, HIGH);
    digitalWrite(relaySeven, HIGH);
    digitalWrite(relayEight, HIGH);
    digitalWrite(relayNine, HIGH);
    digitalWrite(relayTen, HIGH);
    digitalWrite(relayEleven, HIGH);
    digitalWrite(relayTwelve, HIGH);
    delay(500);
    digitalWrite(relayOne, LOW);
    digitalWrite(relayTwo, LOW);
    digitalWrite(relayThree, LOW);
    digitalWrite(relayFour, LOW);
    digitalWrite(relayFive, LOW);
    digitalWrite(relaySix, LOW);
    digitalWrite(relaySeven, LOW);
    digitalWrite(relayEight, LOW);
    digitalWrite(relayNine, LOW);
    digitalWrite(relayTen, LOW);
    digitalWrite(relayEleven, LOW);
    digitalWrite(relayTwelve, LOW);
    delay(500);
  }
}
