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

    /// <summary>
    /// 描画処理をおこないます。
    /// </summary>

    public void Render()
    {
        if (this.XData is null) return;
        if (this.YData is null) return;            
        if (XScale == 0) return;
        if (YScale == 0) return;

        // ToDo: XData と YData を用いてグラフを描画する

        GL.ClearColor(Color4.Black);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); 

        GL.Color4(Color4.White);

        GL.PushMatrix();
        {
            // 左端から描画するために移動
            GL.Translate(-((XMax - XMin)/2), 0, 0);
            // グラフ描画
            DrawGraph();
            // 目盛り線描画
            DrawScale();
        }
        GL.PopMatrix();
    }
    private void DrawGraph()
    {

        GL.Begin(PrimitiveType.LineStrip);
        {
            for(int i = 0; i < XData.Length; i++)
            // 描画領域に合わせて平行移動する必要がある
            GL.Vertex2(XData[i] - XMin, YData[i] - YCenter);
            
        }
        GL.End();

        // 点描画
        float _pointSize = 2.2f;
        GL.PointSize(_pointSize);
        GL.Begin(PrimitiveType.Points);
        {
            for (int i = 0; i < XData.Length; i++)
            // 描画領域に合わせて平行移動する必要がある
            GL.Vertex2(XData[i] - XMin, YData[i] - YCenter);
        }
        GL.End();
        
    }
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