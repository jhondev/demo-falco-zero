module Server

open Falco
open Falco.Host
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging
open Handlers

let configureWebHost (developerMode: Env.DeveloperMode): ConfigureWebHost =
    let configureLogging (log: ILoggingBuilder) =
        let (Env.DeveloperMode developerMode) = developerMode

        let logLevel =
            if developerMode then LogLevel.Information else LogLevel.Error

        log.SetMinimumLevel(logLevel) |> ignore

    let configureServices (services: IServiceCollection) =
        services.AddRouting().AddResponseCaching().AddResponseCompression()
        |> ignore

    let configure (developerMode: Env.DeveloperMode) (endpoints: HttpEndpoint list) (app: IApplicationBuilder) =

        app.UseExceptionMiddleware(Middlewares.exceptionHandler developerMode).UseResponseCaching()
           .UseResponseCompression().UseStaticFiles().UseRouting().UseHttpEndPoints(endpoints)
           .UseNotFoundHandler(notFoundHandler)
        |> ignore

    fun (endpoints: HttpEndpoint list) (webHost: IWebHostBuilder) ->
        webHost.UseKestrel().ConfigureLogging(configureLogging).ConfigureServices(configureServices)
               .Configure(configure developerMode endpoints)
        |> ignore
