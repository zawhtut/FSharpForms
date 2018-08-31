namespace FSharpForms

open Xamarin.Forms

type App() =
    inherit Application()

    let mutable count:int = 1

    let stack = StackLayout(VerticalOptions = LayoutOptions.Center)
    let label = Label(XAlign = TextAlignment.Center, Text = "Welcome to F# Xamarin.Forms!")
    let button = Button(Text="Hello World, Click Me!")
    do
        stack.Children.Add(label)
        stack.Children.Add(button)
        base.MainPage <- ContentPage(Content = stack)
        
        button.Clicked.Add(fun args -> 
            button.Text <- sprintf "%d clicks!" count
            count <- count + 1
        )
    

