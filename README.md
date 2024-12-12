# Notes
Задача будет  следующая:

Нужно будет создать web-приложение "Заметки". 
Система для залогинивания пользователей: Keycloak
Хранение информации в БД: PostgreSQL. 
ORM: EF Core

Функционал следующий:
1) Вход в систему (по логину и паролю с использованием Keycloak).
    1.1) Подключиться к существующему рилму и получить настройки
    1.2) Сохранить основную информацию о пользователе из кейклок к нам в базу
2) Отображение списка заметок. 
    2.1) Добавить новую заметку (одну), 
    2.2) Просмотреть, 
    2.4) Удалить, 
3) Отображение конкретной заметки:
    3.1) У заметки должно быть имя и тело.
    3.2) Заметку можно выделить цветом для визуального отображения (отображаться должно в списке задач).
4) Возможность поделиться заметкой с другим пользователем.
    4.1) Права на чтение заметки (по умолчанию)
    4.2) Права на редактирование заметки
    4.3) Возможность открыть заметку для всех пользователей (только на чтение)
