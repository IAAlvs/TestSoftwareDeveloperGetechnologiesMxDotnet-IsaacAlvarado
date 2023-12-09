API Test Ventas
=============

Instrucciones para ejecutar la Api
-----------------------------------------

Para ejecutar la API de Ventas, sigue los siguientes pasos:

1.  Abre una terminal o línea de comandos en la carpeta raíz del proyecto.
2.  Ejecuta el siguiente comando para restaurar las dependencias:

`dotnet restore`

3.  Ejecuta el siguiente comando para aplicar las migraciones de base de datos:

`dotnet ef database update`

4.  Finalmente, ejecuta la aplicación con el siguiente comando:

`dotnet run`

La API estará disponible en [http://localhost:5000](http://localhost:5000).

Explorar la API en Swagger
--------------------------

Para explorar y probar los endpoints de la API, abre tu navegador y visita:

`http://localhost:7064/api/v1/sales/swagger/index.html`

Desde la interfaz de Swagger, podrás ver la documentación detallada y realizar pruebas en vivo de la API de Ventas.

### Notas importantes:

*   Asegúrate de tener .NET SDK instalado en tu máquina antes de ejecutar la aplicación.
*   Si la aplicación está configurada en un puerto diferente, ajusta las URL en consecuencia.