# RestoreMy2FA
Программа для генерации QR кодов из бэкапа Google Authenticator

Подойдёт только тем, у кого на старом телефоне есть рут и Titanium Backup, а на новом рут получить невозможно/нет желания/это лишит телефон гарантии

Программа извлекает ключи из базы данных Google Authenticator, генерирует по ним QR коды, и сохраняет в виде картинок.
## Как пользоваться:
0. Сбилдить
1. С помощью Titanium Backup сделать бэкап вашего Google Authenticator.
2. Скопировать файл формата com.google.android.apps.authenticator2-YYYYMMDD-HHMMSS.tar.gz с телефона в папку с прогой, где лежит RestoreMy2FA.exe.
3. Запустить RestoreMy2FA.exe, рядом появится папка Export c QR кодами.

===================================================================   
Application for QR code generation from Google Authenticator backup

Suitable only for those who have an rooted old phone with Titanium Backup, and a new one cannot be rooted

The application retrieves the keys from the Google Authenticator database, generates QR codes by these keys, and saves them as poctures with QR codes. 
## How to use: 

0. Build it.
1. Use Titanium Backup to make a backup of your Google Authenticator.
2. Copy file com.google.android.apps.authenticator2-YYYYMMDD-HHMMSS.tar.gz from the phone storage to a folder with the RestoreMy2FA.exe.
3. Start RestoreMy2FA.exe, check your QR codes in the Export folder.
