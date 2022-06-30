namespace OpenTkApp1.Views;

/// <summary>
/// OpenTK による描画をおこなう実装を表します。
/// </summary>
public interface ITkGraphicsItem
{
    /// <summary>
    /// 描画処理をおこないます。
    /// </summary>
    void Render();
}
