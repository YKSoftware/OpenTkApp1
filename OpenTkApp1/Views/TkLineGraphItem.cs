using System.Windows;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Linq;
using System;

namespace OpenTkApp1.Views;

/// <summary>
/// 折れ線グラフを描画する機能を提供します。
/// </summary>
public class TkLineGraphItem : FrameworkElement, ITkGraphicsItem
{
    #region XData

    /// <summary>
    /// XData 依存関係プロパティの定義を表します。
    /// </summary>
    public static readonly DependencyProperty XDataProperty = DependencyProperty.Register("XData", typeof(double[]), typeof(TkLineGraphItem), new PropertyMetadata(null, OnXDataPropertyChanged));

    /// <summary>
    /// 横軸データを取得または設定します。
    /// </summary>
    public double[]? XData
    {
        get => (double[]?)GetValue(XDataProperty);
        set => SetValue(XDataProperty, value);
    }

    /// <summary>
    /// XData プロパティ変更イベントハンドラ
    /// </summary>
    /// <param name="d">イベント発行元</param>
    /// <param name="e">イベント引数</param>
    private static void OnXDataPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion XData

    #region YData

    /// <summary>
    /// YData 依存関係プロパティの定義を表します。
    /// </summary>
    public static readonly DependencyProperty YDataProperty = DependencyProperty.Register("YData", typeof(double[]), typeof(TkLineGraphItem), new PropertyMetadata(null, OnYDataPropertyChanged));

    /// <summary>
    /// 縦軸データを取得または設定します。
    /// </summary>
    public double[]? YData
    {
        get => (double[]?)GetValue(YDataProperty);
        set => SetValue(YDataProperty, value);
    }

    /// <summary>
    /// YData プロパティ変更イベントハンドラ
    /// </summary>
    /// <param name="d">イベント発行元</param>
    /// <param name="e">イベント引数</param>
    private static void OnYDataPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion YData

    #region XScale
    public static readonly DependencyProperty XScaleProperty = DependencyProperty.Register("XScale", typeof(double), typeof(TkLineGraphItem), new PropertyMetadata(0.0, OnXScalePropertyChanged));

    public double XScale
    {
        get => (double)GetValue(XScaleProperty);
        set => SetValue(XScaleProperty, value);
    }

    private static void OnXScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion XScale

    #region YScale
    public static readonly DependencyProperty YScaleProperty = DependencyProperty.Register("YScale", typeof(double), typeof(TkLineGraphItem), new PropertyMetadata(0.0, OnYScalePropertyChanged));

    public double YScale
    {
        get => (double)GetValue(YScaleProperty);
        set => SetValue(YScaleProperty, value);
    }

