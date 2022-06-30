using System;
using System.Diagnostics;
using System.Windows;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Wpf;

namespace OpenTkApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var settings = new GLWpfControlSettings()
            {
                MajorVersion = 2,
                MinorVersion = 1,
            };
            this.tkControl.Start(settings);
        }

        private void tkControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            //SetProjection();
        }

        private void tkControl_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //SetProjection();
        }
private static readonly Stopwatch _stopwatch = Stopwatch.StartNew();
        private void tkControl_OnRender(TimeSpan delta)
        {
            var alpha = 1.0f;
            var hue = (float) _stopwatch.Elapsed.TotalSeconds * 0.15f % 1;
            var c = Color4.FromHsv(new Vector4(alpha * hue, alpha * 0.75f, alpha * 0.75f, alpha));
            GL.ClearColor(c);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.LoadIdentity();
            GL.Begin(PrimitiveType.Triangles);

            GL.Color4(Color4.Red);
            GL.Vertex2(0.0f, 0.5f);

            GL.Color4(Color4.Green);
            GL.Vertex2(0.58f, -0.5f);

            GL.Color4(Color4.Blue);
            GL.Vertex2(-0.58f, -0.5f);

            GL.End();
            // GL.PushMatrix();
            // {
            //     // (-2, -2) まで移動
            //     GL.Translate(-2.0, -2.0, 0.0);

            //     for (int i = 0; i < 5; i++) // x 方向
            //     {
            //         GL.PushMatrix();
            //         {
            //             for (int j = 0; j < 5; j++) // y 方向
            //             {
            //                 // 色の変更
            //                 GL.Color3(i * 0.25, j * 0.25, 0.0);
            //                 // 正方形を描画
            //                 DrawSquare();
            //                 // y 方向に1移動
            //                 GL.Translate(0.0, 1.0, 0.0);
            //             }
            //         }
            //         GL.PopMatrix();

            //         // x 方向に1移動
            //         GL.Translate(1.0, 0.0, 0.0);
            //     }
            // }
            // GL.PopMatrix();

            GL.Finish();
        }

        private void DrawSquare()
        {
            double side = 0.45;
            GL.Begin(PrimitiveType.TriangleStrip);
            {
                GL.Vertex2(side, side);
                GL.Vertex2(-side, side);
                GL.Vertex2(side, -side);
                GL.Vertex2(-side, -side);
            }
            GL.End();
        }

        private void SetProjection()
        {
            // ビューポートの設定
            GL.Viewport(0, 0, (int)this.tkControl.ActualWidth, (int)this.tkControl.ActualHeight);

            // 視体積の設定
            GL.MatrixMode(MatrixMode.Projection);
            var r = this.tkControl.ActualWidth / this.tkControl.ActualHeight;
            float h = 6.0f, w = (float)(h * r);  //-2 <= y <= 2 に変更し、 (幅) = (高さ) × (アスペクト比) で歪みが出ないように
            Matrix4 proj = Matrix4.CreateOrthographic(w, h, 0.01f, 2.0f);
            GL.LoadMatrix(ref proj);

            // MatrixMode を元に戻す
            GL.MatrixMode(MatrixMode.Modelview);
        }
    }
}
