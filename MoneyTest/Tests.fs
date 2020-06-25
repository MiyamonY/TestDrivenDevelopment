module Tests

open System
open Xunit
open Moneys

[<Fact>]
let ``Multiplication`` () =
    let five = Money.Dollar(5)
    Assert.Equal<Money>(Money.Dollar(10), five.Times(2))
    Assert.Equal<Money>(Money.Dollar(15), five.Times(3))

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
    let result = five.Plus(five)
    Assert.Equal(five, result.Augend)
    Assert.Equal(five, result.Addend)

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
