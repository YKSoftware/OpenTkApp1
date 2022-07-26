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
    public string Title { get; } = "OpenTK で折れ線グラフ";

    // 表示データ
    public double[] XData { get; } = Enumerable.Range(0, _dataNum).Select(x => (double)x).ToArray();

    public double[] YData { get; } = Enumerable.Range(0, _dataNum).Select(x => 50.0 * Math.Sin(2.0 * Math.PI * 4.0 * x / 1000.0)).ToArray();

   

    public double XDataMax { get { return XData.Max(); } }

    public double XDataMin { get { return XData.Min(); } }

    public double SettingXMax { get { return _settingXMax; } }

    private double _settingXMax = 1800;

    public double SettingXMin { get { return _settingXMin; } }

    private double _settingXMin = 500;

    public double SettingYMax { get { return _settingYMax; } }

    private double _settingYMax = 60;

    public double SettingYMin { get { return _settingYMin; } }

    private double _settingYMin = -60;

    // x軸目盛り幅
    public double XScale { get; } = 100.0;

    /// <summary>
    /// x座標の最小値を取得または設定します。
    /// </summary>
    public double XMin
    {
        get { return _xMin; }
        private set { SetProperty(ref this._xMin, value); }
    }

    private double _xMin;

    // x座標最大値
    public double XMax 
    { 
        get { return _xMax; }
        private set { SetProperty(ref this._xMax, value); }
    }
    
    private double _xMax;

    // x座標の描画領域
    public double XRange { get { return (_settingXMax - _settingXMin); } }

    // y軸目盛り幅
    public double YScale { get; } = 30.0;

    // y座標最小値
    public double YMin 
    { 
        get { return _yMin; }
        private set
        {
            if(SetProperty(ref this._yMin, value))
            {
                RaisePropertyChanged(nameof(YCenter));
            }
        }
    }

    private double _yMin;

    // y座標最大値
    public double YMax 
    { 
        get { return _yMax; } 
        private set 
        {
            if (SetProperty(ref this._yMax, value))
            {
                RaisePropertyChanged(nameof(YCenter));
            }
        }
    }
    
    private double _yMax;

    // y座標の中点
    public double YCenter 
    { 
        get { return (_yMax + _yMin) / 2; }
    }

    // y座標の描画領域
    public double YRange { get { return (_settingYMax - _settingYMin); } }

    private const int _dataNum = 1000;

    // プロットのサイズ
    public double PlotSize { get { return _plotSize; } }

    private double _plotSize = 1.0;

    //プロットのタイプ
    public MarkerTypes PlotType { get { return _plotType; } }

    private MarkerTypes _plotType = MarkerTypes.Ellipse;

    // プロットの色
    public Color4 PlotColor { get { return _plotColor; } }

    private Color4 _plotColor = Color4.YellowGreen;

    //プロットの有無
    public bool IsPlot { get { return _isPlot; } }

    private bool _isPlot = false;

    //グラフ線の色
    public Color4 LineColor { get { return _lineColor; } }

    private Color4 _lineColor = Color4.Aqua;

    // マウス移動時
    public Action<double,double> CallBackMouseMoved { get; set; }

    // マウス左クリック時
    public Action<double,double> CallBackMouseLeftButtonDowning { get; set; }

    //　現在のx座標
    public double CurrentXPotition
    {
        get { return _currentXPotition; }
        set { SetProperty(ref this._currentXPotition, value); }
    }

    private double _currentXPotition ;

    // 現在のy座標
    public double CurrentYPotition 
    { 
        get { return _currentYPotition; }
        set { SetProperty (ref this._currentYPotition, value); }
    }

    private double _currentYPotition;

    // 座標取得
    private void GetPotition(double x, double y)
    {
        CurrentXPotition = x;
        CurrentYPotition = y;
    }

    // 描画領域平行移動
    private void ViewTranslate(double x, double y)
    {
        if(SettingXMax <= XDataMax && SettingXMin >= XDataMin)
        {
            if (XMax <= XDataMax && XMin >= XDataMin)
            {
                XMax = XMax - x;
                XMin = XMin - x;
            }

            if (XMax >= XDataMax)
            {
                XMax = XDataMax;
                XMin = XDataMax - XRange;
            }

            if (XMin <= XDataMin)
            {
                XMax = XDataMin + XRange;
                XMin = XDataMin;
            }

        }

        else
        {
            XMax = XMax - x;
            XMin = XMin - x;
        }
  
        //YMax = YMax + y;
        //YMin = YMin + y;
    }

    public MainViewModel()
    {
        this.CallBackMouseMoved = GetPotition;
        this.CallBackMouseLeftButtonDowning = ViewTranslate;
        this.XMax = SettingXMax;
        this.XMin = SettingXMin;
        this.YMax = SettingYMax;
        this.YMin = SettingYMin;
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