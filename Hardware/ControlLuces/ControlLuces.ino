int relayOne = 2, redLedLightOne = 3, greenLedLightOne = 4;
int relayTwo = 5, redLedLightTwo = 6, greenLedLightTwo = 7;
int relayThree = 8, redLedLightThree = 9, greenLedLightThree = 10;
int relayFour = 11, redLedLightFour = 12, greenLedLightFour = 13;
int relayFive = 14, redLedLightFive = 15, greenLedLightFive = 16;
int relaySix = 17, redLedLightSix = 18, greenLedLightSix = 19;
int relaySeven = 20, redLedLightSeven = 21, greenLedLightSeven = 22;
int relayEight = 23, redLedLightEight = 24, greenLedLightEight = 25;
int relayNine = 26, redLedLightNine = 27, greenLedLightNine = 28;
int relayTen = 29, redLedLightTen = 30, greenLedLightTen = 31;
int relayEleven = 32, redLedLightEleven = 33, greenLedLightEleven = 34;
int relayTwelve = 35, redLedLightTwelve = 36, greenLedLightTwelve = 37;

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
  int finalValue = stringValue.toInt();
  
  switch (finalValue)
  {
    case 1:
      ShowSpecificColor(lightValue, redLedLightOne, greenLedLightOne, relayOne);
      break;
    case 2:
      ShowSpecificColor(lightValue, redLedLightTwo, greenLedLightTwo, relayTwo);
      break;
    case 3:
      ShowSpecificColor(lightValue, redLedLightThree, greenLedLightThree, relayThree);
      break;
    case 4:
      ShowSpecificColor(lightValue, redLedLightFour, greenLedLightFour, relayFour);
      break;
    case 5:
      ShowSpecificColor(lightValue, redLedLightFive, greenLedLightFive, relayFive);
      break;
    case 6:
      ShowSpecificColor(lightValue, redLedLightSix, greenLedLightSix, relaySix);
      break;
    case 7:
      ShowSpecificColor(lightValue, redLedLightSeven, greenLedLightSeven, relaySeven);
      break;
    case 8:
      ShowSpecificColor(lightValue, redLedLightEight, greenLedLightEight, relayEight);
      break;
    case 9:
      ShowSpecificColor(lightValue, redLedLightNine, greenLedLightNine, relayNine);
      break;
    case 10:
      ShowSpecificColor(lightValue, redLedLightTen, greenLedLightTen, relayTen);
      break;
    case 11:
      ShowSpecificColor(lightValue, redLedLightEleven, greenLedLightEleven, relayEleven);
      break;
    case 12:
      ShowSpecificColor(lightValue, redLedLightTwelve, greenLedLightTwelve, relayTwelve);
      break;
    case 13:
      TriggerAlarm();
    default:
    break;
  }
}

void ShowSpecificColor(int lightStatus, int redLight, int greenLight, int relayNumber)
{
  if(lightStatus == '1')
  {
    digitalWrite(relayNumber, LOW);
    digitalWrite(redLight, HIGH);
    digitalWrite(greenLight, LOW);
  }
  else if(lightStatus == '0')
  {
    digitalWrite(relayNumber, HIGH);
    digitalWrite(redLight, LOW);
    digitalWrite(greenLight, HIGH);
  }
}

void TriggerAlarm()
{
  for(int i = 0; i < 5; i++ )
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
    delay(1000);
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
    delay(1000);
  }
}

