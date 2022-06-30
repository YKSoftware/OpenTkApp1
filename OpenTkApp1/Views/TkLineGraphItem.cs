using System.Windows;

namespace OpenTkApp1.Views;

/// <summary>
/// 折れ線グラフを描画する機能を提供します。
/// </summary>
public class TkLineGraphItem : FrameworkElement, ITkGraphicsItem
{
    #region XData

    /// <summary>
    /// XData 依存関係プロパティの定義を表します。
    /// </summary>
    public static readonly DependencyProperty XDataProperty = DependencyProperty.Register("XData", typeof(double[]), typeof(TkLineGraphItem), new PropertyMetadata(null, OnXDataPropertyChanged));

    /// <summary>
    /// 横軸データを取得または設定します。
    /// </summary>
    public double[]? XData
    {
        get => (double[]?)GetValue(XDataProperty);
        set => SetValue(XDataProperty, value);
    }

    /// <summary>
    /// XData プロパティ変更イベントハンドラ
    /// </summary>
    /// <param name="d">イベント発行元</param>
    /// <param name="e">イベント引数</param>
    private static void OnXDataPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion XData

    #region YData

    /// <summary>
    /// YData 依存関係プロパティの定義を表します。
    /// </summary>
    public static readonly DependencyProperty YDataProperty = DependencyProperty.Register("YData", typeof(double[]), typeof(TkLineGraphItem), new PropertyMetadata(null, OnYDataPropertyChanged));

    /// <summary>
    /// 縦軸データを取得または設定します。
    /// </summary>
    public double[]? YData
    {
        get => (double[]?)GetValue(YDataProperty);
        set => SetValue(YDataProperty, value);
    }

    /// <summary>
    /// YData プロパティ変更イベントハンドラ
    /// </summary>
    /// <param name="d">イベント発行元</param>
    /// <param name="e">イベント引数</param>
    private static void OnYDataPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        (d as TkLineGraphItem)?.Render();
    }

    #endregion YData

    /// <summary>
    /// 描画処理をおこないます。
    /// </summary>
    public void Render()
    {
        if (this.XData is null) return;
        if (this.YData is null) return;

        // ToDo: XData と YData を用いてグラフを描画する
    }
}