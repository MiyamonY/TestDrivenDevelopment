// Learn more about F# at http://fsharp.org
module Money

open System

// []  $5 + 10CHF = $10
// [x] $5 * 2  = $10
// [x]  amountをprivateに
// [x] Dollarの副作用
// []  Moneyの丸め処理
// [x] struct(to value object)
// [x] 5CHF*2 = 10CHF
// [] DollarとFrancの重複
// [x] equalの一般化
// [] timesの一般化
// [] hashCode()
// [] DollarとFrancの比較

type Money(amount:int) =
    let amount = amount

    member _.Amount
        with get() = amount

    override _.Equals(obj: Object) =
        match obj with
            | :? Money as money -> amount = money.Amount
            | _ -> false

type Dollar(amount:int) =
    inherit Money(amount)


    member this.Times(multiplier: int) =
              Dollar(amount*multiplier)


type Franc(amount: int) =
    inherit Money(amount)

    member this.Times(multiplier: int) =
              Franc(amount*multiplier)

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
