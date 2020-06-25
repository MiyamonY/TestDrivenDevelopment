// Learn more about F# at http://fsharp.org
module Moneys

open System

// [] $5 + 10CHF = $10
// [] $5 + $5 = $10
// [] $5+$5がMoneyをかえす
// [x] Bank.reduce(Money)
// [] Moneyを変換して換算を行う
// [] Reduce(Bank, string)

type IExpression =
    abstract member Reduce : string -> Money

and Money(amount:int, currency:string) =
    let amount = amount

    interface IExpression with
        member this.Reduce(to_: string) = this

    member this.currency = currency

    static member Dollar(amount:int) = Money(amount, "USD")
    static member Franc(amount:int) = Money(amount, "CHF")

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

and Sum(augend: Money, addend:Money) =
    let augend = augend
    let addend = addend

    interface IExpression with
        member _.Reduce (to_: string) =
            Money(augend.Amount + addend.Amount, to_)

    member _.Augend
        with get() = augend

    member _.Addend
        with get() = addend

type Bank() =
    member _.Reduce(expr: IExpression, to_: string) =
        expr.Reduce(to_)

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
