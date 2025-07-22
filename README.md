# Empire

## Установка/Обновление внешних зависимостей

1. Убедитесь, что у вас установлен [.NET SDK](https://dotnet.microsoft.com/download).
2. Откройте UnityProjectDependency Solution.
3. Восстановите зависимости:
   ```bash
   dotnet restore
   ```
4. Установите нужные используя Nuget GUI или командную строку:
    ```bash
    dotnet add package <имя пакета>
    ```
5. Соберите проект под релизной сборкой и укажите путь (в текущем проекте путь <../Game/Assets/Plugins/ExternalDependencies>):
    ```bash
    dotnet build --configuration Release --output <path>
    ```