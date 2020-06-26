// Learn more about F# at http://fsharp.org

// [x] テストユニットを呼び出す
// [x] setUpを最初に呼び出す
// [x] tearDownを最後に呼び出す
// [] テストが失敗しても呼び出す
// [] 複数のテストを呼び出す
// [] 収集したテスト結果を出力する
// [x] WasRunで文字列をログに記録する

open System
open System.Reflection

[<AbstractClass>]
type TestCase(name: string) =
    abstract member Setup: unit -> unit
    default _.Setup () = ()

    abstract member TearDown: unit -> unit
    default _.TearDown () = ()

    member this.Run () =
        this.Setup()
        let fn = this.GetType().GetMethod(name)
        fn.Invoke(this, [||]) |> ignore
        this.TearDown()

type WasRun(name: string) =
    inherit TestCase(name)

    let mutable log = []
    member _.Log with get() = List.rev log

    override _.Setup () =
        log <- "Setup"::log

    member _.TestMethod () =
        log <- "TestMethod"::log

    override _.TearDown() =
        log <- "TearDown"::log

type TestCaseTest(name: string) =
    inherit TestCase(name)

    member _.TestTemplateMethod() =
        let test = WasRun("TestMethod")
        test.Run()
        assert (test.Log = ["Setup"; "TestMethod"; "TearDown"])

[<EntryPoint>]
let main argv =
    TestCaseTest("TestTemplateMethod").Run()
    0
