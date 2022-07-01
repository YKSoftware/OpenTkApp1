using System;
using System.Windows;
using System.Windows.Markup;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Wpf;

namespace OpenTkApp1.Views;

[ContentProperty("DrawingItem")]
public class TkGraphics : GLWpfControl
{
    /// <summary>
    /// DrawingItem 依存関係プロパティの定義を表します。
    /// </summary>
    public static readonly DependencyProperty DrawingItemProperty = DependencyProperty.Register("DrawingItem", typeof(ITkGraphicsItem), typeof(TkGraphics), new PropertyMetadata(null));

    /// <summary>
    /// 描画内容を取得または設定します。
    /// </summary>
    public ITkGraphicsItem? DrawingItem
    {
        get => (ITkGraphicsItem?)GetValue(DrawingItemProperty);
        set => SetValue(DrawingItemProperty, value);
    }

    /// <summary>
    /// 新しいインスタンスを生成します。
    /// </summary>
    public TkGraphics()
    {
        var settings = new GLWpfControlSettings()
        {
            MajorVersion = 2,
            MinorVersion = 1,
        };
        Start(settings);

        
        var look = Matrix4.LookAt(Vector3.UnitZ, Vector3.Zero, Vector3.UnitY);
        GL.LoadMatrix(ref look);
        GL.Enable(EnableCap.DepthTest);

        this.Loaded += OnLoaded;
        this.SizeChanged += OnSizeChanged;
        this.Render += OnTkRender;
    }

    /// <summary>
    /// Loaded イベントハンドラ
    /// </summary>
    /// <param name="sender">イベント発行元</param>
    /// <param name="e">イベント引数</param>
    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        SetProjection();
    }

    /// <summary>
    /// SizeChanged イベントハンドラ
    /// </summary>
    /// <param name="sender">イベント発行元</param>
    /// <param name="e">イベント引数</param>
    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        SetProjection();
    }

    /// <summary>
    /// Render イベントハンドラ
    /// </summary>
    /// <param name="delta">経過時間</param>
    private void OnTkRender(TimeSpan delta)
    {
        DrawingItem?.Render();
    }

    /// <summary>
    /// 投影方法を設定します。
    /// </summary>
    private void SetProjection()
    {
        // ビューポートの設定
        GL.Viewport(0, 0, (int)this.ActualWidth, (int)this.ActualHeight);

        // 視体積の設定
        GL.MatrixMode(MatrixMode.Projection);
        {
            var r = this.ActualWidth / this.ActualHeight;
            Matrix4 proj = Matrix4.CreateOrthographic(ViewWidth, ViewHight, 0.01f, 1000.0f);
            GL.LoadMatrix(ref proj);
        }
        GL.MatrixMode(MatrixMode.Modelview);
    }

    static public float ViewHight { get { return _viewHight; } }
    static private float _viewHight = 20.0f;
    static public float ViewWidth {  get { return _viewWidth; } }
    static private float _viewWidth = 2000.0f;
    static public float AspectRatio { get { return _viewWidth / _viewHight; } }
}
