module Router

open Falco
open Handlers

let message = "Hello, world!"

let handleJson = Response.ofJson {| Message = message |}

let routes =
    [ get "/json" handleJson
      post "/matches" matchesHandler ]
