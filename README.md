# Rider bug repo

This is a minimal example based on the SAFE template that reproduces an error in Rider.
The code compiles in both dotnet and fable, so this error is quite distracting because
it makes the project look broken.

To build use dotnet directly, or via tasks in package.json
```bash
$ dotnet tool restore
$ dotnet fake build
// or $ dotnet fake build --target run
```

The basic error that shows up in Rider:
![Screenshot](https://github.com/l3m/rider-error-repro/blob/master/error.png)

Adding explicit template parameters, it points to some mismatch between
Elmish and Fable.Elmish (?)
![Screenshot](https://github.com/l3m/rider-error-repro/blob/master/error-explicit-template-params.png)

