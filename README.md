# ePochtaSms
.Net client for ePochtaSms.ru HTTPS API

Supports only sending messages for now.
Usage:
```javascript
var client = new EpochtaSmsClient("Username", "Password", "Sender");
var message = new Message("79991234567", "Some text");
await client.SendAsync(message);
```

License: [WTFPL](http://www.wtfpl.net/txt/copying/ "WTFPL")
