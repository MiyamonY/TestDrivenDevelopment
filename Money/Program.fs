// Learn more about F# at http://fsharp.org
module Money

open System

// []  $5 + 10CHF = $10
// [x] $5 * 2  = $10
// []  amountをprivateに
// [x] Dollarの副作用
// []  Moneyの丸め処理
// [] struct(to value object)

type Dollar =
    struct
        val amount: int
        new(amount: int) = {amount = amount}

        member this.Amount
          with get() = this.amount

        member this.Times(multiplier: int) =
              Dollar(this.amount*multiplier)
    end

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
