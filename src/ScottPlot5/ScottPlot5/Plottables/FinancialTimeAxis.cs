﻿namespace ScottPlot.Plottables;

/// <summary>
/// This plottable renders date tick labels for financial charts where
/// data is displayed sequentially along the horizontal axis despite
/// DateTimes not being evenly spaced (e.g., data may include gaps)
/// </summary>
public class FinancialTimeAxis(DateTime[] dateTimes) : IPlottable
{
    public DateTime[] DateTimes { get; set; } = dateTimes;
    public bool IsVisible { get; set; } = true;
    public IAxes Axes { get; set; } = new Axes();
    public IEnumerable<LegendItem> LegendItems => LegendItem.None;

    public LabelStyle LabelStyle { get; set; } = new()
    {
        FontSize = 14,
        Alignment = Alignment.UpperCenter,
    };

    public AxisLimits GetAxisLimits() => AxisLimits.NoLimits;

    public virtual void Render(RenderPack rp)
    {
        if (DateTimes.Length == 0)
            return;

        // allow drawing outside the data area
        rp.CanvasState.DisableClipping();

        // get the best tick generator given the field of view
        int minIndexInView = (int)(Math.Max(0, Axes.XAxis.Range.Min));
        int maxIndexInView = (int)(Math.Min(DateTimes.Length - 1, Axes.XAxis.Range.Max));
        if (maxIndexInView <= minIndexInView) return;
        TimeSpan timeSpanInView = DateTimes[maxIndexInView] - DateTimes[minIndexInView];
        IFinancialTickGenerator tickGenerator = GetBestTickGenerator(timeSpanInView, rp.DataRect.Width);
        List<(int, string)> ticks = tickGenerator.GetTicks(DateTimes, minIndexInView, maxIndexInView);

        // render each tick label
        using SKPaint paint = new();
        foreach ((int x, string label) in ticks)
        {
            Pixel px = new(Axes.XAxis.GetPixel(x, rp.DataRect), rp.DataRect.Bottom);
            LabelStyle.Render(rp.Canvas, px, paint, label);
        }
    }

    private static IFinancialTickGenerator GetBestTickGenerator(TimeSpan timeSpan, float widthInPixels)
    {
        // adjust the scale so small plots show fewer ticks
        double scaledViewDays = timeSpan.TotalDays * 600 / widthInPixels;

        if (scaledViewDays < 180)
        {
            return new TickGenerators.Financial.MonthsAndMondays();
        }
        else if (scaledViewDays < 360 * 2)
        {
            return new TickGenerators.Financial.Months();
        }
        else
        {
            return new TickGenerators.Financial.Years();
        }
    }
}
