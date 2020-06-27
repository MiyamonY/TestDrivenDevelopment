// Learn more about F# at http://fsharp.org

// [x] テストユニットを呼び出す
// [x] setUpを最初に呼び出す
// [x] tearDownを最後に呼び出す
// [] テストが失敗しても呼び出す
// [] 複数のテストを呼び出す
// [] 収集したテスト結果を出力する
// [x] WasRunで文字列をログに記録する
// [ ] 失敗したテストを出力する

open System
open System.Reflection

type TestResult() =
    let mutable count = 0

    member _.TestStarted () =
        count <- count + 1

    member _.Summary
        with get () = sprintf "%d run, 0 failed" count

[<AbstractClass>]
type TestCase(name: string) =
    abstract member Setup: unit -> unit
    default _.Setup () = ()

    abstract member TearDown: unit -> unit
    default _.TearDown () = ()

    member this.Run () =
        let result = TestResult()
        result.TestStarted ()

        this.Setup()
        let fn = this.GetType().GetMethod(name)
        fn.Invoke(this, [||]) |> ignore
        this.TearDown()

        result

type WasRun(name: string) =
    inherit TestCase(name)

    let mutable log = []
    member _.Log with get() = List.rev log

    override _.Setup () =
        log <- "Setup"::log

    member _.TestMethod () =
        log <- "TestMethod"::log

    member _.TestBokenMethod () =
        raise (Exception())

    override _.TearDown() =
        log <- "TearDown"::log


type TestCaseTest(name: string) =
    inherit TestCase(name)

    member _.TestTemplateMethod() =
        let test = WasRun("TestMethod")
        test.Run() |> ignore
        assert (["Setup"; "TestMethod"; "TearDown"] = test.Log)

    member _.TestResult () =
        let test = WasRun("TestMethod")
        let result = test.Run()
        assert ("1 run, 0 failed" =  result.Summary)

    member _.TestFailedResult()  =
        let test = WasRun("TestBrokenMethod")
        let result = test.Run()
        assert ("1 run, 1 faild" = result.Summary)

[<EntryPoint>]
let main argv =
    TestCaseTest("TestTemplateMethod").Run() |> ignore
    TestCaseTest("TestResult").Run() |> ignore
    // TestCaseTest("TestFailedResult").Run() |> ignore
    0
