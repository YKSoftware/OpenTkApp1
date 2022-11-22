using OpenTkApp1.Views.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTkApp1.Views
{
    /// <summary>
    /// グラフカーソルに関するクラスです。
    /// </summary>
    public class TkGraphCursor : ItemPosition
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
        /// カーソルの移動する前の位置を取得、設定します。
        /// </summary>
        public double OldPosition { get; set; }

        /// <summary>
        /// カーソルの移動量を取得、設定します。
        /// </summary>
        public double Translate { get; set; }

        /// <summary>
        /// window座標から変換済みのグラフカーソルの移動量を取得、設定します。
        /// </summary>
        public double CorrdinateTranslate { get; set; }

    }
}
