using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Wpf;

namespace OpenTkApp1.Views;


[ContentProperty("GraphBase")]
public class TkGraphics : GLWpfControl
{

    /// <summary>
    /// GraphBase 依存関係プロパティの定義を表します。
    /// </summary>
    public static readonly DependencyProperty GraphBaseProperty = DependencyProperty.Register("GraphBase", typeof(ITkGraphBase), typeof(TkGraphics), new PropertyMetadata(null, OnGraphBasePropertyChanged));

    /// <summary>
    /// 描画内容を取得または設定します。
    /// </summary>
    public ITkGraphBase? GraphBase
    {
        get => (ITkGraphBase?)GetValue(GraphBaseProperty);
        set => SetValue(GraphBaseProperty, value);
    }

    /// <summary>
    /// GraphBase 依存関係プロパティ変更イベントハンドラ
    /// </summary>
    /// <param name="d">イベント発行元</param>
    /// <param name="e">イベント引数</param>
    private static void OnGraphBasePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var control = (TkGraphics)d;
        control.OnGraphBasePropertyChanged(e.OldValue, e.NewValue);
    }

    /// <summary>
    /// DrawingItem 依存関係プロパティ変更イベントハンドラ
    /// </summary>
    /// <param name="oldItem">変更前の値</param>
    /// <param name="newItem">変更後の値</param>
    private void OnGraphBasePropertyChanged(object oldItem, object newItem)
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
        GraphBase?.CreateTextBitmap();
        // コンストラクタのタイミングだと、GraphBaseがnullになってしまう。
        this.MouseMove += GraphBase.OnMouseMove;
        this.MouseLeftButtonDown += GraphBase.OnMouseLeftButtonDown;
        this.MouseLeftButtonUp += GraphBase.OnMouseLeftButtonUp;
        this.KeyDown += GraphBase.OnEscKeyDown;
    }

    /// <summary>
    /// SizeChanged イベントハンドラ
    /// </summary>
    /// <param name="sender">イベント発行元</param>
    /// <param name="e">イベント引数</param>
    private void OnSizeChanged(object sender, SizeChangedEventArgs e)
    {
        SetProjection();
        _currentWidth = ActualWidth;
        _currentHeight = ActualHeight;
        GraphBase?.BitmapPositionChange();

    }

    /// <summary>
    /// Render イベントハンドラ
    /// </summary>
    /// <param name="delta">経過時間</param>
    private void OnTkRender(TimeSpan delta)
    {
        GraphBase?.Render();
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
            Matrix4 proj = Matrix4.CreateOrthographic((int)GraphBase.XRange, (int)GraphBase.YRange, 0.01f, 1000.0f);
            GL.LoadMatrix(ref proj);
        }
        GL.MatrixMode(MatrixMode.Modelview);
    }

    private static double _currentWidth; 
    public static double CurrentWidth { get { return _currentWidth; } }

    private static double _currentHeight;
    public static double CurrentHeight { get { return _currentHeight; } }

}
