open System
open System.Drawing
open System.Windows.Forms

let K = 1.1/600.0
let X0 = 0.3                        //начальное значение x
let Row = [|for i in 0..800 -> 0|]  //массив с нулями
let mutable bit = new Bitmap(1,1)   //графика
let round (x:float) = Convert.ToInt32(System.Math.Round(x))

let DrawFrac wid hei =
    for j = 0 to 599 do
        let R = 2.9 + (float)j*K
        let mutable X = X0

        for i = 1 to 200 do
            X <- X*R*(1.0-X)

        for i = 1 to 10000 do
            X <- X*R*(1.0-X)
            let f = X*800.0
            Row.[round f] <- Row.[round f] + 1

        for i = 0 to 799 do
            let mutable f = (float)Row.[i]/100.0*255.0

            if f < 120.0 then 
                if f > 0.0 then f <- f * 2.0

            if f > 255.0 then f <- 255.0
            Row.[i] <- 0
            bit.SetPixel(i, j, Color.FromArgb(round f, 0, 0, 0))
let form =
    let temp = new Form()
    temp.Text <- "Дерево Фейгенбаума"
    temp.Height <- 600
    temp.Width <- 800

    temp.Paint.Add(fun e -> bit<- new Bitmap(temp.Width, temp.Height))
    temp.Paint.Add(fun e -> DrawFrac temp.Width temp.Height)
    temp.Paint.Add(fun e -> temp.CreateGraphics().DrawImage(bit, 0, 0))

    temp
[<STAThread>]
do Application.Run(form)