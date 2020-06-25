// Learn more about F# at http://fsharp.org
module Moneys

open System

type IExpression =
    abstract member Reduce : Bank*string -> Money
    abstract member Plus : IExpression -> IExpression
    abstract member Times : int -> IExpression

and Money(amount:int, currency:string) =
    interface IExpression with
        member this.Reduce(bank: Bank, to_: string) =
            let rate = bank.Rate(currency, to_)
            Money.Dollar(amount / rate)
        member this.Plus(addend:IExpression) =
            Sum(this, addend)  :> IExpression
        member this.Times(multiplier: int) =
            Money(amount*multiplier, this.currency) :> IExpression

    member this.currency = currency

    member _.Amount
        with get() = amount

    member this.Currency () = currency

    member this.Plus(addend:IExpression) =
        (this :> IExpression).Plus(addend)

    member this.Times(multiplier: int) =
        (this :> IExpression).Times(multiplier)

    override this.Equals(obj: Object) =
        match obj with
            | :? Money as money ->
                amount = money.Amount && this.Currency() = money.Currency()
            | _ -> false

    static member Dollar(amount:int) = Money(amount, "USD")
    static member Franc(amount:int) = Money(amount, "CHF")

and Sum(augend: IExpression, addend: IExpression) =
    interface IExpression with
        member _.Reduce (bank: Bank, to_: string) =
            let amount = augend.Reduce(bank, to_).Amount  + addend.Reduce(bank, to_).Amount
            Money(amount, to_)
        member this.Plus (addend:IExpression) =
            Sum(this, addend) :> IExpression
        member _.Times(multiplier: int) =
            Sum(augend.Times(multiplier), addend.Times(multiplier)) :> IExpression

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
