# RESTClient
A simple library for .net core that can help you to consume REST services.

:warning: BREAK CHANGE IN 4.0.0 - Now the __RESTRequest__ object is disposable 

## Objects
We have two object, RESTClient and RESTResponse that implements IRESTClient and IRESTResponse interfaces.
We have also extended the HttpClient object with Patch\Head\Options wrappers over the classic GetAsync\PutAsync\PostAsync, so we have a full REST Client methods support.

Please check inline docs for more information.

## How to use

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
using(RESTRequest request = new RESTRequest())
{
    request.SetMethod(Method.GET);
    request.SetUrl(apiHost + "2/categories");
    request.AddQueryArgs("order", "asc");
    request.AddHeader("Accept", "application/json");
    using(RESTClient client = new RESTClient())
    {
        using(RESTResponse response = (RESTResponse)await client.PerformRequest(request))
        {
            string myJson = null;
            if(response.IsSuccesStatusCode())
                myJson = response.ReadResponseAsStringAsync();
            ...
        }
    }
}
````

Or you can use directly RESTClient methods versions (PUT, GET, PATCH, ETC)

Es: 
````C#
var apiHost = "https://myapi.com/";
Dictionary<string, string> queryArgs = new Dictionary<string, string>();
queryArgs.Add("order", "asc");
Dictionary<string, string> headers = new Dictionary<string, string>();
headers.Add("Accept", "application/json");
using(RESTClient client = new RESTClient())
    using(RESTResponse response = (RESTResponse)await client.PerformGetRequestAsync(apiHost + "2/categories", queryAargs, headers))
    {
        string myJson = null;
        if(response.IsSuccesStatusCode())
            myJson = response.ReadResponseAsStringAsync();
        ...
    }
}
````

## Types
- __RESTResponse__ : The response object
- __RESTClient__ : The client object
- __RESTRequest__ : Request object

### RESTResponse
- __GetStatusCode()__ : _HttpStatusCode_ - Return the status code.
- __IsSuccessStatusCode()__ : _bool_ - True if the type is Success
- __IsRedirectedStatusCode()__ : _bool_ - True if type is Redirect
- __IsErrorStatusCode()__ : _bool_ - True if type is Error
- __IsFaultStatusCode()__ : _bool_ - True if type is Fault
- __GetHttpStatusCodeType__ : _HttpStatusType_ - Return the status code type. If you want the status code you can use the enum in System.Net
- __ReadResponseAsStringAsync__ : _awaitable Task\<string\>_ - Return the response as a string
- __GetResponse__ : _HttpResponseMessage_ - Get the direct response object
- __RESTResponse(HttpResponseMessage)__ : _constructor_ - Construct a new RESTResponse object

### RESTClient
- __SetValidation(HeadersValidation)__ : _void_ - Set if the header must be validated before sending
- __GetRESTResponse(HttpResponseMessage)__ : _IRESTResponse_ - Return a new RESTResèpmse object from the HttpResponseMessage
- __IsValidUrl(string)__ : bool - Check if an url is valid
- __GetHandler()__ : HttpClientHandler - _Get current http handler_
- __SetHandler(HttpClientHandler)__ : Set a new handler for client. This will create also a new instance of httpclient in it
- __RESTClient(CancellationToken)__ : _constructor_
- __RESTClient(HeadersValidation, CancellationToken) : _constructor_
- __RESTClient(HeaderValidatoin, HttpClientHandler, CancellationToken)__ : _constructor_
- __RESTClient(HttpClientHandler, CancellationToken) : _constructor_
- __PerformRequest(IRESTRequest)__ : _awaitable Task\<IRESTResponse\>_ - Perform a request by an IRESTRequest object
- __PerformDeleteRequestAsync(string, Dictionary\<string,string\>,Dictionary\<string,string\>)__ : _awaitable Task\<IRESTResponse\>_ - Perform a delete request by an IRESTRequest object
- __PerformPutRequestAsync(string, Dictionary\<string,string\>,Dictionary\<string,string\>)__ : _awaitable Task\<IRESTResponse\>_ - Perform a put request by an IRESTRequest object
- __PerformPatchRequestAsync(string, Dictionary\<string,string\>,Dictionary\<string,string\>)__ : _awaitable Task\<IRESTResponse\>_ - Perform a patch request by an IRESTRequest object
- __PerformPostRequestAsync(string, Dictionary\<string,string\>,Dictionary\<string,string\>)__ : _awaitable Task\<IRESTResponse\>_ - Perform a post request by an IRESTRequest object
- __PerformGetRequestAsync(string, Dictionary\<string,string\>,Dictionary\<string,string\>)__ : _awaitable Task\<IRESTResponse\>_ - Perform a get request by an IRESTRequest object
- __PerformHeadRequestAsync(string, Dictionary\<string,string\>,Dictionary\<string,string\>)__ : _awaitable Task\<IRESTResponse\>_ - Perform a head request by an IRESTRequest object
- __PerformOptionsRequestAsync(string, Dictionary\<string,string\>,Dictionary\<string,string\>)__ : _awaitable Task\<IRESTResponse\>_ - Perform a options request by an IRESTRequest object      

### RESTRequest
- __SetContent(HttpContent)__ : _void_ - Add a content to the request
- __SetHeader(string,string)__ : _void_ - Add new header to the request
- __SetQueryArgs(string,string)__ : _void_ - Add a new argoument to the query string
- __SetUrl(string)__ : _void_ - Set the url to the request
- __IsValidUrl(string)__ : bool - Check if an url is valid
- __GetMethod()__ : _Method_ - return the current method
- __SetMethod(Method)__ : _void_ - return the current method
- __GetHeaders()__ : _Dictionary\<string,string\>_ - Return the setted headers
- __GetContent()__ : _HttpContent_ - Return the setted content
- __GetQueryArgs()__ : _Dictionary\<string,string\>_ - Return the setted headers
- __GetUrl()__ : _string_ - Return the request URL
- __AddMultipartFormDataContent(string,byte[],string)__ : _void_ - Add a multipart form data
- __AddMultipartFormDataContent(string,string)__ : _void_ - Add a multipart form data
- __AddMultipartFormDataContent(string,Stream,string)__ : _void_ - Add a multipart form data
- __AddFormUrlEncodedContent(string,string)__ : _void_ - Add a form url encoded data
- __Dispose()__ : _void_
- __RESTRequest(string)__ : _constructor_ - Construct a RESTRequest with url
- __RESTRequest()__ : _constructor_ 

## Note

:warning: BREAK CHANGE IN 4.0.0 - Now the __RESTRequest__ object is disposable 

## NuGet
You can find it on nuget with the name [DewRESTClient](https://www.nuget.org/packages/DewRESTClientStandard/)

## About
[Andrea Vincenzo Abbondanza](http://www.andrewdev.eu)

## Donate
[Help me to grow up, if you want](https://payPal.me/andreabbondanza)

