module Tests

open System
open Xunit
open Moneys

[<Fact>]
let ``Multiplication`` () =
    let five = Money.Dollar(5)
    Assert.Equal<IExpression>(Money.Dollar(10), five.Times(2))
    Assert.Equal<IExpression>(Money.Dollar(15), five.Times(3))

[<Fact>]
let ``Equality`` () =
    Assert.True(Money.Dollar(5).Equals(Money.Dollar(5)))
    Assert.False(Money.Dollar(5).Equals(Money.Dollar(6)))
    Assert.False(Money.Franc(5).Equals(Money.Dollar(5)))

[<Fact>]
let ``Currency`` () =
    Assert.Equal("USD", Money.Dollar(1).Currency())
    Assert.Equal("CHF", Money.Franc(1).Currency())

[<Fact>]
let ``SimpleAddition`` () =
    let five = Money.Dollar(5)
    let sum = five.Plus(five)
    let bank = Bank()
    let reduced = bank.Reduce(sum, "USD")
    Assert.Equal(Money.Dollar(10), reduced)

[<Fact>]
let ``PlusReturnSum`` () =
    let five = Money.Dollar(5)
    let result = five.Plus(five) :?> Sum
    Assert.Equal<IExpression>(five, result.Augend)
    Assert.Equal<IExpression>(five, result.Addend)

[<Fact>]
let ``ReduceMoney`` () =
    let one = Money.Dollar(1)
    let bank = Bank()
    let result = bank.Reduce(one, "USD")
    Assert.Equal(Money.Dollar(1), result)

[<Fact>]
let ``ReduceSum`` () =
    let sum = Money.Dollar(3).Plus(Money.Dollar(4))
    let bank = Bank()
    let result = bank.Reduce(sum, "USD")
    Assert.Equal(Money.Dollar(7), result)

[<Fact>]
let ``ReduceMoneyDifferentCurrency`` () =
    let bank = Bank()
    bank.AddRate("CHF", "USD", 2) |> ignore
    let result = bank.Reduce(Money.Franc(2), "USD")
    Assert.Equal(Money.Dollar(1), result)

[<Fact>]
let ``IdentityRate`` () =
    Assert.Equal(1, Bank().Rate("USD", "USD"))

[<Fact>]
let ``MixedAddition`` () =
    let fiveBucks = Money.Dollar(5)
    let tenFrancs = Money.Franc(10)
    let bank = Bank()
    bank.AddRate("CHF", "USD", 2)
    let result = bank.Reduce(fiveBucks.Plus(tenFrancs), "USD")
    Assert.Equal(Money.Dollar(10), result)

[<Fact>]
let ``SumPlusMoney`` () =
    let fiveBucks = Money.Dollar(5)
    let tenFrancs = Money.Franc(10)
    let bank = Bank()
    bank.AddRate("CHF", "USD", 2)
    let sum = (Sum(fiveBucks, tenFrancs) :> IExpression).Plus(fiveBucks)
    let result = bank.Reduce(sum, "USD")
    Assert.Equal(Money.Dollar(15), result)

[<Fact>]
let ``SumTimes`` () =
    let fiveBucks = Money.Dollar(5)
    let tenFrancs = Money.Franc(10)
    let bank = Bank()
    bank.AddRate("CHF", "USD", 2)
    let sum = (Sum(fiveBucks, tenFrancs) :> IExpression).Times(2)
    let result = bank.Reduce(sum, "USD")
    Assert.Equal(Money.Dollar(20), result)
