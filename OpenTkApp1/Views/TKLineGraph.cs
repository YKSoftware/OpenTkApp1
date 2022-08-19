using System;
using System.Windows;
using System.Windows.Input;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Wpf;

namespace OpenTkApp1.Views
{
    public class TKLineGraph : FrameworkElement, ITkGraphBase
    {
        /// <summary>
        /// DrawingItem 依存関係プロパティの定義を表します。
        /// </summary>
        public static readonly DependencyProperty DrawingItemProperty = DependencyProperty.Register("DrawingItem", typeof(ITkGraphicsItem), typeof(TKLineGraph), new PropertyMetadata(null, OnDrawingItemPropertyChanged));

        /// <summary>
        /// 描画内容を取得または設定します。
        /// </summary>
        public ITkGraphicsItem? DrawingItem
        {
            get => (ITkGraphicsItem?)GetValue(DrawingItemProperty);
            set => SetValue(DrawingItemProperty, value);
        }

        /// <summary>
        /// DrawingItem 依存関係プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param>
        private static void OnDrawingItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (TKLineGraph)d;
            control.OnDrawingItemPropertyChanged(e.OldValue, e.NewValue);
        }

        /// <summary>
        /// DrawingItem 依存関係プロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="oldItem">変更前の値</param>
        /// <param name="newItem">変更後の値</param>
        private void OnDrawingItemPropertyChanged(object oldItem, object newItem)
        {
            RemoveLogicalChild(oldItem);
            AddLogicalChild(newItem);
        }

        #region AxisType
        public static readonly DependencyProperty AxisTypeProperty = DependencyProperty.Register("AxisType", typeof(AxisTypes), typeof(TKLineGraph), new PropertyMetadata(AxisTypes.Left, OnAxisTypePropertyChanged));

        public AxisTypes AxisType
        {
            get => (AxisTypes)GetValue(AxisTypeProperty);
            set => SetValue(AxisTypeProperty, value);
        }

