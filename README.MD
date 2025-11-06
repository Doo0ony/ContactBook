# ContactBook
## Что это

ContactBook — веб-приложение на ASP.NET Core для хранения контактов пользователей и их телефонных номеров. Используется архитектура с разделением на слои: Application, Infrastructure, API и Client.

## Основные возможности

* CRUD-операции для пользователей (Users) и телефонов (Phones)
* Репозитории и сервисы с внедрением зависимостей (DI)
* Entity Framework Core с миграциями и инициализацией БД

## Требования

* .NET SDK 8.0 или выше
* SQL Server
* Установленный инструмент `dotnet-ef` для работы с миграциями

## Установка и запуск

1. Клонируйте репозиторий:

```bash
git clone <ссылка_на_репозиторий>
cd ContactBook
```

2. Настройте строку подключения в `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ContactBookDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

3. Примените миграции и создайте базу данных:

```bash
dotnet ef database update --project ContactBook.Infrastructure
```

4. Запустите API:

```bash
dotnet run --project ContactBook.Api
```

5. Запустите Client:

```bash
dotnet run --project ContactBook.Client
```

После запуска API будет доступен по адресу `http://localhost:5073`.
После запуска клиента он будет доступен по адресу `http://localhost:5071`.

## Команды разработки

* Восстановить пакеты: `dotnet restore`
* Собрать проект: `dotnet build`
* Добавить миграцию: `dotnet ef migrations add <Name> --project src/ContactBook.Infrastructure`
* Запустить тесты: `dotnet test`

## Структура проекта

* `src/ContactBook.Api` — веб-API, точки входа и контроллеры
* `src/ContactBook.Application` — бизнес-логика, интерфейсы маппинги
* `src/ContactBook.Domain` — сущности БД, перечисления
* `src/ContactBook.Shared` — общие сущности, ДТО
* `src/ContactBook.Infrastructure` — контекст данных и репозитории
* `src/ContactBook.Client` — клиентское приложение (Blazor)
