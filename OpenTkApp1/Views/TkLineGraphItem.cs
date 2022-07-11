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

    /// <summary>
    /// 描画処理をおこないます。
    /// </summary>
    /// 
    // 後で修正する
    private int _xScale = 100;

    private int _yScale = 30;
 

    public void Render()
    {
        if (this.XData is null) return;
        if (this.YData is null) return;

        // ToDo: XData と YData を用いてグラフを描画する

        GL.ClearColor(Color4.Black);

        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); 

        GL.Color4(Color4.White);

        GL.PushMatrix();
        {
            GL.Translate(-(TkGraphics.XRange / 2), 0, 0);
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
            GL.Vertex2(XData[i], YData[i]);
            
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
            var _xCurrentPosition = _xScale;
            var _yCurrentPosition = -TkGraphics.YRange;

            while (_xCurrentPosition <= TkGraphics.XRange)
            {
                GL.Vertex2(_xCurrentPosition, -(TkGraphics.YRange));
                GL.Vertex2(_xCurrentPosition, TkGraphics.YRange);
                _xCurrentPosition += _xScale;
            }

            while (_yCurrentPosition <= TkGraphics.YRange)
            {
                GL.Vertex2(0, _yCurrentPosition);
                GL.Vertex2(TkGraphics.XRange,_yCurrentPosition);
                _yCurrentPosition += _yScale;
            }
        }
        GL.End();
        // 点線描画OFF ※これをしないと描画したもの全て点線になる
        GL.Disable(EnableCap.LineStipple);
    }

    // (x1,y1)から(x2,y1)にx軸 (x1,y1)から(x1,y2)にy軸を引く
    private void DrawAxisLine(double x1, double x2, double y1, double y2)
    {
        GL.PushMatrix();
        {
            // x軸描画
            GL.Begin(PrimitiveType.Lines);
            {
                GL.Vertex2(x1, y1);
                GL.Vertex2(x2, y1);
            }
            GL.End();
            
        }
        GL.PopMatrix();

        GL.PushMatrix();
        {
            // y軸描画
            GL.Begin(PrimitiveType.Lines);
            {
                GL.Vertex2(x1, y1);
                GL.Vertex2(x1, y2);
            }
            GL.End();

        }
        GL.PopMatrix();
    }


}