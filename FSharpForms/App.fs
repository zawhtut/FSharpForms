namespace FSharpForms

open System
open Xamarin.Forms
open FSharp.Data

type Company = JsonProvider<"https://autocomplete.clearbit.com/v1/companies/suggest?query=microsoft">

type App() =
  inherit Application()

  let stack = StackLayout(VerticalOptions = LayoutOptions.Start, Margin = Thickness(15., 30.))
  let input = Entry(Placeholder = "Enter a company name")
  let label = Label(XAlign = TextAlignment.Center, Text = "Welcome to F# Xamarin.Forms!")
  let image = Image(Aspect = Aspect.AspectFit)

  let search() = async {
    let url = sprintf "https://autocomplete.clearbit.com/v1/companies/suggest?query=%s" (Uri.EscapeUriString input.Text)
    let! json = Http.AsyncRequestString url
    let company = Company.Parse json |> Seq.head

    label.Text <- company.Name
    image.Source <- ImageSource.FromUri (Uri company.Logo)
  }

  do
    stack.Children.Add input
    stack.Children.Add label
    stack.Children.Add image
    base.MainPage <- ContentPage(Content = stack)

    input.Completed.Add (fun _ -> 
      search() |> Async.StartImmediate)