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
// [x] DollarとFrancの比較
// [] 通貨の概念
// [] testFanctMultiplicationを削除する？

[<AbstractClass>]
type Money(amount:int) =
    let amount = amount

    let Dollar(amount:int) = Dollar(amount) :> Money
    let Franc(amount:int) = Franc(amount) :> Money

    member _.Amount
        with get() = amount

    abstract Times : int -> Money

    override this.Equals(obj: Object) =
        match obj with
            | :? Money as money ->
                amount = money.Amount && this.GetType().Equals(money.GetType())
            | _ -> false

and Dollar(amount:int) =
    inherit Money(amount)

    override this.Times(multiplier: int) =
        Dollar(amount*multiplier) :> Money

and Franc(amount: int) =
    inherit Money(amount)

    override this.Times(multiplier: int) =
        Franc(amount*multiplier) :> Money

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
