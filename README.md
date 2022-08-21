# COIS
Sample reporting app for demonstrate use of .Net Core 6, .Net Core Web Api, .NET Blazor.
Current in progress at freetime. 
This app allows managers in company to report work status and manage employees work time, manage devices used by employees and more. 
To run neccessary is to change connection string in AppDBContext.cs and init migration to DB before run. 
Data access layer based on Entity Framework Core, reporting by .NEt Winforms RDLC Reports. WEB API is only added for demonstrate knowledge of REST API. 
All logic is at COIS project, so we can use many frontend solutions for app. 