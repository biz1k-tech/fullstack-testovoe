# fullstack-testovoe

## Локальный запуск

1. Установить строку подключения в appsettings.json для бека
    ```
    файл находится в : 
    fullstack-testovoe\src\Backend\Api
    ```
2. Сделать миграции
   ```
   default project: Backend/Api
   ```
   
3. Вставить порты апи для фронта в два файла appsettings.json
   ```
   файл находится в : 
   fullstack-testovoe\src\Frontend\Web
   fullstack-testovoe\src\Frontend\Web.Client\wwwroot
   ```
4. Перейти в директорию проекта и выполнить команду:
   ```
   Start-Process dotnet "run --project ./src/Backend/Api/Api.csproj"
   Start-Process dotnet "run --project ./src/Frontend/Web/Web.csproj"
   ```
