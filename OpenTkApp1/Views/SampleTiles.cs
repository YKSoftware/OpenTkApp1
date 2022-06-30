using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace OpenTkApp1.Views;

/// <summary>
/// タイルを描画するサンプルを表します。
/// </summary>
public class SampleTiles : ITkGraphicsItem
{
    /// <summary>
    /// 描画処理をおこないます。
    /// </summary>
    public void Render()
    {
        GL.ClearColor(Color4.Maroon);
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

    /// <summary>
    /// 正方形を描画します。
    /// </summary>
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

    /// <summary>
    /// 正方形を描画します。
    /// </summary>
    /// <param name="color">塗潰す色を指定します。</param>
    private void DrawSquare(Color4 color)
    {
        GL.Color4(color);
        DrawSquare();
    }
}