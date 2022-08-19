using System.Windows;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Linq;
using System;

namespace OpenTkApp1.Views;

/// <summary>
/// OpenTK による描画をおこなう実装を表します。
/// </summary>
public interface ITkGraphicsItem 
{
    ///// <summary>
    ///// 描画処理をおこないます。
    ///// </summary>
    //void Render();

    public double[] XData { get; set; }

    public double[] YData { get; set; }

    public double XScale { get; set; }

    public double YScale { get; set; }

    public double XMax { get; set; }
    
    public double XMin { get; set; }

    public double YMax { get; set; }

    public double YMin { get; set; }

    public double YCenter { get; set; }

    public double XRange { get; set; }

    public double YRange { get; set; }

    public double PlotSize { get; set; }

    public MarkerTypes PlotType { get; set; }

    public Color4 PlotColor { get; set; }

    public bool IsPlot { get; set; }

    public Color4 LineColor { get; set; }
}
