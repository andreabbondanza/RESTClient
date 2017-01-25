
# RESTClient
A simple library for .net core that can help you to consume REST services.

## Objects
We have two object, RESTClient and RESTResponse that implements IRESTClient and IRESTResponse interfaces.
We have also extended the HttpClient object with Patch\Head\Options wrappers over the classic GetAsync\PutAsync\PostAsync, so we have a full REST Client methods support.

Please check inline docs for more information.

u## How to use

#### Status Macrotypes
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


Es: 
````C#
var apiHost = "https://myapi.com/";
RESTRequest request = new RESTRequest();
request.SetMethod(Method.GET);
request.SetUrl(apiHost + "2/categories");
request.AddQueryArgs("order", "asc");
request.AddHeader("Accept", "text/html");
RESTClient client = new RESTClient();
RESTResponse response = (RESTResponse)await client.PerformRequest(request);
````

Or you can use directly RESTClient methods versions (PUT, GET, PATCH, ETC)

Es: 
````C#
var apiHost = "https://myapi.com/";
Dictionary<string, string> queryArgs = new Dictionary<string, string>();
queryArgs.Add("order", "asc");
Dictionary<string, string> headers = new Dictionary<string, string>();
headers.Add("Accept", "text/html");
RESTClient client = new RESTClient();
RESTResponse response = (RESTResponse)await client.PerformGetRequestAsync(apiHost + "2/categories", queryAargs, headers);
````

## Note
You can recompile the .NETStandard version with the .NET target you want. Be careful about the 1.6 target, for a roslyn bug you'll not be able to compile.

## NuGet
You can find it on nuget with the name [DewRESTClient](https://www.nuget.org/packages/DewRESTClientStandard/)

## About
[Andrea Vincenzo Abbondanza](http://www.andrewdev.eu)
