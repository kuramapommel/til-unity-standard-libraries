﻿using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Pommel.Reversi.Presentation.Scene.InGame
{
    public sealed class StoneDispatcher : MonoBehaviour
    {
        [SerializeField]
        private Button m_button;

        [SerializeField]
        private Image m_image;

        private (int x, int y) position;

        public void TurnBlack() => m_image.color = Color.black;

        public void TurnWhite() => m_image.color = Color.white;

        public void None() => m_image.color = Define.NONE_COLOR;

        public bool IsBlack => m_image.color == Color.black;

        public bool IsWhite => m_image.color == Color.white;

        public bool IsNone => m_image.color == Define.NONE_COLOR;

        public void Constructor(int x, int y) => position = (x, y);

        private void Start()
        {
            _ = m_button.OnClickAsObservable()
                .TakeUntilDisable(this)
                .Where(_ => !IsWhite && !IsBlack)
                .Subscribe(_ => Unidux.Dispatch(StoneAction.ActionCreator.Put(position.x, position.y)))
                .AddTo(this);
        }
    }

    public static class StringExtension
    {
        public static Color ToColor(this string self) =>
            ColorUtility.TryParseHtmlString(self, out var color)
                ? color
                : default;
    }

    public static class Define
    {
        public static readonly Color NONE_COLOR = "#13E70E".ToColor();
    }
}
