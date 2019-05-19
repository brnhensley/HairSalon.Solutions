# _Hair Salon_

#### _A Hair Salon Web App, 5-10-19_

#### _By Brian Hensley_

## _Description_

This is a rad program that will allow a hair salon to add and view data about their specialties, stylists, and clients of those stylists; for maximum cool. 😎

## _Setup/Installation Requirements_

* _Clone this repo to your home machine_
* _Start MAMP_
* _Click Start Servers_
* _Click Open WebStart page_
* _On the webpage that opens, click Tools and choose PHPMYADMIN_
* _Create new database named brian_hensley_
* _Select the Import tab_
* _Select the brian_hensley.SQL file and click Go_
* _Navigate the terminal to the HairSalon directory and enter $ dotnet run_
* _Open browser to localhost:5000 to use the app_

## _Specs_

|Objectives|example input|example output|
|-|-|-|
|Create a new stylist|"Larry"|"Larry" added to stylists database table|
|Create a new client|"Curly"|"Curly" added to clients database table|
|View a list of all stylists|Click 'View All Stylists'|Page opens with a list of all stylists|
|View a specific stylist|Click a stylist's name from a list|Page opens with that stylist's information and clients|
|View a list of all a stylist's clients|Click a stylist's name|Page opens with a list of all clients|
|View a specific client|Click a client's name from a list|Page opens with that client's information|
|Delete a client|Click "Delete client"|Client removed from database|
|Delete all clients|Click "Delete all clients"|All clients removed from database|
|Delete a stylist|Click "Delete stylist"|Stylist removed from database|
|Delete all stylists|Click "Delete all stylists"|All stylists removed from database|
|Create a new specialty|"Wigs"|"Wigs" added to specialties database table|
|Connect a specialty to a client|Select "Add Wigs to Larry"|"Larry" and "Wigs" added to the stylists_specialties join table|
|Connect a client to a specialty|Select "Add Larry to Wigs"|"Larry" and "Wigs" added to the stylists_specialties join table|
|View a list of all a stylist's specialties|Click a stylist's name|Page opens with a list of all specialties|
|View a list of all a specialty's linked stylists|Click a specialty's name|Page opens with a list of all stylists associated|

## _Known Bugs_

No known bugs

## _Support and contact details_

Create a pull request on GitHub.

## _Technologies Used_

I used C#, Razor, HTML, CSS, Javascript and DuckDuckGo to build this program.

### _License_

GPL, keep information free.

Copyright (c) 2019 Brian Hensley
