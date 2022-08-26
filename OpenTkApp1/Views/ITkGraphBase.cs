using System.Windows.Input;

namespace OpenTkApp1.Views
{
    public interface ITkGraphBase 
    {
        void Render();
        void OnMouseMove(object sender, MouseEventArgs e);
        void OnMouseLeftButtonDown(object sender, MouseEventArgs e);
        void OnMouseLeftButtonUp(object sender, MouseEventArgs e);
        void OnEscKeyDown(object sender, KeyEventArgs e);
    }
}
