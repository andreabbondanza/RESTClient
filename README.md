# RESTClient
A simple library for .net core that can help you to consume REST services.

## Objects
We have two object, RESTClient and RESTResponse that implements IRESTClient and IRESTResponse interfaces.
We have also extended the HttpClient object with Patch\Head\Options wrappers over the classic GetAsync\PutAsync\PostAsync, so we have a full REST Client methods support.

Please check inline docs for more information.

## How to use

### Status Macrotypes
RESTClient http status macrotypes:
- 2xx : Succesful
- 3xx : Redirected
- 4xx : Error 
- 5xx : Fault 

### Methods
PUT, GET, POST, PATCH, HEAD, OPTIONS

### Interfaces
The library is based on three interfaces:
- __IRESTClient__ : is the RESTClient core for connnections
- __IRESTResponse__ : is the RESTResponse object
- __IRESTRequest__ : is the RESTRequest object

With this approach you can use the dependency injection and use also your implementation of code.

### Two way to approach
You can use the library creating and filling a RESTRequest object this way.

- Instantiate a RESTRequest object
- Set URL, Method, etc
- Execute RESTClient.PerformRequest(myRESTRequestObject)

Or you can use directly RESTClient methods versions (PUT, GET, PATCH, ETC)

Es: 
````C#
RESTResponse response = (RESTResponse)await this.connection.Client.PerformGetRequestAsync(this.connection.ApiHost + "2/categories", queryAargs, headers);
````

## Note
You can recompile the .NETStandard version with the .NET target you want. Be careful about the 1.6 target, for a roslyn bug you'll not be able to compile.


## About
[Andrea Vincenzo Abbondanza](http://www.andrewdev.eu)
