// Learn more about F# at http://fsharp.org
module Moneys

open System

// [] $5 + 10CHF = $10
// [x] $5 + $5 = $10

type IExpression = interface end

type Money(amount:int, currency:string) =
    let amount = amount

    interface IExpression

    member this.currency = currency

    static member Dollar(amount:int) = Money(amount, "USD")
    static member Franc(amount:int) = Money(amount, "CHF")

    member _.Amount
        with get() = amount

    member this.Currency () = currency

    member this.Times(multiplier: int) =
        Money(amount*multiplier, this.currency)

    member this.Plus(addend:Money) =
        Money(amount + addend.Amount, this.currency)

    override this.Equals(obj: Object) =
        match obj with
            | :? Money as money ->
                amount = money.Amount && this.Currency() = money.Currency()
            | _ -> false

type Bank() =
    member _.reduce(expr: IExpression, to_: string) =
        Money.Dollar(10)

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
