module Env

open System
open System.IO
open Falco.StringUtils

[<Struct>]
type DeveloperMode = DeveloperMode of bool

let root = Directory.GetCurrentDirectory()

let tryGetEnv (name: string) =
    match Environment.GetEnvironmentVariable name with
    | null
    | "" -> None
    | value -> Some value

let developerMode =
    match tryGetEnv "ASPNETCORE_ENVIRONMENT" with
    | None -> true
    | Some env -> strEquals env "development"
    |> DeveloperMode
