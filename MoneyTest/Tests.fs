module Tests

open System
open Xunit
open Money

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
let ``FrancMultiplication`` () =
    let five = Money.Franc(5)
    Assert.Equal<Money>(Franc(10), five.Times(2))
    Assert.Equal<Money>(Franc(15), five.Times(3))
