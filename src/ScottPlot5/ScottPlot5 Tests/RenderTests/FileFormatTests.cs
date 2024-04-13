﻿namespace ScottPlotTests.RenderTests;

internal class FileFormatTests
{
    [Test]
    public void Test_Save_Bmp()
    {
        Plot plt = new();
        plt.Add.Signal(Generate.Sin(51));
        plt.Add.Signal(Generate.Cos(51));
        plt.SaveBmp("test-images/test_save.bmp", 200, 100);
    }

    [Test]
    public void Test_Save_Jpeg()
    {
        Plot plt = new();
        plt.Add.Signal(Generate.Sin(51));
        plt.Add.Signal(Generate.Cos(51));
        plt.SaveJpeg("test-images/test_save.jpg", 200, 100);
    }

    [Test]
    public void Test_Save_Png()
    {
        Plot plt = new();
        plt.Add.Signal(Generate.Sin(51));
        plt.Add.Signal(Generate.Cos(51));
        plt.SavePng("test-images/test_save.png", 200, 100);
    }

    [Test]
    public void Test_Save_Webp()
    {
        Plot plt = new();
        plt.Add.Signal(Generate.Sin(51));
        plt.Add.Signal(Generate.Cos(51));
        plt.SaveWebp("test-images/test_save.webp", 200, 100);
    }

    [Test]
    public void Test_Save_Html()
    {
        Plot plt = new();
        plt.Add.Signal(Generate.Sin(51));
        plt.Add.Signal(Generate.Cos(51));
        string img = plt.GetImageHtml(300, 200);
        string html = $"<html><body>{img}</body></html>";
        html.SaveTestString();
    }

    [Test]
    public void Test_Save_Svg()
    {
        Plot plt = new();
        plt.Add.Signal(Generate.Sin(51));
        plt.Add.Signal(Generate.Cos(51));
        plt.SaveSvg("test-images/test_save.svg", 400, 300);
    }
}
