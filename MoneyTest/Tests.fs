module Tests

open System
open Xunit
open Money

[<Fact>]
let ``Multiplication`` () =
    let five = Dollar(5)
    let product = five.Times(2)
    Assert.Equal(10, product.Amount)

    let product = five.Times(3)
    Assert.Equal(15, product.Amount)

[<Fact>]
let ``Equality`` () =
    Assert.Equal(Dollar(5), Dollar(5))
    Assert.NotEqual(Dollar(5), Dollar(6))
