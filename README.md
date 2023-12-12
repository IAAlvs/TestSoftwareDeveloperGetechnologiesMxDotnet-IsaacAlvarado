API Test Ventas
=============

Instrucciones para ejecutar la Api
-----------------------------------------

Para ejecutar la API de Ventas, sigue los siguientes pasos:

1.  Abre una terminal o línea de comandos en la carpeta raíz del proyecto.
2.  Ejecuta el siguiente comando para restaurar las dependencias:

`dotnet restore`

3.  El proyecto ya tiene los archivos DE DB SQLITE con migraciones(con formato **.db), de todas maneras se puede ejecutar el siguiente comando para aplicar las migraciones de base de datos:

`dotnet ef database update`

4.  Finalmente, ejecuta la aplicación con el siguiente comando, para usar el puerto ya definido:

`dotnet run --urls "https://localhost:8080`

La API estará disponible en [https://localhost:8080].

Explorar la API en Swagger
--------------------------

Para explorar y probar los endpoints de la API, abre tu navegador y visita:

`https://localhost:8080/api/v1/sales/swagger/index.html`

Desde la interfaz de Swagger, podrás ver la documentación detallada y realizar pruebas en vivo de la API de Ventas.

### Notas importantes:
*   Puedes usar el dockerfile, sin embargo la direccion de la db tiene que configurarse ya sea 
    que la imagen este basada en linux o windows, simplemente ajusta la variable de "DbConnection" : "DireccionDependiendoTIpodeContenedor\SalesDb.db"

*   Asegúrate de tener .NET SDK instalado en tu máquina antes de ejecutar la aplicación.
*   Si la aplicación está configurada en un puerto diferente, ajusta las URL en consecuencia.
