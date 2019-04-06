// Page 2 of F# for Fun and Profit — https://fsharpforfunandprofit.com/posts/fsharp-in-60-seconds/

let myInt = 5
let myFloat = 3.14
let myString = "hello"

let twoToFive = [2;3;4;5]
let oneToFive = 1 :: twoToFive
let zeroToFive = [0;1] @ twoToFive

let square x = x * x
square 3

let add x y = x + y
add 2 3

let evens list = 
    let isEven x = x%2 = 0
    List.filter isEven list

evens oneToFive

let sumOfSquaresTo100 = 
    List.sum ( List.map square [1..100] )

let sumOfSquaresTo100withFun =
    [1..100] |> List.map (fun x->x*x) |> List.sum

let simplePatternMatch =
    let x = "a"
    match x with
    | "a" -> printfn "x is a"
    | "b" -> printfn "x is b"
    | _ -> printfn "x is something else"

let validValue = Some(99)
let invalidValue = None

let optionPatternMatch input =
    match input with
        | Some i -> printfn "input is an int=%d" i
        | None -> printfn "input is missing"

optionPatternMatch validValue
optionPatternMatch invalidValue

// Tuples
let twoTuple = 1,2
let threeTuple = "a",2,true

// Record types
type Person = {First:string; Last:string}
let person1 = {First="john"; Last="Doe"}

// Union types are for choices
type Temp =
    | DegreesC of float
    | DegreesF of float
let temp = DegreesF 98.6

// Combining types recursively
type Employee =
    | Worker of Person
    | Manager of Employee list
let jdoe = {First="John";Last="Doe"}
let worker = Worker jdoe

// Printing
printfn "Printing an int %i, a float %f, a bool %b" 1 2.0 true
printfn "A string %s, and something generic %A" "hello" [1;2;3;4]

printfn "twoTuple=%A,\nPerson=%A,\nTemp=%A,\nEmployee=%A" twoTuple person1 temp worker

// Page 3

let sumOfSquares n =
    [1..n]
    |> List.map square
    |> List.sum

// Page 5
open System.Net
open System
open System.IO

// Fetch webpage contents
let fetchUrl callback url = 
    let req = WebRequest.Create(Uri(url))
    use resp = req.GetResponse() //'use' lets us dispose IDisposable when out of scope
    use stream = resp.GetResponseStream()
    use reader = new IO.StreamReader(stream)
    callback reader url

let myCallback (reader:IO.StreamReader) url =
    let html = reader.ReadToEnd()
    let html1000 = html.Substring(0,1000)
    printfn "Downloaded %s. First 1000 is %s" url html1000
    html // returns all the html

// test
let google = fetchUrl myCallback "https://google.com"

// 'baking in' the callback

let fetchURL2 = fetchUrl myCallback
let google2 = fetchURL2 "http://google.com"
let bbc     = fetchURL2 "http://news.bbc.co.uk"
let sites = ["http://bing.com";
             "http://google.com";
             "http://yahoo.com"]
sites |> List.map fetchURL2