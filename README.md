# PaymentGateway

- dotnet build .\PaymentGateway.sln
- dotnet run -p .\Presentation\Presentation.API\Presentation.API.csproj

TODOS:

#1
Refactor mapper classes to better apply single responsability

#2
Move Notifications strategy to a crosscutting layer, and apply log recording in Notify() method

#3
Centralize controller's BadRequest and NotFound handling in a BaseController

#4
Centralize validator assertions behavior

#5
Refactor validation strategy to handle it with generic types

#6
Trim credit card number to accept numbers with space

7# 
Uniformize endpoints routes

#8
Standardize authorizationId use in endpoints

#9
Fix multiple refunds bug

#10
Fix capture and refund response code and error message when the authorization is void

#11
Unit tests
