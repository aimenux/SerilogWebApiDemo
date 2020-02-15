![.NET Core](https://github.com/aimenux/SerilogWebApiDemo/workflows/.NET%20Core/badge.svg)
# SerilogWebApiDemo
```
Logging into AppInsights/File using Serilog
```

> In this demo, i m using :
>
> - 2 configuration ways : code config based or json config based (one way is enabled at the same time) 
>
> - 1 exception filter attribute in order to handle some specific domain exceptions as 400 bad requests
>
> - 4 telemetry processors to filter telemetries or to modify the sampling behaviour
>
> - 1 telemetry initializer to add extra information as custom properties

**`Tools`** : vs19, net core 3.1, serilog 3.2