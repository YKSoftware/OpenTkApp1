using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Wpf;
using OpenTK;

namespace OpenTkApp1.Views
{
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

        public double XRange
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

        public void Render()
        {
            // 1回目のRenderが走るタイミングがBindingするより早いため
            if (DrawingItem?.XData is null) return;
            if (DrawingItem.YData is null) return;
            if (XScale == 0) return;
            if (YScale == 0) return;

            GL.ClearColor(Color4.Black);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Color4(Color4.White);

            GL.PushMatrix();
            {
                // 左端から描画するために移動
                GL.Translate(-(XRange / 2), 0, 0);
                // グラフ点描画
                DrawPlot(DrawingItem.PlotSize, DrawingItem.PlotType, DrawingItem.PlotColor);
                // グラフ線描画
                DrawGraph(DrawingItem.LineColor);
                //// 目盛り線描画
                DrawScale();

                DrawString("計測誤差[mg]", 40, Colors.Orange);
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
        /// 文字を描画するメソッドです。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="fontSize"></param>
        /// <param name="color"></param>
        public void DrawString(string str, double fontSize, Color color)
        {
            // 視体積の設定
            GL.MatrixMode(MatrixMode.Projection);
            {
                float r = (float)TkGraphics.CurrentWidth /  (float)TkGraphics.CuurentHeight;
                int h = 800;
                float w =  h * r;
                Matrix4 proj = Matrix4.CreateOrthographic(w, h, 0.01f, 1000.0f);
                GL.LoadMatrix(ref proj);
            }
            GL.MatrixMode(MatrixMode.Modelview);

            // text分の四角形のビットマップを作成した、その四角形をテクスチャとして貼り付けることで描画する。

            var window = Application.Current.MainWindow;

            // テキストの色定義
            Brush foreground = new SolidColorBrush(color);
            double pixelsPerDip = 0;

            var text = new FormattedText(
                str,
                new System.Globalization.CultureInfo("en-us"),
                FlowDirection.LeftToRight,
                new Typeface(
                    window.FontFamily,
                    FontStyles.Normal,
                    FontWeights.Normal,
                    FontStretches.Normal),
                fontSize, 
                foreground,
                pixelsPerDip);

            System.Windows.Media.Imaging.RenderTargetBitmap bmp = null;
            {
                int width = (int)Math.Ceiling(text.Width);
                int height = (int)Math.Ceiling(text.Height);
                var dpi = VisualTreeHelper.GetDpi(this);
                double dpiX = dpi.PixelsPerInchX;  //dot per inc 解像度
                double dpiY = dpi.PixelsPerInchY;
                bmp = new System.Windows.Media.Imaging.RenderTargetBitmap(
                    width, height, dpiX, dpiY, PixelFormats.Pbgra32);
            }

            var drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawRectangle(Brushes.White, null, new Rect(0.0, 0.0, bmp.PixelWidth, bmp.PixelHeight));
                drawingContext.DrawText(text, new Point(0, 0));
            }

            bmp.Render(drawingVisual);

            // ビットマップの幅、高さ取得
            int bmpWidth = bmp.PixelWidth;
            int bmpHeight = bmp.PixelHeight;
            int stride = bmpWidth * 4;
            byte[] tmpbits = new byte[stride * bmpHeight];
            var rectangle = new Int32Rect(0, 0, bmpWidth, bmpHeight);
            bmp.CopyPixels(rectangle, tmpbits, stride, 0);
            // 上下反転する
            byte[] bits = new byte[stride * bmpHeight];
            for (int h = 0; h < bmpHeight; h++)
            {
                for (int w = 0; w < stride; w++)
                {
                    bits[h * stride + w] = tmpbits[(bmpHeight - 1 - h) * stride + w];
                }
            }

            bool isTexture = GL.IsEnabled(EnableCap.Texture2D);
            GL.Enable(EnableCap.Texture2D);

            // テクスチャIDの作成
            int texture = GL.GenTexture();
            // テクスチャ用バッファの紐づけ
            GL.BindTexture(TextureTarget.Texture2D, texture);
            // テクスチャの設定
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,(int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,(int)TextureMagFilter.Linear);
            // テクスチャ割り当て
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmpWidth, bmpHeight, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bits);

            // 四角形で描画
            GL.Begin(PrimitiveType.Quads);
            GL.TexCoord2(0, 0);
            GL.Vertex2(0, 0);
            GL.TexCoord2(1, 0);
            GL.Vertex2(bmpWidth, 0);
            GL.TexCoord2(1, 1);
            GL.Vertex2(bmpWidth, bmpHeight);
            GL.TexCoord2(0, 1);
            GL.Vertex2(0, bmpHeight);
            GL.End();

            if (!isTexture)
            {
                GL.Disable(EnableCap.Texture2D);
            }

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
        /// <param name="xMin"></param>
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
        /// <param name="yCenter"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private double CoordinateYTransformation(double y, double yCenter, int n)
        {
            return Math.Round((-y * YRange / TkGraphics.CuurentHeight) + YRange / 2 + yCenter, n);
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

            // x座標変換
            var x = CoordinateXTransformation(point.X, XMin, DisplayDisits);
            // y座標変換 ※ActualHeightとpoint.Yの間に何故か1.25の差が生じている...
            var y = CoordinateYTransformation(point.Y, YCenter, DisplayDisits);

            // Viewの値の変更をViewModelにも伝えてあげる。
            SetCurrentValue(CurrentXPositionProperty, x);
            SetCurrentValue(CurrentYPositionProperty, y);

            if (_isDrag == true)
            {
                // マウス座標を更新します。 :　描画領域の変化に応じてXMin,YCenterが変化するので、ドラッグ開始時のXMin,YCenterを足してあげます。
                double _movedx = CoordinateXTransformation(point.X, _dragOffsetXMin, DisplayDisits);
                double _movedy = CoordinateYTransformation(point.Y, _dragOffsetYCenter, DisplayDisits);

                // ドラッグ量 MouseMoveイベントは常に走り続けるため、1周期前の座標を現在の座標から引くことで変化量を求めます。
                double _xTranslate = Math.Round(_movedx - _oldXPosition, DisplayDisits);
                double _yTranslate = Math.Round(_movedy - _oldYPosition, DisplayDisits);

                // 前回のマウス座標を更新
                this._oldXPosition = _movedx;
                this._oldYPosition = _movedy;

                // プロパティ値更新,  x,yの最大・最小を変化させることで描画領域を移動
                SetCurrentValue(XMaxProperty, XMax - _xTranslate);
                SetCurrentValue(XMinProperty, XMin - _xTranslate);
                SetCurrentValue(YMaxProperty, YMax - _yTranslate);
                SetCurrentValue(YMinProperty, YMin - _yTranslate);
            }
        }

        /// <summary>
        /// マウスの左のボタンを押した際に実行されるイベントハンドラです。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnMouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            UIElement el = sender as UIElement;
            if (el != null)
            {
                // ドラッグフラグをtrueにします。
                _isDrag = true;

                // ドラッグ開始時の座標を取得します。
                _dragOffset = e.GetPosition(el);
                _oldXPosition = CoordinateXTransformation(_dragOffset.X, XMin, DisplayDisits);
                _oldYPosition = CoordinateYTransformation(_dragOffset.Y, YCenter, DisplayDisits);

                // ドラッグ開始時のx、ｙの最小・最大、yの中間値を取得します。
                _dragOffsetXMax = XMax;
                _dragOffsetXMin = XMin;
                _dragOffsetYCenter = YCenter;
                _dragOffsetYMax = YMax;
                _dragOffsetYMin = YMin;

                el.CaptureMouse();

                Render();
                
            }
        }

        /// <summary>
        /// マウスの左ボタンを離した際に実行されるイベントハンドラです。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnMouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            if (_isDrag == true)
            {
                // ドラッグフラグをfalseにします。
                UIElement el = sender as UIElement;
                el.ReleaseMouseCapture();
                _isDrag = false;

            }
        }

        /// <summary>
        /// キーボードのEscキーを押した際に実行されるイベントハンドラです。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnEscKeyDown(object sender, KeyEventArgs e)
        {
            if (_isDrag == true)
            {
                if (e.Key == Key.Escape)
                {
                    SetCurrentValue(XMaxProperty, _dragOffsetXMax);
                    SetCurrentValue(XMinProperty, _dragOffsetXMin);
                    SetCurrentValue(YMaxProperty, _dragOffsetYMax);
                    SetCurrentValue(YMinProperty, _dragOffsetYMin);
                    Render();
                }
            }
        }
        #endregion　マウスイベント

        #region マウスイベント用フィールド
        /// <summary>
        /// 現在のドラッグ状態を表します。
        /// </summary>
        private bool _isDrag = false;

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

        #endregion マウスイベント用フィールド

    }
}
