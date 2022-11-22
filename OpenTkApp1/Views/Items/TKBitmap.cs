using System;
//using System.Drawing;
using System.Windows;
using System.Windows.Media;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTkApp1.Views.Items;

namespace OpenTkApp1.Views
{
    /// <summary>
    /// グラフに表示するテクスチャの元となる、ビットマップの作成に関するクラスです。
    /// </summary>
    public class TkBitmap : ItemPosition
    {
        double pixelsPerDip = 96;
        double dpiX = 96;  //dot per inc 解像度
        double dpiY = 96;

        /// <summary>
        /// 凡例のビットマップを作成するメソッドです。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="fontSize"></param>
        /// <param name="color"></param>
        /// <param name="lineColor"></param>
        public void CreateLegend(string str, double fontSize, Color color, Color4 lineColor)
        {
            var window = Application.Current.MainWindow;

            // テキストの色定義
            Brush foreground = new SolidColorBrush(color);
            
            // 線種1の色を取得します。
            Color linecolor1 = Draw2MediaColor((System.Drawing.Color)lineColor);

            // テキストのフォーマット定義
            var text = new FormattedText(str, new System.Globalization.CultureInfo("en-us"),
                FlowDirection.LeftToRight, new Typeface(window.FontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal), fontSize, foreground, pixelsPerDip);

            var linetype = new FormattedText("―", new System.Globalization.CultureInfo("en-us"), FlowDirection.LeftToRight,
                new Typeface(window.FontFamily, FontStyles.Normal, FontWeights.UltraBlack, FontStretches.Normal), fontSize, new SolidColorBrush(linecolor1), pixelsPerDip);

            var linetpe2 = new FormattedText("\r\n―", new System.Globalization.CultureInfo("en-us"), FlowDirection.LeftToRight,
                new Typeface(window.FontFamily, FontStyles.Normal, FontWeights.UltraBlack, FontStretches.Normal), fontSize, new SolidColorBrush(Colors.Orange), pixelsPerDip);

            var linetype3 = new FormattedText("\r\n\r\n―", new System.Globalization.CultureInfo("en-us"), FlowDirection.LeftToRight,
               new Typeface(window.FontFamily, FontStyles.Normal, FontWeights.UltraBlack, FontStretches.Normal), fontSize, new SolidColorBrush(Colors.White), pixelsPerDip);

            // 文字と枠線のスペース
            int space = 10;

            // ビットマップのフォーマット定義
            System.Windows.Media.Imaging.RenderTargetBitmap bmp = null;
            {
                int width = (int)Math.Ceiling(text.Width + linetype.Width);
                int height = (int)Math.Ceiling(text.Height);
                bmp = new System.Windows.Media.Imaging.RenderTargetBitmap(width + space * 2, height, dpiX, dpiY, PixelFormats.Pbgra32);
            }

            this.BitmapRect = new Rect(0.0, 0.0, bmp.PixelWidth, bmp.PixelHeight);
            var drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                Pen pen = new Pen(foreground, 3);
                // テキストを書く下地を作る
                drawingContext.DrawRectangle(Brushes.Black, pen, this.BitmapRect);
                // テキストを書く
                drawingContext.DrawText(text, new Point(linetype.Width + space, 0));
                drawingContext.DrawText(linetype, new Point(space, 0));
                drawingContext.DrawText(linetpe2, new Point(space, 0));
                drawingContext.DrawText(linetype3, new Point(space, 0));
            }

            // ビットマップ作成
            bmp.Render(drawingVisual);

            // ビットマップの幅、高さ取得
            this.Width = bmp.PixelWidth;
            this.Height = bmp.PixelHeight;
            // stride: 画像の横１列分のデータサイズ = 画像の横幅 * 1画素あたりのbyte(4byte) 
            int stride = bmp.PixelWidth * 4;
            // ビットマップ全体のサイズ分の配列を定義
            byte[] tmpbits = new byte[stride * bmp.PixelHeight];
            var rectangle = new Int32Rect(0, 0, bmp.PixelWidth, bmp.PixelHeight);
            bmp.CopyPixels(rectangle, tmpbits, stride, 0);
            // ビットマップを上下反転させる。画像空間の座標と、テクスチャ空間の座標が反転しているため。画像空間は左上原点の軸方向が第4事象、テクスチャ空間は左下原点の第1事象。
            _bits = new byte[stride * bmp.PixelHeight];
            for (int h = 0; h < bmp.PixelHeight; h++)
            {
                for (int w = 0; w < stride; w++)
                {
                    _bits[h * stride + w] = tmpbits[(bmp.PixelHeight - 1 - h) * stride + w];
                }
            }
            // 作成したビットマップをテクスチャに貼り付ける設定を行います。
            SettingTexture();
        }

        /// <summary>
        /// グラフデータのビットマップを作成するメソッドです。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="fontSize"></param>
        /// <param name="color"></param>
        public void CreateGraphData(string str, double fontSize, Color textColor, Color frameColor)
        {
            CreateBitmap(str,fontSize, textColor, frameColor);

            if (TextureList.Textures.Count == 1)
                // 作成したビットマップをテクスチャに貼り付ける設定を行います。
                SettingTexture();
            else CustomTexture(3);
        }

