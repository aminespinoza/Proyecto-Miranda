# Aplicaciones cliente UWP y administrador WPF

En esta sección tengo dos proyectos. El primero corresponde a una aplicación WPF que será el administrador y quien enviará la información a Arduino. El segundo proyecto corresponde a una aplicación cliente para UWP que será la que envíe la infromación a la nube y de ahí la aplicación administrador recibirá la información.

## Aplicación administrador (hecha con WPF)

La aplicación para la administración de las luces desde el equipo es sumamente fácil, de hecho, hasta este último [commit](https://github.com/aminespinoza/Control-casa/commit/3ea3b78c247193a45440ba186134d67641357257) la aplicación no cuenta con una interfaz en lo absoluto, simplemente recibe la información desde Azure, la procesa y envía a Arduino. La intención es que paulatinamente pueda agregar cosas nuevas aquí para expandir la funcionalidad.

Por ahora, lo único que vale la pena ver de este proyecto es lo siguiente:  
En primer lugar establece el nombre del puerto por medio del cuál te vas a comunicar con Arduino así como el baudrate que usarás.
```c
string portName = "COM4";
puertoSerial.PortName = portName;
puertoSerial.BaudRate = 9600;
```
Después, nota que establecerás la comunicación con el IoT Hub por medio de los valores siguientes. El número **3** dentro del método CreateReceiver tiene que ver con el partitionId de IoT Hub, para poder saber cuantos tienes y cuál se usa está la línea siguiente. Las opciones son pocas y puedes llegar por descartamiento. Por último un temporizador que obviamente estará cada segundo viendo si se encuentra algún nuevo mensaje ya disponible.
```c
serviceClient = ServiceClient.CreateFromConnectionString(connectionString);

eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, iotHubD2cEndpoint);
eventHubReceiver = eventHubClient.GetDefaultConsumerGroup().CreateReceiver("3", DateTime.UtcNow);

var d2cPartitions = eventHubClient.GetRuntimeInformation().PartitionIds;

timer = new DispatcherTimer();
timer.Interval = TimeSpan.FromSeconds(1);
timer.Tick += Timer_Tick;
timer.Start();
```
El temporizador solo desencadena el evento **HandleReceivedInformation** que obtendrá la información del IoT Hub y después lo procesará usando serialización de JSON para los dos primeros valores que ocupamos.
```c
private async Task HandleReceivedInformation()
{
	EventData eventData = await eventHubReceiver.ReceiveAsync();
	string data = Encoding.UTF8.GetString(eventData.GetBytes());

	JObject serializedObject = JObject.Parse(data);
	string lightNumber = serializedObject["lightNumber"].ToString();
	string lightStatus = serializedObject["lightStatus"].ToString();
	DateTime lastMove = Convert.ToDateTime(serializedObject["date"]);

	HandleLights(lightNumber, lightStatus);
}
```
El método final, llamado HandleLights solo recibe el texto, lo formatea de la manera esperada, abrirá el puerto y después envía el texto para que por último solo cierre el puerto. Esta parte es de gran importancia puesto que mantener el puerto abierto de manera permanente solo hará que la conexión colapse y la aplicación deje de funcionar sin emitir ningún error lo que dificultará mucho poder ubicar el problema de manera veloz. Entre menos operaciones se hagan con el puerto abierto será mucho mejor, es por eso que solo se hará la única indispensable que es escribir sobre el mismo puerto. Como ventaja adicional encontré que esto también mantiene en buen estado el consumo de memoria RAM de la aplicación, si abrimos el puerto todo el tiempo la memoria se incrementa paulatinamente hasta un 50 por ciento.
```c
private void HandleLights(string light, string status)
{
	string finalCommand = string.Format("{0},{1}", light, status);

	puertoSerial.Open();
	puertoSerial.Write(finalCommand);
	puertoSerial.Close();
}
```
Hasta aquí es todo lo que la aplicación de WPF hace por el momento. Considera que esta aplicación, por ser el administrador, será en la que más trabajaremos y en la que más cambios iremos viendo en todo momento.

## La aplicación cliente UWP

El segundo proyecto dentro de esta solución es una Aplicación Universal de Windows y es simplemente un conjunto de doce botones que corresponden a cada una de las luces presentadas.

<img src="Assets/UWPInterface.jpg"/>