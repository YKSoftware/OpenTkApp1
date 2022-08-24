using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using OpenTK.Mathematics;
using OpenTkApp1.Views;

namespace OpenTkApp1.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{

    /// <summary>
    /// 新しいインスタンスを生成します。
    /// </summary>
    public MainViewModel()
    {
        this.CallBackMouseMoved = GetPotition;
        this.CallBackMouseLeftButtonDowning = TranslateView;
        this.CallBackEscKeyDowned = TranslateDragPreviewView;
        this.XMax = SettingXMax;
        this.XMin = SettingXMin;
        this.YMax = SettingYMax;
        this.YMin = SettingYMin;
    }

    public string Title { get; } = "OpenTK で折れ線グラフ";

    // 表示データ
    public double[] XData { get; } = Enumerable.Range(0, _dataNum).Select(x => (double)x).ToArray();

    public double[] YData { get; } = Enumerable.Range(0, _dataNum).Select(x => 50.0 * Math.Sin(2.0 * Math.PI * 4.0 * x / 1000.0)).ToArray();

    /// <summary>
    /// 初めに設定するx座標の最大値を取得または設定します。
    /// </summary>
    public double SettingXMax { get { return _settingXMax; } }

    private double _settingXMax = 700;

    /// <summary>
    /// 初めに設定するx座標の最小値を取得または設定します。
    /// </summary>
    public double SettingXMin { get { return _settingXMin; } }

    private double _settingXMin = 200;

    /// <summary>
    /// 初めに設定するy座標の最大値を取得または設定します。
    /// </summary>
    public double SettingYMax { get { return _settingYMax; } }

    private double _settingYMax = 60;

    /// <summary>
    /// 初めに設定y座標の最小値を取得または設定します。
    /// </summary>
    public double SettingYMin { get { return _settingYMin; } }

    private double _settingYMin = -60;

    /// <summary>
    /// x座標の目盛り幅を取得します。
    /// </summary>
    public double XScale { get; } = 100.0;

    /// <summary>
    /// 現在の範囲のx座標の最小値を取得または設定します。
    /// </summary>
    public double XMin
    {
        get { return Math.Round(_xMin, DisplayDigits); }
        private set { SetProperty(ref this._xMin, value); }
    }

    private double _xMin;

    /// <summary>
    /// 現在の範囲のx座標の最大値を取得または設定します。
    /// </summary>
    public double XMax 
    { 
        get { return Math.Round(_xMax, DisplayDigits); }
        private set { SetProperty(ref this._xMax, value); }
    }
    
    private double _xMax;

    /// <summary>
    /// 現在のx座標の範囲を取得します。
    /// </summary>
    public double XRange { get { return (_settingXMax - _settingXMin); } }

    /// <summary>
    /// 現在のy座標の目盛り幅を取得します。
    /// </summary>
    public double YScale { get; } = 30.0;

    /// <summary>
    /// 現在の範囲のy座標の最小値を取得または設定します。
    /// </summary>
    public double YMin 
    { 
        get { return Math.Round(_yMin, DisplayDigits); }
        private set
        {
            if(SetProperty(ref this._yMin, value))
            {
                RaisePropertyChanged(nameof(YCenter));
            }
        }
    }

    private double _yMin;

    /// <summary>
    /// 現在の範囲のy座標の最大値を取得または設定します。
    /// </summary>
    public double YMax 
    { 
        get { return Math.Round(_yMax, DisplayDigits); } 
        private set 
        {
            if (SetProperty(ref this._yMax, value))
            {
                RaisePropertyChanged(nameof(YCenter));
            }
        }
    }
    
    private double _yMax;

    /// <summary>
    /// 現在の範囲のy座標の中間値を取得します。
    /// </summary>
    public double YCenter 
    { 
        get { return Math.Round((_yMax + _yMin) / 2, DisplayDigits); }
    }

    /// <summary>
    /// y座標の範囲を取得します。
    /// </summary>
    public double YRange { get { return (_settingYMax - _settingYMin); } }

    /// <summary>
    /// グラフの表示桁数を取得または設定します。
    /// </summary>
    public int DisplayDigits { get { return _displayDigits; } }

    private int _displayDigits = 1;

    /// <summary>
    /// データの個数を表します。
    /// </summary>
    private const int _dataNum = 1000;

    /// <summary>
    /// プロットのサイズを取得します。
    /// </summary>
    public double PlotSize { get { return _plotSize; } }

    private double _plotSize = 1.0;

    /// <summary>
    /// プロットの形を取得します。
    /// </summary>
    public MarkerTypes PlotType { get { return _plotType; } }

    private MarkerTypes _plotType = MarkerTypes.Ellipse;

    /// <summary>
    /// プロットの色を取得します。
    /// </summary>
    public Color4 PlotColor { get { return _plotColor; } }

    private Color4 _plotColor = Color4.YellowGreen;

    /// <summary>
    /// プロットの有無を取得します。
    /// </summary>
    public bool IsPlot { get { return _isPlot; } }

    private bool _isPlot = false;

    /// <summary>
    /// グラフの線の色を取得します。
    /// </summary>
    public Color4 LineColor { get { return _lineColor; } }

    private Color4 _lineColor = Color4.Aqua;

    /// <summary>
    /// 軸の位置を取得します。
    /// </summary>
    public AxisTypes AxisType { get { return _axisType; } }

    private AxisTypes _axisType = AxisTypes.Left;

    /// <summary>
    /// マウス移動時に呼びだされる関数を取得または設定します。
    /// </summary>
    public Action<double,double> CallBackMouseMoved { get; set; }

    /// <summary>
    /// マウス左クリック時に呼びだされる関数を取得または設定します。
    /// </summary>
    public Action<double,double> CallBackMouseLeftButtonDowning { get; set; }

    /// <summary>
    /// ドラッグ中にEsckeyが押された時に呼びだされる関数を取得または設定します。
    /// </summary>
    public Action<double, double, double, double> CallBackEscKeyDowned { get; set; }

    /// <summary>
    /// マウスポインタのx座標を取得または設定します。
    /// </summary>
    public double CurrentXPotition
    {
        get { return _currentXPotition; }
        set { SetProperty(ref this._currentXPotition, value); }
    }

    private double _currentXPotition;

    /// <summary>
    /// マウスポインタのy座標を取得または設定します。
    /// </summary>
    public double CurrentYPotition 
    { 
        get { return _currentYPotition; }
        set { SetProperty (ref this._currentYPotition, value); }
    }

    private double _currentYPotition;

    /// <summary>
    /// マウスポインタの座標を取得するメソッドです。
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void GetPotition(double x, double y)
    {
        CurrentXPotition = x;
        CurrentYPotition = y;
    }

    /// <summary>
    /// x,yの最大・最小を変化させることで描画領域を移動するメソッドです。
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void TranslateView(double x, double y)
    {
        XMax = XMax - x;
        XMin = XMin - x;
        YMax = YMax - y;
        YMin = YMin - y;
    }

    private void TranslateDragPreviewView(double oldXMax, double oldXMin, double oldyMax, double oldyMin)
    {
        XMax = oldXMax;
        XMin = oldXMin;
        YMax = oldyMax;
        YMin = oldyMin;
    }

    #region INotifyPropertyChanged の実装

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void RaisePropertyChanged([CallerMemberName]string? propertyName = null) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    protected bool SetProperty<T>(ref T target, T value, [CallerMemberName]string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(target, value)) return false;
        target = value;
        RaisePropertyChanged(propertyName);
        return true;
    }

    #endregion INotifyPropertyChanged の実装
}