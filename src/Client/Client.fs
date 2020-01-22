module Client

open Elmish
open Elmish.Navigation
open Elmish.UrlParser
open Thoth.Fetch

open Shared
open Example

type Route =
    | ExampleRoute of Something: string

let initialCounter () = Fetch.fetchAs<Counter> "/api/init"

// defines the initial state and initial command (= side-effect) of the application
let init (result: Route option) : Model * Cmd<Msg> =
    let initialModel = { Counter = None }
    let loadCountCmd =
        Cmd.OfPromise.perform initialCounter () InitialCountLoaded
    initialModel, loadCountCmd

// The update function computes the next state of the application based on the current state and the incoming events/messages
// It can also run side-effects (encoded as commands) like calling the server via Http.
// these commands in turn, can dispatch messages to which the update function will react.
let update (msg : Msg) (currentModel : Model) : Model * Cmd<Msg> =
    match currentModel.Counter, msg with
    | Some counter, Increment ->
        let nextModel = { currentModel with Counter = Some { Value = counter.Value + 1 } }
        nextModel, Cmd.none
    | Some counter, Decrement ->
        let nextModel = { currentModel with Counter = Some { Value = counter.Value - 1 } }
        nextModel, Cmd.none
    | _, InitialCountLoaded initialCount->
        let nextModel = { Counter = Some initialCount }
        nextModel, Cmd.none
    | _ -> currentModel, Cmd.none

let urlUpdate (result:Option<Route>) (model: Model) =
    match result with
    | Some (ExampleRoute smth) -> model, Cmd.none
    | None -> model, Navigation.modifyUrl "#" // no matching route - go home

let route : Parser<Route -> Route, _> =
    oneOf [
            map ExampleRoute (s "eg" </> str)
          ]


let urlParser location = parseHash route location

open Elmish.React
open Fable.React

#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif
Program.mkProgram init update Example.Say.view
#if DEBUG
// with explicit parameters it points to some mismatch between Elmish and
// Fable.Elmish (?)
//|> Program.toNavigable<Route option, Model, Msg, ReactElement> urlParser urlUpdate
|> Program.toNavigable urlParser urlUpdate // <- shows an error but compiles in dotnet and fable
|> Program.withConsoleTrace
#endif
|> Program.withReactBatched "elmish-app" // <- shows an error but compiles in dotnet and fable
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
