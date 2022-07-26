using System;
using System.Windows;
using System.Windows.Markup;
using System.Linq;
using System.Windows.Input;
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
     #region XRange
    public static readonly DependencyProperty XRangeProperty = DependencyProperty.Register("XRange", typeof(double), typeof(TkGraphics), new PropertyMetadata(0.0, OnXRangePropertyChanged));

    public double XRange
    {
        get => (double)GetValue(XRangeProperty);
        set => SetValue(XRangeProperty, value);
    }

    private static void OnXRangePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion XRange

     #region YRange
    public static readonly DependencyProperty YRangeProperty = DependencyProperty.Register("YRange", typeof(double), typeof(TkGraphics), new PropertyMetadata(0.0, OnYRangePropertyChanged));

    public double YRange
    {
        get => (double)GetValue(YRangeProperty);
        set => SetValue(YRangeProperty, value);
    }

    private static void OnYRangePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion YRange

     #region XMin
    public static readonly DependencyProperty XMinProperty = DependencyProperty.Register("XMin", typeof(double), typeof(TkGraphics), new PropertyMetadata(0.0, OnXMinPropertyChanged));

    public double XMin
    {
        get => (double)GetValue(XMinProperty);
        set => SetValue(XMinProperty, value);
    }

    private static void OnXMinPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion XMin

     #region YCenter
    public static readonly DependencyProperty YCenterProperty = DependencyProperty.Register("YCenter", typeof(double), typeof(TkGraphics), new PropertyMetadata(0.0, OnYCenterPropertyChanged));

    public double YCenter
    {
        get => (double)GetValue(YCenterProperty);
        set => SetValue(YCenterProperty, value);
    }

    private static void OnYCenterPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion YCenter

     #region CurrentXPotition
    public static readonly DependencyProperty CurrentXPotitionProperty = DependencyProperty.Register("CurrentXPotition", typeof(double), typeof(TkGraphics), new PropertyMetadata(0.0, OnCurrentXPotitionPropertyChanged));

    public double CurrentXPotition
    {
        get => (double)GetValue(CurrentXPotitionProperty);
        set => SetValue(CurrentXPotitionProperty, value);
    }

    private static void OnCurrentXPotitionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion CurrentXPotition

   　#region OnMouseMoved
    public static readonly DependencyProperty MouseMovedProperty = DependencyProperty.Register("OnMouseMoved", typeof(Action<double,double>), typeof(TkGraphics), new PropertyMetadata(null, OnMouseMovedPropertyChanged));

    public Action<double, double> OnMouseMoved
    {
        get => (Action<double,double>)GetValue(MouseMovedProperty);
        set => SetValue(MouseMovedProperty, value);
    }

    private static void OnMouseMovedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion OnMouseMoved

     #region OnMouseLeftButtonDowned
    public static readonly DependencyProperty MouseLeftButtonDownedProperty = DependencyProperty.Register("OnMouseLeftButtonDowned", typeof(Action<double, double>), typeof(TkGraphics), new PropertyMetadata(null, OnMouseLeftButtonDownedPropertyChanged));

    public Action<double, double> OnMouseLeftButtonDowned
    {
        get => (Action<double, double>)GetValue(MouseLeftButtonDownedProperty);
        set => SetValue(MouseLeftButtonDownedProperty, value);
    }

    private static void OnMouseLeftButtonDownedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion OnMouseLeftButtonDowned

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
        this.MouseMove += OnMouseMove;
        this.MouseLeftButtonDown += OnMouseLeftButtonDown;
        this.MouseLeftButtonUp += OnMouseLeftButtonUp;
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
            Matrix4 proj = Matrix4.CreateOrthographic((int)XRange, (int)YRange, 0.01f, 1000.0f);
            GL.LoadMatrix(ref proj);
        }
        GL.MatrixMode(MatrixMode.Modelview);
    }

    /// <summary>
    /// マウスが移動した際に実行されるイベントハンドラです。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        Point point = e.GetPosition(this);
        // x座標変換
        var x = (point.X * XRange / ActualWidth + XMin);
        //var x = Math.Round((point.X * XRange/ActualWidth + XMin),0);
        // y座標変換 ※ActualHeightとpoint.Yの間に何故か1.25の差が生じている...
        var y = (-((point.Y) * YRange / ActualHeight)) + YRange / 2 + YCenter;
        //var y = Math.Round((-((point.Y) * YRange/ActualHeight)) + YRange / 2 + YCenter , 0);
        this.OnMouseMoved(x, y);

        if (_isDrag == true)
        {
            double _movedx = (point.X * XRange / ActualWidth + oldXMin);
            //  ドラッグ量
            double _xtranslate = _movedx - oldX;
            this.oldX = _movedx;

            this.OnMouseLeftButtonDowned(_xtranslate,0);
        }
    }
    private double oldX;
    private double oldXMin;

    // マウス左ボタン押した時
    private void OnMouseLeftButtonDown(object sender, MouseEventArgs e)
    {
        UIElement el = sender as UIElement;
        if(el != null)
        {
            _isDrag = true;
            _dragOffset = e.GetPosition(el);
            _dragOffsetX = _dragOffset.X * XRange / ActualWidth + XMin;
            oldX = _dragOffset.X * XRange / ActualWidth + XMin;
            oldXMin = XMin;

            el.CaptureMouse();
        }
    }

    // マウス左ボタン離した時
    private void OnMouseLeftButtonUp(object sender, MouseEventArgs e)
    {
        if (_isDrag == true)
        {
            UIElement el = sender as UIElement;
            el.ReleaseMouseCapture();
            _isDrag = false;
        }
    }

    private bool _isDrag = false;

    private Point _dragOffset;

    private double _dragOffsetX;
}
