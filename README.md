# Агрегатор вакансий 

# Установка 

# Добавление источника данных HeadHunter 

Выполните команду 
```
dotnet VacancyAggregator.Console.dll create-datasource -n HeadHunter -sn HH -a "..\VacancyAggregator.VacancySources.HeadHunter.dll" -c "https://api.hh.ru/"  
```

для добавления источника данных HeadHunter, где параметр -a содержит путь к библиотеке VacancyAggregator.VacancySources.HeadHunter.dll  

# Кастомизация фильтров  

Создайте фильтры, по которым будут фильтроваться вакансии через команду  

```
dotnet VacancyAggregator.Console.dll create-vacancy-filter -t "value" -e "value"  
```

где -t - ключевые слова, 
-e - требуемый опыт, возможные значения:  
0 - Не имеет значения  
1 - Без опыта  
2 - От 1 года до 3 лет  
3 - От 3 лет до 6 лет  
4 - Более 6 лет  
  
Пример команды: 
```
dotnet VacancyAggregator.Console.dll create-vacancy-filter -t "C# javascript" -e 1
```

# Получение данных из источников данных
Запустите сервис VacancyAggregator.Service.dll, который начнет импорт вакансий из источников данных.  

Настройте периодичность импорта через конфигурационный файл appsettings.json:: 

```
"VacancyImporterBackgroundService": {
    "DateTime": "CRON EXPRESSION"
  }
```

# Отображение данных

Используйте веб ui VacancyAggregator.WebUI.dll или консольное приложение VacancyAggregator.Console для просмотра списка загруженных вакансий