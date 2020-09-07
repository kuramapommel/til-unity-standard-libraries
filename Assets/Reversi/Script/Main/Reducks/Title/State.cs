using System;
using Pommel.Reversi.Domain.Title;
using Unidux;

namespace Pommel.Reversi.Reducks.Title
{
    public interface IProps
    {
        bool IsDisplayGameStartModal { get; }

        ValueObjects.Player Player { get; }

        string RoomId { get; }
    }

    public abstract class TitleState : StateElement
    {
        public abstract bool IsDisplayGameStartModal { get; set; }

        public abstract ValueObjects.Player Player { get; set; }

        public abstract string RoomId { get; set; }
    }

    public static class State
    {
        private static readonly Lazy<Impl> m_instance = new Lazy<Impl>(() => new Impl());

        public static TitleState Element => m_instance.Value;

        public static IProps Props => m_instance.Value;

        private sealed class Impl : TitleState, IProps
        {
            public override bool IsDisplayGameStartModal { get; set; } = false;

            public override ValueObjects.Player Player { get; set; } = new ValueObjects.Player(string.Empty, string.Empty);

            public override string RoomId { get; set; } = string.Empty;
        }
    }
}