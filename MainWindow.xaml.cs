using System;
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

            Matrix4 look = Matrix4.LookAt(Vector3.UnitZ, Vector3.Zero, Vector3.UnitY);
            GL.LoadMatrix(ref look);
            GL.Enable(EnableCap.DepthTest);
        }

        private void tkControl_OnLoaded(object sender, RoutedEventArgs e)
        {
            SetProjection();
        }

        private void tkControl_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetProjection();
        }

        private void tkControl_OnRender(TimeSpan delta)
        {
            GL.ClearColor(Color4.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.PushMatrix();
            {
                // (-2, -2) まで移動
                GL.Translate(-2.0, -2.0, 0.0);

                for (int i = 0; i < 5; i++) // x 方向
                {
                    GL.PushMatrix();
                    {
                        for (int j = 0; j < 5; j++) // y 方向
                        {
                            // 正方形を描画
                            DrawSquare(new Color4(i * 0.25f, j * 0.25f, 0.0f, 255.0f));
                            // y 方向に1移動
                            GL.Translate(0.0, 1.0, 0.0);
                        }
                    }
                    GL.PopMatrix();

                    // x 方向に1移動
                    GL.Translate(1.0, 0.0, 0.0);
                }
            }
            GL.PopMatrix();

            GL.Finish();
        }

        private void DrawSquare()
        {
            const double side = 0.45;

            GL.Begin(PrimitiveType.TriangleStrip);
            {
                GL.Vertex2( side,  side);
                GL.Vertex2(-side,  side);
                GL.Vertex2( side, -side);
                GL.Vertex2(-side, -side);
            }
            GL.End();
        }

        private void DrawSquare(Color4 color)
        {
            GL.Color4(color);
            DrawSquare();
        }

        private void SetProjection()
        {
            // ビューポートの設定
            GL.Viewport(0, 0, (int)this.tkControl.ActualWidth, (int)this.tkControl.ActualHeight);

            // 視体積の設定
            GL.MatrixMode(MatrixMode.Projection);
            {
                var r = this.tkControl.ActualWidth / this.tkControl.ActualHeight;
                float h = 6.0f, w = (float)(h * r);  //-3 <= y <= 3 に変更し、 (幅) = (高さ) × (アスペクト比) で歪みが出ないように
                Matrix4 proj = Matrix4.CreateOrthographic(w, h, 0.01f, 2.0f);
                GL.LoadMatrix(ref proj);
            }
            GL.MatrixMode(MatrixMode.Modelview);
        }
    }
}
