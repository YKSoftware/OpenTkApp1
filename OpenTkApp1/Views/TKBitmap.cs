using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace OpenTkApp1.Views
{
    public class TKBitmap
    {
        /// <summary>
        /// グラフカーソルのビットマップを作成するメソッドです。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="fontSize"></param>
        /// <param name="color"></param>
        public void CreateGraphCursolBitmap(string str, double fontSize, Color color)
        {
            var window = Application.Current.MainWindow;

            // テキストの色定義
            Brush foreground = new SolidColorBrush(color);

            double pixelsPerDip = 96;

            // テキストのフォーマット定義
            var text = new FormattedText(str, new System.Globalization.CultureInfo("en-us"),
                FlowDirection.LeftToRight, new Typeface(window.FontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal), fontSize, foreground, pixelsPerDip);

            // ビットマップのフォーマット定義
            System.Windows.Media.Imaging.RenderTargetBitmap bmp = null;
            {
                int width = (int)Math.Ceiling(text.Width);
                int height = (int)Math.Ceiling(text.Height);
                double dpiX = 96;  //dot per inc 解像度
                double dpiY = 96;
                bmp = new System.Windows.Media.Imaging.RenderTargetBitmap(width, height, dpiX, dpiY, PixelFormats.Pbgra32);
            }

            var drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                // テキストを書く下地を作る
                drawingContext.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, bmp.PixelWidth, bmp.PixelHeight));
                // テキストを書く
                drawingContext.DrawText(text, new Point(0, 0));
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
            if (TextureList.Textures.Count == 0)
                // 作成したビットマップをテクスチャに貼り付ける設定を行います。
                SettingTexture();
            else CustomTexture(3);
        }

        /// <summary>
        /// テクスチャの設定を行うメソッドです。。
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
        /// ビットマップの幅を取得、設定します。
        /// </summary>
        public int Width { get; set;}

        /// <summary>
        /// ビットマップの高さを取得、設定します。
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// ビットマップの配列を格納します。
        /// </summary>
        private byte[] _bits;
    }
}
