# RestoreMy2FA
программа для генерации QR кодов по бэкапу Google Authenticator

Подойдёт только тем, у кого на старом телефоне есть рут и Titanium Backup, а на новом рут получить невозможно/нет желания/это лишит телефон гарантии

Программа извлекает ключи из базы данных Google Authenticator, генерирует по ним QR коды, и сохраняет в виде картинок.
Как пользоваться:
0. Сбилдить

1. Распаковать куда угодно.
2. С помощью Titanium Backup сделать бэкап вашего Google Authenticator.
3. Скопировать файл формата com.google.android.apps.authenticator2-YYYYMMDD-HHMMSS.tar.gz с телефона в папку с прогой, где лежит RestoreMy2FA.exe.
4. Запустить RestoreMy2FA.exe, рядом появится папка Export c QR кодами.
