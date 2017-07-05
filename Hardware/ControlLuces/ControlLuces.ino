int relayOne = 2, redLedLightOne = 3, greenLedLightOne = 4;
int relayTwo = 5, redLedLightTwo = 6, greenLedLightTwo = 7;
int relayThree = 8, redLedLightThree = 9, greenLedLightThree = 10;
int relayFour = 11, redLedLightFour = 12, greenLedLightFour = 13;
int relayFive = 22, redLedLightFive = 23, greenLedLightFive = 24;
int relaySix = 25, redLedLightSix = 26, greenLedLightSix = 27;
int relaySeven = 28, redLedLightSeven = 29, greenLedLightSeven = 30;
int relayEight = 31, redLedLightEight = 32, greenLedLightEight = 33;
int relayNine = 34, redLedLightNine = 35, greenLedLightNine = 36;
int relayTen = 37, redLedLightTen = 38, greenLedLightTen = 39;
int relayEleven = 40, redLedLightEleven = 41, greenLedLightEleven = 42;
int relayTwelve = 43, redLedLightTwelve = 44, greenLedLightTwelve = 45;

void setup()
{
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

  pinMode(redLedLightOne, OUTPUT);
  pinMode(redLedLightTwo, OUTPUT);
  pinMode(redLedLightThree, OUTPUT);
  pinMode(redLedLightFour, OUTPUT);
  pinMode(redLedLightFive, OUTPUT);
  pinMode(redLedLightSix, OUTPUT);
  pinMode(redLedLightSeven, OUTPUT);
  pinMode(redLedLightEight, OUTPUT);
  pinMode(redLedLightNine, OUTPUT);
  pinMode(redLedLightTen, OUTPUT);
  pinMode(redLedLightEleven, OUTPUT);
  pinMode(redLedLightTwelve, OUTPUT);

  pinMode(greenLedLightOne, OUTPUT);
  pinMode(greenLedLightTwo, OUTPUT);
  pinMode(greenLedLightThree, OUTPUT);
  pinMode(greenLedLightFour, OUTPUT);
  pinMode(greenLedLightFive, OUTPUT);
  pinMode(greenLedLightSix, OUTPUT);
  pinMode(greenLedLightSeven, OUTPUT);
  pinMode(greenLedLightEight, OUTPUT);
  pinMode(greenLedLightNine, OUTPUT);
  pinMode(greenLedLightTen, OUTPUT);
  pinMode(greenLedLightEleven, OUTPUT);
  pinMode(greenLedLightTwelve, OUTPUT);
  
  Serial.begin(9600);
}

String data = "";
char charBuf[4];

void loop()
{
  digitalWrite(redLedLightOne, HIGH);
  digitalWrite(redLedLightTwo, HIGH);
  digitalWrite(redLedLightThree, HIGH);
  digitalWrite(redLedLightFour, HIGH);
  digitalWrite(redLedLightFive, HIGH);
  digitalWrite(redLedLightSix, HIGH);
  digitalWrite(redLedLightSeven, HIGH);
  digitalWrite(redLedLightEight, HIGH);
  digitalWrite(redLedLightNine, HIGH);
  digitalWrite(redLedLightTen, HIGH);
  digitalWrite(redLedLightEleven, HIGH);
  digitalWrite(redLedLightTwelve, HIGH);
  if(Serial.available()>0)
  {
    data = Serial.readString();
    Serial.println(data);
    char* charArray = data.c_str(); 
    strcpy(charBuf, charArray);
  }
  
  handleLightFromValue(charBuf[0], charBuf[1], charBuf[3]);
  delay(1000);
}

void handleLightFromValue(char lightPosition, char lightSecondPosition, char lightValue)
{
  String stringValue = String(lightPosition) + String(lightSecondPosition);
  Serial.println(stringValue);
  int finalValue = stringValue.toInt();
  Serial.println(finalValue);
  
  switch (finalValue)
  {
    case 1:
    Serial.println("Baño abajo interior");
      ShowSpecificColor(lightValue, redLedLightOne, greenLedLightOne, relayOne);
      break;
    case 2:
    Serial.println("Baño arriba interior");
      ShowSpecificColor(lightValue, redLedLightTwo, greenLedLightTwo, relayTwo);
      break;
    case 3:
    Serial.println("Sala");
      ShowSpecificColor(lightValue, redLedLightThree, greenLedLightThree, relayThree);
      break;
    case 4:
    Serial.println("Oscar");
      ShowSpecificColor(lightValue, redLedLightFour, greenLedLightFour, relayFour);
      break;
    case 5:
    Serial.println("Baño arriba exterior");
      ShowSpecificColor(lightValue, redLedLightFive, greenLedLightFive, relayFive);
      break;
    case 6:
    Serial.println("Baño abajo exterior");
      ShowSpecificColor(lightValue, redLedLightSix, greenLedLightSix, relaySix);
      break;
    case 7:
    Serial.println("Cuarto cosas");
      ShowSpecificColor(lightValue, redLedLightSeven, greenLedLightSeven, relaySeven);
      break;
    case 8:
    Serial.println("led ocho");
      ShowSpecificColor(lightValue, redLedLightEight, greenLedLightEight, relayEight);
      break;
    case 9:
    Serial.println("led nueve");
      ShowSpecificColor(lightValue, redLedLightNine, greenLedLightNine, relayNine);
      break;
    case 10:
    Serial.println("Taller");
      ShowSpecificColor(lightValue, redLedLightTen, greenLedLightTen, relayTen);
      break;
    case 11:
    Serial.println("Polli");
      ShowSpecificColor(lightValue, redLedLightEleven, greenLedLightEleven, relayEleven);
      break;
    case 12:
    Serial.println("Patio");
      ShowSpecificColor(lightValue, redLedLightTwelve, greenLedLightTwelve, relayTwelve);
      break;
    case 13:
      TriggerAlarm();
      break;
    default:
    break;
  }
}

void ShowSpecificColor(int lightStatus, int redLight, int greenLight, int relayNumber)
{
  if(lightStatus == '0')
  {
    digitalWrite(relayNumber, LOW);
    digitalWrite(redLight, HIGH);
    digitalWrite(greenLight, LOW);
  }
  else if(lightStatus == '1')
  {
    digitalWrite(relayNumber, HIGH);
    digitalWrite(redLight, LOW);
    digitalWrite(greenLight, HIGH);
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