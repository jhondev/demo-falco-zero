module Handlers

open System.Threading.Tasks
open Falco
open FSharp.Control.Tasks.V2

[<CLIMutable>]
type Match = { name: string; desc: string }

let matchesHandler: HttpHandler =
    fun ctx ->
        task {
            let! ma = Request.tryBindJson<Match> ctx

            let respondWith =
                match ma with
                | Ok model -> Response.ofPlainText (sprintf "%A" model)
                | Error error -> Response.ofPlainText error

            return! respondWith ctx
        } :> Task

let notFoundHandler: HttpHandler =
    Response.withStatusCode 404
    >> Response.ofPlainText "Not found"
