using System.Windows.Input;

namespace OpenTkApp1.Views
{
    public interface ITkGraphBase 
    {
        void Render();
        void OnMouseMove(object sender, MouseEventArgs e);
        void OnMouseLeftButtonDowned(object sender, MouseEventArgs e);
        void OnMouseLeftButtonUpped(object sender, MouseEventArgs e);
        void OnEscKeyDowned(object sender, KeyEventArgs e);
    }
}
