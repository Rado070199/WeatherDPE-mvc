# WeatherDPE-mvc
Aplikacja została zaimplementowana przy użyciu technologii ASP.NET Core i wzorca MVC, która pozwala na tworzenie skalowalnych i wydajnych aplikacji webowych.

W moim projekcie stworzyłem kontroler o nazwie WeathersController, który jest odpowiedzialny za obsługę żądań HTTP związanych z informacjami o pogodzie. Kontroler ten wykorzystuje interfejs IWeathersService, który zawiera metody do pobierania, dodawania i usuwania informacji o pogodzie z bazy danych.

Jednym z najważniejszych endpointów kontrolera jest Index, który wyświetla informacje o ostatnim odczycie pogody zapisanym w bazie danych. Aby to osiągnąć, kontroler pobiera obiekt WeatherData z największym ID za pomocą metody GetLatestAsync z interfejsu IWeathersService.

Kolejnym ważnym endpointem jest Create, który pozwala na dodanie nowych informacji o pogodzie do bazy danych. Aby to zrobić, kontroler korzysta z API OpenWeatherMap, aby pobrać aktualne dane o pogodzie w wybranej lokalizacji. Następnie tworzy nowy obiekt WeatherData i dodaje go do bazy danych, ale tylko wtedy, gdy nie ma już w bazie rekordu o takiej samej dacie i godzinie.

Ponadto, kontroler WeathersController zawiera również endpointy Privacy, Details, Delete, które umożliwiają odpowiednio wyświetlenie listy wszystkich odczytów pogody, szczegółowe informacje o konkretnym odczycie pogody, usunięcie odczytu pogody.

W aplikacji wykorzystałem też kilka innych technologii, takich jak Entity Framework Core do obsługi bazy danych, Newtonsoft.Json do parsowania danych z API OpenWeatherMap oraz Bootstrap do stylizacji interfejsu użytkownika.