        private static void OnAxisTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TKLineGraph)?.Render();
        }

        #endregion AxisType


        public void Render()
        {
            // 1回目のRenderが走るタイミングがBindingするより早いため
            if (DrawingItem.XData is null) return;
            if (DrawingItem.YData is null) return;
            if (DrawingItem.XScale == 0) return;
            if (DrawingItem.YScale == 0) return;

            GL.ClearColor(Color4.Black);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Color4(Color4.White);

            GL.PushMatrix();
            {
                // 左端から描画するために移動
                GL.Translate(-((DrawingItem.XMax - DrawingItem.XMin) / 2), 0, 0);
                // グラフ点描画
                DrawPlot(DrawingItem.PlotSize, DrawingItem.PlotType, DrawingItem.PlotColor);
                // グラフ線描画
                DrawGraph(DrawingItem.LineColor);
                //// 目盛り線描画
                DrawScale();
            }
            GL.PopMatrix();
        }

        /// <summary>
        /// グラフの点を描画するメソッドです。
        /// </summary>
        /// <param name="size"></param>
        /// <param name="type"></param>
        /// <param name="color"></param>
        private void DrawPlot(double size, MarkerTypes type, Color4 color)
        {
            if (DrawingItem.IsPlot == false) return;

            GL.Color4(color);

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

        /// <summary>
        /// プロットを丸型に設定するメソッドです。
        /// </summary>
        /// <param name="size"></param>
        private void DrawEllipsePlot(double size)
        {
            // 分割数
            int division = 40;
            double delta = 2.0 * Math.PI / (double)division;
            // プロットする図形に歪みが出ないようにアスペクト比をかける。
            double aspectRatio = (double)DrawingItem.XRange / DrawingItem.YRange;
            double xSize = size * aspectRatio;

            GL.Begin(PrimitiveType.Triangles);
            {
                for (int i = 0; i < DrawingItem.XData.Length; i++)
                {
                    for (int j = 0; j <= division; j++)
                    {
                        double x1 = DrawingItem.XData[i] - DrawingItem.XMin + (xSize * Math.Cos(delta * j));
                        double y1 = DrawingItem.YData[i] - DrawingItem.YCenter + (size * Math.Sin(delta * j));
                        double x2 = DrawingItem.XData[i] - DrawingItem.XMin + (xSize * Math.Cos(delta * j + 1));
                        double y2 = DrawingItem.YData[i] - DrawingItem.YCenter + (size * Math.Sin(delta * j + 1));
                        GL.Vertex2(DrawingItem.XData[i] - DrawingItem.XMin, DrawingItem.YData[i] - DrawingItem.YCenter);
                        GL.Vertex2(x1, y1);
                        GL.Vertex2(x2, y2);
                    }
                }
            }
            GL.End();
        }

        /// <summary>
        /// プロットを三角形に設定するメソッドです。
        /// </summary>
        /// <param name="halfsize"></param>
        private void DrawTrianglePlot(double halfsize)
        {
            // プロットする図形に歪みが出ないようにアスペクト比をかける。
            double aspectRatio = (double)DrawingItem.XRange / DrawingItem.YRange;
            double halfXEdge = halfsize * aspectRatio;
            double halfYEdge = halfsize * Math.Sqrt(3);

            GL.Begin(PrimitiveType.Triangles);
            {
                for (int i = 0; i < DrawingItem.XData.Length; i++)
                // 描画領域に合わせて平行移動する必要がある
                {
                    GL.Vertex2(DrawingItem.XData[i] - DrawingItem.XMin - halfXEdge, DrawingItem.YData[i] - DrawingItem.YCenter - halfYEdge);
                    GL.Vertex2(DrawingItem.XData[i] - DrawingItem.XMin + halfXEdge, DrawingItem.YData[i] - DrawingItem.YCenter - halfYEdge);
                    GL.Vertex2(DrawingItem.XData[i] - DrawingItem.XMin, DrawingItem.YData[i] - DrawingItem.YCenter + Math.Sqrt(3) * halfYEdge);
                }
            }
            GL.End();
        }

        /// <summary>
        /// プロットを正方形に設定するメソッドです。
        /// </summary>
        /// <param name="halfsize"></param>
        private void DrawRectanglePlot(double halfsize)
        {
            // プロットする図形に歪みが出ないようにアスペクト比をかける。
            double aspectRatio = (double)DrawingItem.XRange / DrawingItem.YRange;
            double halfXEdge = halfsize * aspectRatio;
            double halfYEdge = halfsize;

            GL.Begin(PrimitiveType.Quads);
            {
                for (int i = 0; i < DrawingItem.XData.Length; i++)
                // 描画領域に合わせて平行移動する必要がある
                {
                    GL.Vertex2(DrawingItem.XData[i] - DrawingItem.XMin - halfXEdge, DrawingItem.YData[i] - DrawingItem.YCenter - halfYEdge);
                    GL.Vertex2(DrawingItem.XData[i] - DrawingItem.XMin + halfXEdge, DrawingItem.YData[i] - DrawingItem.YCenter - halfYEdge);
                    GL.Vertex2(DrawingItem.XData[i] - DrawingItem.XMin + halfXEdge, DrawingItem.YData[i] - DrawingItem.YCenter + halfYEdge);
                    GL.Vertex2(DrawingItem.XData[i] - DrawingItem.XMin - halfXEdge, DrawingItem.YData[i] - DrawingItem.YCenter + halfYEdge);
                }
            }
            GL.End();
        }

        /// <summary>
        /// プロットを逆三角形に設定するメソッドです。
        /// </summary>
        /// <param name="halfsize"></param>
        private void DrawInvertedTrianglePlot(double halfsize)
        {
            // プロットする図形に歪みが出ないようにアスペクト比をかける。
            double aspectRatio = (double)DrawingItem.XRange / DrawingItem.YRange;
            double halfXEdge = halfsize * aspectRatio;
            double halfYEdge = halfsize * Math.Sqrt(3);

            GL.Begin(PrimitiveType.Triangles);
            {
                for (int i = 0; i < DrawingItem.XData.Length; i++)
                // 描画領域に合わせて平行移動する必要がある
                {
                    GL.Vertex2(DrawingItem.XData[i] - DrawingItem.XMin - halfXEdge, DrawingItem.YData[i] - DrawingItem.YCenter + halfYEdge);
                    GL.Vertex2(DrawingItem.XData[i] - DrawingItem.XMin + halfXEdge, DrawingItem.YData[i] - DrawingItem.YCenter + halfYEdge);
                    GL.Vertex2(DrawingItem.XData[i] - DrawingItem.XMin, DrawingItem.YData[i] - DrawingItem.YCenter - halfYEdge);
                }
            }
            GL.End();
        }

        /// <summary>
        /// グラフの線を描画するメソッドです。
        /// </summary>
        /// <param name="color"></param>
        private void DrawGraph(Color4 color)
        {
            GL.Color4(color);

            GL.Begin(PrimitiveType.LineStrip);
            {
                for (int i = 0; i < DrawingItem.XData.Length; i++)
                    // 描画領域に合わせて平行移動する必要がある
                    GL.Vertex2(DrawingItem.XData[i] - DrawingItem.XMin, DrawingItem.YData[i] - DrawingItem.YCenter);
            }
            GL.End();

            GL.Color4(Color4.White);
        }

        /// <summary>
        /// 目盛り線を描画するメソッドです。
        /// </summary>
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

                if (DrawingItem.XMin < 0)
                {
                    while (_xCurrentPosition <= DrawingItem.XMax - DrawingItem.XMin)
                    {
                        DrawXScale(ref _xCurrentPosition);
                    }

                    while (_yCurrentPosition <= DrawingItem.YMax - DrawingItem.YCenter)
                    {
                        DrawUpperYScale(ref _yCurrentPosition, DrawingItem.XMax - DrawingItem.XMin);
                    }

                    _yCurrentPosition = 0;
                    while (_yCurrentPosition >= DrawingItem.YMin - DrawingItem.YCenter)
                    {
                        DrawUnderYScale(ref _yCurrentPosition, DrawingItem.XMax - DrawingItem.XMin);
                    }
                }

                else
                {
                    while (_xCurrentPosition <= DrawingItem.XMax)
                    {
                        DrawXScale(ref _xCurrentPosition);
                    }

                    while (_yCurrentPosition <= DrawingItem.YMax - DrawingItem.YCenter)
                    {
                        DrawUpperYScale(ref _yCurrentPosition, DrawingItem.XMax);
                    }

                    _yCurrentPosition = 0;
                    while (_yCurrentPosition >= DrawingItem.YMin - DrawingItem.YCenter)
                    {
                        DrawUnderYScale(ref _yCurrentPosition, DrawingItem.XMax);
                    }
                }
            }
            GL.End();
            // 点線描画OFF ※これをしないと描画したもの全て点線になる
            GL.Disable(EnableCap.LineStipple);
        }

        /// <summary>
        /// x座標の目盛り線を描画するメソッドです。
        /// </summary>
        /// <param name="x"></param>
        private void DrawXScale(ref double x)
        {
            // 描画領域に合わせて平行移動する必要がある
            GL.Vertex2(x, DrawingItem.YMin - DrawingItem.YCenter);
            GL.Vertex2(x, DrawingItem.YMax - DrawingItem.YCenter);
            x += DrawingItem.XScale;
        }

        /// <summary>
        /// y座標の上半分の目盛り線を描画するメソッドです。
        /// </summary>
        /// <param name="y"></param>
        /// <param name="xend"></param>
        private void DrawUpperYScale(ref double y, double xend)
        {
            GL.Vertex2(0, y);
            GL.Vertex2(xend, y);
            y += DrawingItem.YScale;
        }

        /// <summary>
        /// y座標の下半分を描画するメソッドです。
        /// </summary>
        /// <param name="y"></param>
        /// <param name="xend"></param>
        private void DrawUnderYScale(ref double y, double xend)
        {
            GL.Vertex2(0, y);
            GL.Vertex2(xend, y);
            y -= DrawingItem.YScale;
        }

    }
}
