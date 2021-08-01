[![.NET](https://github.com/aimenux/SerilogWebApiDemo/actions/workflows/ci.yml/badge.svg)](https://github.com/aimenux/SerilogWebApiDemo/actions/workflows/ci.yml)

# SerilogWebApiDemo
```
Using Serilog to send logs to multiple sinks
```

> In this repo, i m using serilog in order to enable logging in web api applications :
>
> - 2 configuration ways : code config based or json config based (one way is enabled at the same time)
>
> - 1 exception filter attribute in order to handle some specific domain exceptions as 400 bad requests
>
> - 4 telemetry processors to filter telemetries or to modify the sampling behaviour
>
> - 1 telemetry initializer to add extra information as custom properties
>
> - 4 sinks : console, file, application insights, udp
>

**`Tools`** : vs19, net 5.0, serilog 4.1