        /// <summary>
        /// グラフカーソルのビットマップを作成するメソッドです。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="fontSize"></param>
        /// <param name="color"></param>
        public void CreateGraphCursor(string str, double fontSize, Color textColor, Color frameColor)
        {
            CreateBitmap(str,fontSize, textColor, frameColor);

            if (TextureList.Textures.Count == 2)
                // 作成したビットマップをテクスチャに貼り付ける設定を行います。
                SettingTexture();
            else CustomTexture(4);
        }

        /// <summary>
        /// 与えられた引数に対して、ビットマップを作成します。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="fontSize"></param>
        /// <param name="color"></param>
        private void CreateBitmap(string str, double fontSize, Color color, Color frameColor)
        {
            var window = Application.Current.MainWindow;

            // テキストの色定義
            Brush foreground = new SolidColorBrush(color);

            // 枠線の色定義
            Brush frame = new SolidColorBrush(frameColor);

            // テキストのフォーマット定義
            var text = new FormattedText(str, new System.Globalization.CultureInfo("en-us"),
                FlowDirection.LeftToRight, new Typeface(window.FontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal), fontSize, foreground, pixelsPerDip);

            // 文字と枠線のスペース
            int space = 10;

            // ビットマップのフォーマット定義
            System.Windows.Media.Imaging.RenderTargetBitmap bmp = null;
            {
                int width = (int)Math.Ceiling(text.Width);
                int height = (int)Math.Ceiling(text.Height);
                bmp = new System.Windows.Media.Imaging.RenderTargetBitmap(width + space * 2, height, dpiX, dpiY, PixelFormats.Pbgra32);
            }

            this.BitmapRect = new Rect(0.0, 0.0, bmp.PixelWidth, bmp.PixelHeight);
            var drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                Pen pen = new Pen(frame, 3);
                // テキストを書く下地を作る
                drawingContext.DrawRectangle(Brushes.Black, pen, this.BitmapRect);
                // テキストを書く
                drawingContext.DrawText(text, new Point(space, -2));
            }

            // ビットマップ作成
            bmp.Render(drawingVisual);

            // ビットマップの幅、高さ取得
            this.Width = bmp.PixelWidth;
            this.Height = bmp.PixelHeight;
            // stride: 画像の横１列分のデータサイズ = 画像の横幅 * 1画素あたりのbyte(4byte) 
            int stride = bmp.PixelWidth * 4;
            // ビットマップ全体のサイズ分の配列を定義
            byte[] tmpbits = new byte[stride * bmp.PixelHeight];
            var rectangle = new Int32Rect(0, 0, bmp.PixelWidth, bmp.PixelHeight);
            bmp.CopyPixels(rectangle, tmpbits, stride, 0);
            // ビットマップを上下反転させる。画像空間の座標と、テクスチャ空間の座標が反転しているため。画像空間は左上原点の軸方向が第4事象、テクスチャ空間は左下原点の第1事象。
            _bits = new byte[stride * bmp.PixelHeight];
            for (int h = 0; h < bmp.PixelHeight; h++)
            {
                for (int w = 0; w < stride; w++)
                {
                    _bits[h * stride + w] = tmpbits[(bmp.PixelHeight - 1 - h) * stride + w];
                }
            }
        }

        /// <summary>
        /// テクスチャの設定を行うメソッドです。
        /// </summary>
        private void SettingTexture()
        {
            // テクスチャを有効化します。
            GL.Enable(EnableCap.Texture2D);

            // テクスチャIDの作成
            int texture = GL.GenTexture();

            // テクスチャIDをコレクションに追加します。
            TextureList.Textures.Add(texture);

            // 指定したIDのテクスチャを現在のテクスチャとします。 
            GL.BindTexture(TextureTarget.Texture2D, texture);

            // テクスチャの拡大・縮小時の補間方法の設定をします。
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            // ビットマップをテクスチャに割り当てます。
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, this.Width, this.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, _bits);

        }

        /// <summary>
        /// 既存テクスチャの編集を行うメソッドです。
        /// </summary>
        /// <param name="id"></param>
        private void CustomTexture(int id)
        {
            // テクスチャを有効化します。
            GL.Enable(EnableCap.Texture2D);

            // 指定したIDのテクスチャを現在のテクスチャとします。 
            GL.BindTexture(TextureTarget.Texture2D, id);

            // テクスチャの拡大・縮小時の補間方法の設定をします。
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            // ビットマップをテクスチャに割り当てます。
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, this.Width, this.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, _bits);
            GL.Disable(EnableCap.Texture2D);

        }

        /// <summary>
        /// System.Drawing.ColorからSystem.Windows.Media.Colorに変換するメソッドです。
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        static public System.Windows.Media.Color Draw2MediaColor(System.Drawing.Color color)
        {
            return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        /// <summary>
        /// ビットマップの幅を取得、設定します。
        /// </summary>
        public int Width { get; set;}

        /// <summary>
        /// ビットマップの高さを取得、設定します。
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// ビットマップの下地の四角形を取得、設定します。
        /// </summary>
        public Rect BitmapRect { get; set; }

        /// <summary>
        /// ビットマップの配列を格納します。
        /// </summary>
        private byte[]? _bits;

        

    }
}
