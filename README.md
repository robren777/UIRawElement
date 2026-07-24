Aplicación de consola C# que extrae los nombres de los sub-menus de XPOS. (btnMenu)

Utiliza la librería nativa de windows UIAutomationCliente para encontrar elementos que tienen la propiedad IsControlElement = false usando RawViewWalker.

Modo de uso
Ejecutar el .exe de la siguiente manera:

Para imprimir todos los textos de los menus mostrados en pantalla actual
UIRawElement.exe 0

Para dar click al elemento especificado del menu mostrado en la pantalla actual
UIRawElement.exe 1 "SKY EN LINEA"
