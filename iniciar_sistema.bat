@echo off
echo Iniciando Sistema CoolDrive...
echo ==============================

echo 1. Iniciando la API (Backend)...
start cmd /k "cd api\ApiAire\ApiAire && dotnet run"

echo.
echo 2. Iniciando la Aplicacion Web (Frontend)...
start cmd /k "python -m http.server 3000 --directory app"

echo.
echo Esperando 5 segundos para que los servicios arranquen...
timeout /t 5 /nobreak >nul

echo.
echo 3. Abriendo el navegador (App y Swagger)...
start http://localhost:3000
start http://localhost:5152/swagger

echo.
echo ==============================
echo El sistema esta corriendo! 
echo Manten abiertas las dos pantallas negras que aparecieron
echo para que el sistema siga funcionando.
echo ==============================
