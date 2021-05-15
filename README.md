# NET5 Academy
It is a course system created to apply current technologies. Instructors and trainees will be able to participate and use the system.

<a href="https://github.com/osman-koc/net5-academy/network/members" target="_blank"><img src="https://img.shields.io/github/forks/osman-koc/net5-academy" /></a> <a href="https://github.com/osman-koc/net5-academy/stargazers" target="_blank"><img src="https://img.shields.io/github/stars/osman-koc/net5-academy" /></a> <a href="https://github.com/osman-koc/net5-academy/issues" target="_blank"><img src="https://img.shields.io/github/issues/osman-koc/net5-academy" /></a> <a href="https://github.com/osman-koc/net5-academy/releases" target="_black"><img src="https://img.shields.io/github/downloads/osman-koc/net5-academy/total" /></a>

## Used Techs and Methods
- ASP.NET 5.0
- ASP.NET Core MVC
- Microservices
- API Gateway with Ocelot library
- Identity Server & JWT Token & Token Exchange
- SQL Server Express (MS-SQL) & PostgreSQL & MongoDB & Redis & RabbitMQ
- Entity Framework Core & Dapper
- AutoMapper
- SOLID principle
- Domain Driven Design & CQRS
- Portainer
- Swagger
- Bootstrap 5.0
- FluentValidation
- Docker & Kubernetes & Helm

<img src="https://img.shields.io/badge/ASP.NET%20Core-5.0-blueviolet" /> <img src="https://img.shields.io/badge/ASP.NET%20MVC%20Core-5.2-blueviolet" /> <img src="https://img.shields.io/badge/IdentityServer4%20-4.1.1-orange" /> <img src="https://img.shields.io/badge/MSSQL%20Server%20(linux)-2017-blue" /> <img src="https://img.shields.io/badge/MongoDB-latest-green" /> <img src="https://img.shields.io/badge/Redis-latest-green" /> <img src="https://img.shields.io/badge/Bootstrap-5.0-blueviolet" />

## Architecture
![GeneralDiagram](img/diagram_general.jpg)

![IdentityDiagram](img/diagram_identity.jpg)

## Features
- Shared / Library <img src="https://findicons.com/files/icons/1671/simplicio/128/notification_done.png" width="20" />
- IdentityServer (MSSQL, EF Core) <img src="https://findicons.com/files/icons/1671/simplicio/128/notification_done.png" width="20" />
- Catalog microservice (MongoDB) <img src="https://findicons.com/files/icons/1671/simplicio/128/notification_done.png" width="20" />
- PhotoStock microservice <img src="https://findicons.com/files/icons/1671/simplicio/128/notification_done.png" width="20" />
- Basket microservice (Redis)  <img src="https://findicons.com/files/icons/1671/simplicio/128/notification_done.png" width="20" />
- Discount microservice (PostgreSQL, Dapper) <img src="https://findicons.com/files/icons/1671/simplicio/128/notification_done.png" width="20" />
- Order microservice (MSSQL, EF Core, Domain Driven Design, CQRS) <img src="https://findicons.com/files/icons/1671/simplicio/128/notification_done.png" width="20" />
- Payment microservice <img src="https://findicons.com/files/icons/1671/simplicio/128/notification_done.png" width="20" />
- API Gateway (Ocelot) <img src="https://findicons.com/files/icons/1671/simplicio/128/notification_done.png" width="20" />
- ASP.NET Core MVC Web Application <img src="https://image.flaticon.com/icons/png/128/1716/1716746.png" width="20" />
  - Cookie based authentication / authorization
  - Bootstrap 5.0 Beta
  - IdentityService & UserService
  - CatalogService & CourseService
  - ClientCredentialTokenService
  - Layout & Home
  - BasketService & DiscountService
  - FluentValidation
  - PaymentService & OrderService
- MassTransit RabbitMQ (Message Broker)
- Eventual Consistency
- Token Exchange
- Docker Containers
- Kubernetes & Helm
