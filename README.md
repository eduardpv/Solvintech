# Solvintech

## Требования

- обратите внимание на файл в серверной части `appsettings.json`, а именно `connectionString`
- в качестве клиента вы можете использовать:
    - react-application
    - swagger
        - либо измените параметр в файле серверной части `Properties->launchSettings.json->profiles->http->launchBrowser=true`
        - либо откройте вкладку после запуска проекта по URI: `http://localhost:5062/swagger/index.html`
    - postman
        - запросы прикрепил в репозитории под папкой `postman`
