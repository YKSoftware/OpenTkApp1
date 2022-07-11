using System;
using System.Windows;
using System.Windows.Markup;
using System.Linq;
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
    public static readonly DependencyProperty DrawingItemProperty = DependencyProperty.Register("DrawingItem", typeof(ITkGraphicsItem), typeof(TkGraphics), new PropertyMetadata(null, OnDrawingItemPropertyChanged));

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
        var control = (TkGraphics)d;
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
            // 座標の範囲を決める    -Range/2 <= x or y <= Range/2 
            var xRange = 1000;
            var yRange = 300;
            Matrix4 proj = Matrix4.CreateOrthographic(xRange, yRange, 0.01f, 1000.0f);
            GL.LoadMatrix(ref proj);
        }
        GL.MatrixMode(MatrixMode.Modelview);
    }


}
