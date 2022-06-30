using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OpenTkApp1.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    public string Title { get; } = "OpenTK で折れ線グラフ";

    public double[] XData { get; } = Enumerable.Range(0, _dataNum).Select(x => (double)x).ToArray();

    public double[] YData { get; } = Enumerable.Range(0, _dataNum).Select(x => 5.0 * Math.Sin(2.0 * Math.PI * 4.0 * x / 1000.0)).ToArray();

    private const int _dataNum = 1000;

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