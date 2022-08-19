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
    /// <summary>
    /// 描画処理をおこないます。
    /// </summary>
    void Render();

    public double XMax { get; set; }
    
    public double XMin { get; set; }

    public double[] XData { get; set; }

    public double[] YData { get; set; }

    public double YCenter { get; set; }

    public Color4 LineColor { get; set; }
}
