// Learn more about F# at http://fsharp.org

// [x] テストユニットを呼び出す
// [x] setUpを最初に呼び出す
// [] tearDownを最後に呼び出す
// [] テストが失敗しても呼び出す
// [] 複数のテストを呼び出す
// [] 収集したテスト結果を出力する

open System
open System.Reflection

[<AbstractClass>]
type TestCase(name: string) =
    abstract member Setup: unit -> unit

    member this.Run () =
        this.Setup()
        let fn = this.GetType().GetMethod(name)
        fn.Invoke(this, [||]) |> ignore


type WasRun(name: string) =
    inherit TestCase(name)

    let mutable wasRun = false
    let mutable wasSetup = false

    override _.Setup () =
        wasRun <- false
        wasSetup <- true

    member _.WasRun with get() = wasRun
    member _.WasSetup with get() = wasSetup

    member _.TestMethod () =
        wasRun <- true


type TestCaseTest(name: string) =
    inherit TestCase(name)

    let mutable test = WasRun("")

    override _.Setup () =
        test <- WasRun("TestMethod")

    member _.TestRunning() =
        test.Run()
        assert test.WasRun

    member _.TestSetup() =
        test.Run()
        assert test.WasSetup


[<EntryPoint>]
let main argv =
    TestCaseTest("TestRunning").Run()
    TestCaseTest("TestSetup").Run()
    0
