[<RequireQualifiedAccess>]
module Repro.Example

type MyLocalType = {
    X: int
}

// add an annotation to mlt by adding
// (mlt: MyLo and it will incorrectly autocomplete in Rider22.1
let printLocalType mlt =
    printf $"%i{mlt.X}"