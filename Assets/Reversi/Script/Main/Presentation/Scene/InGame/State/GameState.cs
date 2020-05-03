using System;
using System.Collections.Generic;
using System.Linq;
using Pommel.Reversi.Domain.InGame;
using UniRx;
using Zenject;

namespace Pommel.Reversi.Presentation.Scene.InGame.State
{
    public interface IGameState
    {
        IEnumerable<IPieceState> PieceStates { get; }

        IObservable<Unit> OnStart { get; }

        void Start(IEnumerable<Piece> pieces);

        void Refresh(IEnumerable<Piece> pieces);
    }

    public sealed class GameState : IGameState
    {
        private readonly IFactory<Point, Color, IPieceState> m_pieceStateFactory;

        private readonly IList<IPieceState> m_pieceStates = new List<IPieceState>();

        private readonly ISubject<Unit> m_onStart = new Subject<Unit>();

        public IEnumerable<IPieceState> PieceStates => m_pieceStates;

        public IObservable<Unit> OnStart => m_onStart;

        public GameState(IFactory<Point, Color, IPieceState> pieceStateFactory)
        {
            m_pieceStateFactory = pieceStateFactory;
        }

        public void Start(IEnumerable<Piece> pieces)
        {
            foreach (var pieceState in pieces.Select(piece => m_pieceStateFactory.Create(piece.Point, piece.Color)))
            {
                m_pieceStates.Add(pieceState);
            }
            m_onStart.OnNext(Unit.Default);
            m_onStart.OnCompleted();
        }

        public void Refresh(IEnumerable<Piece> pieces)
        {
            foreach (var (piece, state) in pieces
                .Join(
                    PieceStates,
                    pieceEntity => pieceEntity.Point,
                    state => state.Point,
                    (pieceEntity, state) => (pieceEntity, state)
                ))
            {
                state.SetColor(piece.Color.Convert());
            }
        }
    }
}