using System.Windows.Input;

namespace OpenTkApp1.Views
{
    public interface ITkGraphBase 
    {
        public double XScale { get; set; }
        public double YScale { get; set; }
        public double XRange { get; set; }
        public double YRange { get; set; }

        void Render();
        void OnMouseMove(object sender, MouseEventArgs e);
        void OnMouseLeftButtonDown(object sender, MouseEventArgs e);
        void OnMouseLeftButtonUp(object sender, MouseEventArgs e);
        void OnEscKeyDown(object sender, KeyEventArgs e);
    }
}
