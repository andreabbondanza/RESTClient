# RESTClient
A simple library for .net core that can help you to consume REST services.

## Objects
We have two object, RESTClient and RESTResponse that implements IRESTClient and IRESTResponse interfaces.
We have also extended the HttpClient object with Patch\Head\Options wrappers over the classic GetAsync\PutAsync\PostAsync, so we have a full REST Client methods support.

Please check inline docs for more information.

## Note
You can recompile the .NETStandard version with the .NET target you want. Be careful about the 1.6 target, for a roslyn bug you'll not be able to compile.


## About
[Andrea Vincenzo Abbondanza](http://www.andrewdev.eu)
