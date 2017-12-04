# Proyecto para automatizar las luces de tu casa 

## EL HARDWARE

En el caso del material requerido para esta ocasión va a cambiar mucho para cada escenario. Lo siguiente es lo adecuado.

- Un módulo relay para cada luz que vas a controlar  
- Un Arduino Mega (puedes usar otros modelos, recomiendo este solo por los pines). 
- Un protoboard, entre más grande, mejor.

## La conexión

Para la configuración de este proyecto debes conectar los dispositivos de la siguiente manera.

<img src="Assets/DiagramaFritzing.jpg"/>

Sé muy bien que guiarte con una imagen de este tipo sería demasiado complejo. Por lo mismo, he subido el esquema de fritzing de manera editable para que te sea más cómodo poder modificar la visualización y puedas ver a detalle que es lo que el sistema hace.

Puedes ver el diagrama de Fritzing en esta [ruta]("https://github.com/aminespinoza/Control-casa/blob/master/Hardware/Assets/DiagramaCasa.fzz").

Recuerda que la configuración se modificará en función de la cantidad de luces que quieras tener activas.

## El código de Arduino

Dentro de la carpeta de [ControlLuces]("https://github.com/aminespinoza/Control-casa/tree/master/Hardware/ControlCasa") podrás encontrar la aplicación para Arduino que se encarga de controlar los relevadores que a su vez controlarán las luces. Hay varias observaciones que debes tener aquí.  
Lo primero es la declaración de las variables iniciales.

```c
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
```
Por cada luz debes declarar un renglón de este tipo, el valor corresponde al pin de Arduino donde deberás colocar el relevador de corriente.  

```c
String data = "";
char charBuf[4];
```
Hay dos variables globales declaradas después de la función de **setup** y serán las que manejen y reciban los datos recibidos desde el puerto serial. En la función **setup** puedes notar el canal de comunicación establecido para el protocolo **I2C** y el receptor del evento ejecutado al recibir información por este medio.

```c
Serial.begin(9600);
Wire.begin(0x40);
Wire.onReceive(receiveEvent);
```

Después, en la función **loop** no necesitarás hacer nada debido que ahora todo se realizará en el método que declaraste para recibir la información de I2C.
```c
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
```

El método **handleLightFromValue** recibe los dos primeros datos como el número de la luz que va a manipular y el tercer dato como el estado donde 0 corresponde a apagado y 1 a encendido.
```c
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
```
En cualquier caso, un último método se invocará pero con los datos de variables adecuados.
```c
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
```
Es un proceso sumamente fácil. Además hacemos que Arduino funcione como una tarjeta meramente transaccional, que solo gestiona los relevadores basándose en la información recibida por el protocolo I2C.

## Las pruebas

Debido a que este es el primer paso del proyecto y quizá el más delicado como fuente de errores debido a las soldaduras, arreglo de pines, etcétera. Antes de continuar, te recomiendo muchísimo no avanzar hasta hacer una prueba muy sencilla. La mejor manera de enviar información es hacerlo usando la misma aplicación de Windows pero de modo local.

Es una prueba muy fácil y que te permitirá comprobar que ya tienes todo listo para continuar con el proceso. Después de todo esto, podrás comenzar ya con la aplicación de Windows 10 IoT que recibirá toda la información y la enviará por I2C a Arduino.


