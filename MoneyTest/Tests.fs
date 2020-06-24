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
    Assert.True(Money.Franc(5).Equals(Money.Franc(5)))
    Assert.False(Money.Franc(5).Equals(Money.Franc(6)))

    Assert.False(Money.Franc(5).Equals(Money.Dollar(5)))

[<Fact>]
let ``DifferentClassEquality`` () =
    Assert.True(Money(10, "CHF").Equals(Franc(10, "CHF")))

[<Fact>]
let ``FrancMultiplication`` () =
    let five = Money.Franc(5)
    Assert.Equal<Money>(Money.Franc(10), five.Times(2))
    Assert.Equal<Money>(Money.Franc(15), five.Times(3))

[<Fact>]
let ``Currency`` () =
    Assert.Equal("USD", Money.Dollar(1).Currency())
    Assert.Equal("CHF", Money.Franc(1).Currency())