    private static void OnYScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }


    #endregion YScale

    #region XMax
    public static readonly DependencyProperty XMaxProperty = DependencyProperty.Register("XMax", typeof(double), typeof(TkLineGraphItem), new PropertyMetadata(0.0, OnXMaxPropertyChanged));

    public double XMax
    {
        get => (double)GetValue(XMaxProperty);
        set => SetValue(XMaxProperty, value);
    }

    private static void OnXMaxPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion XMax

    #region XMin
    public static readonly DependencyProperty XMinProperty = DependencyProperty.Register("XMin", typeof(double), typeof(TkLineGraphItem), new PropertyMetadata(0.0, OnXMinPropertyChanged));

    public double XMin
    {
        get => (double)GetValue(XMinProperty);
        set => SetValue(XMinProperty, value);
    }

    private static void OnXMinPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion XMin

    #region YMax
    public static readonly DependencyProperty YMaxProperty = DependencyProperty.Register("YMax", typeof(double), typeof(TkLineGraphItem), new PropertyMetadata(0.0, OnYMaxPropertyChanged));

    public double YMax
    {
        get => (double)GetValue(YMaxProperty);
        set => SetValue(YMaxProperty, value);
    }

    private static void OnYMaxPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion YMax

    #region YMin
    public static readonly DependencyProperty YMinProperty = DependencyProperty.Register("YMin", typeof(double), typeof(TkLineGraphItem), new PropertyMetadata(0.0, OnYMinPropertyChanged));

    public double YMin
    {
        get => (double)GetValue(YMinProperty);
        set => SetValue(YMinProperty, value);
    }

    private static void OnYMinPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion YMin

    #region YCenter
    public static readonly DependencyProperty YCenterProperty = DependencyProperty.Register("YCenter", typeof(double), typeof(TkLineGraphItem), new PropertyMetadata(0.0, OnYCenterPropertyChanged));

    public double YCenter
    {
        get => (double)GetValue(YCenterProperty);
        set => SetValue(YCenterProperty, value);
    }

    private static void OnYCenterPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion YCenter

    #region XRange
    public static readonly DependencyProperty XRangeProperty = DependencyProperty.Register("XRange", typeof(double), typeof(TkLineGraphItem), new PropertyMetadata(0.0, OnXRangePropertyChanged));

    public double XRange
    {
        get => (double)GetValue(XRangeProperty);
        set => SetValue(XRangeProperty, value);
    }

    private static void OnXRangePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion XRange

    #region YRange
    public static readonly DependencyProperty YRangeProperty = DependencyProperty.Register("YRange", typeof(double), typeof(TkLineGraphItem), new PropertyMetadata(0.0, OnYRangePropertyChanged));

    public double YRange
    {
        get => (double)GetValue(YRangeProperty);
        set => SetValue(YRangeProperty, value);
    }

    private static void OnYRangePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion YRange

    #region PlotSize
    public static readonly DependencyProperty PlotSizeProperty = DependencyProperty.Register("PlotSize", typeof(double), typeof(TkLineGraphItem), new PropertyMetadata(0.0, OnPlotSizePropertyChanged));

    public double PlotSize
    {
        get => (double)GetValue(PlotSizeProperty);
        set => SetValue(PlotSizeProperty, value);
    }

    private static void OnPlotSizePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion PlotSize

    #region PlotType
    public static readonly DependencyProperty PlotTypeProperty = DependencyProperty.Register("PlotType", typeof(MarkerTypes), typeof(TkLineGraphItem), new PropertyMetadata(MarkerTypes.Ellipse, OnPlotTypePropertyChanged));

    public MarkerTypes PlotType
    {
        get => (MarkerTypes)GetValue(PlotTypeProperty);
        set => SetValue(PlotTypeProperty, value);
    }

    private static void OnPlotTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion PlotType

    #region PlotColor
    public static readonly DependencyProperty PlotColorProperty = DependencyProperty.Register("PlotColor", typeof(Color4), typeof(TkLineGraphItem), new PropertyMetadata(Color4.White, OnPlotColorPropertyChanged));

    public Color4 PlotColor
    {
        get => (Color4)GetValue(PlotColorProperty);
        set => SetValue(PlotColorProperty, value);
    }

    private static void OnPlotColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion PlotColor

    #region ISPlot
    public static readonly DependencyProperty IsPlotProperty = DependencyProperty.Register("IsPlot", typeof(bool), typeof(TkLineGraphItem), new PropertyMetadata(false, OnIsPlotPropertyChanged));

    public bool IsPlot
    {
        get => (bool)GetValue(IsPlotProperty);
        set => SetValue(IsPlotProperty, value);
    }

    private static void OnIsPlotPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion IsPlot

    /// <summary>
    /// 描画処理をおこないます。
    /// </summary>

    public void Render()
    {
        if (this.XData is null) return;
        if (this.YData is null) return;            
        if (XScale == 0) return;
        if (YScale == 0) return;
        if (XRange == 0) return;
        if (YRange == 0) return;

        // ToDo: XData と YData を用いてグラフを描画する

        GL.ClearColor(Color4.Black);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); 

        GL.Color4(Color4.White);

        GL.PushMatrix();
        {
            // 左端から描画するために移動
            GL.Translate(-((XMax - XMin)/2), 0, 0);
            // グラフ点描画
            DrawPlot(PlotSize, PlotType, PlotColor);
            // グラフ線描画
            DrawGraph();
            // 目盛り線描画
            DrawScale();
        }
        GL.PopMatrix();

    }

    //グラフ線描画
    private void DrawGraph()
    {

        GL.Begin(PrimitiveType.LineStrip);
        {
            for(int i = 0; i < XData.Length; i++)
            // 描画領域に合わせて平行移動する必要がある
            GL.Vertex2(XData[i] - XMin, YData[i] - YCenter);
        }
        GL.End();
        
    }

    private void DrawPlot(double size, MarkerTypes type, Color4 color)
    {
        if (IsPlot == false) return;

        GL.Color4(color);

        //float _pointSize = 5.0f;
        //GL.PointSize(_pointSize);
        //GL.Begin(PrimitiveType.Points);
        //{
        //    for (int i = 0; i < XData.Length; i++)
        //        // 描画領域に合わせて平行移動する必要がある
        //        GL.Vertex2(XData[i] - XMin, YData[i] - YCenter);
        //}
        //GL.End();

        double halfsize = size / 2;

        switch (type)
        {
            case MarkerTypes.Ellipse:
                DrawEllipsePlot(size);
                break;

            case MarkerTypes.Triangle:
                DrawTrianglePlot(halfsize);
                break;

            case MarkerTypes.Rectangle:
                DrawRectanglePlot(halfsize);
                break;

            case MarkerTypes.InvertedTriangle:
                DrawInvertedTrianglePlot(halfsize);
                break;
        }

        GL.Color4(Color4.White);
    }

    // 丸形プロット
    private void DrawEllipsePlot(double size)
    {
        // 分割数
        int division = 40;
        double delta = 2.0 * Math.PI / (double)division;
        // プロットする図形に歪みが出ないようにアスペクト比をかける。
        double aspectRatio = (double)XRange / YRange;
        double xSize = size * aspectRatio;

        GL.Begin(PrimitiveType.Triangles);
        {
            for (int i = 0; i < XData.Length; i++)
            {
                for (int j = 0; j <= division; j++)
                {
                    double x1 = XData[i] - XMin + (xSize * Math.Cos(delta * j));
                    double y1 = YData[i] - YCenter + (size * Math.Sin(delta * j));
                    double x2 = XData[i] - XMin + (xSize * Math.Cos(delta * j+1));
                    double y2 = YData[i] - YCenter + (size * Math.Sin(delta * j+1));
                    GL.Vertex2(XData[i] - XMin, YData[i] - YCenter);
                    GL.Vertex2(x1, y1);
                    GL.Vertex2(x2, y2);
                }
            }
        }
        GL.End();
    }

    // 三角形プロット
    private void DrawTrianglePlot(double halfsize)
    {
        // プロットする図形に歪みが出ないようにアスペクト比をかける。
        double aspectRatio = (double)XRange / YRange;
        double halfXEdge = halfsize * aspectRatio;
        double halfYEdge = halfsize * Math.Sqrt(3);

        GL.Begin(PrimitiveType.Triangles);
        {
            for (int i = 0; i < XData.Length; i++)
            // 描画領域に合わせて平行移動する必要がある
            {
                GL.Vertex2(XData[i] - XMin - halfXEdge, YData[i] - YCenter - halfYEdge);
                GL.Vertex2(XData[i] - XMin + halfXEdge, YData[i] - YCenter - halfYEdge);
                GL.Vertex2(XData[i] - XMin, YData[i] - YCenter + Math.Sqrt(3) * halfYEdge);
            }
        }
        GL.End();
    }

    // 正方形プロット
    private void DrawRectanglePlot(double halfsize)
    {
        // プロットする図形に歪みが出ないようにアスペクト比をかける。
        double aspectRatio = (double)XRange / YRange;
        double halfXEdge = halfsize * aspectRatio;
        double halfYEdge = halfsize;

        GL.Begin(PrimitiveType.Quads);
        {
            for (int i = 0; i < XData.Length; i++)
            // 描画領域に合わせて平行移動する必要がある
            {
                GL.Vertex2(XData[i] - XMin - halfXEdge, YData[i] - YCenter - halfYEdge);
                GL.Vertex2(XData[i] - XMin + halfXEdge, YData[i] - YCenter - halfYEdge);
                GL.Vertex2(XData[i] - XMin + halfXEdge, YData[i] - YCenter + halfYEdge);
                GL.Vertex2(XData[i] - XMin - halfXEdge, YData[i] - YCenter + halfYEdge);
            }
        }
        GL.End();
    }

    // 逆三角形プロット
    private void DrawInvertedTrianglePlot(double halfsize)
    {
        // プロットする図形に歪みが出ないようにアスペクト比をかける。
        double aspectRatio = (double)XRange / YRange;
        double halfXEdge = halfsize * aspectRatio;
        double halfYEdge = halfsize * Math.Sqrt(3);

        GL.Begin(PrimitiveType.Triangles);
        {
            for (int i = 0; i < XData.Length; i++)
            // 描画領域に合わせて平行移動する必要がある
            {
                GL.Vertex2(XData[i] - XMin - halfXEdge, YData[i] - YCenter + halfYEdge);
                GL.Vertex2(XData[i] - XMin + halfXEdge, YData[i] - YCenter + halfYEdge);
                GL.Vertex2(XData[i] - XMin, YData[i] - YCenter - halfYEdge);
            }
        }
        GL.End();
    }

    // 目盛り線描画
    private void DrawScale()
    {
        // 点線描画ON
        GL.Enable(EnableCap.LineStipple);
        // 破線の形状を決める
        GL.LineStipple(1, 0xF0F0);
        GL.Begin(PrimitiveType.Lines);
        {
            double _xCurrentPosition = 0;
            double _yCurrentPosition = 0;

            while (_xCurrentPosition <= XMax)
            {
                // 描画領域に合わせて平行移動する必要がある
                GL.Vertex2(_xCurrentPosition, YMin - YCenter);
                GL.Vertex2(_xCurrentPosition, YMax - YCenter);
                _xCurrentPosition += XScale;
            }

            while (_yCurrentPosition <= YMax - YCenter)
            {
                GL.Vertex2(0, _yCurrentPosition);
                GL.Vertex2(XMax,_yCurrentPosition);
                _yCurrentPosition += YScale;
            }

            _yCurrentPosition = 0;
            while (_yCurrentPosition >= YMin - YCenter)
            {
                GL.Vertex2(0, _yCurrentPosition);
                GL.Vertex2(XMax, _yCurrentPosition);
                _yCurrentPosition -= YScale;
            }
        }
        GL.End();
        // 点線描画OFF ※これをしないと描画したもの全て点線になる
        GL.Disable(EnableCap.LineStipple);
    }
    

}