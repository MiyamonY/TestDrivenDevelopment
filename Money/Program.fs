// Learn more about F# at http://fsharp.org
module Moneys

open System

// [] $5 + 10CHF = $10
// [x] $5 + $5 = $10
// [] $5+$5がMoneyをかえす
// [x] Bank.reduce(Money)
// [x] Moneyを変換して換算を行う
// [x] Reduce(Bank, string)

type IExpression =
    abstract member Reduce : Bank*string -> Money

and Money(amount:int, currency:string) =
    interface IExpression with
        member this.Reduce(bank: Bank, to_: string) =
            let rate = bank.Rate(currency, to_)
            Money.Dollar(amount / rate)

    member this.currency = currency

    member _.Amount
        with get() = amount

    member this.Currency () = currency

    member this.Times(multiplier: int) =
        Money(amount*multiplier, this.currency)

    member this.Plus(addend:Money) =
        Sum(this, addend)

    override this.Equals(obj: Object) =
        match obj with
            | :? Money as money ->
                amount = money.Amount && this.Currency() = money.Currency()
            | _ -> false

    static member Dollar(amount:int) = Money(amount, "USD")
    static member Franc(amount:int) = Money(amount, "CHF")

and Sum(augend: Money, addend:Money) =
    interface IExpression with
        member _.Reduce (bank: Bank, to_: string) =
            Money(augend.Amount + addend.Amount, to_)

    member _.Augend
        with get() = augend

    member _.Addend
        with get() = addend

and Bank() =
    let mutable rates = Map []

    member this.Reduce(expr: IExpression, to_: string) =
        expr.Reduce(this, to_)

    member _.AddRate(from: string, to_: string, rate: int) =
        rates <- rates.Add((from, to_), rate)

    member _.Rate(from: string, to_: string) =
        if from = to_ then 1
        else rates.TryFind((from, to_)).Value

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
