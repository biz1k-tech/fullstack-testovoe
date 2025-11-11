# fullstack-testovoe

## Локальный запуск

1. Установить строку подключения в appsettings.json для бека
    ```
    файл находится в : 
    fullstack-testovoe\src\Backend\Api
    ```
2. Сделать миграции\
В package manaer console, выбрав default project:backend/api, прописать
   ```
   update-database -context ContextBase1
   update-database -context ContextBase2
   ```
3. Перейти в директорию проекта и выполнить команду:
   ```
   Start-Process dotnet "run --project ./src/Backend/Api/Api.csproj"
   Start-Process dotnet "run --project ./src/Frontend/Web/Web.csproj"
   ```
* Опционально

Если не работает веб, то дело в портах бекенда.\
Необходимо вставить порты апи для фронта в два файла appsettings.json
   ```
   файл находится в : 
   fullstack-testovoe\src\Frontend\Web
   fullstack-testovoe\src\Frontend\Web.Client\wwwroot
   ```
