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
            if (DrawingItem.XData is null) return;
            if (DrawingItem.YData is null) return;

            GL.ClearColor(Color4.Black);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Color4(Color4.White);

            GL.PushMatrix();
            {
                // 左端から描画するために移動
                GL.Translate(-((DrawingItem.XMax - DrawingItem.XMin) / 2), 0, 0);
                //// グラフ点描画
                //DrawPlot(PlotSize, PlotType, PlotColor);
                // グラフ線描画
                DrawGraph(DrawingItem.LineColor);
                //// 目盛り線描画
                //DrawScale();
            }
            GL.PopMatrix();
        }

        private void DrawPlot()
        {
           
        }

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

        private void DrawScale()
        {

        }

        
    }
}
