# sharpy-json 
Sharpy is a platform (back end json API) built on C# (mono) 
 
### Important (!) 
 
I'm just a junior developer, keep that in mind if you want to use this platform, or help me improve it, thanks! :). I see this platform as base / boilerplate for my side projects back end with needed features out of the box. 

### Current features:
* Using as monolith or as microservice (check microservice module)
* MVC pattern
* Model transformers (check UserTransformer)

### How to setup:
* currenlty only postgresql db is supported but adding support for mysql should be pretty easy
* Setup instructions:

### Used tools:
* [Dapper (object mapper for .Net)](https://github.com/StackExchange/Dapper)
* [Phinx (migrations / PHP)](https://github.com/cakephp/phinx)
* [Newtonsoft.JSON](https://github.com/JamesNK/Newtonsoft.Json)
* [WebsocketSharp](https://github.com/sta/websocket-sharp)

### How to add new endpoints:
+ add your request category (ex. Auth) to RequestProcessor (Scripts/Core)
+ all requests ex. Login should have unique enum in RequestTypes (Core/RequestTypes)
+ connect your controller via Process() function which should return RequestResponse
+ in your controller add middleware (if needed) then pass your request parameters to ControllerLogic classes
+ in Logic classes handle your request and then return RequestResponse (example: auth requests logic)

### v0.4 Roadmap
- [ ] crypto API's manager -> checking wallet balances & transactions (Eth, Dash, BTC, LTC)
- [ ] ...

### Incoming features:
- [ ] redis client for caching
- [ ] ...

### Docs
[API v0](https://htmlpreview.github.io/?https://github.com/mxss/sharpy-json/blob/master/docs/api_v0.html)
