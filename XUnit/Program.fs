// Learn more about F# at http://fsharp.org

// [x] テストユニットを呼び出す
// [] setUpを最初に呼び出す
// [] tearDownを最後に呼び出す
// [] テストが失敗しても呼び出す
// [] 複数のテストを呼び出す
// [] 収集したテスト結果を出力する

open System
open System.Reflection

type TestCase(name: string) =
    member this.Run () =
        let fn = this.GetType().GetMethod(name)
        fn.Invoke(this, [||]) |> ignore

and WasRun(name: string) =
    inherit TestCase(name)

    let mutable wasRun = false

    member _.WasRun with get() = wasRun

    member _.TestMethod () =
        wasRun <- true


type TestCaseTest(name: string) =
    inherit TestCase(name)

    member _.TestRunning() =
        let test = WasRun("TestMethod")
        assert (not test.WasRun)
        test.Run()
        assert test.WasRun


[<EntryPoint>]
let main argv =
    TestCaseTest("TestRunning").Run()
    0
