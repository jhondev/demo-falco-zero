// Learn more about F# at http://fsharp.org

open System
open Falco

[<EntryPoint>]
let main args =
    Host.startWebHost args (Server.configureWebHost Env.developerMode) Router.routes
    0 // return an integer exit code
