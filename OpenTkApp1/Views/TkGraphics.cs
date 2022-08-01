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

    #region XRange
    /// <summary>
    /// XRange 依存関係プロパティの定義を表します。
    /// </summary>
    public static readonly DependencyProperty XRangeProperty = DependencyProperty.Register("XRange", typeof(double), typeof(TkGraphics), new PropertyMetadata(0.0, OnXRangePropertyChanged));

    public double XRange
    {
        get => (double)GetValue(XRangeProperty);
        set => SetValue(XRangeProperty, value);
    }

    /// <summary>
    /// XRangeプロパティ変更イベントハンドラ
    /// </summary>
    /// <param name="d">イベント発行元</param>
    /// <param name="e">イベント引数</param
    private static void OnXRangePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion XRange

    #region YRange
    /// <summary>
    /// YRange 依存関係プロパティの定義を表します。
    /// </summary>
    public static readonly DependencyProperty YRangeProperty = DependencyProperty.Register("YRange", typeof(double), typeof(TkGraphics), new PropertyMetadata(0.0, OnYRangePropertyChanged));

    public double YRange
    {
        get => (double)GetValue(YRangeProperty);
        set => SetValue(YRangeProperty, value);
    }

    /// <summary>
    /// YRangeプロパティ変更イベントハンドラ
    /// </summary>
    /// <param name="d">イベント発行元</param>
    /// <param name="e">イベント引数</param
    private static void OnYRangePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion YRange

    #region XMin
    /// <summary>
    /// XMin 依存関係プロパティの定義を表します。
    /// </summary>
    public static readonly DependencyProperty XMinProperty = DependencyProperty.Register("XMin", typeof(double), typeof(TkGraphics), new PropertyMetadata(0.0, OnXMinPropertyChanged));

    public double XMin
    {
        get => (double)GetValue(XMinProperty);
        set => SetValue(XMinProperty, value);
    }

    /// <summary>
    /// XMinプロパティ変更イベントハンドラ
    /// </summary>
    /// <param name="d">イベント発行元</param>
    /// <param name="e">イベント引数</param
    private static void OnXMinPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion XMin

    #region XMax
    /// <summary>
    /// XMin 依存関係プロパティの定義を表します。
    /// </summary>
    public static readonly DependencyProperty XMaxProperty = DependencyProperty.Register("XMax", typeof(double), typeof(TkGraphics), new PropertyMetadata(0.0, OnXMaxPropertyChanged));

    public double XMax
    {
        get => (double)GetValue(XMaxProperty);
        set => SetValue(XMaxProperty, value);
    }

    /// <summary>
    /// XMaxプロパティ変更イベントハンドラ
    /// </summary>
    /// <param name="d">イベント発行元</param>
    /// <param name="e">イベント引数</param
    private static void OnXMaxPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion XMax

    #region YMin
    /// <summary>
    /// XMin 依存関係プロパティの定義を表します。
    /// </summary>
    public static readonly DependencyProperty YMinProperty = DependencyProperty.Register("YMin", typeof(double), typeof(TkGraphics), new PropertyMetadata(0.0, OnYMinPropertyChanged));

    public double YMin
    {
        get => (double)GetValue(YMinProperty);
        set => SetValue(YMinProperty, value);
    }

    /// <summary>
    /// YMinプロパティ変更イベントハンドラ
    /// </summary>
    /// <param name="d">イベント発行元</param>
    /// <param name="e">イベント引数</param
    private static void OnYMinPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion YMin

    #region YMax
    /// <summary>
    /// XMin 依存関係プロパティの定義を表します。
    /// </summary>
    public static readonly DependencyProperty YMaxProperty = DependencyProperty.Register("YMax", typeof(double), typeof(TkGraphics), new PropertyMetadata(0.0, OnYMaxPropertyChanged));

    public double YMax
    {
        get => (double)GetValue(YMaxProperty);
        set => SetValue(YMaxProperty, value);
    }

    /// <summary>
    /// YMaxプロパティ変更イベントハンドラ
    /// </summary>
    /// <param name="d">イベント発行元</param>
    /// <param name="e">イベント引数</param
    private static void OnYMaxPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion YMax

    #region YCenter
    /// <summary>
    /// YCenter 依存関係プロパティの定義を表します。
    /// </summary>
    public static readonly DependencyProperty YCenterProperty = DependencyProperty.Register("YCenter", typeof(double), typeof(TkGraphics), new PropertyMetadata(0.0, OnYCenterPropertyChanged));

    public double YCenter
    {
        get => (double)GetValue(YCenterProperty);
        set => SetValue(YCenterProperty, value);
    }

    /// <summary>
    /// YCenterプロパティ変更イベントハンドラ
    /// </summary>
    /// <param name="d">イベント発行元</param>
    /// <param name="e">イベント引数</param
    private static void OnYCenterPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion YCenter

    #region OnMouseMoved
    /// <summary>
    /// OnMouseMoved 依存関係プロパティの定義を表します。
    /// </summary>
    public static readonly DependencyProperty OnMouseMovedProperty = DependencyProperty.Register("OnMouseMoved", typeof(Action<double,double>), typeof(TkGraphics), new PropertyMetadata(null, OnMouseMovedPropertyChanged));

    public Action<double, double> OnMouseMoved
    {
        get => (Action<double,double>)GetValue(OnMouseMovedProperty);
        set => SetValue(OnMouseMovedProperty, value);
    }

    /// <summary>
    /// OnMouseMovedプロパティ変更イベントハンドラ
    /// </summary>
    /// <param name="d">イベント発行元</param>
    /// <param name="e">イベント引数</param
    private static void OnMouseMovedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion OnMouseMoved

    #region OnMouseLeftButtonDowned
    /// <summary>
    /// OnMouseLeftButtonDowned 依存関係プロパティの定義を表します。
    /// </summary>
    public static readonly DependencyProperty OnMouseLeftButtonDownedProperty = DependencyProperty.Register("OnMouseLeftButtonDowned", typeof(Action<double, double>), typeof(TkGraphics), new PropertyMetadata(null, OnMouseLeftButtonDownedPropertyChanged));

    public Action<double, double> OnMouseLeftButtonDowned
    {
        get => (Action<double, double>)GetValue(OnMouseLeftButtonDownedProperty);
        set => SetValue(OnMouseLeftButtonDownedProperty, value);
    }

    /// <summary>
    /// OnMouseLeftButtonDownedプロパティ変更イベントハンドラ
    /// </summary>
    /// <param name="d">イベント発行元</param>
    /// <param name="e">イベント引数</param
    private static void OnMouseLeftButtonDownedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion OnMouseLeftButtonDowned

    #region OnEscKeyDowned
    /// <summary>
    /// OnEscKeyButtonDowned 依存関係プロパティの定義を表します。
    /// </summary>
    public static readonly DependencyProperty OnEscKeyDownedProperty = DependencyProperty.Register("OnEscKeyDowned", typeof(Action<double, double, double, double>), typeof(TkGraphics), new PropertyMetadata(null, OnEscKeyDownedPropertyChanged));

    public Action<double, double, double, double> OnEscKeyDowned
    {
        get => (Action<double, double, double, double>)GetValue(OnEscKeyDownedProperty);
        set => SetValue(OnEscKeyDownedProperty, value);
    }

    /// <summary>
    /// OnEscKeyDownedプロパティ変更イベントハンドラ
    /// </summary>
    /// <param name="d">イベント発行元</param>
    /// <param name="e">イベント引数</param
    private static void OnEscKeyDownedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion OnEscKeyDowned

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
        // y座標変換 ※ActualHeightとpoint.Yの間に何故か1.25の差が生じている...
        var y = (-((point.Y) * YRange / ActualHeight)) + YRange / 2 + YCenter;

        this.OnMouseMoved(x, y);

        if (_isDrag == true)
        {
            // マウス座標を更新します。 :　描画領域の変化に応じてXMin,YCenterが変化するので、ドラッグ開始時のXMin,YCenterを足してあげます。
            double _movedx = (point.X * XRange / ActualWidth + _dragOffsetXMin);
            double _movedy = (-((point.Y) * YRange / ActualHeight)) + YRange / 2 + _dragOffsetYCenter;

            // ドラッグ量 MouseMoveイベントは常に走り続けるため、1周期前の座標を現在の座標から引くことで変化量を求めます。
            double _xTranslate = _movedx - _oldXPosition;
            double _yTranslate = _movedy - _oldYPosition;

            // 前回のマウス座標を更新
            this._oldXPosition = _movedx;
            this._oldYPosition = _movedy;

            this.OnMouseLeftButtonDowned(_xTranslate,_yTranslate);
        }
    }

    /// <summary>
    /// マウスの左のボタンを押した際に実行されるイベントハンドラです。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnMouseLeftButtonDown(object sender, MouseEventArgs e)
    {
        UIElement el = sender as UIElement;
        if(el != null)
        {
            // ドラッグフラグをtrueにします。
            _isDrag = true;

            // ドラッグ開始時の座標を取得します。
            _dragOffset = e.GetPosition(el);
            _oldXPosition = _dragOffset.X * XRange / ActualWidth + XMin;
            _oldYPosition = -(_dragOffset.Y * YRange / ActualHeight) + YRange / 2 + YCenter;

            // ドラッグ開始時のx、ｙの最小・最大、yの中間値を取得します。
            _dragOffsetXMax = XMax;
            _dragOffsetXMin = XMin;
            _dragOffsetYCenter = YCenter;
            _dragOffsetYMax = YMax;
            _dragOffsetYMin = YMin;

            this.KeyDown += OnEscKeyDown;

            el.CaptureMouse();
        }
    }

    /// <summary>
    /// マウスの左ボタンを離した際に実行されるイベントハンドラです。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnMouseLeftButtonUp(object sender, MouseEventArgs e)
    {
        if (_isDrag == true)
        {
            // ドラッグフラグをfalseにします。
            UIElement el = sender as UIElement;
            el.ReleaseMouseCapture();
            _isDrag = false;

            this.KeyDown -= OnEscKeyDown;
        }
    }

    /// <summary>
    /// キーボードのEscキーを押した際に実行されるイベントハンドラです。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnEscKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            OnEscKeyDowned(_dragOffsetXMax, _dragOffsetXMin, _dragOffsetYMax, _dragOffsetYMin);
        }
    }

    /// <summary>
    /// 現在のドラッグ状態を表します。
    /// </summary>
    private bool _isDrag = false;

    /// <summary>
    /// ドラッグ開始時の座標を表します。
    /// </summary>
    private Point _dragOffset;

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

}
