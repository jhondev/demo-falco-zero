module Middlewares

open System
open Falco
open Microsoft.Extensions.Logging

let exceptionHandler (developerMode: Env.DeveloperMode): ExceptionHandler =
    let (Env.DeveloperMode developerMode) = developerMode

    fun (ex: Exception) (log: ILogger) ->
        let logMessage =
            match developerMode with
            | true -> sprintf "Server error: %s\n\n%s" ex.Message ex.StackTrace
            | false -> "Server Error"

        log.Log(LogLevel.Error, logMessage)

        Response.withStatusCode 500
        >> Response.ofPlainText logMessage
