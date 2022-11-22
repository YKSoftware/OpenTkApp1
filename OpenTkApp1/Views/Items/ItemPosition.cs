﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTkApp1.Views.Items
{
    /// <summary>
    /// グラフに表示されたオブジェクトの座標移動に関するクラスです。
    /// </summary>
    public class ItemPosition
    {
        /// <summary>
        /// x座標の移動量を取得、設定します。
        /// </summary>
        public double XTranslate { get; set; }

        /// <summary>
        /// y座標の移動量を取得、設定します。
        /// </summary>
        public double YTranslate { get; set; }

        /// <summary>
        /// 原点からのx座標のオフセットを取得、設定します。
        /// </summary>
        public double XOffset { get; set; }

        /// <summary>
        /// 原点からのy座標のオフセットを取得、設定します。
        /// </summary>
        public double YOffset { get; set; }

        /// <summary>
        /// windowサイズ変更前に保持するx座標の移動量を取得、設定します。
        /// </summary>
        public double SaveXTranslate { get; set; }

        /// <summary>
        /// windowサイズ変更前に保持するy座標の移動量を取得、設定します。
        /// </summary>
        public double SaveYTranslate { get; set; }

        /// <summary>
        /// windowサイズが変更された時のx座標の移動量を取得、設定します。
        /// </summary>
        public double WindowSizeChangedXTranslate { get; set; }

        /// <summary>
        /// windowサイズが変更された時のy座標の移動量を取得、設定します。
        /// </summary>
        public double WindowSizeChangedYTranslate { get; set; }

        /// <summary>
        /// マウスカーソルがオブジェクト上にあるかどうかを取得、設定します。
        /// </summary>
        public bool OnCursor { get; set; }

        /// <summary>
        /// ドラッグ中かどうかを取得、設定します。
        /// </summary>
        public bool IsDrag { get; set; }
    }
}
