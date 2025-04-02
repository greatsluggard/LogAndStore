# 🗃️ LogAndStore

**LogAndStore** — ASP.NET Core Web API проект для приёма и хранения пар `код-значение`, с возможностью фильтрации и логированием всех операций.  
Проект разворачивается в Docker-контейнерах с PostgreSQL и предоставляет интерфейс для работы через Swagger.

---

## 📦 Состав проекта

- ASP.NET Core 8
- Entity Framework Core 9
- PostgreSQL (через Docker)
- Swagger UI
- AutoMapper
- Логгирование запросов в таблицу `RequestLogs`
- EF Core миграции применяются при запуске

<br>

## 🚀 Запуск проекта

> Требуется установленный Docker.

1. Клонируйте репозиторий:

```bash
git clone https://github.com/greatsluggard/LogAndStore.git
```

2. Перейдите в корневую директорию:

```bash
cd logandstore
```

3. Соберите и запустите проект:

```bash
docker-compose up --build
```

4. Откройте Swagger UI:

```
http://localhost:8080/swagger
```

<br>

## 🧪 Примеры запросов

### 📥 POST `/mydata`

Сохраняет массив данных. Перед сохранением таблица очищается.

#### Пример запроса:

```json
[
  { "code": "1", "value": "value1" },
  { "code": "5", "value": "value2" },
  { "code": "10", "value": "value32" }
]
```

---

### 📤 GET `/mydata`

Получает отсортированные данные с возможностью фильтрации.

#### Пример:

```
GET /mydata?code=5&value=val
```

#### Ответ:

```json
[
  {
    "number": 1,
    "code": 5,
    "value": "value2"
  }
]
```

---

### 📄 GET `/requestlogs`

Возвращает все логи запросов, отсортированные по убыванию времени.

<br>

## 🧰 Структура таблиц

### `MyData`

| Поле   | Тип     | Описание               |
|--------|----------|------------------------|
| Id     | int      | Порядковый номер (PK) |
| Code   | int      | Код                   |
| Value  | string   | Значение              |

### `RequestLogs`

| Поле         | Тип       | Описание               |
|--------------|------------|------------------------|
| Id           | Guid      | Уникальный идентификатор |
| Timestamp    | DateTime  | Время запроса          |
| MethodName   | string    | Название метода        |
| RequestData  | string    | Тело запроса           |
| ResponseData | string?   | Ответ (если есть)      |
| IsSuccess    | bool      | Успешность выполнения  |
| ErrorMessage | string?   | Сообщение об ошибке    |

<br>

## 🧱 Миграции

Миграции применяются автоматически при запуске.  
Для ручного применения:

```bash
# В Package Manager Console:
Add-Migration InitialCreate
Update-Database
```


## 🐳 Полезные команды Docker

```bash
# Остановить и удалить все контейнеры и volume
docker-compose down -v

# Пересобрать и перезапустить проект
docker-compose up --build
```



## 👤 Автор - Слаев Тимур

Разработано в рамках тестового задания. 

Принимаются pull requests и предложения по улучшению!


