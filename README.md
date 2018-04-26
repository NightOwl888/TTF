# TTF - A Web API Demo

This is a demonstration of how to create advanced algorithmic design with complex user input mappings, given a specific set of requirements.

## Requirements

You are given a number of inputs, mappings between inputs and outputs, and the expected output.

The algorithm should be implemented in a SOLID manner, and be exposed in a simple REST API.

Unit testing is not a requirement, but would be preferred.

#### Inputs

We have the following variables: 

- A: bool 
- B: bool 
- C: bool 
- D: int 
- E: int 
- F: int

#### Outputs

The outputs are defined as: 

- X: enum[S,R,T] 
- Y: real/float/decimal

#### Mappings

The assignment consists of a "base mapping", and two specialized mappings that override / extend the base mapping.

##### Base

```
A && B && !C => X = S 
A && B && C => X = R 
!A && B && C => X =T 
[other] => [error]


X = S => Y = D + (D * E / 100) 
X = R => Y = D + (D * (E - F) / 100) 
X = T => Y = D - (D * F / 100)

```

##### Specialized1

```
X = R => Y = 2D + (D * E / 100)
```

##### Specialized2

```
A && B && !C => X = T 
A && !B && C => X = S

X = S => Y = F + D + (D * E / 100)

```

#### REST

The algorithms can be implemented in any RESTful manner. However, the services should return the resulting data as JSON.


## Technologies Used

- C#
- Web API 2
- .NET Framework 4.6
- AutoMapper 5.2.0 (view model to domain model mapping)
- StructureMap 4.4.2 (dependency injection)
- WebApi Versioning 2.0.3 (version the API via URL)
- NUnit 3.6.0
- Moq 4.5.30
- IIS Express

## Design

The application uses a SOLID approach by taking advantage of both class inheritance and the [Strategy pattern](https://sourcemaking.com/design_patterns/strategy), which is particularly well-suited to swapping one formula for another in a loosely-coupled way. Class inheritance was employed to override specific formulas and mapping logic while leaving others as their defaults, which makes the design DRY, flexible and highly maintainable.

Employing the Strategy pattern along with some clever convention-based URL and dependency injection configuration code means that **no code changes** are required to add an additional `IFooCalculator` implementation with different business logic. The only requirement is to create the additional class that implements `IFooCalculator` (or subclass `BaseFooCalculator`). Provided the implementation is put into the `Ttf.BusinessLayer` assembly, it will be automatically added to the DI configuration.

Since naming conventions are important, but there was no information provided about what the calculation pertains to, the name `Foo` was arbitrarily chosen as the entity name. Of course, in a production application, this would be the name of the actual entity the API is working with.

The Web API employs a simple GET protocol to the URL `/api/v1/foo` to calculate the result. The calculation can be overridden from `"base"` by adding the query string `?provider=specialized1` or `?provider=specialized2`. However, note that this is simply cosmetic - it could just as well be a POST with a different URL for each overridden calculation with a few minor changes.

Of course, it is compulsory to add versioning to any sort of web-based API to allow for updates without breaking existing clients, so that was done here as well.

## Usage

Clone the repository and open in Visual Studio 2012 or higher. Ensure `Ttf.WebApi` is the startup project and run. This will open a simple HTML interface where you can interact with the API (if your IIS Express configuration prevents this from happening, you can add `index.html` to the URL). Note that this interface uses CDNs, so it requires Internet access to function.

To interact with the API directly with a browser, you can access it through the URL `/api/v1/foo?provider=base&a=true&b=true&c=false&d=123&e=456&f=789` and adjust the parameters accordingly.

There is also a suite of unit tests that can be run in Visual Studio Test Explorer, provided the [NUnit 3 Test Adapter](https://visualstudiogallery.msdn.microsoft.com/0da0f6bd-9bb6-4ae3-87a8-537788622f2dRob) plugin is installed.