namespace Example


open Fulma
open Elmish
open Elmish.React
open Fable.React
open Fable.React.Props

open Shared

// The model holds data that you want to keep track of while the application is running
// in this case, we are keeping track of a counter
// we mark it as optional, because initially it will not be available from the client
// the initial value will be requested from server
type Model = { Counter: Counter option }

// The Msg type defines what events/actions can occur while the application is running
// the state of the application changes *only* in reaction to these events
type Msg =
    | Increment
    | Decrement
    | InitialCountLoaded of Counter


module Say =
    let hello name =
        printfn "Hello %s" name

    let demo = "blah"

    let safeComponents =
        let components =
            span [ ]
               [ a [ Href "https://github.com/SAFE-Stack/SAFE-template" ]
                   [ str "SAFE  "
                     str Version.template ]
                 str ", "
                 a [ Href "https://saturnframework.github.io" ] [ str "Saturn" ]
                 str ", "
                 a [ Href "http://fable.io" ] [ str "Fable" ]
                 str ", "
                 a [ Href "https://elmish.github.io" ] [ str "Elmish" ]
                 str ", "
                 a [ Href "https://fulma.github.io/Fulma" ] [ str "Fulma" ]

               ]

        span [ ]
            [ str "Version "
              strong [ ] [ str Version.app ]
              str " powered by: "
              components ]

    let button txt onClick =
        Button.button
            [ Button.IsFullWidth
              Button.Color IsPrimary
              Button.OnClick onClick ]
            [ str txt ]


    let show = function
        | { Counter = Some counter } -> string counter.Value
        | { Counter = None   } -> "Loading..."

    let view (model : Model) (dispatch : Msg -> unit) =
        div []
            [ Navbar.navbar [ Navbar.Color IsPrimary ]
                [ Navbar.Item.div [ ]
                    [ Heading.h2 [ ]
                        [ str ("SAFE Template " + demo)] ] ]

              Container.container []
                  [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                        [ Heading.h3 [] [ str ("Press buttons to manipulate counter: " + show model) ] ]
                    Columns.columns []
                        [ Column.column [] [ button "-" (fun _ -> dispatch Decrement) ]
                          Column.column [] [ button "+" (fun _ -> dispatch Increment) ] ] ]

              Footer.footer [ ]
                    [ Content.content [ Content.Modifiers [ Modifier.TextAlignment (Screen.All, TextAlignment.Centered) ] ]
                        [ safeComponents ] ] ]
