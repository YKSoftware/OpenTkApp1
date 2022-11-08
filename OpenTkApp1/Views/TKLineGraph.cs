using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Runtime.CompilerServices;

namespace OpenTkApp1.Views
{
    /// <summary>
    /// Window上の座標は原点が左上、OpenTK上の座標は中央が原点の初期位置。
    /// 描画はOpenTKで定義した、描画領域の座標を扱うが、マウスイベントはWindow上の座標を扱う。
    /// </summary>
    public class TKLineGraph : FrameworkElement, ITkGraphBase
    {
        #region 依存関係プロパティ定義

        #region DrawingItem
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
        #endregion DrawingItem

        #region XMax
        public static readonly DependencyProperty XMaxProperty = DependencyProperty.Register("XMax", typeof(double), typeof(TKLineGraph), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnXMaxPropertyChanged));

        public double XMax
        {
            get => (double)GetValue(XMaxProperty);
            set => SetValue(XMaxProperty, value);
        }

        private static void OnXMaxPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TKLineGraph)?.Render();
        }
        #endregion XMax

        #region XMin
        public static readonly DependencyProperty XMinProperty = DependencyProperty.Register("XMin", typeof(double), typeof(TKLineGraph), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnXMinPropertyChanged));

        public double XMin
        {
            get => (double)GetValue(XMinProperty);
            set => SetValue(XMinProperty, value);
        }

        private static void OnXMinPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TKLineGraph)?.Render();
        }
        #endregion XMin

        #region YMax
        public static readonly DependencyProperty YMaxProperty = DependencyProperty.Register("YMax", typeof(double), typeof(TKLineGraph), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnYMaxPropertyChanged));

        public double YMax
        {
            get => (double)GetValue(YMaxProperty);
            set => SetValue(YMaxProperty, value);
        }
        private static void OnYMaxPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TKLineGraph)d).Render();
        }
        #endregion YMax

        #region YMin
        public static readonly DependencyProperty YMinProperty = DependencyProperty.Register("YMin", typeof(double), typeof(TKLineGraph), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnYMinPropertyChanged));

        public double YMin
        {
            get => (double)GetValue(YMinProperty);
            set => SetValue(YMinProperty, value);
        }

        private static void OnYMinPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TKLineGraph)?.Render();
        }
        #endregion YMin

        #region YCenter
        public static readonly DependencyProperty YCenterProperty = DependencyProperty.Register("YCenter", typeof(double), typeof(TKLineGraph), new PropertyMetadata(0.0, OnYCenterPropertyChanged));

        public double YCenter
        {
            get => (double)GetValue(YCenterProperty);
            set => SetValue(YCenterProperty, value);
        }

        private static void OnYCenterPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TKLineGraph)?.Render();
        }

        #endregion YCenter

        #region XScale
        public static readonly DependencyProperty XScaleProperty = DependencyProperty.Register("XScale", typeof(double), typeof(TKLineGraph), new PropertyMetadata(0.0, OnXScalePropertyChanged));

        public double XScale
        {
            get => (double)GetValue(XScaleProperty);
            set => SetValue(XScaleProperty, value);
        }

        private static void OnXScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TKLineGraph)?.Render();
        }
        #endregion XScale

        #region YScale
        public static readonly DependencyProperty YScaleProperty = DependencyProperty.Register("YScale", typeof(double), typeof(TKLineGraph), new PropertyMetadata(0.0, OnYScalePropertyChanged));

        public double YScale
        {
            get => (double)GetValue(YScaleProperty);
            set => SetValue(YScaleProperty, value);
        }

        private static void OnYScalePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TKLineGraph)?.Render();
        }
        #endregion YSacle

        #region XRange 
        public static readonly DependencyProperty XRangeProperty = DependencyProperty.Register("XRange", typeof(double), typeof(TKLineGraph), new PropertyMetadata(0.0, OnXRangePropertyChanged));

        public  double XRange
        {
            get => (double)GetValue(XRangeProperty);
            set => SetValue(XRangeProperty, value);
        }

        private static void OnXRangePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TKLineGraph)?.Render();
        }
        #endregion XRange

        #region YRange
        public static readonly DependencyProperty YRangeProperty = DependencyProperty.Register("YRange", typeof(double), typeof(TKLineGraph), new PropertyMetadata(0.0, OnYRangePropertyChanged));

        public double YRange
        {
            get => (double)GetValue(YRangeProperty);
            set => SetValue(YRangeProperty, value);
        }

        private static void OnYRangePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TKLineGraph)?.Render();
        }
        #endregion YRange

        #region DisplayDisits
        /// <summary>
        /// DisplayDisits依存関係プロパティの定義を表します。
        /// </summary>
        public static readonly DependencyProperty DisplayDisitsProperty = DependencyProperty.Register("DisplayDisits", typeof(int), typeof(TKLineGraph), new PropertyMetadata(0, OnDisplayDisitsPropertyChanged));

        public int DisplayDisits
        {
            get => (int)GetValue(DisplayDisitsProperty);
            set => SetValue(DisplayDisitsProperty, value);
        }

        /// <summary>
        /// DisplayDisitsプロパティ変更イベントハンドラ
        /// </summary>
        /// <param name="d">イベント発行元</param>
        /// <param name="e">イベント引数</param
        private static void OnDisplayDisitsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as TKLineGraph)?.Render();
        }

        #endregion DisplayDisits

        #region CurrentXPosition
        /// <summary>
        /// 現在のカーソルのx座標を表します。
        /// </summary>
        public static readonly DependencyProperty CurrentXPositionProperty = DependencyProperty.Register("CurrentXPosition", typeof(double), typeof(TKLineGraph), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double CurrentXPosition
        {
            get => (double)GetValue(CurrentXPositionProperty);
            set => SetValue(CurrentXPositionProperty, value);
        }

        #endregion CurrentXPosition

        #region CurrentYPosition
        public static readonly DependencyProperty CurrentYPositionProperty = DependencyProperty.Register("CurrentYPosition", typeof(double), typeof(TKLineGraph), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public double CurrentYPosition
        {
            get => (double)GetValue(CurrentYPositionProperty);
            set => SetValue(CurrentYPositionProperty, value);
        }

        #endregion CurrentYPosition

        #endregion 依存関係プロパティ定義

        TkBitmap LegendBitmap = new TkBitmap();

        TkBitmap GraphDataBitmap = new TkBitmap();

        TkBitmap GraphCursorBitmap = new TkBitmap();

        TkGraphCursor LeftGraphCursor = new TkGraphCursor();

        TkGraphCursor RightGraphCursor = new TkGraphCursor();

        public void Render()
        {
            SetGraphProjection();

            // 1回目のRenderが走るタイミングがBindingするより早いため
            if (DrawingItem?.XData is null) return;
            if (DrawingItem.YData is null) return;
            if (XScale == 0) return;
            if (YScale == 0) return;

            // 背景の定義
            GL.ClearColor(Color4.Black);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Enable(EnableCap.DepthTest);

            GL.PushMatrix();
            {
                // 左端から描画するために移動
                GL.Translate(-(XRange / 2), 0, 0);
                // グラフ点描画
                DrawPlot(DrawingItem.PlotSize, DrawingItem.PlotType, DrawingItem.PlotColor);
                // グラフ線描画
                DrawGraph(DrawingItem.LineColor);
                DrawCarsol();
                // 目盛り線描画
                DrawScale();
                // 原点を中心に戻す。
                GL.Translate(XRange / 2, 0, 0);
                // 重なった時凡例が上になるようにDepthTest解除
                GL.Disable(EnableCap.DepthTest);
                // カーソル上に点が存在した時のみ位置表示
                //if((0 < CurrentXPosition) && (CurrentXPosition < DrawingItem.XData.Length) && (Math.Round(CurrentYPosition,0) == Math.Round(DrawingItem.YData[(int)CurrentXPosition], 0)))
                
            }
            GL.PopMatrix();

            // テキスト描画
            SetTextProjection();

            GL.PushMatrix();
            {
                DrawGraphDataText(GraphDataBitmap);
                // 左端から描画するために移動
                GL.Translate(-(TkGraphics.CurrentWidth / 2), (TkGraphics.CurrentHeight)/3, 0);
                DrawGraphCursorText(GraphCursorBitmap);

            }
            GL.PopMatrix();

            GL.PushMatrix();
            {
                GL.Translate((TkGraphics.CurrentWidth / 5), -5 * (TkGraphics.CurrentHeight) / 12, 0);
                DrawLegend(LegendBitmap);
            }
            GL.PopMatrix();

        }

        #region グラフ描画

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
                    GL.Vertex2(DrawingItem.XData[i] - XMin, DrawingItem.YData[i] - YCenter);
            }
            GL.End();

        }

        /// <summary>
        /// カーソルを描画するメソッドです。
        /// </summary>
        private void DrawCarsol()
        {
            GL.Color4(Color4.Yellow);

            this.LeftGraphCursor.XPosition = XRange / 3 + _cLeftGraphCursorTranslate;
            this.RightGraphCursor.XPosition = 2 * XRange / 3 + _cRightGraphCursorTranslate;
            this.LeftGraphCursor.Height = YRange / 2;
            this.RightGraphCursor.Height = YRange / 2;
            
            GL.Begin(PrimitiveType.Lines);
            {
                GL.Vertex2(this.LeftGraphCursor.XPosition , this.LeftGraphCursor.Height);
                GL.Vertex2(this.LeftGraphCursor.XPosition, -this.LeftGraphCursor.Height);
                GL.Vertex2(this.RightGraphCursor.XPosition, this.RightGraphCursor.Height);
                GL.Vertex2(this.RightGraphCursor.XPosition, -this.RightGraphCursor.Height);

            }
            GL.End();
            GraphCursorBitmap.CreateGraphCursorBitmap("10", 16, Colors.Yellow);
           
            
            
        }

        /// <summary>
        /// 目盛り線を描画するメソッドです。
        /// </summary>
        private void DrawScale()
        {
            GL.Color4(Color4.White);
            // 点線描画ON
            GL.Enable(EnableCap.LineStipple);
            // 破線の形状を決める
            GL.LineStipple(1, 0xF0F0);
            GL.Begin(PrimitiveType.Lines);
            {
                double _xCurrentPosition = 0;
                double _yCurrentPosition = 0;

                if (XMin < 0)
                {
                    while (_xCurrentPosition <= XMax - XMin)
                    {
                        DrawXScale(ref _xCurrentPosition);
                    }

                    while (_yCurrentPosition <= YMax - YCenter)
                    {
                        DrawUpperYScale(ref _yCurrentPosition, XMax - XMin);
                    }

                    _yCurrentPosition = 0;
                    while (_yCurrentPosition >= YMin - YCenter)
                    {
                        DrawUnderYScale(ref _yCurrentPosition, XMax - XMin);
                    }
                }

                else
                {
                    while (_xCurrentPosition <= XMax)
                    {
                        DrawXScale(ref _xCurrentPosition);
                    }

                    while (_yCurrentPosition <= YMax - YCenter)
                    {
                        DrawUpperYScale(ref _yCurrentPosition, XMax);
                    }

                    _yCurrentPosition = 0;
                    while (_yCurrentPosition >= YMin - YCenter)
                    {
                        DrawUnderYScale(ref _yCurrentPosition, XMax);
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
            GL.Vertex2(x, YMin - YCenter);
            GL.Vertex2(x, YMax - YCenter);
            x += XScale;
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
            y += YScale;
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
            y -= YScale;
        }

        #endregion　グラフ描画

        #region プロット描画
        /// <summary>
        /// グラフの点を描画するメソッドです。
        /// </summary>
        /// <param name="size"></param>
        /// <param name="type"></param>
        /// <param name="color"></param>
        private void DrawPlot(double size, MarkerTypes type, Color4 color)
        {
            if (DrawingItem?.IsPlot == false) return;

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
            double aspectRatio = (double)XRange / YRange;
            double xSize = size * aspectRatio;

            GL.Begin(PrimitiveType.Triangles);
            {
                for (int i = 0; i < DrawingItem.XData.Length; i++)
                {
                    for (int j = 0; j <= division; j++)
                    {
                        double x1 = DrawingItem.XData[i] - XMin + (xSize * Math.Cos(delta * j));
                        double y1 = DrawingItem.YData[i] - YCenter + (size * Math.Sin(delta * j));
                        double x2 = DrawingItem.XData[i] - XMin + (xSize * Math.Cos(delta * j + 1));
                        double y2 = DrawingItem.YData[i] - YCenter + (size * Math.Sin(delta * j + 1));
                        GL.Vertex2(DrawingItem.XData[i] - XMin, DrawingItem.YData[i] - YCenter);
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
            double aspectRatio = (double)XRange / YRange;
            double halfXEdge = halfsize * aspectRatio;
            double halfYEdge = halfsize * Math.Sqrt(3);

            GL.Begin(PrimitiveType.Triangles);
            {
                for (int i = 0; i < DrawingItem.XData.Length; i++)
                // 描画領域に合わせて平行移動する必要がある
                {
                    GL.Vertex2(DrawingItem.XData[i] - XMin - halfXEdge, DrawingItem.YData[i] - YCenter - halfYEdge);
                    GL.Vertex2(DrawingItem.XData[i] - XMin + halfXEdge, DrawingItem.YData[i] - YCenter - halfYEdge);
                    GL.Vertex2(DrawingItem.XData[i] - XMin, DrawingItem.YData[i] - YCenter + Math.Sqrt(3) * halfYEdge);
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
            double aspectRatio = (double)XRange / YRange;
            double halfXEdge = halfsize * aspectRatio;
            double halfYEdge = halfsize;

            GL.Begin(PrimitiveType.Quads);
            {
                for (int i = 0; i < DrawingItem.XData.Length; i++)
                // 描画領域に合わせて平行移動する必要がある
                {
                    GL.Vertex2(DrawingItem.XData[i] - XMin - halfXEdge, DrawingItem.YData[i] - YCenter - halfYEdge);
                    GL.Vertex2(DrawingItem.XData[i] - XMin + halfXEdge, DrawingItem.YData[i] - YCenter - halfYEdge);
                    GL.Vertex2(DrawingItem.XData[i] - XMin + halfXEdge, DrawingItem.YData[i] - YCenter + halfYEdge);
                    GL.Vertex2(DrawingItem.XData[i] - XMin - halfXEdge, DrawingItem.YData[i] - YCenter + halfYEdge);
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
            double aspectRatio = (double)XRange / YRange;
            double halfXEdge = halfsize * aspectRatio;
            double halfYEdge = halfsize * Math.Sqrt(3);

            GL.Begin(PrimitiveType.Triangles);
            {
                for (int i = 0; i < DrawingItem.XData.Length; i++)
                // 描画領域に合わせて平行移動する必要がある
                {
                    GL.Vertex2(DrawingItem.XData[i] - XMin - halfXEdge, DrawingItem.YData[i] - YCenter + halfYEdge);
                    GL.Vertex2(DrawingItem.XData[i] - XMin + halfXEdge, DrawingItem.YData[i] - YCenter + halfYEdge);
                    GL.Vertex2(DrawingItem.XData[i] - XMin, DrawingItem.YData[i] - YCenter - halfYEdge);
                }
            }
            GL.End();
        }
        #endregion プロット描画
       
        #region テキスト描画
        /// <summary>
        /// 凡例を描画するメソッドです。
        /// </summary>
        private void DrawLegend(TkBitmap bitmap)
        {
            if (TextureList.Textures.Count > 0)
            // 指定したIDのテクスチャを現在のテクスチャとします。
            GL.BindTexture(TextureTarget.Texture2D, TextureList.Textures[0]);
            GL.Translate(_legendxTranslate, -_legendyTranslate, 0);
            
            DrawString(bitmap);
        }

        private void DrawGraphDataText(TkBitmap bitmap)
        {
            if (TextureList.Textures.Count > 0)
            // 指定したIDのテクスチャを現在のテクスチャとします。
            GL.BindTexture(TextureTarget.Texture2D, TextureList.Textures[1]);
            GL.Translate(-TkGraphics.CurrentWidth/2 + _beforeCoordinatexPosition, -_beforeCoordinateyPosition + TkGraphics.CurrentHeight/2, 0);
            DrawString(bitmap);
            GL.Translate(TkGraphics.CurrentWidth/2 - _beforeCoordinatexPosition, _beforeCoordinateyPosition - TkGraphics.CurrentHeight/2, 0);
        }


        /// <summary>
        /// 文字を描画するメソッドです。
        /// 四角形の上にテキストを書いたビットマップをテクスチャとして貼り付けている。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="fontSize"></param>
        /// <param name="color"></param>
        public void DrawString(TkBitmap bitmap)
        {
            // テキストのサイズを画面の大きさに依存しないように領域を定義します。
            SetTextProjection();

            // テクスチャを有効化します。
            GL.Enable(EnableCap.Texture2D);

            // 四角形で描画
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 0);
            GL.Vertex2(0, 0);
            GL.TexCoord2(1, 0);
            GL.Vertex2(bitmap.Width, 0);
            GL.TexCoord2(1, 1);
            GL.Vertex2(bitmap.Width, bitmap.Height);
            GL.TexCoord2(0, 1);
            GL.Vertex2(0, bitmap.Height);
            GL.End();
            
            GL.Disable(EnableCap.Texture2D);
            
            SetGraphProjection();
        }
        
        #region テキストビットマップ作成
        /// <summary>
        /// 新たにCreateBitmapをインスタンス生成するメソッドです。
        /// ロード時に呼び出されます。
        /// </summary>
        public void CreateTextBitmap()
        {
            if (DrawingItem?.Legend == null) return;
            LegendBitmap.CreateLegendBitmap(DrawingItem.Legend, 20, Colors.White, DrawingItem.LineColor);
            // 凡例の原点(左上)からの距離を導く。
            this._legendxOffset = 7 * TkGraphics.CurrentWidth / 10 ;
            this._legendyOffset = 11 * TkGraphics.CurrentHeight / 12 - LegendBitmap.LegendRect.Height; 
            GraphDataBitmap.CreateGraphDataBitmap("str", 24, Colors.Aqua);        
        }

        public void BitmapPositionChange()
        {
            // 凡例の原点(左上)からの距離を導く。
            this._legendxOffset = 7 * TkGraphics.CurrentWidth / 10 ;
            this._legendyOffset = 11 * TkGraphics.CurrentHeight / 12 - LegendBitmap.LegendRect.Height ;

            //// Windowのサイズが変更された時に、それに応じて凡例を移動させます。
            _windowSizeChangedLegendxTranslate = _saveLegendXTranslate * TkGraphics.CurrentWidth / _saveWidth;
            _windowSizeChangedLegendyTranslate = _saveLegendYTranslate * TkGraphics.CurrentHeight / _saveHeight;

            // Windowサイズ変更を初期サイズから変更していない場合は_windowSizeChangedLegendxTranslateがNaNになる。
            if (!Double.IsNaN(_windowSizeChangedLegendxTranslate))
                _legendxTranslate = _windowSizeChangedLegendxTranslate;
            if (!Double.IsNaN(_windowSizeChangedLegendyTranslate))
                _legendyTranslate = _windowSizeChangedLegendyTranslate;
        }

        #endregion テキストビットマップ作成

        /// <summary>
        /// テキストを描画する際の描画領域を設定するメソッドです。
        /// </summary>
        private void SetTextProjection()
        {
            // 視体積の設定
            GL.MatrixMode(MatrixMode.Projection);
            {
                float r = (float)TkGraphics.CurrentWidth / (float)TkGraphics.CurrentHeight;
                float h = (float)TkGraphics.CurrentHeight;
                float w = h * r;
                Matrix4 proj = Matrix4.CreateOrthographic(w, h, 0.01f, 1000.0f);
                GL.LoadMatrix(ref proj);
            }
            GL.MatrixMode(MatrixMode.Modelview);
        }

        /// <summary>
        /// グラフを描画する際の描画領域を設定するメソッドです。
        /// </summary>
        private void SetGraphProjection()
        {
            GL.MatrixMode(MatrixMode.Projection);
            {
                Matrix4 proj = Matrix4.CreateOrthographic((int)XRange, (int)YRange, 0.01f, 1000.0f);
                GL.LoadMatrix(ref proj);
            }
            GL.MatrixMode(MatrixMode.Modelview);
        }

        #endregion テキスト描画

        #region 座標変換
        /// <summary>
        /// windowのx座標を描画領域に合わせたx座標に変換するメソッドです。
        /// </summary>
        /// <param name="x"></param>
        /// <param name="xMin">軸移動時のみ使用します。</param>
        /// <param name="n"></param>
        /// <returns></returns>
        private double CoordinateXTransformation(double x, double xMin, int n)
        {
            return Math.Round(x * XRange / TkGraphics.CurrentWidth + xMin, n);
        }

        /// <summary>
        ///  windowのy座標を描画領域に合わせたx座標に変換するメソッドです。
        /// </summary>
        /// <param name="y"></param>
        /// <param name="yCenter">軸移動時のみ使用します。</param>
        /// <param name="n"></param>
        /// <returns></returns>
        private double CoordinateYTransformation(double y, double yCenter, int n)
        {
            return Math.Round((-y * YRange / TkGraphics.CurrentHeight) + YRange / 2 + yCenter, n);
        }

        #endregion 座標変換

        #region マウスイベント
        /// <summary>
        /// マウスが移動した際に実行されるイベントハンドラです。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnMouseMove(object sender, MouseEventArgs e)
        {
            System.Windows.Point point = e.GetPosition((IInputElement)sender);
            _beforeCoordinatexPosition = point.X;
            _beforeCoordinateyPosition = point.Y;

            // x座標変換
            var x = CoordinateXTransformation(point.X, XMin, DisplayDisits);
            // y座標変換 ※ActualHeightとpoint.Yの間に何故か1.25の差が生じている...
            var y = CoordinateYTransformation(point.Y, YCenter, DisplayDisits);

            string graphCursor = String.Format("x : {0} \r\ny : {1}", x, y);
            // ビットマップを更新する。
            GraphDataBitmap.CreateGraphDataBitmap(graphCursor, 18, Colors.White);
            
            // Viewの値の変更をViewModelにも伝えてあげる。
            SetCurrentValue(CurrentXPositionProperty, x);
            SetCurrentValue(CurrentYPositionProperty, y);

            // マウスカーソルが左側のグラフカーソル上にある時
            if ( (LeftGraphCursor.XPosition + XMin - 1 <= x) && (x <= LeftGraphCursor.XPosition + XMin + 1))
            {
                this.Cursor = Cursors.SizeAll;
                LeftGraphCursor.OnCursor = true;
            }
            // マウスカーソルが右側のグラフカーソル上にある時
            else if ( ( this.RightGraphCursor.XPosition+ XMin - 1 <= x) && (x <= this.RightGraphCursor.XPosition + XMin + 1))
            {
                this.Cursor = Cursors.SizeAll;
                this.RightGraphCursor.OnCursor = true;
            }
            else
            {
                this.Cursor = Cursors.Arrow;
                this.LeftGraphCursor.OnCursor = false;
                this.RightGraphCursor.OnCursor = false;
            }

            // 左グラフカーソル移動時
            if (this.LeftGraphCursor.IsDrag == true)
            {
                this.Cursor = Cursors.SizeAll;
                this._leftGraphCursorTranslate = point.X - this._oldLeftGraphCursorPosition;
                this._cLeftGraphCursorTranslate = CoordinateXTransformation(point.X - this._oldLeftGraphCursorPosition, 0, this.DisplayDisits);
                _saveWidth = TkGraphics.CurrentWidth;
            }

            // 右グラフカーソル移動時
            if (this.RightGraphCursor.IsDrag == true)
            {
                this.Cursor = Cursors.SizeAll;
                this._rightGraphCursorTranslate = point.X - this._oldRightGraphCursorPosition;
                this._cRightGraphCursorTranslate = CoordinateXTransformation(point.X - this._oldRightGraphCursorPosition, 0, this.DisplayDisits);
                _saveWidth = TkGraphics.CurrentWidth;
            }

            // 軸移動時
            if (this._isAxisDrag == true)
            {
                // マウス座標を更新します。 :　描画領域の変化に応じてXMin,YCenterが変化するので、ドラッグ開始時のXMin,YCenterを足してあげます。
                double movedx = CoordinateXTransformation(point.X, this._dragOffsetXMin, this.DisplayDisits);
                double movedy = CoordinateYTransformation(point.Y, this._dragOffsetYCenter, this.DisplayDisits);

                // ドラッグ量 MouseMoveイベントは常に走り続けるため、1周期前の座標を現在の座標から引くことで変化量を求めます。
                double xTranslate = Math.Round(movedx - this._oldXPosition, this.DisplayDisits);
                double yTranslate = Math.Round(movedy - this._oldYPosition, this.DisplayDisits);

                // 前回のマウス座標を更新
                this._oldXPosition = movedx;
                this._oldYPosition = movedy;

                // Viewからプロパティ値更新,  x,yの最大・最小を変化させることで描画領域を移動,
                SetCurrentValue(XMaxProperty, this.XMax - xTranslate);
                SetCurrentValue(XMinProperty, this.XMin - xTranslate);
                SetCurrentValue(YMaxProperty, this.YMax - yTranslate);
                SetCurrentValue(YMinProperty, this.YMin - yTranslate);
            }

            // 凡例移動時
            if(this._isLegendDrag == true)
            {
                // 凡例の移動量を更新
                this._legendxTranslate = point.X - this._legendDragOffsetPoint.X;
                this._legendyTranslate = point.Y - this._legendDragOffsetPoint.Y;
                // 移動量、Windowサイズを記憶
                _saveLegendXTranslate = _legendxTranslate;
                _saveLegendYTranslate = _legendyTranslate;
                _saveWidth = TkGraphics.CurrentWidth;
                _saveHeight = TkGraphics.CurrentHeight;
            }

        }

        /// <summary>
        /// マウスの左のボタンを押した際に実行されるイベントハンドラです。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnMouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            // ドラッグ開始時の座標を取得します。
            this._dragOffset = e.GetPosition((IInputElement)sender);

            // 前回の移動量を引くことで、移動量の変化を繋げます。
            this._legendDragOffsetPoint.X =this._dragOffset.X - this._legendxTranslate;
            this._legendDragOffsetPoint.Y= this._dragOffset.Y - this._legendyTranslate;

            // 凡例の座標を正しい位置で判定できるように座標変換します。
            Point legendPoint;
            legendPoint.X = this._dragOffset.X - this._legendxOffset - this._legendxTranslate;
            legendPoint.Y = this._dragOffset.Y - this._legendyOffset - this._legendyTranslate;

            if (LegendBitmap.LegendRect.Contains(legendPoint) == true)
            {
                this._isLegendDrag = true;
                return;
            }

            // 左側のグラフカーソル上でクリックした時
            if (this.LeftGraphCursor.OnCursor == true)
            {
                this.LeftGraphCursor.IsDrag = true;
                // 画面のサイズが変更されていたら、移動量を修正する。
                if (_saveWidth != 0)
                    this._leftGraphCursorTranslate *= (TkGraphics.CurrentWidth / _saveWidth);
                this._oldLeftGraphCursorPosition = this._dragOffset.X - this._leftGraphCursorTranslate ;
                return;
            }

            //右側のグラフカーソル上でクリックした時
            if (this.RightGraphCursor.OnCursor == true)
            {
                this.RightGraphCursor.IsDrag = true;
                // 画面のサイズが変更されていたら、移動量を修正する。
                if (_saveWidth != 0)
                    this._rightGraphCursorTranslate *= (TkGraphics.CurrentWidth / _saveWidth);
                this._oldRightGraphCursorPosition = this._dragOffset.X - this._rightGraphCursorTranslate;

                return;
            }

            // ドラッグフラグをtrueにします。
            this._isAxisDrag = true;

            // ドラッグ開始時の座標をグラフ描画領域の座標に変換します。
            this._oldXPosition = CoordinateXTransformation(this._dragOffset.X, this.XMin, this.DisplayDisits);
            this._oldYPosition = CoordinateYTransformation(this._dragOffset.Y, this.YCenter, this.DisplayDisits);

            // ドラッグ開始時のx、ｙの最小・最大、yの中間値を取得します。
            this._dragOffsetXMax = this.XMax;
            this._dragOffsetXMin = this.XMin;
            this._dragOffsetYCenter = this.YCenter;
            this._dragOffsetYMax = this.YMax;
            this._dragOffsetYMin = this.YMin;

        }

        /// <summary>
        /// マウスの左ボタンを離した際に実行されるイベントハンドラです。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnMouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            if (this._isAxisDrag == true)
            {
                // ドラッグフラグをfalseにします。
                UIElement el = sender as UIElement;
                el.ReleaseMouseCapture();
                this._isAxisDrag = false;
            }

            if (this._isLegendDrag == true)
            {
                this._isLegendDrag = false;
            }

            if (this.LeftGraphCursor.IsDrag == true)
            {
                this.LeftGraphCursor.IsDrag = false;
            }

            if (this.RightGraphCursor.IsDrag  == true)
            {
                this.RightGraphCursor.IsDrag = false;
            }
        }

        /// <summary>
        /// キーボードのEscキーを押した際に実行されるイベントハンドラです。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnEscKeyDown(object sender, KeyEventArgs e)
        {
            if (this._isAxisDrag == true)
            {
                if (e.Key == Key.Escape)
                {
                    SetCurrentValue(XMaxProperty, this._dragOffsetXMax);
                    SetCurrentValue(XMinProperty, this._dragOffsetXMin);
                    SetCurrentValue(YMaxProperty, this._dragOffsetYMax);
                    SetCurrentValue(YMinProperty, this._dragOffsetYMin);
                    Render();
                }
            }
        }
        #endregion　マウスイベント

        /// <summary>
        /// 折れ線グラフコントロール上のマウスカーソルの種類を取得、設定します。
        /// </summary>
        public new Cursor Cursor
        {
            get { return this._cursor; }
            set
            {
                this._cursor = value;
                // コントロール上のマウスカーソルの変更を親要素であるTKGraphicsに伝える。
                ((OpenTkApp1.ViewModels.MainViewModel)this.DataContext).MouseCursor = this._cursor;
            }
        }

        private Cursor _cursor = Cursors.Arrow;

        #region フィールド

        /// <summary>
        /// ドラッグ移動させる前の左側のグラフカーソルの位置を表します。
        /// </summary>
        private double _oldLeftGraphCursorPosition;

        /// <summary>
        /// ドラッグを移動させる前の右側のグラフカーソルの位置を表します。
        /// </summary>
        private double _oldRightGraphCursorPosition;

        /// <summary>
        /// window座標上での左側のグラフカーソルの移動量を表します。
        /// </summary>
        private double _leftGraphCursorTranslate;

        /// <summary>
        /// window座標上での右側のグラフカーソルの移動量を表します。
        /// </summary>
        private double _rightGraphCursorTranslate;

        /// <summary>
        /// window座標から変換済みの左側のグラフカーソルの移動量を表します。
        /// </summary>
        private double _cLeftGraphCursorTranslate;

        /// <summary>
        /// window座標から変換済みの右側のグラフカーソルの移動量を表します。
        /// </summary>
        private double _cRightGraphCursorTranslate;

        /// <summary>
        /// 凡例の初期位置の原点からのx座標の距離を表します。
        /// </summary>
        private double _legendxOffset;

        /// <summary>
        /// 凡例の初期位置の原点からのy座標の距離を表します。
        /// </summary>
        private double _legendyOffset;

        /// <summary>
        /// 現在の凡例のドラッグの状態を表します。
        /// </summary>
        private bool _isLegendDrag = false;

        /// <summary>
        /// 凡例のx座標の移動量を表します。
        /// </summary>
        private double _legendxTranslate;

        /// <summary>
        /// 凡例のy座標の移動量を表します。
        /// </summary>
        private double _legendyTranslate;

        /// <summary>
        /// クリック時の凡例の座標を表します。
        /// </summary>
        private Point _legendDragOffsetPoint;

        /// <summary>
        /// 現在の軸のドラッグ状態を表します。
        /// </summary>
        private bool _isAxisDrag = false;

        /// <summary>
        /// ドラッグ開始時の座標を表します。
        /// </summary>
        private System.Windows.Point _dragOffset;

        /// <summary>
        /// 1イベント前のx座標を表します。
        /// </summary>
        private double _oldXPosition;

        /// <summary>
        /// ドラッグ開始時のxの最小値を表します。
        /// </summary>
        private double _dragOffsetXMin;

        /// <summary>
        /// ドラッグ開始時のxの最大値を表します。
        /// </summary>
        private double _dragOffsetXMax;

        /// <summary>
        ///  1イベント前のy座標を表します。
        /// </summary>
        private double _oldYPosition;

        /// <summary>
        /// ドラッグ開始時のyの最小値を表します。
        /// </summary>
        private double _dragOffsetYMin;

        /// <summary>
        /// ドラッグ開始時のyの最大値を表します。
        /// </summary>
        private double _dragOffsetYMax;

        /// <summary>
        /// ドラッグ開始時のyの中間値を表します。
        /// </summary>
        private double _dragOffsetYCenter;

        /// <summary>
        /// Windowのサイズが変化した時の、それに応じた凡例の位置のx方向の移動量です。
        /// </summary>
        private double _windowSizeChangedLegendxTranslate;

        /// <summary>
        /// Windowのサイズが変化した時の、それに応じた凡例の位置のy方向の移動量です。
        /// </summary>
        private double _windowSizeChangedLegendyTranslate;

        /// <summary>
        /// Windowのサイズが変化させる前に、凡例の位置を移動させた場合のx方向の移動量を保持しておきます。
        /// </summary>
        private double _saveLegendXTranslate;

        /// <summary>
        /// Windowのサイズが変化させる前に、凡例の位置を移動させた場合のy方向の移動量を保持しておきます。
        /// </summary>
        private double _saveLegendYTranslate;

        /// <summary>
        /// 凡例の位置を移動させた時のウィンドウの幅を保持しています。
        /// </summary>
        private double _saveWidth;

        /// <summary>
        /// 凡例の位置を移動させた時のウィンドウの高さを保持しています。
        /// </summary>
        private double _saveHeight;

        /// <summary>
        /// マウスカーソルの変換する前のx座標を表します。
        /// </summary>
        private double _beforeCoordinatexPosition;

        /// <summary>
        /// マウスカーソルの変換する前のy座標を表します。
        /// </summary>
        private double _beforeCoordinateyPosition;


        #endregion フィールド

    }
}
