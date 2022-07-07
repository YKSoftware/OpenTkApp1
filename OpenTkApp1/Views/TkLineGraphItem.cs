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

    /// 後で修正する
    public static double[] _xdata = Enumerable.Range(0, _dataNum).Select(x => (double)x).ToArray();
    public static double[] _ydata = Enumerable.Range(0, _dataNum).Select(x => 100.0 * Math.Sin(2.0 * Math.PI * 4.0 * x / 1000.0)).ToArray();
    
    public const int _dataNum = 1000;
    public void Render()
    {
        //if (this.XData is null) return;
        //if (this.YData is null) return;

        // ToDo: XData と YData を用いてグラフを描画する

        GL.ClearColor(Color4.Black);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); 

        GL.Color4(Color4.White);
        GL.PushMatrix();
        {
            // 軸描画
            DrawAxisLine(-(_dataNum/2)+1, (_dataNum/2 - 1), (-(TkGraphics.YRange) / 2.5), TkGraphics.YRange / 2.5);

            GL.Translate(-(_dataNum/2)+1, 0, 0);
            // グラフ描画
            for (int i = 0; i <= (_dataNum - 2); i++)
            {
                DrawGraph(i);
                GL.PushMatrix();
                {
                    GL.Translate(_xdata[i], _ydata[i], 0);
                    Drawplot();
                }
                GL.PopMatrix();
            }
            GL.PopMatrix();
        }
    }
    private void DrawGraph(int count)
    {
        GL.Begin(PrimitiveType.Lines);
        {
            GL.Vertex2(_xdata[count], _ydata[count]);
            GL.Vertex2(_xdata[count+1], _ydata[count+1]);
            
            
        }
        GL.End();
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

    private void Drawplot()
    {
        // 点の半径
        double radius = 0.05;
        double delta = 2.0 * Math.PI / 100;
        GL.PushMatrix();
        // 点描画
        GL.Begin(PrimitiveType.TriangleFan);
        for(int i =0; i < 100; i++)
        {
            double x = ((radius * TkGraphics.AspectRatio)/TkGraphics.ActualAspectRatio) * Math.Cos(i * delta);
            double y = radius * Math.Sin(i * delta);
            GL.Vertex2(x, y);
        }
        GL.End();
        GL.PopMatrix();
    }

}