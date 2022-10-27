using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTkApp1.Views
{
    public class TkGraphCursor
    {

        /// <summary>
        /// カーソルの高さを取得、設定します。
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// カーソルのx座標を取得、設定します。
        /// </summary>
        public double XPosition { get; set; }

        /// <summary>
        /// マウスカーソルがグラフカーソル上にあるかどうかを取得、設定します。
        /// </summary>
        public bool OnCursor { get; set; }

        /// <summary>
        /// グラフカーソルがドラッグ中かどうかを取得、設定します。
        /// </summary>
        public bool IsDrag { get; set; }
    }
}
