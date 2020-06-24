// Learn more about F# at http://fsharp.org
module Moneys

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
// [x] DollarとFrancの比較
// [x] 通貨の概念
// [] testFanctMultiplicationを削除する？

[<AbstractClass>]
type Money(amount:int, currency:string) =
    let amount = amount
    let currency = currency

    static member Dollar(amount:int) = Dollar(amount, "USD") :> Money
    static member Franc(amount:int) = Franc(amount, "CHF") :> Money

    member _.Amount
        with get() = amount

    abstract Times : int -> Money

    member this.Currency () = currency

    override this.Equals(obj: Object) =
        match obj with
            | :? Money as money ->
                amount = money.Amount && this.GetType().Equals(money.GetType())
            | _ -> false

and Dollar(amount:int, currency: string) =
    inherit Money(amount, currency)

    override _.Times(multiplier: int) =
        Money.Dollar(amount*multiplier)

and Franc(amount: int, currency: string) =
    inherit Money(amount, currency)


    override this.Times(multiplier: int) =
        Money.Franc(amount*multiplier)

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
