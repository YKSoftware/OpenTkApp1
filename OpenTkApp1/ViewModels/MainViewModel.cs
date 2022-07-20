using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

    // x軸目盛り幅
    public double XScale { get; } = 100.0;

    // x座標最小値
    public double XMin { get { return _xMin; } }

    private double _xMin = 950.0;

    // x座標最大値
    public double XMax { get { return _xMax; } }
    
    private double _xMax = 1000.0;

    // x座標の描画領域
    public int XRange { get { return (int)(_xMax - _xMin); } }

    // y軸目盛り幅
    public double YScale { get; } = 30.0;


    // y座標最小値
    public double YMin { get { return _yMin; } }

    private double _yMin = -60.0;

    // y座標最大値
    public double YMax { get { return _yMax; } }
    
    private double _yMax = 60.0;

    // y座標の中点
    public double YCenter { get { return (_yMin + _yMax) / 2; } }

    // y座標の描画領域
    public int YRange { get { return (int)(_yMax - _yMin); } }

    private const int _dataNum = 1000;

    // プロットのサイズ
    public double PlotSize { get { return _plotSize; } }

    private double _plotSize = 1.0;

    //プロットのタイプ
    public MarkerTypes PlotType { get { return _plotType; } }

    private MarkerTypes _plotType = MarkerTypes.Triangle;

    // プロットの色
    public Color4 PlotColor { get { return _plotColor; } }
    private Color4 _plotColor = Color4.Crimson;
    //プロットの有無
    public bool IsPlot { get { return _isPlot; } }

    private bool _isPlot = true;

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