module Tests

open System
open Xunit
open Money

[<Fact>]
let ``Multiplication`` () =
    let five = Dollar(5)
    Assert.Equal(Dollar(10), five.Times(2))
    Assert.Equal(Dollar(15), five.Times(3))

[<Fact>]
let ``Equality`` () =
    Assert.Equal(Dollar(5), Dollar(5))
    Assert.NotEqual(Dollar(5), Dollar(6))
    Assert.Equal(Franc(5), Franc(5))
    Assert.NotEqual(Franc(5), Franc(6))

[<Fact>]
let ``FrancMultiplication`` () =
    let five = Franc(5)
    Assert.Equal(Franc(10), five.Times(2))
    Assert.Equal(Franc(15), five.Times(3))
